# PM

This chapter will introduce you to the PM features of LuatOS. Will achieve the use of Air101 development board display and print in the log effect.

## Introduction

MCU module package mcu some special operations

## Hardware preparation

Air101 A development board

## Software usage

Interface documentation can be referred to:[pm library](https://wiki.luatos.org/api/pm.html)

Code display

```lua
-- Add an underlying timer
pm.dtimerStart(0, 300 * 1000) -- 5 Wake up after minutes
-- Request to enter sleep mode
pm.request(pm.HIB)
-- Turn off the underlying timer
-- pm.dtimerStop(0) -- Turn off the underlying timer with id = 0
```
