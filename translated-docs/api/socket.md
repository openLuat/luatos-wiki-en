# socket - Network Interface

{bdg-success}`Adapted` {bdg-primary}`Air780E` {bdg-primary}`Air780EP`

```{tip}
This library has its own demo,[click this link to view the demo example of socket](https://gitee.com/openLuat/LuatOS/tree/master/demo/socket)
```

## Constant

|constant | type | explanation|
|-|-|-|
|socket.ETH0|number|ETH0 with hardware stack, value is 5|
|socket.LWIP_ETH|number|An Ethernet card using the LWIP protocol stack, with a value 4|
|socket.LWIP_STA|number|WIFI STA using LWIP protocol stack, the value is 2|
|socket.LWIP_AP|number|WIFI AP using LWIP protocol stack, the value is 3|
|socket.LWIP_GP|number|Mobile cellular module using the LWIP protocol stack with a value 1|
|socket.USB|number|USB network card using LWIP protocol stack, value is 6|
|socket.LINK|number|LINK Event|
|socket.ON_LINE|number|ON_LINE Event|
|socket.EVENT|number|EVENT Event|
|socket.TX_OK|number|TX_OK Event|
|socket.CLOSED|number|CLOSED Event|


## socket.sntp(sntp_server)

{bdg-success}`This interface only supports` {bdg-primary}`Air780E` {bdg-primary}`Air780EP`

sntp Time synchronization

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string/table|sntp Server address optional|
|int|The serial number of the adapter can only be socket.ETH0 (external Ethernet),socket.LWIP_ETH (internal Ethernet),socket.LWIP_STA (STA with internal WIFI),socket.LWIP_AP (AP with internal WIFI),socket.LWIP_GP (GPRS with internal cellular network),socket.USB (external USB network card). if not filled in, the soc platform is preferred to have its own adapter capable of connecting to the external network. if there is still no adapter, select Last Registered Adapter|

**Return Value**

None

**Examples**

```lua
socket.sntp()
--socket.sntp("ntp.aliyun.com") --custom sntp server address
--socket.sntp({"ntp.aliyun.com","ntp1.aliyun.com","ntp2.aliyun.com"}) --sntp Custom Server Address
--socket.sntp(nil, socket.ETH0) --sntp Custom Adapter Serial Number
sys.subscribe("NTP_UPDATE", function()
    log.info("sntp", "time", os.date())
end)
sys.subscribe("NTP_ERROR", function()
    log.info("socket", "sntp error")
    socket.sntp()
end)

```

---

## socket.ntptm()



timestamp after the network pair (ms level)

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|table|Data containing time information|

**Examples**

```lua
-- This API was added on 2023.11.15
-- Note that this function is not valid until socket.sntp() is executed and the NTP time is obtained.
-- And it is the more accurate value after 2 sntp
-- The smaller the network fluctuation, the more stable the timestamp.
local tm = socket.ntptm()

-- The corresponding table contains multiple data, all of which are integer values.

-- Standard data
-- tsec Current seconds, starting from 1900.1.1 0:0:0, UTC time
-- tms  Current number of milliseconds
-- vaild Valid, true or nil

-- Debugging data, debugging, general users do not have to tube
-- ndelay Average network delay, in milliseconds
-- ssec The second offset of the system start time and 1900.1.1 0:0:0
-- sms The millisecond offset of the system startup time from 1900.1.1 0:0:0
-- lsec Local seconds counter, based on mcu.tick64()
-- lms Local milliseconds counter, based on mcu.tick64()

log.info("tm Data", json.encode(tm))
log.info("Timestamp", string.format("%u.%03d", tm.tsec, tm.tms))

```

---

## socket.sntp_port(port)



Set the port number of the SNTP server

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|port port number, default 123|

**Return Value**

|return value type | explanation|
|-|-|
|int|Returns the current port number|

**Examples**

```lua
-- This function was added in 2024.5.17
-- In most cases, you do not need to set the port number of the NTP server, and the default 123 is sufficient.

```

---

## socket.localIP(adapter)



Get Local ip

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|The serial number of the adapter can only be socket.ETH0 (external Ethernet),socket.LWIP_ETH (internal Ethernet),socket.LWIP_STA (STA with internal WIFI),socket.LWIP_AP (AP with internal WIFI),socket.LWIP_GP (GPRS with internal cellular network),socket.USB (external USB network card). if not filled in, the soc platform is preferred to have its own adapter capable of connecting to the external network. if there is still no adapter, select Last Registered Adapter|

