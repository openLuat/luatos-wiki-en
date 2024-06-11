# Used with communication modules (such as 4G modules)

Common ways are:

1. Use an integrated communication GNSS module, e.g. Air780EG, Air780EPVH
2. collocation way, Air780E+Air510U, Air101 + Air530Z

## Integrated module

Essentially, you put the GNSS chip into the module.:

1. Through UART, the GNSS chip and 4G chip data communication connection
2. Control the power supply of GNSS chip through GPIO
3. Provide standby power for GNSS chips through long-powered GPIO
4. By outputting a 26MHz clock to drive the GNSS chip, only a few modules will be used.

Known integrated modules are :

* `Air780EG = Air780E + Air510U`
* `Air780EPVH = Air780EPV + Air510H`, Reminder: There are currently no Air510H modules
* `Air780ETGG = Air780ET + Air510U`
* `Air820UG = Air724UG + Air530Z`

Recently, there are various modules with single Beidou characteristics, so the above combination will also give rise to a single Beidou version.

## The advantages and disadvantages between the way

1. Integrated module: small size, more compact, saving wiring space, standby power is controlled by the main chip, when the main power supply is powered off, the standby power of the GNSS chip will also be powered off
2. Matching module: it is large in size and needs to take up more space, but the standby power can be controlled by itself, and it can also be made into optional pasting during production, for example, model a has GNSS function, and model B does not have GNSS module pasting without it.

There is no decision, please select according to your own business.
