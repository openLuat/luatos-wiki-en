# fonts - Font Library

{bdg-success}`Adapted` {bdg-primary}`Air101/Air103` {bdg-primary}`Air601` {bdg-primary}`Air105` {bdg-primary}`ESP32C3` {bdg-primary}`ESP32S3` {bdg-primary}`Air780E/Air700E`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/luatfonts/luat_lib_fonts.c). If there is any error, please submit issue or help modify pr, thank you！
```


## fonts.list(tp)



Returns the list of fonts supported by the firmware

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Type, default u8g2, can also be lvgl|

**Return Value**

|return value type | explanation|
|-|-|
|table|Font List|

**Examples**

```lua
-- API Added in 2022-07-12
if fonts.list then
    log.info("fonts", "u8g2", json.encode(fonts.list("u8g2")))
end

```

---

## fonts.u8g2_get(name, tp)



Get Font

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Font name, for example opposansm8_chinese unifont_t_symbols|
|string|Type, default u8g2, can also be lvgl|

**Return Value**

|return value type | explanation|
|-|-|
|userdata|If the font is stored, return the font pointer, otherwise return nil|

**Examples**

```lua
oppo_8 = fonts.get("opposansm8_chinese", "u8g2")
if oppo_8 then
    u8g2.SetFont(oppo_8)
else
    log.warn("fonts", "no such font opposansm8_chinese")
end
-- If you use a cloud-compiled custom font library, use it as follows
oppo_8 = fonts.get("oppo_bold_8", "u8g2") -- oppo_bold_8 It is the font name of the cloud compilation interface.
if oppo_8 then
    u8g2.SetFont(oppo_8)
else
    log.warn("fonts", "no such font opposansm8_chinese")
end

```

---

## fonts.u8g2_load(path, path)



Load fonts from file

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Font path, such /luadb/abc.bin|
|string|Type, which is also supported by the default u8g2.lvgl|

**Return Value**

|return value type | explanation|
|-|-|
|userdata|If the font is stored, return the font pointer, otherwise return nil|

**Examples**

```lua
-- API Added in 2022-07-11
-- Reminder: If the file is located under/luadb, it does not need to occupy memory
-- if the file is in another path, such as tf/sd card or spi flash, it will be automatically loaded into the memory, consuming the memory space of lua vm
-- After loading, please refer to it appropriately without loading the same font file repeatedly.
oppo12 = fonts.load("/luadb/oppo12.bin")
if oppo12 then
    u8g2.SetFont(oppo12)
else
    log.warn("fonts", "no such font file oppo12.bin")
end

```

---
