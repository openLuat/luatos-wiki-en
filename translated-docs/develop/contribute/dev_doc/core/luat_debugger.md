# Luat Debugger

## Basic information

* Date of drafting: 2021-01-02
* Designer: [wendal](https://github.com/wendal)

This document describes the LuatOS debugger-related information, mainly used for single-step debugging of lua scripts.

## Glossary of Terms

* DAP - debug-adapter-protocol A debug adaptation protocol led by Microsoft, mainly used vscode
* Debugger - Debugger, this article is basically equivalent to Lua script debugger, single-step debugging, up and down variable viewing and setting, stack query, etc.

## Communication process

### Designed hardware and software entities

* module, which specifically executes LuatOS and scripts
* Serial port/USB/UART, communication mode between module and debugger
* LuaTools, Form of communication between the proxy debugger and the module
* Debugger, run standalone program inside vscode
* VSCODE, Execute the debugger, communicate with it, show the debug interface

### Mutual communication links

1. Module <-> serial port/USB. The external communication channel of the module is different from the process debugging under windows/linux/macos, but it can still be abstracted Input/Output
2. Serial/USB <-> LuaTools. LuaTools is responsible for parsing the output data of the module, separating the debugging data inside, sending it to the debugger, receiving the debugger's commands, converting it to a format recognized by the module, and writing it to the serial port./USB.
3. LuaTools <--> Debugger. The communication channel between the two is Socket or IPC, usually socket, and the protocol used is LuatOS debugger protocol.
4. Debugger <--> vscode. The two communication channels to the standard input and output, the format is DAP.

Overall display

```
Module <-- USB --> LuaTools <-- Socket --> Debugger <-- Standard Input Output--> vscode
```

Supplementary Notes

* In terms of simplified communication, the debugger can also communicate with the module via serial/USB without the need LuaTools.
* In view of the difficulty of the adapter to read the serial port, and the AP log channel that the rda8910 will go through, the choice of LuaTools is a more reasonable way.
* In the long run, direct communication between the debugger and the device is the best experience..
* LuaTools The specific behavior is equivalent to the protocol agent, masking the differences in the way different hardware device interfaces communicate, between the module and the debugger, walking the LuatOS debugging protocol..

## LuatOS Commissioning Protocol

Current Protocol Version v1

### Basic communication method (output)

The output of the protocol, in the form of `[head] exts`, is output by row

Among them:
* `[head]` is a comma-separated string of only English letters or numbers.
*  `exts` For a string, the meaning will be different according to the `head`

### Basic communication method (input)

The input of the protocol, in the form of `cmd arg1 arg2 ...`, is entered by line, generally as a command.

Among them:
* `cmd` It's an order
*  `arg1 arg2` for different string parameters

#### Status Change Output(state,changed)

`[state,changed,1,2]`

Debugging state changes, the former is the original state, the latter is the updated state

* 0  Debug mode off, program running
* 1  Waiting for debugger connection, program waiting
* 2  The program is running until it encounters a breakpoint.
* 3  The program is paused, usually with an event, such`[event,stopped,break]`
* 4  The program is running and is executing the `next` or `step` operation. The next state is usually 3
* 5  The program is running and is performing the `stepIn` operation. The next state is usually 3
* 6  The program is running and is performing the `stepOut` operation. The next state is usually 3

#### Wait for Debugger(event,waitc)

`[event,waitc]`

Usually, after adding the `dbg.wait(300)`statement to the lua script, luatos will stop at the statement and wait for the debugger`s command.

#### Wait for Debugger(event,waitt)

`[event,waitt]`

After waiting for the specified number of seconds, the` start` command is still not received, the waiting is automatically exited, the status is restored to 0, and the debugging mode is closed.

#### Program suspended(event,stopped)

`[event,stopped,break]`
`[event,stopped,step]`
`[event,stopped,stepIn]`
`[event,stopped,stepOut]`

When a breakpoint is encountered or the debugging instruction (`step/stepIn/stepOut`) is executed, the pause is triggered when the condition is met.

Meanwhile, `[state,changed,X,3]` should come together

#### Response Class

TODO resp Class and executable command parameters


