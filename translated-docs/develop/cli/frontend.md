# CLI Interactive Conceptual Design

## Terms and Definitions

* Scenario-User Story or Use Assumption
* `$> ` Both are commands that require the user to type in.
* `> `  command output or wait for additional user input 
* `#`   Scene Notes

## Just install the toolkit, type in the command directly

* The user obtains it through installation/copy, etc.LuatOS
* Usually, you will double-click the executable program without brain, and consider the pop-up window or pop-up.cmd
* If luatos is executed without parameters in cmd, help information will be output and the user will be prompted by default for the most likely scenario-building a project and flashing lights.

```bash
$> luatos
> LuatOS CLI 1.0.1 build 2022-04-30
> =================================
> Available Commands:
>     prj       Project Management
>     burn      brush machine
>     pkg       Package Management
>     check     Detection
>     update    Program Version Update
>     wiki      Document Query

For example:
Create a new Air101 project and brush the machine, please type the following commands in turn and follow the prompts
luatos prj init
luatos burn
```

## Create the project as prompted and brush the machine

```bash
# The prompt of the previous scene, the user knocks out the command to create the project, and prompts to brush or simulate the machine after creation.
$> luatos prj init
> Please enter the device type[air101]:
>> air101
> Project initialization
> Project initialization complete
> The project is ready and can be run by luatos burn or luatos mock for simulation.

# There must be no device inserted, no driver installed, boot to the page
$> luatos burn
> Serial port not detected, please check power supply and install driver http://xxx

# Perform fully automatic brushing process and prompt one by one
$> luatos burn
> Serial port detected COM8
> Check the script and synthesize the brush file.
> Start brushing: xxx
> The machine is successfully swiped, please use the serial port assistant to view the log.
```

## Try various demo

* After testing the flashing lights, the customer's most likely idea is to change to a demo to test.

```bash
# Update demo
$> luatos demo update
> led       Lighting
> ssd1306   i2c Screen   
> st7735    spi Screen
$> luatos burn demo led
> Please wire XX XX XX
> Please make sure the device is powered
> Please enter to confirm the brush
> Brush process XXX YYY ZZZ
```

## Try a library function

* After trying demo, users may have some peripherals on hand and some libraries need to be downloaded.

```bash
# fuzzy query
$> luatos pkg search ath
> ath10 -- version 2.1.0 sha256 XXXX
> ath20 -- version 2.1.0 sha256 XXXX
# Install as required
$> luatos pkg install ath10
> No ath10 locally, start downloading
> Downloading
> Download complete
> Simple Usage XXX XXX
>         YYY YYY
>         ZZZ ZZZ
# Check what is currently installed
$> luatos pkg list
> ath10 -- version 2.1.0 sha256 XXXX
> ds18b20 -- version 2.2.0 sha256 XXXX
```

## Packaging and Mass Production

TODO
