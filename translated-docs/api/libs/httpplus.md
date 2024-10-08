# httpplus - http Supplement to the library

{bdg-success}`Adapted` {bdg-primary}`Air780E` {bdg-primary}`Air780EP`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../script/libs/httpplus.lua). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view httpplus demo examples](https://gitee.com/openLuat/LuatOS/tree/master/demo/httpplus)
```

**Example**

```lua
-- The library supports the following functions:
--   1. Large file upload problem, unlimited size
--   2. Header settings of arbitrary length
--   3. Set the body of any length.
--   4. Automatic authentication URL identification
--   5. body Use zbuff to return, which can be directly transmitted to libraries such as uart.

-- Differences from the http library
--   1. File download not supported
--   2. Not supported fota

```

## httpplus.request(opts)



Execute HTTP request

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|table|The request parameter is a table, at least with url attribute|

**Return Value**

|return value type | explanation|
|-|-|
|int|Response code, the status code returned by the server> = 100, if an error is detected locally, it will return a value of <0|
|When the server responds normally, the result is returned. Otherwise, it is an error message or nil|

**Examples**

```lua
-- Request parameter introduction
local opts = {
    url    = "https://httpbin.air32.cn/abc", -- Required, Target URL
    method = "POST", -- Optional. By default, GET is set POST
    headers = {}, -- optional, custom extra header
    files = {},   -- Optional. File upload. If this parameter exists, the file is forced to be uploaded in the form of multipart/form-data.
    forms = {},   -- Optional, form parameter, if this parameter exists, if files does not exist, upload by application/x-www-form-urlencoded
    body  = "abc=123",-- Optional. The custom body parameter can be the string/zbuff/table, but cannot exist with files and forms at the same time.
    debug = false,    -- Optional, open debug log, default false
    try_ipv6 = false, -- Optional. Whether to try the ipv6 address first. The default value is false
    adapter = nil,    -- Optional, network adapter number, default is automatically selected
    timeout = 30,     -- Optional. The timeout period for reading the server response, in seconds. Default value 30
    bodyfile = "xxx"  -- Optional. Upload the file content directly as the body. The priority is higher than that of the body parameter.
}

local code, resp = httpplus.request({url="https://httpbin.air32.cn/get"})
log.info("http", code)
-- Description of the return value resp
-- Case 1, when code >= 100, resp will be a table containing 2 elements
if code >= 100 then
    -- headers, is a table
    log.info("http", "headers", json.encode(resp.headers))
    -- body, is a zbuff
    -- The query function can be used to convert string
    log.info("http", "headers", resp.body:query())
    -- It can also be forwarded through uart.tx and other functions that support zbuff.
    -- uart.tx(1, resp.body)
end

```

---

