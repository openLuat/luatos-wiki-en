
# Equipment Support/Supported Devices

## Basic Peripheral Support

| Libs                                                  | Air101 | Air103 | Air105 | Air780E | ESP32C2 | ESP32C3 |
|------------------------------------------------------ |--------|--------|--------|--------|---------|--------|
| [gpio](https://openluat.github.io/luatos-wiki-en/api/gpio.html)         | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [i2c](https://openluat.github.io/luatos-wiki-en/api/i2c.html)           | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [spi](https://openluat.github.io/luatos-wiki-en/api/spi.html)           | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [adc](https://openluat.github.io/luatos-wiki-en/api/adc.html)           | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [dac](https://openluat.github.io/luatos-wiki-en/api/dac.html)           | ✖      | ✖     | ✔      | ✖     | ✖      | ✖      |
| [usb](https://openluat.github.io/luatos-wiki-en/api/usb.html)           | ✖      | ✖     | ✔      | ✔     | ✖      | ✖      |
| [otp](https://openluat.github.io/luatos-wiki-en/api/otp.html)           | ✔      | ✔     | ✔      | ✔     | ✖      | ✖      |
| [pin](https://openluat.github.io/luatos-wiki-en/api/pin.html)           | ✔      | ✔     | ✔      | ✖     | ✖      | ✖      |
| [rtc](https://openluat.github.io/luatos-wiki-en/api/rtc.html)           | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [pwm](https://openluat.github.io/luatos-wiki-en/api/pwm.html)           | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [sdio](https://openluat.github.io/luatos-wiki-en/api/sdio.html)         | ✔      | ✔     | ✖      | ✖     | ✖      | ✖      |
| [wdt](https://openluat.github.io/luatos-wiki-en/api/wdt.html)           | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [crypto](https://openluat.github.io/luatos-wiki-en/api/crypto.html)     | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [keyboard](https://openluat.github.io/luatos-wiki-en/api/keyboard.html) | ✖      | ✖     | ✔      | ✖     | ✖      | ✖      |
| [lcdseg](https://openluat.github.io/luatos-wiki-en/api/lcdseg.html)     | ✖      | ✔     | ✖      | ✖     | ✖      | ✖      |

Supplementary Notes:
* air101 The difference with air103 firmware is flash size, compatible with xt804 core chips, such as Lianshengde's W800/W801/W805/W806
* lcdseg Support or not, related to the package, qfn56 package xt804 support, qfn32 does not support

## Library support

| Libs                                                  | Air101 | Air103 | Air105 | Air780E | ESP32C2 | ESP32C3 |
|-------------------------------------------------------|--------|--------|--------|--------|---------|--------|
| [json](https://openluat.github.io/luatos-wiki-en/api/json.html)         | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [zbuff](https://openluat.github.io/luatos-wiki-en/api/zbuff.html)       | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [pack](https://openluat.github.io/luatos-wiki-en/api/pack.html)         | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [libgnss](https://openluat.github.io/luatos-wiki-en/api/libgnss.html)   | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [libcoap](https://openluat.github.io/luatos-wiki-en/api/libcoap.html)   | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [lcd](https://openluat.github.io/luatos-wiki-en/api/lcd.html)           | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [eink](https://openluat.github.io/luatos-wiki-en/api/eink.html)         | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [u8g2](https://openluat.github.io/luatos-wiki-en/api/u8g2.html)         | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [gtfont](https://openluat.github.io/luatos-wiki-en/api/gtfont.html)     | ✔      | ✔     | ✔      | ✔    | ✖      | ✖      |
| [coremark](https://openluat.github.io/luatos-wiki-en/api/coremark.html) | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [fdb](https://openluat.github.io/luatos-wiki-en/api/fdb.html)           | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [lvgl](https://openluat.github.io/luatos-wiki-en/api/lvgl.html)         | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [sfud](https://openluat.github.io/luatos-wiki-en/api/sfud.html)         | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |
| [statem](https://openluat.github.io/luatos-wiki-en/api/statem.html)     | ✔      | ✔     | ✔      | ✔     | ✔      | ✔      |

## Networking/RF support

| Libs                                                  | Air101 | Air103 | Air105 | Air780E | ESP32C2 | ESP32C3 |
|-------------------------------------------------------|--------|--------|--------|--------|---------|--------|
| [socket](https://openluat.github.io/luatos-wiki-en/api/socket.html)     | ✖      | ✖     | ✔      | ✔     | ⚠      | ⚠      |
| [wlan](https://openluat.github.io/luatos-wiki-en/api/wlan.html)         | ✖      | ✖     | ✔      | ✔     | ✔      | ✔      |
| [nimble](https://openluat.github.io/luatos-wiki-en/api/nimble.html)     | ✔      | ✔     | ✖      | ✖     | ✖      | ✔      |
| [http](https://openluat.github.io/luatos-wiki-en/api/http.html)         | ✖      | ✖     | ✔      | ✔     | ✔      | ✔      |
| [mqtt](https://openluat.github.io/luatos-wiki-en/api/mqtt.html)         | ✖      | ✖     | ✔      | ✔     | ✔      | ✔      |
| [websocket](https://openluat.github.io/luatos-wiki-en/api/websocket.html) | ✖    | ✔     | ✔      | ✔     | ⚠      | ⚠      |

* Air105 W5500 with networking function
* Air780E Only supports wifi scan, no wifi networking function

## Meaning of Illustration Table Legend

|  Illustration | Meaning  |
|-------|-------|
|✔ |Supported Supported|
|⚠ |In progress/partial support WIP/partial support|
|✖ |Not supported Not supported|
