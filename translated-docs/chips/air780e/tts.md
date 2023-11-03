# TTS Description of the function

## Function introduction

TTS, That is, text-to-speech, especially offline TTS. It is not within the scope of discussion to call the cloud server API to generate mp3..

TTS 2 parts in firmware:
1. TTS Function code, which must be compiled into firmware to use TTS functions
2. TTS Resource data, which can be compiled into firmware or external SPI Flash

Because TTS resource data has 750k bytes, there will be differences in different types of firmware.
1. AT The firmware must be LSAT firmware to support TTS, that is, low-speed TTS firmware, and TTS resource data is also stored in the firmware.
2. CSDK Firmware, standard layout can be put down, generally put TTS resource data in firmware
3. LuatOS Firmware, only SPI Flash is supported before V1104, V1104 starts to support firmware, conditional support

API Call:
1. Firmware that supports TTS through external SPI Flash, with sfud and audio libraries, refer demo/tts
2. The firmware with embedded TTS resources, using audio firmware, is also a reference. demo/tts

## Obtain TTS-enabled firmware for LuatOS

1. [Cloud Compilation](../../develop/compile/Cloud_compilation.md)
2. In the LuaTools resource/ec618_lua_lod directory, there will be
3. Obtain from the LuatOS Unified Release Center https://gitee.com/openLuat/LuatOS/releases

Considerations for LuatOS Firmware with Embedded TTS Resource Data:
1. The script area will be reduced to 64k and is unlikely to support low-level upgrades, but script upgrades are supported
2. The UI library is not supported by default because it cannot be put down, but the firmware can still be compiled by itself and extreme operation can be tried.

## TTS Speech Parameter Rules

TTS The parameter adjustment of the voice is set by the special format of the played string.
1. The basic format is `[parameter XXX] Text to be played`
2. Some parameters support multiple use of different positions, such as `[parameter 1] to be played [parameter 2]123 text`

### Original format (select default parameters）

```lua
audio.tts(0, "Hello everyone, I am Xiao Zhou")
```

### Set Pronunciation

​Set code reference, the speaker can only set one at the same time.

```lua
--[[
 Format： [m*] (*=51~55)
 51 – for a long time
 52 – Many
 53 – Xiao Ping
 54 – Donald Duck
 55 – Xu Baobao 
]]
audio.tts(0, "[m52]Hello everyone, I am Xiao Zhou")
```

### Set pronunciation style

```lua
--[[
Format： [f*] (*=0/1/2)
Parameters: 0-word meal
      1 – Plain and straightforward
      2 – Colorful
Description: The default is flat and straightforward style.
]]
audio.tts(0, "[f1]Hello everyone, I am Xiao Zhou")
```

### Select language

```lua
--[[
Format： [g*] (*=0/1/2)
Parameter: 0-automatic judgment
	  1 – Mandarin Chinese
	  2 – English Language
Note: The default language is automatic judgment.
]]
audio.tts(0, "[g0]Hello everyone, I am Xiao Zhou")
```

### Set Digital Processing Policy

```lua
--[[
Format： [n*] (*=0/1/2)
Parameter: 0-automatic judgment
      1 – Number processing
      2 – Digital for numerical processing
Description: automatic judgment is set by default.
]]
audio.tts(0, "[n1]Hello everyone 12312121212")
```

### English Number 0 Reading Settings


```lua
--[[
Format： [o*] (*=0/1)
Parameters: 0-English number 0 read to do“O”
	  1 – English Number 0 Read“zero”
(The default is the English number 0 read to do)“zero”。
Note: The mark will only take effect when 0 is read aloud as a number. When 0 is processed as a numerical value, it will always be read zero。
]]
audio.tts(0, "[o1]Hello everyone 12312121212")
```

### Mute for a while

```lua
--[[
Format: [p *] (* = unsigned integer)
Parameter: *-duration of mute, unit: millisecond(ms)
]]
audio.tts(0, "[p2000]Hello everyone, I am Xiao Zhou")
```

### Set SPEED

```lua
--[[
Format： [s*] (*=0~10)
Parameters： * – 0-10
Note: The default speech speed value is 5, and the adjustment range of speech speed is half to twice the default speech speed, that is, the value of 0 is half slower than the default speech speed, and the value of 10 is twice as fast as the default speech speed.
]]
audio.tts(0, "[s5]Hello everyone, I am Xiao Zhou")
```

### set intonation

```lua
--[[
Format： [t*] (*=0~10)
Parameter: *-The intonation value corresponds to the parameter setting value of 6553 * (value -5), that is, 0 corresponds to -32765,5 corresponds to 0, and 10 corresponds+32765
Note: The default intonation value is 5, and the adjustment range of intonation is 64Hz to the top of the default intonation fundamental frequency. 128Hz。
]]
audio.tts(0, "[t5]Hello everyone, I am Xiao Zhou")
```

### Set Volume

```lua
--[[
Format： [v*] (*=0~10)
Parameters: *-the volume value corresponds to the parameter setting value of 6553 * (value -5), I .e. 0 corresponds to -32765,5 corresponds to 0, and 10 corresponds+32765。
Note: The volume adjustment range is mute to the maximum value supported by the audio device, and the default value is 5 for the middle volume.
]]
audio.tts(0, "[v5]Hello everyone, I am Xiao Zhou")
```

### Set the pronunciation of "1" in Chinese numbers

```lua
--[[
Format： [y*] (*=0/1)
Parameter: 0-Synthesize number when "1" is read“yāo”
      1 – When synthesizing numbers, "1" is read“yī”
Description: The default synthesis number "1" is read“yāo”。
]]
audio.tts(0, "[y1]110 Hello everyone[y0]110")
```
