# WLAN

## Basic information

* Date of drafting：2020-01-14
* Designer：[freestrong @ Snail]

## Why you need WLAN middleware management WIFI
With the rapid development of the Internet of Things, more and more embedded devices are equipped with WIFI wireless network devices. In order to manage WIFI network devices, RT-Thread introduces a WLAN device management framework. This framework has many functions to control and manage WIFI, and provides many conveniences for developers to use WIFI devices.

## Design ideas and boundaries
 * Manage and abstract Wlan's C API, providing a set of Lua API for user code to call
## Relevant knowledge points

* [RT-Thread WLAN Equipment Management](https://www.rt-thread.org/document/site/programming-manual/device/wlan/wlan/)


## C API(Platform layer)

```c
#define Luat_WLAN_SECURITY_OPEN             0x00
#define Luat_WLAN_SECURITY_WEP_PSK          0x01
#define Luat_WLAN_SECURITY_WEP_SHARED       0x02
#define Luat_WLAN_SECURITY_WPA_TKIP_PSK     0x03
#define Luat_WLAN_SECURITY_WPA_AES_PSK      0x04
#define Luat_WLAN_SECURITY_WPA2_AES_PSK     0x05
#define Luat_WLAN_SECURITY_WPA2_TKIP_PSK    0x06
#define Luat_WLAN_SECURITY_WPA2_MIXED_PSK   0x07
#define Luat_WLAN_SECURITY_WPS_OPEN         0x08
#define Luat_WLAN_SECURITY_WPS_SECURE       0x09

#define Luat_WLAN_EVT_READY                 0x01
#define Luat_WLAN_EVT_SCAN_DONE             0x02
#define Luat_WLAN_EVT_SCAN_REPORT           0x03
#define Luat_WLAN_EVT_STA_CONNECTED         0x04
#define Luat_WLAN_EVT_STA_CONNECTED_FAIL    0x05
#define Luat_WLAN_EVT_STA_DISCONNECTED      0x06
#define Luat_WLAN_EVT_AP_START		        0x07
#define Luat_WLAN_EVT_AP_STOP               0x08
#define Luat_WLAN_EVT_AP_ASSOCIATED         0x09
#define Luat_WLAN_EVT_AP_DISASSOCIATED      0x0A


/* Network */
int luat_wlan_set_stanet(luat_wlan_net_t * net)    //Set the network information of the STA
luat_wlan_net_t luat_wlan_get_stanet(void)         //Obtain the network information of a STA.

int luat_wlan_set_apnet(luat_wlan_net_t * net)     //Set the network information of the AP
luat_wlan_net_t luat_wlan_get_apnet(void)          //Obtaining Network Information of an AP


/* WLAN Connection */
int luat_wlan_connect(luat_wlan_info_t *info);	    //Connection hotspot
int luat_wlan_is_ready(void);	                    //Get Ready Flag
luat_wlan_info_t luat_wlan_get_info(void);          //Get connection information
int luat_wlan_get_rssi(void);	                    //Get signal strength

/* WLAN Scan */
luat_wlan_info_t luat_wlan_scan_with_info(void);    //Scan

/* WLAN Hot spot */
int luat_wlan_start_ap(luat_wlan_info_t);           //Start Hotspot
int luat_wlan_ap_is_active(void);                   //Get Startup Flag
int luat_wlan_ap_stop(void);                        //Stop Hotspot
luat_wlan_info_t luat_wlan_ap_get_info(void);       //Obtain hotspot information.
luat_wlan_info_t luat_wlan_ap_get_stainfo(void);    //Get Station information about connected hotspots

/* WLAN Event Callback */
int luat_wlan_register_event_handler(int evt);      //Event Registration
int luat_wlan_unregister_event_handler(int evt));   //Unregister

/* WLAN Power Management */
int luat_wlan_set_powersave(int level);             //Sets the power consumption level for station mode.
int luat_wlan_get_powersave(void);                  //Get Power Level
```


## Constant
```lua
--Security mode
wlan.OPEN                        --Open security
wlan.WEP_PSK                     --WEP Security with open authentication
wlan.WEP_SHARED                  --WEP Security with shared authentication
wlan.WPA_TKIP_PSK                --WPA Security with TKIP
wlan.WPA_AES_PSK                 --WPA Security with AES
wlan.WPA2_AES_PSK                --WPA2 Security with AES
wlan.WPA2_TKIP_PSK               --WPA2 Security with TKIP
wlan.WPA2_MIXED_PSK              --WPA2 Security with AES & TKIP
wlan.WPS_OPEN                    --WPS with open securit
wlan.WPS_SECURE                  --WPS with AES security

--Register handler events for serial port events
wlan.EVT_READY                   --IP Address
wlan.EVT_SCAN_DONE	 	         --Results of the scan
wlan.EVT_SCAN_REPORT             --Scanned hotspot information
wlan.EVT_STA_CONNECTED           --Station information of successful connection
wlan.EVT_STA_CONNECTED_FAIL		 --Station information of connection failure
wlan.EVT_STA_DISCONNECTED	     --Disconnect Station information
wlan.EVT_AP_START		         --AP information on successful startup
wlan.EVT_AP_STOP		         --Information about the AP that failed to start
wlan.EVT_AP_ASSOCIATED		     --Connected Station information
wlan.EVT_AP_DISASSOCIATED        --Disconnected Station information
```



## Lua API


```lua
--set the network information in wifi Station mode
wlan.setStaNet(ip,netmask,gateway)
--[[
Use cases:
    wlan.setStaNet()                    --Start DHCP, automatically obtain ip address, subnet mask, gateway and other information
    wlan.setStaNet(ip)                  --Set ip address statically, ip:xxx.xxx.xxx.xxx, subnet mask: 255.255.255.0, gateway：xxx.xxx.xxx.1
    wlan.setStaNet(ip,net)              --Set ip address statically, ip:xxx.xxx.xxx.xxx, subnet mask: xxx.xxx.xxx.xxx, gateway：xxx.xxx.xxx.1
    wlan.setStaNet(ip,netmask,gateway)  --Set ip address statically, ip:xxx.xxx.xxx.xxx, subnet mask: xxx.xxx.xxx.xxx, gateway：xxx.xxx.xxx.xxx
]]

--obtain network information in wifi Station mode
wlan.getStaNet()
--[[
Use cases:
    local ip,netmask,gateway = wlan.getStaNet()
]]


--Set network information in wifi AP mode
wlan.setApNet()
--[[
Use cases:
    wlan.setStaNet(ip)                  --Set ip address statically, ip:xxx.xxx.xxx.xxx, subnet mask: 255.255.255.0, gateway：xxx.xxx.xxx.1
    wlan.setStaNet(ip,net)              --Set ip address statically, ip:xxx.xxx.xxx.xxx, subnet mask: xxx.xxx.xxx.xxx, gateway：xxx.xxx.xxx.1
    wlan.setStaNet(ip,netmask,gateway)  --Set ip address statically, ip:xxx.xxx.xxx.xxx, subnet mask: xxx.xxx.xxx.xxx, gateway：xxx.xxx.xxx.xxx
]]

--obtain network information in wifi AP mode
wlan.getApNet()
--[[
Use cases:
    local ip,netmask,gateway = wlan.getApNet()
]]

--Scan for hot spots
wlan.scan(ssid)
--[[
Use cases:
    local num,info = wlan.scan()      --Scan for hot spots
    local num,info = wlan.scan(ssid)  --Scan specified ssid hotspots
]]

--Connection hotspot
wlan.connect(ssid,password,bssid)
--[[
Use cases:
    wlan.connect(ssid)                  --Connect open hotspots
    wlan.connect(ssid,password)         --Hotspot for connection encryption
    wlan.connect(ssid,password,bssid)   --Connect to an encrypted hotspot with a specified MAC address
    wlan.connect(ssid,nil,bssid)        --Connect to an open hotspot with a specified MAC address
]]

--Get information about a connection hotspot
wlan.getinfo()
--[[
Use cases:
    local info = wlan.getinfo()
    info.ssid      --Connection Hotspot Information ssid
    info.channel   --Channel to connect to the hotspot
    info.rssi      --Signal strength of the connection hotspot
    info.bssid     --MAC address of the connection hotspot
    info.security  --Safe mode for connecting to hotspots
]]

--Disconnect the hot spot
wlan.disconnect()

--Connection status
wlan.ready()

--Get signal strength
wlan.getrssi()

--Create Hotspot
wlan.ap_start(ssid,password,security,channel,hidden)
--[[
Use cases:
    wlan.connect(ssid)                                        --Create an open hotspot
    wlan.connect(ssid,password)                               --Create an encrypted hotspot
    wlan.connect(ssid,password,security)                      --Create a hotspot encrypted with a specified security level
    wlan.connect(ssid,password,security,channel)              --Create an encrypted hotspot with a specified channel and a specified security level
    wlan.connect(ssid,password,nil,channel)                   --Create a specified channel, encrypted hotspot
    wlan.connect(ssid,nil,nil,channel)                        --Create designated channels, develop hot spots
    wlan.connect(ssid,password,security,channel,hidden)       --Create an encrypted hotspot with the specified security level, the specified channel, and whether to broadcast the ssid
    wlan.connect(ssid,password,nil,channel,hidden)            --Specifies the channel, whether to broadcast ssid encryption hotspots
    wlan.connect(ssid,password,nil,nil,hidden)                --Specifies the channel, whether to broadcast ssid encryption hotspots
    wlan.connect(ssid,nil,nil,nil,hidden)                     --Specifies the channel, whether to broadcast ssid development hotspots
]]

--Get information about creating a hotspot
wlan.getapinfo()
--[[
Use cases:
    local info = wlan.getapinfo()
    info.ssid      --Connection Hotspot Information ssid
    info.channel   --Channel to connect to the hotspot
    info.rssi      --Signal strength of the connection hotspot
    info.bssid     --MAC address of the connection hotspot
    info.security  --Safe mode for connecting to hotspots
    info.hidden    --Whether to broadcast ssid
]]

--Obtain information about a STA that joins a hotspot.
wlan.getJionApInfo()
--[[
Use cases:
    local num,info = wlan.getJionApInfo()
    num             --Number of station already connected
    info[num].ip    --station the ip address
    info[num].mac   --station The MAC address.
]]

--Status of the hotspot
wlan.ap_ready()

--Close Hotspot
wlan.ap_stop()

--Register interrupt function
wlan.on(EVT,fun)
--[[
Use cases:
    wlan.on(wlan.EVT_READY ,function() ... end)
    ...
]]

--Sets the power consumption level for station mode.
wlan.pw(level)

--Get Power Level
wlan.getpw()

```