**Return Value**

|return value type | explanation|
|-|-|
|string|It is usually an intranet ip or an extranet ip, depending on the operator's allocation.|
|string|Network Mask|
|string|Gateway IP|

**Examples**

```lua
sys.taskInit(function()
    while 1 do
        sys.wait(3000)
        log.info("socket", "ip", socket.localIP())
        -- Output example
        -- 62.39.244.10    255.255.255.255    0.0.0.0
    end
end)

```

---

## socket.create(adapter, cb)



Apply for one on an adapted network card.socket_ctrl

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|The serial number of the adapter can only be socket.ETH0 (external Ethernet),socket.LWIP_ETH (internal Ethernet),socket.LWIP_STA (STA with internal WIFI),socket.LWIP_AP (AP with internal WIFI),socket.LWIP_GP (GPRS with internal cellular network),socket.USB (external USB network card). if not filled in, the soc platform is preferred to have its own adapter capable of connecting to the external network. if there is still no adapter, select Last Registered Adapter|
|string|or function string is the taskName of the message notification, function it is a callback function, and if the firmware does not have a built-in sys_wait, it must be function|

**Return Value**

None

**Examples**

None

---

## socket.debug(ctrl, onoff)



Configure whether to open debug information

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|
|boolean|true Turn on the debug switch|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- Turn on debugging information, which is turned off by default.
socket.debug(ctrl, true)

```

---

## socket.config(ctrl, local_port, is_udp, is_tls, keep_idle, keep_interval, keep_cnt, server_cert, client_cert, client_key, client_password)



Configuration network some information，

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|
|int|The local port number is in small endian format. If it is not written, one will be automatically assigned. If the user fills in the port number, it needs to be less than 60000. By default, it is not written|
|boolean|Whether it is UDP, default false|
|boolean|Whether it is encrypted transmission, default false|
|int|tcp keep live The idle time (in seconds) in the mode. If it is left blank, it means that it is not enabled. If it is a network card that does not support the standard posix interface (such as W5500), it is the heartbeat interval.|
|int|tcp keep live The probing interval in the mode (seconds）|
|int|tcp keep live Number of probes in mode|
|string|TCP The server ca certificate data in the mode and PSK in the UDP mode do not need to be encrypted and transmitted to write nil, and all subsequent parameters are also required.nil|
|string|TCP client ca certificate data in mode, PSK-ID in udp mode, and if you do not need to verify the client certificate in tcp mode, ignore, generally do not need to verify the client certificate|
|string|TCP Client private key encrypts data in mode|
|string|TCP Client private key password data in the mode|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success and true on failure false|

**Examples**

```lua
--The most common TCP transport
socket.config(ctrl)
--The most common encrypted TCP transmission, the kind of certificate without verification
socket.config(ctrl, nil, nil ,true)

```

---

## socket.linkup(ctrl)



Wait for network card linkup

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|true No exception occurred, false failed, if false, you don't need to look at the next return value|
|boolean|true There is no linkup,false, and then you need to receive socket.LINK messages.|

**Examples**

```lua
-- Judge whether it is connected to the Internet.
local succ, result = socket.linkup(ctrl)

```

---

## socket.connect(ctrl, ip, remote_port, need_ipv6_dns)



Connect to the server as a client

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|
|string|or int ip Or domain name, if it is IPV4, it can be int value in big-end format.|
|int|Server port number, little-end format|
|boolean|Does domain name resolution need IPV6,true yes, false no, default false no, only protocol stacks that support IPV6 have effect|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|true No exception occurred, false failed, if false, you don't need to look at the next return value, if there is an exception, follow-up close|
|boolean|true It has been connect,false is not connect, and socket.ON_LINE message needs to be received later.|

**Examples**

```lua

local succ, result = socket.connect(ctrl, "netlab.luatos.com", 40123)

--[[
The code value of the common connection failure will be displayed in the log
-1 Low underlying memory
-3 Timeout
-8 Port already occupied
-11 Link not established
-13 Module Active Disconnect
-14 Server Active Disconnect
]]

```

---

## socket.discon(ctrl)



Disconnect as client

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|true No exception occurred, false failed, if false, you don't need to look at the next return value|
|boolean|true Disconnected, false not disconnected, then need to receive socket.CLOSED message|

**Examples**

```lua
local succ, result = socket.discon(ctrl)

