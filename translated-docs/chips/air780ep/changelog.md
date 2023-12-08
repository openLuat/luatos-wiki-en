# Ec718p Series Firmware Release Notes

* [Firmware download address](https://gitee.com/openLuat/LuatOS/releases)
* [Firmware Download Alternate Address](https://pan.air32.cn/s/DJTr?path=%2F)
* Fully automatic compilation of the latest firmware through [Cloud Compile](https://wiki.luatos.org/develop/compile/Cloud_compilation.html)

## V0001

EC718P Series Firmware First Edition

The supported functions are as follows

Basic Peripherals:

1: gpio
2: uart
3: iic
4: spi
5: adc
6: pwm
7: wdt
8: pm（Deep Sleep temporarily unavailable）
9: fota
10: rtc Clock
11: media（tts/amr/mp3/wav）
12: wlanscan

Tool Library:

1: json
2: iotauth
3: fs
4: pack
5: zbuff
7: fskv
8: miniz
9: sfud（spi flash）
10: fatfs（tf Card）
11: w5500
12: protobuf
13: iconv
14: u8g2
15: lcd
16: lvgl

Network function:

1: socket(tcp/udp/tcp_ssl)
2: http/https
3: mqtt/mqtts
4: ftp/ftps
5: ntp
6: sms
7: errDump
8: websocket
