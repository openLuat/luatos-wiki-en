# lbsLoc - lbsLoc Sending Base Station Location Request

{bdg-secondary}`Adaptation status unknown`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../script/libs/lbsLoc.lua). If there is any error, please submit issue or help modify pr, thank you！
```


**Example**

```lua
--Note: due to the use of sys.wait() all apis need to be used in the coroutine
--Usage Examples
--Note: The PRODUCT_KEY here is for demonstration purposes only and should not be used in a production environment
--In mass production projects, you must use the project productKey created in iot.openluat.com, which can be viewed in the project details.
--The coordinate system of base station positioning is WSG84
PRODUCT_KEY = "v32xEAKsGTIEQxtqgwCldp5aPlcnPs3K"
local lbsLoc = require("lbsLoc")
-- Function: Obtain the callback function after the longitude and latitude corresponding to the base station
-- Parameters:-result:number type, 0 indicates success, 1 indicates that the network environment is not ready, 2 indicates that the connection server failed, 3 indicates that the sending data failed, 4 indicates that the receiving server responded with timeout, and 5 indicates that the server returned the query failed; When it is 0, the following 5 parameters are meaningful
        -- lat：string Type, latitude, integer part 3 digits, decimal part 7 digits, for example 031.2425864
        -- lng：string Type, longitude, integer part 3 digits, decimal part 7 digits, for example 121.4736522
        -- addr：currently meaningless
        -- time：string Type or nil, time returned by the server, 6 bytes, year, month, day, hour, minute, second, need to be converted to hexadecimal read
            -- First byte: year minus 2000, for example, 2017 0x11
            -- The second byte: month, for example, 0x 07 in July and 0x 07 in December 0x0C
            -- The third byte: day, for example, the 11th is 0x0B
            -- The fourth byte: when, for example, 18 0x12
            -- The fifth byte: minutes, for example, 59 minutes is 0x3B
            -- The sixth byte: seconds, for example, 48 seconds 0x30
        -- locType：numble Type or nil, positioning type, 0 means base station positioning success, 255 means WIFI positioning success
function getLocCb(result, lat, lng, addr, time, locType)
    log.info("testLbsLoc.getLocCb", result, lat, lng)
    -- Obtain latitude and longitude successfully, coordinate system WGS84
    if result == 0 then
        log.info("Time returned by the server", time:toHex())
        log.info("Positioning type, base station positioning successfully returned 0", locType)
    end
end

sys.taskInit(function()
    sys.waitUntil("IP_READY", 30000)
    while 1 do
        mobile.reqCellInfo(15)
        sys.waitUntil("CELL_INFO_UPDATE", 3000)
        lbsLoc.request(getLocCb)
        sys.wait(60000)
    end
end)

```

## lbsLoc.request(cbFnc,reqAddr,timeout,productKey,host,port,reqTime,reqWifi)



Sending Base Station Location Request

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|function|cbFnc User callback function, the callback function is called in the form：cbFnc(result,lat,lng,addr,time,locType)|
|bool|reqAddr Whether to request the server to return specific location string information is no longer supported, fill false or nil|
|number|timeout Request timeout, in milliseconds. The default value is 20000 milliseconds.|
|string|productKey IOT The product KEY on the website, if the PRODUCT_KEY variable is defined in main.lua, this parameter can be passed.nil|
|string|host Server domain name, default "bs.openluat.com", optional alternate server (not guaranteed to be available) "bs.air32.cn"|
|string|port Server port, default "12411", generally do not need to set|
|return|nil|

**Return Value**

None

**Examples**

```lua
-- Reminder: The returned coordinate value is the WGS84 coordinate system

```

---

