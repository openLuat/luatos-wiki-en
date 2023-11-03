# sys

This chapter describes sys system scheduling for LuatOS

## Introduction

sys The library is a built-in library for system scheduling in LuatOS. Through the sys library, you can create LuatOS tasks, create new timers, and send and subscribe to messages in the system.

## Hardware preparation

Any LuatOS-SOC development board

## Software part

Interface documentation can be found in:[SYS Library](https://openluat.github.io/luatos-wiki-en/api/sys.html)

### Initialize a LuatOS task and start it

The code is as follows

```lua
PROJECT = "SYS"
VERSION = "1.0.0"

-- Introducing the sys library
sys = require("sys")

function test()
    log.info(PROJECT, "test running...")
end

-- Create a task test and execute it
sys.taskInit(test)

-- Start system scheduling
sys.run()

```

The log is as follows

```log
I/user.SYS test running...
```

### Create a loop in a task and use a delay function

The code is as follows

```lua
PROJECT = "SYS"
VERSION = "1.0.0"

-- Introducing the sys library
sys = require("sys")

function test()
    while true do
        log.info(PROJECT, "test running...")
        -- Use wait for 1S delay in task function
        sys.wait(1000)
    end
end

-- Create a task test and execute it
sys.taskInit(test)

-- Start system scheduling
sys.run()
```

The log is as follows

```log
I/user.SYS test running...
I/user.SYS test running...
I/user.SYS test running...
I/user.SYS test running...
I/user.SYS test running...
...
...
```

### Start a single timer or cycle timer

The code is as follows

```lua
PROJECT = "SYS"
VERSION = "1.0.0"

-- Introducing the sys library
sys = require("sys")

function test1(arg)
    log.info(PROJECT, "test1 running...")
    log.info("TEST1-ARG", arg)

end

function test2(arg)
    log.info(PROJECT, "test2 running...")
    log.info("TEST2-ARG", arg)
end

-- Create a one-shot timer
sys.timerStart(test1, 2000, "TEST1")

-- Create a loop timer
sys.timerLoopStart(test2, 2000, "TEST2")

sys.run()
```

The log is as follows

```log
I/user.SYS test1 running...
I/user.TEST1-ARG TEST1
I/user.SYS test2 running...
I/user.TEST2-ARG TEST2
I/user.SYS test2 running...
I/user.TEST2-ARG TEST2
I/user.SYS test2 running...
I/user.TEST2-ARG TEST2
...
...
```

### Stop a timer

The code is as follows

```lua
PROJECT = "SYS"
VERSION = "1.0.0"

-- Introducing the sys library
sys = require("sys")

function test(arg)
    log.info(PROJECT, "test running...")
    log.info("TEST-ARG", arg)
end

sys.taskInit(function()
    -- Create a loop timer
    local tid = sys.timerLoopStart(test, 1000, "TEST_DATA")
    log.info(PROJECT, "5S The post cycle timer will stop running")
    sys.wait(5000)
    -- Stop Cycle Timer
    log.info(PROJECT, "Stop Cycle Timer")
    sys.timerStop(tid)
end)

sys.run()
```

The log is as follows

```log
I/user.SYS 5S The post cycle timer will stop running
I/user.SYS test running...
I/user.TEST-ARG TEST_DATA
I/user.SYS test running...
I/user.TEST-ARG TEST_DATA
I/user.SYS test running...
I/user.TEST-ARG TEST_DATA
I/user.SYS test running...
I/user.TEST-ARG TEST_DATA
I/user.SYS test running...
I/user.TEST-ARG TEST_DATA
I/user.SYS Stop Cycle Timer

```

### Sending and subscribing to user messages

The code is as follows

```lua
PROJECT = "SYS"
VERSION = "1.0.0"

-- Introducing the sys library
sys = require("sys")

local count = 1

sys.subscribe("USER_MSG", function(arg)
    log.info(PROJECT, "receive data : " .. arg)
end)

sys.timerLoopStart(function()
    sys.publish("USER_MSG", "DATA" .. count)
    count = count + 1
end, 3000)

sys.run()
```

The log is as follows

```log
I/user.SYS receive data : DATA1
I/user.SYS receive data : DATA2
I/user.SYS receive data : DATA3
I/user.SYS receive data : DATA4
I/user.SYS receive data : DATA5
...
...
```

### Wait for a message in a task

```lua
PROJECT = "SYS"
VERSION = "1.0.0"

-- Introducing the sys library
sys = require("sys")

local count = 1

sys.taskInit(function()
    local res, data
    while true do
        res, data = sys.waitUntil("USER_MSG")
        log.info(PROJECT, res, data)
    end
end)

sys.timerLoopStart(function()
    sys.publish("USER_MSG", "DATA" .. count)
    count = count + 1
end, 3000)

sys.run()
```

The log is as follows

```log
I/user.SYS true DATA1
I/user.SYS true DATA2
I/user.SYS true DATA3
I/user.SYS true DATA4
I/user.SYS true DATA5
...
...
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
