# Message Bus

## Basic information

* Date of drafting: 2019-11-25
* Designer: [wendal](https://github.com/wendal)

## Why you need a message bus

1. The bottom layer is rtos, and the lua virtual machine runs in a thread.
2. rtos It is required that a thread must give up cpu resources when it is idle, and cannot implement `delay in an endless loop.`
3. lua Single-threaded execution, not friendly to `interrupt` operation
4. Timer/GPIO interrupt/UART data entry/network data entry, all belong to` interrupt`
5. Through the way of message bus, let the `interrupt` information exist in the way of producer, with `rtos.receive` as the consumer.

## Design ideas and boundaries

1. Involving only the collection and disbursement of messages
2. Use a fixed-size message body to save memory resources

## data structure

The message body exists in 4-byte alignment.

```c
struct rtos_msg {
    luat_msg_hanlder handler;
    void* ptr;
} rtos_msg;

#define LUAT_MSGBUS_ITEMCOUNT ((size_t)0xFF)
```

Among them

* handler Message callback function
* ptr     Message payload, determined by specific message type

## C API

```c
void luat_msgbus_init(void);
void* luat_msgbus_data();
uint32_t luat_msgbus_put(struct rtos_msg* msg, size_t timeout);
uint32_t luat_msgbus_get(struct rtos_msg* msg, size_t timeout);
uint32_t luat_msgbus_freesize(void);
```

### Send Message

```c
uint32_t luat_msgbus_put(rtos_msg* msg, size_t timeout);
uint32_t luat_msgbus_get(rtos_msg* msg, size_t timeout);
uint32_t luat_msgbus_freesize(void);
```

### Luat Debugging API

The following API is used for debug and may not be implemented. This module does not provide users for the time being.Lua API

```lua
--  Gets the length of the remaining current queue
rtos.msgbus_current_size() -- Returns a numeric value
-- Get all messages in the queue, which may be empty.
rtos.msgbus_list() -- Return [[msgtype, msgdata], ...]
-- Empty Queue
rtos.msgbus_clear() -- No Return
-- Put in a message
rtos.msgbuf_send(msgtype, msgdata) -- No Return
```

## How to use msgbus

Take a chestnut:

```c
// This function is located in luat_gpio_rtt.c and belongs to the platform-specific implementation
// C layer, the interrupt function called by rtt/freertos/vendor rtos
// Its function is to receive interrupts, package them as rtos_msg objects and submit them to msgbus queue.
int luat_gpio_callback(void *ptr) {
    rtos_msg msg;
    luat_gpio_t *gpio = (luat_gpio_t *)ptr; // When registering a callback function, you can usually pass a custom parameter.
    msg.handler = gpio->hanlder; // The handler here is the l_gpio_handler method mentioned below.
    msg.ptr = ptr; // Pass the data too.
    luat_msgbus_put(&msg, 0); // msg The data of will be copied to msgbu, so just pass the pointer directly.
}
// This function is located in luat_lib_gpio.c and is a generic implementation
int l_gpio_handler(LuaState *L, void *ptr){
    luat_gpio_t *gpio = (luat_gpio_t *)ptr;
    lua_pushinteger(L, MSG_GPIO); // Only msgid is filled in here, instead of logical judgment written in rtos.recv
    lua_pushinteger(L, gpio->pin);
    lua_pushinteger(L, gpio->dist);
    return 3;
}
// rtos.recv wait for the arrival of the new msg, then execute msg.handler(msg.ptr)
// while 1 do
//     local msgid, dataA, dataB = rtos.recv(0)
//     handlers[msgid](dataA, dataB)
// This is the case inside rtos.recv
```
## Relevant knowledge points

* [Luat core mechanism](/markdown/core/luat_core)
