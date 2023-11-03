# Technical indicators

- Maximum measuring range: voltage maximum 24V, current maximum 5A
- Use 0.4% ± 20ppm precision reference chip, reserve 0.1% reference position, 0.1% precision resistor, 12 bit ADC design. The nominal accuracy is 1% (typical value is 0.5), and the user can further calibrate the accuracy to reach 0.1%。
- Automatic measurement range: the current echo gears are 500 μA, 50mA and 5A respectively, which are automatically switched according to the current output current.
- Gear switching speed: CC table adopts multi-channel synchronous sampling, no shift delay.
- Voltage drop switching speed: CC meter uses 2/3 channel sampling resistor, and the maximum delay when the sampling resistor is short-circuited is 100uS
- Current measurement accuracy: resolution 0.15 μA in 500μA gear and 100 μA in 50mA gear (table below)
- Fixed multi-channel 10KHz sampling rate, USB high-speed data transfer
- Can cooperate with PC client to view and analyze current waveform, can also use command line version to capture serial port data (such as strawberry pie) on any device (win/linux/mac), use pc client to import and view

|   Type | Range | Resolution | Nominal Accuracy | Actual Accuracy    |
| :------: | :----: | :----: | :------: | :-----------: |
| Voltage Setting | 0V-21V | 20mV | 2% | PD Tricking PPS Decision |
| Voltage Readback | 0V-24V |  10mV  |    1%    |     <0.5%     |
| Current Setting | 0-3A | 50mA | 2% | PD Tricking PPS Decision |

| Current Range | Range | Resolution | Nominal Accuracy | Actual Accuracy | Type   |
| :------: | :---------: | :----: | :------: | :------: | :------: |
|  500μA   | 0.1uA-500μA | 0.15μA |    1%    |  <0.5%   | Automatic gear |
|   50mA   | 500μA-50mA  |  15μA  |    1%    |  <0.5%   | Automatic gear |
|    5A    |   50mA-5A   | 1.5mA  |    1%    |   <1%    | Automatic gear |

<script>
if (navigator.language.indexOf("CN") < 0 && confirm ("Are you want to switch to English version of this page?")) {
    window.location.href = "tech-en.html";
}
</script>
