import sys
import re
import os
from asterisk.ami import AMIClient
from asterisk.ami import SimpleAction
import configparser
from error import error_massage

config = configparser.ConfigParser()
config.read(os.environ['USERPROFILE'] + "\\AppData\\Roaming\\Light-asterisk-caller\\settings.ini")

def check_phone(tel: str):
    if tel is None: return False
    else:
        internal = config['ASTERISK']["INTERNAL"]

        find = re.compile(r'^[\+]?[0-9]{1,3}?[ ]?[(]?[0-9]{3}[)]?[ ]?[0-9-]{7,9}$')
        find_internal = re.compile('^[0-9]{'+str(internal)+'}$')
        
        match_phone = find.match(tel)
        match_internal_phone = find_internal.match(tel)

        if match_phone is not None or match_internal_phone is not None: return True
        else: return False

def make_call(tel: str):
    if check_phone(tel) is False: 
        error_massage("Wrong phone number!")
        quit()
    else:
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

if sys.argv[1] is not None:
    tel = sys.argv[1]
    tel = tel.replace('tel:','') 
    tel = tel.replace(' ','') 
    tel = tel.replace('(','')
    tel = tel.replace(')','')
    tel = tel.replace('-','') 
    if check_phone(tel) is False: error_massage("Wrong phone number!")
    else: make_call(tel)