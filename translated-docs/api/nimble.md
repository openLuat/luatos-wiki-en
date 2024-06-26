# nimble - Bluetooth BLE library (nimble version)

{bdg-success}`Adapted` {bdg-primary}`Air601` {bdg-primary}`Air101/Air103` {bdg-primary}`ESP32C3` {bdg-primary}`ESP32S3`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/nimble/src/luat_lib_nimble.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view nimble's demo example](https://gitee.com/openLuat/LuatOS/tree/master/demo/nimble)
```

**Example**

```lua
-- This library currently supports Air101/Air103/ESP32/ESP32C3/ESP32S3
-- Please refer to demo for usage. API functions will be attributed to the specified mode.

-- Name explanation:
-- peripheral Peripheral mode, or become slave mode, is the connected device
-- central    Central mode, or become host mode, is to scan and connect other devices
-- ibeacon    Periodic beacon broadcasts

-- UUID       The services (service) and features (characteristic) of the device will be identified by UUID and support 2 bytes/4 bytes/16 bytes, usually with a shortened version of 2 bytes
-- chr        The service (service) of a device consists of multiple characteristics (characteristic), abbreviated chr
-- characteristic A feature consists of a UUID and flags, where the UUID is used as an identifier and the flags represent the functions that the feature can support.

```

## Constant

|constant | type | explanation|
|-|-|-|
|nimble.CHR_F_WRITE|number|chr FLAGS value, writable, and requires a response|
|nimble.CHR_F_READ|number|chr FLAGS value of, readable|
|nimble.CHR_F_WRITE_NO_RSP|number|chr FLAGS value, writable, no response required|
|nimble.CHR_F_NOTIFY|number|chr The FLAGS value can be subscribed to and does not require a reply.|
|nimble.CHR_F_INDICATE|number|chr The FLAGS value of can be subscribed to and needs to be replied.|
|nimble.CFG_ADDR_ORDER|number|UUID The size of the conversion, used in combination with the config function, default 0, optional 0/1|


## nimble.init(name)



Initialize BLE context and start external broadcasting/scanning

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Bluetooth device name, optional, recommended to fill in|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Success or not|

**Examples**

```lua
-- Reference demo/nimble
-- This function works for all modes

```

---

## nimble.deinit()



Close BLE context

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|bool|Success or not|

**Examples**

```lua
-- Only some devices support, may not be currently supported
-- This function works for all modes

```

---

## nimble.mode(tp)



Set Mode

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Mode, default server/peripheral, optional client/central mode nimble.MODE_BLE_CLIENT|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Success or not|

**Examples**

```lua
-- Reference demo/nimble
-- must be called before nimble.init()
-- nimble.mode(nimble.MODE_BLE_CLIENT) -- Short for slave mode, not perfect

```

---

## nimble.connok()



Whether the connection has been established

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|bool|Connected returns true, otherwise false|

**Examples**

```lua
log.info("ble", "connected?", nimble.connok())
-- slave peripheral mode, whether the device has been connected
-- Host central mode, is connected to the device
-- ibeacon Pattern, meaningless

```

---

## nimble.send_msg(conn, handle, data)



Send message

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|connection id, currently fixed 1|
|int|processing id, currently fixed 0|
|string|Data string, which can contain invisible characters|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Success or not|

**Examples**

```lua
-- Reference demo/nimble
-- This function is applicable to peripheral/slave mode

```

---

## nimble.setUUID(tp, addr)



Set the server/peripheral UUID

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Configuration string, which is illustrated in the following example|
|string|address string|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Success or not|

**Examples**

```lua
-- Refer to demo/nimble, firmware compiled after 2023-02-25 supports this API
-- must be called before nimble.init()
-- This function is applicable to peripheral/slave mode

-- Set UUID in SERVER/Peripheral mode, support setting 3
-- Address supports 2/4/16 bytes, binary data required
-- 2 Byte address example: AABB, write string.fromHex("AABB"), or string.char(0xAA, 0xBB)
-- 4 Byte address example: AABBCCDD, write string.fromHex("AABBCCDD"), or string.char(0xAA, 0xBB, 0xCC, 0xDD)
nimble.setUUID("srv", string.fromHex("380D"))      -- Service Primary UUID, default value 180D
nimble.setUUID("write", string.fromHex("FF31"))    -- The UUID of data written to the device. Default value FFF1
nimble.setUUID("indicate", string.fromHex("FF32")) -- The UUID of the data subscribed to this device. Default value FFF2

