# Air101 power consumption curve

## Test version

LuatOS-SoC_V0007_AIR101.soc

| Test Scenario \Main Frequency | 2M | 80M |
| --- | --- | --- |
| No-load | 13.6406mA | 22.4846mA |
| timer | 13.6728mA | 22.5383mA |
| cyclic addition | 14.2334mA | 29.7260mA |
| LIGHT | 2.0060mA | 2.0107mA |
| DEEP | 11.5576uA | 11.6721uA |

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

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302185225870_image.png)

### timer

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302185229105_image.png)

### cyclic addition

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302185236439_image.png)

### LIGHT

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302185244151_image.png)

### DEEP

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302185240480_image.png)

## 80M main frequency

### No-load

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302185255491_image.png)

### timer

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302185258589_image.png)

### cyclic addition

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302185306204_image.png)

### LIGHT

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302185318021_image.png)

### DEEP

![image.png](https://cdn.openluat-luatcommunity.openluat.com/images/20220302185312575_image.png)

## Test scripts and test data files

[Air101 Power consumption test scripts and test data files.7z](https://cdn.openluat-luatcommunity.openluat.com/attachment/20220302193239733_Air101功耗测试脚本及测试数据文件.7z)
