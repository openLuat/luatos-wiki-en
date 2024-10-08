# lcd - lcd Drive module

{bdg-success}`Adapted` {bdg-primary}`Air780E` {bdg-primary}`Air780EP` {bdg-primary}`Air201`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/ LCD /luat_lib_ LCD .c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view the demo example of LCD](https://gitee.com/openLuat/LuatOS/tree/master/demo/lcd)
```

## Constant

|constant | type | explanation|
|-|-|-|
|lcd.font_opposansm12|font|12 Font|
|lcd.font_unifont_t_symbols|font|Symbol Font|
|lcd.font_open_iconic_weather_6x_t|font|Weather Font|
|lcd.font_opposansm16|font|16 Font|
|lcd.font_opposansm18|font|18 Font|
|lcd.font_opposansm20|font|20 Font|
|lcd.font_opposansm22|font|22 Font|
|lcd.font_opposansm24|font|24 Font|
|lcd.font_opposansm32|font|32 Font|
|lcd.font_opposansm12_chinese|font|12 Chinese font|
|lcd.font_opposansm14_chinese|font|14 Chinese font|
|lcd.font_opposansm16_chinese|font|16 Chinese font|
|lcd.font_opposansm18_chinese|font|18 Chinese font|
|lcd.font_opposansm20_chinese|font|20 Chinese font|
|lcd.font_opposansm22_chinese|font|22 Chinese font|
|lcd.font_opposansm24_chinese|font|24 Chinese font|
|lcd.font_opposansm32_chinese|font|32 Chinese font|
|lcd.direction_0|int|0°Direction command|
|lcd.direction_90|int|90°Direction command|
|lcd.direction_180|int|180°Direction command|
|lcd.direction_270|int|270°Direction command|
|lcd.HWID_0|hardware LCD driver id0 | (selected according to chip support)|
|lcd.WIRE_3_BIT_9_INTERFACE_I|3-wire spi | 9bit mode I|
|lcd.WIRE_4_BIT_8_INTERFACE_I|four-wire spi | 8bit mode I|
|lcd.WIRE_3_BIT_9_INTERFACE_II|3-wire spi | 9bit mode II|
|lcd.WIRE_4_BIT_8_INTERFACE_II|four-wire spi | 8bit mode II|
|lcd.DATA_2_LANE|int|Dual Channel Mode|


## lcd.init(tp, args, spi_dev, init_in_service)



lcd Display initialization

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|lcd types, currently supported：<br>st7796<br>st7789<br>st7735<br>st7735v<br>st7735s<br>gc9a01<br>gc9106l<br>gc9306x<br>ili9486<br>custom|
|table|Additional parameters are related to specific equipment:<br>pin_pwr (backlight) is optional, and <br>port:spi port can not be set, such as 0,1,2... if it is device mode, it is "device"<br>pin_dc: LCD data/command selection pin <br>pin_rst: LCD reset pin <br>pin_pwr: LCD backlight pin is optional, it is not necessary to set <br>direction: LCD screen direction 0:0 ° 1:180 ° 2:270 ° 3:90 °<br>w: LCD horizontal resolution <br>h: LCD vertical resolution <br>xoffset:x offset (there will be differences in different screen directions for different screen ic) <br>yoffset:y offset (there will be differences between different screen directions of different screen ic's) <br>direction0:0 ° direction command, (there will be differences between different screen ic's) <br>direction90:90 ° direction command, (there will be differences between different screen ic's) <br>direction180:180 ° direction command, (there will be differences between different screen ic's) <br>direction270:270 ° direction command, (different screen ic will have differences) <br>sleepcmd: sleep command, default 0X10<br>wakecmd: wake-up command, default 0X11 <br>interface_mode LCD mode, default lcd.WIRE_4_BIT_8_INTERFACE_I|
|userdata|spi Device, valid when port = "device"|
|boolean|Allow initialization to run in the LCD service, the default is false|

**Return Value**

None

**Examples**

```lua
-- st7735s for initializing spi0 Note: LCD needs to be initialized before initialization spi
spi_lcd = spi.deviceSetup(0,20,0,0,8,2000000,spi.MSB,1,1)
log.info("lcd.init",
lcd.init("st7735s",{port = "device",pin_dc = 17, pin_pwr = 7,pin_rst = 19,direction = 2,w = 160,h = 80,xoffset = 1,yoffset = 26},spi_lcd))

```

---

## lcd.close()



Turn off the LCD display

**Parameters**

None

**Return Value**

None

**Examples**

```lua
-- Close lcd
lcd.close()

```

---

## lcd.on()



Turn on the LCD display backlight

**Parameters**

None

**Return Value**

None

**Examples**

```lua
-- Turn on the LCD display backlight
lcd.on()

```

---

## lcd.off()



turn off LCD display backlight

**Parameters**

None

**Return Value**

None

**Examples**

```lua
-- turn off LCD display backlight
lcd.off()

```

---

## lcd.sleep()



lcd Sleep

**Parameters**

None

**Return Value**

None

**Examples**

```lua
-- lcd Sleep
lcd.sleep()

```

---

## lcd.wakeup()



lcd Wake up

**Parameters**

None

**Return Value**

None

**Examples**

```lua
-- lcd Wake up
lcd.wakeup()

```

---

## lcd.invon()



lcd anti-apparent

**Parameters**

None

**Return Value**

None

**Examples**

```lua
-- lcd anti-apparent
lcd.invon()

```

---

## lcd.invoff()



lcd Reverse Display Off

**Parameters**

None

**Return Value**

None

**Examples**

```lua
-- lcd Reverse Display Off
lcd.invoff()

```

---

## lcd.cmd(cmd)



lcd Command

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|cmd|

**Return Value**

None

**Examples**

```lua
-- lcd Command
lcd.cmd(0x21)

```

---

## lcd.data(data)



lcd Data

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|data|

**Return Value**

None

**Examples**

```lua
-- lcd Data
lcd.data(0x21)

```

---

## lcd.setColor(back,fore)



lcd Color Settings

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Background Color|
|int|Foreground Color|

**Return Value**

None

**Examples**

```lua
-- lcd Color Settings
lcd.setColor(0xFFFF,0x0000)

```

---

## lcd.draw(x1, y1, x2, y2,color)



lcd Color Fill

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|X position of upper left edge.|
|int|Y position of upper left edge.|
|int|X position of bottom right edge.|
|int|Y position of bottom right edge.|
|string|String or zbuff object|

**Return Value**

None

**Examples**

```lua
-- lcd Color Fill
local buff = zbuff.create({201,1,16},0x001F)
lcd.draw(20,30,220,30,buff)

```

---

## lcd.clear(color)



lcd Clear screen

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Screen color optional parameter, default background color|

**Return Value**

None

**Examples**

```lua
-- lcd Clear screen
lcd.clear()

```

---

## lcd.fill(x1, y1, x2, y2,color)



lcd Color Fill

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|X position of upper left edge.|
|int|Y position of upper left edge.|
|int|X position of lower right edge, excluding|
|int|Y position of lower right edge, excluding|
|int|Paint color optional parameter, default background color|

**Return Value**

None

**Examples**

```lua
-- lcd Color Fill
lcd.fill(20,30,220,30,0x0000)

```

---

## lcd.drawPoint(x0,y0,color)



draw a dot.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|X position of the point.|
|int|Y position of the point.|
|int|Paint color optional parameter, default foreground color|

**Return Value**

None

**Examples**

```lua
lcd.drawPoint(20,30,0x001F)

```

---

## lcd.drawLine(x0,y0,x1,y1,color)



draw a line between two points.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|X position of the first point.|
|int|Y position of the first point.|
|int|X position of the second point.|
|int|Y position of second point.|
|int|Paint color optional parameter, default foreground color|

**Return Value**

None

**Examples**

```lua
lcd.drawLine(20,30,220,30,0x001F)

```

---

## lcd.drawRectangle(x0,y0,x1,y1,color)



Draw a box starting at the x / y position (top left edge)

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|X position of upper left edge.|
|int|Y position of upper left edge.|
|int|X position of bottom right edge.|
|int|Y position of bottom right edge.|
|int|Paint color optional parameter, default foreground color|

**Return Value**

None

**Examples**

```lua
lcd.drawRectangle(20,40,220,80,0x001F)

```

---

## lcd.drawCircle(x0,y0,r,color)



Draw a circle from the x / y position (center)

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|X position of center of circle.|
|int|Y position of center of circle.|
|int|Radius.|
|int|Paint color optional parameter, default foreground color|

**Return Value**

None

**Examples**

```lua
lcd.drawCircle(120,120,20,0x001F)

```

---

## lcd.drawQrcode(x, y, str, size)



Buffer Drawing QRCode

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|x Coordinates|
|int|y Coordinates|
|string|The content of the two-dimensional code|
|int|Display size (note: the generated size of the two-dimensional code is related to the content to be displayed and the error correction level. the generated version is an indefinite size of 1-40 (corresponding to 21x21-177x177). if the size is different from the set size, the two-dimensional code will be automatically displayed in the middle of the specified area. if the two-dimensional code is not displayed, please check the log prompt.)|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## lcd.setFont(font, indentation)



Set Font

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|font lcd.font_XXX Please refer to the Constant Table|
|int|indentation, the right side of ascii in a constant-width font is indented by 0 to 127 pixels. ascii characters in a constant-width font may have a large blank space on the right side. you can delete some of them. If it is left blank or exceeds 127, the right half will be deleted directly. Non-equal-width fonts are invalid.|

**Return Value**

None

**Examples**

```lua
-- Set to font, valid for subsequent drawStr, be sure to set before calling LCD. drawStr

-- If "only font pointer is allow" is prompted, it means that the current firmware does not contain the corresponding font and can be customized free of charge by using the cloud compilation service.
-- Cloud Compilation Document: https://wiki.luatos.org/develop/compile/Cloud_compilation.html

-- lcd The default fonts for the library all start with LCD. font_
lcd.setFont(lcd.font_opposansm12)
lcd.drawStr(40,10,"drawStr")
sys.wait(2000)
lcd.setFont(lcd.font_opposansm12_chinese) -- For specific values, please refer to the constant table in the api document.
lcd.drawStr(40,40,"drawStr Test")

```

---

## lcd.drawStr(x,y,str,fg_color)



Display String

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|x abscissa|
|int|y Note: This (x,y) is the bottom left starting coordinate|
|string|str Document Content|
|int|fg_color str Note: This parameter is optional. If it is not filled in, the previously set color will be used. Drawing will only draw the font part, and the background needs to be cleared by itself.|

**Return Value**

None

**Examples**

```lua
-- Set it to Chinese font before display, which is valid for subsequent drawStr.
lcd.setFont(lcd.font_opposansm12)
lcd.drawStr(40,10,"drawStr")
sys.wait(2000)
lcd.setFont(lcd.font_opposansm16_chinese)
lcd.drawStr(40,40,"drawStr Test")

```

---

## lcd.drawGtfontGb2312(str,size,x,y)



Using gtfont to display gb2312 strings

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|str Display String|
|int|size Font size (supports 16-192 size fonts)|
|int|x abscissa|
|int|y Vertical Coordinates|

**Return Value**

None

**Examples**

```lua
-- Note that gtfont is an additional font chip hardware that needs to be attached to the SPI bus to call this function's
lcd.drawGtfontGb2312("Ah ah ah",32,0,0)

```

---

## lcd.drawGtfontGb2312Gray(str,size,gray,x,y)



Use gtfont to display gb2312 strings in grayscale

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|str Display String|
|int|size Font size (supports 16-192 size fonts)|
|int|gray Gray scale [1st order/2nd order/3rd order/4th order]|
|int|x abscissa|
|int|y Vertical Coordinates|

**Return Value**

None

**Examples**

```lua
-- Note that gtfont is an additional font chip hardware that needs to be attached to the SPI bus to call this function's
lcd.drawGtfontGb2312Gray("Ah ah ah",32,4,0,40)

```

---

## lcd.drawGtfontUtf8(str,size,x,y)



Using gtfont to display UTF8 strings

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|str Display String|
|int|size Font size (supports 16-192 size fonts)|
|int|x abscissa|
|int|y Vertical Coordinates|

**Return Value**

None

**Examples**

```lua
lcd.drawGtfontUtf8("Ah ah ah",32,0,0)

```

---

## lcd.drawGtfontUtf8Gray(str,size,gray,x,y)



Display UTF8 strings with gtfont grayscale

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|str Display String|
|int|size Font size (supports 16-192 size fonts)|
|int|gray Gray scale [1st order/2nd order/3rd order/4th order]|
|int|x abscissa|
|int|y Vertical Coordinates|

**Return Value**

None

**Examples**

```lua
lcd.drawGtfontUtf8Gray("Ah ah ah",32,4,0,40)

```

---

## lcd.getSize()



Get screen size

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|int|wide, if not initialized will return 0|
|int|High, if not initialized will return 0|

**Examples**

```lua
log.info("lcd", "size", lcd.getSize())

```

---

## lcd.drawXbm(x, y, w, h, data)



Draw Bitmap

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|X Coordinates|
|int|y Coordinates|
|int|bitmap width|
|int|Bitmap Height|
|int|Bitmap data, each bit representing a pixel|

**Return Value**

None

**Examples**

```lua
-- Using PCtoLCD2002 software, the negative code can be reversed line by line.
-- In (0,0) for the upper left corner, draw a 16x 16 "today" bitmap
lcd.drawXbm(0, 0, 16,16, string.char(
    0x80,0x00,0x80,0x00,0x40,0x01,0x20,0x02,0x10,0x04,0x48,0x08,0x84,0x10,0x83,0x60,
    0x00,0x00,0xF8,0x0F,0x00,0x08,0x00,0x04,0x00,0x04,0x00,0x02,0x00,0x01,0x80,0x00
))

```

---

## lcd.showImage(x, y, file)



Display pictures, currently only supported.jpg,jpeg

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|X Coordinates|
|int|y Coordinates|
|string|File Path|

**Return Value**

None

**Examples**

```lua
lcd.showImage(0,0,"/luadb/logo.jpg")

```

---

## lcd.flush()



Actively refresh data to the interface, only set buff and disable automatic properties after using

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise nil/false|

**Examples**

```lua
-- This API works with LCD. setupBuff LCD. autoFlush
lcd.flush()

```

---

## lcd.setupBuff(conf, onheap)



Set the display buffer, the required memory size is 2 x width x height bytes. Please measure the memory requirements and the refresh frequency required by the business..

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|conf Pointer, no need to pass|
|bool|true Use heap memory, false use vm memory, default use vm memory, do not need to actively pass|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Success or not|

**Examples**

```lua
-- Initialize the buff buffer of LCD, which can be understood as the FrameBuffer area.
lcd.setupBuff()

```

---

## lcd.autoFlush(enable)



Set automatic refresh, need to cooperate with LCD. setupBuff use

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|bool|Whether to refresh automatically. The default value is true|

**Return Value**

None

**Examples**

```lua
-- Set buff and disable automatic updates
lcd.setupBuff()
lcd.autoFlush(false)
-- After disabling automatic update, you need to use LCD. flush() to actively refresh data to the screen

```

---

## lcd.rgb565(r, g, b, swap)



RGB565 Color Generation

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Red, 0x00 ~ 0xFF|
|int|Green, 0x00 ~ 0xFF|
|int|Blue, 0x00 ~ 0xFF|
|bool|Whether to flip, true flip, false not flip. Default flip|

**Return Value**

|return value type | explanation|
|-|-|
|int|Color value|

**Examples**

```lua
-- This API supports multiple modes, and the number of parameters is 1, 2, 3, 4
-- 1. Single parameter form, 24bit RGB value, swap = true, recommend
local red =   lcd.rgb565(0xFF0000)
local green = lcd.rgb565(0x00FF00)
local blue =  lcd.rgb565(0x0000FF)

-- 2. Two-parameter form, 24bit RGB value, increase the setting of swap
local red =   lcd.rgb565(0xFF0000, true)
local green = lcd.rgb565(0x00FF00, true)
local blue =  lcd.rgb565(0x0000FF, true)

-- 3. Three-parameter form, red/green/blue, each 8bit
local red = lcd.rgb565(0xFF, 0x00, 0x00)
local green = lcd.rgb565(0x00, 0xFF, 0x00)
local blue = lcd.rgb565(0x00, 0x00, 0xFF)

-- 4. Four-parameter form, red/green/blue, each 8bit, increase the setting of swap
local red = lcd.rgb565(0xFF, 0x00, 0x00, true)
local green = lcd.rgb565(0x00, 0xFF, 0x00, true)
local blue = lcd.rgb565(0x00, 0x00, 0xFF, true)

```

---

