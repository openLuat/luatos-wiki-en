# LuaDB File Format

LuaDB Not a database, but a file packaging format widely used for LuatOS firmware.

It acts as a read-only file system..

## File Format Definition

```c
typedef struct header_tld {
    uint8_t type;
    uint8_t len;
    uint8_t data[len]; // length depends on previous len value
}header_tld_t;

typedef struct file_tld {
    uint8_t type;
    uint32_t len;
    uint8_t data[len];
}file_tld_t;

typedef struct luadb {
    header_tld_t[] headers;
    file_tld_t[] files;
}luadb_t;
```

### header_tld.type Value and meaning

* 0x01 The file header len is fixed to 4, the value is 0xA5 0x5A 0xA5 0x5A, and it is fixed to the first one in the header area TLD
* 0x02 The version number len is fixed to 2 and the value is 0x00 0x02
* 0x03 The head length len is fixed to the total length of headers, uint32_t
* 0x04 The number of files len is fixed to 2, and the number of data in the file area, uint16_t
* 0xFE The check value len is fixed to 2, CRC16. This is the last one in the header area.TLD

Although positions other than 0x01/0xFE by definition can be arbitrary, the actual implementation is usually given in the order listed above..

### file_tld.type Value and meaning

* 0x01 Magic number, len is fixed to 4, value is 0xA5 0x5A 0xA5 0x5A
* 0x02 The file name len is not fixed, the value is the file name data
* 0x03 The file data length len is 4, which is different from others. The content is the length of the file data, uint32_t, and then the file data.
* 0xFE The check value len is fixed 2, CRC16

### Special agreement

* At the end of the file area is a file named ".airm2m_all_crc#.bin", and the content is the hex value of md5 of all data before the current file data..


## Fixed Limits for Formatting

* The number of files is limited to 1024 files