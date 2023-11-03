# Luat Framework

-------

## LuaTask

### Function Run

Since there is no main function, some developers who are accustomed to regular single-chip microcomputer do not know how to run the program for a while. There are many ways to run a program

#### Direct call

The lib folder is handled in the project and.main.lua，test.lua。

`main.lua`

```lua
--Important: PROJECT and VERSION variables must be defined at this location
--PROJECT：string Type, can be defined casually, as long as not used, on the line
--VERSION：string Type, if you use the firmware upgrade function of Luat IoT platform, you must define it according to "X.X.X", where X represents 1 digit; Otherwise, you can define it casually.
PROJECT = "testdemo"
VERSION = "1.0.0"
-- sys The library is Luat's core support library and is basically a must-have.
local sys = require"sys"

-- ----------------------------
-- Introduction test.lua
local test = require "test"
test.hi()
---------------------------

sys.run() -- Start the internal event loop, only allowed at the end of main.lua
```

main.lua A script that needs to be written using the `require` reference, here is test.lua。

`test.lua`

```lua
local test = {}

local function ss() -- Internal method, external cannot be called
	print("ss function test")
end
ss()		--Directly called, this function will be called when "test" is require in the main.lua file.

function test.hi() -- Exposure to external methods
    log.info("test", "say hi from test")
end

return test
```

#### Synergy

main.lua Ibid.

`test.lua`

```lua
local test = {}

sys.taskInit(function()
    cnt = 0
    while true do
	 	print("ss function test")
        sys.wait(1000)			-- Suspend 1000ms, similarly, run every 1000ms
    end
end)

return test
```

#### Timer

main.lua Ibid.

`test.lua`

```lua
local test = {}

local function ss(  )
	print("ss function test")
end
--sys.timerStart(ss,3000)	--3 Run once per second
sys.timerLoopStart (ss,5000)  --Cycle timer, running every 5 seconds

return test
```


#### Program Registration

LuaTask The message mechanism is implemented by subscribing to publications.

When a function completes an operation, it can publish a message, and other functions can subscribe to the message and perform the corresponding operation. For example, when the socket sends data after the release of "SEND_FINISH". At this time, the developer wants to send data through the serial port or change the state of an IO port after the socket release is completed. You can subscribe to the message subscribe("SEND_FINISH",callback). callback what needs to be done after receiving the SEND_FINISH message.

Let's look at a program first.

`testMsgPub.lua`

```lua
--testMsgPub.lua
local testMsgPub = {}

local sys = require"sys"
local  a = 2
local function pub()
	print("pub")
	sys.publish("TEST",a)		--Multiple variables can be published sys.publish("TEST",1,2,3)
end
pub()

return testMsgPub
```

`testMsgSub.lua`

```lua
--testMsgSub.lua
local testMsgSub = {}

local sys = require"sys"

local function subCallBack(...)
	print("rev",arg[1])
end

sys.subscribe("TEST",subCallBack)
return testMsgSub
```

What if you want to subscribe to the message in the task function and do the corresponding processing?

```lua
local testMsgSub = {}

require"sys"
local  a = 2
local function pub()
	print("pub")
	sys.publish("TEST",a)
end
pub()
sys.taskInit(function()
	while true do
		result, data = sys.waitUntil("TEST", 10000)
		if result == true then
			print("rev")
			print(data)
		end
		sys.wait(2000)
	end
end)

return testMsgSub
```

Call the sys.waitUntil() function.

Next analysis of the implementation of the source code

In order to better understand the source code, the following preliminary knowledge is required：

1、implementation of callback function

```lua
local function callBackTest(...)
	print("callBack",arg[1])
end

local function test( a,callBackTest )
	if a > 1 then
		callBackTest(1)
	end
end
test(2,callBackTest)
--Output
--callBack	1
```

2、indeterminate parameter

```lua
function g (a, b, ...) end
g(3)              -- a=3, b=nil, arg={n=0}   -- n is the number of indefinite arguments
g(3, 4)           -- a=3, b=4, arg={n=0}
g(3, 4, 5, 8)     -- a=3, b=4, arg={5, 8; n=2}
```

