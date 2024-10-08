# uart - serial port operation library

{bdg-success}`Adapted` {bdg-primary}`Air780E` {bdg-primary}`Air780EP` {bdg-primary}`Air780EPS` {bdg-primary}`Air780EQ` {bdg-primary}`Air700EAQ` {bdg-primary}`Air700EMQ` {bdg-primary}`Air700ECQ` {bdg-primary}`Air201`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/modules/luat_lib_uart.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view the demo example of uart](https://gitee.com/openLuat/LuatOS/tree/master/demo/uart)
```
```{tip}
There are also video tutorials in this library, [click this link to view](https://www.bilibili.com/video/BV1er4y1p75y)
```

## Constant

|constant | type | explanation|
|-|-|-|
|uart.Odd|number|odd check, case compatibility|
|uart.Even|number|Even parity, case compatibility|
|uart.None|number|No checksum, case compatibility|
|uart.ODD|number|odd check|
|uart.EVEN|number|even check|
|uart.NONE|number|No verification|
|uart.LSB|number|Little Endian Mode|
|uart.MSB|number|Big Endian Mode|
|uart.VUART_0|number|Virtual Serial Port 0|
|uart.ERROR_DROP|number|Discard cached data when error is encountered|
|uart.DEBUG|number|Enable debugging function|


## uart.setup(id, baud_rate, data_bits, stop_bits, partiy, bit_order, buff_size, rs485_gpio, rs485_level, rs485_delay, debug_enable, error_drop)



Configure Serial Port Parameters

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Serial ID, uart0 writes 0, uart1 writes 1, and so on, the maximum value depends on the device|
|int|Baud rate, default 115200, optional baud rate table:{2000000,921600,460800,230400,115200,57600,38400,19200,9600,4800,2400}|
|int|Data bit, default is 8, optional 7/8|
|int|Stop bit, default is 1, according to the actual situation, there can be 0.5/1/1.5/2, etc.|
|int|check digit, optional uart.None/uart.Even/uart.Odd|
|int|Size end, default small end uart.LSB, optional uart.MSB|
|int|buffer size, default 1024|
|int|485 Conversion GPIO in mode, default 0xffffffff|
|int|485 The level of GPIO in the rx direction in the mode, the default value.0|
|int|485 The delay time of tx to rx conversion in the mode, the default value is 12bit time, the unit is us, and the baud rate is 9600.20000|
|int|Turn on the debugging function, enable by default, fill in uart.DEBUG or non-digital enable, other values are turned off, currently only supported by the core shift platform|
|int|Whether to abandon cached data in case of receiving error, enable by default, fill in uart.ERROR_DROP or non-digital enable, other values are closed, currently only supported by the core transfer platform|

**Return Value**

|return value type | explanation|
|-|-|
|int|Returns 0 on success, other values on failure|

**Examples**

```lua
-- Most commonly used 115200 8N1
uart.setup(1, 115200, 8, 1, uart.NONE)
-- can be abbreviated uart.setup(1)

-- 485 Automatic switching, select GPIO10 as the transceiver conversion pin
uart.setup(1, 115200, 8, 1, uart.NONE, uart.LSB, 1024, 10, 0, 2000)
-- Receive error not discarded cached data
uart.setup(1, 115200, 8, 1, uart.NONE, nil, 1024, nil, nil, nil, nil, 0)

```

---

## uart.write(id, data)



Write serial port

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Serial ID, uart0 writes 0, uart1 writes 1|
|string/zbuff|The data to be written, if it is zbuff, it will be read from the starting position of the pointer.|
|int|Optional. The length of the data to be sent. The default value is full.|

**Return Value**

|return value type | explanation|
|-|-|
|int|Success Data Length|

**Examples**

```lua
-- Write visible string
uart.write(1, "rdy\r\n")
-- Write a data string in hexadecimal
uart.write(1, string.char(0x55,0xAA,0x4B,0x03,0x86))

```

---

## uart.read(id, len)



Read Serial Port

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Serial ID, uart0 writes 0, uart1 writes 1|
|int|Read Length|
|file/zbuff|optional: File handle or zbuff object|

**Return Value**

|return value type | explanation|
|-|-|
|string|When reading data/passing in zbuff, return the length of the read and move the zbuff pointer back.|

**Examples**

```lua
uart.read(1, 16)

```

---

## uart.close(id)



Close Serial Port

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Serial ID, uart0 writes 0, uart1 writes 1|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
uart.close(1)

```

---

## uart.on(id, event, func)



Register Serial Port Event Callback

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Serial ID, uart0 writes 0, uart1 writes 1|
|string|Event Name|
|function|Callback Method|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
uart.on(1, "receive", function(id, len)
    local data = uart.read(id, len)
    log.info("uart", id, len, data)
end)

```

---

## uart.wait485(id)



Waiting for TX to complete in 485 mode is only required when mcu does not support serial port sending shift register empty or similar interrupt, and is used after send event callback

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Serial ID, uart0 writes 0, uart1 writes 1|

**Return Value**

|return value type | explanation|
|-|-|
|int|How many cycles have I waited for tx to complete, which is used to observe whether the delay time is sufficient for rough observation. If the return is not 0, it means that it needs to be enlarged.delay|

**Examples**

None

---

## uart.exist(id)



Check whether the serial port number exists

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|The serial port id, uart0 writes 0, uart1 writes 1, and so on.|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Existence return true|

**Examples**

None

---

## uart.rx(id, buff)



buff Form read serial port, read all data at one time and store it in buff. If there is not enough buff space, it will automatically expand. Currently, air105 air780e supports this operation.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Serial ID, uart0 writes 0, uart1 writes 1|
|zbuff|zbuff Object|

**Return Value**

|return value type | explanation|
|-|-|
|int|Returns the length of the read and moves the zbuff pointer backward|

**Examples**

```lua
uart.rx(1, buff)

```

---

## uart.rxSize(id)



Read the remaining amount of data in the serial port Rx buffer. Currently, air105 air780e supports this operation.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Serial ID, uart0 writes 0, uart1 writes 1|

**Return Value**

|return value type | explanation|
|-|-|
|int|Returns the length of the read|

**Examples**

```lua
local size = uart.rxSize(1)

```

---

## uart.rxClear(id)



Clear the remaining amount of data in the serial port Rx buffer. Currently, air105 air780e supports this operation.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Serial ID, uart0 writes 0, uart1 writes 1|

**Return Value**

None

**Examples**

```lua
uart.rxClear(1)

```

---

## uart.tx(id, buff, start, len)



buff Form to write serial port, equivalent to c language uart_tx(uart_id, &buff[start], len);

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Serial ID, uart0 writes 0, uart1 writes 1|
|zbuff|The data to be written, if it is zbuff, it will be read from the starting position of the pointer.|
|int|Optional. The starting position of the data to be sent. The default value is 0|
|int|Optional. The length of the data to be sent is valid data in zbuff by default. The maximum value does not exceed the maximum space in zbuff.|

**Return Value**

|return value type | explanation|
|-|-|
|int|Success Data Length|

**Examples**

```lua
uart.tx(1, buf)

```

---

## uart.createSoft(tx_pin, tx_hwtimer_id, rx_pin, rx_hwtimer_id, adjust_period)



Set the hardware configuration of the software uart. Only SOC that supports hardware timers can be used. Currently, only one can be set. The baud rate has different limits according to the software and hardware configuration of the platform. It is recommended to 9600, the receiving cache does not exceed 65535, and MSB is not supported. Support 485 automatic control. The subsequent setup operation is still required.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Send Pin Number|
|int|Hardware timer for sending ID|
|int|Receive pin number|
|int|Hardware timer for reception ID|
|int|Send timing adjustment, the unit is the timer clock cycle, the default is 0, need to be fine-tuned according to the oscilloscope or logic analyzer|
|int|Receive timing adjustment, the unit is the timer clock cycle, the default is 0, need to be fine-tuned according to the oscilloscope or logic analyzer|

**Return Value**

|return value type | explanation|
|-|-|
|int|the id of the software uart. if it fails, it returns nil|

**Examples**

```lua
-- Initialize software uart
local uart_id = uart.createSoft(21, 0, 1, 2) --air780e It is recommended to use timers 0 and 2. tx_pin is preferably AGPIO to prevent the opposite end from being triggered by mistake during sleep.RX

```

---

## uart.list(max)



Get a list of available serial numbers, currently only win32

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Optional, the default 256, the maximum number of serial ports to obtain|

**Return Value**

|return value type | explanation|
|-|-|
|table|List of available serial port numbers obtained|

**Examples**

None

---

