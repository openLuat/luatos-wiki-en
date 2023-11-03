# Air780E Module (EC618 Series General)



**This series of documents applies to EC618 series modules, including but not limited Air780E/Air780EG/Air700E/Air600E**

## What is the EC618 series module (typically Air780E)?

LTE Cat launched by Zeo Communications. 1 bis communication module, using the core-shifting EC618 platform, supports 4G Netcom, including modules:

1. Air780E  -- 4G Cat.1
2. Air780EG -- Air780E+Air510U,Support GNSS/GPS satellite positioning
3. Air700E  -- Small Package, TDD Only Module, China Mobile Card Only
4. Air600E  -- Compatible package, does not recommend secondary development

Main features:
- Support dual card single standby, only support 4G network, do not support 2G/3G/5G, also do not support Wifi communication
- Support USB 2.0, only CDC function, does not support HID and host mode
- Support I2S digital voice interface, also support soft DAC audio output

## LuatOS What functions are provided for it

- Available Lua memory: default 200k, highest 300k
- Script space: default 128k, highest 448k
- Basic Peripherals: GPIO/SPI/I2C/PWM/ADC
- Network function: Mobile/TCP/UDP/Http/Mqtt/WebSocket/FTP/NTP/SMS
- UI Display: LCD/Eink/U8G2/LVGL
- Voice playback: MP3/AMR/TTS

Note that due to the limitations of the chip platform, the following functions cannot be implemented or have limitations:
1. 2G/3G/5G communication not supported
2. SMS sending and receiving of `telecom` network is not supported
3. `wifi` communication is not supported, only in specific scenarios wifi scan
4. Does not support VoLTE, does not support 2G/3G voice
5. Supported but not recommend use LVGL

Air780EG of the differences, please refer to the documentation [Air780EG for additional instructions](air780eg.md)

## Firmware Download

The official version can be downloaded from the release page, and the firmware of the entire series is common.：

[https://gitee.com/openLuat/LuatOS/releases](https://gitee.com/openLuat/LuatOS/releases)

## Brush machine burning tutorial

[https://openluat.github.io/luatos-wiki-en/boardGuide/flash.html](https://openluat.github.io/luatos-wiki-en/boardGuide/flash.html)

## Related Information Links

[Open Source Warehouse Link(BSP)](https://gitee.com/openLuat/luatos-soc-2022)
[open source repository link (main library)](https://gitee.com/openLuat/LuatOS)
