# iotcloud - iotcloud Cloud platform library (supported: tengxun cloud ariyun onenet huawei cloud graffiti cloud baidu cloud tlink cloud others will also support, and the useful issue will accelerate support  )

{bdg-secondary}`Adaptation status unknown`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../script/libs/iotcloud.lua). If there is any error, please submit issue or help modify pr, thank you！
```


**Example**

```lua
--Note: due to the use of sys.wait() all apis need to be used in the coroutine

    -- Tencent Cloud 
    -- dynamic registration
    -- iotcloudc = iotcloud.new(iotcloud.TENCENT,{produt_id = "xxx" ,product_secret = "xxx"})
    -- Key verification
    -- iotcloudc = iotcloud.new(iotcloud.TENCENT,{produt_id = "xxx",device_name = "123456789",device_secret = "xxx=="})
    -- Certificate verification
    -- iotcloudc = iotcloud.new(iotcloud.TENCENT,{produt_id = "xxx",device_name = "123456789"},{tls={client_cert=io.readFile("/luadb/client_cert.crt")}})

    -- Alibaba Cloud  
    -- One type, one secret (no pre-registration-only supported by enterprise version)
    -- iotcloudc = iotcloud.new(iotcloud.ALIYUN,{instance_id = "xxx",produt_id = "xxx",product_secret = "xxx"}) -- Enterprise Edition Public Instance
    -- One type, one secret (pre-registration)
    -- iotcloudc = iotcloud.new(iotcloud.ALIYUN,{produt_id = "xxx",device_name = "xxx",product_secret = "xxx"})                     -- Legacy Public Instance
    -- iotcloudc = iotcloud.new(iotcloud.ALIYUN,{instance_id = "xxx",produt_id = "xxx",device_name = "xxx",product_secret = "xxx"}) -- New Public Instance
    -- One machine, one secret (pre-registration)
    -- iotcloudc = iotcloud.new(iotcloud.ALIYUN,{produt_id = "xxx",device_name = "xxx",device_secret = "xxx"})                    -- Legacy Public Instance
    -- iotcloudc = iotcloud.new(iotcloud.ALIYUN,{instance_id = "xxx",produt_id = "xxx",device_name = "xxx",device_secret = "xxx"})-- New Public Instance


    -- ONENET Cloud
    -- dynamic registration
    -- iotcloudc = iotcloud.new(iotcloud.ONENET,{produt_id = "xxx",userid = "xxx",userkey = "xxx"})
    -- One type, one density
    -- iotcloudc = iotcloud.new(iotcloud.ONENET,{produt_id = "xxx",product_secret = "xxx"})
    -- One machine, one secret
    -- iotcloudc = iotcloud.new(iotcloud.ONENET,{produt_id = "xxx",device_name = "xxx",device_secret = "xxx"})

    -- HUAWEI CLOUD
    -- Dynamic registration (no pre-registration)
    -- iotcloudc = iotcloud.new(iotcloud.HUAWEI,{produt_id = "xxx",project_id = "xxx",endpoint = "xxx",
    --                         iam_username="xxx",iam_password="xxx",iam_domain="xxx"})
    -- Manual registration (pre-registration)
    -- iotcloudc = iotcloud.new(iotcloud.HUAWEI,{produt_id = "xxx",endpoint = "xxx",device_name = "xxx",device_secret = "xxx"})

    -- graffiti cloud 
    -- iotcloudc = iotcloud.new(iotcloud.TUYA,{device_name = "xxx",device_secret = "xxx"})

    -- Baiduyun 
    -- iotcloudc = iotcloud.new(iotcloud.BAIDU,{produt_id = "afadjlw",device_name = "test",device_secret = "BBDVsSRGefaknffT"})
    -- iotcloudc = iotcloud.new(iotcloud.BAIDU,{produt_id = "xxx",device_name = "xxx"},{tls={server_cert=io.readFile("/luadb/GlobalSign.cer"),client_cert=io.readFile("/luadb/client_cert"),client_key=io.readFile("/luadb/client_private_key")}})

    -- Tlink Cloud  
    -- iotcloudc = iotcloud.new(iotcloud.TLINK,{produt_id = "xxx",product_secret = "xxx",device_name = "xxx"})
    -- iotcloudc = iotcloud.new(iotcloud.TLINK,{produt_id = "xxx",product_secret = "xxx",device_name = "xxx"},{tls={client_cert=io.readFile("/luadb/client_cert.crt")}})


