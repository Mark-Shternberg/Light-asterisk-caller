from tkinter import *
from tkinter import ttk
import tkinter
import configparser
import telnetlib
import json
import re
import io
import os
from PIL import Image, ImageTk
from ldap3 import Server, Connection, ALL

config = configparser.ConfigParser()
config.read(os.environ['USERPROFILE'] + "\\AppData\\Roaming\\Light-asterisk-caller\\settings.ini", encoding="utf-8")

def massage(call_tel, call_from, call_photo, call_desc, Caller_post):
    massage = Tk()
    screen_width = massage.winfo_screenwidth()  # Width of the screen
    screen_height = massage.winfo_screenheight() # Height of the screen
    x = (screen_width) - (380)
    y = (screen_height) - (220)
    massage.geometry('%dx%d+%d+%d' % (350, 130, x, y))
    massage.iconbitmap(os.environ['USERPROFILE'] + "\\AppData\\Roaming\\Light-asterisk-caller\\ico\\call_in.ico")
    massage.title("Incoming call - " + call_tel)

    if call_photo is None or call_photo == '':
        image = Image.open('ico/no_photo.png')
    else:
        image = Image.open(io.BytesIO(call_photo))
        
    image = image.resize((80,80), Image.ANTIALIAS)
    img = ImageTk.PhotoImage(image)
    canvas = tkinter.Canvas(massage, height=80, width=80)
    image = canvas.create_image(0, 0, anchor='nw',image=img)

    if call_from is None or call_from == '': call_from = call_tel

    frame_text = Frame(massage)
    canvas.place(relx=0.10,rely=0.15)

    Label(frame_text, text=call_from, font=('Helvetica 12 bold')).pack(pady=1)
    Label(frame_text, text=Caller_post, font=('Helvetica 10'), wraplength=140).pack(pady=1)
    Label(frame_text, text=call_desc, font=('Helvetica 10')).pack(pady=5)

    frame_text.pack(expand=1, padx=(90,0))

    massage.mainloop()

def ldap_search(tel: str, i):
    server = Server(config['LDAP']["URL"])
    conn = Connection(server, config['LDAP']["USER"], config['LDAP']["PASSWORD"], auto_bind=True)
    
    tel_filter = '(|(telephoneNumber='+tel+')(homePhone='+tel+')(mobile='+tel+'))'

    conn.search(config['LDAP']["BASE"], tel_filter , attributes=['cn','thumbnailPhoto', 'description', 'title'])
    for entry in conn.entries:
        if i == "cn": return str(entry['cn'])
        elif i == "img": return entry['thumbnailPhoto']
        elif i == "desc": return str(entry['description'])
        elif i == "title": return str(entry['title'])

async def listen_sip():
    ##
    HOST = config['AMI']["URL"]
    PORT = config['AMI']["PORT"]
    user = config['AMI']["USERNAME"]
    password = config['AMI']["SECRET"]
    pc_phone = config['SIP']["CALLER_ID"]
    ##

    tn = telnetlib.Telnet(HOST,PORT)
    tn.write("Action: login".encode('ascii') + b"\n")
    username = "Username: " + user
    tn.write(username.encode('ascii') + b"\n")
    passWord = "Secret: " + password
    string_NOW = ''
    string_out = ''
    cd = 0

    tn.write(passWord.encode('ascii') + b"\n\n")

    def telnet_for_string(string):
        global string_out
        string_out_def = ''
        for mes in string:
            try:
                if string[mes]['Event'] == 'NewConnectedLine' and string[mes]['ChannelStateDesc'] == 'Ring' and string[mes]['ConnectedLineNum'] == pc_phone:
                    CallerIDNum = string[mes]['CallerIDNum']
                    CallerIDName = string[mes]['CallerIDName']
                    Caller_name = ldap_search(CallerIDNum, 'cn')
                    Caller_photo = ldap_search(CallerIDNum, 'img')
                    Caller_description = ldap_search(CallerIDNum, 'desc')
                    Caller_post = ldap_search(CallerIDNum, 'title')

                    if config['LDAP']["ACTIVE"] == "True" and Caller_name is None:
                        massage(CallerIDNum, Caller_name, Caller_photo, Caller_description, Caller_post)
                    else: massage(CallerIDNum,CallerIDName, None, None, None)
            except UnboundLocalError:
                1+1
            except KeyError:
                1+1
        if string_out_def:
            string_out = string_out_def

    while True:
        string = ''
        event_string = ''
        elements_string = ''
        c = 0

        read_some = tn.read_some()  # Получаем строчку из AMI

        string = read_some.decode('utf8', 'replace').replace('\r\n', '#')   # Декодируем строчки и заменяем переносы строк на #

        # Отлавливаем начало строки и склеиваем строчку
        if not string.endswith('##'):
            string_NOW = string_NOW + string

        # Если строчка закончилась, то доклеиваем конец строки и
        # совершаем магию, которая двойной перенос строки в середине строки заменит на $,
        # а все одинарные переносы заменит на #, так-же удалим кавычки и обратные слеши
        if string.endswith('##'):
            string_NOW = string_NOW + string
            string_NOW = string_NOW.replace('##', '$')  # заменяем двойной перенос строки на $
            string_NOW = string_NOW.replace('\n', '#')  # Заменяем  перенос на #
            string_NOW = string_NOW.replace('\r', '#')  # Заменяем  перенос на #
            string_NOW = string_NOW.replace('"', '')    # Удаляем кавычки
            string_NOW = string_NOW.replace('\\', '')   # удаляем обратный слеш

            # Делим полученую строчку на Евенты т.к. двойной перенос как раз её так и делил
            events = re.findall(r'[A-Z][\w]+:\s[^$]+', string_NOW)
            for event in events:
                c+=1

                event_elements = re.findall(r'[A-Z][\w]+:\s[^#]+', event)   # А тут делим евенты на елемены
                for element in event_elements:
                    element = '\"' + element.replace(': ', '\": "') + '\", '# Вручную делаем словарь
                    elements_string = elements_string + element # Склеиваем строчки обратно, получаем словарь

                event_string = event_string + '\"' + str(c) + '\": ' + '{' + elements_string + '}'
                event_string = event_string.replace('}{', '},{')    #   Добавляем запятую между евентами
                event_string = event_string.replace(', }', '}, ')   #
            event_string = '{' + event_string + '}'
            event_string = event_string.replace('}, }', '}}')

            # Превращаем полученую строчку в json, если вдруг есть ошибка в синтаксисе json, то выводим как сам невалидный
            # json, так и строчку  из которой не получилось его собрать.
            try:
                parsed_string = json.loads(event_string)
            except json.decoder.JSONDecodeError:
                print('')
                #print(event_string, '\n\n\n')
                #print(string_NOW, '\n\n\n')
                #print('')

            telnet_for_string(parsed_string)
            string_NOW = '' 
    