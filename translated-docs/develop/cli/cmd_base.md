# LuatOS CLI Basic Commands

This document describes some simple and basic commands

* help      Help information
* version   Version Information
* upgrade   Update Version

## help Help information

Print help content when there is no command

```bash
$> luatos help
> LuatOS-CLI v2.1.0 build 20220419_220202
> usage:
>   help        Help information
>   version     Version Information
>   upgrade     Update itself
>   prj         Project Management
>   pkg         Package Management
```

## version Display version number

```bash
$> luatos version
> LuatOS-CLI v2.1.0 build 20220419_220202
```

## upgrade Update Program

```bash
$> luatos upgrade -y
> checking ...
> found xxx
> download ...
> finish
```