```

---

## nimble.mac(mac)



Get Bluetooth MAC

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|The MAC address to be set, 6 bytes, is obtained only if it is not transmitted.|

**Return Value**

|return value type | explanation|
|-|-|
|string|Bluetooth MAC address, 6 bytes|

**Examples**

```lua
-- Refer to demo/nimble, firmware compiled after 2023-02-25 supports this API
-- This function works for all modes
local mac = nimble.mac()
log.info("ble", "mac", mac and mac:toHex() or "Unknwn")

-- Modify MAC address, 2024.06.05 new, currently only Air601 supports, restart after modification takes effect
nimble.mac(string.fromHex("1234567890AB"))

```

---

## nimble.sendNotify(srv_uuid, chr_uuid, data)



Send notify

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|UUID of the service, reserved, just fill in nil|
|string|UUID of the feature, which must be filled in|
|string|Data, required, related to MTU size, generally no more than 256 bytes|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise false|

**Examples**

```lua
-- This API was added on July 31, 2023.07.31
-- This function is applicable to peripheral mode
nimble.sendNotify(nil, string.fromHex("FF01"), string.char(0x31, 0x32, 0x33, 0x34, 0x35))

```

---

## nimble.sendIndicate(srv_uuid, chr_uuid, data)



Send indicate

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|UUID of the service, reserved, just fill in nil|
|string|UUID of the feature, which must be filled in|
|string|Data, required, related to MTU size, generally no more than 256 bytes|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise false|

**Examples**

```lua
-- This API was added on July 31, 2023.07.31
-- This function is applicable to peripheral mode
nimble.sendIndicate(nil, string.fromHex("FF01"), string.char(0x31, 0x32, 0x33, 0x34, 0x35))

```

---

## nimble.advParams(conn_mode, disc_mode, itvl_min, itvl_max, channel_map, filter_policy, high_duty_cycle)



Set broadcast parameters

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Broadcast mode, 0-no connection, 1-directed connection, 2-undirected connection, default 0|
|int|Discovery mode, 0-undiscoverable, 1-restricted discovery, 3-universal discovery, default 0|
|int|Minimum broadcast interval, 0-use default, range 1 - 65535, unit 0.625ms, default 0|
|int|Maximum broadcast interval, 0-use default, range 1 - 65535, unit 0.625ms, default 0|
|int|Broadcast channel, default 0, generally do not need to set|
|int|Filter rule, default 0, generally do not need to set|
|int|When the broadcast mode is "directional connection", whether to use high duty cycle mode, default 0, optional 1|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- Only ibeacon mode/peripheral/slave is currently available
-- For example, setting non-connectible limit discovery
-- Needs to be set up before nimble.init
nimble.advParams(0, 1)
-- Note that in peripheral mode, the automatic configuration conn_mode and disc_mode

```

---

## nimble.setChr(index, uuid, flags)



set the characteristics of chr

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|chr The index of, the default 0-3|
|int|chr UUID, can be 2/4/16 bytes|
|int|chr FLAGS, please refer to the constant table|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- peripheral/slave only
nimble.setChr(0, string.fromHex("FF01"), nimble.CHR_F_WRITE_NO_RSP | nimble.CHR_F_NOTIFY)
nimble.setChr(1, string.fromHex("FF02"), nimble.CHR_F_READ | nimble.CHR_F_NOTIFY)
nimble.setChr(2, string.fromHex("FF03"), nimble.CHR_F_WRITE_NO_RSP)
-- Available demo/nimble/kt6368a

```

---

## nimble.config(id, value)



set the characteristics of chr

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|the configured id, please refer to the constant table|
|any|Depending on the configuration, there are different optional values|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- This function is available in any mode
-- This API was added on July 31, 2023.07.31
-- For example, set the size of the address conversion end, the default is 0, compatible with the old code
-- Set to 1, the service UUID and chr UUID are more intuitive.
nimble.config(nimble.CFG_ADDR_ORDER, 1)

```

---

## nimble.ibeacon(data, major, minor, measured_power)



Configure iBeacon parameters, only iBeacon mode available

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Data, must be 16 bytes|
|int|Major version number, default 2, optional, range 0 ~ 65536|
|int|minor version number, default 10, optional, range 0 ~ 65536|
|int|Nominal power, default 0, range -126 20 |

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise false|

**Examples**

```lua
-- Refer to demo/nimble, firmware compiled after 2023-02-25 supports this API
-- This function is applicable to ibeacon mode
nimble.ibeacon(data, 2, 10, 0)
nimble.init()

```

