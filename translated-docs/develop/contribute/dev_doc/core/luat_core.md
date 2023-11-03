# Luat Core

## Basic information

* Date of drafting: 2019-11-25
* Designer: [wendal](https://github.com/wendal)

## Luat What is the core of existence?

In general, the core of Luat is the most basic facility and core process for running luat scripts.

### The most basic facilities include

* Lua Virtual Machine-responsible for parsing and executing scripts
* message bus-one-way information transfer with host rtos system
* File system-read and write files, for the core, read lua scripts
* Timer-delay mechanism within the script

### core processes (Lua expresses)

Responsive Processing Based on Message Bus

```lua
while 1 do
    local msgtype, msgdata = rtos.receive(0)
    if msgtype and handlers[msgtype] then
        handlers[msgtype](msgtype, msgdata)
    end
end
```

Line-by-line description
```lua
-- The outermost layer is an infinite loop that never exits unless something goes wrong.
while 1 do
    -- Receive information from message queue, wait indefinitely
    -- msgtype is the type of message, always a numeric value
    -- msgdata is the content of the message and does not necessarily exist
    local msgtype, msgdata = rtos.recv(0)
    -- handlers is the message processor's table
    if msgtype and handlers[msgtype] then
        -- If there is a processor for the corresponding msgtype, it is executed.
        handlers[msgtype](msgtype, msgdata)
    end
end
```

## Core processes (C-level)

```c
void l_rtos_recv(luaState* L) {
    rtos_msg msg;
    uint32_t re;
    re = luat_msgbus_get(&msg, lua_checkint(L, 1));
    if (re) {
        // TODO Feed the dog
    }
    else {
        msg.handler(L);
    }
}
```

### About msg.handler

In order to isolate the specific processing logic, msg contains msg.ptr and msg.handler
1. Note that there is no msg.id, this part of data is set to lua stack by msg.handler callback function
2. msg.ptr The data required by msg.handler is implicitly passed through the luat_msgbus_data..

## Relevant knowledge points

* [Timer](/markdown/core/luat_timer)
* [Message Bus](/markdown/core/luat_msgbus)
* [File System](/markdown/core/luat_fs)
