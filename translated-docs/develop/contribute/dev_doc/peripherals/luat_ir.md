# IR

## Basic information

* Date of drafting: 2021-10-07
* Designer: [chenxuuu](https://github.com/chenxuuu)

## Use

* Implementation of Common Infrared Remote Control Protocols

## Design ideas and boundaries

* Use the chip's own pwm, output 38k/36kHz frequency
* Or directly use the external pwm source and only use gpio to control the on and off.
* Need to support us-level precision delay
* At least implement NEC, RC5, sony protocols, other protocols to be determined
* The receiving and parsing function is to be determined, and can be sent first.

## C API(Platform layer)

Use the interface between gpio and pwm directly

## Lua API

### Constant

Currently None

### Use cases

```lua
ir.sendNEC(
    0x12,--cmd
    0x34,--data
    10,--Number of repetitions
)
```
