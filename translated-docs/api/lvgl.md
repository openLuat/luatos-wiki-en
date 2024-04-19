# lvgl - LVGL Image Library

{bdg-success}`Adapted` {bdg-primary}`Air780E/Air700E` {bdg-primary}`Air780EP` {bdg-primary}`Air601` {bdg-primary}`Air101/Air103` {bdg-primary}`Air105` {bdg-primary}`ESP32C3` {bdg-primary}`ESP32S3`

```{tip}
This library has its own demo,[click this link to view the demo example of lvgl](https://gitee.com/openLuat/LuatOS/tree/master/demo/lvgl)
```

## lvgl.draw_mask_radius_param_t()



Create a lv_draw_mask_radius_param_t

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|userdata|lv_draw_mask_radius_param_t Pointer|

**Examples**

```lua
local radius = lvgl.draw_mask_radius_param_t()

```

---

## lvgl.draw_mask_radius_param_t_free(radius)



Release one.lv_draw_mask_radius_param_t

**Parameters**

None

**Return Value**

None

**Examples**

```lua
local lvgl.draw_mask_radius_param_t_free(radius)

```

---

## lvgl.draw_mask_line_param_t()



Create a lv_draw_mask_line_param_t

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|userdata|lv_draw_mask_line_param_t Pointer|

**Examples**

```lua
local line = lvgl.draw_mask_line_param_t()

```

---

## lvgl.draw_mask_line_param_t_free(line)



Release one.lv_draw_mask_line_param_t

**Parameters**

None

**Return Value**

None

**Examples**

```lua
local lvgl.draw_mask_line_param_t_free(line)

```

---

## lvgl.draw_mask_fade_param_t()



Create a lv_draw_mask_fade_param_t

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|userdata|lv_draw_mask_fade_param_t Pointer|

**Examples**

```lua
local fade = lvgl.draw_mask_fade_param_t()

```

---

## lvgl.draw_mask_fade_param_t_free(fade)



Release one.lv_draw_mask_fade_param_t

**Parameters**

None

**Return Value**

None

**Examples**

```lua
local lvgl.draw_mask_fade_param_t_free(fade)

```

---

## lvgl.font_get(name)



Get Built-in Font

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Font name font size, for example opposans_m_10|

**Return Value**

|return value type | explanation|
|-|-|
|userdata|Font Pointer|

**Examples**

```lua

local font = lvgl.font_get("opposans_m_12")

```

---

## lvgl.font_load(path/spi_device,size,bpp,thickness,cache_size,sty_zh,sty_en)



Loading fonts from the file system

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string/userdata|Font path/spi_device (spi_device to use an external Qualcomm vector font chip)|
|number|size Optional, font size 16-192 defaults to 16 (use Qualcomm vector font)|
|number|bpp Optional Depth Default 4 (Use Qualcomm Vector Font)|
|number|thickness Optional weight value Default size * bpp (use Qualcomm vector font)|
|number|cache_size Optional default 0 (use Qualcomm vector font)|
|number|sty_zh OPTIONAL SELECT FONT DEFAULT 1 (USING QUALCOMM VECTOR FONT)|
|number|sty_en OPTIONAL SELECT FONT DEFAULT 3 (USING QUALCOMM VECTOR FONT)|

**Return Value**

|return value type | explanation|
|-|-|
|userdata|Font Pointer|

**Examples**

```lua
local font = lvgl.font_load("/font_32.bin")
--local font = lvgl.font_load(spi_device,16)(Qualcomm Vector Font)

```

---

## lvgl.font_free(font)



Release the font and use it with caution!!! Only fonts loaded by font_load are allowed to be unloaded, and fonts obtained by font_get are not allowed to be unloaded

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Font Path|

**Return Value**

|return value type | explanation|
|-|-|
|userdata|Font Pointer|

**Examples**

```lua
local font = lvgl.font_load("/font_32.bin")
-- N N N N Operation
-- Make sure the font is not used, not referenced, and the memory is tight and needs to be released.
lvgl.font_free(font)

```

---

## lvgl.obj_set_event_cb(obj, func)



Set event callbacks for components

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|lvgl Component Pointer|
|func|lua Function, there are 2 parameters (obj, event), where obj is the current object, event is the event type, which is integer|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## lvgl.obj_set_signal_cb(obj, func)



Set signal callbacks for components

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|lvgl Component Pointer|
|func|lua Function, there are 2 parameters (obj, signal), where obj is the current object, signal is the signal type, is integer|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## lvgl.anim_set_exec_cb(anim, func)



Set Animation Callback

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|Animated Pointer|
|userdata|lvgl Component Pointer|
|func|lua Function, there are 2 parameters (obj, value), where obj is the current object, signal is the signal type, is integer|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## lvgl.anim_set_ready_cb(anim, func)



Set Animation Callback

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|Animated Pointer|
|userdata|lvgl Component Pointer|
|func|lua function with 1 argument (anim), where anim is the current object|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## lvgl.anim_path_set_cb(path, func)



