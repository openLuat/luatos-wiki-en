# C Interface implements Task waiting function

## Basic information

- Date of drafting: 2022-02-18
- Designer: [chenxuuu](https://github.com/chenxuuu)

## The current problem

Take esphttp as an example：

- ESPHTTP_EVT This topic needs to be written by hand, which is not easy to remember.
- Different request callbacks are ESPHTTP_EVT, which is not reasonable.
- It is very troublesome to start the request and sys.waitUntil by hand.

For example, the following code requires the user to handle it manually.

```lua
local httpc = esphttp.init(esphttp.GET, "http://xxxxxxxxx")
if httpc then
    local ok, err = esphttp.perform(httpc, true)
    if ok then
        while 1 do
            local result, c, ret, data = sys.waitUntil("ESPHTTP_EVT", 20000)
            log.info("httpc", result, c, ret)
            if c == httpc then
                if esphttp.is_done(httpc, ret) then
                    break
                end
                if ret == esphttp.EVENT_ON_DATA and esphttp.status_code(httpc) == 200 then
                    table.insert(rd,data)
                    --log.info("data", "resp", data)
                end
            end
        end
    else
        log.warn("esphttp", "bad perform", err)
    end
    esphttp.cleanup(httpc)
end
```

## Goals to be achieved

- A line of code calls directly
- Built-in sys.waitUntil, to achieve multi-task waiting function
- The user does not need to worry about the process or the internal topic, but can directly call and wait for the result.
- The use of c interface can be easily docked

## Solution

### sys.lua Features to be added in

function to add the following functions (conception）

```lua
sys.cwait_mt = {}
sys.cwait_mt.__index = function(t,i)
    if i == "await" then
        local r = {sys.waitUntil(rawget(t,"w"))}
        table.remove(r,1)
        return table.unpack(r)
    else
        rawget(t,i)
    end
end
function sys.cwaitCreate(w)
    local t = {w=w}
    setmetatable(t,sys.cwait_mt)
    return t
end

--Call Method
sys.taskInit(function()
    local data,result,header = http.asyncGet("http://xxxxxxxxx").await
    log.info("http get",data,result,header)
end)
sys.taskInit(function()
    local data,result,header = http.asyncGet("http://zzzzzzzzz").await
    log.info("http get",data,result,header)
end)
```

### Functions to be implemented by the corresponding c function

An example with no actual function

```c
static int l_xxxx_block(lua_State *L) {
    lua_getglobal(L, "sys");
    lua_pushstring(L,"cwaitCreate");
    lua_gettable(L, -2);
    lua_pushstring(L, "test_123123");--Need a callback for a while topic
    lua_call(L,1,1);

    //What callback function configuration barabala
    //.....
    //.....

    return 1;--Return the generated meta table for lua to call
}

void cb(char* topic,int data) {
    lua_getglobal(L, "sys_pub");
    lua_pushstring(L, topic);
    lua_pushinteger(L,data);
    lua_call(L, 2, 0);
}
```

- The result should be returned as soon as possible after the call and cannot be blocked.
- topic Cannot be repeated, each call must generate a new topic
- the callback needs to publish the corresponding topic and attach the result.
- publish the topic prefixes of
- The naming of such interfaces needs to be standardized, such as starting with `async`

## Relevant knowledge points

- Message Bus
