# pwm - PWM Module

{bdg-success}`Adapted` {bdg-primary}`Air780E/Air700E` {bdg-primary}`Air780EP/Air780EPV` {bdg-primary}`Air601` {bdg-primary}`Air101/Air103` {bdg-primary}`Air105` {bdg-primary}`ESP32C3` {bdg-primary}`ESP32S3`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/modules/luat_lib_pwm.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view the demo example of pwm](https://gitee.com/openLuat/LuatOS/tree/master/demo/pwm)
```

## pwm.open(channel, period, pulse, pnum, precision)



Turn on the specified PWM channel

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|PWM Channel|
|int|Frequency, 1-1000000hz|
|int|Duty cycle 0-frequency division accuracy|
|int|Output cycle 0 for continuous output, 1 for single output, the other for the specified number of pulses output|
|int|Frequency division accuracy, 100/256/1000, default is 100. If the device does not support it, there will be a log prompt.|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Processing results, return true on success, return on failure false|

**Examples**

```lua
-- Turn on PWM5, frequency 1kHz, duty cycle 50%
pwm.open(5, 1000, 50)
-- Turn on PWM5, frequency 10kHz, divided 31/256
pwm.open(5, 10000, 31, 0, 256)

```

---

## pwm.close(channel)



Turn off the specified PWM channel

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|PWM Channel|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No processing result|

**Examples**

```lua
-- Close PWM5
pwm.close(5)

```

---

## pwm.capture(channel)



PWM Capture

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|PWM Channel|
|int|Capture Frequency|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Processing results, return true on success, return on failure false|

**Examples**

```lua
-- PWM0 Capture
while 1 do
    pwm.capture(0,1000)
    local ret,channel,pulse,pwmH,pwmL  = sys.waitUntil("PWM_CAPTURE", 2000)
    if ret then
        log.info("PWM_CAPTURE","channel"..channel,"pulse"..pulse,"pwmH"..pwmH,"pwmL"..pwmL)
    end
end

```

---

