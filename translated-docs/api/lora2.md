# lora2 - lora2 Driver module (supports multi-hanging)

{bdg-success}`Adapted` {bdg-primary}`ESP32C3` {bdg-primary}`ESP32S3` {bdg-primary}`Air780E/Air700E`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/lora2/luat_lib_lora.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view lora2 demo examples](https://gitee.com/openLuat/LuatOS/tree/master/demo/lora)
```

## Constant

|constant | type | explanation|
|-|-|-|
|lora2.SLEEP|number|SLEEP Mode|
|lora2.STANDBY|number|STANDBY Mode|


## lora2.init(ic, loraconfig,spiconfig)



lora Initialization

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|lora Model, currently supported：<br>llcc68<br>sx1268|
|table|lora Configuration parameters, device-specific|

**Return Value**

|return value type | explanation|
|-|-|
|userdata|Returns the lora object if successful, otherwise nil|

**Examples**

```lua
spi_lora = spi.deviceSetup(spi_id,pin_cs,0,0,8,10*1000*1000,spi.MSB,1,0)
lora_device = lora2.init("llcc68",{res = pin_reset,busy = pin_busy,dio1 = pin_dio1},spi_lora)

```

---

## lora_device:set_channel(freq)



Set channel frequency

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|number|Frequency|

**Return Value**

None

**Examples**

```lua
lora_device:set_channel(433000000)

```

---

## lora_device:set_txconfig(txconfig)



lora Configure Send Parameters

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|table|lora Send configuration parameters, device-specific|

**Return Value**

None

**Examples**

```lua
lora_device:set_txconfig(
    {
        mode=1,
        power=22,
        fdev=0,
        bandwidth=0,
        datarate=9,
        coderate=4,
        preambleLen=8,
        fixLen=false,
        crcOn=true,
        freqHopOn=0,
        hopPeriod=0,
        iqInverted=false,
        timeout=3000
    }
)

```

---

## lora_device:set_rxconfig(set_rxconfig)



lora Configure Receive Parameters

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|table|lora Receive configuration parameters, which are device-specific|

**Return Value**

None

**Examples**

```lua
lora_device:set_rxconfig(
    {
        mode=1,
        bandwidth=0,
        datarate=9,
        coderate=4,
        bandwidthAfc=0,
        preambleLen=8,
        symbTimeout=0,
        fixLen=false,
        payloadLen=0,
        crcOn=true,
        freqHopOn=0,
        hopPeriod=0,
        iqInverted=false,
        rxContinuous=false
    }
)

```

---

## lora_device:send(data)



Send data

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Data written|

**Return Value**

None

**Examples**

```lua
lora_device:send("PING")

```

---

## lora_device:recv(timeout)



Open data collection

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|number|Timeout, default 1000 unit ms|

**Return Value**

None

**Examples**

```lua
sys.subscribe("LORA_RX_DONE", function(data, size)
    log.info("LORA_RX_DONE: ", data, size)
    lora_device:send("PING")
end)
lora_device:recv(1000)

```

---

## lora_device:mode(mode)



Set the entry mode (sleep, normal, etc.)

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|number|Mode Normal Mode: lora.STANDBY Sleep Mode: lora.SLEEP Default to Normal Mode|

**Return Value**

None

**Examples**

```lua
lora_device:mode(lora.STANDBY)

```

---

## lora_device:on(cb)



Register lora callback

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|function|cb lora callback, parameters include lora_device, event, data, size|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
lora_device:on(function(lora_device, event, data, size)
    log.info("lora", "event", event, lora_device, data, size)
    if event == "tx_done" then
        lora_device:recv(1000)
    elseif event == "rx_done" then
        lora_device:send("PING")
    elseif event == "tx_timeout" then

    elseif event == "rx_timeout" then
        lora_device:recv(1000)
    elseif event == "rx_error" then

    end
end)
--[[
event The possible values are
    tx_done         -- Send Complete
    rx_done         -- Receive Completed
    tx_timeout      -- Send Timeout
    rx_timeout      -- Receive Timeout
    rx_error        -- Receive Error
]]

```

---
