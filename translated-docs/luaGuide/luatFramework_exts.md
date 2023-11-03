# Luat framework (advanced-underlying mechanism)

This article deals with the underlying operating mechanism of Luat, which is recommended for beginners to skip.

## get to the point

```lua
------------------------------------------ LUA Application message subscription/publishing interface ------------------------------------------
-- Subscriber List
local subscribers = {}
--Internal Message Queue
local messageQueue = {}

--- Subscribe to messages
-- @param id Message id
-- @param callback Message callback processing
-- @usage subscribe("NET_STATUS_IND", callback)
function subscribe(id, callback)
    if type(id) ~= "string" or (type(callback) ~= "function" and type(callback) ~= "thread") then
        log.warn("warning: sys.subscribe invalid parameter", id, callback)
        return
    end
    if not subscribers[id] then subscribers[id] = {} end    -- If there are no duplicate messages
    subscribers[id][callback] = true        --tag id and callback relationship
end

--- Unsubscribe message
-- @param id Message id
-- @param callback Message callback processing
-- @usage unsubscribe("NET_STATUS_IND", callback)
function unsubscribe(id, callback)
    if type(id) ~= "string" or (type(callback) ~= "function" and type(callback) ~= "thread") then
        log.warn("warning: sys.unsubscribe invalid parameter", id, callback)
        return
    end
    if subscribers[id] then subscribers[id][callback] = nil end  --delete id and callback relationship
end

--- Publish internal messages, stored in the internal message queue
-- @param ... Variable parameters, user-defined
-- @return None
-- @usage publish("NET_STATUS_IND")
function publish(...)
    table.insert(messageQueue, arg)     -- Inserting indeterminate parameters into the queue
end

-- distribution message
local function dispatch()
    while true do
        if #messageQueue == 0 then      --If the queue length is out of loop
            break
        end
        local message = table.remove(messageQueue, 1)   --Get the first of the queue
        if subscribers[message[1]] then                     --If the subscription message exists
            for callback, _ in pairs(subscribers[message[1]]) do
                if type(callback) == "function" then
                    print("unpack",unpack(message, 2, #message))
                    callback(unpack(message, 2, #message))   -- Return second to last
                elseif type(callback) == "thread" then
                    coroutine.resume(callback, unpack(message))
                end
            end
        end
    end
end
```

With sys.publish("TEST",a) and sys.subscribe("TEST",subCallBack), the subscriber list is local subscribers = {}. Internal message queue for local messageQueue = {}：

1、In the publish function, insert the "TEST" message and argument into the messageQueue list

In this messageQueue is{{"TEST",2;n=1}}

2、Determine whether the message and callback types are correct in the subscribe function, and establish the relationship between the message and the callback function in the subscribers if they are correct.

subscribers\["TEST"][subCallBack] = true. Indicates that the return function corresponding to the TEST message is subCallBack

3、In the dispatch() function, get the list of headers.

local message = table.remove(messageQueue, 1)

At this time the message is{"TEST",2;n=1}

Find the callback function or message corresponding to the message. Pass the arguments in the message to the callback function.

The callback function or task corresponding to the message is obtained by traversing the path.

If the callback is a function, pass the parameters at publish time to the callback function.

If the callback is a thread, the thread is awakened.

The above is only an example of a single message, and the same applies to multiple messages, because each cycle will send the head of the messageQueue out of the queue, meeting the FIFO principle.

It is easy to understand the implementation of waitUntil() on the basis

```lua
--- Task The conditional wait function of a task (including conditions such as event messages and timer messages) can only be used in task functions.
-- @param id Message ID
-- @number ms Wait timeout, unit: ms, maximum 126322567 ms
-- @return result Received message returns true, timeout returns false
-- @return data Message Received Return Message Parameters
-- @usage result, data = sys.waitUntil("SIM_IND", 120000)
function waitUntil(id, ms)
    subscribe(id, coroutine.running())
    local message = ms and {wait(ms)} or {coroutine.yield()}
    unsubscribe(id, coroutine.running())
    return message[1] ~= nil, unpack(message, 2, #message)
end
```

