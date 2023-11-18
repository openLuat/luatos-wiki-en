# Lua Library File Writing Guide (Pure Lua Writing)

In actual business, all logic is usually not written to `main.lua`, but is divided into multiple lua files according to functions..

## LuatOS-SoC Basic format of the firmware lua library file

Suppose we want to write a library called `myflib`

### simplest form

```lua
local myflib = {}
return myflib
```

### complete form

```lua
-- [Optional] Import other necessary libraries. Similarly, the way to import this library is reqiure("myflib")
local sys = reqiure("sys")

-- [Must] be defined as a table, which will be returned at the end of the function.
local myflib = {} 

-- [Optional] defines a local variable that is visible only to functions within this file
local myid = "23456"

-- [Optional] Define a library variable. Other lua files can be directly accessed through myflib.mykey or modified
myflib.mykey = "abcdefg"

-- [Optional] defines a local function, only the function in this file is visible
local function myabc()

end

-- [Optional] define a library function, other lua files can be accessed through myflib.myfunc
function myflib.myfunc(key, value)
    return key and "1" or "2"
end

-- [Required] Return library
return myflib
```

## Please note the visibility

1. Variables or functions marked as local can only be accessed/called in this lua file
2. Expose as few variables as possible, use more library functions, and encapsulate them as black boxes.
3. This is lua. The above are all suggestions and can be used on their own.

## A little extended knowledge

1. The library is actually a table, and the table in lua integrates map and list data structures.
2. If any value is returned, the require returns `true`
3. `require` It's also a function, not a keyword.
4. `dofile` Unlike `require`, the latter has one more library loaded by the internal map record.
