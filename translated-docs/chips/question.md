# FAQ List

## Air101 There are several serial ports

A total of 5 serial ports, of which serial port 0 is used for downloading and debugging, and 4 channels are available.

## Air101 What is the ID of I2C of the development board

See demo is used 0

## Air101 Does it support single bus?

Support [sensor-sensor action library](https://wiki.luatos.org/api/sensor.html#sensor "sensor-sensor action library")

## Air101 Can you simulate the mouse and keyboard?

No.

## Air101 Do you want to install drivers for the equipment

ch340

## Air101 Download unsuccessful

Check serial port printing, select the corresponding serial port, baud rate setting bit：921600
[Burn Tutorial](https://wiki.luatos.org/boardGuide/flash.html "Burn Tutorial"")

## Air101 Why did I modify the gpio routine? After burning it in, the indicator light is the same as the original one. There is no change. It is not possible to download the script alone.

Download to ensure that the luatools installation path, project path, firmware path does not appear Chinese \space \special symbols

## Air101 Do the bosses have air101 information? I don't think the community seems to have it yet.

[Air101 Data summary (software and hardware data, firmware download, technical support)](https://doc.openluat.com/article/3508 "Air101 data summary (software and hardware data, firmware download, technical support))")

## Air101 Can the delay of us be realized

Yes, [statem-SM state machine](https://wiki.luatos.org/api/statem.html "statem-SM state machine")

## Air101 support nvm function

There are alternative libraries fdb [fdb-kv database (based on FlashDB)](https://wiki.luatos.org/api/fdb.html "fdb-kv database (based on FlashDB)")

## Air101 Support ink screen

Support,[eink-ink screen operation library](https://wiki.luatos.org/api/eink.html "eink-ink screen operation library"")

## Does Air101 support it?debug

ide Single-step debugging of 101 is temporarily not supported.

## bit How to use Air101?

101 It's 5.3 grammar, just use it directly.

## Air101 The lpmem library is not used.

lpmem library currently only 302 have

## Air101 Can I use the ILI9341 screen with touch?

Can, refer to DEMO to modify initialization parameters [lcd](https://wiki.luatos.org/chips/air101/Air101.html#lcd "LCD")

## Air101 Can I plug in wireless devices with spi interface?

Yes，[SPI](https://wiki.luatos.org/chips/air101/Air101.html#spi "SPI")

## Air101 Did the SPI screen above run through

have reference LCD DEMO [DEMO/LCD](https://gitee.com/openLuat/LuatOS/tree/master/demo/lcd "LCD")

## Air103 Is there a connection method for Bluetooth large screen (4.3 and 7 inches)

103 Can't support such a big screen

## Air103 Is there a pin for the external real-time clock crystal (RTC)?

It has been done internally. With this function, there is no external pin.

## Air103 Brush 101 lighting code, no effect, the light is not on

Because the GPIO of the three lamps of the 103 development board is different from that of the three lamps of the 101 development board, the 103 is 40,41 and 42 GPIO

[Air103 Data Summary (Software and Hardware Data, Firmware Download, Technical Support)](https://doc.openluat.com/article/3674 "Air103 Data Summary (Software and Hardware Data, Firmware Download, Technical Support)")

## Air105 Is the register manual the same as the W800 register?

Not the same

## Air105 How many megabytes of network ports can the development board connect?

It can be connected to 10Mbps or 100Mbps, depending on the capability of the external spi Ethernet conversion chip, the firmware supporting Ethernet is still under development, and the maximum speed cannot be answered yet, but it is no problem to run to a few Mbps, which is enough to meet the general Ethernet iot application.

## soc Can the development version of the Bluetooth antenna be introduced

Can, 101 8 feet, 103 nc feet, self-flying line

## Air105 How many pixels of the camera is supported, and what type of camera interface is it? Does the camera support usb

Not yet, currently 640x 480,DCIM interface

## Air105 Can it be used in industrial occasions? No, can it drive stepping motor, with can port and multi-periodic

No motor, no can, multiple timers

## Air105 What is the 12M crystal oscillator error inside the development board, and if UART or USB is used, is it necessary to connect an external crystal oscillator?

The deviation is relatively large at high and low temperatures, so it is not recommended to save. The crystal oscillator error of 12M is 2%

## Air101 Will it be very inefficient to do algorithms like fft?

101 with dsp, with hardware fft acceleration

## SCM module, is there any support for TTS voice

without  

## Air105 What model and how many pixels is the camera sent by the development board?

gc032a，30w Pixel

## Air105 Brush the firmware just sent, print garbled code

The baud rate needs to be set 1500000

## Air105 Or will the 10x series open the register manual?

Will

## lvgl.img_set_src(img1, "/img/imgbtn_green.png"); Is this not good either?
/luadb/xxxx.png

## Air105 Can the development board read SD card?
Can, through spi, slower than sdio

## Air105 support can bus
Not supported, to plug-in

## rtos-sdk Do you support it and provide relevant information?
esp32 Hizhou only provides LuatOS support, and other information can be directly seen from Lexin's original factory.

## ESP32 Can you connect, LCD expansion board and 1.8 inch display screen
Yes

## Air105 What is the turnover speed of gpio software under the highest frequency?
3M

## fdb Will the previously saved data be emptied for each initialization?
No.

## Air105 Can it be developed with keil?
Unable to debug peripherals with keil's debug, no SVD file

## What features are supported by the default firmware
Can decompress soc to see luat_conf_bsp.h

## Air105 Can you run?Freertos
105 Now the open csdk is running freertos

## Air101 Can I get the uuid of the chip?
mcu.unique_id()

## LCD How to clear the screen at the specified position 
Specified position brush Specified color

## lcd.drawLine(20,20,150,20,0x001F))Where can I find this color? The standard RGB is 3 bytes, and the color here is 2 bytes.
rgb565 

## Air103 What is u4 reserved on the back of the board?
psram

## Air105 Which screen does camera match with a sample program
2.4 Inch TFT LCD 240X320 GC9306 SPI serial port screen

## Air103 And 105 support multi-threading
RTOS support for multitasking

## ST7735 V1.1 The screen displays the white edge
Just set the screen drive offset to 0（xoffset = 0,yoffset = 0）

## Air105 What is the maximum frequency of the PWM
The limit is 26M, the highest = main frequency /8

## Air105 support dual adc mode does not
Not supported, only 1 ADC controller
