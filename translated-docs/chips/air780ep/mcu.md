# Air780EP Module (EC718P Series General)

**This series of documents applies to the EC718P series module**

## What is the EC718P series module (typically Air780EP)?

LTE Cat launched by Zeo Communication. 1 bis communication module, using the core-shifting EC718P platform, supports 4G Netcom, including modules:

1. Air780EP  -- 4G Cat.1

Main features:

- Support dual card single standby, only support 4G network, do not support 2G/3G/5G, also do not support Wifi communication
- Support USB 2.0, only CDC function, does not support HID and host mode
- Support I2S digital voice interface, also support soft DAC audio output

## LuatOS What functions are provided for it

- Available Lua memory: default 256k, up 300k
- script space: default 256k
- Basic Peripherals: GPIO/SPI/I2C/PWM/ADC
- Network function: Mobile/TCP/UDP/Http/Mqtt/WebSocket/FTP/NTP/SMS
- UI Display: LCD/Eink/U8G2/LVGL
- Voice playback: MP3/AMR/TTS

Note that due to the limitations of the chip platform, the following functions cannot be implemented or have limitations:

1. 2G/3G/5G communication not supported
2. Support mobile and Unicom SMS sending and receiving, only Air780EPV support 'telecom' network SMS sending and receiving
3. 'wifi' communication is not supported, only in specific scenarios wifi scan
4. Air780EPV support only VoLTE
5. Support Use LVGL

## Firmware Download

The official version can be downloaded from the release page, and the firmware of the entire series is common.：

[https://gitee.com/openLuat/LuatOS/releases](https://gitee.com/openLuat/LuatOS/releases)

[Firmware Download Alternate Address](https://pan.air32.cn/s/DJTr?path=%2F)

## Brush machine burning tutorial

[https://wiki.luatos.org/boardGuide/flash.html](https://wiki.luatos.org/boardGuide/flash.html)

## Related Information Links

[Open Source Warehouse Link(BSP)](https://gitee.com/openLuat/luatos-soc-2024)
[open source repository link (main library)](https://gitee.com/openLuat/LuatOS)
