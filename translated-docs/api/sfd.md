# sfd - SPI FLASH Operation Library

{bdg-success}`Adapted` {bdg-primary}`Air105` {bdg-primary}`Air780E/Air700E`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/sfd/luat_lib_sfd.c). If there is any error, please submit issue or help modify pr, thank you！
```


## sfd.init(type, spi_id, spi_cs)



Initialization spi flash

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|type, which can be "spi" or "zbuff", or"onchip"|
|int|SPI the id of the bus, or the zbuff instance|
|int|SPI FLASH GPIO corresponding to the chip selection pin of, only needs to be transmitted when the type is spi|

**Return Value**

|return value type | explanation|
|-|-|
|userdata|Returns a data structure on success, otherwise nil|

**Examples**

```lua
local drv = sfd.init("spi", 0, 17)
if drv then
    log.info("sfd", "chip id", sfd.id(drv):toHex())
end
-- 2023.01.15 After that, the firmware supports the onchip type and supports direct reading and writing of a small area of flash on the chip, which is generally 64k
-- This area is usually the area where the fdb/fskv library is located, so don't mix it up.
local onchip = sfd.init("onchip")
local data = sfd.read(onchip, 0x100, 256)
sfd.erase(onchip, 0x100)
sfd.write(onchip, 0x100, data or "Hi")


```

---

## sfd.status(drv)



check spi flash status

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|sfd.init returned data structure|

**Return Value**

|return value type | explanation|
|-|-|
|int|Status value, 0 not initialized successfully, 1 initialized successfully and idle, 2 busy|

**Examples**

```lua
local drv = sfd.init("spi", 0, 17)
if drv then
    log.info("sfd", "status", sfd.status(drv))
end

```

---

## sfd.read(drv, offset, len)



Read data

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|sfd.init returned data structure|
|int|Start Offset|
|int|Read length, currently limited to 256|

**Return Value**

|return value type | explanation|
|-|-|
|string|Data|

**Examples**

```lua
local drv = sfd.init("spi", 0, 17)
if drv then
    log.info("sfd", "read", sfd.read(drv, 0x100, 256))
end

```

---

## sfd.write(drv, offset, data)



Write Data

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|sfd.init returned data structure|
|int|Start Offset|
|string|The data to be written. Currently, 256 bytes or less are supported.|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success and true on failure false|

**Examples**

```lua
local drv = sfd.init("spi", 0, 17)
if drv then
    log.info("sfd", "write", sfd.write(drv, 0x100, "hi,luatos"))
end

```

---

## sfd.erase(drv, offset)



Erase data

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|sfd.init returned data structure|
|int|Start Offset|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success and true on failure false|

**Examples**

```lua
local drv = sfd.init("spi", 0, 17)
if drv then
    log.info("sfd", "write", sfd.erase(drv, 0x100))
end

```

---

## sfd.id(drv)



The only chip.id

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|sfd.init returned data structure|

**Return Value**

|return value type | explanation|
|-|-|
|string|8 Byte (64bit) chip id|

**Examples**

```lua
local drv = sfd.init("spi", 0, 17)
if drv then
    log.info("sfd", "chip id", sfd.id(drv))
end

```

---
