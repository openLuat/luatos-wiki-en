# FOTA Upgrade and generation of upgrade package

## FOTA Upgrade

1. EC618 Series (typically Air780E/Air780EG/Air700E) and other modules, support FOTA upgrade, along with the underlying and script upgrade
2. When upgrading, the underlying differential package cannot exceed 480k, and the script area is the same size.
3. The size of the script area is determined when the firmware is compiled, regardless of the actual script size. Firmware with inconsistent script area sizes cannot be upgraded differentially
4. Differential upgrade requires the soc/binpkg file of the old and new firmware. Please keep the soc file of the old version properly for future use.
5. Please refer to the specific upgrade process demo/fota

## Upgrade Package Generation

1. LuaTools When a production file is generated, a. binfile for script generation and a.soc
2. If you need to generate an upgrade package containing bottom-layer differences, you need to use the "SoC Differential Upgrade Package" generation tool in the LuaTools menu.
3. In addition, a command line tool is provided to generate an upgrade package, which is located[luatos-soc-2022](https://gitee.com/openLuat/luatos-soc-2022/tree/master/tools/dtools)

## Uncovered matters please contact technical support

https://doc.openluat.com/wiki/37?wiki_page_id=4578

or contact sales