```

## Constant

|constant | type | explanation|
|-|-|-|
|iotcloud.TENCENT|string|Tencent Cloud|
|iotcloud.ALIYUN|string|Alibaba Cloud|
|iotcloud.ONENET|string|ONENET Cloud|
|iotcloud.HUAWEI|string|HUAWEI CLOUD|
|iotcloud.TUYA|string|graffiti cloud|
|iotcloud.BAIDU|string|Baiduyun|
|iotcloud.TLINK|string|Tlink Cloud|
|iotcloud.CONNECT|string|Connect to the server|
|iotcloud.SEND|string|Send Message|
|iotcloud.RECEIVE|string|Message received|
|iotcloud.DISCONNECT|string|Server Disconnected|
|iotcloud.OTA|string|ota Message|


## iotcloud.new(cloud,iot_config,connect_config)



Create a cloud platform object

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Cloud platform iotcloud.TENCENT: Tencent cloud iotcloud.ALIYUN: Alibaba cloud iotcloud.ONENET: China mobile cloud iotcloud.HUAWEI: Huawei cloud iotcloud.TUYA: graffiti cloud iotcloud.BAIDU: Baidu cloud iotcloud.TLINK: Tlink cloud|
|table|iot Cloud platform configuration, device_name: optional, default is imei, otherwise it is unique_id iot_config.product_id: product id (alibaba cloud is product key) iot_config.product_secret: product key, if there is this item, it is dynamically registered iot_config.device_secret: device secret key, if there is this item, it is secret key connection instance_id: public instance id, new version alibaba public user id userkey private registration dynamic user id: onuser id enuserid:|
|table|mqtt Configuration, host: optional, default is platform default host ip: optional, default is platform default ip tls: encryption, if there is this item is generally product certification keepalive: heartbeat time, unit s optional, default 240 autoreconn: automatic reconnection, number: reconnection time, unit ms /bool reconnection, default 3000ms optional, default not automatic reconnection|

**Return Value**

|return value type | explanation|
|-|-|
|table|Cloud Platform Objects|

**Examples**

```lua

    -- Tencent Cloud 
    -- dynamic registration
    -- iotcloudc = iotcloud.new(iotcloud.TENCENT,{produt_id = "xxx" ,product_secret = "xxx"})
    -- Key verification
    -- iotcloudc = iotcloud.new(iotcloud.TENCENT,{produt_id = "xxx",device_name = "123456789",device_secret = "xxx=="})
    -- Certificate verification
    -- iotcloudc = iotcloud.new(iotcloud.TENCENT,{produt_id = "xxx",device_name = "123456789"},{tls={client_cert=io.readFile("/luadb/client_cert.crt")}})

    -- Alibaba Cloud  
    -- One type, one secret (no pre-registration-only supported by enterprise version)
    -- iotcloudc = iotcloud.new(iotcloud.ALIYUN,{instance_id = "xxx",produt_id = "xxx",product_secret = "xxx"}) -- Enterprise Edition Public Instance
    -- One type, one secret (pre-registration)
    -- iotcloudc = iotcloud.new(iotcloud.ALIYUN,{produt_id = "xxx",device_name = "xxx",product_secret = "xxx"})                     -- Legacy Public Instance
    -- iotcloudc = iotcloud.new(iotcloud.ALIYUN,{instance_id = "xxx",produt_id = "xxx",device_name = "xxx",product_secret = "xxx"}) -- New Public Instance
    -- One machine, one secret (pre-registration)
    -- iotcloudc = iotcloud.new(iotcloud.ALIYUN,{produt_id = "xxx",device_name = "xxx",device_secret = "xxx"})                    -- Legacy Public Instance
    -- iotcloudc = iotcloud.new(iotcloud.ALIYUN,{instance_id = "xxx",produt_id = "xxx",device_name = "xxx",device_secret = "xxx"})-- New Public Instance

    -- ONENET Cloud
    -- dynamic registration
    -- iotcloudc = iotcloud.new(iotcloud.ONENET,{produt_id = "xxx",userid = "xxx",userkey = "xxx"})
    -- One type, one density
    -- iotcloudc = iotcloud.new(iotcloud.ONENET,{produt_id = "xxx",product_secret = "xxx"})
    -- One machine, one secret
    -- iotcloudc = iotcloud.new(iotcloud.ONENET,{produt_id = "xxx",device_name = "xxx",device_secret = "xxx"})

    -- HUAWEI CLOUD
    -- Dynamic registration (no pre-registration)
    -- iotcloudc = iotcloud.new(iotcloud.HUAWEI,{produt_id = "xxx",project_id = "xxx",endpoint = "xxx",
    --                         iam_username="xxx",iam_password="xxx",iam_domain="xxx"})
    -- Key verification (pre-registration)
    -- iotcloudc = iotcloud.new(iotcloud.HUAWEI,{produt_id = "xxx",endpoint = "xxx",device_name = "xxx",device_secret = "xxx"})

    -- graffiti cloud 
    -- iotcloudc = iotcloud.new(iotcloud.TUYA,{device_name = "xxx",device_secret = "xxx"})

    -- Baiduyun 
    -- iotcloudc = iotcloud.new(iotcloud.BAIDU,{produt_id = "afadjlw",device_name = "test",device_secret = "BBDVsSRGefaknffT"})
    -- iotcloudc = iotcloud.new(iotcloud.BAIDU,{produt_id = "xxx",device_name = "xxx"},{tls={server_cert=io.readFile("/luadb/GlobalSign.cer"),client_cert=io.readFile("/luadb/client_cert"),client_key=io.readFile("/luadb/client_private_key")}})

    -- Tlink Cloud  
    -- iotcloudc = iotcloud.new(iotcloud.TLINK,{produt_id = "xxx",product_secret = "xxx",device_name = "xxx"})
    -- iotcloudc = iotcloud.new(iotcloud.TLINK,{produt_id = "xxx",product_secret = "xxx",device_name = "xxx"},{tls={client_cert=io.readFile("/luadb/client_cert.crt")}})


```

---

## cloudc:connect()



Cloud Platform Connectivity

**Parameters**

None

**Return Value**

None

**Examples**

```lua
iotcloudc:connect()

```

---

## cloudc:disconnect()



Cloud platform disconnected

**Parameters**

None

**Return Value**

None

**Examples**

```lua
iotcloudc:disconnect()

```

---

## cloudc:subscribe(topic, qos)



Cloud Platform Subscription

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string/table|Theme|
|number|topic 0/1/2 Default 0|

**Return Value**

None

**Examples**

None

---

## cloudc:unsubscribe(topic)



Cloud platform unsubscribe

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string/table|Theme|

**Return Value**

None

**Examples**

None

---

## cloudc:publish(topic,data,qos,retain)



Cloud platform release

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string/table|Theme|
|string|Message, required, but length can be 0|
|number|Message Level 0/1 Default 0|
|number|Archive, 0/1, default 0|

**Return Value**

None

**Examples**

None

---

## cloudc:close()



Cloud platform shutdown

**Parameters**

None

**Return Value**

None

**Examples**

```lua
iotcloudc:close()

```

---

