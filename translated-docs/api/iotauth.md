# iotauth - IoT Authentication library, used to generate parameters for various cloud platforms

{bdg-success}`Adapted` {bdg-primary}`Air101/Air103` {bdg-primary}`Air601` {bdg-primary}`Air105` {bdg-primary}`ESP32C3` {bdg-primary}`ESP32S3` {bdg-primary}`Air780E/Air700E` {bdg-primary}`Air780EP`

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
|string|produt_id|
|string|device_name|
|string|key|
|string|method Encryption mode, optional "md5" "sha1" "sha256", default"md5"|
|number|cur_timestamp The optional default is 32472115200(2999-01-01 0:0:0)|
|string|version Optional Default"2018-10-31"|

**Return Value**

|return value type | explanation|
|-|-|
|string|mqtt triplet client_id|
|string|mqtt triplet user_name|
|string|mqtt triplet password|

**Examples**

```lua
local client_id,user_name,password = iotauth.onenet("123456789","test","KuF3NT/jUBJ62LNBB/A8XZA9CqS3Cu79B/ABmfA1UCw=")
print(client_id,user_name,password)

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

