# Luat Platform layer

In order to be cross-platform, a "platform layer" is needed to isolate the different underlying implementations.


## Why you need a "platform layer""

* Lua C language implementation, the code is basically cross-platform, but not all
* Luat As an encapsulation of the hardware platform, it needs to be abstracted HAL
* Another point is the abstraction of task scheduling, including timers and message queues.

----------------------------------------------------------------------------------
## Lua cross-platform

What is being discussed here is`Lua 5.3.5`.

Lua Basically, it is cross-platform and can be considered as more than 95%. The following are platform functions that need abstraction.

----------------------------------------------------------------------------------
### Memory allocation

lua Without a global variable, all memory is allocated through the l_alloc parameter (a method pointer) of the lua_newstate(l_alloc, NULL).

The prototype is as follows

```c
void *l_alloc (void *ud, void *ptr, size_t osize, size_t nsize)
```

So, we need to define an abstract C API

```c
void* luat_heap_alloc(void *ud, void *ptr, size_t osize, size_t nsize);
```

Detailed API design, defined in [memory pool](/markdown/core/luat_memory), including the abstract form of` malloc` `luat_heap_malloc`

----------------------------------------------------------------------------------
### io Operation

lua The io operation involves 4 aspects.
* `loadfile` Loading lua files, involving `fopen`/`fread`/`close` and other methods
* `lua_writestring` The original implementation is to pass the data to the standard output.(stdout)
* io Library, involving common POSIX methods such as `fopen`/`fread`/`fseek`/`close`/`feof`/`ferror`
* debug library, involving a `getc` method that reads user input from standard input (stdin)

```c
Lua_FILE * luat_fs_fopen(char const* _FileName, char const* _Mode);
```

Detailed API design, defined in [Filesystem](/markdown/core/luat_fs)

This part of the modification needs to change the lua source code and replace the original api call.

----------------------------------------------------------------------------------
### lua The system inside API

lua The APIs closely related to the system are

```c
time()    // os Module, get system time, need support
popen()   // io/os module, start a new process, no support required, delete the relevant lua API
pclose()  // io/os Module, close process, no support required, delete related lua API
exit()    // Close the process where lua is located, no support is required, and the related lua API is deleted.
```

It can be seen that only the time method must be implemented, and the implementation of time is strongly related to the specific platform, so it must be abstract.

```c
uint8_t luat_os_get_time(*time_t);
```

----------------------------------------------------------------------------------
## Luat cross-platform

The encapsulation of device interfaces and network communications is essentially the encapsulation of vendor private APIs..

----------------------------------------------------------------------------------
### Peripherals

Most peripherals are synchronous APIs, which are very similar to Lua APIs.

```c
uint8_t luat_gpio_setup(LuatGpioPin pin, Lua_Value value, LuatGpioPULL pullup);
```

```lua
local LED = 33
gpio.setup(LED, gpio.LOW, gpio.PULLUP)
```

----------------------------------------------------------------------------------
### Network Communication

This part is very dependent on the SDK of the manufacturer. If it is assumed that communication packages such as lwip are provided, there is not much content in this part..

If you assume that there is no encapsulation such as lwip, you need a very detailed API design (copying lwip is not impossible.)

TODO API design for network communication to be completed

----------------------------------------------------------------------------------
## Task Scheduling


That Luat relies on the API related to task scheduling, rtos layer

* Timer timer -- single/cycle trigger interrupt
* message queue -- message queuing
* semaphore sem -- reserved

In principle, even if there is no rtos, as long as the above three groups of API are implemented, Luat will have no problem running on the bare board..

For hardware that can run Luat, its resources can definitely run rtos. Whether to use rtos or not is determined by the manufacturer SDK..

Therefore, the above three sets of API also have corresponding abstractions.

* [Timer](/markdown/core/luat_timer)
* [Message Bus](/markdown/core/luat_msgbus)
* [Semaphore]() No scene support for the time being, not designed

## Relevant knowledge points

* [Coding Specification](/markdown/proj/code_style)
* [File System](/markdown/core/luat_fs)
* [Timer](/markdown/core/luat_timer)
* [Message Bus](/markdown/core/luat_msgbus)
* [Memory Pool](/markdown/core/luat_memory)
