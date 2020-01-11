# Importing the necessary libraries
from Adafruit_IO import *
from gpiozero import LED
from gpiozero import MotionSensor
import RPi.GPIO as GPIO

GPIO.setmode(GPIO.BCM) # setting the board
GPIO.setup(16, GPIO.IN, pull_up_down=GPIO.PUD_DOWN) # reading GPIO pin 16

# aio = Client(user_name, AIO key)
aio = Client('moitbytes', 'aio_UJGo80Rfy67c3PJiX9At87u4UoYW') # setting up the client 

# manual function to take inputs manually
def manual():
    global c
    global power
    d = Data(value=0)
    #setting up the feeds to an initial value of zero
    aio.create_data('manual', d)
    aio.create_data('auto', d)
    aio.create_data('active', d)
    f1 = True
    while(f1):    
        data = aio.receive('ledbtn') #receiving data from ledbtn feed
        data2 = aio.receive('auto') #receiving data from auto feed
        if(data2.value == "1"):
            f1 = False
            auto()  # redirecting raspberry to auto if user presses auto
            break
        if(int(data.value) == 1):
            aio.send('active', 1) # sending data to active feed stating that device is active
            c+=1 # increasing the number of times street light went on
            power+=0.167 # increasing the power consumption variable
            green_led.on() #turning on all the lights on the street
            led1.on()
            led2.on()
            led3.on()
            led4.on()
            led5.on()
            aio.send('led', c) # sending data to led feed
            aio.send('power', power) # sending data to power feed
        elif(int(data.value) == 0):
            aio.send('active', 0)
            green_led.off()
            led1.off()
            led2.off()
            led3.off()
            led4.off()
            led5.off()
             
       
def auto():
    d = Data(value=0)
    #setting up the feeds to an initial value of zero
    aio.create_data('manual', d)
    aio.create_data('auto', d)
    aio.create_data('active', d)
    global c
    global power
    f2 = True
    while(f2):
        data2 = aio.receive('manual') #receiving data from auto feed
        if(data2.value == "1"):
            f2 = False
            manual() # redirecting raspberry to manual if user presses manual
            break
        if((GPIO.input(16)) == 1): # If LDR sensor detects darkness then perform the below task
            pir.wait_for_motion() # waiting for a motion
            print("Motion Detected")
            aio.send('active', 1) # sending data to active feed stating that device is active
            green_led.on() # turning on all the street lights
            led1.on()
            led2.on()
            led3.on()
            led4.on()
            led5.on()
            c+=1 # increasing the counter 
            power+=0.167 # increasing the power
            print(c)
            aio.send('led', c) # sending data to led feed
            aio.send('power', power)  # sending data to power feed
            pir.wait_for_no_motion() # waiting for no motion
            aio.send('active', 0)
            green_led.off() # turning off all the street lights
            led1.off()
            led2.off()
            led3.off()
            led4.off()
            led5.off()
            print("Motion Stopped")      



c = 0 # initializing number of led turned on as 0
power = 0 # initializing power consumption as 0
green_led=LED(17) # GPIO 17
led1 =LED(12) # GPIO 12
led2 =LED(13) # GPIO 13
led3 =LED(19) # GPIO 19
led4 =LED(21) # GPIO 21
led5 =LED(26) # GPIO 26
pir = MotionSensor(4) # GPIO 4
green_led.off() 
led1.off()
led2.off()
led3.off()
led4.off()
led5.off()
auto() # By default refering to the auto function for automatic switching on/off the street-lights
