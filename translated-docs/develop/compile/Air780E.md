# 📶 Air780E Compilation Guide

This document is suitable for combined modules based on the same chip scheme.

* Air780E
* Air600E
* Air780EG
* Air700E
* Air780EX


## Preparations

1. Windows 10(In theory win7 can also be used, but not recommend), linux (only ubuntu verified)
2. At least more than 5G of the remaining space, there will be a large number of temporary files
3. If it is best to connect to the Internet, there is no solution, and it will be explained.

## Download source code

Note that two libraries are required

* Master Library `https://gitee.com/openLuat/LuatOS`
* bsp Library `https://gitee.com/openLuat/luatos-soc-2022` linux compilation will have more instructions, please refer README.md

The code is updated frequently. It is recommended to use `git` for clone download. Zip download is not recommended..

The downloaded directory **must** conform to the following structure, and the directory name must be `LuatOS` and`luatos-soc-2022`.

assuming that in `D:\gitee`

```
D:\gitee\
    - LuatOS
        - lua
        - luat
        - components
    - luatos-soc-2022
        - xmake.lua
        - project
        - interface
```

If using zip download, **be sure to correct the directory name to match the above structure!!**

## Prepare the tool

Install xmake, which can be downloaded from xmake's official website, and can be downloaded directly from [this link](https://cdn.openluat-luatcommunity.openluat.com/attachment/20221113234354456_xmake-v2.7.3.win64.exe)

During installation, PATH **will be selected by default, and if not, it will be checked.

**Note: Environment variables need to restart the computer to take effect**

## Tool chain download

In a networked environment, xmake will download the gcc toolchain itself. If you have a normal internet connection, skip this step

:::{dropdown} Specific operation method

If you cannot connect to the Internet, or if the network is limited, you will usually have this prompt.:

```
error: fatal: not a git repository
```

Or the prompt of git/http connection failure. Therefore, the offline gcc tool chain download and compilation method is provided here.

1. Download [gcc for arm toolchain](http://cdndownload.openluat.com/xmake/toolchains/gcc-arm/gcc-arm-none-eabi-10.3-2021.10-win32.zip)
2. Decompress, do not select too deep a directory, do not contain Chinese characters and special symbols, it is recommended to decompress to the `D disk root directory`, the compression package comes with a layer of directory`gcc-arm-none-eabi-10.3-2021.10`
3. Assuming that the decompressed path is` D:\gcc-arm-none-eabi-10.3-2021.10 `, check whether `D:\gcc-arm-none-eabi-10.3-2021.10\bin\arm-none-eabi-g .exe` exists. if it does not exist, it must be an additional directory..
4. Use a text editor (such as vscode) to open the `build.bat` of the luatos-soc-2022, and modify the content as follows

```
The original content:
rem set GCC_PATH=E:\gcc_mcu
Modify the statement at the beginning of set. Note that rem is removed and the value is modified..
set GCC_PATH=D:\gcc-arm-none-eabi-10.3-2021.10
```

:::

## Start Compiling

1. Double-click `cmd.lnk` under `luatos-soc-2022. `. **Do not use PowerShell!!**
2. In the popup cmd command line, enter the command

```
build luatos
```

The final output is as follows (roughly):

```
D:\github\luatos-soc-2022\PLAT\driver\chip\ec618\ap\inc_cmsis/Driver_USART.h:345:3: warning: type qualifiers ignored on function return type [-Wignored-qualifiers]
[ 99%]: archiving.debug libluatos.a
[ 99%]: linking.debug luatos.elf

7-Zip 19.00 (x64) : Copyright (c) 1999-2018 Igor Pavlov : 2019-02-21

Scanning the drive:
9 files, 2549515 bytes (2490 KiB)

Creating archive: LuatOS-SoC_V1001_EC618.7z

Add new data to archive: 9 files, 2549515 bytes (2490 KiB)


Files read from disk: 8
Archive size: 1328565 bytes (1298 KiB)
Everything is Ok
[100%]: build ok!
end
```

That is to say, the compilation was successful. The output` soc` file can be found in the` out\luatos` directory. You can use the LuaTools to brush the machine.

As an additional note, the soc file is a compressed package and does not represent the actual size of the firmware.!!

## Common Compilation Issues

* Prompt network failure, git error, please refer to` tool chain download (offline environment) `section
* Prompt missing `luat_msgbus.h `files, please consult the `download source`, check the directory structure, and ensure that no path does not contain special characters
* Prompt `refer to xxx` and other ld link errors, please update the code, both code bases need to be updated. If the error is still reported, please report issue

