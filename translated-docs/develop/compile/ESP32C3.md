# 📡 ESP32 Series Compilation Document

This document is suitable for the following chips:
1. esp32c3
2. esp32c2
3. esp32
4. esp32s3

Please confirm the following information before using this document:

1. You "probably don`t need" this document, which is an advanced document for self-expansion of firmware.
2. The firmware package we provide contains [compiled firmware](https://gitee.com/openLuat/LuatOS/releases)
3. If you just want to add your existing libraries to the firmware, you can use our [Online Cloud Compilation](https://wiki.luatos.org/develop/compile/Cloud_compilation.html) to generate custom firmware.
4. If you are looking for application-oriented documents such as machine brushing/compiling lua scripts, then this document is` not what you need` to view.

Video tutorial link: [Station B drinks porridge every day](https://www.bilibili.com/video/BV1D3411p7MK?p=1)

## Prepare the environment

1. `windows 10 x64`, Other versions solve idf installation problems by themselves, requiring idf5, the latest idf5!! The lower version cannot be compiled.!!
2. All steps use `CMD`, not `PowerShell`. If you don`t know what these are, please learn from Baidu first.
3. At least 2G of disk space for code and intermediate files

## Prepare the project

### Build a folder to store all the files needed for compilation.

recommend use `D:\github`, the minimum requirement is no spaces, Chinese, special strings, and as short as possible

### Get source code

Source code to 2 copies, [LuatOS main library](https://gitee.com/openLuat/LuatOS) and [luatos-soc-idf5]((https://gitee.com/openLuat/luatos-soc-idf5), which are two different warehouses, both need!!

How to get it: recommend git and download zip after registration.

It must be placed according to the following directory structure, taking `D:\gitee` as an example

```
D:\
    github\
        LuatOS\
            lua\
            luat\
            components\
            Other catalogs
        luatos-soc-idf5\
            luatos\
```

Checkpoint, if the path is correct, the following file path must exist. If it cannot be found, it must be a naming problem. It is futile to add it manually.

* `D:\github\LuatOS\lua\src\lgc.c`
* `D:\github\luatos-soc-idf5\luatos\include\luat_conf_bsp.h`


`LuatOS` `luatos-soc-idf5` They are all fixed directory names and cannot be changed. For example, "LuatOS-master" is a wrong name and must be changed back. `LuatOS`

If it is really inconvenient to put the main inventory in `D:\github\LuatOS`, modify the value of LUATOS_ROOT in luatos-soc-idf5\luatos\CMakeLists.txt. For example, stored in `E:/abc/LuatOS`, modify it

```
set(LUATOS_ROOT "E://abc/LuatOS/")
```

## Final preparation before compilation

Installation idf5
1. Access Address https://dl.espressif.cn/dl/esp-idf/
2. download idf5 offline installation package `ESP-IDF v5.1 - Offline Installer`
3. After downloading, double-click to start, follow the prompts to install
4. After the installation is completed, the start menu will have a shortcut to idf5

## Compile

use the start menu or shortcut to enter the idf5 CMD

```
D:
cd D:\github\luatos-soc-idf5\luatos
idf.py fullclean
idf.py set-target esp32c3
idf.py build
```

（idf.py set-target In order to set the chip to be compiled, enter the model you want to compile, and do not enter it without brains.esp32c3）

When the words` Project build complete.`appears, it indicates that the compilation was successful and a file with`. soc` will be generated. use the LuaTools to swipe the machine.

## Libraries in Custom Firmware

Open `D:\github\luaotos-soc-idf5\luatos\include\luat_conf_bsp.h `to comment or uncomment as needed. Note that if the function is too large and the firmware cannot be placed, the compilation will fail.

