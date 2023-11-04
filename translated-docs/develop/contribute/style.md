# Coding Specification

## C API Specification

- C API Start with `luat_`, followed by the module name, followed by the method name
- Use abstract type definitions, such as not using `int`, using`uint32_t`
- Use underline naming

Examples

```c
LUA_API void* luat_heap_alloc(void *ud, void *ptr, size_t osize, size_t nsize);
```

## Lua API Specification

### Naming Specification

- Library name, all lowercase letters
- Constants within the library, all named in uppercase letters, separated by underscores
- The interface in the library is named by hump, the first letter is lowercase, and the writing method separated by underscores is prohibited.**
  - Determine whether the symbol requires an interface, beginning with `is`
  - Open operations begin with `open`
  - Close operation begins with `close`
  - Initialization operation begins with `setup`
  - Change state operations begin with `set`
  - Get status operation starts with `get`
  - Send and receive interface
    - When both `send`/`recv` and `read`/`write` exist
      - Send **raw data** operation starts with `send`
      - The receive **raw data** operation starts with `recv`
      - Send information processed by **c code * * the operation starts with` write` (e.g. write by register address i2c）
      - Receive information processed by **c code** operation starts with `read`
    - When only one of `send`/`recv` and `read`/`write` exists
      - Write according to habit (for example, serial port uses `read`/`write`, while socket uses`send`/`recv`）
  - Asynchronous interface (interface adapted by using `C interface to implement Task waiting function`) starts with `task`

Examples

```c
local spiDevice = spi.setupDevice(0,17,0,0,8,2000000,spi.MSB,1,1)
i2c.isExist(id)
adc.open(id)
camera.setup(...)
```

### Interaction Specification

#### Interface of the returned result

- Only interfaces that may return success or failure
  - Use `true` to return, representing **success**
  - Use `nil,"reason" `to return, representing **failure**
  - **Avoid **using pure numbers to return as a result of success or failure
- Interface that returns progress
  - Returns the number of successful execution progress (such as the length of data sent successfully by the serial port and spi.）
- Interface that returns specific data
  - If the length is returned, use it without data.`0`
  - If the return is` string`, use the string with length 0 if there is no data`
  - If you use `nil` to represent a reception failure, you need to bold the return value section in the interface document.**
- Interfaces that need to wait a while to get results
  - If non-blocking can be achieved
    - You can use callbacks to report events to users.
    - You can adapt` C interface implements Task waiting function` to provide asynchronous interfaces that can be run in tasks ([refer to this page for implementation](https://wiki.luatos.org/develop/c_wait.html)）
    - **Try to avoid using the specific` topic` to allow users to manually call` sys.waitUntil` for adaptation
  - **Try to avoid **adding interfaces with longer blocking times

#### Interfaces that require incoming GPIO

- When the port number **does not exist**, use` negative number` to represent, try to avoid using a specific positive number or 0 (some MCU exist GPIO_0）
- Others to be determined

## Git Submission Specification

1. Master Branch master
2. The development branch, which is opened by the developer. The name follows: issue_xxx, feature_xxx prefix
3. at the time of submission commit
4. Forced push using `git push -f` is strictly prohibited

```
add:  xxxx   adding features, features
update: xxx  Modify functions, features, change behavior
remove: xxxx delete function, feature
fix:  xxxx   Explicitly fix the specified issue, affixing the# issue full number
revert: xxx  Roll back an operation
```

> Special skill: GitHub and Gitee will automatically recognize the beginning of the`# `sign, followed by the syntax of the issue number (spaces are required before and after), which will be automatically associated with the specified issue. And if there are words such as` close` and` fix` in the commit content, the issue will be automatically closed. Such： close #12345

## Lua Interface Annotation Specification

In order to automatically generate the interface document, the interface needs to be annotated strictly in the following format

### Interfaces within the C file

At the top of the file, the format is as follows (if there is no such beginning, the file will not be scanned）：

```c
/*
@module  The name of the module, such：adc
@summary Short description information of the module, such as: Digital-to-analog conversion
@version Version number, optional
@data    Date, optional
@demo    Optional, the folder name in LuatOS/demo
@video   Optional, the library's video tutorial URL
@tag     Optional, the macro definition tags enabled by the library, such：LUAT_USE_ADC
@usage
--Simple use example of this library, optional
--Can write multiple lines
*/
```

Functions that can be called in Lua, in the following format：

```c
/*
The first line states the purpose of the function, such as opening the adc channel.
@api module.function(The full function name used when the call is made.)
@tag Optionally, if the interface only supports some chips, fill in the macro definition tag enabled by the library, such：LUAT_USE_ADC
@string The first argument, @ followed by the argument type, and a space followed by the argument explanation. If there is a default value, it needs to be stated in full.
@number Second parameter
@table The third parameter
...According to the actual, list all parameters
@return The first value returned by the type must be filled in according to the format. If there is a default value, it needs to be fully stated.
@return string The second value returned, of type string
...According to the actual, all return values at the column.
@usage
--The example of use can be multi-line
lcoal a,b,c = module.function("test",nil,{1,2,3})
lcoal d,e,f = module.function("abc",nil,{1,2,3})
*/
static int l_module_function(lua_State *L) {
    //a bunch of code
}
```

Static constants, you can add comments in the following format near the constant：

```c
//@const Constant name type explanation
/*Such*/
//@const NONE number No verification

/*Just write the constant name
  The library name is automatically generated in the document. Result of constant name */
```

### Interfaces within Lua files

At the top of the file, the format is basically the same as that of the C interface, except that there are more library examples. The format is as follows：

```lua
--[[
@module  The call name of the module
@summary Short description information for the module
@version Version number, optional
@data    Date, optional
@author  Author name, optional
@demo    Optional, the name of the demo in the LuatOS/demo folder
@video   Optional, the library's video tutorial URL
@usage
--Examples of use of this library
--Can write multiple lines
]]
```

callable function, basically the same as the c interface format, as follows：

```lua
--[[
The first line states the purpose of the function, such as opening the adc channel.
@api module.function(The full function name used when the call is made.)
@string The first argument, @ followed by the argument type, and a space followed by the argument explanation. If there is a default value, it needs to be stated in full.
@number Second parameter
@table The third parameter
...According to the actual, list all parameters
@return The first value returned by the type must be filled in according to the format. If there is a default value, it needs to be fully stated.
@return string The second value returned, of type string
...According to the actual, all return values at the column.
@usage
--The example of use can be multi-line
--Barabala
]]
```

Static constants, you can add comments in the following format near the constant：

```lua
--//@const Constant name type explanation
--Such
--//@const NONE number No verification

--Just write the constant name
--The library name is automatically generated in the document. Result of constant name
```

## sys_pub published topic description specification

Near `lua_call or lua_pushstring, add relevant explanation to this message

Note that the first line `/*` cannot be preceded by any indentation

```c
        lua_getglobal(L, "sys_pub");
/*
@sys_pub wlan（the module to which the topic belongs）
The first line states the purpose of the message, such as: WIFI scanning is over.
WLAN_SCAN_DONE  （The full name of the topic.）
@string The first data passed, @ followed by data type, space followed by data explanation, if not, don't write these lines
@number Second data
...According to the actual, list all the data passed
@usage
--The example of use can be multi-line
sys.taskInit(function()
    xxxxxxxxxx
    xxxxxxx
    sys.waitUntil("WLAN_SCAN_DONE")
    xxxxxxxxxx
end)
*/
        lua_pushstring(L, "WLAN_SCAN_DONE");
        lua_call(L, 1, 0);
```
