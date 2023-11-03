# Command Line Brush Guide

## 1. brush machine introduction:

luatos You can use the command line to brush the machine. If you do not use the luatools brush, you can also use the command line or package the brush software separately. Air101/air103 uses air101_flash.exe, and air105/esp32-c3 uses soc_download.exe to refresh the machine.

Before the introduction, let's take a look at the SOC file composition.：

.soc The file is the firmware format used by the luatos-soc system. We can decompress it with 7z and other decompression software. We can find that the firmware composition is as follows：

.bin files (firmware binaries)

.exe File (flash program, air101/air103 is air101_flash.exe,air105/esp32-c3 is soc_download.exe)

info.json(Brush machine parameter information)

## Introduction to 2. Command Line Brush Parameters：

### 1、air101/103 command line brush：

Air101/Air103 At present, a total of 14 parameters need to be entered.：

1、-ds Set the serial port speed when downloading, the default value is 115200.15200 | 460800 | 921600 | 1000000 | 2000000|1M | 2M

2、-c Serial port numbers such：COM0

3、-ws Working serial port speed, the default value is 115200 can be used 1200 - 2000000|1M | 2M

4、-rs Reset action, set the device reset method, the default is manual control optional none | at | rts

5、-dl Download firmware file, download compressed image by default

**View more parameters using run` air101_flash.exe -h`**

Take an example

`air101_flash.exe -ds 2M -c COM0 -ws 115200 -rs rts -dl air10x.fls    `  

**Each parameter can be viewed in info.json**

### 2、air105 command line brush：

Air105 At present, a total of 14 parameters need to be entered.：

1、Type, string, air105 must be filled in `air105_download`

2、Serial port number, base 10，1~255

3、Baud rate for general bl download, base 10

4、Download binfile path, quoted string

5、Download bootloader file name, quoted string

6、bootloader Write address, hexadecimal,0x

7、Download APP file name, quoted string

8、APP Write address, hexadecimal,0x

9、File name of the download script, quoted string

10、Script write address, hexadecimal,0x

11、RTS Reset level, base 10, 0 or 1

12、Script-only download flag, base 10, 0 or 1,1 means only download scripts

13、File system address

14、The length of the file system to be erased, if not erased, write 0

Take an example

`soc_download.exe air105_download 83 3000000 "E:\air105\core\hex\air105\debug" bootloader.bin 01001000 app.bin 01010000 script.bin 01300000 0 0 01380000 0`

**Each parameter can be viewed in info.json**

### 3、esp32-c3 command line brush：

1、Type, string, esp32-c3 must be filled in.`esp32_download`

2、Serial port number, base 10，1~255

3、Baud rate for general bl download, base 10

4、Download binfile path, quoted string

5、Download bootloader file name, quoted string

6、bootloader Write address, hexadecimal,0x

7、Download APP file name, quoted string

8、APP Write address, hexadecimal,0x

9、File name of the download script, quoted string

10、Script write address, hexadecimal,0x

11、File name of the partition table, quoted string

12、Partition table write address, hexadecimal,0x

13、The chip download parameter is currently 0 x00ff0200,byte3 is the chip type, currently only 0,byte2 is spi flash info (writing 0xff means the size is obtained from ID),byte1 is spi mode (writing 0xff means not modifying the firmware, currently 0x 02),byte0 is useless 0

14、Script-only download flag, base 10, 0 or 1,1 means only download scripts

15、File system address

16、The length of the file system to be erased, if not erased, write 0

Take an example

`soc_download.exe esp32_download 66 1152000 "_temp\soc\download\esp32" "bootloader.bin" 00000000 "luatos.bin" 00010000 "script.bin" 01300000 "partition-table.bin" 00008000 00ff0200 0 00380000 0000`

If 3 bins are combined into 1 bin, it can be as follows

`soc_download.exe esp32_download 66 1152000 "_temp\soc\download\esp32" "bootloader.bin" ffffffff "luatos_esp32.bin" 00000000 "script.bin" 01300000 "partition-table.bin" ffffffff 00ff0200 0 00380000 0000`

**Each parameter can be viewed in info.json**

## The information printed in the 3. console requires attention to the following fields

**download error:xxx Download error and reason**

download stage xxx:yyy Download the yyy phase of XXX(bl,app.bin, script.bin). note that yyy is a number. bl phase has different interpretations according to different chips. app.bin and script.bin have common interpretations, as follows

**air105 of bl**

0：Try to reset the chip through RTS and synchronize the serial port of the chip.

1：Synchronize serial port successfully, start connecting serial port

2：Connect serial port successfully, send bootloader information

3：Start Erase Related flash

4：start writing bl data

5：Complete

**generic bl download protocol:**

0：Start Synchronization

1：Send bin information

2：Send bin data

3：Wait to verify firmware information

**esp32 of ramrun：**

0：Try to reset the chip through RTS and synchronize the serial port of the chip.

1：Synchronize serial port successfully, start connecting serial port

2：Connect serial port successfully, send bootloader information

3：Start Erase Related flash

**download percent:xxx Progress of the current download**

Prompt when all downloads are complete download OK

