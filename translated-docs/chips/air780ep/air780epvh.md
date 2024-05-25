# Air780EPVH Additional instructions

Air780EPVH is a variant of Air780EPV, adding gnss/gps and codec

1. Support GNSS/GPS, namely satellite navigation function
2. Built-in audio codec 8311, support MIC/SPK input and output

## GNSS/GPS Function introduction

1. The GNSS chip used is HD8128
2. The power supply control is GPIO17, and the standby power supply pin is GPIO23
3. 26M output needs to be turned on
4. GNSS The chip is connected to UART2, and as of 2024.5.25, the default baud rate is 9600, which may be corrected later.115200

csdk Method to Open 26M Output

```c
luat_mcu_xtal_ref_output(1, 0);
```

luatos The method of opening 26M output requires firmware compiled after 2024.5.25

```lua
mcu.XTALRefOutput(true, false)
```

GPS command to switch baud rate to 115200, sent to GNSS chip via UART

```
$PCAS01,5*19\r\n
```

## Data Link

1. [Air780EPVH Information](https://air780epvh.cn)
