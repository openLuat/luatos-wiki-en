# timer

This chapter describes how to use the timer library of LuatOS

## Introduction

timer The library uses mcu's hardware timer, which can be created through the timer library.

## Hardware preparation

Any LuatOS-SOC development board

## Software part

Interface documentation can be referred to:[timer library](https://openluat.github.io/luatos-wiki-en/api/timer.html)

### hard blocking

The duration of hard blocking is specified. During the blocking period, no luat code is executed, including the underlying message processing mechanism.

The code is as follows

```lua
PROJECT = "TIMER"
VERSION = "1.0.0"

-- Initialize the watchdog with a timeout 10S
wdt.init(10000)

-- print before blocking starts ticks
log.info("ticks", mcu.ticks())
-- The blocking delay is 5000ms, and most projects will not and should not use this method.
-- This demo is just to demonstrate the availability of API methods
-- mdelay will block the operation of the entire lua vm, and will not respond to any interrupts during the blocking period, including uart
timer.mdelay(5000)
-- Print after blocking ends ticks
log.info("ticks", mcu.ticks())

-- Cycle feeding the dog
while true do
    wdt.feed()
end

```

The log is as follows

```log
I/user.ticks 22
I/user.ticks 5023
```
