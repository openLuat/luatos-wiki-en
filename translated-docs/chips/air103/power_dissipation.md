# Air103 power consumption curve

## Test version

LuatOS-SoC_V0007_AIR103.soc

| Test Scenario \Main Frequency | 2M | 80M |
| --- | --- | --- |
| No-load | 13.4126mA | 22.2793mA |
| timer |13.4344mA | 22.3149mA |
| cyclic addition |13.6115mA| 27.5370mA |
| LIGHT |2.0921mA | 2.3250mA|
| DEEP | 11.8222uA | 11.7956uA|

* Air101/Air103 There are multiple modes of operation

| Mode | Lua Code | RAM Hold | GPIO Hold | RTC Clock | Wake Mode|
|------|--------|---------|-----------|--------|--------|
| Normal| Run | Y | Y | Normal Run | No Sleep  |
| LIGHT | Pause | Y | Y | Normal Run  |wakeup/rtc/dtimer|
| DEEP | Discard | N | N | Normal Run  |wakeup/rtc/dtimer|
| reset | discard | N | N | zero    | reset|

* LIGHT and DEEP can be entered through `pm.request`, and rtc alarm clock or dtimer can be set before entering
* If the wakeup pin is pulled down all the time, the chip will not go to sleep.
* LIGHT After the mode automatically wakes up, the Lua code continues to run
* DEEP After the mode is automatically awakened, the Lua code will run from the beginning and all existing variable data will be lost.

## 2M main frequency

### No-load

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302190214383_image.png)

### timer

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302190216878_image.png)

### cyclic addition

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302190221908_image.png)

### LIGHT

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302190231182_image.png)

### DEEP

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302190226184_image.png)

## 80M main frequency

### No-load

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302190252862_image.png)

### timer

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302190247168_image.png)

### cyclic addition

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302190244578_image.png)

### LIGHT

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302190236896_image.png)

### DEEP

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302190239449_image.png)

## Test scripts and test data files

[Air103 Power consumption test scripts and test data files.7z](https://cdn.openluat-luatcommunity.openluat.com/attachment/20220302193243035_Air103功耗测试脚本及测试数据文件.7z)
