# About the mac address

## The current status quo

1. The mac of the chip is configured by software, and there is no mac address register on the chip, so it is variable.
2. Air601-12F At the factory, only the mac address of AP was actually burned, and STA and Bluetooth addresses were not written.
3. The two-dimensional code and MAC address on the shielding cover are the mac address of AP

Problems caused:

1. When used as STA and using AT/LuatOS firmware compiled before December 2023, STA addresses are default values and will conflict under the same router.
2. The Bluetooth address, which is the default value under AT firmware and the unique value related to the device flash under LuatOS firmware.
3. Because the LuatOS firmware has a defense mechanism for the default STA address, when the default STA address is detected, it will automatically switch to the unique value related to the device flash
4. The lost MAC address cannot be found and can only be rewritten through AT firmware.

## Solution

AT Firmware:

1. Update the AT firmware to the latest (with the download address), and the STA address will be consistent with the AP address.
2. The Bluetooth address will still be the default address, but currently it can only be distributed to the network, so there is no impact for the time being and there is no manpower to solve it for the time being.

LuatOS Firmware:

1. Update LuatOS firmware to the latest (download address is followed), STA address is AP address-1
2. The Bluetooth address will be generated according to the unique value of flash and will not conflict with each other.

## How to Restore/Rewrite MAC

### AT The firmware way

1. Download the latest AT firmware, [click me to download](https://pan.air32.cn/s/DJTr?path=%2F%E5%90%84%E7%A7%8D%E6%B5%8B%E8%AF%95%E5%9B%BA%E4%BB%B6%2FAir601%E7%9A%84MAC%E5%9C%B0%E5%9D%80%E4%BF%AE%E6%AD%A3%E5%9B%BA%E4%BB%B6)
2. Use LuaTools brush machine, select universal serial port to print, baud rate regardless
3. Select the correct COM and click on the "Download Firmware" icon
4. Select the firmware and click "Start""
5. Wait for the brushing to be completed. After the brushing is completed, restart the equipment.
6. In the menu, select "simple serial port tool" and check enter and line feed. Or close the LuaTools and select a serial port tool you like, baud rate 115200, 8N1, and check enter and line feed.
7. Suppose the MAC address is set 12:34:56:78:90:ab
8. To set the AP, enter  `AT+CIPAPMAC=12:34:56:78:90:ab`
9. Set the input for STA `AT+CIPSTAMAC=12:34:56:78:90:ab`
10. Enter 'AT CIPAPMAC?'to check the current mac address, if it is the same as the setting, it will succeed.
11. Enter 'AT CIPSTAMAC?'to check the current mac address, if it is the same as the setting, it will succeed.

### LuatOS The firmware way

Call the wlan.setMAC function of LuatOS, please use the latest [cloud compilation](https://wiki.luatos.org/develop/compile/Cloud_compilation.html) firmware
