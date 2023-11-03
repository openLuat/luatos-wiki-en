# LuatOS-SoC Common Firmware Format soc

LuatOS-SoC In the future, a lot of SoC/MCU chips will be adapted, and the brush formats of various devices are different, so it is necessary to define a unified external format.

This is called soc format, the suffix is selected as soc, and the actual content is info.json and a compressed package of multiple firmware files, either zip or 7z

## About the definition of firmware

* Original firmware, the firmware format provided by the manufacturer, named after fls/pac/img, etc. Use the tools provided by the manufacturer to brush the machine.
* SoC Firmware, on the basis of the original firmware, add info.json, script.bin and other LuatOS-SoC-specific files, and make a compressed package, use LuaTools/LuatIDE to brush the machine

## SoC Contents of the firmware

* [Required] Included Files info.json
* [Optional] script data storage file script.bin, stored in LuaDB v2 format
* [Required] Original firmware, based on different SoC chips

## info.json The content

```json
{
    "version" : 1, // Firmware format version number
    "chip" : {     // Characteristics of the chip itself
        "type" : "air101", // air101
        "ram"  :  {       // Memory Information
            "total" : 288, // Total Memory Information
            "sys" : 64,    // System Memory Size
            "lua" : 176    // Lua Virtual machine available memory
        }
    },
    "bl": {// bl Information
    	"file" : "bootloader.bin"// bl File
    },
	"partition": {// Partition Table Information
    	"file" : "partition-table.bin"// partition table file
    },
    "rom": {        // rom Information
        "file" : "AIR101.fls",// Download Firmware
        "fs" : {    // File system information
            "script" : {
                "offset" : "81E0000",// Offset
                "size" : 112,   // Size
                "type" : "luadb"// File System Format
            }
        },
        "version-core": "v0007",// Master library code version
        "version-bsp" : "v0004",// bsp Version
        "mark" : "default",
        "build" : {
            "build_at" : "",
            "build_by" : "",
            "build_host" : "",
            "build_system" : ""
        }
    },
    "script" : {// Script Information
        "file" : "script.bin",// Script file
        "lua" : "5.3",// lua Version
        "bitw" : 32,
        "use-luac" : true,
        "use-debug" : true,
    },
    "fs" : {
		"total_len" : 448,
		"format_len" : "1000"
	},
    "user" : {
        "project" : "",
        "version" : "",
        "log_br" : "921600"// Default Print Baud Rate
    },
    "download" : {
        "bl_addr" : "ffffffff",// bl Address
		"partition_addr" : "ffffffff",// Partition Table Address
		"core_addr" : "0x00000000",// Firmware Address
		"app_addr" : "0x00000000",// APP Address
		"script_addr" : "0x81E0000",// File system address
        "nvm_addr" : "00000000",// nvm Address
		"fs_addr" : "00390000",// fs Address
        "force_br" : "2M",// Default download baud rate
        "extra_param" : "002f0200"
	}
}
```
