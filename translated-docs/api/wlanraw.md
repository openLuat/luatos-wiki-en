# wlanraw - WLAN Data RAW Transfer

{bdg-secondary}`Adaptation status unknown`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/device/wlanraw/binding/luat_lib_wlanraw.c). If there is any error, please submit issue or help modify pr, thank you！
```


**Example**

```lua
-- Please consult https://github.com/wendal/xt804-spinet

```

## wlanraw.setup(opts, cb)



Initialize the RAW layer of the WLAN

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|table|opts Configuration Parameters|
|function|callback function, form function(buff, size)|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|true Indicates success, other failures|

**Examples**

```lua
-- Currently only supported by the XT804 series, such Air101/Air103/Air601/Air690
wlanraw.setup({
    buffsize = 1600, -- Buffer size, default 1600 bytes
    buffcount = 10, -- number of buffers, default 8
}, cb)

```

---
