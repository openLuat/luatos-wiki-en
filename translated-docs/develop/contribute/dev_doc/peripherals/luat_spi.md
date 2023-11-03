# SPI

## Basic information

* Date of drafting: 2020-03-27
* Designer: [chenxuuu](https://github.com/chenxuuu)

## Why need SPI

* Receive and receive data to communicate with external devices

## Design ideas and boundaries

* Manage and abstract the C API of SPI, and provide a set of Lua API for user code to call

## C API(Platform layer)

```c
//Initialize the configuration SPI parameters and open SPI
//Successful return 0
int8_t luat_spi_setup(luat_spi_t* spi);
//Close SPI, return successfully 0
uint8_t luat_spi_close(uint8_t spi_id);
//Send and receive SPI data, return the number of bytes received
uint32_t luat_spi_transfer(uint8_t spi_id, uint8_t* send_buf, uint8_t* recv_buf, uint32_t length);
//Receive SPI data, return the number of bytes received
uint32_t luat_spi_recv(uint8_t spi_id, uint8_t* recv_buf, uint32_t length);
//Send SPI data, return the number of bytes sent
uint32_t luat_spi_send(uint8_t spi_id, uint8_t* send_buf, uint32_t length);
```

## Lua API

### Constant

```lua
--High and low bit sequence
spi.LSB
spi.MSB--generally use this
--Master-slave
spi.master
spi.slave
--Half Duplex/Full Duplex
spi.half
spi.full
```

### Use cases

```lua
local spiId = 1
local cs = 10

local result = spi.setup(
    spiId,--Serial port id
    cs,
    0,--CPHA
    0,--CPOL
    8,--Data Width
    20000000,--Maximum frequency 20M
    spi.MSB,--The order of high and low bits is optional, and the default high bit is in the front
    spi.master,--Main mode optional, default main
    spi.full,--Full Duplex Optional, Default Full Duplex
)

if result ~= 0 then--The return value is 0, indicating successful opening
    print("spi open error",result)
end

--Send data
spi.write(spiId,"test")

--Received data
print(spi.recv(spiId,10))

--Sending and Receiving Data
print(spi.transfer(spiId,"test"))
```