1、subscription id, and pass in the thread number

2、Blocking thread, if a message is received, returns message

3、Unsubscribe from this id

4、Return Results

## Operating principle

Lua Support for coroutine, this thing is also known as collaborative multi-threading (collaborative multithreading). Lua provides a separate running line for each coroutine. Let's take an easy-to-understand example: when we go to a restaurant for dinner, suppose there is only one chef in the restaurant. At this time, three guests come and order dishes 1, 2 and 3 respectively. If you cook in the order of one, two, three, the efficiency is very low. Now introduce a new mode, each cauliflower takes 2 minutes to do. The order becomes two minutes for the first course, two minutes to go, the second course, two minutes to go, and then the third course. The advantage of this is that each guest's dish is in the process of being made for a period of time, and there will be no other dishes that must wait until the end of one dish. The guest is God. The second guest is hungry, so you can ask the chef to spend 5 minutes making the second dish. One of the benefits of this is the flexibility to allocate time to each dish. In an inappropriate analogy, the chef is the CPU and the guest is the task.

First look at a simple program：

```lua
co = coroutine.create(										--1
    function(i)
        print(coroutine.status(co))
        print(i);
    end
)

print(coroutine.status(co)) 								--2
coroutine.resume(co, 1)   									--3
print(coroutine.status(co))  								--4

--Output Results
--suspended
--running
--1
--dead
```

