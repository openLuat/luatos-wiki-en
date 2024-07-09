# Ec718p Series Firmware Release Notes

* [Firmware download address](https://gitee.com/openLuat/LuatOS/releases)
* [Firmware Download Alternate Address](https://pan.air32.cn/s/DJTr?path=%2F)
* Fully automatic compilation of the latest firmware through [Cloud Compile](https://wiki.luatos.org/develop/compile/Cloud_compilation.html)


## V1002
Compatibility changes
* 1：Due to more functional changes, FLASH space is insufficient
    * (1)LuatOS-SoC_V1001_EC718PV Cannot remotely upgrade LuatOS-SoC_V1002_EC718PV
    * (2)LuatOS-SoC_V1002_EC718PV is the last version of EPV firmware officially released. If you need EPV firmware in the future, use cloud compilation or compile it locally.


Defect repair
* fix: Unable to select jpeg encoding quality when taking pictures
* fix：pwm In the absence of first close, both the cycle and the duty cycle are changed, which may lead to a crash.
* fix：When using the uart485, the steering pin cannot be set to GPIO16 and GPIO17
* fix：otp Abnormal function
* fix：ota At the last minute of ota completion, the underlying OTA will succeed, while the script ota will fail.
* fix：EPV Firmware, unable to enter hibernation
* fix：socket Callback message error on active shutdown
* fix：mqtt When sending, the data is sent out at one time to avoid being interrupted.
* fix：mqttconnect Unable to connect to server when message length exceeds 256
* fix：ftp abnormal crash
* fix：socket Add protection to prevent freed resources from being used again
* fix：Prevent possible time setting errors
* fix：spi table Method sending exception


New Function
* add：audio Library to add recording function
* add：agpio Ability to maintain pre-sleep level after deep sleep wake-up
* add：Reset stack parameters to default
* add：Base station synchronous time switch
* add：Deep Sleep Timer Callback Message
* add：w5500 Add DHCP Timeout Message
* add：DHCP Increased retries for slow routers
* add：socket Query the current connection status
* add：http Custom header supports custom size
* add：sfud mutex protection
* add: Support external flash full upgrade
* add：Support to configure the working voltage of codec
* add：mqtt Add the ability to set the buffer size

Update function
* update：When an unparsed NMEA statement is encountered, the mask prints



## V1001

Precautions:
Adequate testing is required for use


Defect repair:
* Fixes found in V1000 version BUG

New Function:
* add: ioqueue Operation
* add: camera Function
* add: Support sleep function


## V0001

EC718P Series Firmware First Edition

The supported functions are as follows

Basic Peripherals:

1. gpio
2. uart
3. iic
4. spi
5. adc
6. pwm
7. wdt
8. pm（Deep Sleep temporarily unavailable）
9. fota
10. rtc Clock
11. media（tts/amr/mp3/wav）
12. wlanscan

Tool Library:

1. json
2. iotauth
3. fs
4. pack
5. zbuff
7. fskv
8. miniz
9. sfud（spi flash）
10. fatfs（tf Card）
11. w5500
12. protobuf
13. iconv
14. u8g2
15. lcd
16. lvgl

Network function:

1. socket(tcp/udp/tcp_ssl)
2. http/https
3. mqtt/mqtts
4. ftp/ftps
5. ntp
6. sms
7. errDump
8. websocket
