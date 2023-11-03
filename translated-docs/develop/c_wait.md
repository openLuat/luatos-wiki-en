# C Interface implements Task waiting function

## Demand

In many cases, we need to implement such functions：

- Call the interface, but the result will take some time to return.
- While waiting for the results to be obtained, we hope that other tasks will continue to run without blocking the entire system.
- Using the way of subscribing to topic is not easy to remember, very troublesome and not elegant.

In order to solve this problem, you can implement the` Task waiting function` on your C interface. After implementation, you can wait for the data to return, allowing other tasks to continue to run without blocking the entire system.

## User Use

The call method provided to the user, refer to the following demo code (just demo function, actually there is no such interface at present）

```lua
--Waiting for results within task
sys.taskInit(function()
    local data,result,header = http.taskGet("http://xxxxxxxxx").wait()
    log.info("http get",data,result,header)
end)

--Wait callback function
http.taskGet("http://xxxxxxxxx").cb(function(data,result,header)
    log.info("http get",data,result,header)
end)
```

Interfaces start with `task` according to the naming convention.

- The `wait` subscript of the return value after the call is a closure, which can realize the non-blocking waiting function in multitasking.
- The `cb` subscript of the return value after the call is a callback, and the `function` is passed in to implement the asynchronous callback function.

## C Interface Adaptation

### New Waitable Object

In the c interface, you can use the luat_pushcwait function to obtain a waiting object, place it on the top of the stack, and finally return to the user.

At the same time, the function will return a 64-bit id, which is used to publish the message waiting for the end.

```c
static int l_xxxx_block(lua_State *L) {
    uint64_t id = luat_pushcwait(L);//Get the waiting object to the top of the stack and get its id
    //What callback function configuration barabala
    //.....
    //.....
    return 1;//Return the generated wait object for lua to call
}
```

If you only want to return a failed result (such as in the case of initialization failure), you can directly return a waitable object that will only return an error result.

The object will immediately return the given error return value while waiting, and will not return the wait id, without publishing the message.

```c
static int l_xxxx_block(lua_State *L) {
    int initial = xxxxx(xxx);
    if(!initial)//If initialization fails
    {
        lua_pushnil(L);//First Return Value
        lua_pushstring(L,"Failure reason Balabala ");//can add several more return values
        luat_pushcwait_error(L,2);//two return values, so 2
        return 1;//Return the generated wait object for lua to call
    }
    //Handling of Normal Situation Balabala
    //.....
    //.....
}
```

```lua
print(xxx.xxxxxBlock().wait())
--If you go to the failure handling part of the above code, you will return directly.：
--nil  Cause of failure Barabala
```

### Publish Wait End Message

As long as you know the unique id obtained when you initiate the wait, you can stop waiting later.

#### If no return value

Just call the luat_cbcwait_noarg function and pass in the id.

```c
static int l_xxxx_cb(uint64_t id) {
    luat_cbcwait_noarg(id);
}
```

Or call the` luat_cbcwait `function can also be (when there is a lua stack in the function）

```c
static int timer_handler(lua_State *L, void* ptr) {
    luat_timer_t *timer = (luat_timer_t *)ptr;
    uint64_t* idp = (uint64_t*)timer->id;
    luat_cbcwait(L, *idp, 0); //Indicates that this callback has 0 return values
    return 0;
}
```

#### If the function has a return value

First push the parameters to be returned into the lua stack, then call the "luat_cbcwait" function and pass in the id.

```c
static int timer_handler(lua_State *L, void* ptr) {
    luat_timer_t *timer = (luat_timer_t *)ptr;
    uint64_t* idp = (uint64_t*)timer->id;
    lua_pushstring(L,"This is the return value of the demo");
    lua_pushstring(L,"This is the second return value of the demo");
    luat_cbcwait(L, *idp, 2); //Indicates that this callback has 2 return values
    return 0;
}
```

## Full sample code

C Code

```c
// This is just a structure statement for demonstration, which can be expanded according to business needs.
struct struct myctx {
    uint64_t cwait_id; // It is used to save the return value of the luat_pushcwait, which is unique to cwait.id
    void* data;
}myctx_t;

// Three functions are used here.
int l_mylib_abc(lua_State *L); // API registered for lua calls, for example mylib.abc(xxx)
int wbc_abc_cb(myctx_t* ctx ); // It is used to send messages to msgbus of luatos after executing custom logic, because it cannot be directly obtained and called lua_State *L
int wbc_abc_handler(lua_State *L, void* ptr); // used to process the messages sent by the wbc_abc_cb and transfer the cwait results to the lua layer

int l_mylib_abc(lua_State *L) {
    uint64_t id = luat_pushcwait(L);//Get the waiting object to the top of the stack and get its id
    // ------------------
    // First, prepare the data to be processed and put it into context.
    myctx_t* ctx = luat_heap_malloc(sizeof(myctx_t));
    ctx->cwait_id = id;
    ctx->data = NULL; // Put custom data
    // Then, call the custom function
    int ret = wba_abc(ctx); // Note, don't block here, otherwise the effect of cwait will not be reflected, and you can use task or queue to pass it out.
    if (ret) { // If the startup fails, an error is returned directly.
        lua_pushnil(L);//First Return Value
        lua_pushstring(L,"Initialization call wba_abc failed ");//can add several more return values
        luat_pushcwait_error(L,2);//two return values, so 2
        return 1;//Return the generated wait object for lua to call
    }
    // ------------------
    return 1;//Return the generated wait object for lua to call
}

// When data needs to be returned after wba_abc execution
int wbc_abc_cb(myctx_t* ctx) {
    rtos_msg_t msg = {0};
    msg.handler = wbc_abc_handler; // Note that it is quoted here.wbc_abc_handler
    msg.ptr = ctx;                 // Pass the ctx too
    luat_msgbus_put(&msg, 0);
}

// This function will be executed in the main thread of luatos
int wbc_abc_handler(lua_State *L, void* ptr) {
    myctx_t* ctx = (myctx_t*)ptr;
    uint64_t* idp = (uint64_t*)ctx->cwait_id;
    lua_pushinteger(L, 0);
    lua_pushstring(L,"Execution complete");
    luat_cbcwait(L, *idp, 2); //Indicates that this callback has 2 return values
    // The data has been used and released.ctx
    luat_heap_free(ctx);
    // It is OK to return 0 here, which has nothing to do with cwait..
    return 0;
}

// l_mylib_abc The process of registering as mylib.abc, ordinary non-cwait functions are exactly the same, there is no difference
// omitted here, please refer add_myap_5min
```

Lua Code

```lua
sys.taskInit(function()
    local ret,msg = myblib.taskExec("abc").wait()
    log.info("abc", ret, msg)
end)
```

Process of data transfer

```
lua code --> l_mylib_abc --> return cwait object (table) --> lua code calls. wait() to wait asynchronously
l_mylib_abc --> Start task, configure timer, configure interrupt, send message, etc.
Timer timeout/interrupt/task trigger --> execute wbc_abc_cb --> send message to msgbus
luatos The main thread-> receives msgbus message-> executes wbc_abc_handler-> lua code's. wait() returns the result and continues to execute
```