-   Creating a coroutine requires a single call to `coroutine.create `. It only receives a single argument, which is the main function of the coroutine. The `create` function simply creates a new coroutine and returns its controller (an object of type thread); it does not start the coroutine.
-   Output the current thread state, which is suspend (suspended, not executed）
-   Wake up the thread, pass in the parameter, execute the thread at this time, the thread state is running, output 1
-   The thread ends and exits normally, `coroutine.resume(co, 1)`returns true. Output thread status, which is dead. Note: you can`t resume after dead (how can a dead person wake up?/funny）

Three states are mentioned here, and a diagram is drawn to describe the relationship between them.

![flow](../img/flow.jpg)

| method | paraphrase                                                         |
| ------------------- | ------------------------------------------------------------ |
| coroutine.create()  | Create coroutine, return thread, parameter is a function after the thread is built belongs to the suspended state, and did not execute！ |
| coroutine.resume()  | Execute the thread, which is used in conjunction with create, when the thread is in the running state.。          |
| coroutine.yield()   | Pending coroutine to set the coroutine to the pending state. The next time resume is executed, the program will return to the suspended position to continue execution instead of restarting from the beginning. Suspend successful return true |
| coroutine.status()  | View the status of the coroutine Note: There are three coroutine statuses：dead，suspend，running。 |
| coroutine.running() | Returns the running coroutine, a coroutine is a thread, when using running, is to return a corouting thread number |

coroutine There are two ways to terminate a run: a normal exit, which means that its main function returns (after the last instruction is run, with or without an explicit return instruction), and an abnormal exit, which occurs when an unprotected error occurs. In the first case, `coroutine.resume` returns true, followed by a series of return values coroutine the main function. In the second case, `coroutine.resume` returns false, followed by an error message.

Next we analyze a more detailed example (quoted in the Lua manual)：

```lua
function foo (a)										--1
    print("foo Function Output", a)
    return coroutine.yield(2 * a) -- Returns the value of 2 * a
end

co = coroutine.create(function (a , b)					--2
    print("First Coprogram Execution Output", a, b) -- co-body 1 10
    local r = foo(a + 1)

    print("Second collaborative program execution output", r)
    local r, s = coroutine.yield(a + b, a - b)  -- a，b The value of is passed in the first call to the co-program.

    print("Third collaborative program execution output", r, s)
    return b, "End the co-program "-the value of B is passed in when the co-program is called the second time.
end)

print("main", coroutine.resume(co, 1, 10)) -- true, 4		--3
print("--Split line----")
print("main", coroutine.resume(co, "r")) -- true 11 -9		--4
print("---Split line---")
print("main", coroutine.resume(co, "x", "y")) -- true 10 end	--5
print("---Split line---")
print("main", coroutine.resume(co, "x", "y")) -- cannot resume dead coroutine	--5
print("---Split line---")

--Output Results
--[[
First Coprogram Execution Output	1	10
foo Function Output	2
main	true	4
--Split line----
Second collaborative program execution output	r
main	true	11	-9
---Split line---
Third collaborative program execution output	x	y
main	true	10	End Collaboration
---Split line---
main	false	cannot resume dead coroutine
---Split line---

]]
```

Obviously, this example is much more complicated than the one above, but it won't be difficult to understand if you analyze it carefully.

-   Call resume to wake up the thread and pass parameters 1 and 10. Output "First coprogram execution output 1 10". Next, execute the foo function and output "foo function output 2". In the foo function, yeild is encountered and the thread is suspended. At this time, the program stays here and resumes execution from there the next time the thread is awakened. Returns the arguments for yeild. Output“main	true	4”。
	   The second call to resume wakes up the thread and passes in the parameter "r". note: the parameter "r" passed in at this time is assigned to coroutine.yield, so it is equivalent to local r = "r" and outputs "output r of second coprogram execution". When yeild is encountered again, the thread is suspended. At this time, the program stays here and resumes execution from there the next time the thread is awakened. Returns the arguments for yeild. Output“main	true	11	-9”。
	   The third call to resume wakes up the thread, passes in the parameters "x" and "y" and assigns them to the coroutine.yield, which is equivalent to local r,s = "r","s" and outputs "xy output from the third coprogram execution". Here the whole thread ends, outputting "main true 10" to end the co-program.”
-   The fourth time resume is called to wake up the thread. At this time, the thread is dead and cannot be woken up.

resume The strength of the combination with yield is that resume is in the main process, which passes external state (data) into the collaborative program, while yield returns internal state (data) to the main process.

Let's take another small example to illustrate the relationship between resume and yield.

```lua
co = coroutine.create (function (a,b)
  local a,b = coroutine.yield(a+b)
  print("co", a,b)
end)
print(coroutine.resume(co,4,5))
coroutine.resume(co, 7, 8)
--Output
--[[
true	9
co	7	8
]]
```

-   Call resume to wake up the thread and pass in 4,5. Encounter yeild, suspend the program, return a B. So the output“true	9”。
-   The second call to resume wakes up the thread and passes in 7,8. At this point, it returns to the last suspended position and assigns a, B. Equivalent local a,b = 7,8



In order to better understand the LuaTask, spent a lot of time to explain Lua collaborative multi-threading, and then get down to business.

Write a test program first

```lua
module(..., package.seeall)


sys.taskInit(function()
    cnt = 0
    while true do
    	cnt = cnt + 1
        print("task_A_cnt: ", cnt)
        sys.wait(1000)
    end
end)

sys.taskInit(function()
    cnt = 0
    while true do
    	cnt = cnt + 1
        print("task_B_cnt: ", cnt)
        sys.wait(2000)
    end
end)
```

Output results, only a small part of the excerpt

```lua
task_B_cnt: 	132
task_A_cnt: 	133
task_A_cnt: 	134
task_B_cnt: 	135
task_A_cnt: 	136
task_A_cnt: 	137
task_B_cnt: 	138
task_A_cnt: 	139
task_A_cnt: 	140
task_B_cnt: 	141
task_A_cnt: 	142
```

The test program has created a total of 2 tasks. The first task is added 1 at a time and suspended for 1000ms. The second task is added 1 at a time and suspended for 2000ms. Therefore, the final output is: output task_A\_cnt twice and output task_ B _cnt once. If you are used to writing UCOS or FreeRTOS on the microcontroller, developers will certainly not be unfamiliar with such a structure.

First, call sys.taskInit to create a task. The format of the task body is

```lua
sys.taskInit(function()
    xxxx
    while true do
		xxxxx
        sys.wait(100)
    end
end)
```

There is also a kind of

```lua
local function xxxx(...)
	xxxx
end
sys.taskInit(xxxx,...)
```



And UCOS, the FreeRTOS task body is roughly the same, a while dead loop, and then switch tasks through delay.

Next, analyze the two important functions of sys.taskInit and sys.wait

First look at the source code of sys.taskInit

```lua
function taskInit(fun, ...)
    local co = coroutine.create(fun)
    coroutine.resume(co, unpack(arg))
    return co
end
```

sys.taskInit It actually encapsulates `coroutine.create` and `coroutine.resume `. Creates a task thread and executes the thread, returning the thread number.

Look again sys.wait

```lua
function wait(ms)
    -- Parameter detection, parameter cannot be negative
    assert(ms > 0, "The wait time cannot be negative!")
    -- Select an unused timer ID for the task thread
    if taskTimerId >= TASK_TIMER_ID_MAX then taskTimerId = 0 end
    taskTimerId = taskTimerId + 1
    local timerid = taskTimerId
    taskTimerPool[coroutine.running()] = timerid
    timerPool[timerid] = coroutine.running()
    -- Call the rtos timer of the core
    if 1 ~= rtos.timer_start(timerid, ms) then log.debug("rtos.timer_start error") return end
    -- Suspend calling task thread
    local message, data = coroutine.yield()
    if message ~= nil then
        rtos.timer_stop(timerid)
        taskTimerPool[coroutine.running()] = nil
        timerPool[timerid] = nil
        return message, data
    end
end
```

How are timers and tasks organized? One of the most important is the taskTimerPool,timerPool these two tables. Before that, the thread number of each thread is unique and unchanged.

Procedure flow：

-   Check whether the timing time is correct
-   Determine whether the timer runs out, and if not, assign an unused timer ID to the task thread.
-   Timer ID Plus 1
-   Store the timer ID number in the taskTimerPool table with the thread number as the subscript.
-   Store the thread ID in the timerPool table with the timer ID number as the subscript.
-   Start Timer

This description is more abstract, and an example will be better understood.

```lua
sys.taskInit(function()
    cnt = 0
    while true do
        print("task: ", 1)
        sys.wait(100)
    end
end)
```

Explain with this simple example

sys.taskInit Create and run the thread, enter the sys.wait function, the initial value of the taskTimerId is 0, so 1,taskTimerId = 1,coroutine.running() returns the thread number of the running task, that is, the thread number of the current task, such as x8218dbc0 0 in this example. Note: The thread number is the only one that does not change. So taskTimerPool[0 x8218dbc0] = 1,timerPool[1] = 0 x8218dbc0. This associates the timer ID with the thread number. Then start the timer, suspend the task, and execute the next task.

Here comes the problem. What should I do when the timer reaches the timing?？

Look at the code below

```lua
function run()
    while true do
        -- Distribute internal messages
        dispatch()
        -- Blocking reading external messages
        local msg, param = rtos.receive(rtos.INF_TIMEOUT)
        -- Determine whether it is a timer message and whether the message is registered
        if msg == rtos.MSG_TIMER and timerPool[param] then
            if param < TASK_TIMER_ID_MAX then
                local taskId = timerPool[param]
                timerPool[param] = nil
                if taskTimerPool[taskId] == param then
                    taskTimerPool[taskId] = nil
                    coroutine.resume(taskId)
                end
            else
                local cb = timerPool[param]
                timerPool[param] = nil
                if para[param] ~= nil then
                    cb(unpack(para[param]))
                else
                    cb()
                end
            end
        --Other messages (audio messages, charge management messages, key messages, etc.）
        elseif type(msg) == "number" then
            handlers[msg](param)
        else
            handlers[msg.id](msg)
        end
    end
end
```

Read the external message, when the timer reaches the timer time, a message will occur.

![rtos](../img/rtos.jpg)

So, msg is rtos.MSG_TIMER and param is the timer ID number.

-   Determine whether the timer is started for the task. If yes, determine whether the timer ID exceeds the maximum value.
-   Get thread number as timerPool and clear
-   If the timer ID and task number can be found in the taskTimerPool, the thread is woken up.

In this way, scheduling between tasks can be achieved.

-----


