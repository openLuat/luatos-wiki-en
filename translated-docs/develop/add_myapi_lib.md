# Add custom libraries and functions (full version)

## pre-knowledge

1. For source code compilation of the corresponding module, please refer to [compilation instructions](compile.md)
2. Understanding of Lua's C API [C API Documentation](https://wiki.luatos.org/_static/lua53doc/manual.html#4)
3. Understanding of LuatOS header files [core header file directory](https://gitee.com/openLuat/LuatOS/tree/master/luat/modules)

Again, please make sure that you have compiled a 'standard' firmware under the default configuration, and then add a custom library.

## Codebase involved

There will always be 2 git libraries, and it is highly recommended that you use git to download and manage relevant codes.

* LuatOS The main code of the library 'https://gitee.com/openLuat/LuatOS', later called 'LuatOS' code base
* The library corresponding to the module, for example, the https://gitee.com/openLuat/luatos-soc-2022' corresponding to the Air780E, which is subsequently called the 'bsp' code library.

## Where to put the new files

When trying for the first time, you can first put the' lua' directory of the' luatOS' code library. '.h' put the' lua/include', '.c' file `lua/src`
Because:
* All BSPs will include this header path
* All BSPs will include the. c file for this source path

As for the familiar, there are two directions:
* `LuatOS`Add a new directory to the component directory of the code library and store the library code in the 'include' 'src' directory.
* If it is an implementation of a specific BSP or is not intended to be compatible with other BSPs, it is placed in directories such as' port', 'project/luatos' of the corresponding BSP, and there is no uniform regulation.

## Add an instance of a common library

Here, we add a library called' mymath' and add 3 files, which are stored in the' LuatOS' code library in the following path

* `lua/src/luat_lib_mymath.c` Code that interacts directly with Lua, there must be
* `lua/src/luat_mymath.c` Specific implementation code, optional
* `lua/include/luat_mymath.h` The header file of the corresponding code, optional

Specific code for 'luat_mymath.h' and 'luat_mymath.c'

* No expansion here, they are no different from ordinary C functions.
* If you don't need them, then don't add them, which has no effect on the overall logic.

The following highlights`luat_lib_mymath.c`

```c
#include "luat_base.h"  // must be introduced
#include "luat_mymath.h" // If yes, as required

// Here is a standard Lua C function declaration
static int l_mymath_myplus(lua_State *L) {
    lua_pushstring(L, "myplus"); // Push the string into lua's virtual stack, stack depth+1
    return 1; // Note that this is the number of return values to lua by this function, not the daily ERR_ OK, ERR_FAIL and other 0/1 return values..
}

// Library Function Registry
#include "rotable2.h" // The current version is v2, corresponding rotable2.h
static const rotable_Reg_t reg_mymath[] =
{
    // Function name, the. function pointer to which lua calls l_mymath_myplus
    { "myplus" ,          ROREG_FUNC(l_mymath_myplus)},
    { NULL,               ROREG_INT(0)} // This line must be added at the end.
};

// Here is the declaration of the library, which will be used later.
LUAMOD_API int luaopen_mymath( lua_State *L ) {
    luat_newlib2(L, reg_mymath); // This is the standard way to write, through the rotable2 to generate non-memory library pointers
    return 1; // luat_newlib2 will push an element to the virtual stack, so the return value is also 1
}
```

## Register Library Functions

Before registering, please run the compilation process again and you should be able to see the new files compiled above.
* No new additions are seen-if it is ESP32 series, please execute it first.`idf.py fullclean`
* I don't see the new addition-check the file name, especially the suffix.

After the compilation is passed, proceed to the next step. Currently, it is only compiled and has not been registered to the lua virtual machine.

Modify 'luat/include/luat_libs.h 'in the 'LuatOS' code base to add a new line

```c
LUAMOD_API int luaopen_mymath( lua_State *L );
```

After adding, it is recommended to run the compilation again, usually there will be no error. Unless the semicolon is missed

Now open the c file at the beginning of luat_base_'of 'bsp library'
* For example, in the Air780E library `luat_base_ec618.c`
* For example, in the Air101 library `luat_base_air101.c`

Find the following array declaration
```c
static const luaL_Reg loadedlibs[]
```

There will be many library declarations, in the form

```c
#ifdef LUAT_USE_FASTLZ
  {"fastlz",    luaopen_fastlz},
#endif
```

Corresponding to the normal writing method, macros will be used to control the opening or disabling of a library, and the specific macro definition will be in the 'luat_conf_bsp.h 'file of the 'bsp' code library..

But we don't have to bother here, we just need to add one element, and there is no problem without macro judgment.

```c
{"mymath",    luaopen_mymath},
```

* Be careful not to place '{NULL, NULL}' after the ending element
* `mymath` is the library name and will be registered as a global variable
* `luaopen_mymath` is the name added in 'luat_libs.h'

At this point, the names of the previous steps and the function names will be linked.

* `luat_lib_mymath.c` The specific lua c function, the function list of the declared 'mymath' library and the luaopen_mymath function have been written.
* `luat_libs.h` Modify the statement with the addition of luaopen_mymath
* `luat_base_` At the beginning of the C file, modify the new element of 'loadedlibs'`mymath`

Finally, compile to get the required firmware

## Validation Library Functions

Write 'main.lua' and download it to the device along with' new underlying firmware'

```lua
-- LuaTools Need to PROJECT and VERSION these two information
PROJECT = "apidemo"
VERSION = "1.0.0"

_G.sys = require("sys")

sys.taskInit(function()
    sys.wait(3000)
    while 1 do
        if mymath then
            log.info("Find mymath library")
            log.info("Execute mymath.myplus function", mymath.myplus())
        else
            log.info("Mymath library not found")
        end
        sys.wait(1000)
    end
end)

sys.run()
```

For example, if there is a Chinese function in the code, 'mymath' inventory in the current firmware, it will be printed and executed.`mymath.myplus()`

If the prompt cannot be found:
* Check whether the new bottom layer has been downloaded, perform compilation and cleaning, recompile and download, and observe the compilation time in the startup log.
* Check whether the function name and library name are misspelled

## Follow-up recommendations

After running through the process, if it is a more complicated library, it is recommended:
* Add a dedicated folder to the 'components' directory of the 'LuatOS' repository
* Modify the 'xmake.lua' (not idf5) or 'CMakefile'(idf5) of the 'bsp' code library, refer to the path of other libraries, and add it

Stack size and memory usage issues
* The stack size of most BSP's Lua main thread is 8~12k bytes. Do not use too large specific variables and variable-size arrays.
* Larger memory footprint, you must use the' luat_heap_malloc 'and' malloc' provided by the' luat_malloc.h' function, do not directly use' malloc' and luat_heap_free`free`
* The function should not be blocked for a long time, because lua vm is essentially a single-threaded operation. if you need to perform a time-consuming operation, please refer to' luat_rtos.h 'to create a new underlying task to execute it.
