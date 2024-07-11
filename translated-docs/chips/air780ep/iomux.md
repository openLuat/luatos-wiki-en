# LuatOS IO multiplexing configuration under firmware

**This document describes the LuatOS and CSDK perspectives**

1. Part of the mapping can be modified by [mcu.altfun function](https://wiki.luatos.org/api/mcu.html).
2. The external pin layout of different modules is different, but the' PAD(paddr)'value is the same. to correspond to the "PIN/GPIO correspondence table" document, the document can be found in [air780ep.cn](https://air780ep.cn)
3. For AT firmware, this document is meaningless, please ignore it.
4. Cloud compilation and mcu.altfun function can adjust part of the reuse relationship, please refer to the document linked to [mcu library](https://wiki.luatos.org/api/mcu.html)
5. If SIM2 is used, it will take up 4 IO(gpio4/5/6/23)
6. `mcu.iomux` The function needs to be used with GPIO multiplexing table. Find the PDF document of GPIO in the table of hardware data and homepage of [air780ep.cn](https://air780ep.cn)

## PWM Description

|software channel id | actual hardware channel | corresponding GPIO | corresponding PAD | remarks|
|----------|------------|---------|---------|----|
|0         |    0       | GPIO1   |    16   | AU_OUT,It is recommended to avoid using|
|1         |    1       | GPIO24  |    49   | MAIN_RI,weak driving ability |
|2         |    2       | GPIO25  |    50   | AGPIO5,weak driving ability|
|4         |    4       | GPIO27  |    52   | NetLed,Network Status Light |

Description:

1. The actual available channels are 4 (0/1/2/4), and the 4 channels can be used separately without affecting each other. **PWM3/PWM5 has been used by the bottom layer**.
2. However, the same hardware channel, for example, PWM1 and PWM11 both use hardware channel 1, and only one of them can be selected at the same time.**.
3. Enabling PWM1 will not enable PWM11, enabling PWM1 and enabling PWM22 will not be restricted.
4. When calling the API of pwm library, fill in' software channel id', not GPIO number, not hardware channel number
5. The current firmware version V1001 does not support the use of other PWM multiplexing channels, and subsequent versions will support

## UART Description

physics uart has 4(0/1/2/3)

1. uart0 It is a log port (DBG_TX/DBG_RX), which is not recommended. It also has output during startup. LuatOS firmware does not allow users to use it by default uart0
2. uart1 is the primary serial port (MAIN_TX/MAIN_RX), recommended
3. uart2 It is a serial port(AUX_TX/AUX_RX)
4. uart3 Is a standby serial port, no default function
5. The following mappings are default and supported through 'mcu.iomux' configuration
6. Release is not currently supported uart0

|Function | Software Meaning | Corresponding GPIO | Corresponding PAD | Remarks|
|--------|----------|---------|---------|----|
|DBG_RX  | UART0_RX | -       |    31   ||
|DBG_TX  | UART0_TX | -       |    32   ||
|MAIN_RX | UART1_RX | GPIO18  |    33   ||
|MAIN_TX | UART1_TX | GPIO19  |    34   ||
|AUX_RX  | UART2_RX | GPIO12  |    27   ||
|AUX_TX  | UART2_TX | GPIO13  |    28   ||
|UART3_RX| UART3_RX | GPIO34  |    40   ||
|UART3_TX| UART3_TX | GPIO35  |    41   ||

## I2C Description

1. Physics i2c has 2(0/1)
2. The following mappings are default, configurable via 'mcu.altfun'

|Function | Software Meaning | Corresponding GPIO | Corresponding PAD | Remarks|
|---------|---------|---------|---------|----|
|I2C0_SCL | I2C0 Clock | GPIO18 | 13 | GPIO function see later description|
|I2C0_SDA | I2C0 Data | GPIO19 | 14 | GPIO function see later description|
|I2C1_SCL | I2C1 Clock | GPIO9 | 24 | conflicts with SPI0|
|I2C1_SDA | I2C1 Data | GPIO8 | 23 | Conflicts with SPI0|

Attention:

1. Although the GPIO number of I2C0 is the same as that of UART1/MAIN_UART, the actual GPIO18/19 is multiplexed on UART1/MAIN_UART pin and does not conflict with I2C0
2. I2C It is a bus structure and can connect multiple devices with different i2c addresses.

## SPI Description

1. There are 3 physical SPIs, of which '0/1' is a general SPI and '5' is a special SPI for LCD screen brushing.
2. The following mappings are default values and other configuration items are not supported.

|Function | Software Meaning | Corresponding GPIO | Corresponding PAD | Remarks|
|---------|------------|---------|---------|----|
|SPI0_CS  | SPI0 Chip Select | GPIO8 | 23 | Conflicts with I2C1|
|SPI0_MOSI| SPI0 Host output | GPIO9 | 24 | conflicts with I2C1|
|SPI0_MISO| SPI0 Output from machine| GPIO10  |    25   ||
|SPI0_SCL | SPI0 Clock    | GPIO11  |    26   ||
|SPI1_CS  | SPI1 Select | GPIO12 | 27 | Note conflict with UART2|
|SPI1_MOSI| SPI1 Host output | GPIO13 | 28 | Note conflict with UART2|
|SPI1_MISO| SPI1 Output from machine| GPIO14  |    29   ||
|SPI1_SCL | SPI1 Clock    | GPIO15  |    30   ||
|SPI5_CLK | LCD Clock | GPIO34 | 40 | LCD _CLK, note conflict with UART3|
|SPI5_CS  | LCD Select | GPIO35 | 41 | LCD _CS, note conflict with UART3|
|SPI5_RST | LCD Reset     | GPIO36  |    42   | LCD_RST|
|SPI5_SCL | Data Output    | GPIO37  |    43   | LCD_DOUT |
|SPI5_RS  | LCD_RS     | GPIO38  |    44   | LCD_RS|

Attention:

1. SPI0 It is in conflict with UART2, which is the case.
2. SPI5 It is a special SPI for LCD screen brushing, and does not support the general SPI function

## GPIO Additional Instructions

1. AONGPIO It is a GPIO that can still maintain a high level during sleep, but the driving ability is very weak.
2. When ordinary GPIO is configured in input/interrupt mode, the up-down pull cannot be set. If the default up-down pull cannot meet the requirements, it can be set to'0' to cancel the default up-down pull, and then add the pull-down externally

## Virtual GPIO

Air780EP(EC718P/EC718PV The whole system) supports multiple virtual GPIO, and non-GPIO pins are used by software simulation into GPIO.

|Number | Name | Function | Remarks|
|----|----|----|---|
|39| wakeup0|Only supports input and interrupt | wakeup0 sleep wake-up pin|
|40| wakeup1/vbus|Only supports input and interrupt | VBUS of USB, detects whether USB is inserted|
|41| wakeup2|Only supports input and interrupt | wakeup2 sleep wake-up pin, USIM_DET|
|42| wakeup3|Only supports input and interrupt | wakeup3 sleep wake-up pin, AGPIOWU0|
|43| wakeup4|Only supports input and interrupt | wakeup4 sleep wake-up pin, AGPIOWU1|
|44| wakeup5|Only supports input and interrupt | wakeup5 sleep wake-up pin, MAIN_DTR|
|46| pwrkey |Only supports input and interrupt | instant power-on key, when normal GPIO is used after power-on|

Description:

1. vbus Decoupled from USB functionality
2. Different from conventional understanding, USB function is still available without vbus
3. Before entering sleep, set the above 'wakeup0/wakeup1/wakeup2/wakeup3/wakeup4/wakeup5' to the interrupt state to realize the pin wake-up function
4. Non-wakeup ordinary GPIO does not support sleep wake-up

For example, if 'wakup0' is set as the wake-up pin, the interrupt callback can be an empty function.

```lua
gpio.setup(39, function() end, gpio.PULLUP)
```

## iosel Feet and usbboot feet

* BOOT Foot, namely GPIO0, can be used as a normal GPIO after boot
* iosel The foot (io_sel foot) is different from the Air780E(EC618 scheme), and the iosel foot of the Air780EP(EC718 scheme) is actually a common GPIO foot
* iosel The pin can be used as an ordinary GPIO after startup, and the multiplexing relationship is GPIO12 ALT4

```lua
gpio.setup(12, 1, gpio.PULLUP, nil, 4)
```

## Additional Notes on USB

1. **BOOT Mode requires high USB wiring * *, must do differential line and impedance matching!!!
2. When there is USB communication, it is impossible to sleep. USB communication can be turned off by pass' pm.power(pm.USB, false)'
3. UART1 Can also brush the machine, but need to use the amount of production tool brush, LuaTools temporarily does not support the Air780EP/Air780EPV brush through UART!!!
