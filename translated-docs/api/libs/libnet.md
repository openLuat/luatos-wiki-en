# libnet - libnet synchronous blocking api based on the socket library, the socket library itself is asynchronous non-blocking api

{bdg-secondary}`Adaptation status unknown`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../script/libs/libnet.lua). If there is any error, please submit issue or help modify pr, thank you！
```


## libnet.waitLink(taskName,timeout,...)



Blocking the network connection waiting for the network card can only be used in the task function created by sysplus.taskInitEx

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Mission Flag|
|int|Timeout, if = = 0 or null, there is no timeout for a consistent wait|
|...|Other parameters are consistent with socket. linkage|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Failure or timeout returns false returned on success true|

**Examples**

None

---

## libnet.connect(taskName,timeout,...)



Blocking and waiting for IP or domain name connections. If the encrypted connection has to wait for the handshake to complete, it can only be used in task functions created by sysplus.taskInitEx

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Mission Flag|
|int|Timeout, if = = 0 or null, there is no timeout for a consistent wait|
|...|Other parameters are consistent with socket.connect|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Failure or timeout returns false returned on success true|

**Examples**

None

---

## libnet.listen(taskName,timeout,...)



Blocking waiting on client connections can only be used in task functions created by sysplus.taskInitEx

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Mission Flag|
|int|Timeout, if = = 0 or null, there is no timeout for a consistent wait|
|...|Other parameters are consistent with socket.listen|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Failure or timeout returns false returned on success true|

**Examples**

None

---

## libnet.tx(taskName,timeout,...)



Blocking waits for data to be sent. It can only be used in task functions created by sysplus.taskInitEx

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Mission Flag|
|int|Timeout, if = = 0 or null, no timeout waits|
|...|Other parameters are the same as socket.tx|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Failure or timeout returns false, buffer full or success true|
|boolean|Whether the cache is full|

**Examples**

None

---

## libnet.wait(taskName,timeout, netc)



Blocking and waiting for new network events can only be used in task functions created by sysplus.taskInitEx, and can be forced to exit by sysplus.sendMsg(taskName,socket.EVENT,0) or sys_send(taskName,socket.EVENT,0)

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Mission Flag|
|int|Timeout, if = = 0 or null, there is no timeout for a consistent wait|
|userdata|socket.create Return netc|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Network exception returns false, other returns true|
|boolean|Timeout returns false, there are new network events to return true|

**Examples**

None

---

## libnet.close(taskName,timeout, netc)



Blocking waiting for network disconnection, can only be used in task functions created by sysplus.taskInitEx

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|Mission Flag|
|int|Timeout, if = = 0 or null, there is no timeout for a consistent wait|
|userdata|socket.create Return netc|

**Return Value**

None

**Examples**

None

---
