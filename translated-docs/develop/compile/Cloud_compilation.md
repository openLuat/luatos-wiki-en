# ☁️ Cloud Compilation

Are you still puzzling how to build a compilation environment? With more and more chips and more and more environments? A good computer card ~ OK, cloud compilation will help you solve your problems~

Supported Platforms:
1. air101       - Air101 LuatOS Firmware
2. air103       - Air103 LuatOS Firmware
3. air105       - Air105 LuatOS Firmware
4. idf5-esp32c3 - ESP32C3 LuatOS Firmware
5. idf5-esp32s3 - ESP32S3 LuatOS Firmware
6. ec618-luatos - ec618 LuatOS firmware for series (Air780E/Air600E, etc.)
6. ec618-csdk   - ec618 Series (Air780E/Air600E, etc.)CSDK
8. air601       - Air601 LuatOS Firmware
8. air601-at    - Air601 AT Firmware

## Reminder

1. The flash occupancy displayed on the page is an estimated value, which can be put down if the compilation is successful.
2. soc The file itself is also a compressed package (but it does not need to be decompressed), and the size of the soc file does not represent the true size of the underlying firmware.

Here we show Air101

## Visit the build site

[https://luatos.com](https://luatos.com)

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20211011092251619_image.png)

## Click Login

Use the [erp account](http://erp.openluat.com) to log in. If you do not have an account, you can register for free. This service is free to use..

![QQ Screenshot 20211011092353.png](https://cdn.openluat-luatcommunity.openluat.com/images/20211011092608787_QQ截图 20211011092353.png)

## Click Build in the upper right corner

Click the new build menu in the upper right corner to customize the build name, which can be any English character, regardless of the specific build type..

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20211011092859451_image.png)

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20211011092945203_image.png)

![20211011093313018_image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20211011094139885_20211011093313018_image.png)

## Check the components you want

![20211011093354772_image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20211011094238218_20211011093354772_image.png)

## Click the top right corner to save changes, then click Ready

**Note: Be sure to click Save Changes**

![20211011093452493_image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20211011094345320_20211011093452493_image.png)

Wait for compilation and refresh the results after a period of time.

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20211011093739823_image.png)

Compile successfully, click to download

## **FAQ：**

### Why did I fail to compile?？

There are many problems with compilation failure, usually these are more common.：

1. Click Ready for Save changes without clicking.
2. Too many components are selected, or the font selection causes the flash size to be insufficient, resulting in compilation failure
3. Firmware packages are all compressed packages, and soc is also a compressed package, which does not directly represent the firmware size.
4. If the failure persists, please [contact us](../../pages/supports.md) or [report issue](https://github.com/openLuat/LuatOS.git)

## How to use custom font

The name of the custom font, which is the same as the "naming" value of the font on the cloud compilation page. That is, the English font name_font type_font size

1. Custom font does not conflict with original font
2. The difference between the reference method of the custom font library and the original font library

```lua
-- References to the original font
lcd.font_opposansm12_chinese

-- Customize the reference method of the font library
fonts.get("oppo_bold_12")
```

Mode of use

```lua
-- The reference method of the original font library, each reference
lcd.setFont(lcd.font_opposansm12_chinese)
eink.setFont(eink.font_opposansm12_chinese)
u8g2.setFont(u8g2.font_opposansm12_chinese)

-- Customize the reference method of the word library and use it uniformly.
lcd.setFont(fonts.get("oppo_bold_12"))
eink.setFont(fonts.get("oppo_bold_12"))
u8g2.setFont(fonts.get("oppo_bold_12"))
```
