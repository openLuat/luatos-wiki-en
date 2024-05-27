# iotauth - IoT Authentication library, used to generate parameters for various cloud platforms

{bdg-success}`Adapted` {bdg-primary}`Air780E/Air700E` {bdg-primary}`Air780EP/Air780EPV` {bdg-primary}`Air601` {bdg-primary}`Air101/Air103` {bdg-primary}`Air105` {bdg-primary}`ESP32C3` {bdg-primary}`ESP32S3`

```{tip}
This library has its own demo,[click this link to view iotauth demo examples](https://gitee.com/openLuat/LuatOS/tree/master/demo/iotauth)
```

## iotauth.aliyun(product_key, device_name,device_secret,method,cur_timestamp)



Alibaba Cloud IoT Platform Triple Generation

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|product_key|
|string|device_name|
|string|device_secret|
|string|method Encryption method, "hmacmd5", "hmacsha1", "hmacsha256" optional, default"hmacmd5"|
|number|cur_timestamp The optional default is 32472115200(2999-01-01 0:0:0)|
|bool|istls TLS direct connection true:TLS direct connection false:TCP direct connection mode Default TCP direct connection mode|

**Return Value**

|return value type | explanation|
|-|-|
|string|mqtt triplet client_id|
|string|mqtt triplet user_name|
|string|mqtt triplet password|

**Examples**

```lua
local client_id,user_name,password = iotauth.aliyun("123456789","abcdefg","Y877Bgo8X5owd3lcB5wWDjryNPoB")
print(client_id,user_name,password)

```

---

## iotauth.onenet(produt_id, device_name,key,method,cur_timestamp,version)



China Mobile Internet of Things Platform Triple Generation

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|produt_id Products id|
|string|device_name Equipment Name|
|string|key  device key or the project's acess_key|
|string|method Encryption mode, optional "md5" "sha1" "sha256", default"md5"|
|number|Time stamp, no need to fill in|
|string|version Optional Default"2018-10-31"|
|string|When key is access_key, fill in "products/" .. product_id. This parameter is added in 2024.1.29|

**Return Value**

|return value type | explanation|
|-|-|
|string|mqtt triplet client_id|
|string|mqtt triplet user_name|
|string|mqtt triplet password|

**Examples**

```lua
-- OneNet Platform official website: https://open.iot.10086.cn/
-- OneNet There are many versions. Pay attention to the distinction. Generally speaking, produt_id pure numbers are the old version, otherwise they are the new version.

-- In the new OneNET platform, the product id is an English letter string.
-- Corresponding demo/onenet/studio
local produt_id = "Ck2AF9QD2K"
local device_name = "test"
local device_key = "KuF3NT/jUBJ62LNBB/A8XZA9CqS3Cu79B/ABmfA1UCw="
local client_id,user_name,password = iotauth.onenet(produt_id, device_name, device_key)
log.info("onenet.new", client_id,user_name,password)

-- In the old version of OneNET platform, the product id is a numeric string. New in 2024.1.29
-- Corresponding demo/onenet/old_mqtt
local produt_id = "12342334"
local device_name = "test"
local access_key = "adfasdfadsfadsf="
local client_id,user_name,password = iotauth.onenet(produt_id, device_name, access_key, nil, nil, nil, "products/" .. produt_id)
log.info("onenet.old", client_id,user_name,password)


```

---

## iotauth.iotda(device_id,device_secret,cur_timestamp)



Huawei IoT Platform Triple Generation

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|device_id|
|string|device_secret|
|number|cur_timestamp Optional If it is not filled in, the timestamp will not be verified.|

**Return Value**

|return value type | explanation|
|-|-|
|string|mqtt triplet client_id|
|string|mqtt triplet user_name|
|string|mqtt triplet password|

**Examples**

```lua
local client_id,user_name,password = iotauth.iotda("6203cc94c7fb24029b110408_88888888","123456789")
print(client_id,user_name,password)

```

---

## iotauth.qcloud(product_id, device_name,device_secret,method,cur_timestamp,sdk_appid)



Tencent Networking Platform Triple Generation

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|the product id, which can be viewed after the project is created, similar LD8S5J1L07|
|string|Device name, such as the device's imei number|
|string|The device key, after creating the device, view the device details to obtain|
|string|method Encryption method, "sha1" "sha256" optional, default"sha256"|
|number|cur_timestamp The optional default is 32472115200(2999-01-01 0:0:0)|
|string|sdk_appid The optional default is"12010126"|

**Return Value**

|return value type | explanation|
|-|-|
|string|mqtt triplet client_id|
|string|mqtt triplet user_name|
|string|mqtt triplet password|

**Examples**

```lua
local client_id,user_name,password = iotauth.qcloud("LD8S5J1L07","test","acyv3QDJrRa0fW5UE58KnQ==")
print(client_id,user_name,password)

```

---

## iotauth.tuya(device_id,device_secret,cur_timestamp)



Three-tuple generation for graffiti networking platform

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|device_id|
|string|device_secret|
|number|cur_timestamp Optional Default 7258089600(2200-01-01 0:0:0)|

**Return Value**

|return value type | explanation|
|-|-|
|string|mqtt triplet client_id|
|string|mqtt triplet user_name|
|string|mqtt triplet password|

**Examples**

```lua
local client_id,user_name,password = iotauth.tuya("6c95875d0f5ba69607nzfl","fb803786602df760")
print(client_id,user_name,password)

```

---

## iotauth.baidu(iot_core_id, device_key,device_secret,method,cur_timestamp)



Baidu Internet of Things Platform Triple Generation

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|iot_core_id|
|string|device_key|
|string|device_secret|
|string|method Encryption method, "MD5" "SHA256" optional, default"MD5"|
|number|cur_timestamp Optional If it is not filled in, the timestamp will not be verified.|

**Return Value**

|return value type | explanation|
|-|-|
|string|mqtt triplet client_id|
|string|mqtt triplet user_name|
|string|mqtt triplet password|

**Examples**

```lua
local client_id,user_name,password = iotauth.baidu("abcd123","mydevice","ImSeCrEt0I1M2jkl")
print(client_id,user_name,password)

```

---

