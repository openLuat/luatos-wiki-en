# sysplus - sys A strong complement to the library

{bdg-success}`Adapted` {bdg-primary}`Air780E` {bdg-primary}`Air780EP` {bdg-primary}`Air780EPS` {bdg-primary}`Air780EQ` {bdg-primary}`Air700EAQ` {bdg-primary}`Air700EMQ` {bdg-primary}`Air700ECQ` {bdg-primary}`Air201`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/modules/luat_lib_sysplus_doc.c). If there is any error, please submit issue or help modify pr, thank you！
```


**Example**

```lua
-- This library is a supplement to the sys library, adding the following content:
-- 1. cwait Mechanism
-- 2. Task message mechanism, an enhanced version of sub/pub

-- It is needed in socket,libnet,http library and other scenarios, so it is also needed.require

```

## sysplus.waitMsg(taskName, target, timeout)



Waiting to receive a target message

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|The task name, which wakes up the task id|
|string|The target message, if nil, indicates that any message received will exit|
|int|The timeout period. If it is nil, it means that there is no timeout and it waits forever.|

**Return Value**

|return value type | explanation|
|-|-|
|table|successful return table type msg, timeout return false|

**Examples**

```lua
-- Wait for task
sysplus.waitMsg('a', 'b', 1000)
-- Note that this function is automatically registered as a global function sys_wait

```

---

## sysplus.sendMsg(taskName, target, arg2, arg3, arg4)



Send a message to the target task

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|The task name, which wakes up the task id|
|any|The parameter 1 in the message is also in the waitMsg.target|
|any|Parameters in the message 2|
|any|Parameters in the message 3|
|any|Parameters in the message 4|

**Return Value**

|return value type | explanation|
|-|-|
|bool|Returns true on success, otherwise false|

**Examples**

```lua
-- Send message to task a, target B
sysplus.sendMsg('a', 'b')
-- Note that this function is automatically registered as a global function sys_send

```

---

## sysplus.taskInitEx(fun, taskName, cbFun, ...)



Create a task thread, call the function at the end of the module and register the task function in the module, and main.lua can import the module.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|function|The name of the task function, which is called when the resume wakes up.|
|string|The task name, which wakes up the task id|
|function|Callback function when non-target message is received|
|any|... Variable parameters of the task function fun|

**Return Value**

|return value type | explanation|
|-|-|
|number|Returns the thread number of the task|

**Examples**

```lua
sysplus.taskInitEx(task1,'a',callback)

```

---

## sysplus.taskDel(taskName)



Delete task threads created by taskInitEx

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|The task name, which wakes up the task id|

**Return Value**

None

**Examples**

```lua
sysplus.taskDel('a')

```

---

## sysplus.cleanMsg(taskName)



clear the message queue of the specified task.

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Task Name|

**Return Value**

None

**Examples**

```lua
sysplus.cleanMsg('a')

```

---

