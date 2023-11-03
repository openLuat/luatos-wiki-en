# GPIO

## Basic information

* Date of drafting: 2019-12-18
* Designer: [wendal](https://github.com/wendal)

## Why need GPIO

* Read the level signal of the external input
* Output specified level

## Design ideas and boundaries

* Manage and abstract the C API of GPIO, and provide a set of Lua API for user code to call

## C API(Platform layer)

```c
#define Luat_GPIO_LOW                 0x00
#define Luat_GPIO_HIGH                0x01

#define Luat_GPIO_OUTPUT         0x00
#define Luat_GPIO_INPUT          0x01
#define Luat_GPIO_INPUT_PULLUP   0x02
#define Luat_GPIO_INPUT_PULLDOWN 0x03
#define Luat_GPIO_OUTPUT_OD      0x04

#define Luat_GPIO_RISING             0x00
#define Luat_GPIO_FALLING            0x01
#define Luat_GPIO_RISING_FALLING     0x02

int luat_gpio_setup(luat_gpio_t* gpio);
int luat_gpio_set(int pin, int level);
int luat_gpio_get(int pin);
int luat_gpio_close(int pin);
```

## Lua API

## Constant

```lua
gpio.OUTPUT          -- Output mode, push-pull
gpio.OUTPUT_OD       -- Output mode, open drain
gpio.INPUT_PULLUP    -- Input mode, pull-up
gpio.INPUT_PULLDOWN  -- Input mode, pull-down
gpio.INPUT           -- input mode, default
gpio.LOW             -- Low level
gpio.HIGH            -- High Level
```

### Enter the specified power level

```lua
-- Simple input
gpio.setup(PIN, gpio.INPUT)
-- Simple output
gpio.setup(PIN, gpio.OUTPUT, 0)
-- interrupt, the value of t of the callback function is GPIO_RISING or GPIO_FALLING
-- The last parameter is interrupt mode, GPIO_RISING_FALLING is the default value, bilateral trigger
gpio.setup(PIN, gpio.INPUT, function(t) end, gpio.GPIO_RISING_FALLING)
-- Set the output to high
gpio.set(PIN, gpio.HIGH)
-- Get the input level or the current output level
gpio.get(PIN)
-- off pin, which is in fact the input high impedance state
gpio.close(PIN)
```
## Relevant knowledge points

* [Luat core mechanism](/markdown/device/luat_core)