Set Animation Callback

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|Animated Pointer|
|userdata|lvgl Component Pointer|
|func|lua function with 1 parameter (path), where path is the current object|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## lvgl.event_send(obj, ent)



Send events to components

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|Component Pointer|
|int|event id, for example lvgl.EVENT_PRESSED|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, false if the object has been deleted, or nil|
|int|The underlying return value, if obj is nil nil|

**Examples**

None

---

## lvgl.scr_act()



Get the currently active screen object

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|pointer | screen pointer|

**Examples**

```lua
local scr = lvgl.scr_act()


```

---

## lvgl.layer_top()



Get layer_top

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|pointer | layer pointer|

**Examples**

None

---

## lvgl.layer_sys()



Get layer_sys

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|pointer | layer pointer|

**Examples**

None

---

## lvgl.scr_load(scr)



Load the specified screen

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|screen Pointer|

**Return Value**

None

**Examples**

```lua
    local scr = lvgl.obj_create(nil, nil)
    local btn = lvgl.btn_create(scr)
    lvgl.obj_align(btn, lvgl.scr_act(), lvgl.ALIGN_CENTER, 0, 0)
    local label = lvgl.label_create(btn)
    lvgl.label_set_text(label, "LuatOS!")
    lvgl.scr_load(scr)

```

---

## lvgl.scr_load_anim(scr)



Load the specified screen and use the specified transition animation

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|screen Pointer|

**Return Value**

None

**Examples**

```lua
    local scr = lvgl.obj_create(nil, nil)
    local btn = lvgl.btn_create(scr)
    lvgl.obj_align(btn, lvgl.scr_act(), lvgl.ALIGN_CENTER, 0, 0)
    local label = lvgl.label_create(btn)
    lvgl.label_set_text(label, "LuatOS!")

    local scr2 = lvgl.obj_create(nil,nil)
    local btn2 = lvgl.btn_create(scr2)
    lvgl.obj_align(btn, scr2, lvgl.ALIGN_CENTER, 0, 20)
    local label2 = lvgl.label_create(btn2)
    lvgl.label_set_text(label2, "Btn2")
    lvgl.scr_load(scr)
    --sys.wait(1000);
    lvgl.scr_load_anim(scr2,lvgl.SCR_LOAD_ANIM_OVER_LEFT,100,100,false)
primitive function：lv_scr_load_anim(lv_obj_t * new_scr, lv_scr_load_anim_t anim_type, uint32_t time, uint32_t delay, bool auto_del)

```

---

## lvgl.theme_set_act(name)



Set Theme

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Subject name, optional values are default/mono/empty/material_light/material_dark/material_no_transition/material_no_focus|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise nil|

**Examples**

```lua
-- Black and White Theme
lvgl.theme_set_act("mono")
-- Blank Theme
lvgl.theme_set_act("empty")

```

---

## lvgl.sleep(enable)



LVGL Sleep control, pause/resume refresh timer, currently only 105 and EC618 can be used

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|boolean|true Pause False Resume|

**Return Value**

None

**Examples**

```lua
lvgl.sleep(true)        --Pause refresh, system can sleep
lvgl.sleep(false)        --Resume refresh, system does not sleep

```

---

## lvgl.init(w, h, lcd, buff_size, buff_mode)



Initialization LVGL

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|screen width, optional, taken from LCD by default|
|int|Screen height, optional, default from LCD|
|userdata|lcd pointer, optional, default value after LCD initialization, reserved multi-screen entry|
|int|Buffer size, default width * 10, without color depth.|
|int|Buffer mode, default 0, single buff mode, optional 1, double buff mode|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise false|

**Examples**

None

---

## lvgl.anim_create()



Create and initialize a anim

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|userdata|anim Pointer|

**Examples**

```lua
local anim = lvgl.anim_create()

```

---

## lvgl.anim_free(anim)



Release one.anim

**Parameters**

None

**Return Value**

None

**Examples**

```lua
local lvgl.anim_free(anim)

```

---

## lvgl.anim_path_t()



Create a lv_anim_path_t

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|userdata|lv_anim_path_t Pointer|

**Examples**

```lua
local anim_path_t = lvgl.anim_path_t()

```

---

## lvgl.anim_path_t_free(anim_path_t)



Release one.lv_anim_path_t

**Parameters**

None

**Return Value**

None

**Examples**

```lua
local lvgl.anim_path_t_free(anim_path_t)

```

---

## lvgl.anim_set_path_str(anim, tp)



Set the animation path method

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|Animated Pointer|
|string|types, support linear/ease_in/ease_out/ease_in_out/overshoot/bounce/step|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## lvgl.qrcode_create(parent, size, dark_color, light_color)



