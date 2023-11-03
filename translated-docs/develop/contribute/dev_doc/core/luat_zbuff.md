# C style buffer

## Basic information

* Date of drafting: 2021-03-24
* Designer: [wendal](https://github.com/wendal)

## What's the use

Directly points to a memory area and provides a series of API operations on this memory.

* The data sent by the package is partially replaced by the pack library.
* As a FrameBuffer, acts as a display buffer in the display frame

## Design ideas and boundaries


## Lua API

Use Example

```lua
-- Create zbuff
local buff = zbuff.create(1024) -- Blank
-- local buff = zbuff.create(1024, "123321456654") -- Create and populate the contents of an existing string.

-- Read and write operations of class file
buff:write("123") -- Write data, pointer moves backward accordingly
buff:seek(0, zbuff.SEEK_SET) -- Set the pointer to the specified position
local str = buff:read(3) -- Read out the three bytes just now, the content is a string, and the pointer has moved back.

-- Read and write by data type
local n = buff:readInt8() -- Support int8~int64,uint8~uint32,float32,double64
-- buff:writeInt8(0x32)   -- Also supports writing the above integer/floating point numbers

-- Support for pack/unpack operations
local _, a, b, c = buff:unpack("IIH") -- Support unpack decoding
-- buff:pack("IIH", 0x1234, 0x4567, 0x12) -- Pack packaging is also supported
log.info("buff", str, n)

-- class array operation
buff:seek(0, zbuff.SEEK_SET) -- Back to the beginning
local b = buff[2] -- Read directly by array to get ASCII code, 0x 32. Note that here, according to C standard
buff[3] = 0x33 -- Direct assignment is OK

-- FrameBuffer Operation
buff:asFB(128, 240, 16) -- Wide, High, Deep
buff:drawLine(1, 2, 1, 20, 0x21) -- Line drawing operation
buff:drawRect(20, 40, 40, 40) -- Draw Rectangle
buff:setPix(1, 2, 0x01) -- Set specific pixel values
buff:drawStr(20, 40, "Chinese is OK ") -writing
buff:clear() -- Empty, fill all 0

-- fetches the contents of the buffer and the return value is string
buff:str() -- The default is 0 ~ limit/len from head to limit value or to tail
buff:str(2, 12) -- Take out a specific offset and length
```


## Relevant knowledge points

* [Luat core mechanism](/markdown/core/luat_core)

