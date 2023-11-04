
````{figure} _static/logo-big.svg
---
alt: LuatOS logo
align: center
---
**Welcome LuatOS**

[![](https://gitee.com/openLuat/LuatOS/badge/star.svg?theme=gvp)](https://gitee.com/openLuat/LuatOS)
[![](https://img.shields.io/badge/Lua-5.3-blue)](https://gitee.com/openLuat/LuatOS)
[![](https://img.shields.io/badge/license-MIT-green)](https://gitee.com/openLuat/LuatOS/blob/master/LICENSE)
````

LuatOS It is a script running framework for embedded, which can greatly improve development efficiency.  
Using Lua 5.3 as the main language, it is optimized for the embedded environment with less resources, which greatly improves the operation efficiency.  
Powerful embedded Lua Engine for IoT devices, with many components and low memory requirements (16K RAM, 128K Flash)

::::{grid} 1 2 2 3
:gutter: 1 1 1 2

:::{grid-item-card} {octicon}`rocket;1.5em;sd-mr-1` Quick Start
:link: boardGuide/roadmap
:link-type: doc

Beginner-friendly tutorial, while taking into account the veteran, text and video tutorials can let you quickly get started.

+++
[Learn more »](boardGuide/roadmap)
:::

:::{grid-item-card} {octicon}`tools;1.5em;sd-mr-1` API Manual
:link: api/index
:link-type: doc

LuatOS Provides a wealth of peripheral interface library, can be more convenient to achieve creative, do not need to care about the underlying implementation.

+++
[Learn more »](api/index)
:::

:::{grid-item-card} {octicon}`stack;1.5em;sd-mr-1` chip selection
:link: chips/chips
:link-type: doc

Look at the functions of the chips supported by LuatOS at this stage and choose the chip that suits you.

+++
[Learn more »](chips/chips)
:::

::::

---

```{rubric} More information
```

- [Chip Data](chips/index)
- [Lua Native API Manual](https://wiki.luatos.org/_static/lua53doc/index.html)
- [Simulator](pages/emulator)
- [Development Board Purchase](https://luat.taobao.com)
- [B Station video tutorial](https://space.bilibili.com/532832)

---

```{rubric} Modules and chips that have been adapted so far
```

|Model | Classification | Remarks|
|--------|--------|-------|
|[Air780E](chips/air780e/index) |4G-Cat.1 Mobile communication module | Also supported Air780EG/Air600E|
|[Air101](chips/air101/index) |MCU|Small size, only 4*4mm|
|[Air103](chips/air103/index) |MCU|io More, support.psram|
|[Air105](chips/air105/index) |MCU|Large memory, camera, camera USB|
|[ESP32](chips/esp32c3/index) Series | (wifi/bt chip) | ESP32C3/ESP32S3 etc|
|[win32](chips/win32) |win32 version of LuatOS | available as an emulator|
|[RT-Thread](https://github.com/openLuat/luatos-soc-rtt) |memory needs to meet minimum requirements | merged into rtt main line|
|[Air302](chips/air302/index) |nbiot Module| EOL|
|[Air640w](chips/air640w/index) |wifi Module|  EOL|

```{note}
We are adapting more MCU and wireless chip modules, welcome you to join^_^
```



<style type="text/css">
    .chatlink {
        position: fixed;
        z-index: 2147483645;
        width: auto;
        font-size: 16px;
        line-height: 24px;
        top: 60px;
        right: 100px;
        color: #19caa6;
        text-align: center;
        border-top-left-radius: 5px;
        border-top-right-radius: 5px;
        border-bottom-left-radius: 5px;
        border-bottom-right-radius: 5px;
    }
</style>
<div class="chatlink" id="chatlink">
    <button onclick="window.open('https://chat.openluat.com')">The document was not resolved, the forum sent a post.！</button><p/>
</div>
<script>
if (location.href.indexOf("https://wiki.luatos.org") == 0 ) {
  document.getElementById('chatlink').style.cssText = "display: none";
}
</script>

```{toctree}
:hidden:
:caption: 💁 LuatOS Introduction
🏠️ Home Page <https://wiki.luatos.org>
pages/emulator
pages/tools
pages/supports
```

```{toctree}
:hidden:
:caption: 🌠 Quick to get started
luaGuide/index
chips/index
peripherals/index
boardGuide/index
Firmware Download <https://gitee.com/openLuat/LuatOS/releases>
```

```{toctree}
:hidden:
:caption: 📖 Reference Manual
api/index
api/sys_pub
🌕 Native API Manual <https://wiki.luatos.org/_static/lua53doc/index.html>
api/libs/index
```

```{toctree}
:hidden:
:caption: 🖥️ Kernel Development
develop/compile
develop/docs
develop/contribute/index
```

```{toctree}
:hidden:
:caption: 💼 actual combat reference
appDevelopment/index
```

```{toctree}
:hidden:
:caption: 🗄️ Other information
iotpower/index
archives
```
