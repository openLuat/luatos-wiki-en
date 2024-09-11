# iconv - iconv Operation

{bdg-success}`Adapted` {bdg-primary}`Air780E` {bdg-primary}`Air780EP` {bdg-primary}`Air780EPS`

```{tip}
This library has its own demo,[click this link to view the demo example of iconv](https://gitee.com/openLuat/LuatOS/tree/master/demo/iconv)
```

## iconv.open(tocode, fromcode)



Open the corresponding character encoding conversion function

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Interpretation: target encoding format <br> value：gb2312/ucs2/ucs2be/utf8|
|string|Interpretation: source encoding format <br> value：gb2312/ucs2/ucs2be/utf8|

**Return Value**

|return value type | explanation|
|-|-|
|userdata|The conversion handle of the encoding conversion function, which returns if it does not exist.nil|

**Examples**

```lua
--unicode Big Endian Code Converts to utf8 Code
local iconv = iconv.open("utf8", "ucs2be")

```

---

## iconv:iconv(inbuf)



Character encoding conversion

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Interpretation: String to be converted|

**Return Value**

|return value type | explanation|
|-|-|
|number|Interpretation: return the result after code conversion <br> value: 0 success,-1 failure|

**Examples**

```lua
--unicode Big Endian Code Converts to utf8 Code
function ucs2beToUtf8(ucs2s)
    local iconv = iconv.open("utf8", "ucs2be")
    return iconv:iconv(ucs2s)
end

```

---

## iconv.open(tocode, fromcode) 



Open the corresponding character encoding conversion function

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|tocode$Target Encoding Format$gb2312/ucs2/ucs2be/utf8|
|string|fromcode$Source encoding format$gb2312/ucs2/ucs2be/utf8|
|return|table$cd$The conversion handle of the encoding conversion function.$ |

**Return Value**

None

**Examples**

```lua
--unicode Big Endian Code Converts to utf8 Code
local cd = iconv.open("utf8", "ucs2be")

```

---

## cd:iconv(inbuf) 



Character encoding conversion

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|inbuf$Enter the string $for example:ucs2s |
|return|number$result$Returns the result after code conversion $0 succeeded,-1 failed|

**Return Value**

None

**Examples**

```lua
--unicode Big Endian Code Converts to utf8 Code
function ucs2beToUtf8(ucs2s)
    local cd = iconv.open("utf8", "ucs2be")
    return cd:iconv(ucs2s)
end

```

---

## iconv.close(cd) 



Turn off character encoding conversion

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|cd$iconv.open Handle returned$ |
|return| |

**Return Value**

None

**Examples**

```lua
--Turn off character encoding conversion
local cd = iconv.open("utf8", "ucs2be")
iconv.close(cd)

```

---

