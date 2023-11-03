# Hardware Data

Please note that this is the information page of **Air103**, not **Air32F103**, these are two completely different chips.

For information about **Air32F103** series chips, please select**air32f103**


## Chip and Pin Definition

### Pin Mapping Table

| GPIO Numbering | Naming | Default Features and Extended Features     |
| -------- | ---- | ---------------------- |
| 0| PA_00  | BOOT |
| 1|PA_01|GPIO_01 / ADC_1 / I2C_SCL|
| 2|PA_02|GPIO_02 / ADC_3/ PWM_30|
| 3|PA_03|GPIO_03 / ADC_2/ PWM_31|
| 4|PA_04|GPIO_04 / ADC_0 / I2C_SDA|
| 5|PA_05|GPIO_05|
| 6|PA_06|GPIO_06|
| 7|PA_07|GPIO_07/ PWM_04|
| 8|PA_08|GPIO_08 / UART4_TX|
| 9|PA_09|GPIO_09 / UART4_RX|
|10|PA_10|GPIO_10/ PWM_10|
|11|PA_11|GPIO_11 / PWM_11|
|12|PA_12|GPIO_12/ UART5_TX/ PWM_12|
|13|PA_13|GPIO_13/ UART5_RX/ PWM_13|
|14|PA_14|GPIO_14/ PWM_14|
|15|PA_15|GPIO_15 / PSRAM_CLK|
|16|PB_00|GPIO_16 / PWM_00 / UART3_TX|
|17|PB_01|GPIO_17 / PWM_01 / UART3_RX|
|18|PB_02|GPIO_18  / UART2_TX / PSRAM_D0 / SPI0_CLK / PWM_02|
|19|PB_03|GPIO_19 / UART2_RX / PSRAM_D1 / SPI0_MISO / PWM_03|
|20|PB_04|GPIO_20 / PSRAM_D2 / SPI0_CS|
|21|PB_05|GPIO_21 / PSRAM_D3 / SPI0_MOSI|
|22|PB_06|GPIO_22 / UART1_TX / SDIO_CLK|
|23|PB_07|GPIO_23 / UART1_RX / SDIO_CMD|
|24|PB_08|GPIO_24 / SDIO_D0|
|25|PB_09|GPIO_25 / SDIO_D1|
|26|PB_10|GPIO_26 / SDIO_D2|
|27|PB_11|GPIO_27 / SDIO_D3|
|28|PB_12|GPIO_28 / PWM_20|
|29|PB_13|GPIO_29 / PWM_21|
|30|PB_14|GPIO_30  / SPI1_CS/ PWM_22|
|31|PB_15|GPIO_31 / SPI1_CLK/ PWM_23|
|32|PB_16|GPIO_32  / SPI1_MISO / PWM_24|
|33|PB_17|GPIO_33 / SPI1_MOSI|
|34|PB_18|GPIO_34|
|35|PB_19|UART0_TX|
|36|PB_20|UART0_RX|
|37|PB_21|GPIO_37|
|38|PB_22|GPIO_38|
|40|PB_24|GPIO_40/ PWM_32|
|41|PB_25|GPIO_41/ PWM_33|
|42|PB_26|GPIO_42 / PWM_34|
|43|PB_27|GPIO_43 / PSRAM_CS|

Only `BOOT` and` UART0_TX/RX` are configured when starting up, other digital pins are GPIO pins, and the status is input high resistance.

| ADC Number (LuatOS) | Features         |
| ----------------- | ------------ |
| 0                 | Module ADC0-PA1 |
| 1                 | Module ADC1-PA4 |
| 2                 | Module ADC2-PA3 |
| 3                 | Module ADC3-PA2 |
| 10                | CPU Temperature      |
| 11                | Internal voltage     |

## Data Link

* Hardware Design Manual: [Air103_MCU Design Manual V1.2.pdf](https://cdn.openluat-luatcommunity.openluat.com/attachment/20211202193606476_Air103_MCU设计手册 V1.2.pdf)
* Development Board BOM: [EVB-Air103_BOM_B_Air103_A10_V1.1_20211022.xlsx](https://cdn.openluat-luatcommunity.openluat.com/attachment/20211231152759844_EVB-Air103_BOM_B_Air103_A10_V1.1_20211022.xlsx)
* Development Board Crystal Oscillator datasheet: [2.3.3.400001004-MDH201808109-D3102512A40000A(1)(1).pdf](https://cdn.openluat-luatcommunity.openluat.com/attachment/20211013165122024_2.3.3.400001004-MDH201808109-D3102512A40000A(1)(1).pdf)
* LDO Manual: [SGM2019-3.3YN5G_TR.PDF](https://cdn.openluat-luatcommunity.openluat.com/attachment/20211202193445472_SGM2019-3.3YN5G_TR.PDF)
* Air103_ Core Board Design Manual: [Air103_Core Board Design Manual V1.2.pdf](https://cdn.openluat-luatcommunity.openluat.com/attachment/20211202193519160_Air103_核心板设计手册 V1.2.pdf)
* Register manual (not recommend): [Register manual and W800 common](https://www.winnermicro.com/upload/1/editor/1607327764402.pdf)
