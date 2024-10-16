# camera - Camera

{bdg-success}`Adapted` {bdg-primary}`Air780EP`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/camera/luat_lib_camera.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view the demo example of camera](https://gitee.com/openLuat/LuatOS/tree/master/demo/camera)
```

## Constant

|constant | type | explanation|
|-|-|-|
|camera.AUTO|number|Camera works in automatic mode|
|camera.SCAN|number|The camera works in scan mode and only outputs Y component|


## camera.init(InitReg_or_cspi_id, cspi_speed, mode, is_msb, rx_bit, seq_type, is_ddr, only_y, scan_mode, w, h)



Initialize camera

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|table/integer|If it is a table, see demo/camera/AIR105 for DVP camera configuration, and ignore subsequent parameters. If it is a number, it is the serial number of the camera spi bus.|
|int|camera spi Bus speed|
|int|camera spi Mode,0~3|
|int|Is the bit order of bytes msb,0 no 1 yes|
|int|Number of bits received simultaneously,1,2,4|
|int|byte Sequence,0~1|
|int|Double edge sampling configuration, 0 is not enabled, other values are determined according to actual SOC|
|int|Only receive Y component, 0 is not enabled, 1 is enabled, scan code must be enabled, otherwise it will fail.|
|int|Working mode, camera.AUTO, camera.SCAN scan code|
|int|Camera width|
|int|Camera height|

**Return Value**

|return value type | explanation|
|-|-|
|int/false|Return camera_id on success, return on failure false|

**Examples**

```lua
camera_id = camera.init(GC032A_InitReg)--screen output rgb image
--After initialization, start is required to start output/scan code
camera.start(camera_id)--Start the specified camera

```

---

## camera.on(id, event, func)



Register camera event callback

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|camera id, camera 0 Write 0, camera 1 write 1|
|string|Event Name|
|function|Callback Method|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
camera.on(0, "scanned", function(id, str)
--id int camera id
--str Various types of false cameras do not work normally, the true photo mode is successfully photographed and saved, the size of the data returned this time in int original data mode, and the decoded value after the code is successfully scanned in string scan mode.
    print(id, str)
end)

```

---

## camera.start(id)



Start the specified camera

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|camera id,For example 0|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success, otherwise false|

**Examples**

```lua
camera.start(0)

```

---

## camera.stop(id)



Stop the specified camera

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|camera id,For example 0|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success, otherwise false|

**Examples**

```lua
camera.stop(0)

```

---

## camera.close(id)



Close the specified camera and release the corresponding I/O resources.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|camera id,For example 0|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success, otherwise false|

**Examples**

```lua
camera.close(0)

```

---

## camera.capture(id, save_path, quality)



camera Take pictures

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|camera id,For example 0|
|string/zbuff/nil|save_path,The path to save the file. If it is empty, it is written in the last path. The default is/capture.jpg. If it is zbuff, the picture is saved in buff and not written to the file system.|
|int|quality, jpeg Compression quality, 1 worst, small footprint, 3 highest, largest footprint and time-consuming, default 1|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success, otherwise returns false, and the received length is called back through the callback function set by camera.on after the actual completion.|

**Examples**

```lua
camera.capture(0)

```

---

## camera.video(id, w, h, out_path)



camera Output video stream USB

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|camera id,For example 0|
|int|Width|
|int|Height|
|int|Output path, currently only virtual serial port can be used 0|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success, otherwise false|

**Examples**

```lua
camera.video(0, 320, 240, uart.VUART_0)

```

---

## camera.startRaw(id, w, h, buff)



Start camera to output the original data to the user's zbuff buffer, stop after outputting 1fps, and call back the received length through the callback function set by camera.on. if you need to output again, please call camera.getRaw

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|camera id,For example 0|
|int|Width|
|int|Height|
|zbuff|The buffer area used to store data. The size must be no less w X h X 2 byte|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success, otherwise false|

**Examples**

```lua
camera.startRaw(0, 320, 240, buff)

```

---

## camera.getRaw(id)



Start camera again to output the original data to the user's zbuff buffer, stop after outputting 1fps, and call back the received length through the callback function set by camera.on. if you need to output again, please continue to call this API

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|camera id,For example 0|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success, otherwise false|

**Examples**

```lua
camera.getRaw(0)

```

---

## camera.preview(id, onoff)



Start and stop camera preview function, directly output to LCD, only the SOC supported by hardware can run

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|camera id,For example 0|
|boolean|true On, false to stop|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success, otherwise false|

**Examples**

```lua
camera.preview(1, true)

```

---

