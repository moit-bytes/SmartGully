from Adafruit_IO import *
from gpiozero import LED
from gpiozero import MotionSensor
import RPi.GPIO as GPIO

GPIO.setmode(GPIO.BCM)
GPIO.setup(16, GPIO.IN, pull_up_down=GPIO.PUD_DOWN)


aio = Client('moitbytes', 'aio_UJGo80Rfy67c3PJiX9At87u4UoYW')

def manual():
    d = Data(value=0)
    aio.create_data('manual', d)
    aio.create_data('auto', d)
    f1 = True
    while(f1):    
        data = aio.receive('ledbtn')
        data2 = aio.receive('auto')
        if(data2.value == "1"):
            f1 = False
            auto()
            break
        if(data.value == "ON"):
            green_led.on()
            led1.on()
            led2.on()
            led3.on()
            led4.on()
            led5.on()
        elif(data.value == "OFF"):
            green_led.off()
            led1.off()
            led2.off()
            led3.off()
            led4.off()
            led5.off()
             
       
def auto():
    d = Data(value=0)
    aio.create_data('manual', d)
    aio.create_data('auto', d)
    global c
    global power
    f2 = True
    while(f2):
        data2 = aio.receive('manual')
        if(data2.value == "1"):
            f2 = False
            manual()
            break
        if((GPIO.input(16)) == 1):
            pir.wait_for_motion()
            print("Motion Detected")
            green_led.on()
            led1.on()
            led2.on()
            led3.on()
            led4.on()
            led5.on()
            c+=1
            power+=0.167
            print(c)
            aio.send('led', c)
            aio.send('power', power)
            pir.wait_for_no_motion()
            green_led.off()
            led1.off()
            led2.off()
            led3.off()
            led4.off()
            led5.off()
            print("Motion Stopped")
       

def menu():
    d = Data(value=0)
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
power = 0
green_led=LED(17)
led1 =LED(12)
led2 =LED(13)
led3 =LED(19)
led4 =LED(21)
led5 =LED(26)
pir = MotionSensor(4)
green_led.off()
led1.off()
led2.off()
led3.off()
led4.off()
led5.off()
menu()
