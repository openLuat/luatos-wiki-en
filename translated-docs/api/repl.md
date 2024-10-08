# repl - "Read-evaluate-output "loop

{bdg-success}`Adapted` {bdg-primary}`Air780E` {bdg-primary}`Air780EP`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/repl/luat_lib_repl.c). If there is any error, please submit issue or help modify pr, thank you！
```


**Example**

```lua
--[[
Modules and corresponding ports supported by this function
Module/chip port baud rate and other parameters
Air101/Air103    UART0   921600  8 None 1
Air105           UART0   1500000 8 None 1
ESP32C3          UART0   921600  8 None 1 -- Note that the simple version (without CH343) does not support
ESP32C2          UART0   921600  8 None 1
ESP32S2          UART0   921600  8 None 1
Air780E          Virtual serial port arbitrary -- call from physical UART not supported

Method of use:
1. Non-Air780E series can use any serial port tool, open the corresponding serial port, remember to check "enter line feed"
2. Air780E Please use it with the LuaTools. There is a "simple serial port tool" in the menu to send. Remember to check "Enter and wrap.""
2. Send a lua statement and end with a carriage return

Statement support:
1. Single-line lua statement, which ends with a carriage return
2. Multi-line statements, wrapped and sent in the following format, such

<<EOF
for k,v in pairs(_G) do
  print(k, v)
end
EOF

Precautions:
1. can be disabled by the repl.enable(false) statement REPL
2. REPL automatically fails after UART port specified with uart.setup/uart.close
3. Single-line statements generally support up to 510 bytes. For longer statements, please use the "multi-line statement" method.
4. To define global variables, use the form_G.xxx = yyy

If you have any questions, please post feedback at chat.openluat.com
]]

```

## repl.enable(re)



Enable or disable the REPL feature

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|bool|Enabled or not, the default is enabled|
|return|Previous setting status|

**Return Value**

None

**Examples**

```lua
-- If the firmware supports REPL, that is, REPL is enabled during compilation, the REPL function is enabled by default.
-- This function is to provide a way to close the REPL
repl.enable(false)

```

---

## repl.push(data)



Proactively push the data to be processed to the bottom layer

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Data to be processed, usually from the serial port.|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- This function is only required for virtual serial devices

```

---

