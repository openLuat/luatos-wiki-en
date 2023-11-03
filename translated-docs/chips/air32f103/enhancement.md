# Enhanced function instructions

This migration guide is designed to help you use the enhanced features supported by AIR32F103 devices

## PLL High Frequency Configuration

Description: AIR32F103 built-in PLL can output 216MHz clock

Example of use: Reference [\ModuleDemo\RCC\RCC_ClockConfig Project](https://gitee.com/openLuat/luatos-soc-air32f103/tree/master/ModuleDemo/RCC)

```c
......
RCC_PLLCmd(DISABLE);                                        //Close PLL
AIR_RCC_PLLConfig(RCC_PLLSource_HSE_Div1, RCC_PLLMul_27, 1);//Configuration PLL,8*27=216MHz

RCC_PLLCmd(ENABLE); //enable PLL
......
```

## GPIO Support independent pull-up control

Description: The AIR32F103 supports independent pull-down control (40K). When IO is a multiplexing function, it can replace external circuit resistors. For example：

- When using SDIO module, D0-D3 and CMD can use internal pull-up resistor
- The use of IIC is, when the IIC rate is less than or equal to 100K, the available internal pull-up resistor

Example of use: Reference [\ModuleDemo\IIC\IIC_IntTransmit Project](https://gitee.com/openLuat/luatos-soc-air32f103/tree/master/ModuleDemo/IIC)

```c
//Open internal pull-up
GPIO_ForcePuPdCmd(GPIOB, ENABLE);
GPIO_ForcePullUpConfig(GPIOB, GPIO_Pin_8);  // PB8 Pull-up
GPIO_ForcePullUpConfig(GPIOB, GPIO_Pin_9);  // PB9 Pull-up
```

## USB Internal optional 1.5K pull-up resistor

Description: USB internal DP optional 1.5K pull-up resistor, which can replace external circuit pull-up resistor; And can realize software re-enumeration (no need to add triode control outside PCB)

Example: Reference [\ModuleDemo\USB\Virtual_COM_Port Engineering](https://gitee.com/openLuat/luatos-soc-air32f103/tree/master/ModuleDemo/USB/Virtual_COM_Port)

```c
DP_PUUP = 1;
```

## USB Supports 1/1.5/2/2.5/3/3.5/4/4.5 frequency division of PLL clock as USB clock

Description: Supports 1/1.5/2/2.5/3/3.5/4/4.5 frequency division of PLL clock as USB clock

Example: Reference [\ModuleDemo\USB\Virtual_COM_Port Engineering](https://gitee.com/openLuat/luatos-soc-air32f103/tree/master/ModuleDemo/USB/Virtual_COM_Port)

```c
//USB Clock Configuration Functions,USBclk=48Mhz@HCLK=72Mhz
void Set_USBClock(void)
{
    RCC_USBCLKConfig(RCC_USBCLKSource_PLLCLK_4Div5);    //USBclk=PLLclk/1.5=48Mhz
    RCC_APB1PeriphClockCmd(RCC_APB1Periph_USB, ENABLE); //USB Clock enable
}
```

## MCO Output PLL 2-16 Divided Output Support

Description: MCO support output PLL 2-16 frequency output

Example of use: Reference [\ModuleDemo\MCO\MCO_PllDiv Project](https://gitee.com/openLuat/luatos-soc-air32f103/tree/master/ModuleDemo/MCO)

```c
/** @defgroup Clock_source_to_output_on_MCO_pin
  * @{
  */
enum
{
    RCC_MCO_NoClock = 0x00,
    RCC_MCO_SYSCLK = 0x04,
    RCC_MCO_HSI,
    RCC_MCO_HSE,
    RCC_MCO_PLLCLK_Div2,
    RCC_MCO_PLLCLK_Div3,
    RCC_MCO_PLLCLK_Div4,
    RCC_MCO_PLLCLK_Div5,
    RCC_MCO_PLLCLK_Div6,
    RCC_MCO_PLLCLK_Div7,
    RCC_MCO_PLLCLK_Div8,
    RCC_MCO_PLLCLK_Div9,
    RCC_MCO_PLLCLK_Div10,
    RCC_MCO_PLLCLK_Div11,
    RCC_MCO_PLLCLK_Div12,
    RCC_MCO_PLLCLK_Div13,
    RCC_MCO_PLLCLK_Div14,
    RCC_MCO_PLLCLK_Div15,
    RCC_MCO_PLLCLK_Div16,
};
```
