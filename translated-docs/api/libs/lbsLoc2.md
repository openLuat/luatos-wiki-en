# lbsLoc2 - base station positioning v2

{bdg-secondary}`Adaptation status unknown`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../script/libs/lbsLoc2.lua). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view lbsLoc2 demo examples](https://gitee.com/openLuat/LuatOS/tree/master/demo/lbsLoc2)
```

**Example**

```lua
-- Attention:
-- 1. Because sys.wait() is used, all apis need to be used in the coroutine.
-- 2. Only single base station positioning is supported, that is, the currently networked base station
-- 3. This service is currently in test status
sys.taskInit(function()
    if mobile.status() == 0 then
        sys.waitUntil("IP_READY", 3000)
    end
    local lat,lng,t = lbsLoc2.request(3000)
    log.info("lbs", lat, lng, json.encode(t or {}))
end

```

## lbsLoc2.request(timeout, host, port, reqTime)



Execute positioning request

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|number|Request timeout, in milliseconds, by default.15000|
|number|Server address, with default value, can be domain name, generally do not need to fill in|
|number|Server port, default 12411, generally do not need to fill in|
|bool|Request to return server time|

**Return Value**

|return value type | explanation|
|-|-|
|string|If successful, return the latitude of the positioning coordinate, which is the WGS84 coordinate system, otherwise it will be returned nil|
|string|If successful, return the accuracy of the positioning coordinates, which is the WGS84 coordinate system, otherwise it will be returned.nil|
|table|Server time, East Zone 8 time. When the reqTime is true and the location is successful, it will be returned.|

**Examples**

```lua
sys.taskInit(function()
    if mobile.status() == 0 then
        sys.waitUntil("IP_READY", 3000)
    end
    local lat,lng,t = lbsLoc2.request(3000)
    log.info("lbs", lat, lng, json.encode(t or {}))
end

```

---
