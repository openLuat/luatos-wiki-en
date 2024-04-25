# SPI SLOVAL FUNCTION

XT804 Scheme with a SPI/SDIO slave function, support 4-wire SPI or SDIO mode

In view of the small number of modules supporting SDIO master, the SPI slave mode is mainly described here.

## Hardware

Take Air601/Air101 as an example.

|Host | Air601 | Description           |
|----------------|------|---------------|
|3.3v            | 3.3v | Power          |
|GND             | GND  | ground            |
|SPI_CLK         | PB06 | SPI Clock|
|GPIOx           | PB07 | Interrupt for slave data notification to the host, optional|
|SPI_CS          | PB09 | Film selection          |
|SPI_MOSI        | PB10 | Master-> Slave, Data Downstream|
|SPI_MISO        | PB11 | Master <-Slave, Data Upstream|

The highest communication rate is 50MHz, and the lowest is not limited (well, the actual situation is not measured-_-, it is recommended not to be lower 100k)

Corresponding to Air103, there is another multiplexing method as follows

|Host | Air601 | Description           |
|----------------|------|---------------|
|3.3v            | 3.3v | Power          |
|GND             | GND  | ground            |
|SPI_CLK         | PB12 | SPI Clock|
|GPIOx           | PB13 | Interrupt for slave data notification to the host, optional|
|SPI_CS          | PB14 | Film selection          |
|SPI_MOSI        | PB15 | Master-> Slave, Data Downstream|
|SPI_MISO        | PB16 | Master <-Slave, Data Upstream|

## Software

For detailed SPI slave communication protocol, please refer to [W800 register manual, right-click save as](https://www.winnermicro.com/upload/1/editor/1663831830972.pdf), chapter * * 10 high-speed SPI device controller**

Make a simple description here

Basic read-write rules

1. Pull low CS
2. Send read and write instruction (register address), 1 byte
3. Send/receive data, 2 or 4 bytes
4. Pull high CS

* Where, when the register address is 0x00/0x10/0x01/0x 11, the data is 4 bytes, otherwise it is 2 bytes
* The difference between reading and writing lies in the highest bit of the instruction, 0 is read and 1 is write.

### Read and write instructions

The complete table is in section **10.4.2 Host-Side Access HSPI Controller Register List**

Here are a few confusing points, especially the registers of data transmission.

* DATA Registers are 0x 00 and 0x 10, one is the start register and the other is the end register, which can write up to 1500 bytes and 4 bytes at a time.
* CMD Registers are 0x 10 and 0x 20, one is the start register and the other is the end register, which can write up to 256 bytes and 4 bytes at a time.
* The so-called DATA and CMD are divided into slave machines, not only data (DATA) or instructions (CMD) can be transmitted. The actual content is not limited. The slave machine is in different areas. There are callback parameters to distinguish the sources.
* Write data must be 4-byte aligned, not enough to complete
* If the total amount of data is 4 bytes, then write directly to the end register without writing to the start register.

1. To write data (DATA) to the slave, first write register 0x 00 and the last packet write 0x 10. Each time, 4 bytes should be written. If there are only 4 bytes of data in total, write register 0x 01 directly.
2. To write the instruction (CMD) to the slave, first write register 0x 10 and the last packet write 0x 11, each time writing 4 bytes

Take sending a 12-byte data array as an example.

The contents of the data array are {0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2A, 0x2B, 0x2C}

1. Pull low CS
2. Send 0x 00, one byte
3. Send 0x 20, 0x 21, 0x 22, 0x 23, four bytes
4. Pull high CS
5. Pull low CS
6. Send 0x 00, one byte
7. Send 0x 24, 0x 25, 0x 26, 0x 27, four bytes
8. Pull high CS
9. Pull low CS
10. Send 0x 10, one byte
11. Send 0x 28, 0x 29, 0x2A, 0x2B, four bytes
12. Pull high CS

## Recommended Programming Modes

1. Initialize SPI Host, Configure Clock Frequency, Mode, Chip Select Pin, Initialize Interrupt
2. Read the writable length register of the slave, calculate the writable length, and write data
3. Read the readable length register of the slave, calculate the readable length, and read the data.
4. Reciprocating

Because the readable and writable length does not represent the actual length, it is recommended to add a header to the front of the data to describe the length of the entire data packet, for example

```c
uint8_t magic; // 0xA5
uint8_t crc8; // check digit, calculated from after crc
uint16_t len; // Actual Data Length
```

## Sample project

* Air601 Do SPI network card, address[spi-net](https://github.com/wendal/xt804-spinet)