Create the qrcode component

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|Parent Component|
|int|length, because qrcode is square|
|int|Color of data points in QR code, RGB color, default 0x3333ff|
|int|The color of the back scenic spot in the two-dimensional code, RGB color, default 0xeeeeff|

**Return Value**

|return value type | explanation|
|-|-|
|userdata|qrcode Components|

**Examples**

```lua
-- Create and display qrcode
local qrcode = lvgl.qrcode_create(scr, 100)
lvgl.qrcode_update(qrcode, "https://luatos.com")
lvgl.obj_align(qrcode, lvgl.scr_act(), lvgl.ALIGN_CENTER, -100, -100)

```

---

## lvgl.qrcode_update(qrcode, cnt)



set the qrcode component's qr code content and use it with the qrcode_create

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|qrcode components, created by qrcode_create|
|string|Content data of two-dimensional code|

**Return Value**

|return value type | explanation|
|-|-|
|bool|If the update is successful, it returns true, otherwise it returns false. It is usually returned only if the data is too long to accommodate.false|

**Examples**

None

---

## lvgl.qrcode_delete(qrcode)



Remove qrcode component

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|qrcode components, created by qrcode_create|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## lvgl.style_t()



Create a style

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|userdata|style Pointer|

**Examples**

```lua
local style = lvgl.style_t()
lvgl.style_init(style)

```

---

## lvgl.style_create()



Create a style and initialize

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|userdata|style Pointer|

**Examples**

```lua
local style = lvgl.style_create()

```

---

## lvgl.style_list_create()



Create a style_list

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|userdata|style Pointer|

**Examples**

```lua
local style_list = lvgl.style_list_create()

```

---

## lvgl.style_list_t()



Create a style_list

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|userdata|style Pointer|

**Examples**

```lua
local style = lvgl.style_list_t()

```

---

## lvgl.style_delete(style)



Delete style, use caution, usually do not perform delete operation

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|style Pointer|

**Return Value**

None

**Examples**

```lua
local style = lvgl.style_create()
-- ...
-- ...
-- lvgl.style_delete(style)

```

---

## lvgl.style_list_delete(style)



Delete style_list, use caution, usually do not perform delete operation

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|style Pointer|

**Return Value**

None

**Examples**

```lua
local style_list = lvgl.style_list_create()
-- ...
-- ...
-- lvgl.style_list_delete(style_list)

```

---

## lvgl.demo_benchmark()



lvgl benchmark demo

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
lvgl.init()
lvgl.demo_benchmark()

```

---

## lvgl.demo_keypad_encoder()



lvgl keypad_encoder demo

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
lvgl.init()
lvgl.demo_keypad_encoder()

```

---

## lvgl.demo_music()



lvgl music demo

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
lvgl.init()
lvgl.demo_music()

```

---

## lvgl.demo_printer()



lvgl printer demo

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
lvgl.init()
lvgl.demo_printer()

```

---

## lvgl.demo_stress()



lvgl stress demo

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
lvgl.init()
lvgl.demo_stress()

```

---

## lvgl.demo_widgets()



lvgl widgets demo

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
lvgl.init()
lvgl.demo_widgets()

```

---

## lvgl.indev_drv_register(tp, dtp)



Register Input Device Driver

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Device type, currently supports "pointer", pointer class/touch class, "keyboard", keyboard type|
|string|Device model, currently supports "emulator", emulator type|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise false|

**Examples**

```lua
lvgl.indev_drv_register("pointer", "emulator")

```

---

## lvgl.indev_point_emulator_update(x, y, state)



Update coordinate data for analog input devices

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|x Coordinates, with the upper left corner as 0 and the lower right corner as the maximum value|
|int|y Coordinates, with the upper left corner as 0 and the lower right corner as the maximum value|
|int|State, 0 is released, 1 is pressed|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
-- Simulate clicks on the screen, simulate long and short presses through the timeout
sys.taskInit(function(x, y, timeout)
    lvgl.indev_point_emulator_update(x, y, 1)
    sys.wait(timeout)
    lvgl.indev_point_emulator_update(x, y, 0)
end, 240, 120, 50)

```

---

## lvgl.indev_kb_update(key)



Update key values for keyboard input devices

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|Key value, default 0, key up|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

None

---

## lvgl.gif_create(parent, path)



create gif component

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|The parent component, which can be nil, but typically will not be nil|
|string|File Path|

**Return Value**

|return value type | explanation|
|-|-|
|userdata|The component pointer. If it fails, nil is returned. We recommend that you check|

**Examples**

```lua
local gif = lvgl.gif_create(scr, "S/emtry.gif")
if gif then
    log.info("gif", "create ok")
end


```

---

## lvgl.gif_restart(gif)



Replay the gif component

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|gif Component support, returned by the gif_create method|

**Return Value**

|return value type | explanation|
|-|-|
|nil|No return value|

**Examples**

```lua
local gif = lvgl.gif_create(scr, "S/emtry.gif")
if gif then
    log.info("gif", "create ok")
end


```

---

