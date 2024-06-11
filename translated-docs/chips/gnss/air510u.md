# Air510U

## Hardware Data

* Information website: [air510u.cn](https://air510u.cn)

The built-in GNSS chip is the core and object UC6228CI

## Software Information

Data Link: [UC6228CI](https://www.unicorecomm.com/products/detail/41)

### UART Parameters

* The default baud rate is 115200
* Data bit 8, stop bit 1, no check
* UART The level is 3.3V
* The highest baud rate supported is 115200, the lowest 9600

### Supported navigation constellations

* Default firmware: GPS, Beidou II, QZSS
* Loading firmware: GPS, Beidou II, Beidou III, QZSS, GLONASS, GALILEO

Switch Navigation Constellation:

1. Constellation number: GPS = 1, Beidou=10
2. Constellations can be combined, such as GPS Beidou = 11, single Beidou is 10

Examples:

```txt
// Switch to GPS Beidou
$CFGSYS,H11\r\n
// Switch to Single GPS
$CFGSYS,H01\r\n
// Switch to single Beidou
$CFGSYS,H10\r\n
```

Reminder:

* Switching the navigation constellation will soft restart the module, the NMEA configuration will restore the default value, and the non-mode NMEA statement needs to be opened again.

### Ephemeris information

Address:

* [GPS+The Big Dipper Calendar http://download.openluat.com/9501-xingli/HXXT_GPS_BDS_AGNSS_DATA.dat](http://download.openluat.com/9501-xingli/HXXT_GPS_BDS_AGNSS_DATA.dat)
* [Single Big Dipper Ephemeris http://download.openluat.com/9501-xingli/HXXT_BDS_AGNSS_DATA.dat](http://download.openluat.com/9501-xingli/HXXT_BDS_AGNSS_DATA.dat)

Update cycle is 10 minutes once, ephemeris validity: GPS is 4 hours, Beidou is 1 hour

The single Big Dipper ephemeris will be smaller than the GPS Big Dipper ephemeris, which is normal. The Big Dipper ephemeris is a subset of GPS Beidou

Write Method:

1. Ephemeris itself is a binary file (RTCM format), which can be written directly to the UART RXD port of the module. It can be written in segments (not less than 256 bytes) or once.
2. The ephemeris itself comes with verification, but there is no return value for writing.

Reminder:

1. The ephemeris is also continuously received from the satellite while the device is in operation, thereby automatically extending the ephemeris validity period
2. Keeping the standby power supply without dropping can keep the ephemeris. After the standby power supply is also powered off, the ephemeris will be lost and needs to be injected again.

## Adaptation

1. lua Library: [uc6228 for LuatOS](https://github.com/wendal/luatos-lib-uc6228)
2. AT Firmware comes with driver
3. CSDK Please refer example_gnss

## Known issues

1. In the first version of the Air510U development board, the PPS resistance is too large, resulting in the PPS lamp still not bright after successful positioning.
2. Built-in firmware is ROM version, configuration information cannot be saved
3. The built-in firmware only supports GPS Beidou QZSS, and does not support other navigation constellations
4. The built-in firmware only supports satellites with Beidou number 37 and below
5. Air510U The antenna pin of the development board does not have feed. If you need to use an active antenna, you need to connect the capacitor from the VDD_REF pin to the antenna pin.

## Firmware Loading (Advanced Topics)

Air510U The built-in firmware is ROM version, which cannot save configuration information or update firmware, but supports "load firmware" function

The files required for the following operations are downloaded from the pkg directory of [uc6228 for LuatOS](https://github.com/wendal/luatos-lib-uc6228)

### Firmware Loading Operation Flow

1. At a frequency of 10ms, continuously send 'M! to the RX pin of the module! T' instruction, 3 bytes, send continuously
2. After the module is completely powered off, the module will be powered on again, and the module will return to 'YC', and then output 'C' every 2 seconds, which means that the loading mode is successfully entered. Otherwise, please power off and power on again.
3. Stop sending 'M! T', use 'xmodem' protocol, send bootloader file
4. Switch the serial port baud rate according to the selected bootloader file, for example, the bl921600 represents baud rate 921600
5. At this time, the' C' character should be continuously received, which means that the bootloader startup is complete and the firmware is ready to be received.
6. Send firmware file using 'xmodem' protocol
7. After the sending is completed, the module will restart and enter the firmware operation mode. According to the different firmware files, switch to the correct baud rate, usually 115200
8. At this point, the module should output an NMEA statement, representing that the module is running correctly.
