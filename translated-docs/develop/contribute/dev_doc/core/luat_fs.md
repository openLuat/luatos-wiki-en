# File System

## Basic information

* Date of drafting: 2019-11-28
* Designer: [wendal](https://github.com/wendal)

## Why do you need a file system?

* mcu Built-in flash area or external flash
* Use this area to store lua scripts and other files
* In the future, you may need to use fatfs to mount the sd card.

## Design ideas and boundaries

* The lua api that provides file operations (add, delete, modify and check) is used in the same way as lua's native io module.
* Provide lua virtual machine to read lua scripts C API
* provides additional apis for obtaining file system information, including C and lua

## C API

```c
Luat_FILE luat_fs_fopen(char const* _FileName, char const* _Mode);
uint8_t luat_fs_getc(Luat_FILE stream);
uint8_t luat_fs_fseek(Luat_FILE stream, long offset, int origin);
uint32_t luat_fs_ftell(Luat_FILE stream);
uint8_t luat_fs_fclose(Luat_FILE stream);
```

## Lua API

basic API, same as native io module

### Iterate through folders

```lua
local names = io.lsdir("/ldata/")
```


## Relevant knowledge points

* [Luat core mechanism](/markdown/core/luat_core)

