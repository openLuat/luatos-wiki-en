# Hardware Data

## INFORMATION DOCUMENTS

* Hardware Design Manual: [Air101_MCU Design Manual V1.0.pdf](https://cdn.openluat-luatcommunity.openluat.com/attachment/air101_%E8%8A%AF%E7%89%87%E8%A7%84%E6%A0%BC%E4%B9%A6_v1.1.pdf)
* Development Board BOM: [Air101_CORE_BOM_B_Air101_CORE_A10_V1.4_20210909().xlsx](http://cdndownload.openluat.com/wiki/chips/air101/20211013165102234_Air101_CORE_BOM_B_Air101_CORE_A10_V1.4_20210909.xlsx)
* Development Board Crystal Oscillator datasheet: [2.3.3.400001004-MDH201808109-D3102512A40000A.pdf](http://cdndownload.openluat.com/wiki/chips/air101/2.3.3.400001004-MDH201808109-D3102512A40000A.pdf)
* Register manual (not recommended): [Register manual is common to W800](https://www.winnermicro.com/upload/1/editor/1607327764402.pdf)

## Pin Mapping Table

| GPIO Numbering | Naming | Default Features and Extended Features     |
| -------- | ---- | ---------------------- |
| 0        | PA0  | BOOT                   |
| 1        | PA1  | I2C_SCL/ADC0           |
| 4        | PA4  | I2C_SDA/ADC1           |
| 7        | PA7  | GPIO/PWM4              |
| 16       | PB0  | GPIO/PWM0/UART3_TX     |
| 17       | PB1  | GPIO/PWM1/UART3_RX     |
| 18       | PB2  | SPI_SCK/PWM2/UART2_TX  |
| 19       | PB3  | SPI_MISO/PWM3/UART2_RX |
| 20       | PB4  | SPI_CS/UART4_TX        |
| 21       | PB5  | SPI_MOSI/UART4_RX      |
| 22       | PB6  | UART1_TX               |
| 23       | PB7  | UART1_RX               |
| 24       | PB8  | GPIO                   |
| 25       | PB9  | GPIO                   |
| 26       | PB10 | GPIO                   |
| 27       | PB11 | GPIO                   |
| 35       | PB19 | UART0_TX               |
| 36       | PB20 | UART0_RX               |

Only `BOOT` and` UART0_TX/RX` are configured when starting up, other digital pins are GPIO pins, and the status is input high resistance.

| ADC Number (LuatOS) | Features         |
| ----------------- | ------------ |
| 0                 | Module ADC0-PA1 |
| 1                 | Module ADC1-PA4 |
| 10                | CPU Temperature      |
| 11                | Internal voltage     |

