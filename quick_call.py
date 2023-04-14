import sys
import re
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

def make_call(tel: str):
    if check_phone(tel) is False: 
        print("Wrong phone number!")
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
        requests.get(url, cookies=session.cookies.get_dict())

if sys.argv[1] is not None:
    tel = sys.argv[1]
    tel = tel.replace('tel:','') 
    tel = tel.replace(' ','') 
    tel = tel.replace('(','')
    tel = tel.replace(')','')
    tel = tel.replace('-','') 
    if check_phone(tel) is False: error_massage("Wrong phone number!")
    else: make_call(tel)