# LuatOS-SoC The file system

## Common file system layouts

```bash
/          # A readable (perhaps writable) piece of flash on the chip, or a virtual file system.
  - luadb  # When you swipe through luatools/luatide, all files are placed in this directory, including scripts and resource files.
  - sd     # The path that will appear after the sd card is mounted. There are fixed sd card/tf card paths on some platforms.
  - lfs    # usually the path to mount the spi flash. you can customize
  - ram    # Memory file system, can read and write. firmware support after 2023-01-12
```

On most platforms, scripts and resource files that are flushed into the device are in the `/luadb` directory and exist in a single folder.

## File System Type

LuatOS-SoC It does not rely on specific rtos, so the file system also has its own set of API, called VFS

Similar to many virtual file system APIs on the market, VFS provides a set of abstract APIs for reading and writing files to isolate differences from the actual hardware environment..

LuatOS-SoC Built-in multiple file system formats

* luadb - Read-only, file system organized based on the TLD(Tag-Len-Data) format with up to 21 bytes of file name.
* lfs   - Read and write, full name lifftefs, https://github.com/littlefs-project/littlefs
* posix - Can read and write, Linux posix standard interface, LuatOS-SoC have encapsulation for it
* fatfs - Read and write, read and write SD card/TF card open source Fat32 file system
* ram   - Can read and write, stored in memory
- romfs - Read-only, standard romfs format

LuatOS-SoC The VFS supports any third-party file system, whether read-only or read-write, and can be docked by implementing relevant operation functions..

## A simple description of read-write

```
/        Can read and write
/luadb/  Read Only
/ram/    Some modules support, can read and write, occupy sys memory, do not consume flash write life, but need to pay attention to delete files
```

```lua
-- The resource file selected when reading the machine.
local f = io.open("/luadb/abc.txt", "r")

-- Open and prepare to write a file with 0 bytes truncated
local f = io.open("/myfile.txt", "w+")
```

For more examples, refer  https://gitee.com/openLuat/LuatOS/blob/master/demo/fs/main.lua
