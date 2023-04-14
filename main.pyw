import pystray
import sys
from PIL import Image
from pystray import MenuItem as item
from pystray import Icon as icon, Menu as menu, MenuItem as item
from listener import listen_sip
from call import call_out_massage

#--------------------TRAY--------------------#
def close():
    sys.exit()

def tray():
    icon = pystray.Icon('icon')
    image = Image.open("favicon.ico")

    icon.icon = image
    icon.menu = menu(item('Make call', lambda: call_out_massage()),
                    item('Close app', lambda: quit())
                    )

    def setup(icon):
        icon.visible = True

    icon.run(setup)

#------------------TRAY END------------------#

def main():
    tray() #show icon with menu in tray
   # listen_sip() #listen for calls and show massages

main()