# rsa - RSA encryption and decryption

{bdg-success}`Adapted` {bdg-primary}`Air780E` {bdg-primary}`Air780EP` {bdg-primary}`Air780EPS`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/rsa/binding/luat_lib_rsa.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view the demo example of rsa](https://gitee.com/openLuat/LuatOS/tree/master/demo/rsa)
```

**Example**

```lua
-- Please generate the private key and public key on the computer. Currently, the maximum support is 4096bit. Generally speaking, 2048bit is enough.
-- openssl genrsa -out privkey.pem 2048
-- openssl rsa -in privkey.pem -pubout -out public.pem
-- privkey.pem is the private key and public.pem is the public key
-- The private key is used for encryption and signing, is usually kept secret, and is placed on the server side.
-- The public key is used for decryption and signature verification. Generally, it can be made public and placed on the device.

-- To demonstrate the use of the API, the private key is also placed on the device here.

local res = rsa.encrypt((io.readFile("/luadb/public.pem")), "abc")
-- Print Results
log.info("rsa", "encrypt", res and #res or 0, res and res:toHex() or "")

-- The following is decryption, usually not on the device side, here is mainly to demonstrate the usage, will be very slow
if res then
    -- Read the private key and then decode the data
    local dst = rsa.decrypt((io.readFile("/luadb/privkey.pem")), res, "")
    log.info("rsa", "decrypt", dst and #dst or 0, dst and dst:toHex() or "")
end

-- Demo Signature and Verification
local hash = crypto.sha1("1234567890"):fromHex()
-- Signing is usually slow and is usually done by the server.
local sig = rsa.sign((io.readFile("/luadb/privkey.pem")), rsa.MD_SHA1, hash, "")
log.info("rsa", "sign", sig and #sig or 0, sig and sig:toHex() or "")
if sig then
    -- The check is very fast.
    local ret = rsa.verify((io.readFile("/luadb/public.pem")), rsa.MD_SHA1, hash, sig)
    log.info("rsa", "verify", ret)
end

```

## rsa.encrypt(key, data)



RSA Encryption

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Public key data, only PEM format is supported|
|string|The data to be encrypted cannot exceed half of the number of bits of the public key. For example, a 2048-bit public key can only encrypt 128 bytes of data.|

**Return Value**

|return value type | explanation|
|-|-|
|string|The data after the encryption is successful, if it fails, it will be returned.nil|

**Examples**

```lua
-- "abc" in the following code is the data to be encrypted
local res = rsa.encrypt((io.readFile("/luadb/public.pem")), "abc")
-- Print Results
log.info("rsa", "encrypt", res and #res or 0, res and res:toHex() or "")

```

---

## rsa.decrypt(key, data, pwd)



RSA Decryption

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Private key data. Only PEM format is supported.|
|string|Data to be decoded|
|string|Password for the private key, optional|

**Return Value**

|return value type | explanation|
|-|-|
|string|The decrypted data. If the decrypted data fails, the data is returned.nil|

**Examples**

```lua
-- Note that decryption is usually slow and recommended on the server side
-- res is the data to be decoded
local dst = rsa.decrypt((io.readFile("/luadb/privkey.pem")), res, "")
log.info("rsa", "decrypt", dst and #dst or 0, dst and dst:toHex() or "")

```

---

## rsa.verify(key, md, hash, sig)



RSA Check and sign

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Public key data, only PEM format is supported|
|int|Signature patterns, such rsa.MD_SHA1 , rsa.MD_SHA256|
|string|hash Data, if it is a HEX string, remember to convert fromHex to binary data.|
|string|sig Data, if it is a HEX string, remember to convert fromHex to binary data.|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Valid return true, otherwise false, error return nil|

**Examples**

```lua
local ret = rsa.verify((io.readFile("/luadb/public.pem")), rsa.MD_SHA1, hash, sig)
log.info("rsa", "verify", ret)

```

---

## rsa.sign(key, md, hash, pwd)



RSA Signature

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Private key data. Only PEM format is supported.|
|int|Signature patterns, such rsa.MD_SHA1 , rsa.MD_SHA256|
|string|hash Data, if it is a HEX string, remember to convert fromHex to binary data.|
|string|Private key password, optional|

**Return Value**

|return value type | explanation|
|-|-|
|string|Returns the sig data successfully, otherwise nil|

**Examples**

```lua
local sig = rsa.sign((io.readFile("/luadb/privkey.pem")), rsa.MD_SHA1, hash, "")
log.info("rsa", "sign", sig and #sig or 0, sig and sig:toHex() or "")

```

---

