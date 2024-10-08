# http - http Client

{bdg-success}`Adapted` {bdg-primary}`Air780E` {bdg-primary}`Air780EP`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/network/libhttp/luat_lib_http.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view http demo examples](https://gitee.com/openLuat/LuatOS/tree/master/demo/http)
```

**Example**

```lua
-- To use the http library, you need to introduce the sysplus library and use it in the task.
require "sys"
require "sysplus"

sys.taskInit(function()
    sys.wait(1000)
    local code,headers,body = http.request("GET", "http://www.example.com/abc").wait()
    log.info("http", code, body)
end)


```

## http.request(method,url,headers,body,opts,ca_file,client_ca, client_key, client_password)



http Client

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|The request method, which supports legitimate HTTP methods such as GET and POST.|
|string|url Address, support http and https, support domain name, support custom port|
|tabal|The request header is optional such {["Content-Type"] = "application/x-www-form-urlencoded"}|
|string/zbuff|body Optional|
|table|Additional configuration options include timeout: timeout unit ms is optional, default is 10 minutes, write 0 is permanent wait dst: download path, optional adapter: select to use network card, optional debug: whether to open debug information, optional, ipv6: whether to ipv6 default is not, optional callback: download callback function, parameter content_len: total length body_len: userdata user parameter by download length, optional userdata: callback custom parameter  |
|string|server ca certificate data, optional, generally not required|
|string|client ca certificate data, optional, generally not required, two-way https authentication is required|
|string|client private key encryption data, optional, generally not required, two-way https authentication is required|
|string|Client private key password data, optional, generally not required, two-way https authentication is required.|

**Return Value**

|return value type | explanation|
|-|-|
|int|code , The value of server feedback is> = 100, the most common is 200. If it is a low-level error, such as connection failure, the return value is less 0|
|tabal|headers When 100, the header data returned on behalf of the server |
|string/int|body The content string of the server response, or the file size if it is in download mode|

**Examples**

```lua

--[[
code Error reporting information list:
-1 HTTP_ERROR_STATE Error status, usually a bottom-level exception, please report issue
-2 HTTP_ERROR_HEADER Incorrect response header, usually a server problem
-3 HTTP_ERROR_BODY Bad response body, usually a server problem
-4 HTTP_ERROR_CONNECT Failed to connect to server, not networked, address error, domain name error
-5 HTTP_ERROR_CLOSE Disconnected prematurely, network or server issues
-6 HTTP_ERROR_RX Receive data error, network problem
-7 HTTP_ERROR_DOWNLOAD Error reported during file download, network problem or download path problem
-8 HTTP_ERROR_TIMEOUT Timeouts, including connection timeouts, read data timeouts
-9 HTTP_ERROR_FOTA fota The function reports an error, usually because the update package is illegal.
]]

-- GET Request
local code, headers, body = http.request("GET","http://site0.cn/api/httptest/simple/time").wait()
log.info("http.get", code, headers, body)
-- POST Request
local code, headers, body = http.request("POST","http://httpbin.com/post", {}, "abc=123").wait()
log.info("http.post", code, headers, body)

-- GET request, but download to file
local code, headers, body = http.request("GET","http://httpbin.com/", {}, "", {dst="/data.bin"}).wait()
log.info("http.get", code, headers, body)

-- Custom Timeout, 5000ms
http.request("GET","http://httpbin.com/", nil, nil, {timeout=5000}).wait()

```

---

