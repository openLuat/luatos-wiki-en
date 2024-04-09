# LuatOS IO multiplexing configuration under firmware

**This document describes the LuatOS perspective**

1. Due to the existence of firmware characteristics, LuatOS's io multiplexing is fixed by default. Starting from V1107, the mapping can be partially modified through mcu.iomux function.
2. The external pin layout of different modules is different, but the' PAD(paddr)'value is the same. to correspond to the "PIN/GPIO correspondence table" document, the document can be found in [air780e.cn](https://air780e.cn)
3. Due to the small number of pins in the chip, there are a large number of multiplexing scenarios, and many functions will conflict.
4. For AT firmware, this document is meaningless, please ignore it.
5. For CSDK, relevant reuse can be modified, so please ignore this document
6. Air600E Doomed not suitable for secondary development, some pins in the hardware design manual description will be different, pay attention to distinguish
7. Cloud compilation and mcu.iomux function can adjust part of the reuse relationship, please refer to the document linked to [mcu library](https://wiki.luatos.org/api/mcu.html)
8. If SIM2 is used, it will take up 4 IO(gpio4/5/6/23)

## PWM Description

The actual available channels are 4 (0/1/2/4), but each has 3 configurations, PWM3/PWM5 has been used by the bottom layer.

For example, PWM1 and PWM11 both use hardware channel 1, **only one of them can be selected to use**.

PWM11 cannot be enabled when PWM1 is enabled. when calling the API of pwm library, fill in` software channel id`

|software channel id | actual hardware channel | corresponding GPIO | corresponding PAD | remarks|
|----------|------------|---------|---------|----|
|0         |    0       | GPIO23  |    43   | AGPIO3,Weak drive capability. **Avoid using this pin**|
|1         |    1       | GPIO24  |    44   | MAIN_RI,Actually AGPIO4, weak driving ability |
|2         |    2       | GPIO25  |    45   | AGPIO5,weak driving ability|
|4         |    4       | GPIO27  |    47   | NetLed,Network Status Light |
|10        |    0       | GPIO1   |    16   | LCD_RST,Actual is normal GPIO|
|11        |    1       | GPIO2   |    17   | MAIN_DCD,Actual is normal GPIO |
|12        |    2       | GPIO16  |    31   | MAIN_CTS,Actual is normal GPIO |
|14        |    4       | GPIO18  |    33   | UART1_RXD/MAIN_RXD |
|20        |    0       | None      |    39   | I2S_MCLK|
|21        |    1       | GPIO29  |    35   | I2S_BCLK |
|22        |    2       | GPIO30  |    36   | I2S_LRCK |

PS:

1. Software channel 10/11/12/14 requires firmware above V1002, 20221219 later compiled version
2. Software channels 20/21/22 require firmware above V1016, 20230330 later compiled versions
3. The above mapping is fixed, mcu.iomux has no configuration items, and all available PWM channels have been enumerated..

## UART Description

physical uart has 3(0/1/2)

1. uart0 It is a log port (DBG_TX/DBG_RX), which is not recommend to use and has output during startup. LuatOS firmware does not allow users to use it by default uart0
2. uart1 is the primary serial port (MAIN_TX/MAIN_RX), recommend use
3. uart2 This is a serial port (AUX_TX/AUX_RX), **the module with GNSS function will be connected to the GNSS chip**, and the PAD is different and cannot be used for other functions
4. Note that UART2 is different from Air780EG PAD in Air780E, but the software will automatically adapt and does not need attention.
5. The following mappings are default, configurable via mcu.iomux
6. [Cloud Compilation](https://wiki.luatos.org/develop/compile/Cloud_compilation.html) supports releasing uart0, although this is not recommend..

|Function | Software Meaning | Corresponding GPIO | Corresponding PAD | Remarks|
|--------|----------|---------|---------|----|
|DBG_RX  | UART0_RX | -       |    29   ||
|DBG_TX  | UART0_TX | -       |    30   ||
|MAIN_RX | UART1_RX | GPIO18  |    33   ||
|MAIN_TX | UART1_TX | GPIO19  |    34   ||
|AUX_RX  | UART2_RX | GPIO10  |    25   |Air780EG In PAD 27|
|AUX_TX  | UART2_TX | GPIO11  |    26   |Air780EG In PAD 28|

## I2C Description

1. Physics i2c has 2(0/1)
2. The following mappings are default, configurable via mcu.iomux
3. Air780EX Only I2C0, and need to call' mcu.iomux(mcu.I2C, 0,2) 'to switch to PAD 31/32 GPIO 16/17

|Function | Software Meaning | Corresponding GPIO | Corresponding PAD | Remarks|
|---------|---------|---------|---------|----|
|I2C0_SCL | I2C0 Clock | GPIO14 | 13 | GPIO function see description later|
|I2C0_SDA | I2C0 Data | GPIO15 | 14 | GPIO functions see description later|
|I2C1_SCL | I2C1 Clock | GPIO9 | 24 | conflicts with SPI0|
|I2C1_SDA | I2C1 Data | GPIO8 | 23 | Conflicts with SPI0|

## SPI Description

1. Physical SPI has 2(0/1)
2. The following mappings are default, configurable via mcu.iomux

|Function | Software Meaning | Corresponding GPIO | Corresponding PAD | Remarks|
|---------|------------|---------|---------|----|
|SPI0_CS  | SPI0 Chip Select | GPIO8 | 23 | Conflicts with I2C1|
|SPI0_MOSI| SPI0 Host output | GPIO9 | 24 | conflicts with I2C1|
|SPI0_MISO| SPI0 Output from machine| GPIO10  |    25   ||
|SPI0_SCL | SPI0 Clock    | GPIO11  |    26   ||
|SPI1_CS  | SPI1 Film selection    | GPIO12  |    27   ||
|SPI1_MOSI| SPI1 Host Output| GPIO13  |    28   ||
|SPI1_MISO| SPI1 slave output | - | 29 | note no GPIO function|
|SPI1_SCL | SPI1 Clock | - | 30 | Note no GPIO capability|

Attention:

1. SPI0 It is in conflict with UART2/I2C1, which is the case.
2. SPI1 Although MISO and SCL can be reused as GPIO14/15, these GPIO are actually mapped to other pins. see `GPIO additional instructions`

## GPIO Additional Instructions

1. GPIO14/15 There are changes in V1103, correctly mapped `PAD 13/14`
2. Ordinary GPIO in deep sleep/SLEEP2, there will be periodic high-level pulses, be sure to pay attention
3. AONGPIO It is a GPIO that can still maintain a high level during sleep, but the driving ability is very weak.
4. GPIO12/GPIO13 There are two types of mapping, using different APIs.
5. When ordinary GPIO is configured in input/interrupt mode, the up-down pull cannot be set. If the default up-down pull cannot meet the requirements, it can be set to'0' to cancel the default up-down pull, and then add the pull-down externally
6. GPIO20,21,22 When configured to interrupt mode, it is a wakeup function, which can be configured to pull up and down or cancel the use of external pull up and down.
7. **GPIO23** After power-on, the input pull-down is first, and then it will be set to **output pull-up high level**. It is recommended to avoid using this GPIO
8. **Note **, only GPIO 20-22 supports 'two-way triggering (rising and falling) ', other GPIOs only support one-way triggering of 'rising edges' or 'falling edges'
9. GPIO 20-25 The level flip speed of is slower than other GPIO

10. When using GPIO with multiplexing function, the default GPIO pin needs to be multiplexed into other functions before multiplexing can be used normally.GPIO

    ```lua
    -- Using the 97-pin GPIO12, the 58-pin GPIO12 needs to be multiplexed into I2C or UART function first.
    -- Multiplexing pins 57 and 58 I2C0
    mcu.iomux(mcu.I2C, 0, 1)
    -- Enable I2C
    i2c.setup(0, i2c.SLOW)
    
    local function gpio12CbFnc()
    	log.info("gpio", "12")    
    end
    
    -- Enable multiplexing for ALT4, pin 97 GPIO12
    gpio.setup(12, gpio12CbFnc, gpio.PULLUP, gpio.FALLING, 4)
    ```

|Corresponding GPIO | Corresponding PAD | Examples of API used | Remarks|
|---------|---------|---------|----|
| GPIO12   |    11   |pm.power(pm.DAC_EN, true Or false)| LDO_CTL, the Air600E target is GPIO12|
| GPIO13   |    12   |pm.power(pm.GPS, true Or false)| no lead, in the Air780EG to control the power of GPS|
| GPIO12   |    27   |gpio.setup(12, 0)|I2C0_SDA,is also multiplexed|
| GPIO13   |    28   |gpio.setup(13, 0)|I2C0_SCL,is also multiplexed|

## Virtual GPIO

Air780E(EC618 The whole system) supports multiple virtual GPIO, and non-GPIO pins are used by software simulation into GPIO.

|Number | Name | Function | Remarks|
|----|----|----|---|
|32| wakeup0|Only supports input and interrupt | wakeup0 sleep wake-up pin|
|33| vbus/wakeup1|Only supports input and interrupt | VBUS of USB, detects whether USB is inserted|
|34| wakeup2|Only supports input and interrupt | wakeup2 sleep wake-up pin, USIM_DET|
|35| pwrkey |Only supports input and interrupt | instant power-on key, when normal GPIO is used after power-on|

vbus Description:

1. In CSDK/LuatOS firmware, vbus and USB functions are decoupled
2. Different from regular understanding, USB function is still available without vbus
3. Before entering sleep, set the above `wakeup0/wakeup1/wakeup2` to interrupt state to realize pin wake-up function
4. Non-wakeup ordinary GPIO does not support sleep wake-up

For example, if `wakup0` is set as the wake-up pin, the interrupt callback can be an empty function.

```lua
gpio.setup(32, function() end, gpio.PULLUP)
```

## Additional Notes on USB

1. **BOOT Mode requires high USB wiring * *, must do differential line and impedance matching!!!
2. When there is USB communication, it is impossible to sleep. USB communication can be turned off by pass' pm.power(pm.USB, false)'
3. UART1 Can also brush the machine, but need to use the amount of production tool brush, LuaTools temporarily does not support the Air780EP/Air780EPV brush through UART!!!
