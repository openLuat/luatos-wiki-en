# log - Logstore

{bdg-success}`Adapted` {bdg-primary}`Air780E` {bdg-primary}`Air780EP` {bdg-primary}`Air780EPS` {bdg-primary}`Air780EQ` {bdg-primary}`Air700EAQ` {bdg-primary}`Air700EMQ` {bdg-primary}`Air700ECQ` {bdg-primary}`Air201`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/modules/luat_lib_log.c). If there is any error, please submit issue or help modify pr, thank you！
```


## Constant

|constant | type | explanation|
|-|-|-|
|log.LOG_SILENT|number|No log mode|
|log.LOG_DEBUG|number|debug Log Mode|
|log.LOG_INFO|number|info Log Mode|
|log.LOG_WARN|number|warning Log Mode|
|log.LOG_ERROR|number|error Log Mode|


## log.setLevel(level)



Set log level

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|level Log level, which can be a string or a numeric value. The string is (SILENT,DEBUG,INFO,WARN,ERROR,FATAL) and the numeric value is(0,1,2,3,4,5)|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- Set the log level INFO
log.setLevel("INFO")

```

---

## log.style(val)



Set log style

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Log style, the default is 0, do not pass is to get the current value|

**Return Value**

|return value type | explanation|
|-|-|
|int|Current log style|

**Examples**

```lua
-- Take log.info("ABC", "DEF", 123) as an example, assuming that the code is located on line 12 of main.lua
-- Default Log 0
-- I/user.ABC DEF 123
-- Debug style 1, adding additional debugging information
-- I/main.lua:12 ABC DEF 123
-- Debug style 2, add additional debug information, location is different
-- I/user.ABC main.lua:12 DEF 123

log.style(0) -- Default Style 0
log.style(1) -- Debugging style 1
log.style(2) -- Debugging style 2

```

---

## log.getLevel()



Get Log Level

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|int|Log level correspondence 0,1,2,3,4,5|

**Examples**

```lua
-- Get Log Level
log.getLevel()

```

---

## log.debug(tag, val, val2, val3, ...)



output log, level debug

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|tag         Log ID, which must be a string|
|...|Parameters to be printed|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- Log Output D/onenet connect ok
log.debug("onenet", "connect ok")

```

---

## log.info(tag, val, val2, val3, ...)



output log, level info

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|tag         Log ID, which must be a string|
|...|Parameters to be printed|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- Log Output I/onenet connect ok
log.info("onenet", "connect ok")

```

---

## log.warn(tag, val, val2, val3, ...)



output log, level warn

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|tag         Log ID, which must be a string|
|...|Parameters to be printed|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- Log Output W/onenet connect ok
log.warn("onenet", "connect ok")

```

---

## log.error(tag, val, val2, val3, ...)



output log, level error

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|tag         Log ID, which must be a string|
|...|Parameters to be printed|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- Log Output E/onenet connect ok
log.error("onenet", "connect ok")

```

---