```

---

## socket.close(ctrl)



Force Close socket

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## socket.tx(ctrl, data, ip, port, flag)



Send data to the opposite end, UDP should not send more than 1460 bytes at a time, otherwise it is easy to fail.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|
|string|or user_data zbuff  Data to send|
|string|or int The opposite IP is ignored if it is TCP application, and if it is UDP, the parameter at connect time is used if it is left blank. if it is IPV4, it can be int value in big-end format.|
|int|The opposite-end slogan and small-end format are ignored if it is TCP application, and if it is UDP, the parameters at connect time are used if it is left blank.|
|int|Send parameter, currently reserved, does not work|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|true No exception occurred, false failed, if false, you don't need to see the next return value, if false, follow-up close|
|boolean|true The buffer is full, false is not full, if true, you need to wait for a period of time or wait for the socket.TX_ OK message before trying to send, while ignoring the next return value|
|boolean|true A reply has been received, false has not received a reply, and then you need to receive the socket.TX_ OK message, or you can ignore and continue sending full==true|

**Examples**

```lua

local succ, full, result = socket.tx(ctrl, "123456", "xxx.xxx.xxx.xxx", xxxx)

```

---

## socket.rx(ctrl, buff, flag, limit)



Receive the data sent by the opposite end, note that the data has been cached at the bottom layer, use this function only to extract, UDP mode will only take out one data packet at a time

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|
|user_data|zbuff Store the received data, and automatically expand the capacity if the buffer is not enough.|
|int|Receiving parameter, currently reserved, does not work|
|int|Receive data length limit, if specified, only the first N bytes are taken. 2024.1.5 New|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|true No exception occurred, false failed, if false, you don't need to see the next return value, if false, follow-up close|
|int|Length of data received this time|
|string|The peer IP is meaningful only in UDP mode. TCP mode returns nil. Pay attention to the format of the return. If it is IPV4,1byte 0x 00 4byte address, if it is IPV6,1byte 0x 01 16byte address|
|int|The port on the opposite end, which is meaningful only in UDP mode and returned in TCP mode.0|

**Examples**

```lua
-- Read data from the socket, ctrl is returned by socket.create, please refer demo/socket
local buff = zbuff.create(2048)
local succ, data_len, remote_ip, remote_port = socket.rx(ctrl, buff)

-- Limit Read Length, 2024.1.5 New
-- Attention
-- If it is UDP data, if the limit is less than the length of the UDP packet, only the first limit bytes will be taken and the remaining data will be discarded.
-- If it is TCP data, if there is any remaining data, it will not be discarded and can continue to be read..
-- New EVENT data is generated only when new data arrives. Data that has not been read will not trigger a new EVENT event.
local succ, data_len, remote_ip, remote_port = socket.rx(ctrl, buff, 1500)

-- Read buffer size, added in 2024.1.5. Note that the old version of firmware will report an error if it does not pass buff parameters.
-- For TCP data, the total length of the data to be read is returned here.
-- For UDP data, the length of a single UDP packet is returned here
local succ, data_len = socket.rx(ctrl)
if succ then
    log.info("Length of data to be charged", data_len)
end

```

---

## socket.read(netc, len)



Read data (non-zbuff version)

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|socket.create obtained ctrl|
|int|Limit the length of read data, optional, not to pass is to read all|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Read success or not|
|string|Data read, valid only if read successfully|
|string|The IP address of the other party is valid only when the reading is successful and UDP communication is performed.|
|int|Opposite port, valid only for read success and UDP traffic|

**Examples**

```lua
-- This function was added in 2024.4.8 to easily read small data.
-- Please give priority to the socket.rx function, which is mainly used for flexible calls when the firmware does not contain the zbuff library.
local ok, data = socket.read(netc, 1500)
if ok and #data > 0 then
    log.info("Read data", data)
end

```

---

## socket.wait(ctrl)



Wait for a new socket message. After the connection is successful and the data is sent successfully, use one time to switch the network state to receiving new data.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|true No exception occurred, false failed, if false, you don't need to see the next return value, if false, follow-up close|
|boolean|true There is new data to receive, false no data, and then the socket.EVENT message needs to be received.|

**Examples**

```lua
local succ, result = socket.wait(ctrl)

