# Power and Low Power Management

## Basic information

* Date of drafting: 2019-11-28
* Designer: [wendal](https://github.com/wendal)

## Why you need power and low power management

* mcu usually provides multiple low power consumption levels, some levels can continue to run lua, some can only run C

## Design ideas and boundaries

* A C API that manages and abstracts power, providing a set of Lua APIs for user code to call
* User can request direct access to the specified low power level

## C API(Platform layer)

```c
uint32_t luat_pm_mode(uint8_t mode);
```

## Lua API

## Constant

```lua
pm.IDLE   -- Idle mode, high power consumption
pm.SLEEP1 -- Sleep mode 1, main memory does not power down, low power memory (lpmem) power down
pm.SLEEP2 -- Sleep mode 2, main memory power down, low power memory (lpmem) does not power down
pm.HIB    -- stop mode, only timer or gpio can wake up
```

### Enter the specified power level

```lua
pm.mode(mode)
```
## Relevant knowledge points

* [Luat core mechanism](/markdown/core/luat_core)

