# rtc - Real Time Clock

{bdg-success}`Adapted` {bdg-primary}`Air101/Air103` {bdg-primary}`Air601` {bdg-primary}`Air105` {bdg-primary}`ESP32C3` {bdg-primary}`ESP32S3` {bdg-primary}`Air780E/Air700E`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/modules/luat_lib_rtc.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view the demo example of rtc](https://gitee.com/openLuat/LuatOS/tree/master/demo/rtc)
```

## rtc.set(tab)



Set Clock

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|table|or int Clock parameters, see example|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise it returns nil or false|

**Examples**

```lua
rtc.set({year=2021,mon=8,day=31,hour=17,min=8,sec=43})
--Currently, only Air101/Air103/Air105/EC618 series support timestamp
rtc.set(1652230554)

```

---

## rtc.get()



Get Clock

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|table|Clock parameters, see example|

**Examples**

```lua
local t = rtc.get()
-- {year=2021,mon=8,day=31,hour=17,min=8,sec=43}
log.info("rtc", json.encode(t))

```

---

## rtc.timerStart(id, tab)



Set RTC wake-up time

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|clock id, usually only supported 0|
|table|Clock parameters, see example|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise it returns nil or false|

**Examples**

```lua
-- It is recommended to set rtc.set to the correct time before use
rtc.timerStart(0, {year=2021,mon=9,day=1,hour=17,min=8,sec=43})

```

---

## rtc.timerStop(id)



Cancel RTC wake-up time

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|clock id, usually only supported 0|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise it returns nil or false|

**Examples**

```lua
rtc.timerStop(0)

```

---

## rtc.setBaseYear(Base_year)



Set RTC base year, no recommend

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|base year Base_year, usually 1900|

**Return Value**

None

**Examples**

```lua
rtc.setBaseYear(1900)

```

---

## rtc.timezone(tz)



Read or set the time zone

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Time zone value, note that the unit is 1/4 time zone. For example, the East Eighth District is 32, not 8. You can not pass it on.|
|return|Time zone value after current/set|

**Return Value**

None

**Examples**

```lua
-- Set to East Zone 8
rtc.timezone(32)
-- Set to East Zone 3
rtc.timezone(12)
-- Set to West Zone 4
rtc.timezone(-16)
-- Note: rtc.get/set is always UTC regardless of the time zone set
-- Time zone affects OS. date/OS. time functions
-- Only some modules support setting the time zone, and the default value is generally 32, that is, the eighth district in the east.

```

---
