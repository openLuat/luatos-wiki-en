# 🖥️ Win32

## Description

1. underlying win32 api adaptation
2. UI Based on SDL2
3. compile environment windows sdk, build: xmake, compile tool chain: LLVM
4. file system, win32 native file system, based on the working directory
5. The default luavm and rtos memory allocations are 1MByte

## Compile environment

Install [Visual Studio](https://visualstudio.microsoft.com/zh-hans/vs/) check the windows sdk and install it.

## Compilation Description

- Installation [xmake](https://github.com/xmake-io/xmake/releases)
- Directly execute `xmake` compilation under Luatos/bsp/win32
- luatos.exe will be generated in the build folder

## Simple Usage

- Create a new directory and copy the` luatos.exe `into it (optional, you can use the full path when executing)
- Copy sys.lua to the directory
- Create a new main.lua in the directory and write the following content

```lua
local sys = require "sys"

log.info("sys","from win32")

sys.taskInit(function ()
    while true do
        log.info("hi",os.date())
        log.info("sys",rtos.meminfo("sys"))
        log.info("lua",rtos.meminfo("lua"))
        sys.wait(1000)
    end
end)

sys.run()
```

Execute `.\luatos.exe .\main.lua` or drag the script directly to luatos.exe
