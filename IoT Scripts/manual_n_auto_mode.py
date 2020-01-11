# Importing the necessary librarirs
from Adafruit_IO import *
from gpiozero import LED
from gpiozero import MotionSensor

# aio = Client(user_name, AIO key)
aio = Client('moitbytes', 'aio_UJGo80Rfy67c3PJiX9At87u4UoYW')  #setting up my client

# manual function to take inputs manually
def manual():
    f1 = True
    while(f1):
        data = aio.receive('ledbtn') # receiving feed from ledbtn
        data2 = aio.receive('goback') # receiving feed from goback
        if(data2.value == "1"):
            f1 = False
            menu()
            break
        if(data.value == "ON"): 
            green_led.on() # turning on street light
        elif(data.value == "OFF"):
            green_led.off() # turning off the street light


# auto function to monitor street lights through sensors

def auto():
    global c
    f2 = True
    while(f2):
        data2 = aio.receive('goback') # receiving data from goback 
        if(data2.value == "1"):
            f2 = False
            menu()
            break
        pir.wait_for_motion()
        print("Motion Detected")
        green_led.on()
        c+=1 # increasing the counter by 1
        print(c)
        aio.send('led', c)
        pir.wait_for_no_motion()
        green_led.off()
        print("Motion Stopped")


def menu():
    d = Data(value=0)
    aio.create_data('goback', d) # setting value as 0
    aio.create_data('manual', d) # setting value as 0
    aio.create_data('auto', d) # setting value as 0
    f3 = True
    print("inside menu")
    while(f3):
        data = aio.receive('manual') # receiving feed from manual
        data2 = aio.receive('auto') # receiving feed from auto
        if(data2.value == "1"): 
            f3 = False
            auto()
        elif(data.value == "1"):
            f3 = False
            manual()

c = 0 # setting counter as 0
green_led=LED(17) # setting street light at GPIO pin 17
pir = MotionSensor(4) # setting motion sensor at GPIO pin 4
green_led.off() # turning off the street lights
menu() # calling the menu function
