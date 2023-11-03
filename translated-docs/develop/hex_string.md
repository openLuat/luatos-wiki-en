# Explain lua's string and hex data (hexadecimal)

Thinking Conversion of String and hex (Hexadecimal Number) in Lua

## Background

LuatOS When dealing with communications, data processing is always involved, such:

```lua
socket:send(data)
socket:recv(1024)
spi.send(data)
spi.recv(16)
uart.write(1, data)
```

These methods either send string or return string instead of using byte[]/char[] of other programming languages, because lua does not have byte and char

## What is hex

Let's look at the form of hex first.

```
The ASCII code for the string "1" is 0x31
The ASCII code for the string "2" is 0x32

0x3132         -- Value Identification
"3132"         -- hex String, this HEX string for people to see
"12"           -- Equivalent to 0x 3132 lua string
{0x31, 0x32}   -- lua Array(table)
[0x31, 0x32]   -- java/c/c++of byte[]/char[]
```

## Data Conversion

Write 0x 3132 to uart, socket/spi is the same
```lua
// Method 1, byte-by-byte description using string.char
local data = string.char(0x31, 0x32)
uart.write(id, data)

// Method 2, using string.fromHex, passing in a hex string of multiple length of 2
local data = string.fromHex("3132")
uart.write(id, data)

// Method 3, using the pack library, H represents the number of 2 bytes of symbols, I represents the number of 4 bytes of unsigned, wiki can be checked
local data = pack.pack("H", 0x3132)
uart.write(id, data)
```

Convert the data read by the socket to a numerical value, and the same applies to uart/spi.
```lua
local re, data = socket:recv(1000) -- Wait 1 second
-- Note that data is lua string, content is "12", corresponding to hex value [0x31,0x32]
-- lua string Not an array is not a table and cannot be read directly by subscript.
Print its hex string form
local hexStr, len = string.toHex(data) -- Return value "3132",2, followed by 2 is the length
print(hexStr) -- will be output 3132

--- Method 1, use pack.unpack
-- Decompose into 2 numbers, B is the number of unsigned bytes, and 2 represents the number
local nexti, numa, numb = pack.unpack(data, "b2")
print(numa) -- Numbers 31
print(numb) -- Numbers 32
Take the second number directly and write the position 2
local nexti, numb = pack.unpack(data,"b",2)
print(numb) -- Numbers 32

-- Method 2, use string.byte
local numa = string.byte(data, 1)
local numb = string.byte(data, 2)
print(numa) -- Numbers 31
print(numb) -- Numbers 32
```

## Summary of methods

* string.byte Equivalent data[i-1]
* string.char Equivalent data = {a, b, c, d}
* pack.pack Data Packaging
* pack.unpack Data unpacking
* zbuff library, class C array operations
