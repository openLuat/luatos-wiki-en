# sfud - SPI FLASH sfud Software package

{bdg-success}`Adapted` {bdg-primary}`Air101/Air103` {bdg-primary}`Air601` {bdg-primary}`Air105` {bdg-primary}`ESP32C3` {bdg-primary}`ESP32S3` {bdg-primary}`Air780E/Air700E`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/sfud/luat_lib_sfud.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view the demo example of sfud](https://gitee.com/openLuat/LuatOS/tree/master/demo/sfud)
```

## sfud.init(spi_id, spi_cs, spi_bandrate)/sfud.init(spi_device)



Initialization sfud

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|spi_id SPI of ID/userdata spi_device|
|int|spi_cs SPI Selected pieces|
|int|spi_bandrate SPI The frequency|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise false|

**Examples**

```lua
--spi
log.info("sfud.init",sfud.init(0,20,20 * 1000 * 1000))
--spi_device
local spi_device = spi.deviceSetup(0,17,0,0,8,2000000,spi.MSB,1,0)
log.info("sfud.init",sfud.init(spi_device))

```

---

## sfud.getDeviceNum()



obtain the total number of devices in the flash device information table

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|int|Returns the total number of devices|

**Examples**

```lua
log.info("sfud.getDeviceNum",sfud.getDeviceNum())

```

---

## sfud.getDevice(index)



Obtain the flash device through the index in the flash information table.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|index flash Index in Information Table|

**Return Value**

|return value type | explanation|
|-|-|
|userdata|Returns a data structure on success, otherwise nil|

**Examples**

```lua
local sfud_device = sfud.getDevice(1)

```

---

## sfud.getDeviceTable()



obtain the flash device information table.

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|userdata|Returns a data structure on success, otherwise nil|

**Examples**

```lua
local sfud_device = sfud.getDeviceTable()

```

---

## sfud.chipErase(flash)



Erase all Flash data

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|flash Flash The data structure returned by the device object sfud.get_device_table()|

**Return Value**

|return value type | explanation|
|-|-|
|int|Successful return 0|

**Examples**

```lua
sfud.chipErase(flash)

```

---

## sfud.erase(flash,add,size)



Erase Flash Specified address Specified size

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|flash Flash The data structure returned by the device object sfud.get_device_table()|
|number|add Erase Address|
|number|size Erase size|

**Return Value**

|return value type | explanation|
|-|-|
|int|Successful return 0|

**Examples**

```lua
sfud.erase(flash,add,size)

```

---

## sfud.read(flash, addr, size)



Read Flash data

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|flash Flash The data structure returned by the device object sfud.get_device_table()|
|int|addr Start Address|
|int|size Total size of data read from start address|

**Return Value**

|return value type | explanation|
|-|-|
|string|data Read data|

**Examples**

```lua
log.info("sfud.read",sfud.read(sfud_device,1024,4))

```

---

## sfud.write(flash, addr,data)



Write data to Flash

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|flash Flash The data structure returned by the device object sfud.get_device_table()|
|int|addr Start Address|
|string|data Data to be written|

**Return Value**

|return value type | explanation|
|-|-|
|int|Successful return 0|

**Examples**

```lua
log.info("sfud.write",sfud.write(sfud_device,1024,"sfud"))

```

---

## sfud.eraseWrite(flash, addr,data)



Erase and then write data to Flash

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|flash Flash The data structure returned by the device object sfud.get_device_table()|
|int|addr Start Address|
|string|data Data to be written|

**Return Value**

|return value type | explanation|
|-|-|
|int|Successful return 0|

**Examples**

```lua
log.info("sfud.eraseWrite",sfud.eraseWrite(sfud_device,1024,"sfud"))

```

---

## sfud.mount(flash, mount_point, offset, maxsize)



mount the sfud lfs file system

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|flash Flash The data structure returned by the device object sfud.get_device_table()|
|string|mount_point Mount directory name|
|int|starting offset, default 0|
|int|Total size, the default is the entire flash|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Successful return true|

**Examples**

```lua
log.info("sfud.mount",sfud.mount(sfud_device,"/sfud"))
log.info("fsstat", fs.fsstat("/"))
log.info("fsstat", fs.fsstat("/sfud"))

```

---
