# auxiliary positioning AGPS

AGPS(Assisted GPS)Assisted positioning refers to the injection of three-factor data into the GPS/GNSS module to accelerate positioning and reduce the time-consuming success of the first positioning.

The three elements refer:

* Satellite Ephemeris
* Precise UTC time
* Rough Current Position

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