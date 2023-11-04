# log

This chapter introduces how to use the log library of LuatOS.

## Introduction

log The log library is a built-in library for LuatOS to output logs. The log library can output user logs of different levels.

## Hardware preparation

Any LuatOS-SOC development board

## Software part

Interface documentation can be referred to:[log library](https://wiki.luatos.org/api/log.html)

### Log Output

Log rank ordering from low to high is debug < info < warn < error

LuatOS The default log level is debug, which can output logs of debug level and above.

The code is as follows

```lua
PROJECT = "LOG"
VERSION = "1.0.0"

-- Initialize the watchdog with a timeout 10S
wdt.init(10000)

log.debug(PROJECT, "debug message")
log.info(PROJECT, "info message")
log.warn(PROJECT, "warn message")
log.error(PROJECT, "error message")

-- Cycle feeding the dog
while true do
    wdt.feed()
end
```

The log is as follows

```log
D/user.LOG debug message
I/user.LOG info message
W/user.LOG warn message
E/user.LOG error message
```

### Modify log output level

+ SILENT  Silence all logs
+ DEBUG   Output logs above debug level
+ INFO    Output logs above the info level
+ WARN    Output logs above the warn level
+ ERROR   Output logs above the error level

The code is as follows

```lua
PROJECT = "LOG"
VERSION = "1.0.0"

-- Initialize the watchdog with a timeout 10S
wdt.init(10000)

log.setLevel("INFO")
print(log.getLevel())

-- This debug-level log will not be output.
log.debug(PROJECT, "debug message")
log.info(PROJECT, "info message")
log.warn(PROJECT, "warn message")
log.error(PROJECT, "error message")

-- Cycle feeding the dog
while true do
    wdt.feed()
end
```

The log is as follows

```log
2
I/user.LOG info message
W/user.LOG warn message
E/user.LOG error message
```

### modify the log style

+ 0 Default style, including log flag and log content
+ 1 Debug style 1, including the line number of the log printing place
+ 2 Debug style 2, including the line number where the log is printed, but different from debug style 1

The code is as follows

```lua
PROJECT = "LOG"
VERSION = "1.0.0"

-- Initialize the watchdog with a timeout 10S
wdt.init(10000)

log.style(0)
log.debug(PROJECT, "debug message")
log.info(PROJECT, "info message")
log.warn(PROJECT, "warn message")
log.error(PROJECT, "error message")

log.style(1)
log.debug(PROJECT, "debug message")
log.info(PROJECT, "info message")
log.warn(PROJECT, "warn message")
log.error(PROJECT, "error message")

log.style(2)
log.debug(PROJECT, "debug message")
log.info(PROJECT, "info message")
log.warn(PROJECT, "warn message")
log.error(PROJECT, "error message")

-- Cycle feeding the dog
while true do
    wdt.feed()
end
```

The log is as follows

```log
D/user.LOG debug message
I/user.LOG info message
W/user.LOG warn message
E/user.LOG error message
D/main.lua:40 LOG debug message
I/main.lua:41 LOG info message
W/main.lua:42 LOG warn message
E/main.lua:43 LOG error message
D/user.LOG main.lua:46 debug message
I/user.LOG main.lua:47 info message
W/user.LOG main.lua:48 warn message
E/user.LOG main.lua:49 error message
```
