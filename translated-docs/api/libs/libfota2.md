# libfota2 - fota Upgrade v2

{bdg-secondary}`Adaptation status unknown`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../script/libs/libfota2.lua). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view libfota2 demo examples](https://gitee.com/openLuat/LuatOS/tree/master/demo/fota2)
```

**Example**

```lua
--Usage Examples
local libfota2 = require("libfota2")

-- function: get the callback function of fota
-- Parameters:
-- result:number Type
--   0 Indicates success
--   1 Indicates a connection failure
--   2 Indicates that the url is wrong
--   3 Indicates that the server is disconnected
--   4 Indicates an error in receiving a message.
--   5 Indicates that the use of the iot platform VERSION requires the use of the form xxx.yyy.zzz
function libfota_cb(result)
    log.info("fota", "result", result)
    -- fota Success
    if result == 0 then
        rtos.reboot()   --If there is something else to do, decide for yourself the timing of the reboot
    end
end

--the following example shows the joint iot platform, address :http://iot.openluat.com
libfota2.request(libfota_cb)

--If you use a self-built server, replace it yourself.url
-- The requirement for a custom server is:
-- If you need to upgrade, respond to http 200, body is the content of the upgrade file.
-- If you do not need to upgrade, you must pay attention to the code 300 or above.
local opts = {url="http://xxxxxx.com/xxx/upgrade"}
-- opts For a detailed description of, see the following function API documentation
libfota2.request(libfota_cb, opts)

-- If a scheduled upgrade is required
-- Joo-IoT Platform
sys.timerLoopStart(libfota2.request, 4*3600*1000, libfota_cb)
-- Self-built platform
sys.timerLoopStart(libfota2.request, 4*3600*1000, libfota_cb, opts)

```

## libfota.request(cbFnc, opts)



fota Upgrade

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|table|fota Parameters, described in detail later|
|function|cbFnc User callback function. The callback function is called in the form of cbFnc(result) and must be passed|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

