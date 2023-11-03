# LuatOS Package Management Commands

In essence, it is to add, delete, change and check

## pkg install Install software packages

If the file exists, prompt the user whether to overwrite it, and use `-y` to directly overwrite it.

```bash
$> luatos pkg install aht10
> found version xxx.xx
> download ...
> installed
```

## pkg remove Delete Software Package

Prompt the user whether to delete, use `-y` to delete directly

```bash
$> luatos pkg remove aht10
> remove script/pkgs/aht10.lua ? [y/N]
y
> removed
```


## pkg update Update Package

If the file has been modified, prompt the user whether to overwrite it or not, and use `-y` to overwrite it directly.

```bash
$> luatos pkg update aht10
> found version xxx.xx
> download ...
> installed
```

## pkg query Query software package

```bash
$> luatos pkg query aht
> aht10 - 2.1.0 build 20220420_223344
> aht20 - 2.2.0 build 20220420_223344
```

