import re
from tkinter import *
from tkinter import ttk
import tkinter as tk
from asterisk.ami import AMIClient
from asterisk.ami import SimpleAction
from ldap3 import Server, Connection, ALL
import configparser
from error import error_massage

config = configparser.ConfigParser()
config.read("settings.ini", encoding="utf-8")

def check_phone(tel: str):
    print(tel)
    if tel is None: return False
    else:
        config = configparser.ConfigParser()
        config.read("settings.ini")

        internal = config['ASTERISK']["INTERNAL"]

        find = re.compile(r'^[\+]?[0-9]{1,3}?[ ]?[(]?[0-9]{3}[)]?[ ]?[0-9-]{7,9}$')
        find_internal = re.compile('^[0-9]{'+str(internal)+'}$')
        
        match_phone = find.match(tel)
        match_internal_phone = find_internal.match(tel)

        if match_phone is not None or match_internal_phone is not None: return True
        else: return False

def ldap_search(string: str, i: str):
    server = Server(config['LDAP']["URL"])
    conn = Connection(server, config['LDAP']["USER"], config['LDAP']["PASSWORD"], auto_bind=True)
    
    tel_filter = '(cn='+string+')'
    tel_attr = ['telephoneNumber']
    names_filter = '(&(objectCategory=person)(objectClass=user))'
    names_attr = ['cn']

    if i == "tel": 
        Filter = tel_filter
        Attr = tel_attr
    elif i == "names": 
        Filter = names_filter
        Attr = names_attr

    conn.search(config['LDAP']["BASE"], Filter , attributes=Attr)
    names = []
    if i == "tel": 
        for entry in conn.entries:
            return entry['telephoneNumber']
    elif i == "names": 
        for entry in conn.entries:
            names += entry['cn']
        return names

def call_out_massage():
    massage = tk.Tk()
    screen_width = massage.winfo_screenwidth()  # Width of the screen
    screen_height = massage.winfo_screenheight() # Height of the screen
    if config['LDAP']["ACTIVE"] == "True":
        x = (screen_width/2) - (350/2)
        y = (screen_height/2) - (180/2)
        massage.geometry('%dx%d+%d+%d' % (350, 180, x, y))
    else:
        x = (screen_width/2) - (350/2)
        y = (screen_height/2) - (150/2)
        massage.geometry('%dx%d+%d+%d' % (350, 150, x, y))
    massage.iconbitmap("ico/call_out.ico")
    massage.title("Make call")

    Label(massage, text="Enter phone number:", font=('Helvetica 12 bold')).pack(pady=5)

    tel = ttk.Entry(massage,width=30)
    tel.pack(pady=10)

    if config['LDAP']["ACTIVE"] == "True":
        names = ldap_search('','names') #get names from LDAP

        combobox = ttk.Combobox(values=names,state="readonly")
        combobox.pack(pady=5)

        def selected(event):
            selection = combobox.get()
            tel.delete(0,'')
            tel.insert(0,ldap_search(selection,'tel')) #insert to entry tel of selected person

        def clear_combobox():
            tel.delete(0,'')
            combobox.set('')


        combobox.bind("<<ComboboxSelected>>", selected)

        ttk.Button(massage,
                width=25,
                text='Clear', 
                command=clear_combobox
                ).pack()
        
    def enter(event):
        make_call(tel.get())
        
    massage.bind("<Return>",enter)

    ttk.Button(massage,
               width=25,
               text='Call', 
               command=lambda: enter
               ).pack()
        
    massage.mainloop()

def make_call(tel: str):
    if check_phone(tel) is False: 
        error_massage("Wrong phone number!")
        quit()
    else:
        config = configparser.ConfigParser()
        config.read("settings.ini")

        client = AMIClient(address=config['AMI']["URL"],port=int(config['AMI']["PORT"]))
        client.login(username=config['AMI']["USERNAME"],secret=config['AMI']["SECRET"],callback=10)

        action = SimpleAction(
            'Originate',
            Channel=config['SIP']["CHANNEL"],
            Exten=tel,
            Priority=1,
            Context=config['SIP']["CONTEXT"],
            CallerID=config['SIP']["CALLER_ID"],
        )
        client.send_action(action)