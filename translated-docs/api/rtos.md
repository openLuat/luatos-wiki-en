# rtos - RTOS underlying operation library

{bdg-success}`Adapted` {bdg-primary}`Air780E/Air700E` {bdg-primary}`Air780EP/Air780EPV` {bdg-primary}`Air601` {bdg-primary}`Air101/Air103` {bdg-primary}`Air105` {bdg-primary}`ESP32C3` {bdg-primary}`ESP32S3`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/modules/luat_lib_rtos.c). If there is any error, please submit issue or help modify pr, thank you！
```


## rtos.receive(timeout)   



Accept and process the underlying message queue.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Timeout duration, usually -1, waiting permanently|

**Return Value**

|return value type | explanation|
|-|-|
|msgid|If it is a timer message, the timer message id and additional information will be returned. Other messages are determined by the bottom layer and are not guaranteed to the lua layer..|

**Examples**

None

---

## rtos.timer_start(id,timeout,_repeat)   



Start a timer

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Timer id|
|int|Timeout duration, in milliseconds|
|int|The number of repetitions. The default value is 0|

**Return Value**

|return value type | explanation|
|-|-|
|id|If it is a timer message, the timer message id and additional information will be returned. Other messages are determined by the bottom layer and are not guaranteed to the lua layer..|

**Examples**

```lua
-- User code please use sys.timerStart
-- Start a 3 second cycle timer
rtos.timer_start(10000, 3000, -1)

```

---

## rtos.timer_stop(id)   



Turn off and release a timer

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Timer id|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- User code please use sys.timerStop
rtos.timer_stop(id)

```

---

## rtos.reboot()   



Device restart

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## rtos.buildDate()



Get Firmware Compile Date

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|string|Firmware Compile Date|

**Examples**

```lua
-- Get Compile Date
local d = rtos.buildDate()

```

---

## rtos.bsp()



get hardware bsp model

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|string|hardware bsp model|

**Examples**

```lua
-- get hardware bsp model
local bsp = rtos.bsp()

```

---

## rtos.version()        



Get the firmware version number

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|string|Firmware version number, for example"V0001"|

**Examples**

```lua
-- Read version number
local luatos_version = rtos.version()

```

---

## rtos.standy(timeout)



Enter standby mode, only some devices are available, this API has been abandoned, recommend use pm library

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Sleep duration, in milliseconds|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- Enter standby mode
rtos.standby(5000)

```

---

## rtos.meminfo(type)



Get memory information

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|type|"sys"System memory, "lua" virtual machine memory, "psram" psram memory, default is lua virtual machine memory|

**Return Value**

|return value type | explanation|
|-|-|
|int|Total memory size in bytes|
|int|Size of memory currently used, in bytes|
|int|Maximum memory used in history, in bytes|

**Examples**

```lua
-- Print memory footprint
log.info("mem.lua", rtos.meminfo())
log.info("mem.sys", rtos.meminfo("sys"))

```

---

## rtos.firmware()



Returns the underlying description in the format of LuatOS_$VERSION_$BSP, which can be used to determine the underlying information for OTA upgrades.

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|string|underlying description information|

**Examples**

```lua
-- Print the underlying description information
log.info("firmware", rtos.firmware())

```

---

## rtos.setPaths(pathA, pathB, pathC, pathD)



Set a custom lua script search path with priority over the built-in path

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Path A, such as "/sdcard/%s.luac", defaults to "" if no value is passed. In addition, the maximum length cannot exceed 23 bytes.|
|string|Path B, for example "/sdcard/%s.lua"|
|string|Path C, for example "/lfs2/%s.luac"|
|string|Path D, for example "/lfs2/%s.lua"|

**Return Value**

None

**Examples**

```lua
-- mount sd card or after spiflash
rtos.setPaths("/sdcard/user/%s.luac", "/sdcard/user/%s.lua")
require("sd_user_main") -- will search and load/sdcard/user/sd_user_main.luac and /sdcard/user/sd_user_main.lua

```

---

## rtos.nop()



Empty function, do nothing

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- This function is simply lua -> c -> lua walk again
-- No parameters, no return value, no logical processing
-- In the vast majority of cases, calls to this function will not be encountered.
-- It usually only appears in the code of the performance test, because it does nothing..
rtos.nop()

```

---

## rtos.autoCollectMem(period, warning_level, force_level)



automatic memory collection configuration is a supplement to lua's own collection mechanism. it is not necessary and is only triggered when luavm is idle

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|The cycle of automatic collection is equivalent to the number of receive calls, 0~60000. If it is 0, the automatic collection function is turned off, the default is 100|
|int|The memory uses the warning water level line, which is the percentage of the total luavm memory amount, 50~95, and the memory will not start to judge whether to collect until the memory reaches the (>=) warning line. The default is 80|
|int|The memory usage forced collection water level line is the percentage of the total luavm memory amount, 50~95. When the memory reaches (>=) the forced collection line, it will be forced to collect. The default is 90, which must be greater than the warning water line.|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
rtos.autoCollectMem(100, 80, 90)

```

---

