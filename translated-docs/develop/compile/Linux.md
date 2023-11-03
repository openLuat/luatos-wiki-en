# 🖥️ Linux

## Description

1. Based on posix
2. UI Based on SDL2
3. build: xmake, compile toolchain: LLVM
4. The default luavm memory allocation is 1MByte

## Compilation Description

- Installation xmake： `wget https://xmake.io/shget.text -O - | bash`

- Installation dependency: `sudo apt-get install git make gcc p7zip-full llvm-dev libsdl2-dev -y` (llvm sdl is optional, if xmake is not installed, the source code will be automatically downloaded, compiled and installed, which is extremely time-consuming and recommend installed directly here.）

- Directly execute `xmake` compilation under Luatos/bsp/linux

- luatos The corresponding classification will be generated in the build folder.

## Simple Usage

- Create a new directory and copy `luatos into it (optional, you can use the full path when executing)
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

Execute `./luatos ./main.lua`
