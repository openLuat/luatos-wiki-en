# sqlite3 - sqlite3 Database Operations

{bdg-success}`Adapted` {bdg-primary}`Air101/Air103` {bdg-primary}`Air601` {bdg-primary}`Air780E/Air700E` {bdg-primary}`Air780EP`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/sqlite3/binding/luat_lib_sqlite.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view sqlite3 demo examples](https://gitee.com/openLuat/LuatOS/tree/master/demo/sqlite3)
```

**Example**

```lua
-- Note that this library is still under development and is not yet supported by most BSPs
-- This transplant is based on sqlite3 3.44.0
sys.taskInit(function()
    sys.wait(1000)
    local db = sqlite3.open("/ram/test.db")
    log.info("sqlite3", db)
    if db then
        sqlite3.exec(db, "CREATE TABLE devs(ID INT PRIMARY KEY NOT NULL, name CHAR(50));")
        sqlite3.exec(db, "insert into devs values(1, \"ABC\");")
        sqlite3.exec(db, "insert into devs values(2, \"DEF\");")
        sqlite3.exec(db, "insert into devs values(3, \"HIJ\");")
        local ret, data = sqlite3.exec(db, "select * from devs;")
        log.info("Query Results", ret, data)
        if ret then
            for k, v in pairs(data) do
                log.info("Data", json.encode(v))
            end
        end
        sqlite3.close(db)
    end
end)

```

## sqlite3.open(path)



Open Database

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|The path of the database file, which must be filled in, is automatically created if it does not exist.|

**Return Value**

|return value type | explanation|
|-|-|
|userdata|Database pointer, whether to return nil|

**Examples**

```lua
local db = sqlite3.open("/test.db")
if db then
   -- Database Operations xxxx

    -- Must be turned off after use
    sqlite3.close(db)
end

```

---

## sqlite3.exec(db, sql)



Execute SQL statements

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|Database pointer obtained through sqlite3.open|
|string|SQL String, must be filled in|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success, otherwise nil|
|table|The query result (if any) is returned successfully, otherwise the string with an error is returned.|

**Examples**

None

---

## sqlite3.close(db)



Close the database

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|userdata|Database pointer obtained through sqlite3.open|

**Return Value**

|return value type | explanation|
|-|-|
|boolean|Returns true on success, otherwise nil|

**Examples**

None

---

