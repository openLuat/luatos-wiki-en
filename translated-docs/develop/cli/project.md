# LuatOS User Project

## Overview of project structure

Contents required by the project shall be stored as required.

```bash
$ROOT                       # [Required] Items and Catalogues

                            #>>>>> User's own files
    - src                   # [Required] User Directory
        - script            # [Required] Script File
            - main.lua      # [Required] main lua entry file
            - myfunc.lua    # [Can] user-defined functions
        - resource          # [Can be] non-scripted resource files
            - logo.bin      # [Can] Picture Example
            - myfont.bin    # [Can] font example
        - pkgs              # [Can] library functions, this part is jointly managed by cli and users
            - aht10.lua     # [Examples of available] library files

    - lod                   # [Can] Firmware Directory
        - air101            # [Can] Device Type
            - LuatOS-SoC_Air101_V0008.soc   # Specific Firmware Files

    - test                  # [Can] test directory
        - test_json.lua     # [Can] unit test scripts

                            #>>>>> git Related Documents
    - .git                  # [Can] git directory, actively guide users to use git management code
    - .gitignore            # [Can] provide default git ignore files and ignore temporary files

                            #>>>>> luatos cli Associated Files
    - .luatos               # [Required] LuatOS project configuration directory
        - conf.json         # [Required] Master Configuration File
        - tmp               # [Can] temporary directory
        - pkgs
            - metas.json    # [Can] library function list to avoid scanning every time
            - gitee         # [Can] store rule namespace/package name
                - openLuat
                    - LuatOS
                        - aht10 # 
                            - REAMDE.md
                            - src
                                aht10.lua
                        - tm1638
                            - REAMDE.md
                            - src
                                aht10.lua
        - demos             # [Can] support quick demo examples
            - gpio          # [Can] light up demo
                - README.md
                - src
                    - main.lua 
```

## 
