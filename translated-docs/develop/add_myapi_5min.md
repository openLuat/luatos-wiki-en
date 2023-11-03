# Add Custom Functions (Rapid Edition)

Do you have such trouble?
* There is an algorithm implemented by C, how to get it into LuatOS
* I just want to add a function, LuatOS code a lot of what's wrong?

This article takes air101/air103 as an example to introduce how to add functions for lua code to call.

## preparatory knowledge

* A little vscode experience, basic C language programming ability
* Can compile existing code, such as air101/air103. If not, first look at [Compile Tutorial](compile/Air101.md)
* It is better to understand the basic operation of git.

## Expected effect

```lua
local ok, msg = sayhi("wendal", 18)
log.info("custom", ok, msg) -- Print true/false and a paragraph
```

## Create a new c file and store the sample code.

1. Using vscode, open the source directory of air101/air103
2. Navigate to the app/custom directory to create a new file `myapi.c`
3. Paste the following

```c
//-------------------
//    LuatOS The header file for, located in the LuatOS library's luat/include
#include "luat_base.h"   // basic function
#include "luat_malloc.h" // memory allocation function

#define LUAT_LOG_TAG "custom"

#include "luat_log.h"    // log function, supporting LUAT_LOG_TAG

// -------------------
// Need to use the header file, import it yourself
//#include "xxx.h"
// -------------------

// function prototype int $name(lua_State *L)
// int The number of return values, which refers to the number of values popped from lua's stack as return values.
// C The function prototype has only one parameter, and the corresponding lua parameter is in the virtual stack of lua.
static int luat_custom_sayhi(lua_State *L) { // static Not necessary, but recommend added.
    size_t len;
    const char* name = luaL_checklstring(L, 1, &len); // corresponds to the first parameter of the lua call,  "wendal"
    int age = luaL_checkinteger(L, 2);                // corresponding to the second parameter of lua call, the value 123

    // age If it's less than 19, it's "the right age.""
    if (age < 19) {
        // Push 2 return values into lua stack
        lua_pushboolean(L, 1); // Pressing in true
        lua_pushfstring(L, "%s's age is %d", name, age); // age is correct!!!
    }
    // Otherwise, the current is the "wrong age"."
    else {
        // Push 2 return values into lua stack
        lua_pushboolean(L, 1); // Pressing in false
        lua_pushfstring(L, "%s's age is 18, always!!", name); // No, wendal is only 18
    }
    return 2; // Here is the number of return values, not the correct/error return code such as 0/1.
}

void luat_custom_init(lua_State *L) { // Function declaration in luat_base.h
    LLOGD("custom init begin ...");

    // Add a global function, the function name is sayhi
    lua_pushcfunction(L, luat_custom_sayhi); // Push stack first
    lua_setglobal(L, "sayhi");               // Pressing in _G.sayhi = XXX

    LLOGD("custom init done");
    return; // No return value required.
}
```

## Enable custom function construction

1. Open `app/include/luat_conf_bsp.h`
2. In the middle, add the following macro definition

```c
#define LUAT_HAS_CUSTOM_LIB_INIT 1
```

## test happily

1. Execute xmake and compile it regular
2. Brush machine, under the script, see the effect

```log
You fill in ^_^
```

* Compilation failed? Fix syntax errors according to prompts
* After brushing the machine, the prompt cannot find the` sayhi`-make sure the compilation is correct, make sure the correct underlying firmware file is selected, and make sure the new underlying layer is brushed in.

## Can't do it? Ask for help? To the QQ group for help or gitee post.

1. QQ Group: 1061642968
2. gitee: https://gitee.com/openLuat/LuatOS By the way, order a star, thank you

## Expansion 1-there are many functions, can I make a library?

It must be possible. It is also a quick way to change it directly with a ready-made copy of lib_xx.c.

* First, give the library a cool name.
* Imitate the existing `luat_lib_xxx.c `to write a library
* At first, it was suggested to write only one function and declare the "rotable_Reg" and "luaopen_xxx" methods.
* Call the luaopen_xxx method in the custom_init.
* If you do not use custom_init, luat_base_xxx.c also declares

## Expansion 2-Add static libraries for linking

* Need a little xmake knowledge, but the search string can also be added
* Put the file into the lib directory, assuming the name is `libcool.a`
* Modify `xmake.lua` to look ` ./lib/libgt.a `
* Before or after it, add `./lib/libcool. a`, noting the space before and after
* happily compiled
