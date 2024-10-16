# lcdseg - Segment lcd

{bdg-secondary}`Adaptation status unknown`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/modules/luat_lib_lcdseg.c). If there is any error, please submit issue or help modify pr, thank you！
```


## Constant

|constant | type | explanation|
|-|-|-|
|lcdseg.BIAS_STATIC|number|No bias voltage(bias)|
|lcdseg.BIAS_ONEHALF|number|1/2 Bias voltage(bias)|
|lcdseg.BIAS_ONETHIRD|number|1/3 Bias voltage(bias)|
|lcdseg.BIAS_ONEFOURTH|number|1/4 Bias voltage(bias)|
|lcdseg.DUTY_STATIC|number|100%Duty cycle(duty)|
|lcdseg.DUTY_ONEHALF|number|1/2 Duty cycle(duty)|
|lcdseg.DUTY_ONETHIRD|number|1/3 Duty cycle(duty)|
|lcdseg.DUTY_ONEFOURTH|number|1/4 Duty cycle(duty)|
|lcdseg.DUTY_ONEFIFTH|number|1/5 Duty cycle(duty)|
|lcdseg.DUTY_ONESIXTH|number|1/6 Duty cycle(duty)|
|lcdseg.DUTY_ONESEVENTH|number|1/7 Duty cycle(duty)|
|lcdseg.DUTY_ONEEIGHTH|number|1/8 Duty cycle(duty)|


## lcdseg.setup(bias, duty, vlcd, com_number, fresh_rate, com_mark, seg_mark)



Initialize the lcdseg library

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|bias value, usually 1/3 bias, corresponding lcdseg.BIAS_ONETHIRD|
|int|duty value, usually 1/4 duty, corresponding lcdseg.DUTY_ONEFOURTH|
|int|Voltage, unit 100mV, for example, 2.7v write 27. air103 supported values are 27/29/31/33|
|int|COM The number of feet, depending on the specific module, Air103 supports 1-4|
|int|refresh rate, typically 60, corresponding 60HZ|
|int|COM Enabled or not mask, the default is 0xFF, all enabled. If only COM0/COM1 is enabled, then 0x03|
|int|seg The mask of whether to enable or not, the default is 0xFFFFFFFF, that is, all are enabled. If only the first 16 are enabled, 0xFFFF|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise false|

**Examples**

```lua
-- Initialization lcdseg
if lcdseg.setup(lcdseg.BIAS_ONETHIRD, lcdseg.DUTY_ONEFOURTH, 33, 4, 60) then
    lcdseg.enable(1)

    lcdseg.seg_set(0, 1, 1)
    lcdseg.seg_set(2, 0, 1)
    lcdseg.seg_set(3, 31, 1)
end

```

---

## lcdseg.enable(en)



Enable or disable the lcdseg library

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|1 Enabled, 0 Disabled|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Success or not|

**Examples**

None

---

## lcdseg.power(en)



Enable or disable output for lcdseg

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|1 Enabled, 0 Disabled|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Success or not|

**Examples**

None

---

## lcdseg.seg_set(com, seg, en)



Set the status of a specific segment code

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|COM No.|
|int|seg Bit index of the field to be changed|
|int|1 Enabled, 0 Disabled|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Success or not|

**Examples**

None

---

