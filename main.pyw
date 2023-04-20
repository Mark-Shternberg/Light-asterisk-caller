import pystray
import sys
from PIL import Image
from pystray import MenuItem as item
from pystray import Icon as icon, Menu as menu, MenuItem as item
from multiprocessing import Process
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

def run_parallel(*functions):
    '''
    Run functions in parallel
    '''
    processes = []
    for function in functions:
        proc = Process(target=function)
        proc.start()
        processes.append(proc)
    for proc in processes:
        proc.join()

def main():
    if __name__ == '__main__':
        run_parallel(tray, listen_sip)

main()