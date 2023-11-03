# Add New Library

Total development is divided into several parts:

1. lua Virtual Machine Development/Optimization
2. lua Development of modules
3. Development of supporting tools/features

This chapter mainly describes how to develop a lua module

## Lua Components of Module Development

divided into several necessary parts

1. Design documentation for the module
2. Lua API implementation of the module
3. C API implementation of the module (platform abstraction layer)

Outputs

1. Several documents
2. luat_lib_xxx.c One/many
3. luat_xxx.h One/many
4. luat_xxx_rtt.c/luat_xxx_freertos.c a copy
5. luat_demo_xxx.lua Optional

### Design documentation for the module

Basic template for document

```markdown
# Title

## Basic information

## Why write this function-what function to achieve

## What are the design boundaries-what to do and what not to do

## Lua API -- How is the api exposed to lua code designed? It is best to provide the envisaged code.

## C API -- Abstract platform layer, encapsulate it
```

### Lua API design and implementation

Lua API, It is an API exposed to the user's lua code call, responsible for reading user parameters, verifying parameters, and sorting out return values..

Usually divided into 3 kinds: pure lua, part lua part c, pure c

At present, our design and implementation is generally a pure C implementation, which is similar to this.

```c
int luat_lib_sys_run(Lua_State *L) {
    luaL_checkXXX ; // Take parameters

    lua_sys_xxx   ; // Call C API

    luaL_pushXXXX ; // Push the return value onto the stack

    return 2; // Number of return values
}
```

Then it is accompanied by a registration function, which can refer to lua built-in library, for example `lmathlib.c`

Requirements:

1. In general, only the beginning of the luat_xxx and system functions can be called
2. Do not directly call freertos/rt-thread/private library functions, special case special analysis.
3. Use luat_heap_mallac to allocate memory mainly, pay attention to prevent memory leaks.
4. You may not provide a header file

### C API design and implementation

Requirements:

1. A header file must be provided for Lua API/other C API calls.
2. Use luat_heap_mallac to allocate memory mainly, pay attention to prevent memory leaks.
3. The designed API should try to shield platform differences and provide a consistent external look and feel.
4. By not needing to pass `Lua_State * L`, but instead passing a parameter list or data structure

```c
int luat_gpio_setup(luat_gpio_t conf) {
    //Platform-related implementations...

    return 0; // ok or not
}
```
