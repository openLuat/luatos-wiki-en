# UART

## Basic information

* Date of drafting: 2020-01-14
* Designer: [chenxuuu](https://github.com/chenxuuu)

## Why need Uart

* Receive and receive data to communicate with external devices

## Design ideas and boundaries

* Manages and abstracts Uart's C API, providing a set of Lua APIs for user code to call

## C API(Platform layer)

```c
//Initialize the configuration of the serial port parameters, and open the serial port
//Successful return 0
int8_t luat_uart_setup(luat_uart_t* uart);
//Close the serial port and return successfully 0
uint8_t luat_uart_close(uint8_t uartid);
//Manually read the serial port data in the cache
uint32_t luat_uart_read(uint8_t uartid, uint8_t* buffer, uint32_t length);
//Send serial port data
uint32_t luat_uart_write(uint8_t uartid, uint8_t* data, uint32_t length);
```

## Lua API

### Constant

```lua
--check digit
uart.Odd
uart.Even
uart.None
--High and low bit sequence
uart.LSB
uart.MSB
```

### Use cases

```lua
local uartid = 1
local maxBuffer = 4*100

local result = uart.setup(
    uartid,--Serial port id
    115200,--Baud rate
    8,--data bit
    1,--Stop bit
    uart.None,--check digit
    uart.LSB,--High and low bit sequence
    maxBuffer,--Buffer Size
    function ()--Receive Callback
        local str = uart.read(uartid,maxBuffer)
        print("uart","receive:"..str)
    end,
    function ()--Send Completion Callback
        print("uart","send ok")
    end
)

if result ~= 0 then--The return value is 0, indicating successful opening
    print("uart open error",result)
end

--Send data
uart.write(uartid,"test")

--The polling method and the callback method are both used.
sys.taskInit(function ()
    while true do
        local data = uart.read(uartid,maxBuffer)
        if data:len() > 0 then
            print(data)
        end
        sys.wait(100)--non-blocking delay function
    end
end)

```
