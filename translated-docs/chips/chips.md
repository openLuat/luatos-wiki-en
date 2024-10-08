# 📊 Chip comparison and selection table

## Chip contrast

| Peripherals                                                  | Air101    | Air103    | Air105    | Air302    | Air640W   | Air106         | ESP32C3   | Air780E/Air700E        | Air780EP/Air780EPV     |
| ----------------------------------------------------- | --------- | --------- | --------- | --------- | --------- | -------------- | --------- | ---------------------- | ---------------------- |
| Encapsulation                                                  | qfn32     | qfn56     | qfn88     | LCC       | qfn32     | LQFP100        | qfn32     | LGA 109PIN             | LGA 109PIN             |
| Total Flash | 2M | 1M | 4M | 4M | 1M/2M | 512k External Flash | External Flash | 4M                     | 4M                     |
| Total Ram                                                 | 288k      | 288k      | 640k      | 256k      | 288k      | 64k+8M         | 400k      | 1.25M                  | 2M+2M                  |
| Kernel                                                  | xt804     | xt804     | m4f       | m3        | m3        | m33            | risc-v    | M3                     | M3                     |
| [uart](https://wiki.luatos.org/api/uart.html)         | 5(4)      | 6(5)      | 4(3)      | 3(1)      | 3(2)      | 4(3)           | 2         | 3                      | 4                      |
| [gpio](https://wiki.luatos.org/api/gpio.html)         | 18        | 44        | 56        | 20        | 16        | 74             | 15        | 32                     | 39                     |
| [i2c](https://wiki.luatos.org/api/i2c.html)           | 1         | 1         | 1         | 1         | 1         | 2              | 1         | 2                      | 2                      |
| [spi](https://wiki.luatos.org/api/spi.html)           | 1         | 1         | 4         | 1         | 1         | 2              | 1         | 2                      | 2                      |
| [adc](https://wiki.luatos.org/api/adc.html)           | 2         | 4         | 5         | 2         | 2         | 19             | 6         | 4                      | 4                      |
| [dac](https://wiki.luatos.org/api/dac.html)           | ✖         | ✖         | 1         | ✖         | ✖         | 1              | ✖         | ✖                      | ✖                      |
| [usb](https://wiki.luatos.org/api/usb.html)           | ✖         | ✖         | 1         | ✖         | ✖         | 1              | 1         | 1                      | 1                      |
| [keyboard](https://wiki.luatos.org/api/keyboard.html) | ✖         | ✖         | 1         | ✖         | ✖         | ✖              | ✖         | 5*5                    | 5*5                    |
| [lcdseg](https://wiki.luatos.org/api/lcdseg.html)     | ✖         | 4*31      | ✖         | ✖         | ✖         | ✖              | ✖         | ✖                      | ✖                      |
| [otp](https://wiki.luatos.org/api/otp.html)           | 1         | 1         | 1         | ✖         | ✖         | ✖              | ✖         | 1                      | 1                      |
| [rtc](https://wiki.luatos.org/api/rtc.html)           | 1         | 1         | 1         | 1         | 1         | 1              | 1         | 1                      | 1                      |
| [pwm](https://wiki.luatos.org/api/pwm.html)           | 5         | 5         | 5         | 4         | 5         | 20(18)         | 4         | 4                      | 4                      |
| [sdio](https://wiki.luatos.org/api/sdio.html)         | 1         | 1         | ✖         | ✖         | ✖         | 1              | ✖         | ✖                      | ✖                      |
| [硬狗](https://wiki.luatos.org/api/wdt.html)          | 1         | 1         | 1         | ✖         | 1         | 1              | 1         | 1                      | 1                      |
| [硬件加速](https://wiki.luatos.org/api/crypto.html)   | md5/sha1  | md5/sha1  | md5/sha   | ✖         | md5/sha1  | jpeg           | md5/sha1  | md5/des/crc7/sha Series, etc. | md5/des/crc7/sha series, etc |
| Hardware Timer                                            | 5         | 5         | 8(6)      | 2         | 5         | 15(13)         | 4         | ?                      | ?                      |
| 2d加速                                                | ✖         | ✖         | ✖         | ✖         | ✖         | 1              | ✖         | ✖                      | ✖                      |
| 摄像头                                                | ✖         | ✖         | 1         | ✖         | ✖         | ✖              | ✖         | ✖                      | 1                      |
| psram                                                 | ✖         | Can be external |✖|✖|✖| Inline |✖|✖| Inline                   |
| wifi                                                  | ✖         | ✖         | ✖         | ✖         | 1         | ✖              | 1         | ✖                      | ✖                      |
| ble                                                   | 1         | 1         | ✖         | ✖         | ✖         | ✖              | 1         | ✖                      | ✖                      |
| nbiot                                                 | ✖         | ✖         | ✖         | 1         | ✖         | ✖              | ✖         | ✖                      | ✖                      |
| recommend resolution                                            | `128*160` | `128*120` | `320*240` | `128*160` | `128*160` | `1024*768`     | `320*240` | `320*240`              | `480*320`              |

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
