# mobile - Cellular Network

{bdg-success}`Adapted` {bdg-primary}`Air780E/Air700E`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/mobile/luat_lib_mobile.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view the demo example of mobile](https://gitee.com/openLuat/LuatOS/tree/master/demo/mobile)
```

**Example**

```lua
-- Simple Demo

log.info("imei", mobile.imei())
log.info("imsi", mobile.imsi())
local sn = mobile.sn()
if sn then
    log.info("sn",   sn:toHex())
end
log.info("muid", mobile.muid())
log.info("iccid", mobile.iccid())
log.info("csq", mobile.csq())
log.info("rssi", mobile.rssi())
log.info("rsrq", mobile.rsrq())
log.info("rsrp", mobile.rsrp())
log.info("snr", mobile.snr())
log.info("simid", mobile.simid())

```

## Constant

|constant | type | explanation|
|-|-|-|
|mobile.UNREGISTER|number|Not registered|
|mobile.REGISTERED|number|Registered|
|mobile.SEARCH|number|Searching|
|mobile.DENIED|number|Registration Rejected|
|mobile.UNKNOW|number|Unknown|
|mobile.REGISTERED_ROAMING|number|Registered, Roaming|
|mobile.SMS_ONLY_REGISTERED|number|Registered, only SMS|
|mobile.SMS_ONLY_REGISTERED_ROAMING|number|Registered, Roaming, Only SMS|
|mobile.EMERGENCY_REGISTERED|number|Registered, Emergency Services|
|mobile.CSFB_NOT_PREFERRED_REGISTERED|number|Registered, non-primary service|
|mobile.CSFB_NOT_PREFERRED_REGISTERED_ROAMING|number|Registered, Non Primary Service, Roaming|
|mobile.CONF_RESELTOWEAKNCELL|number|Cell reselection signal difference threshold, flight mode setting required|
|mobile.CONF_STATICCONFIG|number|Network static mode optimization, flight mode setting required|
|mobile.CONF_QUALITYFIRST|number|Network switching gives priority to signal quality, flight mode setting is required, 0 is not on, 1 is on, 2 is on and the switching is accelerated, power consumption will increase|
|mobile.CONF_USERDRXCYCLE|number|LTE Paging, flight mode setting is required, use it carefully, 0 is not set, 1~7 increases or decreases the DrxCycle cycle multiple, 1:1/8 times 2:1/4 times 3:1/2 times 4:2 times 5:4 times 6:8 times 7:16 times, 8~12 is configured with a fixed DrxCycle cycle, this configuration will only take effect if the period is greater than the network-assigned DrxCycle period,8:320ms 9:640ms 10:1280ms 11:2560ms 12:5120ms|
|mobile.CONF_T3324MAXVALUE|number|PSM T3324 time in mode, units S|
|mobile.CONF_PSM_MODE|number|PSM Mode switch, 0 off, 1 on|
|mobile.CONF_CE_MODE|number|attach Mode, 0 is EPS ONLY and 2 is mixed. If IMSI detach is out of the network, set it to 0. Note that when it is set to EPS ONLY, the SMS function will be canceled.|
|mobile.CONF_SIM_WC_MODE|number|SIM Configure and read the number of writes|
|mobile.PIN_VERIFY|number|Verify PIN operation|
|mobile.PIN_CHANGE|number|Change PIN Operation|
|mobile.PIN_ENABLE|number|enable PIN verification|
|mobile.PIN_DISABLE|number|Turn off PIN verification|
|mobile.PIN_UNBLOCK|number|Unlock PIN|


## mobile.imei(index)



Get IMEI

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Number, default 0.0 or 1 will only appear on modules that support dual cards|

**Return Value**

|return value type | explanation|
|-|-|
|string|The current IMEI value, if failed to return nil|

**Examples**

None

---

## mobile.imsi(index)



Get IMSI

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Number, default 0.0 or 1 will only appear on modules that support dual cards|

**Return Value**

|return value type | explanation|
|-|-|
|string|The current IMSI value, if failure returns nil|

**Examples**

None

---

## mobile.sn()



Get SN

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|string|The current SN value, which returns nil on failure. Note that SN may contain invisible characters|

**Examples**

```lua
-- Note that the factory may not have written SN
-- A unique id for general purposes, which can be replaced with mobile.imei()
-- If you need a truly unique ID, use mcu.unique_id()

```

---

## mobile.muid()



Get MUID

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|string|Current MUID value, returned if failure nil|

**Examples**

None

---

## mobile.iccid(id)



Gets or sets ICCID

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|SIM Card number, for example 0, 1, default 0|

**Return Value**

|return value type | explanation|
|-|-|
|string|ICCID Value to return if failure nil|

**Examples**

None

---

## mobile.number(id)



Obtain the mobile phone card number. Note that it can only be read out if the mobile phone number is written, so it may be empty.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|SIM Card number, for example 0, 1, default 0|

**Return Value**

|return value type | explanation|
|-|-|
|string|number Value to return if failure nil|

**Examples**

None

---

## mobile.simid(id)



Get the current SIM card slot, or switch card slots

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|SIM The number of the card, such as 0,1, if you support dual cards, such as EC618, you can fill in 2 for adaptation, but it will take up 4 IO(gpio4/5/6/23). If you don't fill it in, read the current card slot directly.|
|boolean|Whether to use SIM0 first, only SIM card number write 2 adaptive is useful!!!. True gives priority to SIM0,false is determined by the specific platform, supports dual-card dual-standby SIM0 priority, does not support the last detected priority, the default is false, must be configured at boot, otherwise it is invalid|

**Return Value**

|return value type | explanation|
|-|-|
|int|current sim slot number, return if failure-1|

**Examples**

```lua
mobile.simid(0) -- Fixed use SIM0
mobile.simid(1) -- Firmware usage SIM1
mobile.simid(2) -- Automatic identification SIM0, SIM1, priority depends on the specific platform
mobile.simid(2, true) -- -- Automatically identify SIM0, SIM1, and SIM0 takes precedence
-- Reminder, automatic recognition will increase time

```

---

## mobile.simPin(id,operation,pin1,pin2)



Check whether the current SIM card is ready and perform relevant operations on the PIN code of the SIM card.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|SIM The number of the card, such as 0,1, only those who support dual cards and dual standby need to select|
|int|PIN Code operation type, can only be mobile.PIN_XXXX, no operation is left blank|
|string|Change the pin code of the operation when pin, or verify the pin code of the operation, or unlock the PUK when pin code, 4~8 bytes|
|string|New pin code when changing pin code operation, new PIN when unlocking pin code, 4~8 bytes|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|When there is no PIN operation, return whether the SIM card is ready, and when there is a PIN operation, return whether it is successful|

**Examples**

```lua
local cpin_is_ready = mobile.simPin() -- Whether the current sim card is ready or not, generally returning false means no card
local succ = mobile.simPin(0, mobile.PIN_VERIFY, "1234")	-- Enter pin code verification

```

---

## mobile.rtime(time, auto_reset_stack)



Set the RRC automatic release time interval. When the RRC is enabled, frequent data operations may cause serious network failures when extremely weak signals are encountered. Therefore, additional automatic restart protocol stack settings are required.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|RRC The automatic release time is equivalent to the AT RTIME of Air724, in seconds. If you write 0 or not, it is disabled. It should not exceed 20 seconds. It is meaningless.|
|boolean|When the network encounters a serious failure, it tries to recover automatically, which conflicts with the flight mode/SIM card switching. true is turned on, false is turned off, and when it is left blank, it will be turned on automatically if the time is set. The original factory does not need it after optimizing the protocol stack. This parameter is obsolete|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## mobile.setAuto(check_sim_period, get_cell_period, search_cell_time, auto_reset_stack, network_check_period)



Set up some auxiliary periodic or automatic functions. Currently, it supports recovery after the SIM card is temporarily separated, periodically obtains cell information, and tries to recover automatically when the network encounters serious faults.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|SIM Card automatic recovery time, in milliseconds, recommended 5000~10000, conflicts with flight mode/SIM card switching, cannot be used at the same time, must be staggered. To write 0 or not to write is to turn off the function|
|int|The time interval for periodically obtaining cell information, in milliseconds. Acquiring cell information increases some of the power consumption. To write 0 or not to write is to turn off the function|
|int|The maximum search time for each cell search, in seconds. Do not exceed 8 seconds.|
|boolean|When the network encounters a serious failure, it tries to recover automatically, which conflicts with flight mode/SIM card switching. true is on, false is off, the starting state is false, and no change is made if it is left blank.|
|int|Set timing to check whether the network is normal and recover by restarting the protocol stack when no network is detected for a long time. The recovery time of no network is in ms. It is recommended to be more than 60000. Reserve enough time for network search. Leave it blank and make no change.|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## mobile.apn(index, cid, new_apn_name, user_name, password, ip_type, protocol)



To obtain or set the APN, the APN must be set before entering the network, for example, before the SIM card identification is completed.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Number, default 0.0 or 1 will only appear on modules that support dual cards|
|int|cid, The default value is 0. If you want to activate with a non-default APN, you must>0|
|string|New APN, do not fill in is to obtain APN, fill in is to set APN, whether to support setting depends on the underlying implementation|
|string|The username of the new APN. If the APN is not empty, it must be filled in. If there is no empty string "". If the APN is empty, then you can nil|
|string|The password of the new APN. If the APN is not empty, it must be filled in. If there is no empty string "". If the APN is empty, then you can nil|
|int|IP TYPE when APN is activated, 1 = IPV4 2 = IPV6 3 = IPV4V6, default is 1|
|int|When activating APN, if you need to username and password, you must write the authentication protocol type, 1~3, default 3, which means 1 and 2 are both tried. Write without authentication 0|
|boolean|Whether to delete APN,true yes, others no, only when parameter 3 new APN is not string|

**Return Value**

|return value type | explanation|
|-|-|
|string|The default APN value obtained. If the APN fails, the value is returned.nil|

**Examples**

```lua
mobile.apn(0,1,"cmiot","","",nil,0) -- The APN of the mobile public network card is set to cmiot, which is generally not required.
mobile.apn(0,1,"name","user","password",nil,3) -- The demo,name,user, set by the special network card, password contact the card dealer to obtain

```

---

## mobile.ipv6(onff)



Whether the IPV6 function is enabled by default must be set before LTE network connection

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|boolean|Switch true on false off|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|true is currently on, false is currently off|

**Examples**

```lua
-- Note that after ipv6 is turned on, the startup network will be 2~3 seconds slower.

```

---

## mobile.csq()



Get csq

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|int|The current CSQ value, if failure returns 0. Range 0 - 31, the larger the better|

**Examples**

```lua
-- Note that the CSQ value of 4G module is for reference only, rsrp/rsrq is the real signal strength indicator

```

---

## mobile.rssi()



Get rssi

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|int|The current rssi value. If it fails, it returns 0. The range is 0 to -114, the smaller the better.|

**Examples**

None

---

## mobile.rsrp()



Get rsrp, reference signal received power

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|int|The current rsrp value. If the rsrp value fails, return 0. Value range: -44~-140. The larger the value, the better.|

**Examples**

None

---

## mobile.rsrq()



Get rsrq, reference signal transmit power

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|int|The current rsrq value. If it fails, return 0. Value range: -3 to -19.5. The larger the value, the better.|

**Examples**

None

---

## mobile.snr()



obtain snr, signal-to-noise ratio

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|int|the current snq value, if the failure returns 0. range 0 - 30, the larger the better|

**Examples**

None

---

## mobile.eci()



Get the current serving cell ECI(E-UTRAN Cell Identifier)

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|int|current eci value. if the eci value fails, return-1|

**Examples**

None

---

## mobile.tac()



Obtain the TAC of the current serving cell or LAC

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|int|the current eci value. if it fails, it returns -1. if it has not been registered to the network, it returns 0|

**Examples**

```lua
-- This API was added on 2023.7.9

```

---

## mobile.enbid()



Get the current serving cell eNBID(eNodeB Identifier)

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|int|The current enbid value, which is returned if it fails.-1|

**Examples**

None

---

## mobile.flymode(index, enable)



In and out of flight mode

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Number, default 0.0 or 1 will only appear on modules that support dual cards|
|bool|Whether to set to flight mode, true to set, false to exit, optional|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Status of original flight mode|

**Examples**

None

---

## mobile.status()



Get Network Status

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|int|Current network status, 0: the network is not registered; 1: the network is registered; 2: searching the network; 3: network registration rejected|

**Examples**

None

---

## mobile.getCellInfo()



Obtain mechanism information

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|table|Array containing base station data|

**Examples**

```lua
-- Note: Starting from 2023.06.20, you need to actively request a reqCellInfo to have base station data..

--Sample output
--[[
[
    {"rsrq":-10,"rssi":-55,"cid":124045360,"mnc":17,"pci":115,"earfcn":1850,"snr":15,"rsrp":-85,"mcc":1120,"tdd":0},
    {"pci":388,"rsrq":-11,"mnc":17,"earfcn":2452,"snr":5,"rsrp":-67,"mcc":1120,"cid":124045331},
    {"pci":100,"rsrq":-9,"mnc":17,"earfcn":75,"snr":17,"rsrp":-109,"mcc":1120,"cid":227096712}
]
]]

mobile.reqCellInfo(60)
-- Subscription
sys.subscribe("CELL_INFO_UPDATE", function()
    log.info("cell", json.encode(mobile.getCellInfo()))
end)

-- Regular rotation training
sys.taskInit(function()
    sys.wait(3000)
    while 1 do
        mobile.reqCellInfo(15)
        sys.waitUntil("CELL_INFO_UPDATE", 15000)
        log.info("cell", json.encode(mobile.getCellInfo()))
    end
end)

```

---

## mobile.reqCellInfo(timeout)



Initiate base station information query, including adjacent cells

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Timeout duration, in seconds, default 15. Minimum 5, maximum 60|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- Reference mobile.getCellInfo function

```

---

## mobile.reset()



Restart the protocol stack

**Parameters**

None

**Return Value**

None

**Examples**

```lua
-- Restart the LTE protocol stack
mobile.reset()

```

---

## mobile.dataTraffic(clearUplink, clearDownlink)



Data volume traffic processing

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|boolean|Clear the cumulative value of upstream traffic, true, and ignore others.|
|boolean|Clear the cumulative value of downlink traffic, true, and ignore others.|

**Return Value**

|return value type | explanation|
|-|-|
|int|Upstream traffic GB|
|int|Upstream traffic B|
|int|Downstream traffic GB|
|int|Downstream traffic B|

**Examples**

```lua
-- Obtain the cumulative value of uplink and downlink traffic
-- Upstream traffic value Byte = uplinkGB * 1024 * 1024 * 1024 + uplinkB
-- Downstream traffic value Byte = downlinkGB * 1024 * 1024 * 1024 + downlinkB
local uplinkGB, uplinkB, downlinkGB, downlinkB = mobile.dataTraffic()

-- Clear the cumulative value of uplink and downlink traffic
mobile.dataTraffic(true, true)

```

---

## mobile.config(item, value)



Special network configuration, different configurations for different platforms, careful use, currently only EC618

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|configuration project, see mobile.CONF_XXX|
|int|Configuration Value|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Success or not|

**Examples**

```lua
-- EC618 Configure the cell reselection signal difference threshold, which cannot be greater than 15dbm and can only be used in flight mode.
mobile.flymode(0,true)
mobile.config(mobile.CONF_RESELTOWEAKNCELL, 15)
mobile.config(mobile.CONF_STATICCONFIG, 1) --Turn on network static optimization
mobile.flymode(0,false)

-- EC618 Set statistics for SIM writes
-- Close Statistics
mobile.config(mobile.CONF_SIM_WC_MODE, 0)
-- Enable statistics, which is also enabled by default..
mobile.config(mobile.CONF_SIM_WC_MODE, 1)
-- Read statistical values, asynchronous, need to be obtained through the system message SIM_IND.
sys.subscribe("SIM_IND", function(stats, value)
    log.info("SIM_IND", stats)
    if stats == "SIM_WC" then
        log.info("sim", "write counter", value)
    end
end)
mobile.config(mobile.CONF_SIM_WC_MODE, 2)
-- Empty Statistics
mobile.config(mobile.CONF_SIM_WC_MODE, 3)

```

---

## mobile.getBand(band, is_default)



Gets the currently used/supported band

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|zbuff|Output band|
|boolean|true Default support, false currently supported, default is false, current is reserved function, do not write true|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Return true on success, return true on failure false|

**Examples**

```lua
local buff = zbuff.create(40)
mobile.getBand(buff) --Output the currently used band, and put the band number in buff，buff[0]，buff[1]，buff[2] .. buff[buff:used() - 1]

```

---

## mobile.setBand(band, num)



Set band

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|zbuff|Enter the used band|
|int|band Quantity|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Return true on success, return true on failure false|

**Examples**

```lua
local buff = zbuff.create(40)
buff[0] = 3
buff[1] = 5
buff[2] = 8
buff[3] = 40
mobile.setBand(buff, 4) --Set a total of 4 band to use,3,5,8,40

```

---

## mobile.nstOnOff(onoff, uart_id)



RF Test switches and configurations

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|boolean|true Test mode on, false off|
|int|Serial port number|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
mobile.nstOnOff(true, uart.VUART_0)	--Open test mode and send results with virtual serial port
mobile.nstOnOff(false) --Close Test Mode

```

---

## mobile.nstInput(data)



RF Test data input

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|or zbuff The data obtained by the user from the serial port, note that after all the data is obtained, another nil needs to be sent as the end of the transmission.|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
mobile.nstInput(uart_data)
mobile.nstInput(nil)

```

---
