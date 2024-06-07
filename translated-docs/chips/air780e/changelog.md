# Ec618 Series Firmware Release Notes

* [Firmware download address](https://gitee.com/openLuat/LuatOS/releases)
* [Firmware Download Alternate Address](https://pan.air32.cn/s/DJTr?path=%2F)
* Fully automatic compilation of the latest firmware through [Cloud Compile](https://wiki.luatos.org/develop/compile/Cloud_compilation.html)

## v1110

Compatibility changes:
1：tts_onchip Turn off support for websocket and ftp clients under
2：tts_onchip Lower Close ftp
3：Due to insufficient space, subsequent versions to be tts_onchip can be compiled on the cloud or locally. This version will not be updated.

Defect repair
1：Prevent possible time setting errors
2：luaot The size of the luadb partition read by the firmware is incorrect.
3：wait485 When, the 485 turned to io control timer did not stop, resulting in data reception problems.
4：When pwm is not closed and the cycle and duty cycle are changed, it may crash.
5：485 I can't use the reversing foot.GPIO14、GPIO15
6：The software serial port cannot use timer1 and timer4
7：OTA When the underlying data is written but the script data is not completed, the upgrade is not allowed.
8：uart485 Unable to use ALT4 GPIO18 and GPIO19
9：socket Callback message error on active shutdown
10：Mold into the original patch
11：mqtt When sending, the data is sent out at one time to avoid being interrupted.
12：mqttconnect Unable to connect to server when message length exceeds 256
13：ftp abnormal crash
14：socket Add protection to prevent freed resources from being used again


New Features
add：Reset stack parameters to default
add：Base station synchronous time switch
add：Deep Sleep Timer Callback Message
add：Retains the level set before sleep during deep sleep wake-up
add：w5500 Add DHCP Timeout Message
add：DHCP Increased retries for slow routers
add：socket Query the current connection status
add：http Custom header supports custom size
add：sfud mutex protection

Update function
update：When an unparsed NMEA statement is encountered, the mask prints


## V1109

Compatibility changes:

1：tts_onchip Lower Close YMODEM
2：tts_onchip Close REPL
3：Adjust the floating-point number format of json.encode to%.7f, which is more in line with practical use, otherwise it will eat floating-point precision.

Defect repair:
* fix: **Join the original patch to repair SWD CP IO will crash when encountering abnormal signals, and repair the vulnerability of pseudo base station protection.**
* fix: socket.rx When receiving data, if zbuff expansion fails, first try to reduce the receiving length, if there is no space, only an error can be returned.
* fix: u8g2.CopyBuff Not working properly, the reason is that there is an error in judging the length of zbuff
* fix: ftp login crash after failure
* fix: socket.sntp Using a custom domain name will report an error crash.
* fix:luatos i2s Recording cannot be configured frame size
* fix:Fix luatos firmware enable tts times luat_sfud cannot link
* fix:websocket Heartbeat packet not sent out normally
* fix:When the cloud compiled luatos firmware chooses to disable DTLS, the mbedtls_ssl_conf_handshake_timeout function will be reported as not existing.
* fix:Unable to validate pin code

New Features:
* add: sfud Support to obtain flash capacity and page information
* add: adc Partial pressure range Add maximum limit
* add: pm.dtimerCheck Add Remaining Time
* add: http Support big data upload
* add: pseudo base station masking time
* add: u8g2 Support for configuring x-axis offset
* add: libgnss.getIntLocation Add Speed Parameter Item
* add: errdump Support for custom domain names and ports
* add: crypto.crc16_modbus Support to set the initial value, convenient for multi-segment data continuous calculation
* add: Added u8g2.SetPowerSave function
* add: pcf8563t Clock module driver and demo
* add: luatos Compilation of the xxtea library added to the firmware
* add: luatos Add Ant Chain integration
* add: luatos Firmware add ercoap library
* add: ntp-based millisecond timestamp socket.ntptm()

Update function:
* update: libgnss.casic_aid Compatible with the string coordinate value returned by base station positioning.
* update:remove the 4096 limit of mqtt receiving a single packet
* update: u8g2 Add a driver ssd1309 i2c mode, previously only SPI
* update:Optimizing the dhcp process for the w5500
* update:luatos Firmware I2C uses poll mode by default
* update:luatos Supplemental I2S mono case, left and right channel selection
* update:luatos uart too many received messages are not allowed to prevent a large number of uart received messages from freezing under abnormal conditions
* update:adc Maximum limit of partial pressure range

## V1108

This version has been released on 2023.11.15, and the corresponding git tag is v1108.ec618.release

Compatibility changes:

1. Correct the unit of CPU temperature
   * Impact, the previous version returned CPU temperature is degrees Celsius, other BSP are 1/1000 degrees Celsius
   * Solution: The new data' //1000 'will get the original data value

Defect repair:

* fix: **socket close When the new data flag is not cleared * *, SSL has a probability of continuous reconnection failure.
* fix: **mqtt In some cases of the library, buffer_offset reconnect is not set to zero * *, MQTT has a probability of continuous reconnect failure.
* fix: **mqtt Heartbeat timer count error **causes mqtt heartbeat may not be emitted
* fix: **CPU The unit of temperature should be 1/1000 degrees Celsius, which is wrong.**
* fix: **Rewrite the sntp function and support automatic timeout. **In a weak network, sntp may run out of socket connections.
* fix: **Inform the original patch to fix a crash caused by searching for base stations.**
* fix: mqtt The library should close when sending the package to report an error.socket
* fix: lvgl Repeated creation of style will crash
* fix: fatfs The lsdir cannot scan the folder.
* fix: RTC library compatible mon attribute
* fix: AES-128-ECB And PKCS7, decryption error data cannot be returned
* fix: Fix iotcloud library onenet partial data truncation problem
* fix: Fix ymodem path string with no 0 at the end
* fix: 64bit For luatos firmware, printing print(-1) will output a large value.
* fix: 780E w5500 sntp crash
* fix: ftp Starting the background thread should judge whether it is successful or not, and the failure process should be followed if the creation fails.
* fix: errDump The open parameter of the manual read file is incorrect.
* fix: libgnss.getIntLocation The speed value is abnormal.
* fix: sim The number of times the card is erased is not counted correctly.
* fix: 64bit The callback message that the audio playing in the audio library under the firmware does not end playing..
* fix: Fix the problem that socket cannot be reconnected when it cannot be connected.
* fix: vfs_fatfs Capacity calculation error in
* fix: i2c.createSoft In the Air780E V1107 firmware will report an error, just fill in the delay value
* fix: http The closing logic of the library is incomplete and the compilation warning is cleared.
* fix: When the DNS procedure is in progress, the call network_force_close_socket and the connection is no longer in progress, the DNS completion still calls back
* fix: crypto.totp Function has a memory leak
* fix: gmssl library's sm4 encryption mode error
* fix: libemqtt All large arrays in are changed to heap allocation.
* fix: Fix partial characters lost when iconv library converts long data
* fix: http attributes related to the library tls certificate are not forcibly initialized to 0, and illegal values may appear.
* fix: fonts library does not enumerate new sarasa fonts correctly
* fix: ssl When sending large amounts of data, you need to send them in batches.
* fix: adc The acquisition tag is not updated, resulting in the acquisition of the adc value may be the last
* fix: TTS Firmware crashes when SPI FLASH is not successfully mounted
* fix: tts Unable to select while playing i2s1
* fix: The sarasa English font compiled by the cloud does not take effect.
* fix: adc The internal partial pressure mode is not completely turned off when it is selected to turn off the internal partial pressure mode.

Features added and updated:

* add: iotcloud Library supports graffiti/Baiduyun
* add: ftp add data port to return intranet ip compatibility
* add: Support to obtain hardware version number
* add: fskv library to add the sett function
* add: Add fastlz compression library
* add: Pour back the json.null property
* add: crypto library adds streaming hash interface
* add: sntp Add Adapter Options
* add: Add u8g2.DrawButtonUTF8
* add: mobile Add SIM card write statistics API
* add: lcd The library supports off-screen coordinates for drawing, such as the part of the picture outside the screen.
* add: mqtt Add error return parameter
* add: lcd Add Qualcomm font gbk interface
* add: mqtt Add Status Acquisition Interface
* add: sms.send The new auto_phone_fix can disable the automatic release of the corresponding target number to adapt to the complex number rules abroad.
* add: es8311 Basic loop recording demo
* add: Add crypto.crc7 function
* add: u8g2.CopyBuffer Function
* add: gmssl.sm2 Encryption and decryption add website compatibility mode
* add: gmssl.sm2 Encryption and decryption support old national standard C1C2C3
* add: gmssl Add sm2 signature and signature verification to the library.
* add: http The library supports authentication information in the URL.
* add: Added bit64.strtoll function
* add: luatos Cloud compilation supports LVGL-enabled PNG and BMP decoding
* update: Improve the end processing of ymodem received files
* update: gpio.debounce In mode 1, removing it is an unreasonable interruption.
* update: FTP Optimize the process of waiting for data transmission
* update: Update ws2812 demo,EC618 support pin direct drive
* update: ymodem Compatible ymodem-1k
* update: Optimize memory allocation for ftp receive files
* update: Optimizing http callback download length is worth accuracy
* update: Optimized secondary memory reclamation and provides interface
* update: No longer a simple prompt when there is insufficient memory, but print out the usage
* update: sim The card may be in arrears as a reminder.
* update: Automatically turn off wdt before executing the poweroff, otherwise it will crash and restart after 20 seconds.

## V1107

Compatibility changes:

1. No longer automatically query base station information
   * Impact: If the mobile.reqCellInfo function is not called, mobile.cellInfo() returns an empty array
   * Workaround: Call on demand or call regularly `mobile.reqCellInfo(60)`
2. Volume configuration for software DAC audio takes effect
   * Impact: `audio.vol (0,50) `may not be heard clearly when using the Air780E software DAC function
   * Solution: Set the volume to 100 or above

Function added:

* uart.read Support for reading specified length
* Added mcu.iomux function to support configuring uart/spi/i2c multiplexing
* Added pm.ioVol function to support configuration of io voltage
* New combined PSM ultra-low power mode, integrated in pm.power function
* Extend the gpio.setup function, add alt_func parameters, and support configuration reuse
* Extended http.request function, support for Fota and download process callback
* Add a new iotcloud library to connect with cloud platforms such as Alibaba Cloud/Tencent Cloud/Huawei Cloud/Onenet
* Added mobile.config function to set static network optimization
* Added lora2 library to support mounting of multiple lora devices
* Added the mobile.setPin function to support operations related to the PIN code of the SIM card.
* Add a variety of sensors/peripheral drivers, ina226/ak8963/mpu9250/st7565, etc.
* The new repl library supports direct input of lua statements from the serial port and output the results after execution.
* Added adc.set library to support partial pressure setting
* New demo
   * Low Power Demo      demo/psm
   * DingTalk robot      demo/dingding
   * Flying book robot      demo/feishu
   * Cloud Platform Docking      demo/iotcloud
   * Department standard jt808 docking   demo/jt808
   * Recording function        demo/record
   * virtual serial host computer  demo/usb_uart
   * China Telecom ctwing  demo/ctwing

Please refer to git's commit log for more detailed updates.

## V1106

1. New: mobile library adds network special configuration function
2. New: obtain the cellid of the current serving cell, no need to search again
3. New: websocket library to add sent/disconnect events
4. Added: http support fota
5. New: Tengxun Cloud demo
6. New: fota.file(path)
7. New: Mobile adds a constant in network search.mobile.SEARCH
8. new: mqtt library supports qos2 message delivery
9. New: mqtt adds the verify parameter, which can be used to force certificate verification.
10. new: the sent event callback is added to the luatos usb serial port, but only represents that data is written to the underlying cache.
11. New: Add httpsrv
12. Added: TF Card Power-on Control
13. New: domain name resolution, if the remote_port in socket.connect is set to 0, only DNS will be performed, no connection will be made, and DNS will be returned directly after completion. ON_LINE
14. Optimization: Optimize cloud compilation configuration, increase uart0 release, fonts, etc.
15. Optimization: adjust the default size of the luat_uart_setup buffer, set the minimum value of 2k and the maximum value of 8k, to solve the problem of insufficient uart buffer in large number of scenarios, especially Air780EG uart2
16. Optimization: Increase the number of RX DMA buffers for UART and can be adjusted with the user's RX buffer
17. Optimization: string.fromhex() filters out illegal characters
18. Optimization: more uniform use socket id
19. Optimization: It is more reasonable for LCD to clear the screen to black by default. Its main function is to avoid flower screens when displayed after initialization.
20. Optimization: gnss processing is transferred to lua task
21. Optimization: Perform gc before and after loading built-in libraries and require to peak-cut memory consumption
22. Optimization: Allow cid1 to set the user's apn for dedicated network cards that cannot be activated with public network APN
23. Optimization: lpuart exception handling
24. Optimization: luatos boot print full hardware version number
25. Optimization: luatos uart rs485 will be forced to change to if the conversion timeout setting is less than 1ms 1ms
26. Optimization: luat_websocket_ping judge the connection status before sending
27. Optimization: Optimized luatos volume adjustment
28. Optimization: Improved task mailbox to reduce memory consumption
29. Optimized: mp3 decoder repackaged
30. Optimization: Speed up the allocation of local ports of the network card for hardware protocols
31. optimization: reduce the ram consumption of ftp
32. fix: lwip releases the same tcp twice with small probability
33. Fix: luatos wdt reinitialization failed
34. Fix: Fix gc9306 90 ° orientation setting error
35. Fix: zbuff:unpack, pack.unpack add lua virtual stack detection
36. Fix: luatos fetch cellinfo sometimes fails
37. Fix: json library will become scientific counting method when floating point numbers are 0.0.
38. FIX: libgnss.clear() fails to properly clear historical positioning data
39. FIX: Internal hardware state machine does not automatically recover after I2C read/write failure
40. Fix: Fix i2c1 default pin error
41. Repair: After the low-power serial port is turned on, there will still be interruptions when it is turned off, and the serial port will crash when it is turned off.
42. Fix: When uart0 outputs EPAT log, if there is clutter on rx, it may crash
43. Fix: http library timeout_timer has the possibility of multiple free
44. Fix: mqtt library setting will allow payload to be empty
45. Fix: Abnormal problem when http Content-Length = 0
46. Fix: Incorrect judgment of sntp_connect

## LuatOS-SoC@EC618 V1105

1. New: Add software DAC (PWM audio output) **Note: This feature is not supported on the existing version development board**

2. Fix: Rollback the difference between reading and writing integer/floating-point data in fskv library upgraded from V1103 to V1104

   **This version also contains [V1104](https://gitee.com/openLuat/LuatOS/releases/tag/v0007.ec618.v1104) modify all updates**

## LuatOS-SoC@EC618 V1104

tag: v0007.ec618.v1104
date: 2023-03-13

1. New: Added gmssl library to support national security sm2/sm3/sm4
2. Added: Software uart
3. New: support w5500, can plug-in Ethernet module
4. New: uart1 automatically enables LPUART function under the conditions of 600,1200,2400,4800 and 9600 baud rates, and data reception is not lost during sleep
5. New: luatos adds amr encoding function
6. New: support iconv library
7. New: fatfs
8. New: luatos can choose to turn on powerkey anti-shake
9. New: luatos added cam_vcc control
10. New: audio.config increases the time interval for setting the closing of pa and dac after audio playback is completed, eliminating possible pop sound
11. New: Add base station wifi positioning demo lcsLoc.lau
12. New: mqtt Add Disconnect Event
13. New: Print prompt if script is not swiped in
14. New: Add the iter and next functions of the fdb/fskv library
15. New: Free boot download script
16. Optimization: adc's id is compatible with the old 10/11 configuration
17. Optimization: The limit on the number of concurrent logs per user has been lifted.
18. Optimization: Optimize usb serial port output
19. Optimization: Optimize the timing of RRC release
20. optimization: dynamic ram allocation optimization
21. optimization: put interrupt service functions, high real-time functions, and some commonly used functions into ram to improve running efficiency
22. Optimization: uart rx uses DMA to receive in normal mode, greatly improving the stability of large data reception at high baud rates.
23. Optimization: luatos' fota defense cannot be initialized because of insufficient memory
24. Optimization: When encountering a fake base station, quickly switch to a normal base station
25. Optimization: SPI open internal pull up and down to improve stability
26. Optimization: http ignores customizations Content-Length
27. Optimization: When the network encounters a fatal error, the protocol stack can be automatically restarted to recover, which needs to be turned on manually.
28. optimization: perfect the operation of apn activation
29. Optimization: http library url length is unlimited
30. Optimization: audio task priority increases to improve playback stability
31. Fix: crash in luatos socket dtls mode
32. Fix: Incomplete audio_play_stop Judgment
33. Fix: Fix the problem that the dns query interface is blocked and no return is returned in a weak network environment.
34. FIX: Fix luat_fs_fopen crashing when opening a path containing a non-existent directory
35. Fix: After the tls handshake is completed, if there is no data interaction for a period of time, it will time out.
36. FIX: SNTP custom domain named 3 when handling exceptions
37. Fix: protobuf library does not decode 64bit data correctly
38. Fix: duplicate miniz library constant causes dead loop when pairs
39. Fixed: Module type not recognized after deep sleep wake-up

## ChangeLog for LuatOS@EC618 V1103

tag: v0007.ec618.v1103
date: 2023-02-06

**Note: Since the return value of the socket interface is not compatible with the previous one, the version number is hereby raised from v1002 to v1103 as a reminder.**

**This version is fully supported.Air780EG**

1. New: ipv6 is supported, mobile.ipv6 needs to be called to open, but it is not opened by default. ipv6 needs to be supported when opening the card (any good ideas for this application scenario can be fed back to us）
2. New: Support ftp
3. New: Support fskv
4. New: libfota.lua package library, fota is simpler
5. New: Mobile adds IP_LOSE messages
6. New: mobile allows first use of power-on SIM0
7. New: lbsLoc.lua package library, base station positioning easier
8. new: sms library supports cleaning up long sms snippets sms.clearLong()
9. New: Add timeout parameter for http
10. New: Added rtc.timezone function
11. New: Recording function
12. new: sms library supports disabling automatic merging of long sms
13. New: i2s callback and asynchronous receive function
14. New: Add mlx90614 driver
15. new: add a new ram file system
16. Added: pm.lastReson() More detailed boot reasons are available
17. New: Support gtfont
18. New: Support for user-defined APN and activation
19. Optimization: 485 waiting for sending to complete
20. Optimization: USB virtual serial port single transmission length is no longer limited 512
21. Optimization: SPI underlying driver optimization, enable DMA
22. Optimization: I2C underlying driver optimization
23. Optimization: UART underlying driver optimization
24. Optimization: Adjust the code of the iotauth library so that it does not use static memory, adjust the default timestamp, and correct the length of the output key.
25. Modified: GPIO14/15 maps to ALT 4 of PAD 13/14, thus avoiding conflicts with UART0
26. Modification: return value of socket interface specification (incompatible with previous version, important！！！！！）
27. Fix: udp receive will have memory leak
28. Fix: http library does not support customization Host
29. fix: sntp custom address table handling exception
30. Fix: fota only updates scripts and has a probability of failure when it is very young.
31. Fix: sms library judges errors when correcting multiple long short messages
32. fix: sms library receives multiple long short messages in a row and the order is out of order, the content of short messages is merged incorrectly. 
33. Fix: rx callback for virtual UART
34. Fix: If qos = 0 when the mqtt library publish messages, the returned pkgid is unreasonable and should be fixed 0
35. Fix: Incomplete UDP Receive Data
36. Fix: rtc library is not implemented correctly
37. fix: http chunk encoding exception

**core_V1103.zip** Is the firmware file, the other two are the underlying source code, no need to download.
** Air780EG Test positioning effect_match public number article. zip **is used to test Air780EG positioning effect
