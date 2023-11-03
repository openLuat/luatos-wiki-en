# LuatOS dot matrix font format

In order to meet the character display requirements of U8G2/EINK/LCD/LVGL, the problem of waste of multiple font formats is solved.

## constraint condition

* Try to be constant in memory and can be stored in ROM
* The mapping table is stored separately from the dot matrix data, and multiple dot matrix data are supported using the same mapping table.

Only two relative widths of dot matrix data are defined, corresponding to `half-angle` and `full-angle` respectively.`

Let `row heet` be `x`, and` x` is always an even number, the width of` half-width `character is` X/2`, and the width of` half-width `character is `X`

And the addressing always looks up by `unicode`

## data structure

Data table, the data is always arranged in the corresponding font_map order.

```c
typedef struct luat_font_data {
    uint8_t map_type;  // Type data, detailed description later
    uint8_t unicode_w; // map The byte size of the table characters, which can be 2,4
    uint16_t count;    // Number of characters
    uint32_t unicode_min;
    uint32_t unicode_max;
    // uint32_t reserved; // reserved area, for extension, default 0x0000
}luat_font_data_t;
```

File Structure


```c
    uint8_t magic;                 // Always 0xC5
    uint8_t version;               // Currently 0x0001
    uint8_t font_w;                // Font Size
    uint8_t access_mode         : 4;   // Access Mode
    uint8_t font_data_count     : 4;   // The total number of data, usually 1 or 2, not many.

    // uint32_t reserved; // reserved area, for extension, default 0x0000
    luat_font_data_t datas[font_data_count];

    // The following is the difference between the file and the ROM
    // When the file is stored, the data is sorted in turn.
    // When compiled into ROM, the following 4 are pointers
    uint8_t font_map[font_data_count]; // For example, when the font_data_count is 2
                                       // rom mode, uint8_t* font_map[2]
                                       // file In the mode, to uint8_t font_map[map length corresponding to map0], uint8_t font_map[map length corresponding to map1]

    uint8_t font_data[font_data_count]; // Same with map.
```

Agreement:

map_type Represents the type of the mapping table

* 0x0001 Custom map data
* 0x0002 ASCII The basic mapping table 0x 20 ~ 0xFE, because it is continuous, does not need to be built-in.
* 0x0003 GB2312 Full table, built in the source code, the role is to generate font files can be omitted
* 0x0004 Emotional package, 3-byte unicode, mapped to 4-byte (pending)
