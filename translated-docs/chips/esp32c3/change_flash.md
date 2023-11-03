# Replace flash tutorial

CORE ESP32 The flash size on the core board is 4MB. Although it is large enough, there are still some friends who like to toss about and want to change to a bigger one.flash。

This article will teach you how to compile the appropriate LuatOS firmware for 8MB(64Mb) and 16MB(128Mb) flash.

## **Wait a minute.！！**

```{warning}
Before replacing flash, please press [ESP32C3 Firmware Compilation Guide](https://openluat.github.io/luatos-wiki-en/develop/compile/ESP32C3.html) to ensure that the default firmware can be successfully compiled and that the module can be used normally after burning, and then try to replace it.flash。
```

## Replace hardware

Here, use `W25Q128`(16MB) as an example, just remove the original flash and solder on the new flash.

## Configure the flash size in the project

Switch to your `luatos-soc-idf5/luatos` folder and execute the `idf.py menuconfig` command on the `IDF` command line

Enter the` Partition Table`, select` (partitions.8m.csv) Custom partition csv file` press enter, change the file name to the file name represented by the flash size you need (16MB is used here as an example, so change the item to` partitions.16m.csv `), and press enter after change

Press` ESC` to return to the first page, enter `Serial flasher config` -> `Flash size`, and change the flash size according to the actual replacement, with a space or enter to confirm

Press S to save, enter, and then press Q to exit

## Caught-out

The above changes have been completed, and the normal compilation and burning can be done.
