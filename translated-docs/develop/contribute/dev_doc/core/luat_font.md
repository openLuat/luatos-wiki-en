# Font module

## Basic information

- Date of drafting: 2022-03-15
- Designer: [chenxuuu](https://github.com/chenxuuu)

## Known issues

At present, the font size of the existing font is not uniform, refresh is slow, and it is difficult to customize the font. Need a universal dot matrix font library

## Goals to be achieved

- The dot matrix information of a word in the specified font can be obtained through the C interface
- Use UTF16 encoding (fixed-length）
- Supporting the host computer, can easily generate font code to add. Try to make a web version
- Provides Lua interface to obtain dot matrix information of a font from Lua layer
- Support the way to load fonts from the file system, and the host computer should also support the generation of such font files.
- Support gray scale, to be determined, did not expect the implementation method
- Vector font, to be determined, did not expect the implementation method

## Solution

### lua interface

```lua
--Siyuan Blackbody 16px
local data,width,height = font.get(font.SOURCE_SANS_16,"Measurement")
local data,width,height = font.get(font.SOURCE_SANS_16,"Test")
```

### c Interface

```c
typedef struct luat_font {
    uint8_t* addr; //Address of data storage
    size_t start;  //Where coding starts
    size_t len;    //How many words altogether
    uint16_t width; //Font Width
    uint16_t height;//Font Height
} luat_font_t;

//The returned pointer points directly to the rom area free
uint8_t* luat_font_get(L_Font* font, uint16_t c, uint16_t* width, uint16_t* height, uint32_t* size);
```

## Relevant knowledge points

None
