# 📶 Air780EP Compilation Guide

This document is suitable for combined modules based on the same chip scheme.

* Air780EP


Note: More detailed compilation instructions can be viewed  https://gitee.com/openLuat/luatos-soc-2024/blob/master/README.md


## Preparations

1. Windows 10(or above), linux (only ubuntu verified)
2. At least more than 5G of the remaining space, there will be a large number of temporary files
3. If it is best to connect to the Internet, there is no solution, and it will be explained.

## Download source code

Note that two libraries are required

* Master Library `https://gitee.com/openLuat/LuatOS`
* bsp Library 'https://gitee.com/openLuat/luatos-soc-2024' linux compilation will have more instructions, please refer README.md

The code is frequently updated, recommend use 'git' for clone download, and do not recommend zip download.

The downloaded directory **must** conform to the following structure, and the directory name must be 'LuatOS' and`luatos-soc-2024`.

assuming that in `D:\gitee`

```tree
D:\gitee\
    - LuatOS
        - lua
        - luat
        - components
    - luatos-soc-2024
        - csdk.lua
        - project
        - interface
```

If using zip download, **be sure to correct the directory name to match the above structure!!**

## Prepare tools

The installation of xmake can be downloaded from the official website of xmake, and can be downloaded directly from [this link](https://pan.air32.cn/s/DJTr?path=/常用工具). It is recommended to use the latest version..

During installation, PATH **will be selected by default, and if not, it will be checked.

**Note: Environment variables need to restart the computer to take effect**

## Start Compiling

The sample environment uses windows,linux/macosx is actually a common compilation method.

Open Terminal under luatos-soc-2024\project\luatos

First execute the configuration, execute `xmake f --menu`

`Basic Configuration` We don't need to worry about it. It contains the compilation architecture/compiler related configuration. The cross compiler will be automatically configured in the project. Here we ignore it and select' Project Configuration' to configure according to our actual use.

Select Exit to exit after configuration is complete, and ask if you want to save the selection.yes

**Note: The above configuration operation only needs to be configured once and will take effect all the time. You need to perform the configuration operation again only after modifying the configuration.**

Then execute 'xmake to compile, the generated binpkg/soc/log database (comdb.txt) and other files are located in the' out' directory under the project



The final output is as follows (roughly):

```shell
D:\github\luatos-soc-2023\PLAT\driver\chip\ec718p\ap\inc_cmsis/Driver_USART.h:345:3: warning: type qualifiers ignored on function return type [-Wignored-qualifiers]
[ 99%]: archiving.debug libluatos.a
[ 99%]: linking.debug luatos.elf

7-Zip 19.00 (x64) : Copyright (c) 1999-2018 Igor Pavlov : 2019-02-21

Scanning the drive:
9 files, 2549515 bytes (2490 KiB)

Creating archive: LuatOS-SoC_V1001_EC718P.7z

Add new data to archive: 9 files, 2549515 bytes (2490 KiB)


Files read from disk: 8
Archive size: 1328565 bytes (1298 KiB)
Everything is Ok
[100%]: build ok!
end
```

That is to say, the compilation is successful, and the generated binpkg/soc/log database (comdb.txt) and other files are all located in the' out' directory under the project, and you can use the LuaTools to swipe the machine.

As an additional note, the soc file is a compressed package and does not represent the actual size of the firmware.!!


ps：If you do not want to use graphics, you can also use the command line configuration method, for example, 'xmake f -- chip_target = ec718p -- lspd_mode = y -- denoise_force = n'. for specific supported configuration items and parameters, execute' xmake f -- help 'to view the specific description under the Command options (Project Configuration)


## Common Compilation Issues

1，xmake The package is installed with disk c by default. I don't have much space for disk c and don't want to install it on disk c.

A: Set the relevant directory to another disk
`xmake g --pkg_installdir="xxx"` Set package installation directory 
`xmake g --pkg_cachedir="xxx"` Set Package Cache Directory

2，My computer cannot be connected to the Internet, how to download the installation package？

A: You can prepare the package in advance. Here, take the windows environment to install the gcc cross-compilation tool chain of this warehouse as an example.

First of all, download the gcc-arm-none-eabi-10-2020-q4-major-win32.zip to the root directory of disk d, execute' xmake g-pkg_searchdirs = "d:" 'xmake will give priority to searching the set directory to search for the installation package, so there is no need to connect to the network.

gcc For the download connection, refer to the connection in csdk.lua and select the corresponding platform to download the download.

3，github Whether the package can be accelerated？

`xmake g --proxy_pac=github_mirror.lua`you can set the built-in github accelerated image

