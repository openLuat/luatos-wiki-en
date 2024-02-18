# Portable wifi data collection

This document is` Air780E` `ESP32S3` to do portable wifi related information

1. Mainly with Air780E, you can actually use EC618 series of other modules, such as Air700E,Air780EG and so on
2. The speed is not fast, it can be said that it is still relatively slow. As a wifi-to -4G platform, there is a certain load, but don't consider just being a hot spot for mobile phones..
3. wifi The account password is `luatos` `12345678` or `12341234`
4. The management interface account password is `luatos` `12345678` or `12341234`
5. ESP32S3 Brush is the firmware of the ESP-Iot project, non-LuatOS firmware, if you need to open the development, is to use the esp-idf to use the C language programming.

## Firmware and Brush Steps

Firmware Download: [ESP32S3 MIFI Demo Firmware](https://pan.air32.cn/s/DJTr?path=%2F%E5%90%84%E7%A7%8D%E6%B5%8B%E8%AF%95%E5%9B%BA%E4%BB%B6%2F%E9%9A%8F%E8%BA%ABwifi%E7%9A%84%E5%9B%BA%E4%BB%B6_ESP32S3)

Brush machine instructions:

1. Air780E Only AT firmware is required, and the factory default is AT firmware, so there is no need to brush the machine under normal circumstances.
2. ESP32S3 Brush the firmware in the above link
3. After the brushing machine is completed, there is a yellow toggle switch on the ESP32S3 development board, tear off the protective film, and toggle to the USB side
4. Connect the two development boards with a Type-C-to-Type-C line.
5. Use additional Dupont cable to supply Air780E or ESP32S3 5V/GND
6. Air780E Remember to insert the card, the corresponding wifi network will be activated after the Air780E is connected to the Internet, and it can be searched.
7. Air780E You need to press the pwrkey key for 2 seconds to turn on the machine. You can short-circuit the pad next to the pwrkey button to turn on the machine..

## FAQ

1. ESP32S3 source code of firmware

[ESP32S3-AIR780E_CdcPPPDemo](https://github.com/yangzichen123/ESP32S3-AIR780E_CdcPPPDemo)

[esp-iot-bridge](https://github.com/espressif/esp-iot-bridge/blob/master/components/iot_bridge/User_Guide_CN.md)

2. wifi Can't find, how to do

The high probability is that the Air780E is not connected to the Internet or the card is not plugged in. Plug the Air780E development board directly into the computer to see if the RNDIS network card appears. If not, you need to brush the AT firmware.
