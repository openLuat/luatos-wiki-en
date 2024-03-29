# Firmware Release Notes

This document describes changes to LuatOS firmware for Air601

Firmware download address:

1. [Hezhou Cloud Pan](https://pan.air32.cn/s/DJTr?path=%2F)
2. LuaTools It will also be downloaded automatically.
3. [Gitee Library publishing path](https://gitee.com/openLuat/LuatOS/releases)

## V1023

1. adapts to the fota function and supports full upgrade of underlying scripts
2. Support dynamic switch ble to achieve ble and wifi coexistence
3. Support spi slave function, maximum rate 50M, corresponding demo/spi_slave
4. Support ulwip library, can be integrated w5100s, corresponding demo/ulwip
5. Support for AP hiding ssid
6. http Library supports arbitrary header
7. optimizes ble memory usage, reducing approx.10k
8. Optimize ble power consumption, when firmware does not contain wifi function, save more than 50% power
9. Font optimization to solve the alignment problem of Chinese display

## V1022

1. Fix the compatibility problem with AT firmware, read and write the MAC address of AP and STA correctly
2. Fix nimble library unable to disconnect Bluetooth connection
3. socket The library supports obtaining millisecond-level timestamps.

The matching soc_script version is: v2023.12.11.10

## V1021

1. Support esptouch and airkiss distribution network, compatible with Tencent
2. Fix the problem that wifi mac address causes the connection to mobile phone/computer hotspot to fail.
3. Support TLS and other encryption links, the default firmware is not enabled, can be self-cloud compilation
4. Support larger Lua memory, default 92k
5. Support Bluetooth function, but need to be used separately from wifi, can not be enabled at the same time

## V1020

Initial version