---

## nimble.advData(data, flags)



Configure broadcast data, only iBeacon mode is available

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Broadcast data, currently up to 128 bytes|
|int|Broadcast identification, optional, default value is 0x 06, that is, traditional Bluetooth (0x 04) common discovery mode is not supported(0x02)|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise false|

**Examples**

```lua
-- Refer to demo/nimble/adv_free, firmware compiled after 2023-03-18 supports this API
-- This function is applicable to ibeacon mode
-- Data sources can be varied
local data = string.fromHex("123487651234876512348765123487651234876512348765")
-- local data = crypto.trng(25)
-- local data = string.char(0x11, 0x13, 0xA3, 0x5A, 0x11, 0x13, 0xA3, 0x5A, 0x11, 0x13, 0xA3, 0x5A, 0x11, 0x13, 0xA3, 0x5A)
nimble.advData(data)
nimble.init()

-- nimble Support to call again at any time after init to achieve data update
-- For example, once a minute.
while 1 do
    sys.wait(60000)
    local data = crypto.trng(25)
    nimble.advData(data)
end

```

---

## nimble.scan(timeout)



Scan slave

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Timeout, in seconds, 28 seconds by default|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Start scan successfully or not|

**Examples**

```lua
-- Reference demo/nimble/scan
-- This function is available for central/host mode
-- This function returns directly and then returns the result via an asynchronous callback

-- Before calling this function, you need to make sure that you have nimble.init()
nimble.scan()
-- timeout Parameter added on 2023.7.11

```

---

## nimble.connect(mac)



Connect to slave

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|MAC address of the device|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Start connection successfully or not|

**Examples**

```lua
-- This function is available for central/host mode
-- This function returns directly and then returns the result via an asynchronous callback

```

---

## nimble.disconnect()



Disconnect from the slave

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- This function is available for central/host mode
-- This function returns directly

```

---

## nimble.discSvr()



List of services for scanning slave

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- This function is available for central/host mode
-- This function returns directly and then returns the result asynchronously
-- This API usually does not need to be called, and will be called once after the connection to the slave is completed.

```

---

## nimble.listSvr()



Obtain the service list of the slave.

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|table|Array of service UUIDs|

**Examples**

```lua
-- This function is available for central/host mode

```

---

## nimble.discChr(svr_uuid)



Characteristic value of the specified service of the scanning slave

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Specifies the UUID value of the service|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Scan started successfully or not|

**Examples**

```lua
-- This function is available for central/host mode

```

---

## nimble.listChr(svr_uuid)



Obtain the feature value list of the specified service of the slave.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Specifies the UUID value of the service|

**Return Value**

|return value type | explanation|
|-|-|
|table|A list of characteristic values, containing the UUID and flags|

**Examples**

```lua
-- This function is available for central/host mode

```

---

## nimble.discDsc(svr_uuid, chr_uuid)



Scan other properties of the characteristic value of the specified service of the slave.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Specifies the UUID value of the service|
|string|The UUID value of the eigenvalue|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Scan started successfully or not|

**Examples**

```lua
-- This function is available for central/host mode

```

---

## nimble.writeChr(svr_uuid, chr_uuid, data)



Writes data to the specified feature value of the specified service.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Specifies the UUID value of the service|
|string|Specifies the UUID value of the feature value|
|string|Data to be written|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Write started successfully or not|

**Examples**

```lua
-- This function is available for central/host mode

```

---

## nimble.readChr(svr_uuid, chr_uuid)



Read data from the specified feature value of the specified service (asynchronous)

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Specifies the UUID value of the service|
|string|Specifies the UUID value of the feature value|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Write started successfully or not|

**Examples**

```lua
-- This function is available for central/host mode
-- Detailed Usage Please Parameters demo/nimble/central

```

---

## nimble.subChr(svr_uuid, chr_uuid)



Subscribe to the specified characteristic value of the specified service

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Specifies the UUID value of the service|
|string|Specifies the UUID value of the feature value|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Subscription started successfully or not|

**Examples**

```lua
-- This function is available for central/host mode
-- Detailed Usage Please Parameters demo/nimble/central

```

---

## nimble.unsubChr(svr_uuid, chr_uuid)



Unsubscribe from the specified feature value of the specified service

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Specifies the UUID value of the service|
|string|Specifies the UUID value of the feature value|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Start unsubscribe successfully or not|

**Examples**

```lua
-- This function is available for central/host mode
-- Detailed Usage Please Parameters demo/nimble/central

```

---

