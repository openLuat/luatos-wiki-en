# dac - Digital-to-analog conversion

{bdg-success}`Adapted` {bdg-primary}`Air105`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/modules/luat_lib_dac.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view the demo example of dac](https://gitee.com/openLuat/LuatOS/tree/master/demo/multimedia)
```

## dac.open(ch, freq, mode)



Open the DAC channel and configure the parameters

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Channel number, for example 0|
|int|Output frequency, unit hz|
|int|Mode, default 0, reserved|

**Return Value**

|return value type | explanation|
|-|-|
|true|Returns true on success, otherwise false|
|int|The underlying return value, used for debugging|

**Examples**

```lua
if dac.open(0, 44000) then
    log.info("dac", "dac ch0 is opened")
end


```

---

## dac.write(ch, data)



Output a waveform from a specified DAC channel, or a single value

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Channel number, for example 0|
|string|If the output fixed value, you can fill in the value, if the output waveform, fill in string|

**Return Value**

|return value type | explanation|
|-|-|
|true|Returns true on success, otherwise false|
|int|The underlying return value, used for debugging|

**Examples**

```lua
if dac.open(0, 44000) then
    log.info("dac", "dac ch0 is opened")
    dac.write(0, string.fromHex("ABCDABCD"))
end
dac.close(0)

```

---

## dac.close(ch)



Turn off DAC channel

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Channel number, for example 0|

**Return Value**

|return value type | explanation|
|-|-|
|true|Returns true on success, otherwise false|
|int|The underlying return value, used for debugging|

**Examples**

```lua
if dac.open(0, 44000) then
    log.info("dac", "dac ch0 is opened")
    dac.write(0, string.fromHex("ABCDABCD"))
end
dac.close(0)

```

---
