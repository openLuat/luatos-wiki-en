# 🔋 IoT Power Pro

> ~~[Go to Taobao to buy](https://luat.taobao.com/)~ ~ not yet on sale

```{warning}
This product has not yet been sold, some descriptions may change, and the actual equipment shall prevail.
```

IOT Power Is a fully functional, stable and reliable small hand-held power meter. It can：

- Measure voltage and current of low-power devices via USB C power supply
- Used as a power supply, supporting output specified voltage
- Support current limit output, can set any current limit value
- The maximum voltage is 5V and the maximum current is 2A (the power supply needs to meet the requirements）
- Short circuit protection, short circuit automatically limit the current to 1.5A or less
- Current **Multi-channel synchronous sampling**, no shift delay, highest resolution 0.05μA
- High precision, small error, details can be found in [technical indicators](https://wiki.luatos.org/iotpower/pro/tech.html), welcome to verify
- Up to **10KHz sampling rate** to meet the power consumption test requirements of cellular modules, Bluetooth, WIFI, etc.
- You can **connect a PC client** to view and analyze the current waveform, or use the command line version to capture serial port data (such as strawberry pie) on any device (win/linux/mac) and use the pc client to import and view

IOT Power Pro Is the majority of users carry the ideal test tool.

::::{grid} 1 2 2 3
:gutter: 1 1 1 2

:::{grid-item-card} {octicon}`repo-forked;1.5em;sd-mr-1` Connection and Appearance
:link: connect
:link-type: doc
:img-top: img/font-cn.jpg

Show how Pro connects to the device under test and how each part is useful.

+++
[Learn more »](connect)
:::

:::{grid-item-card} {octicon}`stopwatch;1.5em;sd-mr-1` Operating Instructions
:link: usage
:link-type: doc
:img-top: img/wave.jpg

Describe the usage of each function of the device in detail

+++
[Learn more »](usage)
:::

:::{grid-item-card} {octicon}`law;1.5em;sd-mr-1` Technical indicators
:link: tech
:link-type: doc
:img-top: img/comp.png

List the parameters and performance indicators of the equipment

+++
[Learn more »](tech)
:::

::::

## Upgrade compared to IoT Power V1

|      IoT Power V1       |     IoT Power Pro      |
| :---------------------: | :--------------------: |
| Software shift, shift time ≥ 40us | hardware shift, no shift delay  |
|    Output short circuit will burn | output short circuit automatic protection   |
|       Single STM8 Master | Air32 STM8 Dual Core     |
|   No discharge after closing the output | automatic discharge of energy after closing the output |
|      USB Serial communication | pure USB communication, low occupancy    |
|       The roller is not sensitive | The roller follows the hand and is very comfortable.   |
|      176*128 TFT Screen | 280*240 IPS Screen      |
|         UI Simple | UI fancy       |
|      Screen brightness fixed | can adjust the brightness       |
|     To use a computer to see the waveform | equipment can also see the waveform     |
|    To connect the computer to simulate the battery | the device comes with a simulated battery curve  |

---

```{toctree}
tech
connect
usage
question
```
