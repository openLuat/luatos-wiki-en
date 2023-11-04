# fdb

This chapter describes how to use the fdb library for LuatOS.

## Introduction

fbd The library is based on FlashDB and provides the ability to save data on flash. The data storage method is similar to redis's k-v

## Hardware preparation

Any LuatOS-SOC development board

## Software part

Interface documentation can be found at:[fdb library](https://wiki.luatos.org/api/fdb.html)

Initialize the fdb database before using the fdb library

```lua
fdb.kvdb_init("env", "onchip_fdb")
```

### Save Data

The data saving format is key-value

+ key is the index of the data
+ value For the actual stored data, the type can be.string/number/bool/table

For example：
| key| value|
|----|---|
| data1| "stringData"|
| data2| 1|
|data3|true|
|data4|{name="Jeremy",age = 18}|

The code is as follows

```lua
PROJECT = "fdb"
VERSION = "1.0.0"
sys = require("sys")
function test()
    if fdb.kvdb_init("env", "onchip_fdb") == false then
        log.error(PROJECT .. ".kvdb_init", "error")
        return
    end
    log.info(PROJECT .. ".kv_set", fdb.kv_set("data1", "stringData"))
    log.info(PROJECT .. ".kv_set", fdb.kv_set("data2", 1))
    log.info(PROJECT .. ".kv_set", fdb.kv_set("data3", true))
    log.info(PROJECT .. ".kv_set", fdb.kv_set("data4", {
        name = "Jeremy",
        age = 18
    }))
end
sys.taskInit(test)
sys.run()
```

The log is as follows

```log
I/fal Flash Abstraction Layer (V0.5.0) initialize success.
D/flashdb KVDB size is 65536 bytes.
D/flashdb FlashDB V1.1.0 is initialize success.
D/flashdb You can get the latest version on https://github.com/armink/FlashDB .
I/user.fdb.kv_set true 0
I/user.fdb.kv_set true 0
I/user.fdb.kv_set true 0
I/user.fdb.kv_set true 0
```

### Query data

Read the corresponding data according to the key set in the previous step.

The code is as follows

```lua
PROJECT = "fdb"
VERSION = "1.0.0"

sys = require("sys")

function test()
    if fdb.kvdb_init("env", "onchip_fdb") == false then
        log.error(PROJECT .. ".kvdb_init", "error")
        return
    end

    log.info(PROJECT .. ".kv_get", fdb.kv_get("data1"))
    log.info(PROJECT .. ".kv_get", fdb.kv_get("data2"))
    log.info(PROJECT .. ".kv_get", fdb.kv_get("data3"))
    -- data4 is stored in a table
    log.info(PROJECT .. ".kv_get", fdb.kv_get("data4"))
    for k, v in pairs(fdb.kv_get("data4")) do
        print(k, v)
    end
end

sys.taskInit(test)

sys.run()
```

The log is as follows

```log
I/fal Flash Abstraction Layer (V0.5.0) initialize success.
D/flashdb KVDB size is 65536 bytes.
D/flashdb FlashDB V1.1.0 is initialize success.
D/flashdb You can get the latest version on https://github.com/armink/FlashDB .
I/user.fdb.kv_get stringData
I/user.fdb.kv_get 1
I/user.fdb.kv_get true
I/user.fdb.kv_get table: 200345A8
name Jeremy
age 18
```

### Delete a key

The code is as follows

```lua
PROJECT = "fdb"
VERSION = "1.0.0"

sys = require("sys")

function test()
    if fdb.kvdb_init("env", "onchip_fdb") == false then
        log.error(PROJECT .. ".kvdb_init", "error")
        return
    end

    -- Get data previously stored in data1.
    log.info(PROJECT .. ".kv_get", fdb.kv_get("data1"))
    -- delete data stored in data1
    log.info(PROJECT .. ".kv_del", fdb.kv_del("data1"))
    -- Query the data stored in data1 again nil
    log.info(PROJECT .. ".kv_get", fdb.kv_get("data1"))
end

sys.taskInit(test)

sys.run()
```

The log is as follows

```log
I/fal Flash Abstraction Layer (V0.5.0) initialize success.
D/flashdb KVDB size is 65536 bytes.
D/flashdb FlashDB V1.1.0 is initialize success.
D/flashdb You can get the latest version on https://github.com/armink/FlashDB .
I/user.fdb.kv_get stringData
I/user.fdb.kv_del true
I/user.fdb.kv_get nil
```

### Empty the entire fdb database

The code is as follows

```lua
PROJECT = "fdb"
VERSION = "1.0.0"

sys = require("sys")

function test()
    if fdb.kvdb_init("env", "onchip_fdb") == false then
        log.error(PROJECT .. ".kvdb_init", "error")
        return
    end

    -- Query data that still exists
    log.info(PROJECT .. ".kv_get", fdb.kv_get("data2"))
    log.info(PROJECT .. ".kv_get", fdb.kv_get("data3"))
    -- data4 is stored in a table
    log.info(PROJECT .. ".kv_get", fdb.kv_get("data4"))
    for k, v in pairs(fdb.kv_get("data4")) do
        print(k, v)
    end

    -- Empty the entire fdb database
    fdb.kv_clr()

    -- Query the data just queried again
    log.info(PROJECT .. ".kv_get", fdb.kv_get("data2"))
    log.info(PROJECT .. ".kv_get", fdb.kv_get("data3"))
    log.info(PROJECT .. ".kv_get", fdb.kv_get("data4"))
end

sys.taskInit(test)

sys.run()
```

The log is as follows

```log
I/fal Flash Abstraction Layer (V0.5.0) initialize success.
D/flashdb KVDB size is 65536 bytes.
D/flashdb FlashDB V1.1.0 is initialize success.
D/flashdb You can get the latest version on https://github.com/armink/FlashDB .
I/user.fdb.kv_get 1
I/user.fdb.kv_get true
I/user.fdb.kv_get table: 20034598
name Jeremy
age 18
I/user.fdb.kv_get nil
I/user.fdb.kv_get nil
I/user.fdb.kv_get nil
```
