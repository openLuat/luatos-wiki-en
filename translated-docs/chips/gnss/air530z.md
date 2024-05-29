# Air530Z

## Hardware Data

* Information website: [air530z.cn](https://air530z.cn)

The hardware is divided into two versions, the ordinary version of 'Air530Z' and the single Beidou version of 'Air530Z-BD ', the hardware package is exactly the same, the difference is that the software supports different constellations

## Software Information

For detailed communication protocol, please refer to [Air530Z protocol document](https://cdn.openluat-luatcommunity.openluat.com/attachment/20210301115201307_CASIC%E5%A4%9A%E6%A8%A1%E5%8D%AB%E6%98%9F%E5%AF%BC%E8%88%AA%E6%8E%A5%E6%94%B6%E6%9C%BA%E5%8D%8F%E8%AE%AE%E8%A7%84%E8%8C%83-20210301.pdf)

### UART Parameters

* Depending on the batch and package, the default baud rate may be 9600 or 115200
* Data bit 8, stop bit 1, no check
* Command to switch to 115200 baud rate: '$PCAS01,5*19\r\n' Note that the following'\r\n' is required, the actual sending is '0x0D 0x0A' 2 bytes
* After switching the baud rate, subsequent data will be sent at the new baud rate immediately
* UART The level is 3.3V
* The highest baud rate supported is 115200, the lowest 9600

### Supported navigation constellations

* Ordinary version: GPS, Beidou second generation/third generation, GLONASS
* Single Beidou Edition: Beidou II/III

Switch Navigation Constellation:

1. Constellation number: GPS = 1, Beidou=2, GLONASS=4
2. Constellations can be combined, such as GPS Beidou = 3, Beidou GLONASS = 6, all=7

Examples:

```
// Switch to GPS Beidou
$PCAS04,3*1A\r\n
// Switch to Single GPS
$PCAS04,1*18\r\n
// Switch to single Beidou
$PCAS04,2*1B
```

Reminder: The single Beidou version ('Air530Z-BD') does not support switching navigation constellations, and the above instructions will be ignored.

### Ephemeris information

Address:

* Air530Z Series [GPS Beidou http://download.openluat.com/9501-xingli/CASIC_data.dat](http://download.openluat.com/9501-xingli/CASIC_data.dat)
* Air530Z Series [Single Beidou http://download.openluat.com/9501-xingli/CASIC_data_bds.dat](http://download.openluat.com/9501-xingli/CASIC_data_bds.dat)

Update cycle is 10 minutes once, ephemeris validity: GPS is 4 hours, Beidou is 1 hour

The single Big Dipper ephemeris will be smaller than the GPS Big Dipper ephemeris, which is normal. The Big Dipper ephemeris is a subset of GPS Beidou

Write Method:

1. Ephemeris itself is a binary file, which can be written to the UART RXD port of the module. It can be written in segments (not less than 256 bytes) or once.
2. The ephemeris itself comes with verification, but there is no return value for writing.

Reminder:

1. The ephemeris is also continuously received from the satellite while the device is in operation, thereby automatically extending the ephemeris validity period
2. Keeping the standby power supply without dropping can keep the ephemeris. After the standby power supply is also powered off, the ephemeris will be lost and needs to be injected again.
