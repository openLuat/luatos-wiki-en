# sfd

This chapter will introduce you to the sfd features of LuatOS. It will be possible to connect an external spi flash using the Air101 development board and directly read and write through sfd mounting.

## Introduction

Generally, the read and write instructions of the external spi flash are compatible. In the process of daily use, it will be very troublesome if we directly use spi to read and write flash through instructions. As a result, LuatOS has designed a set of interfaces to realize abstract read and write of the external spi flash through this set of interfaces, and to realize simple read and write of Lua's io.

## Hardware preparation

Air101 A development board, a plug-in spi flash

## Software usage

Interface documentation can be found in:[sfd library](https://wiki.luatos.org/api/sfd.html)

Code display

```lua
sys.taskInit(
    function()
        local drv = sfd.init("spi", 0, 20)
        if drv then
            log.info("sfd", "chip id", sfd.id(drv):toHex())
        end
        while 1 do
            if sfd.status(drv) == 1 then
                log.info("sfd", "write", sfd.write(drv, 0x100, "hi,luatos"))
                log.info("sfd", "read", sfd.read(drv, 0x100, 32))
            end
            sys.wait(1000)
        end
    end
)
```
