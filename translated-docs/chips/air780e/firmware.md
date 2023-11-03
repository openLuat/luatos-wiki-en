# Firmware Description

## Version of the firmware

Take the V1108 version as an example

|Name | Category | Script Area Size|
|----------------------------|--------|---------|
|LuatOS-SoC_V1108_EC618      |data transmission edition  |448kb    |
|LuatOS-SoC_V1108_EC618_TTS  |TTS Version, need external SPI FLASH   |448kb    |
|LuatOS-SoC_V1108_EC618_TTS_ONCHIP |TTS Built-in resources|64kb    |
|LuatOS-SoC_V1108_EC618_FULL |Full-featured version|128kb    |
|LuatOS-SoC_V1108_EC618_CLOUD|Cloud Build | Custom    |

Firmware files are suffixed with `.soc`

## Explanation of firmware classification

1. The data transmission version does not contain UI class (U8G2/LCD/EINK/DISP/LVGL)/TTS, and only contains a small number of peripheral driver libraries.
2. TTS version, adding TTS support to the digital distribution version, but TTS resources need to be external to the additional SPI Flash
3. TTS_ONCHIP Version, add TTS support to the digital version, and TTS resources are built-in, but the script space is small.
4. The full-featured version will open most libraries, but it should be noted that the built-in TTS resource mode of the TTS library is not included.
5. Cloud build version, firmware customized by [cloud compilation](../../develop/compile/Cloud_compilation.md)

If the first three models cannot meet the requirements, please use the [cloud compilation](../../develop/compile/Cloud_compilation.md) function to customize the firmware you need.

V1107 The EC618 series of cloud compilation supports customization according to the source code of the release version, which is already online.

## OTA Related explanations

1. The size of the script area, including the total size of the script and resource files..
2. All versions support `Script OTA`, which is enabled by default
3. The bottom differential of adjacent versions refers to the FOTA between adjacent versions, for example, the bottom differential of V1103 is upgraded to the bottom differential of V1105
4. Cross-version bottom differential, which refers to the FOTA between non-adjacent versions, for example, V1103 bottom differential is upgraded to V1105 bottom differential
5. EC618 The underlying upgrade of is based on the differential algorithm, and the differential package of the AP CP part cannot exceed 480kb
6. The differential upgrade package generated in the menu of the LuaTools main interface will display the details of the differential package.
7. Note: **Script area size is different** in the bottom layer, the bottom layer differential upgrade cannot be performed.
8. Cloud-compiled firmware, in the case of the same configuration, the underlying difference of the adjacent version is usually feasible.

Level of support
1. Support-Ensure support, worst case scenario is multiple differential complete upgrades
2. Support as much as possible-if not, a transitional version or related solution will be given.
3. No guarantee-no guarantee


## Firmware download and change history

Firmware compression package download: https://gitee.com/openLuat/LuatOS/releases, the LuaTools will automatically download the latest version, which can be found in the resource directory.
Change History: https://gitee.com/openLuat/luatos-soc-2022/blob/master/changelog_luatos.txt

## Contents of the firmware package

Take the V1105 version as an example, the overall layout is as follows

```
- compressed package root directory
    - LuatOS-SoC_V1105_EC618.soc
    - LuatOS-SoC_V1105_EC618_TTS.soc
    - LuatOS-SoC_V1105_EC618_FULL.soc
    - Functional Test Firmware
        - Air780EG Positioning Test Firmware _LuatOS-SoC_V1105_EC618_FULL.soc
        - Flash Test Firmware _LuatOS-SoC_V1105_EC618_FULL.soc
        - AirTun Networking Test Firmware _LuatOS-SoC_V1105_EC618_FULL.soc
    - demo
        - adc
        - gpio
        - socket
        - http
        - mqtt
    - script
        - libs
            ads1115.lua
            mcp25125.lua
        - turnkey
            - web_audio
                - main.lua
```

Directory Explanation:
1. `Functional Test Firmware `contains firmware that can be directly brushed for testing, with scripts
2. `demo` Contains examples of various features
3. `script/libs` Expansion function, mainly peripheral driver library, supports a variety of sensors and expansion components
4. `script/turnkey` Quasi-commercial grade solution with full business logic implementation as reference project code

The firmware release package is a compressed package in `zip` format, please decompress it and use it.

## Solution for differential packets exceeding 480k-multiple differential upgrades

Due to the limitation of the differential mechanism, the AP CP differential of the differential package cannot exceed 480k, but it can still be upgraded at the bottom layer through multiple differentials.

The version on the device is assumed to be 1.0.0, and the target version is 2.0.0

differential chain:
```
1.0.0 --> One or more differential packages containing only FOTA scripts --> 2.0.0
```

The idea is as follows:
1. The intermediate version only takes FOTA upgrade version, which is designed to upgrade to the next version.
2. The last differential version, adding the full business script, finally differentiating 2.0.0

Requirements for intermediate versions :
1. The earlier the intermediate version, consider subtraction first to ensure that the differential package is reduced to 480k, and the script only writes FOTA code.
2. The later the intermediate version, you should gradually increase the library.

**Technical Details Reminder: Be sure to run through the entire differential chain before upgrading in batches.**

with fota-only differential script

```lua
-- The following parameters must be set
PROJECT = "fotademo"
VERSION = "1.0.0"
PRODUCT_KEY = "123"

sys = require "sys"
sysplus = require "sysplus"
libnet = require "libnet"
libfota = require "libfota"

function fota_cb(ret)
    log.info("fota", ret)
    if ret == 0 then
        rtos.reboot() -- FOTA Done, restart
    end
end

-- upgrade using the co-zhou iot platform
libfota.request(fota_cb)
 -- Write here for 10 minutes to OTA to the next version faster, please judge according to the actual situation.
sys.timerLoopStart(libfota.request, 600000, fota_cb)

-- Upgrade with a self-built server
-- local ota_url = "http://myserv.com/myapi/version=" .. _G.VERSION .. "&imei=" .. mobile.imei()
-- libfota.request(fota_cb, ota_url)
-- sys.timerLoopStart(libfota.request, 3600000, fota_cb, ota_url)

-- User code ended---------------------------------------------
-- It always ends with this sentence.
sys.run()
-- sys.run()Don't add any statements after that.!!!!!
```
