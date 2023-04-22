import pystray
import sys
import os
from PIL import Image
from pystray import MenuItem as item
from pystray import Icon as icon, Menu as menu, MenuItem as item
import asyncio
from listener import listen_sip
from call import call_out_massage

#--------------------TRAY--------------------#

async def tray():

    def stop():
        icon.stop()
        end()
        
    icon = pystray.Icon('icon')
    image = Image.open(os.environ['USERPROFILE'] + "\\AppData\\Roaming\\Light-asterisk-caller\\ico\\favicon.ico")

    icon.icon = image
    icon.menu = menu(item('Make call', lambda: call_out_massage()),
                    item('Close app', lambda: stop())
                    )
    
    def setup(icon):
        icon.visible = True

    icon.run(setup)

#------------------TRAY END------------------#

async def main():
    task1 = asyncio.create_task(tray())
    task2 = asyncio.create_task(listen_sip())
    # планируем одновременные вызовы:
    global tasks
    tasks = asyncio.gather(task1, task2)
    #await tasks

    global end
    def end():
        task2.cancel()
        sys.exit()

    print("end")

if __name__ == '__main__':
    asyncio.run(main())