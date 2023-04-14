import re
from tkinter import *
from tkinter import ttk
import tkinter as tk
import requests
import configparser
from error import error_massage

config = configparser.ConfigParser()
config.read("settings.ini")

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

def call_out_massage():
    massage = tk.Tk()
    screen_width = massage.winfo_screenwidth()  # Width of the screen
    screen_height = massage.winfo_screenheight() # Height of the screen
    x = (screen_width/2) - (350/2)
    y = (screen_height/2) - (150/2)
    massage.geometry('%dx%d+%d+%d' % (350, 130, x, y))
    massage.iconbitmap("ico/call.ico")
    massage.title("Make call")

    Label(massage, text="Enter phone number:", font=('Helvetica 12 bold')).pack(pady=5)

    tel = ttk.Entry(massage,width=30)
    tel.pack(pady=10)

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

        URL=config['AMI']["URL"]
        PORT=config['AMI']["PORT"]
        USERNAME=config['AMI']["USERNAME"]
        SECRET=config['AMI']["SECRET"]

        CHANNEL=config['SIP']["CHANNEL"]
        CONTEXT=config['SIP']["CONTEXT"]
        CALLER_ID=config['SIP']["CALLER_ID"]

        session = requests.Session()
        session.get('http://'+URL+':'+PORT+'/rawman?action=login&username='+USERNAME+'&secret='+SECRET+'')
        url = 'http://'+URL+':'+PORT+'/rawman?action=Originate&Channel='+CHANNEL+'&Context='+CONTEXT+'&Priority=1&Exten='+tel+'&CallerID='+CALLER_ID
        requests.get(url, cookies=session.cookies.get_dict(), timeout=1)