```

---

## socket.listen(ctrl)



Start listening as a server

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|true No exception occurred, false failed, if false, you don't need to see the next return value, if false, follow-up close|
|boolean|true It has been connect,false is not connect, and socket.ON_LINE message needs to be received later.|

**Examples**

```lua
local succ, result = socket.listen(ctrl)

```

---

## socket.accept(ctrl)



As the server receives a new client, note that if the hardware protocol stack similar to W5500 does not support 1-to-many, the second parameter is not required.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create Get ctrl, here is the server side|
|string|or function or nil string is the taskName of the message notification, and the function is the callback function, which is the same as the socket.create parameter.|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|true No exception occurred, false failed, if false, you don't need to see the next return value, if false, follow-up close|
|user_data|or nil If 1-to-many is supported, a new ctrl will be returned, automatically create, if not supported, return nil|

**Examples**

```lua
local succ, new_netc = socket.listen(ctrl, cb)

```

---

## socket.state(ctrl)



Get the current state of the socket

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|

**Return Value**

|return value type | explanation|
|-|-|
|int|or nil,If the input parameter is correct, return the value of the status, otherwise return nil|
|string|or nil,If the input parameters are correct, return the Chinese description of the status, otherwise return nil|

**Examples**

```lua
local state, str = socket.state(ctrl)
log.info("state", state, str)
state    0    "Hardware Offline",
        1    "Offline",
        2    "Wait DNS",
        3    "Connecting",
        4    "TLS handshake in progress",
        5    "Online",
        6    "In listening",
        7    "Offline",
        8    "Unknown"

```

---

## socket.release(ctrl)



Active release network_ctrl

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|

**Return Value**

None

**Examples**

```lua
-- It can no longer be used after release.
socket.release(ctrl)

```

---

## socket.setDNS(adapter_index, dns_index, ip)



Set up the DNS server

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|The adapter serial number can only be socket.ETH0,socket.STA,socket.AP. If it is not filled in, the last registered adapter will be selected.|
|int|dns Server sequence number, starting from 1|
|string|or int dns，If it is IPV4, it can be an int value in big-end format.|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success and true on failure false|

**Examples**

```lua
-- Set the DNS configuration for the default network adapter
socket.setDNS(nil, 1, "114.114.114.114")
-- Set up DNS configuration for your network adapter
socket.setDNS(socket.ETH0, 1, "114.114.114.114")

```

---

## socket.sslLog(log_level)



Set log registration for SSL

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|mbedtls log Grade|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
--[[
SSL/TLS log Level Description
0 Don't Print
1 Print errors and alarms only
2 Mostly info
3 and more than 3 detailed debug

Too much information can cause memory fragmentation
]]
-- Print most info logs
socket.sslLog(2)

```

---

## socket.adapter(index)



Viewing the Networking Status of a NIC Adapter

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|The serial number of the adapter to be viewed can be left blank and all network cards will be viewed until IP READY is encountered. If the network card is specified, it can only be socket.ETH0 (external Ethernet),socket.LWIP_ETH (internal Ethernet),socket.LWIP_STA (STA with internal WIFI),socket.LWIP_AP (AP with internal WIFI),socket.LWIP_GP (GPRS with internal cellular network),socket.USB (external USB network card）|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Whether the checked adapter is IP READY,true indicates that it is ready for networking, false temporarily cannot be networked|
|int|Last viewed adapter serial number|

**Examples**

```lua
-- Check all network cards until you find one that is IP READY
local isReady,index = socket.adapter() --If the isReady is true, index is the NIC adapter serial number of IP READY
--Check if an external Ethernet (such as W5500)IP READY
local isReady,default = socket.adapter(socket.ETH0)

```

---

## socket.remoteIP(ctrl)



Get peer ip

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|user_data|socket.create obtained ctrl|

**Return Value**

|return value type | explanation|
|-|-|
|string|IP1，If the value is nil, the IP address is not obtained.|
|string|IP2，If nil, it means no IP2|
|string|IP3，If nil, it means no IP3|
|string|IP4，If nil, it means no IP4|

**Examples**

```lua
-- Note: It can only be obtained after receiving the socket.ON_LINE message, returning up to 4 IP。
-- socket.connect If the remote_port is set to 0, the socket.ON_LINE message will be returned when DNS is completed.
local ip1,ip2,ip3,ip4 = socket.remoteIP(ctrl)

```

---

