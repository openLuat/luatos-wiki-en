# Method of use

## Acquisition method

1. Through [co-disk](https://pan.air32.cn/s/DJTr?path=/LuatOS模拟器(win32)), select' PC simulator' to download
2. Self-compiled, [source code address](https://gitee.com/openLuat/luatos-soc-pc) need to be compiled with LuatOS main library

## Operation mode

### Interactive

* windows Platform, directly double-click 'luatos-pc.exe' to run
* linux platform, run directly `./luatos-pc`

### Script Run

This method executes "single script" and "multi-directory execution" in two ways, which need to be operated under the command line.

Take the windows platform as an example, first enter the console and switch to the current directory.

Single script run:

```cmd
luatos-pc.exe main.lua
luatos-pc.exe test\001.helloworld\main.lua
```

Multi-Directory Run:

```cmd
luatos-pc.exe test\001.helloworld\  ..\LuatOS\scrips\libs\
```
