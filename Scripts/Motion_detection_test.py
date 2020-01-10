"""
Used to check motion sensors and the server's response.
Using this we can send/receive feeds through adafruit IO whenever a motion is detected.

"""
# importing necessary libraries
from Adafruit_IO import *
from gpiozero import LED
from gpiozero import MotionSensor

# aio = Client(user_name, AIO key)
aio = Client('moitbytes', 'aio_UJGo80Rfy67c3PJiX9At87u4UoYW') #setting up the client

c = 0 # counter to count number of times led got on
green_led=LED(17) # GPIO 17
pir = MotionSensor(4) # GPIO 4
green_led.off() 

while True:
    pir.wait_for_motion() # waiting for motion
    print("Motion Detected")
    green_led.on()
    c+=1
    print(c)
    aio.send('led', c) # sending data to feed
    pir.wait_for_no_motion() # waiting for no motion
    green_led.off()
    print("Motion Stopped")
