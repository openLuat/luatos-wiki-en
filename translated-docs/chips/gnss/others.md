# Introduction to Other Positioning Methods

Common positioning methods are:

1. base station positioning
2. wifi Positioning
3. Bluetooth positioning
4. RTK Positioning
5. UWB Positioning

## base station positioning

* Principle: Search and report the surrounding base station information. After reporting to the server, the server estimates an approximate coordinate according to the triangle positioning method, with an accuracy ranging from 500 to 5000 meters.
* Advantages: as long as it is 4G communication module support, no additional hardware costs
* Disadvantages: positioning accuracy is not high, the error ranges from 500 to 5000 meters

## wifi Positioning

* Principle: Search and report nearby wifi information. After reporting to the server, the server estimates an approximate coordinate according to the triangle positioning method, with an accuracy ranging from 50 to 500 meters.
* Advantages: as long as there is wifi scan function to support
* Disadvantages: The positioning accuracy is not high, and the error ranges from 50 to 500 meters. However, for modules without independent wifi antennas, searching for wifi affects data communication, such as Air780E series

## Bluetooth positioning

* Principle: Search and report nearby Bluetooth information. After reporting to the server, the server estimates an approximate coordinate according to the triangle positioning method.
* Advantages: as long as there is Bluetooth scan function to support
* Disadvantages: Usually used in conjunction with other positioning methods, Bluetooth itself has no networking function

## RTK Positioning

* Principle: The high-end GNSS module supports RTK differential positioning. By continuously receiving differential data, higher precision positioning can be achieved, up to centimeter level.
* Advantages: high positioning accuracy, error within centimeter level
* Disadvantages: Requires hardware support, expensive, differential data needs to be continuously injected, usually obtained through the network, some high-end models can support satellite-based differential data.

## UWB Positioning

* Principle: Use ultra-wideband technology to achieve ultra-low power consumption, low cost, high precision, and the error is within the centimeter level
* Advantages: high positioning accuracy, error within centimeter level
* Disadvantages: Requires hardware support, expensive, usually only for indoor positioning

## LuatOS Support library for the above positioning methods

Most are third-party libraries, please verify and evaluate

* Base station location: [luatos official library lbsLoc](https://wiki.luatos.org/api/libs/lbsLoc.html)
* Base station positioning v2: [luatos official library lbsLoc2](https://wiki.luatos.org/api/libs/lbsLoc2.html)
* Pure wifi positioning: [luatos-lib-wifiloc, third-party library](https://github.com/wendal/luatos-lib-wifiloc)
* rtk Positioning, docking ntrip protocol: [luatos-lib-ntrip, third-party library](https://github.com/wendal/luatos-lib-ntrip)
* Converged Positioning, Tencent LBS, Base Station wifi Bluetooth: [luatos-lib-qqlbs, Third Party Library](https://github.com/wendal/luatos-lib-qqlbs)
* rtk Positioning, China Mobile Single Frequency ntrip Broadcast: [luatos-lib-onenetcors, Third Party Library](https://github.com/wendal/luatos-lib-onenetcors)
