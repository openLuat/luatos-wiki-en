# Generate and manage luadb files

luadb It is one of the core file systems used by LuatOS and is used to store scripts and resource files for line brushes.

For a detailed description of the file system, see [LuaDB File System](../../develop/contribute/luadb.md)

## Load script from folder and generate luadb file

```
luatos-pc.exe F:\user_script\ --dump_luadb=disk.fs --norun=1 --luac_strip=0
```

Parameter Interpretation:

- `F:\user_script\` : Specify the folder where the script is located. All lua files will be loaded. Note that the end of '\' is required.
- `--dump_luadb=disk.fs` : Specify the file name of the build
- `--norun=1` : Do not execute script, exit after export
- `--luac_strip=0` : Whether to remove debugging information, 0 does not remove comments, 1 removes comments, but retains debugging information of main.lua, 2 removes all debugging information
