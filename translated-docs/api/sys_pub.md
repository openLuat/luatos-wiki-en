# 📮 sys System Messages


Here is a list of system messages that come with the LuatOS framework



## touchkey



[touchkey Interface Documentation Page](https://wiki.luatos.org/api/touchkey.html)



### TOUCHKEY_INC

Touch key messages

**Additional return parameters**

|return parameter type | explanation|
|-|-|
|number|port, Sensor id|
|number|state, Counter, touch count|

**Examples**

```lua
sys.subscribe("TOUCHKEY_INC", function(id, count)
    -- Sensor id
    -- Counter, touch count
    log.info("touchkey", id, count)
end)

```

---

## keyboard



[keyboard Interface Documentation Page](https://wiki.luatos.org/api/keyboard.html)



### KB_INC

Keyboard Matrix Messages

**Additional return parameters**

|return parameter type | explanation|
|-|-|
|number|port, keyboard id Currently fixed to 0, can be ignored|
|number|data, keyboard Keys need to be parsed in conjunction with init's map.|
|number|state, Key state 1 is pressed, 0 is released|

**Examples**

```lua
sys.subscribe("KB_INC", function(port, data, state)
    -- port Currently fixed to 0, can be ignored
    -- data, Need to cooperate with init map for parsing
    -- state, 1 is pressed, 0 is released
    log.info("keyboard", port, data, state)
end)

```

---

## sys



[sys Interface Documentation Page](https://wiki.luatos.org/api/sys.html)



### Starts with 0x 01 as the first byte

for system messaging inside luatos

**Additional return parameters**

|return parameter type | explanation|
|-|-|
|args|Data returned|

**Examples**

```lua
--This is a message for internal use of the system, do not use it externally

```

---

## pm



[pm Interface Documentation Page](https://wiki.luatos.org/api/pm.html)



### DTIMER_WAKEUP

deep sleep timer Timed time to callback

**Additional return parameters**

None

**Examples**

```lua
sys.subscribe("DTIMER_WAKEUP", function(timer_id)
    log.info("deep sleep timer", timer_id)
end)

```

---

## w5500



[w5500 Interface Documentation Page](https://wiki.luatos.org/api/w5500.html)



### IP_READY

Networked

**Additional return parameters**

None

**Examples**

```lua
-- This message will be sent once after networking.
sys.subscribe("IP_READY", function(ip, adapter)
    log.info("w5500", "IP_READY", ip, (adapter or -1) == socket.LWIP_GP)
end)

```

---

### IP_LOSE

The network has been broken

**Additional return parameters**

None

**Examples**

```lua
-- This message will be sent once after the network is cut off.
sys.subscribe("IP_LOSE", function(adapter)
    log.info("w5500", "IP_LOSE", (adapter or -1) == socket.ETH0)
end)

```

---

### W5500_IND

w5500 State change

**Additional return parameters**

None

**Examples**

```lua
sys.subscribe("W5500_IND", function(status)
    -- status The value of is:
    -- CABLE_INSERT Network cable insertion
    -- CABLE_REMOVE Pull out the network cable
	-- DHCP_TIMEOUT Get IP Timeout
    log.info("w5500 status", status)
end)

```

---

## libgnss



[libgnss Interface Documentation Page](https://wiki.luatos.org/api/libgnss.html)



### GNSS_STATE

GNSS State change

**Additional return parameters**

None

**Examples**

```lua
sys.subscribe("GNSS_STATE", function(event, ticks)
    -- event Values are
    -- FIXED Positioning success
    -- LOSE  LOST POSITIONING
    -- ticks is the time of the event and can generally be ignored
    log.info("gnss", "state", event, ticks)
end)

```

---

## mobile



[mobile Interface Documentation Page](https://wiki.luatos.org/api/mobile.html)



### SIM_IND

sim Card status change

**Additional return parameters**

None

**Examples**

```lua
sys.subscribe("SIM_IND", function(status, value)
    -- status The value of is:
    -- RDY SIM Card is ready, value is nil
    -- NORDY No SIM card, value is nil
    -- SIM_PIN PIN required, value is nil
    -- GET_NUMBER Get the phone number (not necessarily a value), value is nil
    -- SIM_WC SIM The number of times the card is written, power failure returns to 0, and value is the statistical value.
    log.info("sim status", status, value)
end)

```

---

### CELL_INFO_UPDATE

Base station data updated

**Additional return parameters**

None

**Examples**

```lua
-- Subscription
sys.subscribe("CELL_INFO_UPDATE", function()
    log.info("cell", json.encode(mobile.getCellInfo()))
end)

```

---

### SCELL_INFO

Cell Extra Information Update

**Additional return parameters**

None

**Examples**

```lua
-- Subscription
sys.subscribe("SCELL_INFO", function()
    log.info("service cell", mobile.scell()))
end)

```

---

### IP_READY

Networked

**Additional return parameters**

None

**Examples**

```lua
-- This message will be sent once after networking.
sys.subscribe("IP_READY", function(ip, adapter)
    log.info("mobile", "IP_READY", ip, (adapter or -1) == socket.LWIP_GP)
end)

```

---

### IP_LOSE

The network has been broken

**Additional return parameters**

None

**Examples**

```lua
-- This message will be sent once after the network is cut off.
sys.subscribe("IP_LOSE", function(adapter)
    log.info("mobile", "IP_LOSE", (adapter or -1) == socket.LWIP_GP)
end)

```

---

### NTP_UPDATE

Time has been synchronized

**Additional return parameters**

None

**Examples**

```lua
-- For telecom/mobile cards, the base station will issue the time after networking, but the Unicom card will not, so be sure to pay attention to it.
sys.subscribe("NTP_UPDATE", function()
    log.info("mobile", "time", os.date())
end)

```

---

### CC_IND

Call status change

**Additional return parameters**

None

**Examples**

```lua
sys.subscribe("CC_IND", function(status, value)
    log.info("cc status", status, value)
end)

```

---

## softkeyboard



[softkeyboard Interface Documentation Page](https://wiki.luatos.org/api/softkeyboard.html)



### SOFT_KB_INC

Software Keyboard Matrix Messages

**Additional return parameters**

|return parameter type | explanation|
|-|-|
|number|port, keyboard id Currently fixed to 0, can be ignored|
|number|data, keyboard Keys need to be parsed in conjunction with init's map.|
|number|state, Key state 1 is pressed, 0 is released|

**Examples**

```lua
sys.subscribe("SOFT_KB_INC", function(port, data, state)
    -- port Currently fixed to 0, can be ignored
    -- data, Need to cooperate with init map for parsing
    -- state, 1 is pressed, 0 is released
    log.info("keyboard", port, data, state)
end)

```

---

## socket



[socket Interface Documentation Page](https://wiki.luatos.org/api/socket.html)



### NTP_UPDATE

Time has been synchronized

**Additional return parameters**

None

**Examples**

```lua
sys.subscribe("NTP_UPDATE", function()
    log.info("socket", "sntp", os.date())
end)

```

---

### NTP_ERROR

Time synchronization failed

**Additional return parameters**

None

**Examples**

```lua
sys.subscribe("NTP_ERROR", function()
    log.info("socket", "sntp error")
end)

```

---

## lora



[lora Interface Documentation Page](https://wiki.luatos.org/api/lora.html)



### LORA_TX_DONE

LORA Send Complete

**Additional return parameters**

None

**Examples**

```lua
sys.subscribe("LORA_TX_DONE", function()
    lora.recive(1000)
end)

```

---

### LORA_RX_DONE

LORA Receive Completed

**Additional return parameters**

None

**Examples**

```lua
sys.subscribe("LORA_RX_DONE", function(data, size, rssi, snr)
    -- rssi and snr were added on September 06, 2023
    log.info("LORA_RX_DONE: ", data, size, rssi, snr)
    lora.send("PING")
end)

```

---

### LORA_TX_TIMEOUT

LORA Send Timeout

**Additional return parameters**

None

**Examples**

```lua
sys.subscribe("LORA_TX_TIMEOUT", function()
    lora.recive(1000)
end)

```

---

### LORA_RX_TIMEOUT

LORA Receive Timeout

**Additional return parameters**

None

**Examples**

```lua
sys.subscribe("LORA_RX_TIMEOUT", function()
    lora.recive(1000)
end)

```

---

### LORA_RX_ERROR

LORA Receive Error

**Additional return parameters**

None

**Examples**

```lua
sys.subscribe("LORA_RX_ERROR", function()
    lora.recive(1000)
end)

```

---

## sms



[sms Interface Documentation Page](https://wiki.luatos.org/api/sms.html)



### SMS_INC

SMS received

**Additional return parameters**

|return parameter type | explanation|
|-|-|
|string|Mobile phone number|
|string|SMS content, UTF8 encoding|

**Examples**

```lua
--The example of use can be multi-line
-- Receive SMS, support a variety of ways, just choose one
-- 1. Set callback function
--sms.setNewSmsCb( function(phone,sms)
    log.info("sms",phone,sms)
end)
-- 2. Subscribe to system messages
--sys.subscribe("SMS_INC", function(phone,sms)
    log.info("sms",phone,sms)
end)

```

---

