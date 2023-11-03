# 📊 Chip comparison and selection table

## Chip contrast

| Peripherals                                                  | Air101 | Air103 | Air105 | Air302 | Air640W | Air106 |ESP32C3|
|------------------------------------------------------ |--------|--------|--------|--------|---------|--------|-------|
| Encapsulation                                                  | qfn32   | qfn56  | qfn88 | LCC    | qfn32   | LQFP100 | qfn32|
| Total Flash | 2M | 1M | 4M | 4M | 1M/2M | 512k External Flash | External Flash|
| Total Ram                                                 | 288k    | 288k   | 640k  | 256k   | 288k    | 64k+8M|400k|
| Kernel                                                  | xt804   | xt804  | m4f   | m3     | m3      | m33    |risc-v|
| [uart](https://openluat.github.io/luatos-wiki-en/api/uart.html)         | 5(4)    | 6(5)   | 4(3)  | 3(1)   | 3(2)    | 4(3)      |2|
| [gpio](https://openluat.github.io/luatos-wiki-en/api/gpio.html)         | 18      | 44     | 56    | 20     | 16      | 74      |15|
| [i2c](https://openluat.github.io/luatos-wiki-en/api/i2c.html)           | 1       | 1      | 1     | 1      | 1       | 2      |1|
| [spi](https://openluat.github.io/luatos-wiki-en/api/spi.html)           | 1       | 1      | 4     | 1      | 1       | 2      |1|
| [adc](https://openluat.github.io/luatos-wiki-en/api/adc.html)           | 2       | 4      | 5     | 2      | 2       | 19      |6|
| [dac](https://openluat.github.io/luatos-wiki-en/api/dac.html)           | ✖      | ✖      | 1     | ✖      | ✖      | 1      |✖|
| [usb](https://openluat.github.io/luatos-wiki-en/api/usb.html)           | ✖      | ✖      | 1     | ✖      | ✖      | 1      |1|
| [keyboard](https://openluat.github.io/luatos-wiki-en/api/keyboard.html) | ✖      | ✖      | 1     | ✖      | ✖      | ✖      |✖|
| [lcdseg](https://openluat.github.io/luatos-wiki-en/api/lcdseg.html)     | ✖      | 4*31    | ✖     | ✖     | ✖      | ✖      |✖|
| [otp](https://openluat.github.io/luatos-wiki-en/api/otp.html)           | 1       | 1      | 1      | ✖     | ✖       | ✖      |✖|
| [rtc](https://openluat.github.io/luatos-wiki-en/api/rtc.html)           | 1       | 1      | 1      | 1     | 1        | 1      |1|
| [pwm](https://openluat.github.io/luatos-wiki-en/api/pwm.html)           | 5       | 5      | 5      | 4      | 5       | 20(18)      |4|
| [sdio](https://openluat.github.io/luatos-wiki-en/api/sdio.html)         | 1       | 1      | ✖      | ✖     | ✖      | 1      |✖|
| [硬狗](https://openluat.github.io/luatos-wiki-en/api/wdt.html)          | 1       | 1     | 1      | ✖     | 1      | 1      |1|
| [硬件加速](https://openluat.github.io/luatos-wiki-en/api/crypto.html)   |md5/sha1 |md5/sha1| md5/sha| ✖    |md5/sha1| jpeg   |md5/sha1|
| Hardware Timer                                            | 5       | 5      | 8(6)      | 2      | 5      | 15(13)     |4|
| 2d加速                                                | ✖      | ✖      | ✖     |  ✖     | ✖      | 1      |✖|
| 摄像头                                                | ✖      | ✖      | 1      |  ✖     | ✖      | ✖      |✖|
| psram                                                 | ✖      | Can be external |✖|✖|✖| Inline    |✖|
| wifi                                                  | ✖       | ✖       | ✖     |  ✖     | 1       | ✖      |1|
| ble                                                   | 1       | 1        | ✖     |  ✖     | ✖       | ✖      |1|
| nbiot                                                 | ✖       | ✖        | ✖     |  1     | ✖       | ✖      |✖|
|recommend resolution                                             | `128*160` | `128*120`|`320*240`|`128*160`|`128*160`|`1024*768`|`320*240`|

* uart Quantity The total number of uarts. The number of uarts commonly available to the user is shown in parentheses. In general, one uart is required for logging and brushing..
* gpio The quantity can be reused as the quantity of GPIO, including bootmode and uart0, which are available after startup but do not recommend multiplexing.GPIO.
* In case of discrepancy, the hardware design manual shall prevail.
* Lianshengde w800/w801/w805/806 is similar to air101/air103 and belongs to the arrangement and combination of package and flash size.
* air105 There are 1 high-speed SPI, the highest 96M, 3 low-speed SPI, the highest 24M
* air101/air103 BLE is a gift, high power consumption, not suitable for low power consumption scenarios

## Meaning of Illustration Table Legend

|  Illustration | Meaning  |
|-------|-------|
|✔ |Supported Supported|
|⚠ |In progress/partial support WIP/partial support|
|✖ |Not supported Not supported|
|? |Not clear/confidential status |
