# auxiliary positioning AGPS

AGPS(Assisted GPS)Assisted positioning refers to the injection of three-factor data into the GPS/GNSS module to accelerate positioning and reduce the time-consuming success of the first positioning.

The three elements refer:

* Satellite Ephemeris
* Precise UTC time
* Rough Current Position

Just like its name "auxiliary positioning", these data are * * auxiliary *, not necessary, or can not be used, just to speed up positioning.

If the signal in the environment is not good, or the antenna performance is too poor, the auxiliary positioning cannot be saved..

After assisted positioning, the position will still drift statically, which is the characteristic of GNSS and cannot be completely avoided. RTK positioning equipment can be bought with higher precision, which is more than 10 times more expensive..

## Satellite Ephemeris

Included content is:

1. Satellite orbit parameters, which can be used to calculate the satellite position
2. ionospheric correction parameters
3. Fixed-point UTC correction information

Ephemeris download address:

* Air510U and Air780EG [GPS Beidou](http://download.openluat.com/9501-xingli/HXXT_GPS_BDS_AGNSS_DATA.dat)
* Air530Z Series [GPS Beidou](http://download.openluat.com/9501-xingli/CASIC_data.dat)
* Air530Z Series [Single Beidou](http://download.openluat.com/9501-xingli/CASIC_data_bds.dat)
* Air780EPVH [GPS+Beidou](http://download.openluat.com/9501-xingli/HXXT_GPS_BDS_AGNSS_DATA.dat)

Update cycle is 10 minutes once, ephemeris validity: GPS is generally 4 hours, Beidou is 1 hour

## Precise UTC time

This is generally obtained from NTP, and the recommended update period is 4 hours.

## Rough Current Position

The accuracy of the position does not need to be very high, but it should be within 15km as much as possible.

1. Base station positioning/wifi positioning
2. Old known location

### base station positioning

Base station positioning, AT/CSDK/LuatOS development have corresponding commands and functions, specific reference to the corresponding development documents

### Old known location

* It is recommended to save the current location immediately after the first successful positioning
* Save the current position every 10 minutes by time period
* If the network can be continuously connected or the base station can be successfully located, it is also possible not to save the location locally.

## About Indoor Positioning

* There are GNSS signals by the window, but they are not strong, and there is no guarantee that they can be located. There are many kinds of "windows" and there are many that cannot see the sky. It is normal that the positioning is not successful..
* All * * nominal * * XXX seconds positioning success, are according to the outdoor, open, sunny environment test, is the theoretical best value
* The mobile phone can be positioned indoors because there are additional means such as base station/wifi/Bluetooth, and the antenna is debugged well, GNSS chip is also expensive (usually integrated in SoC)!!!

## Ephemeris analysis (not required, research use)

for study and research [https://gitee.com/openLuat/luatos-ext-gnss](https://gitee.com/openLuat/luatos-ext-gnss)
