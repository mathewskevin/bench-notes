# https://www.geeksforgeeks.org/build-a-basic-text-editor-using-tkinter-in-python/
from tkinter import *
from tkinter.ttk import *
from datetime import datetime
import pyautogui as pygui

import pdb

root = Tk()

# This function is used to
# display time on the label
def screen_shot():
	now = datetime.now()
	now_str = now.strftime('%y%m%d%H%M%S')	
	png_name = 'screenshot_{now_str}.png'.format(now_str = now_str)
	pygui.screenshot(png_name)

def reset_time():
	global now_string
	now = datetime.now()
	now_string = now.strftime('%m/%d/%Y %H:%M:%S')
	
# This function is used to  
# display time on the label 
def time_calc():
    reset_time()
    lbl.config(text = now_string)
    lbl.after(1000, time_calc)
	
reset_time()
root.title('Bench Notes / Setup')
root.wm_attributes("-topmost", 1)
root.geometry("350x150")
#root.minsize(height=250, width=350)
#root.maxsize(height=250, width=350)

frame = Frame(root)
frame.pack(side=BOTTOM, fill=X)

# reset button
btn = Button(frame, text ="Screenshot", command = screen_shot)

lbl = Label(frame, font = ('arial', 12),
			background = 'white',
			foreground = 'black')

lbl_buf = Label(frame, font = ('arial', 12),
			background = 'white',
			foreground = 'black')

# https://stackoverflow.com/questions/18550710/pack-labels-right-next-to-entry-box-in-tkinter-python
btn.pack(side=LEFT)
#lbl_buf.pack(side=LEFT, anchor='center', fill=BOTH)
lbl.pack(side=RIGHT)

bottomframe = Frame(root)
bottomframe.pack(side=TOP, fill=BOTH)

# packing scrollbar
scrollbar = Scrollbar(bottomframe)
scrollbar.pack(side=RIGHT, fill=Y)

text_info = Text(bottomframe, yscrollcommand=scrollbar.set)
text_info.pack(fill=BOTH)
scrollbar.config(command=text_info.yview) # configuring the scrollbar

time_calc()
mainloop()