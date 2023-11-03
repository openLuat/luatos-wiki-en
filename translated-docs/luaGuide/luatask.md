# LuaTask Framework

> Video tutorial can be found here: [[LuatOS] ③ LuaTask multitasking framework](https://www.bilibili.com/video/BV1194y1o7q2)

## Brief description of the framework

LuaTask The framework, using the coroutet, implements the multitasking function in Lua. Developers can create multiple tasks in the simplest way, instead of using timers to delay as in the traditional development method.

When using the LuaTask framework, you need to reference the `sys` library (`_G.sys = require("sys")`) in the code, and on the last line of the code, call `sys.run()`to start the LuaTask framework, and the task code in the framework will run in `sys.run().

## get started use

### Multitasking

```lua
sys = require("sys")
--The first task
sys.taskInit(function()
    while true do
        log.info("task1","wow")
        sys.wait(1000) --Delay 1 second, this time can run other code
    end
end)

--Second task
sys.taskInit(function()
    while true do
        log.info("task2","wow")
        sys.wait(500) --Delay of 0.5 seconds, during which time other code can be run
    end
end)

sys.run()
```

### Multitasking waiting for each other

```lua
sys = require("sys")
--The first task
sys.taskInit(function()
    while true do
        log.info("task1","wow")
        sys.wait(1000) --Delay 1 second, this time can run other code
        sys.publish("TASK1_DONE")--Release this message and all those waiting will receive it at this time.
    end
end)

--Second task
sys.taskInit(function()
    while true do
        sys.waitUntil("TASK1_DONE")--Waiting for this message, this task is blocked here.
        log.info("task2","wow")
    end
end)

--Third task
sys.taskInit(function()
    while true do
        local result = sys.waitUntil("TASK1_DONE",500)--Waiting for timeout time of 500ms, if it exceeds, it will return false and will not wait.
        log.info("task3","wait result",result)
    end
end)

--A separate subscription can be used as a callback.
sys.subscribe("TASK1_DONE",function()
    log.info("subscribe","wow")
end)

sys.run()
```

### Waiting and passing data between multiple tasks

```lua
sys = require("sys")
--The first task
sys.taskInit(function()
    while true do
        log.info("task1","wow")
        sys.wait(1000) --Delay 1 second, this time can run other code
        sys.publish("TASK1_DONE","balabala")--Publish this message and bring a data
    end
end)

--Second task
sys.taskInit(function()
    while true do
        local _,data = sys.waitUntil("TASK1_DONE")--Waiting for this message, this task is blocked here.
        log.info("task2","wow receive",data)
    end
end)

--Third task
sys.taskInit(function()
    while true do
        local result,data = sys.waitUntil("TASK1_DONE",500)--Waiting for timeout time of 500ms, if it exceeds, it will return false and will not wait.
        log.info("task3","wait result",result,data)
    end
end)

--A separate subscription can be used as a callback.
sys.subscribe("TASK1_DONE",function(data)
    log.info("subscribe","wow receive",data)
end)

sys.run()
```

### Traditional Timer

```lua
sys = require("sys")

--Execute a function after one second, you can pass parameters later.
sys.timerStart(log.info,1000,"1s timer")
--Write a function
sys.timerStart(function()
    log.info("1s timer function")
end,1000)

--Execute per second, loop permanently, return timer number
local loopId = sys.timerLoopStart(log.info,1000,"1s loop timer")
--10 Manually stop the infinite loop timer above after seconds
sys.timerStart(function()
    sys.timerStop(loopId)
    log.info("stop 1s loop timer")
end,10000)

sys.run()
```

<script>
window.onload = function(){
    //Add quick test code link near code block
    $("pre").each(function () {
        if($(this).text().indexOf("log.info") >= 0)
            $(this).before('<a class="run-code-btn" href="https://openluat.github.io/luatos-wiki-en/_static/luatos-emulator/lua.html?'+escape($(this).text())+'" target="_blank">Point me to quickly test the following code</a>');
    });
}
</script>

## sys Library Interface Documentation

### sys.wait(ms)

Task The task delay function can only be used in task functions.

* Parameters

|Incoming Value Type | Paraphrase|
|-|-|
|number|ms  Integer, maximum wait 126322567 milliseconds|

* Return Value

The timing end returns nil, which is evoked by other threads and returns the parameters passed in by the calling thread.

* Examples

```lua
sys.wait(30)
```

---

### sys.waitUntil(id, ms)

Task The conditional wait function of a task (including conditions such as event messages and timer messages) can only be used in task functions.

* Parameters

|Incoming Value Type | Paraphrase|
|-|-|
|param|id Message ID|
|number|ms Wait timeout, unit: ms, maximum 126322567 ms|

* Return Value

result Received message returns true, timeout returns false
data Message Received Return Message Parameters

* Examples

```lua
result, data = sys.waitUntil("SIM_IND", 120000)
```

---

### sys.waitUntilExt(id, ms)

Task The conditional wait function extension of a task (including conditions such as event messages and timer messages) can only be used in task functions.

* Parameters

|Incoming Value Type | Paraphrase|
|-|-|
|param|id Message ID|
|number|ms Wait timeout, unit: ms, maximum 126322567 ms|

* Return Value

message Received message returns message, timeout returns false
data Message Received Return Message Parameters

* Examples

```lua
result, data = sys.waitUntilExt("SIM_IND", 120000)
```

---

### sys.taskInit(fun, ...)

Create a task thread, call the function at the end of the module and register the task function in the module, and main.lua can import the module.

* Parameters

|Incoming Value Type | Paraphrase|
|-|-|
|param|fun The name of the task function, which is called when the resume wakes up.|
|param|... Variable parameters of the task function fun|

* Return Value

co  Returns the thread number of the task

* Examples

```lua
sys.taskInit(task1,'a','b')
```

---

### sys.timerStop(val, ...)

Close Timer

* Parameters

|Incoming Value Type | Paraphrase|
|-|-|
|param|val When the value is number, it is identified as timer ID, and when the value is callback function, parameters need to be passed|
|param|... val When the value is a function, the variable parameter of the function|

* Return Value

None

* Examples

```lua
timerStop(1)
```

---

### sys.timerStopAll(fnc)

Turn off all timers for the same callback function

* Parameters

|Incoming Value Type | Paraphrase|
|-|-|
|param|fnc Timer callback function|

* Return Value

None

* Examples

```lua
timerStopAll(cbFnc)
```

---

### sys.timerStart(fnc, ms, ...)

Start a timer

* Parameters

|Incoming Value Type | Paraphrase|
|-|-|
|param|fnc Timer callback function|
|number|ms Integer, maximum timing 126322567 ms|
|param|... Parameters of variable parameter fnc|

* Return Value

number Timer ID, if failed, returns nil

* Examples

None

---

### sys.timerLoopStart(fnc, ms, ...)

Start a cycle timer

* Parameters

|Incoming Value Type | Paraphrase|
|-|-|
|param|fnc Timer callback function|
|number|ms Integer, maximum timing 126322567 ms|
|param|... Parameters of variable parameter fnc|

* Return Value

number Timer ID, if failed, returns nil

* Examples

None

---

### sys.timerIsActive(val, ...)

Determine whether a timer is on

* Parameters

|Incoming Value Type | Paraphrase|
|-|-|
|param|val There are two forms <br> one is the timer id returned when the timer is started. in this form, a timer can be uniquely marked without passing in variable parameters... the other is a callback function when the timer is started. in this form, a timer can be uniquely marked with variable parameters...|
|param|... variable parameter|

* Return Value

number On status returns true, otherwise nil

* Examples

None

---

### sys.subscribe(id, callback)

Subscribe to messages

* Parameters

|Incoming Value Type | Paraphrase|
|-|-|
|param|id Message id|
|param|callback Message callback processing|

* Return Value

None

* Examples

```lua
subscribe("NET_STATUS_IND", callback)
```

---

### sys.unsubscribe(id, callback)

Unsubscribe message

* Parameters

|Incoming Value Type | Paraphrase|
|-|-|
|param|id Message id|
|param|callback Message callback processing|

* Return Value

None

* Examples

```lua
unsubscribe("NET_STATUS_IND", callback)
```

---

### sys.publish(...)

Publish internal messages, stored in the internal message queue

* Parameters

|Incoming Value Type | Paraphrase|
|-|-|
|param|... Variable parameters, user-defined|

* Return Value

None

* Examples

```lua
publish("NET_STATUS_IND")
```

---

### sys.run()

run()Obtain core messages from the bottom layer and process related messages in time, query timers and schedule successfully registered task threads to run and suspend.

* Parameters

None

* Return Value

None

* Examples

```lua
sys.run()
```

