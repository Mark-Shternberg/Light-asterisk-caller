from tkinter import *
from tkinter import ttk
import os

def error_massage(error):
    massage = Tk()
    screen_width = massage.winfo_screenwidth()  # Width of the screen
    screen_height = massage.winfo_screenheight() # Height of the screen
    x = (screen_width/2) - (350/2)
    y = (screen_height/2) - (150/2)
    massage.geometry('%dx%d+%d+%d' % (350, 130, x, y))
    massage.iconbitmap(os.environ['USERPROFILE'] + "\\AppData\\Roaming\\Light-asterisk-caller\\ico\\x.ico")
    massage.title("Error")

    Label(massage, text=error, font=('Helvetica 12 bold')).pack(pady=20)

    ttk.Button(massage, text='OK', command= massage.destroy).pack()

    def close(event):
        massage.destroy

    massage.bind("<Return>",close)
    
    massage.mainloop()