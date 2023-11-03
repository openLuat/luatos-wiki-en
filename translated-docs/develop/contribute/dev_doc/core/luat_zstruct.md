# C The Structure of Style

## Basic information

* Date of drafting: 2022-06-24
* Designer: [wendal](https://github.com/wendal)

## Motivation

* Processing structured data, using the pack library is not intuitive enough, zbuff is suitable for byte-by-byte
* c Layer processing table data is quite troublesome, and a lot of configuration information needs table to be transmitted.

## Design ideas and boundaries

* Supports basic types, does not support nested types, does not support uion
* Support fixed-length data, only supports first-level pointers

## Use Scenario

### modbus synthesis and analysis

Host Send: `01 06 01 05 01 90 99 CB`
Reply from machine: `01 06 01 05 01 90 99 CB`

C The structure is declared as follows
```c
typedef struct modbus {
    uint8_t addr;
    uint8_t func;
    uint16_t regaddr;
    uint16_t data;
    uint16_t crc;
} modbus_t;
```

envisaged lua code, usage A

```lua
modbus_t = zstruct.define([[
typedef struct modbus {
    uint8_t addr;
    uint8_t func;
    uint16_t regaddr;
    uint16_t data;
    uint16_t crc;
} modbus_t;
]])
local modbus = modbus_t:new()
modbus:addr = 0x01
modbus:func = 0x06
modbus:regaddr = 0x0105
modbus:data = 0x0190
modbus:crc = crypto.crc_modbus(modbus)

uart.write(1, zstruct.data(modbus))

local data = uart.read(1, zstruct.sizeof(modbus_t))
modbus_slave = modbus_t:wrap(data)
log.info("modbus", "addr", modbus_slave:addr)
log.info("modbus", "func", modbus_slave:func)
log.info("modbus", "regaddr", modbus_slave:regaddr)
log.info("modbus", "data", modbus_slave:data)
```

envisaged lua code, usage B

```lua
-- define Only the data between {} is needed, other parts can be omitted
modbus_t = zstruct.define([[
    uint8_t addr;
    uint8_t func;
    uint16_t regaddr;
    uint16_t data;
    uint16_t crc;
]])
local modbus = modbus_t:new({
    addr = 0x01,
    func = 0x06,
    regaddr = 0x0105,
    data = 0x0190
})
modbus:crc = crypto.crc_modbus(modbus)

uart.write(1, zstruct.data(modbus))

local data = uart.read(1, zstruct.sizeof(modbus_t))
modbus_slave = modbus_t:wrap(data)
log.info("modbus", modbus_slave:JSON()) -- Output JSON format for easy viewing
```

Imagine code C, which is convenient for the implementation of the C layer, and the user layer is still table

```c
int luat_struct_map(lua_State *L, int index, char* buff, const char* Define);

modbus_t modbus = {0};
// table is the first parameter, and then the passed Define is the modbus_t text form..
int ret = luat_struct_map(L, 1, &modbus, Define);
```

## Data types to be supported

Note: `_t` suffix optional.

```c
// Symbolic Class
char
int8_t
int16_t
int32_t
int // equivalent int32
float

// Unsigned Class
uint8_t
uint16_t
uint32_t

// Pointer Class
char* 

// Array, fixed length, can be the underlying type of a non-pointer class
char[8]
```

## Extended Support

* Support for default values `uint8 addr = 0;`
* Support Bit Field   `uint8 addr : 4;`
