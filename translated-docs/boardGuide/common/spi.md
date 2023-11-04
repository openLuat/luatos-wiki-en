# SPI

This chapter will introduce you to the SPI features of LuatOS. The value of the ID of the SPI will be read and printed in the log using the Air101 development board.

## Introduction

SPI It is an abbreviation of Serial Peripheral Interface (Serial Peripheral Interface). It is a high-speed, full-duplex, synchronous communication bus. The equipment is divided into master and slave. At present, SPI of Air101 can only be used as host.

## Hardware preparation

Air101 A development board, SPI flash a

Connect flash to the development board in SPI line sequence

## Software usage

Interface documentation can be found at:[spi library](https://wiki.luatos.org/api/spi.html)

Code display

```lua
sys.taskInit(
    function()
        local count = 0
        spi.setup(0,20,0,0,8,2000000,spi.MSB,1,1)
        while 1 do
            spi.send(0,string.char(0x9f))
            r = spi.recv(0,3)
            log.info("spi data",r:toHex())
            sys.wait(1000)
        end
    end
)
```
