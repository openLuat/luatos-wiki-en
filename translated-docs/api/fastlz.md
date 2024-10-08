# fastlz - FastLZ Compression

{bdg-success}`Adapted` {bdg-primary}`Air780E` {bdg-primary}`Air780EP`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/fastlz/luat_lib_fastlz.c). If there is any error, please submit issue or help modify pr, thank you！
```


**Example**

```lua
-- Differences with the miniz library
-- The memory requirement is much smaller, miniz library is close to 200k, fastlz only needs 32k original data size
-- compression ratio, the compression ratio of miniz is better fastlz

-- Prepare the data
local bigdata = "123jfoiq4hlkfjbnasdilfhuqwo;hfashfp9qw38hrfaios;hfiuoaghfluaeisw"
-- compressed
local cdata = fastlz.compress(bigdata) 
-- lua The string of is equivalent to char[] with length, which can store all data including 0x 00
if cdata then
    -- Check data size before and after compression
    log.info("fastlz", "before", #bigdata, "after", #cdata)
    log.info("fastlz", "cdata as hex", cdata:toHex())

    -- Decompress to get the original text
    local udata = fastlz.uncompress(cdata)
    log.info("fastlz", "udata", udata)
end

```

## fastlz.compress(data, level)



fast compression

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|The data to be compressed. It is not recommended to compress data less than 400 bytes, and the compressed data cannot be larger 32k|
|int|Compression level, default 1, optional 1 or 2, 2 compression ratio is higher (sometimes)|

**Return Value**

|return value type | explanation|
|-|-|
|string|If the compression is successful, return the data string, otherwise return nil|

**Examples**

```lua
-- Note that the memory consumption of the compression process is as follows
-- System memory, fixed 32k
-- lua Memory, 1.05 times the size of the original data, minimum 1024 bytes.

```

---

## fastlz.uncompress(data, maxout)



Fast Decompression

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Data to be compressed|
|int|The maximum size after decompression, the default is 4k, can be adjusted as needed|

**Return Value**

|return value type | explanation|
|-|-|
|string|If the decompression is successful, return the data string, otherwise return nil|

**Examples**

None

---

