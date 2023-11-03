# Luat Timer

## Basic information

* Date of drafting: 2019-11-25
* Designer: [wendal](https://github.com/wendal)

## Why do you need a timer?

1. The need for single timing, for example, to perform temperature measurement after 10 minutes
2. Cycle timing needs, network once every 5 minutes, send heartbeat
3. `sys.wait`The delay execution of lua is realized through the timer mechanism.

## Design ideas and boundaries

1. design based on rtos timer api
2. After the timer expires, the message is produced and sent to the `message bus` for consumption by `rtos.receive`
3. The underlying support loop timing.

## C API(Platform layer)

### data structure

```c
#define LUAT_TIMER_MAXID ((size_t) 0xFFFF)
```

### Interface API

```c
uint32_t luat_timer_start(luat_timer_t* timer);
uint32_t luat_timer_stop(luat_timer_t* timer);
```

## Lua API

## Constant

Spare

```lua
timer.HW -- "HW" Hardware Timer
timer.OS -- "OS" Software Timer
```

### Start Timer

```lua
-- timerout Timeout duration, value, 1-0xFFFFFFFF, unit: milliseconds, meaningful if greater than 0
-- repeat   Extra number of repetitions, value, 1-0xFFFFFFFF, unit milliseconds, default 0
local t = timer.start(timeout, _repeat, function() end)
if not t then
    -- Startup success
else
    -- Start failed, id may be full or timeout value is wrong
end
```

### Turn off timer (including delete)

```lua
-- timer_id clock id, numeric value, 0-0xFF, depends on LUAT_TIMER_MAXID
timer.stop(t)
-- As long as a numeric id is passed in, the timer_stop will always succeed.
```

## Relevant knowledge points

* [Luat core mechanism](/markdown/core/luat_core)
* [Message Bus](/markdown/core/msgbus)
