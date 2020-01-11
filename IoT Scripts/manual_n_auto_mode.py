from Adafruit_IO import *
from gpiozero import LED
from gpiozero import MotionSensor

aio = Client('moitbytes', 'aio_UJGo80Rfy67c3PJiX9At87u4UoYW')

def manual():
    f1 = True
    while(f1):
        data = aio.receive('ledbtn')
        data2 = aio.receive('goback')
        if(data2.value == "1"):
            f1 = False
            menu()
            break
        if(data.value == "ON"):
            green_led.on()
        elif(data.value == "OFF"):
            green_led.off()


def auto():
    global c
    f2 = True
    while(f2):
        data2 = aio.receive('goback')
        if(data2.value == "1"):
            f2 = False
            menu()
            break
        pir.wait_for_motion()
        print("Motion Detected")
        green_led.on()
        c+=1
        print(c)
        aio.send('led', c)
        pir.wait_for_no_motion()
        green_led.off()
        print("Motion Stopped")


def menu():
    d = Data(value=0)
    aio.create_data('goback', d)
    aio.create_data('manual', d)
    aio.create_data('auto', d)
    f3 = True
    print("inside menu")
    while(f3):
        data = aio.receive('manual')
        data2 = aio.receive('auto')
        if(data2.value == "1"):
            f3 = False
            auto()
        elif(data.value == "1"):
            f3 = False
            manual()

c = 0
green_led=LED(17)
pir = MotionSensor(4)
green_led.off()
menu()
