
# Equipment Support/Supported Devices

## bsp Support

[Adaptation Status-LuatOS Documentation](https://wiki.luatos.org/api/supported.html)

## Basic Peripheral Support

| Libs                                                  | Air101 | Air103 | Air105 | Air780E | ESP32C2 | ESP32C3 | Air780E/Air700E | Air780EP/Air780EPV |
| ----------------------------------------------------- | ------ | ------ | ------ | ------- | ------- | ------- | --------------- | ------------------ |
| [gpio](https://wiki.luatos.org/api/gpio.html)         | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |
| [i2c](https://wiki.luatos.org/api/i2c.html)           | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |
| [spi](https://wiki.luatos.org/api/spi.html)           | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |
| [adc](https://wiki.luatos.org/api/adc.html)           | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |
| [dac](https://wiki.luatos.org/api/dac.html)           | ✖      | ✖      | ✔      | ✖       | ✖       | ✖       | ✖               | ✖                  |
| [usb](https://wiki.luatos.org/api/usb.html)           | ✖      | ✖      | ✔      | ✔       | ✖       | ✖       | ✖               | ✖                  |
| [otp](https://wiki.luatos.org/api/otp.html)           | ✔      | ✔      | ✔      | ✔       | ✖       | ✖       | ✔               | ✔                  |
| [pin](https://wiki.luatos.org/api/pin.html)           | ✔      | ✔      | ✔      | ✖       | ✖       | ✖       | ✖               | ✖                  |
| [rtc](https://wiki.luatos.org/api/rtc.html)           | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |
| [pwm](https://wiki.luatos.org/api/pwm.html)           | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |
| [sdio](https://wiki.luatos.org/api/sdio.html)         | ✔      | ✔      | ✖      | ✖       | ✖       | ✖       | ✖               | ✖                  |
| [wdt](https://wiki.luatos.org/api/wdt.html)           | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |
| [crypto](https://wiki.luatos.org/api/crypto.html)     | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |
| [keyboard](https://wiki.luatos.org/api/keyboard.html) | ✖      | ✖      | ✔      | ✖       | ✖       | ✖       | ✖               | ✖                  |
| [lcdseg](https://wiki.luatos.org/api/lcdseg.html)     | ✖      | ✔      | ✖      | ✖       | ✖       | ✖       | ✖               | ✖                  |

Supplementary Notes:
* air101 The difference with air103 firmware is flash size, compatible with xt804 core chips, such as Lianshengde's W800/W801/W805/W806
* lcdseg Support or not, related to the package, qfn56 package xt804 support, qfn32 does not support

## Library support

| Libs                                                  | Air101 | Air103 | Air105 | ESP32C2 | ESP32C3 | Air780E/Air700E | Air780EP/Air780EPV |
| ----------------------------------------------------- | ------ | ------ | ------ | ------- | ------- | --------------- | ------------------ |
| [json](https://wiki.luatos.org/api/json.html)         | ✔      | ✔      | ✔      | ✔       | ✔       | ✔               | ✔                  |
| [zbuff](https://wiki.luatos.org/api/zbuff.html)       | ✔      | ✔      | ✔      | ✔       | ✔       | ✔               | ✔                  |
| [pack](https://wiki.luatos.org/api/pack.html)         | ✔      | ✔      | ✔      | ✔       | ✔       | ✔               | ✔                  |
| [libgnss](https://wiki.luatos.org/api/libgnss.html)   | ✔      | ✔      | ✔      | ✔       | ✔       | ✔               | ✔                  |
| [lcd](https://wiki.luatos.org/api/lcd.html)           | ✔      | ✔      | ✔      | ✔       | ✔       | ✔               | ✔                  |
| [eink](https://wiki.luatos.org/api/eink.html)         | ✔      | ✔      | ✔      | ✔       | ✔       | ✔               | ✔                  |
| [u8g2](https://wiki.luatos.org/api/u8g2.html)         | ✔      | ✔      | ✔      | ✔       | ✔       | ✔               | ✔                  |
| [gtfont](https://wiki.luatos.org/api/gtfont.html)     | ✔      | ✔      | ✔      | ✖       | ✖       | ✔               | ✔                  |
| [coremark](https://wiki.luatos.org/api/coremark.html) | ✔      | ✔      | ✔      | ✔       | ✔       | ✔               | ✔                  |
| [fdb](https://wiki.luatos.org/api/fdb.html)           | ✔      | ✔      | ✔      | ✔       | ✔       | ✔               | ✔                  |
| [lvgl](https://wiki.luatos.org/api/lvgl.html)         | ✔      | ✔      | ✔      | ✔       | ✔       | ✔               | ✔                  |
| [sfud](https://wiki.luatos.org/api/sfud.html)         | ✔      | ✔      | ✔      | ✔       | ✔       | ✔               | ✔                  |
| [statem](https://wiki.luatos.org/api/statem.html)     | ✔      | ✔      | ✔      | ✔       | ✔       | ✔               | ✔                  |

## Networking/RF support

| Libs                                                    | Air101 | Air103 | Air105 | Air780E | ESP32C2 | ESP32C3 | Air780E/Air700E | Air780EP/Air780EPV |
| ------------------------------------------------------- | ------ | ------ | ------ | ------- | ------- | ------- | --------------- | ------------------ |
| [socket](https://wiki.luatos.org/api/socket.html)       | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |
| [wlan](https://wiki.luatos.org/api/wlan.html)           | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |
| [nimble](https://wiki.luatos.org/api/nimble.html)       | ✔      | ✔      | ✖      | ✖       | ✖       | ✔       | ✖               | ✖                  |
| [http](https://wiki.luatos.org/api/http.html)           | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |
| [mqtt](https://wiki.luatos.org/api/mqtt.html)           | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |
| [websocket](https://wiki.luatos.org/api/websocket.html) | ✔      | ✔      | ✔      | ✔       | ✔       | ✔       | ✔               | ✔                  |

* Air105 W5500 with networking function
* Air780E Only supports wifi scan, no wifi networking function
* Air101/Air103 Cloud compilation is required to turn on wifi function to connect to the Internet, and the development board does not lead out the antenna.

## Meaning of Illustration Table Legend

|  Illustration | Meaning  |
|-------|-------|
|✔ |Supported Supported|
|⚠ |In progress/partial support WIP/partial support|
|✖ |Not supported Not supported|
