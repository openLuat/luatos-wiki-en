# DISP

This chapter will introduce you to the DISP features of LuatOS. Will achieve the use of Air101 development board driver oled display.

## Introduction

DISP In fact, it encapsulates part of U8G2 API for display, so that everyone can use lua to drive quickly.oled

## Hardware preparation

Air101 A development board, 0.96 inches OLED

Hardware connection diagram

(todo)

## Software usage

Interface documentation can be referred to:[disp library](https://openluat.github.io/luatos-wiki-en/api/disp.html)

Code Introduction

```lua
function display_str(str)
    disp.clear()
    disp.drawStr(str, 1, 18)
    disp.update()
end

local function ui_update()
    disp.clear() -- Clear screen
    disp.drawStr(os.date("%Y-%m-%d %H:%M:%S"), 1, 12) -- Write date
    disp.drawStr("Luat@Air101" .. " " .. _VERSION, 1, 24) -- Write version number
    disp.update()
end

-- Initialize display
log.info("disp", "init ssd1306") -- log library is a built-in library, none of the built-in libraries need require
disp.init({
    mode="i2c_sw",
    pin0=xx--[[Press your board to change your pin number.]],
    pin1=xx--[[Press your board to change your pin number.]],
}) -- Analog i2c,pin0 is SCL,pin1 is SDA, can also use hardware i2c pin
disp.setFont(1) -- Enable Chinese fonts, Wenquanyi dot matrix Song style 12x12
display_str("Starting ...")

sys.taskInit(function()
    while 1 do
        sys.wait(1000)
        log.info("disp", "ui update", rtos.meminfo()) -- rtos is also a built-in library
        ui_update()
    end
end)
```
