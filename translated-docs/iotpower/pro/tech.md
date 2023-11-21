# Technical indicators

- Maximum measuring range: maximum voltage 5V, maximum current 2A
- Use 0.4% ± 20ppm precision reference chip, reserve 0.1% reference position, 0.1% precision resistor, 12 bit ADC design. The nominal accuracy is 1% (typical value is 0.5), and the user can further calibrate the accuracy to reach 0.1%。
- Automatic measurement range: the current echo gears are 200 μA, 20mA and 2A respectively, which are automatically switched according to the current output current.
- Gear switching speed: Pro adopts multi-channel synchronous sampling, no shift delay.
- Voltage drop switching speed: Pro meter uses 2/3 channel sampling resistor, and the maximum delay when the sampling resistor is short-circuited is 100uS
- Current measurement accuracy: resolution in 200μA gear? μA, resolution in 20mA gear? μA (the following table)
- Fixed multi-channel 10KHz sampling rate, USB data transfer
- Can cooperate with PC client to view and analyze current waveform, can also use command line version to capture serial port data (such as strawberry pie) on any device (win/linux/mac), use pc client to import and view

|   Type | Range | Resolution | Nominal Accuracy | Actual Accuracy |
| :------: | :---: | :----: | :------: | :------: |
| Voltage setting | 0V-5V |  1mV   |    1%    |   <?%    |
| Voltage Readback | 0V-5V |  10mV  |    1%    |  <0.5%   |
| Current setting | 0-2A  |  1μA   |    1%    |   <?%    |

| Current Range | Range | Resolution | Nominal Accuracy | Actual Accuracy | Type   |
| :------: | :---------: | :----: | :------: | :------: | :------: |
|  200μA   | 0.1uA-200μA |  ?μA   |    1%    |  <0.5%   | Automatic gear |
|   20mA   | 200μA-20mA  |  ?μA   |    1%    |  <0.5%   | Automatic gear |
|    2A    |   20mA-2A   |  ?mA   |    1%    |   <1%    | Automatic gear |
