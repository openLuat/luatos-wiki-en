# Air780EPS Module Firmware Release Notes

## Firmware Partition Description
|Partition | Size      |
|----------|----------|
|Script area     | 512K    |
|Script OTA Area  | 360K    |
|File System Area  | 512K   |
|Ground floor FOTA area |  1024K |
|KV Area      | 64K    |


## V1004

Air780EPS Module First Edition Firmware

Supported functions are as follows

Basic Peripherals:

1. uart
2. gpio
3. iic
4. spi
5. adc
6. pwm
7. wdt
8. wlan
9. rtc Clock
10. media（amr/mp3/wav/tts（Firmware built-in TTS resource library））
11. wlanscan

Tool Library:

1. iotauth
2. crypto
3. json
4. zbuff
5. pack
6. libgnss
7. fs
8. sensor
9. fskv
10. i2ctools
11. miniz
12. iconv

Network function:

1. socket(tcp/udp/tcp_ssl)
2. http/https
3. mqtt/mqtts
4. ftp/ftps
5. ntp
6. errDump
7. websocket



