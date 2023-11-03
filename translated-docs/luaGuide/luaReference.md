# Lua 5.3 Reference Manual

Author Roberto Ierusalimschy, Luiz Henrique de Figueiredo, Waldemar Celes

Translator [Yun Feng](http://blog.codingnow.com)

Lua.org, PUC-Rio 版权所有 © 2015 ， It can be used freely under the terms of [Lua license](http://www.lua.org/license.html).

If you are looking for the original manual page, please [click here to view](https://openluat.github.io/luatos-wiki-en/_static/lua53doc/index.html)

* * *

## 1 – Introduction

Lua It is an extended programming language designed to support general procedural programming and has related data description facilities. It also provides good support for object-oriented programming, functional programming and data-driven programming. As a powerful, lightweight embedded scripting language, it can be used by any program that needs it. Lua is implemented as a library by_clean C (a subset common between standard C and C).

As an extended language, Lua does not have the concept of a "main" program: it can only work in a host program, which is called an embedded program or, for short, a host program_. The host program can call functions to execute a small piece of Lua code, can read and write Lua variables, and can register C functions for Lua code to call. Relying on C functions, Lua can share the same syntax framework to customize the programming language and thus apply to different domains. The official release of Lua includes an example of a host program called `lua`, which is a complete stand-alone Lua interpreter implemented using the Lua library, which can be used for interactive applications or batch processing.

Lua It is a free software, and its use license determines its use without any guarantee. The implementation described in this manual can be found on Lua`s official website at `www.lua.org.

Like many other reference manuals, this document is somewhat boring. For the design ideas behind Lua, take a look at the technical papers available on the Lua website. As for the details of programming with Lua, please refer to Roberto book，_Programming in Lua_。

## 2 – Basic concepts

This chapter describes the basic concepts of language.

### 2.1 – Value and Type

Lua is a_dynamically typed language_. This means that variables have no type; only values have a type. There is no type definition in the language. All values carry their own type.

Lua All the values in are_first-class citizens_. This means that all values can be stored in variables, passed as arguments to other functions, and returned as values.

Lua There are eight basic types in: _nil_,_boolean_,_number_,_string_,_function_,_userdata_,_thread_, and_table_. _Nil_is the type of value **nil** whose main characteristic is to distinguish it from other values; it is usually used to represent the state when a meaningful value does not exist. _Boolean_is a type of both **false** and **true** values. Both **nil** and **false** cause the condition to be false; any other value is true. _Number_represents integers and real numbers (floating point numbers). _String_represents an immutable sequence of bytes. Lua is 8-bit friendly: a string can hold any 8-bit value, including zeros (`\0`). Lua`s string has nothing to do with encoding; it doesn`t care about the exact contents of the string.

_number_ There are two internal representations of types,_integer_and_floating point_. Lua has clear rules for when to use which internal form, but it also makes automatic conversions as needed (see [§ 3.4.3](#3.4.3)). Therefore, in most cases, the programmer can choose to ignore the difference between integers and floating-point numbers or assume complete control over the internal representation of each number. Standard Lua uses 64-bit integers and double-precision (64-bit) floats, but you can also compile Lua to use 32-bit integers and single-precision (32-bit) floats. Representation of numbers in 32 bits is particularly suitable for small machines and embedded systems. (See macros in the `luaconf.h` file `LUA_32BITS` 。）

Lua Functions written in Lua or C (see [§ 3.4.10](#3.4.10)) can be called (and operated). These two functions have a uniform type _function_。

_userdata_ type allows data in C to be stored in Lua variables. The value of the user data type is a memory block, and there are two types of user data:_complete user data_, which refers to an object corresponding to a memory managed by Lua;_lightweight user data_, which refers to a simple C pointer. User data has no predefined operations in Lua other than assignment and equality determination. By using the_metatable_, the programmer can define a series of operations on full user data (see [§ 2.4](#2.4)). You can only create or modify the value of user data in Lua code through the C API, which ensures that the data is only controlled by the host program.

_thread_ A type represents an independent execution sequence that is used to implement a coroutine (see [§ 2.6](#2.6)). The threads of Lua have nothing to do with the threads of the operating system. Lua provides coroutet support for all systems, including those that do not support native threads.

_table_ is an associative array, that is, this array is not only indexed by numbers, but all Lua values except **nil** and NaN can be indexed. (The_Not a Number_ is a special number that is used to represent the result of an undefined or unrepresentative operation, such as` 0/0 `.) Tables can be heterogeneous; that is, they can contain values of any type (except **nil**). Any key with a value of **nil** will not be recorded inside the table structure. In other words, for keys that do not exist in the table, they all correspond to values. **nil** 。

Tables are the only data structures in Lua that can be used to represent ordinary arrays, sequences, symbolic tables, collections, records, graphs, trees, and so on. For records, Lua uses the domain name as an index. The language provides syntactic sugar such as `a.name` instead of `a["name"]`to facilitate the use of this structure. There are several convenient ways to create tables in Lua (cf. [§3.4.9](#3.4.9)）。

We use the term_sequence_to denote a table indexed by the set of positive integers {1 .._n_}. Here the non-negative integer_n_is called the length of the sequence (cf. [§3.4.7](#3.4.7)）。

Like an index, the value of each field in a table can be of any type. It is important to note that since functions are first-class citizens, the fields of the table can also be functions. In this way, the table can carry_method. (See [§3.4.11](#3.4.11)）。

The principle of indexing a table follows the rules of direct comparison in the language. The expressions `a [I] `and `a[j]` represent the same elements in the table if and only if `I` and `j` are directly compared equal (I. e., not by meta-method comparison). In particular, a floating-point number that can be fully represented as an integer is equal to the corresponding integer (for example: `1.0 = = 1 `). In order to eliminate ambiguity, when a floating-point number that can be fully represented as an integer is used as a key value, it will be converted to the corresponding integer storage. For example, when you write `a[2.0] = true`, the key that is actually inserted into the table is the integer `2 `. (On the other hand, 2 and "`2`" are two different Lua values, so they can be different items in the same table.。）

Tables, functions, threads, and full user data are called_objects_in Lua: variables do not really_hold_their values, but only_references_to these objects_. Assignments, parameter passing, and function returns are all operations on references rather than values, and none of these operations do any form of implicit copy.

The library function [`type`](#pdf-type) is used to return the type of a given value as a string. (See [§6.1](#6.1)）。

### 2.2 – Environment and Global Environment

As discussed later in [§ 3.2](#3.2) and [§ 3.3.3](#3.3.3), reference to a free name called `var` (referring to a name that is not declared at any level) is syntactically translated as `_ENV.var`. In addition, every compiled Lua code block will have an external local variable called `_ENV` (see [§ 3.3.2](#3.3.2)), so the name`_ENV` will never be a free name in a code block.

When translating those free names, it doesn`t matter whether `_ENV` is the external local variable. There is no difference between `_ENV` and other variable names you can use. In particular, you can define a new variable or specify a parameter by this name. The `_ENV` used by the compiler when translating free names refers to the variable named \_ENV that is visible to your program at that point. (Lua`s visibility rules see [§3.5](#3.5)）

The table used by `_ENV` for values is called_environment _。

Lua There is a special environment called_global environment. It is stored under a special index in the C registry (see [§ 4.5](#4.5)). In Lua, the global variable [`_G`](#pdf-_G) is initialized to this value. ([`_G`](#pdf-_G) is not used anywhere internally。）

When Lua loads a block of code, the default value of `_ENV` is the global environment (see [`load`](#pdf-load)). Therefore, by default, the free names mentioned in Lua code refer to the relevant items in the global environment (therefore, they are also called_global variables_). In addition, all standard libraries are loaded into the global environment, and some functions also operate on this environment. You can load blocks of code with [`load`](#pdf-load) (or [`loadfile`](#pdf-loadfile)) and give them different environments. (In C, when you load a block of code, you can change its environment by changing its first upper value.。）

### 2.3 – Error handling

Since Lua is an embedded extension language, all its behaviors are derived from the call of a Lua library function by the C code in the host program. (When Lua is used alone, the `lua` program is the host program.) Therefore, in the process of compiling or running Lua code block, whenever an error occurs, control is returned to the host, which is responsible for taking appropriate measures (such as printing error messages）。

You can call the [`error`](#pdf-error) function in Lua code to explicitly throw an error. If you need to catch these errors in Lua, you can use [`pcall`](#pdf-pcall) or [`xpcall`](#pdf-xpcall) to call a function in_protected mode.

Whenever an error occurs, an_error object_ (_error message_) carrying the error message is thrown. Lua itself will only generate string type error objects for errors, but your program can generate any type of error object for errors, depending on how your Lua program or host program handles these error objects.

When using [`xpcall`](#pdf-xpcall) or [`lua_pcall](#lua_pcall), you should provide a_message handler_to be called when the error is thrown. This function receives the original error message and returns a new error message. It is called when the stack has not been expanded since the error occurred, so you can use the stack to collect more information, such as by exploring the stack to create a set of stack backtracking information. At the same time, the handler is also in protected mode, so an error within the function triggers it again (recursively). If the recursion is too deep, Lua terminates the call and returns an appropriate message.

### 2.4 – Meta Table and Meta Method

Lua Each value in can have a_meta table_. This_meta table_is a normal Lua table that defines the behavior of the original value under a specific operation. If you want to change the behavior of a value under a specific operation, you can set the corresponding field in its meta table. For example, when you add a non-numeric value, Lua checks the function under the `__add` field in the meta table for that value. If it can be found, Lua calls this function to complete the add operation.

The key value of an event in the meta table is a string with a double underscore (`__`) plus the event name; those values associated with the key are called the_meta method_. In the previous example, `__add` is the key value, and the corresponding meta method is the function that performs the add operation.

You can use the [`getmetatable`](#pdf-getmetatable) function to get the metatable of any value. Lua uses direct access to query meta methods from the meta table (see [`rawget`](#pdf-rawget)). So, the meta-method for getting event `ev` from object `o` is equivalent to the following code：

     rawget(getmetatable(_o_) or {}, "\_\__ev_")

You can use [`setmetatable`](#pdf-setmetatable) to replace the meta table of a table. In Lua, you cannot change the meta table for values of types other than table (unless you use a debugging library (see [§ 6.10](#6.10))); to change the meta table for values of these non-table types, use C API。

Tables and full user data have separate meta tables (of course, multiple tables and user data can share the same meta table). Other types of values share a meta table by type; that is, all numbers share the same meta table, all strings share another meta table, and so on. By default, the value has no meta table, but the string library sets the meta table for the string type at initialization (see. [§6.4](#6.4)）。

Metatables determine the behavior of an object during mathematical operations, bit operations, comparisons, joins, lengths, calls, and indexes. A metatable can also define a function that is called when a table object or user data object is garbage collected (see [§ 2.5](#2.5)).

For unary operators (negative, length, bit inversion), when the meta method is called, the second parameter is a dummy with a value equal to the first parameter. This treatment is only to simplify the internal implementation of Lua (this treatment allows all operations to be consistent with binary operations), and this behavior may be removed in future versions. (The behavior of using this additional parameter is uncertain.。）

Next is a detailed list of the events that the meta table can control. Each operation is distinguished by the corresponding event name. The key name of each event is represented by a string prefixed with ``__``; for example, the key name of the "add" operation is a string "`__add`"。

*   **`__add`:** `+` Operation. If any value that is not a number (including a string that cannot be converted to a number) is added, Lua will try to call the meta method. First, Lua checks the first operand (even if it is legal). If this operand does not have a meta method defined for the "`__add`" event, Lua checks the second operand. Once Lua finds the meta-method, it will pass the two operands as parameters to the meta-method, and the result of the meta-method (adjusted to a single value) as the result of the operation. If the meta method cannot be found, an error will be thrown.
*   **`__sub`:** `-` Operation. The behavior is similar to the "add" operation.
*   **`__mul`:** `*` Operation. The behavior is similar to the "add" operation.
*   **`__div`:** `/` Operation. The behavior is similar to the "add" operation.
*   **`__mod`:** `%` Operation. The behavior is similar to the "add" operation.
*   **`__pow`:** `^` （power) operation. The behavior is similar to the "add" operation.
*   **`__unm`:** `-` （negative) operation. The behavior is similar to the "add" operation.
*   **`__idiv`:** `//` （down to take the whole division) operation. The behavior is similar to the "add" operation.
*   **`__band`:** `&` （bitwise and) operation. The behavior is similar to the "add" operation, except that Lua will try the element method when any of the operands cannot be converted to an integer (see [§ 3.4.3](#3.4.3)).
*   **`__bor`:** `|` （bitwise or) operation. The behavior is similar to the "band" operation.
*   **`__bxor`:** `~` （bitwise XOR) operation. The behavior is similar to the "band" operation.
*   **`__bnot`:** `~` （bitwise not) operation. The behavior is similar to the "band" operation.
*   **`__shl`:** `<<` （left shift) operation. The behavior is similar to the "band" operation.
*   **`__shr`:** `>>` （right) operation. The behavior is similar to the "band" operation.
*   **`__concat`:** `..` （connection) operation. The behavior is similar to the "add" operation, except that Lua tries the meta method if any operand is neither a string nor a number (a number can always be converted to the corresponding string).
*   **`__len`:** `#` （take the length) operation. If the object is not a string, Lua tries its meta methods. If there is a meta method, it is called and the object is passed in as a parameter, and the return value (adjusted to a single) is the result. If the object is a table and there is no meta-method, Lua uses the table length operation (see [§ 3.4.7](#3.4.7)). In other cases, errors are thrown.
*   **`__eq`:** `==` （equal to) operation. The behavior is similar to the "add" operation, except that Lua only tries meta methods when both values are tables or full user data and they are not the same object. The result of the call is always converted to a boolean.
*   **`__lt`:** `<` （less than) operation. The behavior is similar to the "add" operation, except that Lua only tries meta methods when the two values are not all integers or all strings. The result of the call is always converted to a boolean.
*   **`__le`:** `<=` （Less than or equal to) operation. Unlike other operations, the less than or equal to operation may use two different events. First, like the behavior of the "lt" operation, Lua looks for the "`__le`" meta-method in both operands. If a meta-method cannot find it, it will look for the "`__lt`" event again, assuming that `a <= B` is equivalent to `not (B <a)`. Similar to other comparison operators, the results are converted to Boolean quantities.
*   **`__index`:** Index `table[key]`. This event is triggered when `table` is not a table or the key `key` does not exist in the table `table. At this point, the corresponding meta-method of `table` is read out.

    Despite the name, the meta-method for this event can actually be a function or a table. If it is a function, it is called with `table` and `key` as arguments. If it is a table, the final result is the result of indexing the table with `key. (This indexing process is a regular process, not a direct index, so this index may trigger another meta-method.。）

*   **`__newindex`:** Index assignment `table[key] = value `. Similar to the index event, it occurs when `table` is not a table or the key `key` does not exist in the table `table. At this point, the corresponding meta-method of `table` is read out.

    As with the indexing process, the meta-method for this event can be either a function or a table. If it is a function, the parameters `table`, `key`, and `value` are passed in. If it is a table, Lua assigns an index to the table. (This indexing process is a regular process, not a direct index assignment, so this index assignment may trigger another meta-method。）

    Once you have the "newindex" meta-method, Lua no longer does the initial assignment. (If necessary, [`rawset`](#pdf-rawset) can be called inside the meta method to do the assignment。）

*   **`__call`:** The function calls the operation `func(args)`. This event is triggered when Lua tries to call a value that is not a function (I. e. `func` is not a function). Find the meta-method of` func`, and if you can find it, call this meta-method, `func` is passed in as the first parameter, and the originally called parameter (` args`) is followed by the order.

### 2.5 – Garbage collection

Lua Automatic memory management. This means that you don't have to worry about how the memory needed by newly created objects is allocated, or how to release the memory occupied by objects after they are no longer in use. Lua runs a_garbage collector_to collect all_dead objects_(that is, objects that are no longer accessible in Lua) to complete the work of automatic memory management. All memory used in Lua, such as strings, tables, user data, functions, threads, internal structures, etc., are subject to automatic management.

Lua An incremental marker-scan collector is implemented. It uses these two numbers to control the garbage collection cycle:_garbage collector intermittent rate_and_garbage collector step rate_. Both of these numbers use percentages as units (e. g. the value 100 is represented internally 1 ）。

The garbage collector intermittency rate controls how long the collector has to wait before starting a new cycle. Increasing this value will reduce the enthusiasm of the collector. When this value is less than 100, the collector will not wait before starting a new loop. Setting this value to 200 will cause the collector to wait until the total memory usage has doubled before starting a new cycle.

The garbage collector step rate controls the rate at which the collector operates relative to the memory allocation rate. Increasing this value not only makes the collector more aggressive, but also increases the length of each incremental step. Don't set this value less than 100, then the collector will work too slowly to finish a cycle forever. The default value is 200, which means that the collector operates at twice the speed of memory allocation.

If you set the step factor to a very large number (10% larger than the number of bytes your program may use), the collector behaves like a stop-the-world collector. Then if you set the intermittent rate to 200, the collector behaves like the previous version of Lua: every time Lua doubles the memory used, it does a complete collection.

You can change these numbers by calling [`lua_gc`](#lua_gc) in C or [`collectgarbage`](#pdf-collectgarbage) in Lua. These two functions can also be used to directly control the collector (e. g. stop it or restart it）。

#### 2.5.1 – garbage collection meta-method

You can set the meta method for garbage collection for tables, and for full user data (see [§ 2.4](#2.4)), you need to use the C API. The meta-method is called_terminator_. Finalizers allow you to work with Lua's garbage collector to do some additional resource management tasks (such as closing files, network or database connections, or freeing up some of your own memory.）。

If you want an object (table or user data) to enter the finalization process during collection, you must_mark_it needs to trigger the finalizer. When you set up a meta table for an object, if at this moment the meta table uses a field indexed by the string "`__gc`", then it is marked that the object needs to trigger the finalizer. Note: If you set up a meta table without `__gc` field for an object and then add this field to the meta table, then the object is not marked as needing to trigger the finalizer. However, once the object is marked, you are still free to change the `__gc` field in its meta table.

When a marked object becomes garbage, the garbage collector does not immediately recycle it. Instead, Lua puts it into a linked list. After the collection is complete, Lua will traverse the linked list. Lua will check the `__gc` meta-method of each object in the linked list: if it is a function, it will be called with the object as the only parameter; Otherwise, it will be ignored directly.

In the final stage of each garbage collection cycle, the triggering order of the finalizers of the objects detected in this cycle that need to be recycled is carried out in the reverse order of the order in which the objects were marked as needing to trigger the finalizers. That is to say, the first called finalizer is the one carried by the last marked object in the program. The operation of each finalizer may occur at any point during the execution of the regular code.

Since the recycled object still needs to be used by the finalizer, the object (and other objects that can only be accessed through it) must be revived by Lua_. Usually, the resurrection is short-lived and the memory to which the object belongs is freed in the next garbage collection cycle. Then, if the finalizer saves the object in some global place (for example, in a global variable), the resurrection will continue to take effect. In addition, if an object that is entering the finalizer process is marked again in the finalizer to trigger the finalizer, its finalizer function will be called again as long as the object is still unreachable in the next loop. In either case, the memory to which the object belongs is released only if the object is unreachable during the garbage collection cycle and is not marked as needing to trigger the finalizer.

When you close a state machine (see [`lua_close`](#lua_close)), Lua will call all the finalization procedures marked as needing to trigger the finalizer object, in the reverse order of the marked order. Any act of the finalizer marking the object again does not take effect during this process.

#### 2.5.2 – Weak table

_ Weak table_refers to a table whose internal element is_weak reference. The garbage collector ignores weak references. In other words, if an object is referenced only by weak references, the garbage collector reclaims the object.

A weak table can have weak keys or weak values, or both keys and values can be weak references. A table with a weak value allows the collector to reclaim its value, but prevents the collector from reclaiming its key. If the key values of a table are weak references, the collector can recycle any key and value. In any case, whenever either the key or the value is recycled, the associated key-value pair is removed from the table. The '__mode' field in the meta table of a table controls the weak properties of the table. When the '__mode' field is a string containing the character ''k'', all keys in this table are weak references. When the '__mode' field is a string containing the character' v', all values in this table are weak references.

A table whose attribute is a weak key and strong value is also called a_temporary table_. For a temporary table, whether its value is reachable depends only on whether its corresponding key is reachable. Note that if a key in a table is referenced only by its value, the key-value pair will be removed from the table.

Modifications to a table's weak attributes take effect only on the next collection cycle. Especially when you change the table from weak to strong, Lua may still recycle some items in the table before the modification takes effect.

Only those objects that have an explicit construction process are removed from the weak table. Values, such as numbers and lightweight C functions, are not governed by the garbage collector and therefore are not removed from the weak table (unless their associated items are recycled). Although strings are governed by the garbage collector, they do not have an explicit construction process, so they are not removed from the weak table.

Weak tables have special behavior for resurrected objects (those that are going through the finalizer process and can only be accessed by the finalizer). Objects referenced by weak values are removed before their finalizers are run, while objects referenced by weak keys are not removed until the finalizers are run and the next collection when the objects are actually released. This behavior allows the finalizer runtime to access the properties associated by the object in the weak table.

If a weak table is in the resurrection object in the current collection cycle, then the table may not be properly cleaned up before the next cycle.

### 2.6 – Synergy

Lua Support coroutation, also called_collaborative multithreading_. A coroutine in Lua represents a separate thread of execution. However, unlike threads in multi-threaded systems, a coroutine suspends current execution only when a yield function is explicitly called.

Call the function [`coroutine.create`](#pdf-coroutine.create) to create a coprocess. Its only argument is the main function of the coroutine. The `create` function is only responsible for creating a coroutine and returning its handle (an object of type_thread_).

Call the [`coroutine.resume`](#pdf-coroutine.resume) function to execute a coroutine. When calling [`coroutine.resume`](#pdf-coroutine.resume) for the first time, the first parameter should pass in the thread object returned by [`coroutine.create`](#pdf-coroutine.create), and then the coroutine starts execution from the first line of its main function. Additional parameters passed to [`coroutine.resume`](#pdf-coroutine.resume) are passed in as arguments to the main coroutine function. After the coroute is started, it will run until it terminates or gives up._。

The running of the coroutine may be terminated in two ways: the normal way is that the main function returns (explicitly returns or runs the last instruction); The abnormal way is that an error has not been caught. For normal termination, [`coroutine.resume`](#pdf-coroutine.resume) will return **true**, followed by the return value of the coroutine main function. When an error occurs, [`coroutine.resume`](#pdf-coroutine.resume) will return **false** with the error message.

By calling [`coroutine.yield`](#pdf-coroutine.yield), the execution of the coroutine is suspended to give up the execution right. When a coroutine cedes, the corresponding nearest [`coroutine.resume`](#pdf-coroutine.resume) function returns immediately, even if the cede occurs in an inline function call (I. e., not in the main function, but inside a function called directly or indirectly by the main function). [`coroutine.resume`](#pdf-coroutine.resume) also returns **true** with the parameters passed to [`coroutine.yield`](#pdf-coroutine.yield). The next time the same co-process is restarted, the co-process will then continue to execute from the point of yield. At this point, the previous call to [`coroutine.yield`](#pdf-coroutine.yield) at the exit point will be returned, and the return value is other than the first parameter passed to [`coroutine.resume`](#pdf-coroutine.resume).

Similar to [`coroutine.create`](#pdf-coroutine.create), the [`coroutine.wrap`](#pdf-coroutine.wrap) function also creates a coroutine. The difference is that instead of returning the coroutine itself, it returns a function. Calling this function will start the coroutage. Any arguments passed to this function are treated as extra arguments to [`coroutine.resume`](#pdf-coroutine.resume). [`coroutine.wrap`](#pdf-coroutine.wrap) returns all the return values of [`coroutine.resume`](#pdf-coroutine.resume) except the first return value (boolean error code). Unlike [`coroutine.resume`](#pdf-coroutine.resume), [`coroutine. pdf-coroutine.wrap](# resume`) does not catch errors; instead, any errors are propagated to the caller.

The following code shows an example of a coroutet working：

     function foo (a)
       print("foo", a)
       return coroutine.yield(2\*a)
     end

     co = coroutine.create(function (a,b)
           print("co-body", a, b)
           local r = foo(a+1)
           print("co-body", r)
           local r, s = coroutine.yield(a+b, a-b)
           print("co-body", r, s)
           return b, "end"
     end)

     print("main", coroutine.resume(co, 1, 10))
     print("main", coroutine.resume(co, "r"))
     print("main", coroutine.resume(co, "x", "y"))
     print("main", coroutine.resume(co, "x", "y"))

When you run it, it produces the following output：

     co-body 1       10
     foo     2
     main    true    4
     co-body r
     main    true    11      -9
     co-body x       y
     main    true    10      end
     main    false   cannot resume dead coroutine

You can also use the C API to create and manipulate coroutine: see functions [lua_newthread](#lua_newthread), [lua_resume](#lua_resume), and [`lua_yield`](#lua_yield)。

## 3 – Language Definition

This chapter describes the lexical, grammatical and syntactic aspects of Lua. In other words, this chapter describes which symbols are valid, how they are combined, and what these combinations mean.

Concepts about the composition of the language will be written in common extended BNF expressions. It looks like this: {_a_} means 0 or more_a_, and \[_a_\] means an optional_a_. Non-final symbols that can be decomposed are written as non-terminal, keywords are written as **kword**, and other final symbols that cannot be decomposed are written as' **\= * * '. The complete Lua syntax can be found in the last chapter of this manual [§ 9](#9).

### 3.1 – lexical convention

Lua The language format is free. It ignores spaces (including line breaks) and comments between syntax elements (symbols) and only sees them as a separator between names and keywords.

Lua The_name_(also known as_identifier_) in can be a string consisting of any letter underscore and number that is not a number. Identifiers can be used to name variables, fields of tables, and labels.

The following_keywords_are reserved and cannot be used for names：

     and       break     do        else      elseif    end
     false     for       function  goto      if        in
     local     nil       not       or        repeat    return
     then      true      until     while

Lua The language is case sensitive: `and` is a reserved word, but `And` and `AND` are two different valid names. As a convention, programs should avoid creating names that are underlined with one or more uppercase letters (e. g. [`_VERSION`](#pdf-_VERSION)）。

The following strings are some other symbols：

     +     -     \*     /     %     ^     #
     &     ~     |     <<    >>    //
     ==    ~=    <=    >=    <     >     =
     (     )     {     }     \[     \]     ::
     ;     :     ,     .     ..    ...

_ The literal string_can be enclosed in single or double quotes. The literal string can contain the following C- style escape strings: ``\a`` (bell), ``\B ``(backspace), ``\f`` (page change), ``\n`` (line change), ``\r`` (carriage return), ``\t`` (horizontal item tabulation), ``\v`` (vertical tabulation), ``\ ``(backslash), ``\``(double quotation marks), and ```` (single quotation mark). A true line break followed by a backslash is equivalent to writing a line break in the string. The escape string ``\z`` ignores a series of blank characters after it, including line breaks; it is useful when you need to break multiple lines for a long string constant and want to keep indenting each new line.

Lua The string in can hold any 8-bit value, including 0 represented by `\0. In general, you can use the numeric value of the character to represent the character. The method is to use the escape string `\x_XX_`, where_XX_must be a hexadecimal number of exactly two characters. Or you can use the escape string `\_ddd_`, where_ddd_is one to three decimal digits. (Note that if the escape character happens to be followed by a number symbol, you must write three digits in the escape form。）

For Unicode characters encoded in UTF-8, you can use the escape character `\u{_XXX_}` (there must be a pair of curly braces), where_XXX_is the hexadecimal character number.

Literal strings can also be defined in a way that_long brackets_are enclosed. We define the_n_equal sign inserted between two positive square brackets as the_n_level open long bracket_. That is to say, level 0 long brackets are written as `[[`, level 1 long brackets are written as `[=[`, and so on. _Closed long brackets_are similarly defined; For example, the long brackets for level 4 inversion are written `]====]`. A_long literal string_can begin with an open-length parenthesis of any level and end with a closed-length parenthesis of the first encountered sibling. A string described this way can contain anything, except, of course, for a specific level of backparentheses. The entire lexical analysis process will not be restricted by branches, will not handle any escape characters, and will ignore any long parentheses at different levels. Any form of line feed string encountered (carriage return, line feed, carriage return plus line feed, line feed plus carriage return) will be converted to a single line feed.

Each byte in the literal string that is not affected by the above rules is presented as itself. However, Lua uses text mode to open source file parsing, and some systems' file manipulation functions may have problems with the handling of certain control characters. Therefore, for non-text data, it is safer to enclose it in quotation marks and explicitly express it according to escape character rules.

For convenience, when a long parenthesis is followed by a line break, the line break is not placed in the string. For example, suppose a system uses ASCII code (where ''a'' is encoded as 97, line feed is encoded as 10, and ''1'' is encoded as 49 ), and the following five ways describe exactly the same string.：

     a = 'alo\\n123"'
     a = "alo\\n123\\""
     a = '\\97lo\\10\\04923"'
     a = \[\[alo
     123"\]\]
     a = \[==\[
     alo
     123"\]==\]

_ The numeric constant_(or_numeric quantity_) may consist of an optional decimal part and an optional base-ten exponential part, which is marked with the character ``e` or ``E. Lua also accepts hexadecimal constants starting with `0x` or `0X. Hexadecimal constants also accept the form of decimal plus exponential parts. The exponential part is based on two and is marked with the character ``p`` or ``P. A numeric constant is considered a floating-point number when it contains a decimal or exponential part; otherwise it is considered an integer. Here are some examples of legal integer constants：

     3   345   0xff   0xBEBADA

The following are legal floating-point constants：

     3.0     3.1416     314.16e-2     0.31416E1     34e1
     0x0.1E  0xA23p-4   0X1.921FB54442D18P+1

A portion beginning with a double-bar (`--`) that appears anywhere outside the string is_comment_. If `--` is not followed by an open brace, the comment is_short comment_, and the comment ends at the end of the current line. Otherwise, this is a_long comment_, and the comment area is maintained until the corresponding closed bracket. Long comments are often used to temporarily mask out large sections of code.

### 3.2 – Variable

Variables are where values are stored. There are three types of variables in Lua: global variables, local variables, and fields of tables.

A single name can refer to a global variable or a local variable (or a formal parameter of a function, which is a special form of local variable.）。

	var ::= Name

The name refers to the identifier defined in [§ 3.1](#3.1).

All variable names that are not explicitly declared as local (see [§ 3.3.7](#3.3.7)) are treated as global variables. Local variables have their scope: local variables can be used freely by functions defined in their scope (see. [§3.5](#3.5)）。

Before the first assignment of a variable, the value of the variable is **nil**。

Square brackets are used to index tables：

	var ::= prefixexp ‘**\[**’ exp ‘**\]**’

The meaning of access to global variables and the fields of the table can be changed through the meta-table. Indexing a variable`t [I] `is equivalent to calling gettable_event(t, I)`. (See [§ 2.4](#2.4) for a complete description of the gettable_event function. This function is not defined in lua and cannot be called in lua. Here we mention it only for convenience to illustrate the problem.。）

`var.Name` This syntax is just a syntactic sugar used to represent `var["Name"]`：

	var ::= prefixexp ‘**.**’ Name

The operation on the global variable `x` is equivalent to the operation `_ENV.x`. Because of the way the code block is compiled, `_ENV` can never be a global name (cf. [§2.2](#2.2)）。

### 3.3 – Statement

Lua All common forms of statements similar to Pascal or C are supported. This collection includes assignments, control structures, function calls, and variable declarations.

#### 3.3.1 – statement block

A statement block is a sequence of statements that are executed in order：

	block ::= {stat}

Lua Support_empty statement_, you can use semicolon to divide the statement, you can also start a statement block with semicolon, or write two semicolon in succession：

	stat ::= ‘**;**’

Both function calls and assignment statements may be headed with a parenthesis, which may make Lua's syntax ambiguous. Let's take a look at the following code snippet：

     a = b + c
     (print or io.write)('done')

Grammatically, there are two possible ways of interpretation：

     a = b + c(print or io.write)('done')

     a = b + c; (print or io.write)('done')

The current parser always uses the first structure to parse, and it will see the open parenthesis as the beginning of the parameter passing of the function call. To avoid this ambiguity, it is a good habit to put a semicolon in front of a statement when it begins with a parenthesis.：

     ;(print or io.write)('done')

A statement block can be explicitly delimited as a single statement：

	stat ::= **do** block **end**

Explicit delimitation of a block is often used to control the scope of an internal variable declaration. Sometimes, explicit delimitation is also used to insert return in the middle of a statement block (see. [§3.3.4](#3.3.4)）。

#### 3.3.2 – Code Block

Lua A compilation unit of is called a_code block_. In terms of syntactic composition, a code block is a statement block.

	chunk ::= block

Lua Treat a block of code as an anonymous function with indefinite parameters (see [§ 3.4.11](#3.4.11)). In this way, local variables can be defined within the code block, which can receive parameters and return several values. In addition, the anonymous function is compiled with an external local variable `_ENV` bound to its scope (see [§ 2.2](#2.2)). The function always has `_ENV` as its only upper value, even if the function does not use this variable, it still exists.

The code block can be saved in a file or as a string inside the host program. To execute a block of code, first have Lua load it, pre-compile the code in the block into instructions in the virtual machine, and then Lua uses the virtual machine interpreter to run the compiled code.

A block of code can be pre-compiled into binary form; see the program `luac` and the function [`string.dump`](#pdf-string.dump) for more details. Programs expressed in source code and compiled forms can be freely replaced; Lua will automatically detect the file format for corresponding processing (see [`load`](#pdf-load)）。

#### 3.3.3 – Assignment

Lua Multiple assignments are allowed. Thus, the syntactic definition of assignment is to put a list of variables to the left of the equal sign and a list of expressions to the right of the equal sign. The elements in both sides of the list are separated by commas.：

	stat ::= varlist ‘**\=**’ explist
	varlist ::= var {‘**,**’ var}
	explist ::= exp {‘**,**’ exp}

Expressions are discussed in [§ 3.4](#3.4).

Before the assignment operation, the value list will be adjusted to the number of variables on the left. If there are more values than needed, the extra values are thrown away. If the number of values is not enough, it will be expanded as many **nil** as needed * *. If the expression list ends with a function call, all values returned by the function will be placed in the value list before the adjustment operation (unless the function call is enclosed in parentheses; see [§3.4](#3.4)）。

The assignment statement first allows all expressions to complete the operation, and then the assignment operation. Therefore, the following code

     i = 3
     i, a\[i\] = i+1, 20

will set `a[3]`to 20 without affecting `a[4]`. This is because the `I` in `a [I] `is calculated before it is assigned to 4 (it was 3 at the time). Simply put, such a line

     x, y = y, x

would swap the values of `x` and `y`, and

     x, y, z = y, z, x

The values of `x`,`y`,`z` will be rotated.

The meaning of assignment operations to global variables and fields of the table can be changed by the meta-table. Assigning a variable index such as`t [I] = val` is equivalent to `settable_event(t, I, val)`. (For a detailed description of the function `settable_event, `see [§ 2.4](#2.4). This function is not defined in Lua and cannot be called. We list them here for ease of explanation only.。）

The assignment to the global variable `x = val` is equivalent to `_ENV.x = val` (cf. [§2.2](#2.2)）。

#### 3.3.4 – control structure

**if**, **while**, and **repeat** These control structures conform to the usual meaning, and there is a similar syntax：

	stat ::= **while** exp **do** block **end**
	stat ::= **repeat** block **until** exp
	stat ::= **if** exp **then** block {**elseif** exp **then** block} \[**else** block\] **end**

Lua There is also a **for** statement, which has two forms (see [§3.3.5](#3.3.5)）。

A conditional expression in a control structure can return any value. **false** and **nil** are both considered false. All values other than **nil** and **false** are considered true (in particular, the number 0 and the empty string are also considered true.）。

In the **repeat**-**until** loop, the end point of the inner statement block is not at the **until** keyword, which also includes the conditional expression that follows it. Therefore, local variables defined in the internal statement block of the loop can be used in conditional expressions.

**goto** statement transfers the control point of the program to a label. For syntactic reasons, labels in Lua are also considered statements：

	stat ::= **goto** Name
	stat ::= label
	label ::= ‘**::**’ Name ‘**::**’

The label is visible to the entire statement block in which it is defined, except in the case where a label with the same name is defined in an embedded function and in an embedded statement block. As long as goto does not enter the scope of a new local variable, it can jump to any visible label.

Labels and statements without content are called_empty statements_and they do nothing.

**break** Is used to end a **while**, **repeat**, or **for** loop, which jumps to the next statement outside the loop to run：

	stat ::= **break**

**break** Jump out of the innermost loop.

**return** is used to return a value from a function or block of code (which is actually a function). A function can return more than one value, so the syntax for **return** is

	stat ::= **return** \[explist\] \[‘**;**’\]

**return** Can only be written in the last sentence of a statement block. If you really need to return from the middle of the statement block, you can use an explicit definition of an inner statement block, generally written `do return end `. It can be written like this because now **return** is the last sentence of the (internal) statement block.

#### 3.3.5 – For Statement

**for** There are two forms: one is digital form and the other is universal form.

The numerical form of the **for** loop, which continuously runs the internal code block through a mathematical operation. Here is its syntax：

	stat ::= **for** Name ‘**\=**’ exp ‘**,**’ exp \[‘**,**’ exp\] **do** block **end**

_block_ _name_will be used as a loop variable. Starting from the first_exp_until the value of the second_exp_, the step size is the third_exp_. More specifically, a **for** loop looks like this

     for v = _e1_, _e2_, _e3_ do _block_ end

This is equivalent to code：

     do
       local _var_, _limit_, _step_ = tonumber(_e1_), tonumber(_e2_), tonumber(_e3_)
       if not (_var_ and _limit_ and _step_) then error() end
       _var_ = _var_ - _step_
       while true do
         _var_ = _var_ + _step_
         if (_step_ >= 0 and _var_ > _limit_) or (_step_ < 0 and _var_ < _limit_) then
           break
         end
         local v = _var_
         _block_
       end
     end

Note these points below.：

*   All three control expressions are evaluated only once, before the loop begins. The result of these expressions must be a number.
*   `_var_`，`_limit_`，and `_step_` are invisible variables. The names given here are only for convenience of explanation.
*   If the third expression (step) is not given, the step is set 1 。
*   You can use **break** and **goto** to exit the **for** loop.
*   The loop variable `v` is a local variable inside a loop; if you need to use this value at the end of the loop, assign it to another variable before exiting the loop.

The generic form of **for** works through a function called_iterator. With each iteration, the iterator function is called to produce a new value, and when this value is **nil**, the loop stops. The syntax of the generic form of a **for** loop is as follows：

	stat ::= **for** namelist **in** explist **do** block **end**
	namelist ::= Name {‘**,**’ Name}

Such a **for** statement

     for _var\_1_, ···, _var\_n_ in _explist_ do _block_ end

It is equivalent to such a piece of code：

     do
       local _f_, _s_, _var_ = _explist_
       while true do
         local _var\_1_, ···, _var\_n_ = _f_(_s_, _var_)
         if _var\_1_ == nil then break end
         _var_ = _var\_1_
         _block_
       end
     end

Note the following points：

*   `_explist_` It will only be counted once. It returns three values, an_iterator_function, an_state_, and the initial value of the_iterator._。
*   `_f_`， `_s_`，Variables with `_var_` are invisible. The names given here are just for the convenience of explanation.
*   You can use **break** to get out of the **for** loop.
*   The loop variable `_var_ I_` is local to the loop; you can`t use it after the loop ends. If you need to keep these values, assign them to other variables before the loop breaks out or ends.

#### 3.3.6 – function call statement

To allow the use of function side effects, a function call can be executed as a statement：

	stat ::= functioncall

In this case, all return values are discarded. Function calls are explained in [§ 3.4.10](#3.4.10).

#### 3.3.7 – Local declaration

Local variables can be declared anywhere in the statement block. A declaration can contain an initialization assignment operation：

	stat ::= **local** namelist \[‘**\=**’ explist\]

The syntax of the initial assignment operation is the same as that of the assignment operation, if any (see [§ 3.3.3](#3.3.3) ). If there is no initialization value, all variables are initialized **nil**。

A code block is also a block of statements (see [§ 3.3.2](#3.3.2)), so local variables can be placed outside of those explicitly noted in the code block.

The visibility rules for local variables are explained in [§ 3.5](#3.5).

### 3.4 – Expression

Lua There are these basic expressions in：

	exp ::= prefixexp
	exp ::= **nil** | **false** | **true**
	exp ::= Numeral
	exp ::= LiteralString
	exp ::= functiondef
	exp ::= tableconstructor
	exp ::= ‘**...**’
	exp ::= exp binop exp
	exp ::= unop exp
	prefixexp ::= var | functioncall | ‘**(**’ exp ‘**)**’

Numbers and literal strings are explained in [§ 3.1](#3.1); variables are explained in [§ 3.2](#3.2); function definitions are explained in [§ 3.4.11](#3.4.11); function calls are explained in [§ 3.4.10](#3.4.10); and table construction is explained in [§ 3.4.9](#3.4.9). The expression for variable parameters is written in three points (''...''), which can only be used directly in functions with variable parameters; these are explained in [§ 3.4.11](#3.4.11).

Binary operators include mathematical operation operators (see [§ 3.4.1](#3.4.1)), bit operators (see [§ 3.4.2](#3.4.2)), comparison operators (see [§ 3.4.4](#3.4.4)), logical operators (see [§ 3.4.5](#3.4.5)), and concatenate operators (see [§ 3.4.6](#3.4.6)). The unary operators include the minus sign (see [§ 3.4.1](#3.4.1)), the bitwise NOT (see [§ 3.4.2](#3.4.2)), the logical NOT (see [§ 3.4.5](#3.4.5)), and the take-length operator (see [§3.4.7](#3.4.7)）。

Both function calls and variable argument expressions can be placed in multiple return values. If a function call is treated as a statement (see [§ 3.3.6](#3.3.6)), its return value list is adjusted to zero elements, I .e. all return values are discarded. If the expression is used on the last (or only) element of the expression list, no adjustment is made (unless the expression is enclosed in parentheses). In other cases, Lua will adjust the result to an element and place it in the expression list, I .e. keep the first result and ignore all the values after it, or fill in the single when there is no result. **nil**。

Here are some examples：

     f()                -- Adjust to 0 results
     g(f(), x)          -- f() will be adjusted to a result
     g(x, f())          -- g Receive all results returned by x and f()
     a,b,c = f(), x     -- f() were adjusted to 1 result (c received nil）
     a,b = ...          -- a Received the first argument of the list of variadic arguments，
                        -- b Receive the second parameter (if the variable parameter list.
                        -- Without actual parameters, both a and B receive nil）

     a,b,c = x, f()     -- f() Adjusted for 2 results
     a,b,c = f()        -- f() Adjusted for 3 results
     return f()         -- Returns all returned results of f()
     return ...         -- Returns all parameters received from the variable parameter list parameters
     return x,y,f()     -- Returns all values returned by x, y, and f()
     {f()}              -- Create a list with all the return values of f()
     {...}              -- Create a list with all the values in the variable parameter
     {f(), nil}         -- f() be adjusted to a result

An expression enclosed in parentheses is always treated as a value. So, `(f(x,y,z))` even though `f` returns multiple values, the expression will always be a single value. The value of (`(f(x,y,z))` is the first value returned by `f. If `f` does not return a value, then its value is **nil** 。）

#### 3.4.1 – Mathematical Operation Operator

Lua The following mathematical operators are supported：

*   **`+`:** Addition
*   **`-`:** subtraction
*   **`*`:** Multiplication
*   **`/`:** floating point division
*   **`//`:** division down
*   **`%`:** Modulus
*   **`^`:** power
*   **`-`:** take negative

With the exception of power and floating-point division operations, mathematical operations work as follows: If both operands are integers, the operation operates as an integer and the result will also be an integer. Otherwise, when both operands are numbers or strings that can be converted to numbers (see [§ 3.4.3](#3.4.3)), the operands are converted to two floating-point numbers, and the operation is performed according to the usual floating-point rules (generally following the IEEE 754 standard), and the result is also a floating-point number.

The power and floating-point division (`/`) always converts the operand to a floating-point number, and the result is always a floating-point number. The power uses the ISO C function `pow`, so it can also accept non-integer exponents.

Division (`//`) refers to division once and rounding the quotient to the side close to negative infinity, I .e. division of the operand floor 。

The modulus is defined as the remainder of the division, and its quotient is rounded to the side close to negative infinity (the division rounded down）。

For the overflow problem of integer mathematical operations, the strategy adopted by these operations is to follow the usual_wrap_rule for mathematical operations with 2 as the complement. (In other words, they return the number after the mathematical result of their operation is modulo_264_。）

#### 3.4.2 – bit operator

Lua The following bit operators are supported：

*   **`&`:** Bits and
*   **`|`:** Bits or
*   **`~`:** bitwise exclusive or
*   **`>>`:** Move Right
*   **`<<`:** Move left
*   **`~`:** bitwise non

All bit operations convert the operand to an integer (see [§ 3.4.3](#3.4.3)) and then operate bitwise, the result of which is an integer.

For both right and left shifts, the gaps are filled with zeros. If the number of bits moved is negative, it will be shifted in the opposite direction; If the absolute value of the number of bits moved is greater than or equal to the number of bits of the integer itself, the result is zero (all bits are moved out）。

#### 3.4.3 – Forced Conversion

Lua The internal representations of some types and values do some mathematical transformations at run time. Bit operations always convert floating-point operands to integers. Exponentiation and floating-point division always convert integers to floating-point numbers. Other mathematical operations convert integers to floating-point numbers for mixed operands (integers and floating-point numbers); this is known as the usual rule_. The C API also converts integers to floats and floats to integers on demand. In addition, string concatenation operations can also accept numbers as parameters in addition to strings.

Lua also converts the string to a number when the operation requires a number.

When converting an integer to a floating-point number, if the integer value happens to be represented as a floating-point number, then take that floating-point number. Otherwise, the conversion takes the nearest larger or smaller value to represent the number. This conversion will not fail.

The process of converting a floating-point number to an integer checks whether the floating-point number can be accurately expressed as an integer (I. e., the floating-point number is an integer value and is within the range that the integer can express). If you can, the result is that number, otherwise the conversion fails.

The conversion process from a string to a number follows the following process: First, the syntax is analyzed according to the rules of the Lua lexer to convert to the corresponding integer or floating point number. (Strings can have leading or trailing spaces and a symbol.) The resulting number is then converted to the desired type (floating point or integer）。

Converts a number to a string using a non-specified human-readable format. To fully control the number-to-string conversion process, you can use the `format` function in the string library (see [`string.format`](#pdf-string.format)）。

#### 3.4.4 – Comparison Operators

Lua The following comparison operators are supported：

*   **`==`:** Equal
*   **`~=`:** not equal
*   **`<`:** Less
*   **`>`:** Greater
*   **`<=`:** Less than or equal
*   **`>=`:** Greater than or equal

The result of these operations is either **false** or **true**。

The equals operation (`= =`) compares the type of the operands first. If the types are different, the result is **false * *. Otherwise, continue to compare values. Strings are compared in the usual way. Numbers follow the rules of binary operations: if both operands are integers, they are compared as integers; otherwise, they are first converted to floating-point numbers and then compared.

Tables, user data, and threads are all compared by reference: they are considered equal only if they refer to the same object. Every time you create a new object (a table, user data, or a thread), the new object must be different from the existing and existing object. Closure of the same reference must be equal. Any perceptible differences (different behaviors, different definitions) must be unequal.

You can change the way Lua compares table and user data by using the "eq" meta-method (see [§ 2.4](#2.4)).

Equals operations do not convert strings to numbers and vice versa. That is, `"0" = = 0` results in **false**, and`t[0]`and`t["0"]` refer to different items in the table.

`~=` The operation is completely equivalent to the inverse of the (`= =`) operation.

The size comparison operation is performed in the following manner. If the arguments are all numbers, they proceed in the regular of a binary operation. Otherwise, if both parameters are strings, their values are compared by the current locale. Then, Lua tries to call the "lt" or "le" meta-method (see [§ 2.4](#2.4)). The comparison of `a> B `is translated as` B <a` and `a >= B `is translated `b <= a`。

#### 3.4.5 – logical operator

Lua The logical operators in are **and**, **or**, and **not * *. As with control structures (see [§ 3.3.4](#3.3.4)), all logical operators treat **false** and **nil** as false, and everything else as true.

The **not** operation always returns one of **false** or **true. with the **and** operator returns the first argument when the first argument is **false** or **nil**; otherwise, **and** returns the second argument. or **or** operator returns the first parameter when the first parameter is neither **nil** nor **false**, otherwise returns the second parameter. **and** and **or** both follow the short-circuit rule; that is, the second operand is evaluated only when needed. Here are some examples：

     10 or 20            --> 10
     10 or error()       --> 10
     nil or "a"          --> "a"
     nil and 10          --> nil
     false and error()   --> false
     false and nil       --> false
     false or nil        --> nil
     10 and 20           --> 20

（In this manual, `-->` refers to the result of the previous expression。）

#### 3.4.6 – String concatenation

Lua The join operator in the string is written as two dots (''..''). If both operands are strings or numbers, the concatenation operation will convert them to strings according to the rules mentioned in [§ 3.4.3](#3.4.3). Otherwise, the meta-method '__concat' is called (cf. [§2.4](#2.4)）。

#### 3.4.7 – take length operator

Take the length operator to write the unary prefix `#`. The length of a string is its number of bytes (that is, the length of the string calculated in one character by one byte.）。

The `__len` meta-method (see [§ 2.4](#2.4)) can be used to modify the behavior of the length operation on any value outside of the string type.

If the `__len` meta-method is not given, the length of table `t` is only defined when the table is a_sequence. A sequence refers to a table with a positive set of keys equal to_{1 .. n }_, where_n_is a non-negative integer. In this case,_n_is the length of the table. Note that such a table

     {10, 20, nil, 40}

is not a sequence because it has bond `4` but no bond `3 `. (Therefore, the set of positive integer keys for the table is not equal to the_{1 .. n}_set, so there is no_n_.) Note that whether a table is a sequence has nothing to do with its non-numeric keys.

#### 3.4.8 – Priority

Lua The priorities of the operators in are written in the following table, sorted from low to high priority.：

     or
     and
     <     >     <=    >=    ~=    ==
     |
     ~
     &
     <<    >>
     ..
     +     -
     \*     /     //    %
     unary operators (not   #     -     ~)
     ^

Usually, you can use parentheses to change the order of operations. The join operator (``..``) and the power operation (``^``) are right-to-left. All other operations are from left to right.

#### 3.4.9 – Table Construction

A table constructor is an expression that constructs a table. Each time the constructor is executed, a new table is constructed. Constructors can be used to construct an empty table, or they can be used to construct a table and initialize some of its fields. The general constructor syntax is as follows

	tableconstructor ::= ‘**{**’ \[fieldlist\] ‘**}**’
	fieldlist ::= field {fieldsep field} \[fieldsep\]
	field ::= ‘**\[**’ exp ‘**\]**’ ‘**\=**’ exp | Name ‘**\=**’ exp | exp
	fieldsep ::= ‘**,**’ | ‘**;**’

Each field of the shape `[exp1] = exp2` adds a new item to the table with the key `exp1` and the value `exp2`. A field of the shape `name = exp` is equivalent to `["name"] = exp`. Finally, a field of the shape `exp` is equivalent to `[I] = exp`, where` I `is a growing number starting at 1. Other fields in this format do not break their notation. Take an example：

     a = { \[f(1)\] = g; "x", "y"; x = 1, f(x), \[30\] = 23; 45 }

equivalent

     do
       local t = {}
       t\[f(1)\] = g
       t\[1\] = "x"         -- 1st exp
       t\[2\] = "y"         -- 2nd exp
       t.x = 1            -- t\["x"\] = 1
       t\[3\] = f(x)        -- 3rd exp
       t\[30\] = 23
       t\[4\] = 45          -- 4th exp
       a = t
     end

The order of assignments in the constructor is undefined. (The order problem will only affect the situation when those keys are repeated.。）

If the last field in the form is in the form of `exp` and its expression is a function call or a variable parameter, then all the return values of this expression will go into the list in turn (see [§3.4.10](#3.4.10)）。

The initialization domain table can have one more separator at the end, so that the design can be easily generated by the machine.

#### 3.4.10 – function call

Lua The syntax for the function call in is as follows：

	functioncall ::= prefixexp args

In the first step of a function call, prefixexp and args are evaluated first. If the type of the prefixexp value is_function_, then the function is called with the given arguments. Otherwise the prefixexp meta-method "call" is called, the first parameter is the prefixexp value, followed by the original call parameter (cf. [§2.4](#2.4)）。

such a form

	functioncall ::= prefixexp ‘**:**’ Name args

Can be used to call a "method". This is a syntactic sugar supported by Lua. Like `v:name(args)`, it is interpreted as `v.name(v,args)`, where `v` is evaluated only once.

The syntax of the parameter is as follows：

	args ::= ‘**(**’ \[explist\] ‘**)**’
	args ::= tableconstructor
	args ::= LiteralString

All argument expressions are evaluated before the function call. The call form `f{fields}`is a syntactic sugar for `f({fields})`; here the argument list is a newly created list. The form `f`_string_``(or `f"_string_"`or `f[[_string_]]`) is also syntactic sugar for `f(`_string_`)`; the argument list is a single string.

`return _functioncall_` Such a call form will trigger a_tail call_. Lua implements_full tail call_(or_full tail recursion_): In a tail call, the called function reuses the stack items of the calling function. Thus, there is no limit to the number of layers of nested tail calls that a program executes. However, the tail call will remove any debug information for the function that called it. Note that tail calls only occur under a specific syntax, only if **return** has only a single function call as an argument; this syntax allows all results of the calling function to be returned in its entirety. Therefore, the following examples are not tail calls.：

     return (f(x))        -- The return value is adjusted to a
     return 2 \* f(x)
     return x, f(x)       -- Append several return values
     f(x); return         -- All return values are discarded
     return x or f(x)     -- The return value is adjusted to a

#### 3.4.11 – Function Definition

The syntax for a function definition is as follows：

	functiondef ::= **function** funcbody
	funcbody ::= ‘**(**’ \[parlist\] ‘**)**’ block **end**

In addition, some syntactic sugar is defined to simplify the writing of function definitions.：

	stat ::= **function** funcname funcbody
	stat ::= **local** **function** Name funcbody
	funcname ::= Name {‘**.**’ Name} \[‘**:**’ Name\]

The statement

     function f () _body_ end

be translated

     f = function () _body_ end

The statement

     function t.a.b.c.f () _body_ end

be translated

     t.a.b.c.f = function () _body_ end

The statement

     local function f () _body_ end

be translated

     local f; f = function () _body_ end

instead

     local f = function () _body_ end

（This difference only exists when the function body needs to reference `f`。）

A function definition is an executable expression whose result is a value of type_function. When Lua precompiles a code block, the code block acts as a function, and the entire function body is precompiled. So, whenever Lua executes a function definition, the function itself is_instantiated_(or_closed_). The instance (or_closure_) of this function is the final value of the expression.

A formal parameter is treated as some local variable that will be initialized by the value of the argument：

	parlist ::= namelist \[‘**,**’ ‘**...**’\] | ‘**...**’

When a function is called, if the function is not a_variable parameter function_, that is, three points (''...'') are noted at the end of the formal parameter list, then the argument list is adjusted to the length of the formal parameter list. The variable-length parameter function does not adjust the argument list; instead, it will put all the extra arguments together and pass them to the function through the variable-length parameter expression, which is still written as three points. The value of this expression is a list of argument values, which looks like a function that can return multiple results. If a variable-length argument expression is used in another expression, or in the middle of another string of expressions, its return value is adjusted to a single value. If this expression is placed at the end of a series of expressions, no adjustment will be made (unless this last argument is enclosed in parentheses）。

Let's make the following definition first, and then look at an example.：

     function f(a, b) end
     function g(a, b, ...) end
     function r() return 1,2,3 end

Let's look at the mapping of real parameters to shape parameters and variable length parameters.：

     CALL            PARAMETERS

     f(3)             a=3, b=nil
     f(3, 4)          a=3, b=4
     f(3, 4, 5)       a=3, b=4
     f(r(), 10)       a=1, b=10
     f(r())           a=1, b=2

     g(3)             a=3, b=nil, ... -->  (nothing)
     g(3, 4)          a=3, b=4,   ... -->  (nothing)
     g(3, 4, 5, 8)    a=3, b=4,   ... -->  5  8
     g(5, r())        a=5, b=1,   ... -->  2  3

The result is returned by **return** (see [§ 3.3.4](#3.3.4)). If you execute to the end of the function without encountering any **return** statements, the function will not return any results.

The limit on the number of values a function can return is system-dependent. This limit must be greater 1000 。

_ The colon_syntax can be used to define_method_, that is, a function can have an implicit parameter `self `. Therefore, the following statement

     function t.a.b.c:f (_params_) _body_ end

is the grammatical sugar of such a way of writing

     t.a.b.c.f = function (self, _params_) _body_ end

### 3.5 – Visibility Rules

Lua Language has lexical scope. The scope of variables begins with the first statement segment after they are declared and ends with the last non-empty statement of the innermost statement block containing the declaration. Look at these examples below：

     x = 10                -- global variable
     do                    -- New statement block
       local x = x         -- A new 'x' whose value is now 10
       print(x)            --> 10
       x = x+1
       do                  -- Another statement block
         local x = x+1     -- yet another one 'x'
         print(x)          --> 12
       end
       print(x)            --> 11
     end
     print(x)              --> 10 （Which one is the global one）

Note here that for statements like `local x = x`, the new `x` is being declared, but has not yet entered its scope, so the second `x` points to a variable from an outer layer.

Because there is such a lexical scoping rule, a local variable can be used freely by functions defined within its scope. When a local variable is used by the inner function, it is called the_upper value_, or_outer local variable by the inner function._。

Note that each execution of a **local** statement defines a new local variable. Look at such an example：

     a = {}
     local x = 20
     for i=1,10 do
       local y = 0
       a\[i\] = function () y=y+1; return x+y end
     end

This loop creates ten closures (this means ten instances of anonymous functions). Each of these closures uses a different `y` variable, and they share the same `x`。

## 4 – Programming Interface

This section describes Lua`s C API, a set of C functions that the host program uses to communicate with Lua. All API functions are declared in the header file `lua.h` with their associated types and constants.

Although we are talking about "functions", part of the simple API is provided in the form of macros. Unless otherwise noted, all of these macros use their arguments only once (except for the first argument, which must be the Lua state), so you don't have to worry about the side effects of expanding these macros.

C All Lua API functions in the library do not check whether the parameters are compatible and valid. However, you can change this behavior by adding a macro switch `LUA_USE_APICHECK when compiling Lua.

### 4.1 – Stack

Lua Use a_virtual stack_to pass values to and from C. Each element on the stack is a Lua value (**nil**, number, string, etc.）。

Whenever Lua calls C, the called function gets a new stack, which is independent of the stack of the C function itself and of the previous Lua stack. It contains all the parameters Lua passes to the C function, and the C function puts the result to be returned into this stack to return to the caller (see [`lua_CFunction`](#lua_CFunction)）。

For convenience, all API query operations on the stack do not strictly follow the stack's operating rules. Instead, you can use an_index_to point to any element on the stack: a positive index refers to the absolute position on the stack (starting at 1); a negative index refers to the offset from the top of the stack. In terms of expansion, if the stack has_n_elements, then index 1 represents the first element (that is, the element that is pushed first) and index_n_refers to the last element; Index -1 also refers to the last element (that is, the element at the top of the stack), and index_\-n_refers to the first element.

### 4.2 – Stack Size

When you use the Lua API, you are responsible for making the appropriate calls. In particular, it is your responsibility to control the stack overflow_. You can use the [lua_checkstack](#lua_checkstack) function to increase the size of the available stack.

Whenever Lua calls C, it only guarantees that there is at least LUA_MINSTACK so much stack space available. LUA_MINSTACK is generally defined as 20, so as long as you`re not constantly stacking data, you don`t usually care about stack size.

When you call a Lua function without specifying how many return values to receive (see [lua_call](#lua_call)), Lua guarantees that there will be enough space on the stack to receive all the return values, but there is no guarantee that there will be extra space. Therefore, after making such a call, if you need to continue to push the stack, you need to use [`lua_checkstack`](#lua_checkstack)。

### 4.3 – Valid and Acceptable Indexes

API If the function in needs to pass in the stack index, this index must be a valid index or an acceptable index._。

_ Valid index_refers to the index of the real position in the stack; that is, the position between 1 and the top of the stack (`1 ≤ abs(index) ≤ top`). Typically, a function that might modify the value at that location needs to pass in a valid index.

Unless otherwise noted, any function that can accept a valid index also accepts a_pseudo-index_. Pseudo-indexes refer to Lua values that can be accessed by C code, but they are not on the stack. This is used to access the registry as well as the upper value of the C function (see [§4.4](#4.4)）。

For functions that only need values in the stack (such as query functions) and do not need to specify a stack location, they can be called with an acceptable index. The acceptable index can be not only any valid index including a pseudo index, but also any positive index that exceeds the top of the stack but falls within the space allocated for the stack. (Note that 0 is never an acceptable index.) Unless otherwise noted, functions in the API accept acceptable indexes.

Acceptable indexes are allowed to avoid additional checks for queries outside the top of the stack. For example, a C function can directly query the third parameter passed to it without first checking whether there is a third parameter, that is, there is no need to check whether 3 is a valid index.

For functions that can accept index calls, the invalid index is considered to contain a value of virtual type LUA_TNONE, which behaves the same as nil.

### 4.4 – C closure

When a C function is created, it is possible to associate some values together, I .e. create a_C closure_(see [lua_pushcclosure](#lua_pushcclosure)); these associated values are called_upper values_, and they can be accessed when the function is called.

Whenever a C function is called, the upper value of the function can be located with a pseudo index. We can use the [lua_upvalueindex](#lua_upvalueindex) macro to generate these pseudo-indexes. The first value associated with the function is placed in the lua_upvalueindex(1) position, and so on. When using `lua_upvalueindex(_n_)`, if_n_is greater than the total number of upper values of the current function (but cannot be greater than 256), it will produce an acceptable but invalid index.

### 4.5 – Registry

Lua A_registry_is provided, which is a predefined table that can be used to hold any Lua value that C code wants to hold. This table can be located with a valid pseudo-index LUA_REGISTRYINDEX. Any C library can store data in this table. To prevent conflicts, you need to choose the key names carefully. The general usage is that you can use a string containing your library name as the key name, or take the address of your own C object as the key in the form of lightweight user data, or use any Lua object created by your code as the key. Regarding the variable name, the name of the string key name with the underlined letter is reserved by Lua.

Integer keys in the registry are used for the reference mechanism (see [luaL_ref](#luaL_ref)), as well as some predefined values. Therefore, integer keys should not be used for other purposes.

When you create a new Lua state machine, several values are predefined in the registry. These predefined values can be indexed to by integers, which are defined in the form of constants in `lua.h. have the following constants：

*   **`LUA_RIDX_MAINTHREAD`:** Under this index in the registry is the main thread of the state machine. (The main thread and the state machine are created at the same time.。）
*   **`LUA_RIDX_GLOBALS`:** Under this index of the registry is the global environment.

### 4.6 – C Error handling in

In its internal implementation, Lua uses C`s `longjmp` mechanism to handle errors. (If you compile in C, Lua will replace it with an exception; search for `LUAI_THROW in the source code for details.) When Lua encounters any errors (such as memory allocation errors, type errors, syntax errors, and runtime errors) it will throw out an error; that is, call a long jump. In the_protected environment, Lua uses `setjmp` to set a recovery point; any errors that occur will jump to the nearest recovery point.

If the error occurs outside the protected environment, Lua will first call the_panic function_(see [lua_atpanic](#lua_atpanic)) and then call `abort` to exit the host program. Your panic function can not exit the program as long as it does not return (for example, long jump to the recovery point you set yourself outside Lua).

panic The function operates as an error message handler (see [§ 2.3](#2.3)); the error message is at the top of the stack. The difference is that it does not guarantee stack space. Before doing any stack pressing, the panic function must first check whether there is enough space (see [§4.2](#4.2)）。

Most API functions have the potential to throw errors, such as when there is a memory allocation error. The documentation for each function notes whether it might throw an error.

Inside a C function, you can throw an error by calling [`lua_error`](#lua_error).

### 4.7 – C Give-up processing in

Lua Internally use C`s `longjmp` mechanism to give up a coroutation. Therefore, if a C function` foo` calls an API function, and the API function gives up (directly or indirectly calls the give-out function). Since `longjmp` removes the stack frame of the C stack, Lua cannot return to `foo.

In order to avoid this kind of problem, when calling out in API calls, in addition to those API that throw errors, three functions are provided: [lua_yieldk](#lua_yieldk), [lua_callk](#lua_callk), and [lua_pcallk](#lua_pcallk). They can continue to run from the_continuation function_(parameter named `k`) passed in when the yield occurs.

We need to presuppose some terms to explain the continuation point. For C functions called from Lua, we call them_primitive_. The three C API functions described above that are called from this primitive function are called_called functions_. The called function can make the current thread give way. (yield occurs when the called function is [`lua_yieldk`](#lua_yieldk), or the function passed into [`lua_callk`](#lua_callk) or [`lua_pcallk`](#lua_pcallk) calls yield。）

Suppose the running thread gives up when executing the called function. When the thread is continued again, it wants to continue the function being called. However, the called function cannot be returned to the original function. This is because the previous yield operation destroyed the stack frame of the C stack. As an alternative, Lua calls the_continuation function_given as an argument to the called function_. As the name suggests, a continuation function will continue the task of the original function.

The following function will do a description：

     int original\_function (lua\_State \*L) {
       ...     /\* code 1 \*/
       status = lua\_pcall(L, n, m, h);  /\* calls Lua \*/
       ...     /\* code 2 \*/
     }

Now we want to allow Lua code that is run by [lua_pcall](#lua_pcall) to give up. First, we rewrite the function like this.：

     int k (lua\_State \*L, int status, lua\_KContext ctx) {
       ...  /\* code 2 \*/
     }

     int original\_function (lua\_State \*L) {
       ...     /\* code 1 \*/
       return k(L, lua\_pcall(L, n, m, h), ctx);
     }

In the above code, the new function `k` is a_continuation function_(the function type is [lua_KFunction](#lua_KFunction)). Its job is what it does after the call to [lua_pcall](#lua_pcall) in the original function. Now we must inform Lua that you must continue to call `k` after the Lua code executed by [`lua_pcall`](#lua_pcall) has been interrupted (error or yield) `. So we have to continue to rewrite this code and replace [`lua_pcall`](#lua_pcall) [`lua_pcallk`](#lua_pcallk)：

     int original\_function (lua\_State \*L) {
       ...     /\* code 1 \*/
       return k(L, lua\_pcallk(L, n, m, h, ctx2, k), ctx1);
     }

Note the additional explicit call to the continuation function here: Lua calls the continuation function only when it is needed, which may be caused by an error, or it may be due to a concession that needs to continue running. If no yield has occurred and the called function returns normally, then [lua_pcallk](#lua_pcallk) (and [lua_callk](#lua_callk)) will also return normally. (Of course, in this example, you can also not call the continuation function later, but write the work that needs to be done directly after the call of the original function.。）

In addition to the Lua state, the continuation function has two parameters: one is the last state code of the call, and the other is the context (`ctx`) initially passed in by [lua_pcallk](#lua_pcallk). (Lua does not use this value itself; it simply forwards the value from the original function to the continuation function.) For [`lua_pcallk`](#lua_pcallk), the status code and [`lua_pcallk`](#lua_pcallk) should return the same value, except that the status code is [`LUA_YIELD`](#pdf-LUA_YIELD)(instead of [`LUA_OK `](#pdf-LUA_ OK)) when the execution is completed after a release. For [`lua_yieldk`](#lua_yieldk) and [`lua_callk`](#lua_callk), the status code passed in by calling the continuation function must be [`LUA_YIELD`](#pdf-LUA_YIELD). (For these two functions, Lua does not call continuation functions for any errors, because they do not handle errors.) Similarly, when you use [lua_callk](#lua_callk), you should use [`LUA_OK `](#pdf-LUA_ OK) as the status code to call the continuation function. (For [lua_yieldk](#lua_yieldk), there is almost no place to call the continuation function directly, because [lua_yieldk](#lua_yieldk) itself does not return。）

Lua Will treat the continuation function as the original function. The continuation function will receive the same Lua stack as the original function, and the lua state it receives is also consistent with the state the called function should have after returning. (For example, after the [lua_callk](#lua_callk) call, previously pushed functions and call parameters on the stack are replaced by the return value generated by the call.) It also has the same upper value. When it returns, Lua will treat it as the return of the original function to operate.

### 4.8 – Functions and Types

Here is an alphabetical list of all the functions and types in the C API. Each function has a hint like this： \[-o, +p, _x_\]

For the first field, 'o' refers to how many elements the function will pop from the stack. The second field, 'p', refers to how many elements the function will stack. (All functions will pop the parameters and then push the results on the stack.) The field of the form 'x | y' indicates that the function may push (or pop) 'x' or 'y' elements depending on the situation; the question mark ''?'' indicates that we cannot know how many elements the function will pop/push (for example, the number depends on what is on the stack) just by the argument. The third field, 'x', explains whether the function will throw an error: ''-'' means that the function will never throw an error; ''e'' means that the function may throw an error; ''v'' means that the function may throw a meaningful error.

* * *

#### `lua_absindex`

\[-0, +0, –\]

int lua\_absindex (lua\_State \*L, int idx);

Convert an acceptable index `idx` to an absolute index (that is, a value that does not depend on where the top of the stack is）。

* * *

#### `lua_Alloc`

typedef void \* (\*lua\_Alloc) (void \*ud,
                             void \*ptr,
                             size\_t osize,
                             size\_t nsize);

Lua The type of memory allocator function used in the state machine. The memory allocation function must provide a function similar to `realloc` but not identical. Its parameters are `ud`, a pointer passed to it by [lua_newstate](#lua_newstate); `ptr`, a pointer to a memory block that has been allocated/will be reallocated/to be freed; `Osize`, the original size of the memory block or a code about what will be allocated; `nsize`, the size of the new memory block.

If `ptr` is not `NULL`, `osize` is the size of the memory block pointed to by `ptr`, that is, the size of the memory block originally allocated or reallocated.

If `ptr` is `NULL`, `osize` is the encoding of the type of object Lua is about to assign. When (and only if) Lua creates a new object of the corresponding type, `osize` is one of [LUA_TSTRING](#pdf-LUA_TSTRING), [LUA_TTABLE](#pdf-LUA_TTABLE), [LUA_TFUNCTION](#pdf-LUA_TFUNCTION), [LUA_TUSERDATA](#pdf-LUA_TUSERDATA), or [LUA_TTHREAD](#pdf-LUA_TTHREAD). If `osize` is something else, Lua will allocate memory for something else.

Lua The allocator function is assumed to follow the following behavior：

When `nsize` is zero, the allocator must behave like `free` and return `NULL`。

When `nsize` is not zero, the allocator must behave like `realloc. If the allocator cannot complete the request, it returns `NULL `. Lua assumes that the allocator will never fail if `osize >= nsize` holds.

Here is a simple implementation of the allocator function. This implementation is placed in the supplemental library for use by [luaL_newstate](#luaL_newstate).

     static void \*l\_alloc (void \*ud, void \*ptr, size\_t osize,
                                                size\_t nsize) {
       (void)ud;  (void)osize;  /\* not used \*/
       if (nsize == 0) {
         free(ptr);
         return NULL;
       }
       else
         return realloc(ptr, nsize);
     }

Note that standard C ensures that `free(NULL)`has no side effects and that `realloc(NULL,size)` is equivalent to `malloc(size)`. This code assumes that `realloc` will not fail when reducing the block length. (Although standard C does not guarantee this behavior, this seems to be a safe assumption。）

* * *

#### `lua_arith`

\[-(2|1), +1, _e_\]

void lua\_arith (lua\_State \*L, int op);

Do a mathematical or bit operation on the top two values (or one, say, inverted) of the stack. where the value at the top of the stack is the second operand. It pops the pushed value and puts the result on top of the stack. This function follows Lua's corresponding operator arithmetic rules (I. e., it is possible to trigger meta-methods.）。

`op` The value of must be one of the following constants：

*   **`LUA_OPADD`:** Addition (`+`)
*   **`LUA_OPSUB`:** subtraction (`-`)
*   **`LUA_OPMUL`:** Multiplication (`*`)
*   **`LUA_OPDIV`:** floating point division (`/`)
*   **`LUA_OPIDIV`:** Division with round down (`//`)
*   **`LUA_OPMOD`:** Modulus (`%`)
*   **`LUA_OPPOW`:** power (`^`)
*   **`LUA_OPUNM`:** take negative (one yuan `-`)
*   **`LUA_OPBNOT`:** bitwise inversion (`~`)
*   **`LUA_OPBAND`:** Bits and (`&`)
*   **`LUA_OPBOR`:** Bits or (`|`)
*   **`LUA_OPBXOR`:** bitwise exclusive or (`~`)
*   **`LUA_OPSHL`:** Move left (`<<`)
*   **`LUA_OPSHR`:** Move Right (`>>`)

* * *

#### `lua_atpanic`

\[-0, +0, –\]

lua\_CFunction lua\_atpanic (lua\_State \*L, lua\_CFunction panicf);

Set a new panic function and return the one you set previously. (See [§4.6](#4.6)）。

* * *

#### `lua_call`

\[-(nargs+1), +nresults, _e_\]

void lua\_call (lua\_State \*L, int nargs, int nresults);

Call a function.

To call a function, please follow the following protocol: first, the function to be called should be pushed onto the stack. Finally, call [`lua_call`](#lua_call); `nargs` is the number of parameters you pushed onto the stack. When the function call is completed, all the parameters and the function itself will be out of the stack. The return value of the function is then pushed on the stack. The number of returned values will be adjusted to `nresults` unless `nresults` is set to LUA_MULTRET `. In this case, all return values are pushed onto the stack. Lua will ensure that the return value is placed in the stack space. Function return values will be pushed in positive order (the first return value is pushed first), so after the call, the last return value will be placed on the top of the stack.

Errors that occur within the called function will always be thrown up (via `longjmp`).

In the following example, this line of Lua code is equivalent to doing some work in C code in the host program.：

     a = f("how", t.x, 14)

Here is the code in C.：

     lua\_getglobal(L, "f");                  /\* function to be called \*/
     lua\_pushliteral(L, "how");                       /\* 1st argument \*/
     lua\_getglobal(L, "t");                    /\* table to be indexed \*/
     lua\_getfield(L, -1, "x");        /\* push result of t.x (2nd arg) \*/
     lua\_remove(L, -2);                  /\* remove 't' from the stack \*/
     lua\_pushinteger(L, 14);                          /\* 3rd argument \*/
     lua\_call(L, 3, 1);     /\* call 'f' with 3 arguments and 1 result \*/
     lua\_setglobal(L, "a");                         /\* set global 'a' \*/

Note that the above code is balanced: at the end, the stack is restored to its original configuration. This is a good programming practice.

* * *

#### `lua_callk`

\[-(nargs + 1), +nresults, _e_\]

void lua\_callk (lua\_State \*L,
                int nargs,
                int nresults,
                lua\_KContext ctx,
                lua\_KFunction k);

The behavior of this function is exactly the same as [lua_call](#lua_call), except that it also allows the called function to give up (cf. [§4.7](#4.7)）。

* * *

#### `lua_CFunction`

typedef int (\*lua\_CFunction) (lua\_State \*L);

C The type of the function.

In order to communicate properly with Lua, C functions must use the following protocols. This protocol defines the method of passing parameters and return values: C functions accept parameters through the stack in Lua, and the parameters are stacked in positive order (the first parameter is stacked first). Therefore, when the function starts, lua_gettop(L) returns the number of arguments received by the function. The first parameter (if any) is at index 1, and the last parameter is at index lua_gettop(L). When returning values to Lua, the C function only needs to push them onto the stack in positive order (the first return value is pushed first), and then return the number of these return values. Below these return values, everything on the stack is dropped by Lua. Like Lua functions, calling C functions from Lua can have many return values.

The function in the following example takes several numeric arguments and returns their average and sum.：

     static int foo (lua\_State \*L) {
       int n = lua\_gettop(L);    /\* Number of parameters \*/
       lua\_Number sum = 0.0;
       int i;
       for (i = 1; i <= n; i++) {
         if (!lua\_isnumber(L, i)) {
           lua\_pushliteral(L, "incorrect argument");
           lua\_error(L);
         }
         sum += lua\_tonumber(L, i);
       }
       lua\_pushnumber(L, sum/n);        /\* First Return Value \*/
       lua\_pushnumber(L, sum);         /\* Second Return Value \*/
       return 2;                   /\* Number of return values \*/
     }

* * *

#### `lua_checkstack`

\[-0, +0, –\]

int lua\_checkstack (lua\_State \*L, int n);

Make sure there are at least `n` extra spaces on the stack. If the stack cannot be expanded to the appropriate size, the function returns false. Reasons for failure include expanding the stack to more than the fixed maximum size (at least a few thousand elements) or failing to allocate memory. This function never shrinks the stack; if the stack is already larger than needed, it stays the same.

* * *

#### `lua_close`

\[-0, +0, –\]

void lua\_close (lua\_State \*L);

Destroys all objects in the specified Lua state machine (garbage collection-related meta methods, if any, are called) and frees all dynamic memory used in the state machine. On some platforms, you don't need to call this function, because when the host program ends, all resources are naturally released. On the other hand, a long-running program, such as a background program or a web server, creates multiple Lua state machines. Then you should quickly close them when you don't need them.

* * *

#### `lua_compare`

\[-0, +0, _e_\]

int lua\_compare (lua\_State \*L, int index1, int index2, int op);

Compares two Lua values. When the value at index `index1` is compared with the value at index `index2` by `op`, the function returns 1. This function follows Lua`s corresponding operation rules (I. e., it is possible to trigger meta methods). Otherwise, the function returns 0. When either index is invalid, the function also returns 0 。

`op` value must be one of the following constants：

*   **`LUA_OPEQ`:** Comparison of equality (`==`)
*   **`LUA_OPLT`:** Less Than Compare (`<`)
*   **`LUA_OPLE`:** Less than or equal to comparison (`<=`)

* * *

#### `lua_concat`

\[-n, +1, _e_\]

void lua\_concat (lua\_State \*L, int n);

Connect the `n` values at the top of the stack, then pull these values off the stack and put the result on the top of the stack. If `n` is 1, the result is that value on the stack (I. e., the function does nothing); if `n` is 0, the result is an empty string. The connection is done according to the usual semantics in Lua (see. [§3.4.6](#3.4.6) ）。

* * *

#### `lua_copy`

\[-0, +0, –\]

void lua\_copy (lua\_State \*L, int fromidx, int toidx);

Copy a value from index `fromidx` to a valid index `toidx`, overwriting the original value there. Values in other locations are not affected.

* * *

#### `lua_createtable`

\[-0, +1, _e_\]

void lua\_createtable (lua\_State \*L, int narr, int nrec);

Creates a new empty table pressure stack. The parameter `narr` suggests how many elements the table will have when used as a sequence; the parameter `nrec` suggests how many elements outside the sequence the table may have. Lua will use these suggestions to pre-allocate the new table. If you know more about the purpose of this table, pre-allocation can improve performance. Otherwise, you can use the function [`lua_newtable`](#lua_newtable) 。

* * *

#### `lua_dump`

\[-0, +0, _e_\]

int lua\_dump (lua\_State \*L,
                        lua\_Writer writer,
                        void \*data,
                        int strip);

Export the function as a block of binary code. The function receives the Lua function at the top of the stack as an argument and then generates its binary code block. If the exported thing is loaded again, the result of the load is equivalent to the original function. When it generates a code block, [lua_dump](#lua_dump) writes data by calling the function `writer` (see [lua_Writer](#lua_Writer) ), followed by the `data` parameter. `writer` 。

If `strip` is true, the binary code block will not contain debugging information for the function.

The last return value from `writer` will be returned as the return value of this function; 0 means there is no error.

This function does not pop Lua functions off the stack.

* * *

#### `lua_error`

\[-1, +0, _v_\]

int lua\_error (lua\_State \*L);

Throws a Lua error with the value at the top of the stack as the error object. This function will do a long jump, so it must not return (cf. [`luaL_error`](#luaL_error)）。

* * *

#### `lua_gc`

\[-0, +0, _e_\]

int lua\_gc (lua\_State \*L, int what, int data);

Controls the garbage collector.

This function initiates several different tasks depending on its parameter `what`：

*   **`LUA_GCSTOP`:** Stop the garbage collector.
*   **`LUA_GCRESTART`:** Restart the garbage collector.
*   **`LUA_GCCOLLECT`:** Initiate a full garbage collection cycle.
*   **`LUA_GCCOUNT`:** Returns the total amount of memory used by Lua (in K bytes）。
*   **`LUA_GCCOUNTB`:** Returns the remainder of the current memory usage divided by the 1024.
*   **`LUA_GCSTEP`:** Initiates one-step incremental garbage collection.
*   **`LUA_GCSETPAUSE`:** Set `data` to_garbage collector intermittent rate_(see [§ 2.5](#2.5)) and return to the previously set value.
*   **`LUA_GCSETSTEPMUL`:** Set `data` to_garbage collector step factor_(see [§ 2.5](#2.5)) and return to the previously set value.
*   **`LUA_GCISRUNNING`:** Returns whether the collector is running (I. e. not stopped）。

For details on these options, see [`collectgarbage`](#pdf-collectgarbage) 。

* * *

#### `lua_getallocf`

\[-0, +0, –\]

lua\_Alloc lua\_getallocf (lua\_State \*L, void \*\*ud);

Returns the memory allocator function for the given state machine. If `ud` is not `NULL` , Lua puts the pointer set when setting the memory allocation function `*ud` 。

* * *

#### `lua_getfield`

\[-0, +1, _e_\]

int lua\_getfield (lua\_State \*L, int index, const char \*k);

Stack the value of`t[k]`, where `t` is the value pointed to by the index. In Lua, this function may trigger the meta-method corresponding to the "index" event (see [§2.4](#2.4) ）。

function returns the type of the pushed value.

* * *

#### `lua_getextraspace`

\[-0, +0, –\]

void \*lua\_getextraspace (lua\_State \*L);

Returns a pointer to the associated memory block in the Lua state machine. Programs can use this memory for any purpose; Lua won't use it.

Each new thread carries a piece of memory, initialized as a copy of that memory for the main thread.

By default, the size of this memory is the size of a null pointer. However you can recompile Lua to set this piece of memory to a different size. (See in `luaconf.h` `LUA_EXTRASPACE`。）

* * *

#### `lua_getglobal`

\[-0, +1, _e_\]

int lua\_getglobal (lua\_State \*L, const char \*name);

Stacks the value in the global variable name and returns the type of the value.

* * *

#### `lua_geti`

\[-0, +1, _e_\]

int lua\_geti (lua\_State \*L, int index, lua\_Integer i);

Stacks the value of`t [I] `, where `t` refers to the value referred to by the given index. As in Lua, this function may trigger the meta-method of the "index" event (see [§2.4](#2.4)）。

Returns the type of the pushed value.

* * *

#### `lua_getmetatable`

\[-0, +(0|1), –\]

int lua\_getmetatable (lua\_State \*L, int index);

If the value at the index has a meta table, the meta table is stacked and 1 is returned. Otherwise nothing will be put on the stack, return 0 。

* * *

#### `lua_gettable`

\[-1, +1, _e_\]

int lua\_gettable (lua\_State \*L, int index);

Stacks the value of`t[k]`, where `t` refers to the value pointed to by the index, and` k` is the value placed at the top of the stack.

This function pops the key on the stack and puts the result in the same position on the stack. As in Lua, this function may trigger a meta-method corresponding to the "index" event (see [§2.4](#2.4) ）。

Returns the type of the pushed value.

* * *

#### `lua_gettop`

\[-0, +0, –\]

int lua\_gettop (lua\_State \*L);

Returns the index of the element at the top of the stack. Because the index is numbered from 1, this result is equal to the number of elements on the stack; specifically, 0 means the stack is empty.

* * *

#### `lua_getuservalue`

\[-0, +1, –\]

int lua\_getuservalue (lua\_State \*L, int index);

Stacks the Lua value associated with the user data at a given index.

Returns the type of the pushed value.

* * *

#### `lua_insert`

\[-1, +1, –\]

void lua\_insert (lua\_State \*L, int index);

Move the top element of the stack to the specified valid index, and then move the elements above this index. Do not call this function with a pseudo-index, because the pseudo-index does not really point to a location on the stack.

* * *

#### `lua_Integer`

typedef ... lua\_Integer;

Lua The integer type in.

When missing, this is` long long` (usually a 64-bit integer with two as its complement), or it can be modified to` long` or` int` (usually a 32-bit integer with two as its complement). (See in `luaconf.h` `LUA_INT` 。）

Lua Two constants are defined: LUA_MININTEGER and LUA_MAXINTEGER to represent the minimum and maximum values that this type can represent.

* * *

#### `lua_isboolean`

\[-0, +0, –\]

int lua\_isboolean (lua\_State \*L, int index);

Returns 1 when the value of the given index is a boolean, otherwise 0 。

* * *

#### `lua_iscfunction`

\[-0, +0, –\]

int lua\_iscfunction (lua\_State \*L, int index);

Returns 1 when the value of the given index is a C function, otherwise 0 。

* * *

#### `lua_isfunction`

\[-0, +0, –\]

int lua\_isfunction (lua\_State \*L, int index);

Returns 1 when the value of the given index is a function (either a C or Lua function), otherwise it returns 0 。

* * *

#### `lua_isinteger`

\[-0, +0, –\]

int lua\_isinteger (lua\_State \*L, int index);

Returns 1 when the value of the given index is an integer (its value is a number and is stored internally as an integer), otherwise it returns 0 。

* * *

#### `lua_islightuserdata`

\[-0, +0, –\]

int lua\_islightuserdata (lua\_State \*L, int index);

Returns 1 when the value of the given index is a lightweight user data, otherwise 0 。

* * *

#### `lua_isnil`

\[-0, +0, –\]

int lua\_isnil (lua\_State \*L, int index);

Returns 1 when the value of the given index is **nil**, otherwise 0 。

* * *

#### `lua_isnone`

\[-0, +0, –\]

int lua\_isnone (lua\_State \*L, int index);

Returns 1 when the given index is invalid, otherwise 0 。

* * *

#### `lua_isnoneornil`

\[-0, +0, –\]

int lua\_isnoneornil (lua\_State \*L, int index);

Returns 1 when the given index is invalid or its value is **nil**, otherwise 0 。

* * *

#### `lua_isnumber`

\[-0, +0, –\]

int lua\_isnumber (lua\_State \*L, int index);

Returns 1 when the value of the given index is a number or a string that can be converted to a number, otherwise 0 。

* * *

#### `lua_isstring`

\[-0, +0, –\]

int lua\_isstring (lua\_State \*L, int index);

Returns 1 when the value of the given index is a string or a number (a number can always be converted to a string), otherwise 0 。

* * *

#### `lua_istable`

\[-0, +0, –\]

int lua\_istable (lua\_State \*L, int index);

Returns 1 when the value of the given index is a table, otherwise 0 。

* * *

#### `lua_isthread`

\[-0, +0, –\]

int lua\_isthread (lua\_State \*L, int index);

Returns 1 when the value of the given index is a thread, otherwise 0 。

* * *

#### `lua_isuserdata`

\[-0, +0, –\]

int lua\_isuserdata (lua\_State \*L, int index);

Returns 1 when the value of the given index is a user data (whether full or lightweight), otherwise 0 。

* * *

#### `lua_isyieldable`

\[-0, +0, –\]

int lua\_isyieldable (lua\_State \*L);

If the given coroutine can be conceded, return 1, otherwise return 0 。

* * *

#### `lua_KContext`

typedef ... lua\_KContext;

The type of the continuation function context parameter. This must be a numeric type. When there is a intptr_t, it is defined as a intptr_t, so it can also hold a pointer. Otherwise, it is defined `ptrdiff_t`。

* * *

#### `lua_KFunction`

typedef int (\*lua\_KFunction) (lua\_State \*L, int status, lua\_KContext ctx);

types of continuation functions (cf. [§4.7](#4.7) ）。

* * *

#### `lua_len`

\[-0, +1, _e_\]

void lua\_len (lua\_State \*L, int index);

Returns the length of the value at the given index. It is equivalent to the ``#`` operator in Lua (see [§ 3.4.7](#3.4.7)). It is possible to trigger the meta-method corresponding to the "length" event (see [§ 2.4](#2.4) ). The result is stacked.

* * *

#### `lua_load`

\[-0, +1, –\]

int lua\_load (lua\_State \*L,
              lua\_Reader reader,
              void \*data,
              const char \*chunkname,
              const char \*mode);

Loads a block of Lua code, but does not run it. If there are no errors, lua_load push a compiled block of code to the top of the stack as a Lua function. Otherwise, push the error message.

`lua_load` The return value of can be：

*   **[`LUA_OK`](#pdf-LUA_OK):** No error；
*   **`LUA_ERRSYNTAX`:** Syntax errors encountered during precompilation；
*   **[`LUA_ERRMEM`](#pdf-LUA_ERRMEM):** Memory allocation error；
*   **[`LUA_ERRGCMM`](#pdf-LUA_ERRGCMM):** An error occurred while running the `__gc` meta-method. (This error has nothing to do with the code block loading process, it is caused by the garbage collector.。）

`lua_load` function uses a user-supplied `reader` function to read a block of code (see [lua_Reader](#lua_Reader) ). The `data` parameter is passed into the `reader` function.

`chunkname` This parameter can be given a name to the code block, which is used for error information and debugging information (see. [§4.9](#4.9)）。

`lua_load` It will automatically detect whether the code block is text or binary, and then do the corresponding loading operation (see program `luac`). The string `mode` is the same as the function [`load`](#pdf-load). It can also be `NULL` equivalent to a string "`bt`"。

`lua_load` The stack is used internally, so the reader function must always leave the stack as it is each time it returns.

If the returned function has an upper value, the first upper value is set to the global context stored at the LUA_RIDX_GLOBALS index in the registry (see [§ 4.5](#4.5)). When the main code block is loaded, this upper value is the `_ENV` variable (see [§ 2.2](#2.2)). All other upper values are initialized **nil**。

* * *

#### `lua_newstate`

\[-0, +0, –\]

lua\_State \*lua\_newstate (lua\_Alloc f, void \*ud);

Create a thread that runs in a new independent state machine. Returns `NULL` if the thread or state machine cannot be created (due to limited memory) `. Parameter `f` is an allocator function; Lua will use this function to do all memory allocation operations within the state machine. The second parameter `ud`, this pointer will be transferred in each call to the allocator.

* * *

#### `lua_newtable`

\[-0, +1, _e_\]

void lua\_newtable (lua\_State \*L);

Create an empty table and stack it. It is equivalent `lua_createtable(L, 0, 0)` 。

* * *

#### `lua_newthread`

\[-0, +1, _e_\]

lua\_State \*lua\_newthread (lua\_State \*L);

Creates a new thread, pushes it on the stack, and returns the [`lua_State`](#lua_State) pointer that maintains this thread. The new thread returned by this function shares the global context of the original thread, but it has a separate run stack.

There is no explicit function to close or destroy a thread. A thread is one of the garbage collected items like any other Lua object.

* * *

#### `lua_newuserdata`

\[-0, +1, _e_\]

void \*lua\_newuserdata (lua\_State \*L, size\_t size);

This function allocates a memory block of the specified size, uses the memory block address as a full user data stack, and returns this address. The host program is free to use this memory.

* * *

#### `lua_next`

\[-1, +(2|0), _e_\]

int lua\_next (lua\_State \*L, int index);

Pop a key from the top of the stack, and then push a key-value pair in the table specified by the index (the "next" pair after the popped key). If there are no more elements in the table, then [lua_next](#lua_next) will return 0 (nothing is pushed on the stack）。

The typical traversal method is like this：

     /\*  Table placed at index't' \*/
     lua\_pushnil(L);  /\* First Key \*/
     while (lua\_next(L, t) != 0) {
       /\* Use 'key' (at index -2) and 'value' (at index -1）\*/
       printf("%s - %s\\n",
              lua\_typename(L, lua\_type(L, -2)),
              lua\_typename(L, lua\_type(L, -1)));
       /\* Remove 'value'; keep 'key' for next iteration \*/
       lua\_pop(L, 1);
     }

When traversing a table, do not call [lua_tolstring](#lua_tolstring) directly on the key, unless you know that the key must be a string. The call to [lua_tolstring](#lua_tolstring) has the potential to change the value of a given index position; this will affect the next call to [lua_next](#lua_next).

See the [`next`](#pdf-next) function for a note on modifying the table being iterated during the iteration.

* * *

#### `lua_Number`

typedef double lua\_Number;

Lua The type of the floating-point number in.

Lua The type of the number in. The default is double, but you can change it to float. (See in `luaconf.h` `LUA_REAL` 。）

* * *

#### `lua_numbertointeger`

int lua\_numbertointeger (lua\_Number n, lua\_Integer \*p);

Converts a Lua floating-point number to a Lua integer. This macro assumes that `n` has a corresponding integer value. If the value is in the range of Lua integer representation, it is converted to an integer and assigned to `* p`. The result of the macro is a Boolean quantity that indicates whether the conversion was successful. (Note that due to rounding, this range test is difficult to do correctly without this macro。）

The macro may take multiple values for its parameters.

* * *

#### `lua_pcall`

\[-(nargs + 1), +(nresults|1), –\]

int lua\_pcall (lua\_State \*L, int nargs, int nresults, int msgh);

Call a function in protected mode.

`nargs` and `nresults` have the same meaning as in [lua_call](#lua_call). If no error occurs during the call, [lua_pcall](#lua_pcall) behaves exactly the same as [lua_call](#lua_call). However, if an error occurs, [lua_pcall](#lua_pcall) will catch it, then push the unique value (error message) on the stack, and return the error code. Like [lua_call](#lua_call), [lua_pcall](#lua_pcall) always removes the function itself and its arguments from the stack.

If `msgh` is 0, the error message returned at the top of the stack is exactly the same as the original error message. Otherwise, `msgh` is treated as the index position of the_error handler_on the stack. (In the current implementation, this index cannot be a pseudo-index.) When a runtime error occurs, this function is called and the parameter is the error message. The return value of the error handling function will be returned on the stack as an error message by [lua_pcall](#lua_pcall).

In typical usage, error handling functions are used to add more debugging information, such as stack trace information, to the error message. After [lua_pcall](#lua_pcall) returns, this information cannot be collected because the stack is already expanded.

[`lua_pcall`](#lua_pcall) function returns one of the following constants (defined in `lua.h`)：

*   **`LUA_OK` (0):** Success.
*   **`LUA_ERRRUN`:** Runtime error.
*   **`LUA_ERRMEM`:** Memory allocation error. For such errors, Lua does not call the error handling function.
*   **`LUA_ERRERR`:** The error that occurred when the error handling function was run.
*   **`LUA_ERRGCMM`:** Error occurred while running the `__gc` meta-method. (This error has nothing to do with the function being called.。）

* * *

#### `lua_pcallk`

\[-(nargs + 1), +(nresults|1), –\]

int lua\_pcallk (lua\_State \*L,
                int nargs,
                int nresults,
                int msgh,
                lua\_KContext ctx,
                lua\_KFunction k);

The behavior of this function is exactly the same as [lua_pcall](#lua_pcall), except that it also allows the called function to give up (cf. [§4.7](#4.7)）。

* * *

#### `lua_pop`

\[-n, +0, –\]

void lua\_pop (lua\_State \*L, int n);

Pop `n` elements from the stack.

* * *

#### `lua_pushboolean`

\[-0, +1, –\]

void lua\_pushboolean (lua\_State \*L, int b);

Put `B` as a Boolean quantity on the stack.

* * *

#### `lua_pushcclosure`

\[-n, +1, _e_\]

void lua\_pushcclosure (lua\_State \*L, lua\_CFunction fn, int n);

Put a new C closure on the stack.

When you create a C function, you can associate some values with it, which is creating a C closure (see [§ 4.4](#4.4)); these values can then be accessed by the function whenever the function is called. In order to associate some values to a C function, first these values need to be pushed onto the stack (if there are multiple values, the first one is pushed first). Next call [lua_pushcclosure](#lua_pushcclosure) to create the closure and push the C function onto the stack. The parameter `n` indicates how many values the function has to associate with the function. [lua_pushcclosure](#lua_pushcclosure) will also pop these values off the stack.

`n` The maximum value of is 255 。

When `n` is zero, this function creates a lightweight C function, which is a pointer to a C function. In this case, it is not possible to throw a memory error.

* * *

#### `lua_pushcfunction`

\[-0, +1, –\]

void lua\_pushcfunction (lua\_State \*L, lua\_CFunction f);

Stacks a C function. This function takes a C function pointer and stacks a Lua value of type `function. When this value at the top of the stack is called, the corresponding C function is triggered.

Any function registered to Lua must follow the correct protocol to receive parameters and return values (see [`lua_CFunction`](#lua_CFunction) ）。

`lua_pushcfunction` is present as a macro definition：

     #define lua\_pushcfunction(L,f)  lua\_pushcclosure(L,f,0)

* * *

#### `lua_pushfstring`

\[-0, +1, _e_\]

const char \*lua\_pushfstring (lua\_State \*L, const char \*fmt, ...);

Stacks a formatted string and returns a pointer to the string. It is similar to the C function `sprintf`, but with some important differences.：

*   You don't need to allocate space for the result: the result is a Lua string, and Lua cares about its memory allocation (while freeing up memory through garbage collection.）。
*   This conversion is very limited. Symbol, width, precision are not supported. The converter only supports ''%%'(insert a character'%'), '%s' (insert a string with zero terminator, no length limit), '%f' (insert a [' lua_Number](#lua_Number)), '%I' (insert a ['lua_Integer](#lua_Integer)), '%p' (insert a pointer or a hexadecimal number), ''%d'' (inserts an 'int'), ''%c'' (inserts a single-byte character denoted by 'int'), and ''%U'' (inserts a UTF-8 word denoted by 'long int'）。

* * *

#### `lua_pushglobaltable`

\[-0, +1, –\]

void lua\_pushglobaltable (lua\_State \*L);

Stacks the global environment.

* * *

#### `lua_pushinteger`

\[-0, +1, –\]

void lua\_pushinteger (lua\_State \*L, lua\_Integer n);

Stacks an integer with the value `n.

* * *

#### `lua_pushlightuserdata`

\[-0, +1, –\]

void lua\_pushlightuserdata (lua\_State \*L, void \*p);

Stacks a lightweight user data.

The user data is the C value that is kept in Lua. _lightweight user data_represents a pointer `void * `. It is a value like a number: you don`t need to create it specifically, it doesn`t have a separate meta table, and it won`t be collected (because it never needs to be created). As long as the C addresses represented are the same, the two lightweight user data are equal.

* * *

#### `lua_pushliteral`

\[-0, +1, _e_\]

const char \*lua\_pushliteral (lua\_State \*L, const char \*s);

This macro is equivalent to [lua_pushstring](#lua_pushstring), except that it can only be used if `s` is a literal. It will automatically give the length of the string.

* * *

#### `lua_pushlstring`

\[-0, +1, _e_\]

const char \*lua\_pushlstring (lua\_State \*L, const char \*s, size\_t len);

Stacks the string of length `len` pointed to by pointer`s. Lua makes an internal copy of this string (or reuses a copy), so the memory at `s` can be released or reused immediately after the function returns. Within the string can be any binary data, including zero characters.

Returns a pointer to the internal copy.

* * *

#### `lua_pushnil`

\[-0, +1, –\]

void lua\_pushnil (lua\_State \*L);

Stacks a null value.

* * *

#### `lua_pushnumber`

\[-0, +1, –\]

void lua\_pushnumber (lua\_State \*L, lua\_Number n);

Stacks a floating point number with a value of `n.

* * *

#### `lua_pushstring`

\[-0, +1, _e_\]

const char \*lua\_pushstring (lua\_State \*L, const char \*s);

Stacks the zero-terminated string pointed to by pointer s. Lua makes an internal copy of this string (or reuses a copy), so the memory at `s` can be released or reused immediately after the function returns.

Returns a pointer to the internal copy.

If `s` is `NULL`, **nil** is pushed and returned `NULL`。

* * *

#### `lua_pushthread`

\[-0, +1, –\]

int lua\_pushthread (lua\_State \*L);

Stacks the thread represented by `L. If this thread is the main thread of the current state machine, return 1 。

* * *

#### `lua_pushvalue`

\[-0, +1, –\]

void lua\_pushvalue (lua\_State \*L, int index);

Stacks a copy of the element at a given index on the stack.

* * *

#### `lua_pushvfstring`

\[-0, +1, _e_\]

const char \*lua\_pushvfstring (lua\_State \*L,
                              const char \*fmt,
                              va\_list argp);

Equivalent to [`lua_pushfstring`](#lua_pushfstring), but with `va_list` instead of a variable number of actual parameters.

* * *

#### `lua_rawequal`

\[-0, +0, –\]

int lua\_rawequal (lua\_State \*L, int index1, int index2);

Returns 1 if index `index1` is equal to the value at index `index2` itself (I. e. no meta method is called). Otherwise it returns 0. Also returns when either index is invalid 0 。

* * *

#### `lua_rawget`

\[-1, +1, –\]

int lua\_rawget (lua\_State \*L, int index);

Similar to [`lua_gettable`](#lua_gettable), but makes a direct access (does not trigger the meta method）。

* * *

#### `lua_rawgeti`

\[-0, +1, –\]

int lua\_rawgeti (lua\_State \*L, int index, lua\_Integer n);

Stacks the value of`t[n]`, where `t` refers to the table at the given index. This is a direct access; that is, it does not trigger the meta method.

Returns the type of the push value.

* * *

#### `lua_rawgetp`

\[-0, +1, –\]

int lua\_rawgetp (lua\_State \*L, int index, const void \*p);

Stack the value of`t[k]`, where `t` refers to the table at a given index, and `k` is the lightweight user data corresponding to the pointer `p. This is a direct access; that is, it does not trigger the meta method.

Returns the type of the push value.

* * *

#### `lua_rawlen`

\[-0, +0, –\]

size\_t lua\_rawlen (lua\_State \*L, int index);

Returns the inherent "length" of the value at the given index: for strings, it refers to the length of the string; for tables, it refers to the value that the take length operation (''#'') should get without triggering the meta-method; for user data, it refers to the size of the memory block allocated for that user data; for other values, it is 0 。

* * *

#### `lua_rawset`

\[-2, +0, _e_\]

void lua\_rawset (lua\_State \*L, int index);

Similar to [`lua_settable](#lua_settable), but does a direct assignment (does not trigger the meta method）。

* * *

#### `lua_rawseti`

\[-1, +0, _e_\]

void lua\_rawseti (lua\_State \*L, int index, lua\_Integer i);

Equivalent to`t [I] = v`, where `t` refers to the table at the given index and `v` is the value at the top of the stack.

This function will pop the value off the stack. The assignment is straightforward; that is, the meta-method is not triggered.

* * *

#### `lua_rawsetp`

\[-1, +0, _e_\]

void lua\_rawsetp (lua\_State \*L, int index, const void \*p);

Equivalent to`t[k] = v`, where `t` refers to the table at the given index and `k` is the lightweight user data corresponding to pointer `p. And `v` is the value at the top of the stack.

This function will pop the value off the stack. The assignment is straightforward; that is, the meta-method is not triggered.

* * *

#### `lua_Reader`

typedef const char \* (\*lua\_Reader) (lua\_State \*L,
                                    void \*data,
                                    size\_t \*size);

[`lua_load`](#lua_load) The reader function used, every time it needs a new block of code, [lua_load](#lua_load) calls the reader, passing in a parameter `data` each time `. The reader needs to return a pointer to a piece of memory containing the new code block and set `size` to the size of that piece of memory. The memory block must exist until the next function is called. The reader may indicate the end of the code block by returning `NULL` or setting `size` to 0. The reader may return multiple blocks, each of which may have an arbitrary size greater than zero.

* * *

#### `lua_register`

\[-0, +0, _e_\]

void lua\_register (lua\_State \*L, const char \*name, lua\_CFunction f);

Set C function `f` to global variable `name. It is defined by a macro：

     #define lua\_register(L,n,f) \\
            (lua\_pushcfunction(L, f), lua\_setglobal(L, n))

* * *

#### `lua_remove`

\[-1, +0, –\]

void lua\_remove (lua\_State \*L, int index);

Removes an element from a given valid index and removes all elements above that index to fill the gap. This function cannot be called with a pseudo index, because the pseudo index does not point to a real location on the stack.

* * *

#### `lua_replace`

\[-1, +0, –\]

void lua\_replace (lua\_State \*L, int index);

The top-of-stack element is placed at a given position without moving other elements (thus overwriting the value at that position), and then the top-of-stack element is popped.

* * *

#### `lua_resume`

\[-?, +?, –\]

int lua\_resume (lua\_State \*L, lua\_State \*from, int nargs);

Start or continue a coroutation in a given thread 。

To start a coroutine, you need to push the main function and its required parameters into the thread stack. Then call [`lua_resume`](#lua_resume) and set `nargs` to the number of parameters. This call will return when the coroutine is suspended or when it finishes running. When the function returns, there will be all the values passed to [lua_yield](#lua_yield) on the stack, or all the return values of the main function. When the coroutine gives up, [`lua_resume`](#lua_resume) returns [`LUA_YIELD`](#pdf-LUA_YIELD), and if the coroutine ends running without any errors, it returns 0. Returns an error code if there is an error (see [`lua_pcall`](#lua_pcall) ）。

In the event of an error, the stack is not unrolled, so you can use the debug API to handle it. The error message is placed at the top of the stack.

To continue a coroutine, you need to clear all the results left over from the last [`lua_yield`](#lua_yield), you stack the value that needs to be passed to` yield` as the result, and then call [`lua_resume`](#lua_resume) 。

The parameter `from` indicates from which the coroutation continues `L. If such a coroutage does not exist, this parameter can be `NULL` 。

* * *

#### `lua_rotate`

\[-0, +0, –\]

void lua\_rotate (lua\_State \*L, int idx, int n);

Rotate the elements starting from `idx` to the top of the stack by `n` positions. When `n` is a positive number, the rotation direction is to the top of the stack. When `n` is negative, it rotates `-n` positions to the bottom of the stack. The absolute value of `n` cannot be greater than the slice length of the rotation.

* * *

#### `lua_setallocf`

\[-0, +0, –\]

void lua\_setallocf (lua\_State \*L, lua\_Alloc f, void \*ud);

Replace the allocator function of the specified state machine with the one with the user data` ud` `f` 。

* * *

#### `lua_setfield`

\[-1, +0, _e_\]

void lua\_setfield (lua\_State \*L, int index, const char \*k);

Do an operation equivalent to`t[k] = v`, where `t` is the value at the given index and `v` is the value at the top of the stack.

This function will pop this value off the stack. As in Lua, this function may trigger a meta-method for a "newindex" event (see [§2.4](#2.4)）。

* * *

#### `lua_setglobal`

\[-1, +0, _e_\]

void lua\_setglobal (lua\_State \*L, const char \*name);

Pop a value off the stack and set it to the new value of the global variable `name.

* * *

#### `lua_seti`

\[-1, +0, _e_\]

void lua\_seti (lua\_State \*L, int index, lua\_Integer n);

Do an operation equivalent to`t[n] = v`, where `t` is the value at the given index and `v` is the value at the top of the stack.

This function will pop this value off the stack. As in Lua, this function may trigger a meta-method for a "newindex" event (see [§2.4](#2.4)）。

* * *

#### `lua_setmetatable`

\[-1, +0, –\]

void lua\_setmetatable (lua\_State \*L, int index);

Pop a table off the stack and make it the meta table for the value at the given index.

* * *

#### `lua_settable`

\[-2, +0, _e_\]

void lua\_settable (lua\_State \*L, int index);

Do an operation equivalent to`t[k] = v`, where `t` is the value at the given index, `v` is the value at the top of the stack, and `k` is the value below the top of the stack.

This function pops both the key and the value on the stack. As in Lua, this function may trigger a meta-method for a "newindex" event (see [§2.4](#2.4)）。

* * *

#### `lua_settop`

\[-?, +?, –\]

void lua\_settop (lua\_State \*L, int index);

The parameter allows any index to be passed in as well as 0. It will set the top of the stack to this index. If the new top of the stack is larger than the original, the excess new elements will be filled with **nil * *. If `index` is 0, all elements on the stack are removed.

* * *

#### `lua_setuservalue`

\[-1, +0, –\]

void lua\_setuservalue (lua\_State \*L, int index);

Pops a value from the stack and sets it as the associated value for the user data at the given index.

* * *

#### `lua_State`

typedef struct lua\_State lua\_State;

An opaque structure that points to a thread and indirectly (through that thread) references the state of the entire Lua interpreter. The Lua library is fully reentrant: it doesn't have any global variables. All information about the state machine can be accessed through this structure.

A pointer to this structure must be passed as the first argument to every library function. The exception is [lua_newstate](#lua_newstate), which creates a Lua state machine from scratch.

* * *

#### `lua_status`

\[-0, +0, –\]

int lua\_status (lua\_State \*L);

Returns the status of thread `L.

The normal thread state is 0 ([`LUA_OK `](#pdf-LUA_ OK)). The status value is the error code when the thread finishes execution with [lua_resume](#lua_resume) and throws an error. If the thread is suspended, the status is `LUA_YIELD` 。

You can only call a function on a thread whose status is [`LUA_OK `](#pdf-LUA_ OK). You can continue a thread with a status of [`LUA_OK `](#pdf-LUA_ OK) (for starting a new LUA_YIELD) or a thread with a status of [`LUA_OK`](#pdf-LUA_YIELD) (for continuing a coroutine）。

* * *

#### `lua_stringtonumber`

\[-0, +1, –\]

size\_t lua\_stringtonumber (lua\_State \*L, const char \*s);

Converts a zero-terminated string `s` to a number, stacks the number, and returns the total length of the string (I. e. length plus one). The result of the conversion may be an integer or a floating point number, depending on Lua`s conversion syntax (see [§ 3.1](#3.1)). This string can have leading and trailing spaces and symbols. If the string is not a valid number, returning 0 does not put anything on the stack. (Note that this result can be used as a Boolean quantity, and if it is true, the conversion is successful.。）

* * *

#### `lua_toboolean`

\[-0, +0, –\]

int lua\_toboolean (lua\_State \*L, int index);

Converts the Lua value at a given index to a boolean (0 or 1) in C. As with all tests in Lua, [lua_toboolean](#lua_toboolean) will return any values other than **false** and **nil** as true; otherwise, it will return false. (If you want to receive only real boolean values, you need to use [`lua_isboolean`](#lua_isboolean) to test the type of the value。）

* * *

#### `lua_tocfunction`

\[-0, +0, –\]

lua\_CFunction lua\_tocfunction (lua\_State \*L, int index);

Converts the Lua value at the given index to a C function. This value must be a C function; if not, it returns `NULL` 。

* * *

#### `lua_tointeger`

\[-0, +0, –\]

lua\_Integer lua\_tointeger (lua\_State \*L, int index);

is equivalent to calling [`lua_tointegerx](#lua_tointegerx) with parameter `isnum` `NULL`。

* * *

#### `lua_tointegerx`

\[-0, +0, –\]

lua\_Integer lua\_tointegerx (lua\_State \*L, int index, int \*isnum);

Converts the Lua value at the given index to the signed integer type [lua_Integer](#lua_Integer). The Lua value must be an integer, or a number or string that can be converted to an integer (see [§ 3.4.3](#3.4.3)); otherwise, lua_tointegerx returns 0 。

If `isnum` is not `NULL`, `* isnum` will be set to whether the operation was successful.

* * *

#### `lua_tolstring`

\[-0, +0, _e_\]

const char \*lua\_tolstring (lua\_State \*L, int index, size\_t \*len);

Converts the Lua value at the given index to a C string. If `len` is not `NULL`, it also sets the string length to `* len. The Lua value must be a string or a number; otherwise, `NULL` is returned `. If the value is a number, lua_tolstring will also convert the actual type of that value on the stack to a string_. (When traversing a table, if the lua_tolstring is applied to the key, this conversion may cause [`lua_next`](#lua_next) to be wrong。）

`lua_tolstring` Returns an aligned pointer to a string in the Lua state machine. This string always guarantees that the last character (required by C) is zero ('\0'), and it allows multiple such zeros to be contained within the string.

Because of the possibility of garbage collection in Lua, there is no guarantee that the pointer returned by the lua_tolstring will still be valid after the corresponding value is removed from the stack.

* * *

#### `lua_tonumber`

\[-0, +0, –\]

lua\_Number lua\_tonumber (lua\_State \*L, int index);

is equivalent to calling [`lua_tonumberx](#lua_tonumberx) with parameter `isnum` `NULL`。

* * *

#### `lua_tonumberx`

\[-0, +0, –\]

lua\_Number lua\_tonumberx (lua\_State \*L, int index, int \*isnum);

Converts the Lua value at the given index to a C type [lua_Number](#lua_Number) (see Lua \_Number). This Lua value must be a number or a string that can be converted to a number (see [§ 3.4.3](#3.4.3)); otherwise, [lua_tonumberx](#lua_tonumberx) returns 0 。

If `isnum` is not `NULL`, `* isnum` will be set to whether the operation was successful.

* * *

#### `lua_topointer`

\[-0, +0, –\]

const void \*lua\_topointer (lua\_State \*L, int index);

Converts the value at the given index to a generic C pointer (`void * `). This value can be a user object, a table, a thread, or a function; otherwise, lua_topointer returns NULL `. Different objects have different pointers. There is no way to return a pointer to the original type.

This function is usually only used for debugging information.

* * *

#### `lua_tostring`

\[-0, +0, _e_\]

const char \*lua\_tostring (lua\_State \*L, int index);

Equivalent to calling [lua_tolstring](#lua_tolstring), whose argument `len` is `NULL` 。

* * *

#### `lua_tothread`

\[-0, +0, –\]

lua\_State \*lua\_tothread (lua\_State \*L, int index);

Converts the value at the given index to a Lua thread (denoted as `lua_State * `). This value must be a thread; otherwise the function returns `NULL`。

* * *

#### `lua_touserdata`

\[-0, +0, –\]

void \*lua\_touserdata (lua\_State \*L, int index);

If the value at the given index is a full user data, the function returns the address of its memory block. If the value is a lightweight user data, the pointer it represents is returned. Otherwise, return `NULL` 。

* * *

#### `lua_type`

\[-0, +0, –\]

int lua\_type (lua\_State \*L, int index);

Returns the type of the value at the given valid index, or LUA_TNONE when the index is invalid (or inaccessible) `. [`lua_type](#lua_type) The returned type is encoded as some constant defined in `lua.h`： `LUA_TNIL`， `LUA_TNUMBER`， `LUA_TBOOLEAN`， `LUA_TSTRING`， `LUA_TTABLE`， `LUA_TFUNCTION`， `LUA_TUSERDATA`， `LUA_TTHREAD`， `LUA_TLIGHTUSERDATA`。

* * *

#### `lua_typename`

\[-0, +0, –\]

const char \*lua\_typename (lua\_State \*L, int tp);

Returns the name of the type represented by `tp`, which must be one of the possible values returned by [lua_type](#lua_type).

* * *

#### `lua_Unsigned`

typedef ... lua\_Unsigned;

[`lua_Integer`](#lua_Integer) The unsigned version.

* * *

#### `lua_upvalueindex`

\[-0, +0, –\]

int lua\_upvalueindex (int i);

Returns the pseudo-index of the `I` top value of the currently running function (see [§ 4.4](#4.4)).

* * *

#### `lua_version`

\[-0, +0, _v_\]

const lua\_Number \*lua\_version (lua\_State \*L);

Returns the address of the version number stored in the Lua kernel. When called, a legal [`lua_State`](#lua_State) is passed in, which returns the address of the version when the state machine was created. If called with `NULL`, return the caller`s version address.

* * *

#### `lua_Writer`

typedef int (\*lua\_Writer) (lua\_State \*L,
                           const void\* p,
                           size\_t sz,
                           void\* ud);

Writer function used by [lua_dump](#lua_dump). Each time [lua_dump](#lua_dump) spawns a new block of code, it calls the writer. Pass in the buffer to be written (`p`) and its size (`sz`), and the parameters passed to [lua_dump](#lua_dump) `data` 。

The writer returns an error code: 0 indicates no error; any other value indicates an error and causes [lua_dump](#lua_dump) to stop calling the writer again.

* * *

#### `lua_xmove`

\[-?, +?, –\]

void lua\_xmove (lua\_State \*from, lua\_State \*to, int n);

Swap values in different threads under the same state machine.

This function pops `n` values from the `from` stack and pushes them onto the `to` stack.

* * *

#### `lua_yield`

\[-?, +?, _e_\]

int lua\_yield (lua\_State \*L, int nresults);

This function is equivalent to calling [lua_yieldk](#lua_yieldk), except that the continuation function is not provided (see [§ 4.7](#4.7)). Therefore, when the thread is extended, the thread will continue to run the function that called the lua_yield function.

* * *

#### `lua_yieldk`

\[-?, +?, _e_\]

int lua\_yieldk (lua\_State \*L,
                int nresults,
                lua\_KContext ctx,
                lua\_KFunction k);

give up the coroutine (thread）。

When the C function calls [`lua_yieldk`](#lua_yieldk), the currently running coroutine will be suspended, and the [`lua_resume`](#lua_resume) call that started this thread will return. The parameter `nresults` is the number of values returned to [lua_resume](#lua_resume) on the stack.

When the coroutine is continued again, Lua calls the continuation function `k` to continue running the suspended C function (see [§ 4.7](#4.7)). The continuation function receives the same stack from the previous function, and the `n` return values in the stack are removed and the parameters passed in from [lua_resume](#lua_resume) are pushed. In addition, the continuation function receives an argument passed to [lua_yieldk](#lua_yieldk) `ctx`。

Normally, this function does not return; as the coroutine continues over and over again, it continues from the continuation function. However, there is one exception: when the function is called from a hook function that runs line by line (see [§ 4.9](#4.9)), the lua_yieldk cannot provide a continuation function. (that is, a form similar to [`lua_yield`](#lua_yield)), and in this case, the hook function will return immediately after calling out. Lua will make the coroutine give up. Once the coroutine is continued again, the function that triggered the hook will continue to run normally.

When a thread is in a C call that does not provide a continuation function, calling it will throw an error. Calling it from a thread that is not started with a continuation (for example, the main thread) will do the same.

### 4.9 – Debug Interface

Lua There is no built-in debugging mechanism. But it provides a special set of function interfaces and_hooks_. This set of interfaces can be used to build different debuggers, performance profilers, or other tools that need to get "inside information" from the interpreter.

* * *

#### `lua_Debug`

typedef struct lua\_Debug {
  int event;
  const char \*name;           /\* (n) \*/
  const char \*namewhat;       /\* (n) \*/
  const char \*what;           /\* (S) \*/
  const char \*source;         /\* (S) \*/
  int currentline;            /\* (l) \*/
  int linedefined;            /\* (S) \*/
  int lastlinedefined;        /\* (S) \*/
  unsigned char nups;         /\* (u) Number of upper values \*/
  unsigned char nparams;      /\* (u) Number of parameters \*/
  char isvararg;              /\* (u) \*/
  char istailcall;            /\* (t) \*/
  char short\_src\[LUA\_IDSIZE\]; /\* (S) \*/
  /\* Private part \*/
  _ Other Domains _
} lua\_Debug;

This is a structure that carries various information about a function or activity record. [`lua_getstack](#lua_getstack) will only populate the private part of the structure for later use. Calling [lua_getinfo](#lua_getinfo) fills in [lua_Debug](#lua_Debug) those information fields that can be used.

The following is a description of each field of [lua_Debug](#lua_Debug)：

*   **`source`:** The name of the block of code that created the function. If 'source' is preceded by ''@'', it means that the function is defined in a file, and the part after ''@'' is the file name. If 'source' is first with ''='', the rest is determined by user behavior how to represent the source code. In other cases, the function is defined in a string, and 'source' is exactly that string.
*   **`short_src`:** A "printable version" of `source` for error messages.
*   **`linedefined`:** The line number at which the function definition begins.
*   **`lastlinedefined`:** The line number at the end of the function definition.
*   **`what`:** A string `"Lua"` if the function is a Lua function, `"C"` if it is a C function, or if it is the body part of a code block `"main"`。
*   **`currentline`:** The line on which the given function is executing. When line number information is not available, `currentline` is set -1 。
*   **`name`:** Given a reasonable name for the function. Because functions in Lua are first-class citizens, they have no fixed names: some functions may be the values of global compound variables, others may simply be stored in a field in a table. The lua_getinfo function examines how the function was called to find an appropriate name. If it cannot find a name, `name` is set `NULL` 。
*   **`namewhat`:** Used to interpret the `name` domain. The value of `namewhat` can be `"global"`, `"local"`, `"method"`, `"field"`, `"upvalue"`, or `""` (empty string). It depends on how the function is called. (Lua uses an empty string to indicate that other options do not match。）
*   **`istailcall`:** This value is true if the function is called as a tail call. In this case, when the layer's caller is not in the stack.
*   **`nups`:** The number of upper values of the function.
*   **`nparams`:** The number of function fixed parameters (for C functions will always be 0 ）。
*   **`isvararg`:** True if the function is a variable parameter function (always true for C functions）。

* * *

#### `lua_gethook`

\[-0, +0, –\]

lua\_Hook lua\_gethook (lua\_State \*L);

Returns the current hook function.

* * *

#### `lua_gethookcount`

\[-0, +0, –\]

int lua\_gethookcount (lua\_State \*L);

Returns the current hook count.

* * *

#### `lua_gethookmask`

\[-0, +0, –\]

int lua\_gethookmask (lua\_State \*L);

Returns the current hook mask.

* * *

#### `lua_getinfo`

\[-(0|1), +(0|1|2), _e_\]

int lua\_getinfo (lua\_State \*L, const char \*what, lua\_Debug \*ar);

Returns information about a specified function or function call.

When used to get information about a function call, the parameter `ar` must be a valid active record. This record can be the result of a previous call to [lua_getstack](#lua_getstack), or the result of a hook (see [lua_Hook](#lua_Hook) ).

When used to obtain information about a function, you can push the function onto the stack and then start the `what` string with the character ``>. (This will cause the lua_getinfo to pop the function from the top of the stack.) For example, to know on which line the function `f` is defined, you can use the following code：

     lua\_Debug ar;
     lua\_getglobal(L, "f");  /\* Get global variables 'f' \*/
     lua\_getinfo(L, ">S", &ar);
     printf("%d\\n", ar.linedefined);

`what` Each character in the string filters out some fields in the structure`ar` structure to fill in, or to push a value onto the stack.：

*   **'`n`':** Populating the `name` and `namework` fields；
*   **'`S`':** Fill the fields `source`, `linedefined`, `lastlinedefined`, and `what` with short_src；
*   **'`l`':** Populating the `currentline` field；
*   **'`t`':** Populating the `istailcall` domain；
*   **'`u`':** Populate the `nups`, `nparams`, and `isvararg` fields；
*   **'`f`':** Stacks the function at the specified level in the running.；
*   **'`L`':** A table is stacked, and the integer index in this table is used to describe which rows in the function are valid rows. (_valid line_refers to the line with the actual code, that is, the line where you can put the breakpoint. Invalid lines include blank lines and lines with comments only。）

    If this option is used at the same time as option ''f'', the table is stacked after the function.


This function error will return 0 (for example, there is an invalid option in `what`）。

* * *

#### `lua_getlocal`

\[-0, +(0|1), –\]

const char \*lua\_getlocal (lua\_State \*L, const lua\_Debug \*ar, int n);

Gets information about a local variable from a given activity record or from a function.

For the first case, the parameter `ar` must be a valid active record. This record can be the result of a previous call to [lua_getstack](#lua_getstack) or an argument to a hook (see [lua_Hook](#lua_Hook) ). The index `n` is used to select which local variable to review; see [`debug.getlocal`](#pdf-debug.getlocal) for the description of variable indexes and names.

[`lua_getlocal`](#lua_getlocal) Stacks the value of a variable and returns its name.

For the second case, `ar` must be filled with `NULL `. Functions that need to be probed must be placed at the top of the stack. In this case, only the parameters of the Lua function are visible (there is no information about which other active variables are available) and no values are stacked.

When the index is greater than the number of active local variables, return `NULL` (no stack）

* * *

#### `lua_getstack`

\[-0, +0, –\]

int lua\_getstack (lua\_State \*L, int level, lua\_Debug \*ar);

Gets information about the interpreter's runtime stack.

This function fills in part of the [lua_Debug](#lua_Debug) structure with the_activity record_of the function at the specified level that is running. Layer 0 represents the currently running function, and the function on the_n 1_layer is the one that calls the function on the_n_layer (except for the tail call, which is not included in the stack level). If there are no errors, [lua_getstack](#lua_getstack) returns 1; when the level passed in by the call is greater than the stack depth, it returns 0 。

* * *

#### `lua_getupvalue`

\[-0, +(0|1), –\]

const char \*lua\_getupvalue (lua\_State \*L, int funcindex, int n);

Gets information about the upper value of a closure. (For Lua functions, the upper values are external local variables that the function needs to use, so these variables are included in the closure.) [`lua_getupvalue`](#lua_getupvalue) gets the `n` top value, stacks the value of this top value, and returns its name. The `funcindex` points to the location of the closure on the stack. (Because the upper values are valid throughout the function, they have no particular order. Therefore, they are numbered in alphabetical order。）

Returns `NULL` when the index number is greater than the number of values above (and does not push anything). For C functions, the names of all upper values are empty strings. `""`。

* * *

#### `lua_Hook`

typedef void (\*lua\_Hook) (lua\_State \*L, lua\_Debug \*ar);

The type of hook function used for debugging.

Whenever the hook is called, the `event` field in its parameter `ar` is set to the event that fires the hook. Lua defines these events as the following constants: LUA_HOOKCALL, LUA_HOOKRET, LUA_HOOKTAILCALL, LUA_HOOKLINE, LUA_HOOKCOUNT `. In addition, for the line event, the `currentline` field is also set. To get other fields in `ar`, the hook must call [`lua_getinfo`](#lua_getinfo) 。

For the call event, `event` can be the usual value for `LUA_HOOKCALL, `or LUA_HOOKTAILCALL for tail call; in the latter case, there is no corresponding return event.

When Lua runs inside a hook, it masks other calls to the hook. In other words, if a hook function is called back to Lua to execute a function or a block of code, the execution operation will not trigger any hooks.

A hook function cannot have a continuation point, that is, it cannot call [`lua_yieldk`](#lua_yieldk), [`lua_pcallk`](#lua_pcallk) with a non-empty `k`, or [`lua_callk`](#lua_callk)。

The hook function can give up when the following conditions are met: only row count events can give up, and no value can be passed out when giving up; To give up from the hook, [`lua_yield](#lua_yield) must be used to end the operation of the hook, and` nresults` must be zero.

* * *

#### `lua_sethook`

\[-0, +0, –\]

void lua\_sethook (lua\_State \*L, lua\_Hook f, int mask, int count);

Set up a hook function for debugging.

The parameter `f` is a hook function. `mask` specifies which events are called upon: it consists of the following set of bit constants LUA_MASKCALL, LUA_MASKRET, LUA_MASKLINE, LUA_MASKCOUNT `. The parameter `count` is meaningful only if it contains a LUA_MASKCOUNT in the mask. For each event, the case where the hook is called is explained as follows：

*   **call hook:** Called when the interpreter calls a function. The hook will be called after Lua enters a new function and before the function takes parameters.
*   **return hook:** Called when the interpreter returns from a function. The hook will be called at the moment before Lua leaves the function. There is no standard way to access the values returned by the function.
*   **line hook:** Called when the interpreter is ready to start executing a new line of code, or when jumping into this line of code (even if jumping within the same line). (This event only occurs when Lua executes a Lua function。）
*   **count hook:** Called after the interpreter executes every `count` instruction. (This event only occurs when Lua executes a Lua function。）

Hooks can be masked by setting `mask` to zero.

* * *

#### `lua_setlocal`

\[-(0|1), +0, –\]

const char \*lua\_setlocal (lua\_State \*L, const lua\_Debug \*ar, int n);

Sets the value of a local variable in a given active record. The parameter `ar` is the same as in `n` and [lua_getlocal](#lua_getlocal) (see [lua_getlocal](#lua_getlocal) ). [`lua_setlocal`](#lua_setlocal) Assign the value of the top of the stack to the variable and return the name of the variable. It will pop the value from the top of the stack.

When the index is greater than the number of active local variables, return `NULL` (nothing pops up.）。

* * *

#### `lua_setupvalue`

\[-(0|1), +0, –\]

const char \*lua\_setupvalue (lua\_State \*L, int funcindex, int n);

Sets the value of the value on the closure. It pops the value at the top of the stack and assigns it to the upper value and returns the name of the upper value. The argument `funcindex` is the same as in `n` and [lua_getupvalue](#lua_getupvalue) (cf. [`lua_getupvalue`](#lua_getupvalue) ）。

When the index is greater than the number of upper values, return `NULL` (nothing pops up）。

* * *

#### `lua_upvalueid`

\[-0, +0, –\]

void \*lua\_upvalueid (lua\_State \*L, int funcindex, int n);

Returns a unique identifier for the upper value numbered `n` in the closure at index `funcindex. The parameter `funcindex` is the same as in `n` and [lua_getupvalue](#lua_getupvalue) (see [lua_getupvalue](#lua_getupvalue) ). (But `n` cannot be greater than the number of upper values.）。

These unique identifiers can be used to detect whether different closures share the same upper value. Lua closures that share the same upper value (that is, they refer to the same external local variable) will return the same identity for this upper value.

* * *

#### `lua_upvaluejoin`

\[-0, +0, –\]

void lua\_upvaluejoin (lua\_State \*L, int funcindex1, int n1,
                                    int funcindex2, int n2);

Let the `n1` upper value of the Lua closure at index `funcindex1` refer to the `n2` upper value of the Lua closure at index `funcindex2.

## 5 – auxiliary library

_ The auxiliary library_provides some convenient functions for programming Lua in C. The base API provides the main functions for C and Lua interaction, while the auxiliary libraries provide higher-order functions for some common tasks.

All functions and types in the auxiliary library are defined in the header file `lauxlib.h` with the prefix `luaL_`。

All functions in the auxiliary library are implemented based on the base API. Therefore, they do not provide any functions that the underlying API cannot implement. Nevertheless, using auxiliary libraries can make your code more robust.

Some helper library functions use some extra stack space internally. When auxiliary libraries use less than five stacks, they don't check the stack size; they simply assume that the stack is sufficient.

Functions in some auxiliary libraries are used to check the parameters of C functions. Because the error message is formatted to refer to the parameter (for example, "`bad argument #1 `"), you should not use these functions for values other than parameters.

If the check fails, the `luaL_check * `functions will definitely throw an error.

### 5.1 – Functions and Types

Here we have listed all the functions and types in the auxiliary library in alphabetized order.

* * *

#### `luaL_addchar`

\[-?, +?, _e_\]

void luaL\_addchar (luaL\_Buffer \*B, char c);

Add a byte to cache `B` (see [luaL_Buffer](#luaL_Buffer)) `c`。

* * *

#### `luaL_addlstring`

\[-?, +?, _e_\]

void luaL\_addlstring (luaL\_Buffer \*B, const char \*s, size\_t l);

Add a string `s` of length `l` to cache `B` (see [luaL_Buffer](#luaL_Buffer)) `. This string can contain zeros.

* * *

#### `luaL_addsize`

\[-?, +?, _e_\]

void luaL\_addsize (luaL\_Buffer \*B, size\_t n);

Add to cache `B` (see [luaL_Buffer](#luaL_Buffer)) a string of length `n` that was previously copied to buffer (see [luaL_prepbuffer](#luaL_prepbuffer)).

* * *

#### `luaL_addstring`

\[-?, +?, _e_\]

void luaL\_addstring (luaL\_Buffer \*B, const char \*s);

Add a zero-terminated string to cache `B` (see [luaL_Buffer](#luaL_Buffer)) `s`。

* * *

#### `luaL_addvalue`

\[-1, +?, _e_\]

void luaL\_addvalue (luaL\_Buffer \*B);

Add a value at the top of the stack to cache `B` (see [luaL_Buffer](#luaL_Buffer)) and then pop it.

This function is the only one that manipulates the string cache that will (and must) put extra elements on the stack. This element will be added to the cache.

* * *

#### `luaL_argcheck`

\[-0, +0, _v_\]

void luaL\_argcheck (lua\_State \*L,
                    int cond,
                    int arg,
                    const char \*extramsg);

Check if `cond` is true. If it is not true, an error is thrown in the form of standard information (see [`luaL_argerror`](#luaL_argerror)）。

* * *

#### `luaL_argerror`

\[-0, +0, _v_\]

int luaL\_argerror (lua\_State \*L, int arg, const char \*extramsg);

Throws an error reporting the problem with the `arg` parameter of the called C function. It uses the following standard information and includes an `extramsg` as an annotation：

     bad argument #_arg_ to '_funcname_' (_extramsg_)

This function never returns.

* * *

#### `luaL_Buffer`

typedef struct luaL\_Buffer luaL\_Buffer;

_ The type of string cache.

String caching allows C code to segment a Lua string. The use mode is as follows：

*   First define a variable of type [`luaL_Buffer`](#luaL_Buffer) `b`。
*   Call `luaL_buffinit(L, & B) `to initialize it.
*   Then call the `luaL_add *` set of functions to add string fragments to it.
*   Last call `luaL_pushresult(& B) `. This last call leaves the final string at the top of the stack.

If you know the length of the result string in advance, you can use the cache like this.：

*   First define a variable of type [`luaL_Buffer`](#luaL_Buffer) `b`。
*   Then call `luaL_buffinitsize(L, & B, sz)`to pre-allocate a space of size `sz.
*   Then copy the string into this space.
*   Finally, call `luaL_pushresultsize(& B, sz)`, where `sz` refers to the length of the string that has been copied into the cache.

In general operation, the string cache will use an inconstant number of stack slots. Therefore, in using caching, you cannot assume where the top of the stack is currently. You can use the stack between function calls to the cache operation, just keep the stack balanced; that is, when you make a cache operation call, the stack position at that time is the same as the position after the last cache operation was called. (The only exception is for [luaL_addvalue](#luaL_addvalue).) After calling [`luaL_pushresult`](#luaL_pushresult), the stack is restored to the position where the cache was initialized and the final string is pushed at the top.

* * *

#### `luaL_buffinit`

\[-0, +0, –\]

void luaL\_buffinit (lua\_State \*L, luaL\_Buffer \*B);

Initialize cache `B`. This function does not allocate any space; the cache must be declared as a variable (cf. [`luaL_Buffer`](#luaL_Buffer)）。

* * *

#### `luaL_buffinitsize`

\[-?, +?, _e_\]

char \*luaL\_buffinitsize (lua\_State \*L, luaL\_Buffer \*B, size\_t sz);

Equivalent to call sequence [`luaL_buffinit`](#luaL_buffinit)， [`luaL_prepbuffsize`](#luaL_prepbuffsize)。

* * *

#### `luaL_callmeta`

\[-0, +(0|1), _e_\]

int luaL\_callmeta (lua\_State \*L, int obj, const char \*e);

Call a meta method.

If the object at index `obj` has a meta table and the meta table has field `e `. This function calls the field with the object as an argument. In this case, the function returns true and the call return value is pushed on the stack. If there is no meta table at that location, or no corresponding meta method, this function returns false (and does not stack anything）。

* * *

#### `luaL_checkany`

\[-0, +0, _v_\]

void luaL\_checkany (lua\_State \*L, int arg);

Checks if the function has parameters of any type (including **nil**) at the `arg` position.

* * *

#### `luaL_checkinteger`

\[-0, +0, _v_\]

lua\_Integer luaL\_checkinteger (lua\_State \*L, int arg);

Checks whether the `arg` parameter of the function is an integer (or can be converted to an integer) and returns the integer value as type [lua_Integer](#lua_Integer).

* * *

#### `luaL_checklstring`

\[-0, +0, _v_\]

const char \*luaL\_checklstring (lua\_State \*L, int arg, size\_t \*l);

Check whether the `arg` parameter of the function is a string and return the string; If `l` is not `NULL`, fill in the length of the string `*l`。

This function uses [lua_tolstring](#lua_tolstring) to get the result. So the function has the potential to cause conversions that are equally valid.

* * *

#### `luaL_checknumber`

\[-0, +0, _v_\]

lua\_Number luaL\_checknumber (lua\_State \*L, int arg);

Checks whether the `arg` parameter of the function is a number and returns this number.

* * *

#### `luaL_checkoption`

\[-0, +0, _v_\]

int luaL\_checkoption (lua\_State \*L,
                      int arg,
                      const char \*def,
                      const char \*const lst\[\]);

Check whether the `arg` parameter of the function is a string and look for this string in the array `lst` (e. g. a zero-terminated string array). Returns the index number of the matched string in the array. If the argument is not a string, or if the string does not match in the array, an error is thrown.

If `def` is not `NULL`, the function takes `def` as the default value. The default value takes effect when the parameter `arg` does not exist or the parameter is **nil.

This function is typically used to map strings to C enumerations. (Doing this conversion in the Lua library allows it to use strings instead of numbers to do some options.。）

* * *

#### `luaL_checkstack`

\[-0, +0, _v_\]

void luaL\_checkstack (lua\_State \*L, int sz, const char \*msg);

Extend stack space to `top sz` elements. If it does not extend, an error is thrown. `msg` is the extra text for the error message (`NULL` means no extra text is needed）。

* * *

#### `luaL_checkstring`

\[-0, +0, _v_\]

const char \*luaL\_checkstring (lua\_State \*L, int arg);

Checks whether the `arg` parameter of the function is a string and returns the string.

This function uses [lua_tolstring](#lua_tolstring) to get the result. So the function has the potential to cause conversions that are equally valid.

* * *

#### `luaL_checktype`

\[-0, +0, _v_\]

void luaL\_checktype (lua\_State \*L, int arg, int t);

Checks if the type of the `arg` parameter of the function is`t `. See [lua_type](#lua_type) to look up the encoding of type`t.

* * *

#### `luaL_checkudata`

\[-0, +0, _v_\]

void \*luaL\_checkudata (lua\_State \*L, int arg, const char \*tname);

Check that the `arg` parameter of the function is user data of type `tname` (see [luaL_newmetatable](#luaL_newmetatable) ). It returns the address of the user data (see. [`lua_touserdata`](#lua_touserdata)）。

* * *

#### `luaL_checkversion`

\[-0, +0, –\]

void luaL\_checkversion (lua\_State \*L);

Check that the kernel that called it is the kernel that created the Lua state machine. and whether the code that calls it uses the same Lua version. It also checks whether the kernel that called it and the kernel that created the Lua state machine use the same piece of address space.

* * *

#### `luaL_dofile`

\[-0, +?, _e_\]

int luaL\_dofile (lua\_State \*L, const char \*filename);

Loads and runs the specified file. It is defined by the following macro：

     (luaL\_loadfile(L, filename) || lua\_pcall(L, 0, LUA\_MULTRET, 0))

If there is no error, the function returns false; if there is an error, it returns true.

* * *

#### `luaL_dostring`

\[-0, +?, –\]

int luaL\_dostring (lua\_State \*L, const char \*str);

Loads and runs the specified string. It is defined by the following macro：

     (luaL\_loadstring(L, str) || lua\_pcall(L, 0, LUA\_MULTRET, 0))

If there is no error, the function returns false; if there is an error, it returns true.

* * *

#### `luaL_error`

\[-0, +0, _v_\]

int luaL\_error (lua\_State \*L, const char \*fmt, ...);

Throws an error. The format of the error message is given by `fmt. Several parameters are provided later, which follow the rules in [lua_pushfstring](#lua_pushfstring). If available, it also prefaces the message with the file name and line number at the time of the error.

This function never returns. But in C functions the idiom is usually followed： `return luaL_error(_args_)` 。

* * *

#### `luaL_execresult`

\[-0, +3, _e_\]

int luaL\_execresult (lua\_State \*L, int stat);

This function is used to generate the return values of standard library and process-related functions. (referring to [`OS. execute`](#pdf-os.execute) and [`io.close`](#pdf-io.close)）。

* * *

#### `luaL_fileresult`

\[-0, +(1|3), _e_\]

int luaL\_fileresult (lua\_State \*L, int stat, const char \*fname);

This function is used to generate the return values of file-related functions in the standard library. (refers to ([`io.open`](#pdf-io.open), [`OS. rename`](#pdf-os.rename), [`file:seek`](#pdf-file:seek), etc.。)。

* * *

#### `luaL_getmetafield`

\[-0, +(0|1), _e_\]

int luaL\_getmetafield (lua\_State \*L, int obj, const char \*e);

Stacks the value of the `e` field in the meta table of the object at index `obj. If the object does not have a meta table, or if the meta table does not have a related field, this function does not stack anything and returns `LUA_TNIL`。

* * *

#### `luaL_getmetatable`

\[-0, +1, –\]

int luaL\_getmetatable (lua\_State \*L, const char \*tname);

Stack the meta table corresponding to `tname` in the registry (see [luaL_newmetatable](#luaL_newmetatable)). If there is no meta table corresponding to `tname`, **nil** is pushed on the stack and false is returned.

* * *

#### `luaL_getsubtable`

\[-0, +1, _e_\]

int luaL\_getsubtable (lua\_State \*L, int idx, const char \*fname);

Make sure`t[fname]`is a table and stack that table. Here `t` refers to the value at index `idx. If it was originally a table, return true; otherwise, create a new table for it and return false.

* * *

#### `luaL_gsub`

\[-0, +1, _e_\]

const char \*luaL\_gsub (lua\_State \*L,
                       const char \*s,
                       const char \*p,
                       const char \*r);

Generate a copy of the string `s` and replace all of the string `p` with the string `r `. Stacks the result string and returns it.

* * *

#### `luaL_len`

\[-0, +0, _e_\]

lua\_Integer luaL\_len (lua\_State \*L, int index);

Returns the "length" of the value at the given index as a number; it is equivalent to the operation of calling ''#'' in Lua (see [§ 3.4.7](#3.4.7)). If the result of the operation is not an integer, an error is thrown. (This only happens when the meta method is triggered.。）

* * *

#### `luaL_loadbuffer`

\[-0, +1, –\]

int luaL\_loadbuffer (lua\_State \*L,
                     const char \*buff,
                     size\_t sz,
                     const char \*name);

Equivalent to [luaL_loadbufferx](#luaL_loadbufferx) with `mode` parameter equal `NULL`。

* * *

#### `luaL_loadbufferx`

\[-0, +1, –\]

int luaL\_loadbufferx (lua\_State \*L,
                      const char \*buff,
                      size\_t sz,
                      const char \*name,
                      const char \*mode);

Load a cache as a block of Lua code. This function uses [lua_load](#lua_load) to load the memory area of length `sz` pointed to by `buff.

This function returns the same value as [lua_load](#lua_load). `name` is the name of the code block used for debugging information and error messages. The role of the `mode` string is the same as that of the function [`lua_load`](#lua_load)。

* * *

#### `luaL_loadfile`

\[-0, +1, _e_\]

int luaL\_loadfile (lua\_State \*L, const char \*filename);

Equivalent to [luaL_loadfilex](#luaL_loadfilex) with `mode` parameter equal `NULL`。

* * *

#### `luaL_loadfilex`

\[-0, +1, _e_\]

int luaL\_loadfilex (lua\_State \*L, const char \*filename,
                                            const char \*mode);

Load a file as a Lua code block. This function uses [lua_load](#lua_load) to load data from a file. The name of the code block is called `filename `. If `filename` is `NULL`, it is loaded from standard input. If the first line of the file is prefixed with `#`, this line is ignored.

`mode` The role of the string is the same as the function [`lua_load`](#lua_load)。

The return value of this function is the same as [lua_load](#lua_load), but it may also generate an error code called LUA_ERRFILE. This error occurs when the file cannot be opened or read, or the file has an incorrect mode.

Like [lua_load](#lua_load), this function only loads blocks of code and does not run.

* * *

#### `luaL_loadstring`

\[-0, +1, –\]

int luaL\_loadstring (lua\_State \*L, const char \*s);

Loads a string as a Lua code block. This function uses [lua_load](#lua_load) to load a zero-terminated string `s`。

The return value of this function is the same as [lua_load](#lua_load).

Also like [lua_load](#lua_load), this function only loads blocks of code and does not run.

* * *

#### `luaL_newlib`

\[-0, +1, _e_\]

void luaL\_newlib (lua\_State \*L, const luaL\_Reg l\[\]);

Create a new table and register the functions in the list `l.

It is implemented with the following macros：

     (luaL\_newlibtable(L,l), luaL\_setfuncs(L,l,0))

Array `l` must be an array, not a pointer.

* * *

#### `luaL_newlibtable`

\[-0, +1, _e_\]

void luaL\_newlibtable (lua\_State \*L, const luaL\_Reg l\[\]);

Create a new table and pre-allocate enough space to hold the contents of array `l` (but not fill it). This is for use with [`luaL_setfuncs](#luaL_setfuncs) (cf. [`luaL_newlib`](#luaL_newlib)）。

It is implemented as a macro, and the array `l` must be an array, not a pointer.

* * *

#### `luaL_newmetatable`

\[-0, +1, _e_\]

int luaL\_newmetatable (lua\_State \*L, const char \*tname);

Returns 0 if the key `tname` already exists in the registry. Otherwise, create a new table for the meta table of user data. Add `__name = tname` key-value pair to this table and add`[tname] = new table` to the registry, returning 1. (`__name` item can be used for some error output functions。）

Both cases will stack the value associated with `tname` in the final registry.

* * *

#### `luaL_newstate`

\[-0, +0, –\]

lua\_State \*luaL\_newstate (void);

Create a new Lua state machine. It calls [lua_newstate](#lua_newstate) with a memory allocator based on the standard C implementation of the `realloc` function. And set up a panic function (see [§ 4.6](#4.6)) that prints some error information to the standard error output to handle fatal errors.

Returns the new state machine. Returns if memory allocation fails `NULL` 。

* * *

#### `luaL_openlibs`

\[-0, +0, _e_\]

void luaL\_openlibs (lua\_State \*L);

Opens all Lua standard libraries in the specified state machine.

* * *

#### `luaL_optinteger`

\[-0, +0, _v_\]

lua\_Integer luaL\_optinteger (lua\_State \*L,
                             int arg,
                             lua\_Integer d);

If the `arg` argument to the function is an integer (or can be converted to an integer), return that integer. If the parameter does not exist or is **nil**, return `d `. Other than that, an error is thrown.

* * *

#### `luaL_optlstring`

\[-0, +0, _v_\]

const char \*luaL\_optlstring (lua\_State \*L,
                             int arg,
                             const char \*d,
                             size\_t \*l);

If the `arg` argument to the function is a string, return that string. If the parameter does not exist or is **nil**, return `d `. Other than that, an error is thrown.

If `l` is not `NULL`, fill in the length of the result. `*l` 。

* * *

#### `luaL_optnumber`

\[-0, +0, _v_\]

lua\_Number luaL\_optnumber (lua\_State \*L, int arg, lua\_Number d);

If the `arg` argument to the function is a number, return that number. If the parameter does not exist or is **nil**, return `d `. Other than that, an error is thrown.

* * *

#### `luaL_optstring`

\[-0, +0, _v_\]

const char \*luaL\_optstring (lua\_State \*L,
                            int arg,
                            const char \*d);

If the `arg` argument to the function is a string, return that string. If the parameter does not exist or is **nil**, return `d `. Other than that, an error is thrown.

* * *

#### `luaL_prepbuffer`

\[-?, +?, _e_\]

char \*luaL\_prepbuffer (luaL\_Buffer \*B);

Equivalent to [`luaL_prepbuffsize](#luaL_prepbuffsize) with a predefined size `LUAL_BUFFERSIZE`。

* * *

#### `luaL_prepbuffsize`

\[-?, +?, _e_\]

char \*luaL\_prepbuffsize (luaL\_Buffer \*B, size\_t sz);

Returns a space address of size `sz. You can copy the string into the cache `B` (see [luaL_Buffer](#luaL_Buffer)). After copying the string into it, you must call [`luaL_addsize`](#luaL_addsize) to pass in the size of the string before it will actually be added to the cache.

* * *

#### `luaL_pushresult`

\[-?, +1, _e_\]

void luaL\_pushresult (luaL\_Buffer \*B);

End the use of cache `B`, leaving the final string at the top of the stack.

* * *

#### `luaL_pushresultsize`

\[-?, +1, _e_\]

void luaL\_pushresultsize (luaL\_Buffer \*B, size\_t sz);

equivalent [`luaL_addsize`](#luaL_addsize)，[`luaL_pushresult`](#luaL_pushresult)。

* * *

#### `luaL_ref`

\[-1, +0, _e_\]

int luaL\_ref (lua\_State \*L, int t);

For the object at the top of the stack, create and return a_reference_in the table pointed to by the index` t` (the top of the stack object will be popped up at the end）。

This reference is a unique integer key. As long as you don`t manually add integer keys to table `t`, [luaL_ref](#luaL_ref) guarantees the uniqueness of the keys it returns. You can retrieve the object referenced by `r` by calling `lua_rawgeti(L, t, r). Function [`luaL_unref `](#luaL_unref) is used to release a reference to the associated object

If the object at the top of the stack is **nil**, [luaL_ref](#luaL_ref) will return the constant LUA_REFNIL `. The constant LUA_NOREF can be guaranteed to be different from other reference values returned by [luaL_ref](#luaL_ref).

* * *

#### `luaL_Reg`

typedef struct luaL\_Reg {
  const char \*name;
  lua\_CFunction func;
} luaL\_Reg;

The array type used for the [luaL_setfuncs](#luaL_setfuncs) registration function. `name` is the function name, and `func` is the function pointer. Any [`luaL_Reg](#luaL_Reg) array must end with the pair `name` and `func` both being `NULL.

* * *

#### `luaL_requiref`

\[-0, +1, _e_\]

void luaL\_requiref (lua\_State \*L, const char \*modname,
                    lua\_CFunction openf, int glb);

If `modname` is not in [`package.loaded`](#pdf-package.loaded), the function `openf` is called and the string `modname` is passed in `. Put its return value into `package.loaded[modname]`. This behaves as if the function had been called through [`require`](#pdf-require).

If `glb` is true, the module is also set to the global variable `modname.

Leave a copy of the module on the stack.

* * *

#### `luaL_setfuncs`

\[-nup, +0, _e_\]

void luaL\_setfuncs (lua\_State \*L, const luaL\_Reg \*l, int nup);

Register all functions in array `l` (see [luaL_Reg](#luaL_Reg)) in a table at the top of the stack (the table is below the optional upper value, see the explanation below）。

If `nup` is not zero, all functions share `nup` values. These values must be pressed on the table before the call. These values are popped from the stack after registration.

* * *

#### `luaL_setmetatable`

\[-0, +0, –\]

void luaL\_setmetatable (lua\_State \*L, const char \*tname);

Set the `tname` associated meta table (see [luaL_newmetatable](#luaL_newmetatable)) in the registry as the meta table of the top-of-stack object.

* * *

#### `luaL_Stream`

typedef struct luaL\_Stream {
  FILE \*f;
  lua\_CFunction closef;
} luaL\_Stream;

The standard file handle structure used in the standard input and output library.

The file handle is implemented as a full user data whose meta-table is called a LUA_FILEHANDLE (LUA_FILEHANDLE is a macro that represents the name of the real meta-table). This meta table is created by the standard input/output library (see [luaL_newmetatable](#luaL_newmetatable)).

User data must begin with the structure luaL_Stream; this structure can be followed by any other data. The `f` field points to a C data stream (if it is `NULL` it means an uncreated handle). The `closef` field points to a Lua function that needs to be called when the stream is closed or recycled. The function will receive one argument, the file handle. It needs to return **true** (the operation succeeded) or **nil** plus an error message (when an error occurs). Once Lua calls this field, the value of the field is changed to `NULL` to indicate that the handle has been closed.

* * *

#### `luaL_testudata`

\[-0, +0, _e_\]

void \*luaL\_testudata (lua\_State \*L, int arg, const char \*tname);

This function is similar to [luaL_checkudata](#luaL_checkudata). But it returns `NULL` instead of throwing an error when the test fails.

* * *

#### `luaL_tolstring`

\[-0, +1, _e_\]

const char \*luaL\_tolstring (lua\_State \*L, int idx, size\_t \*len);

Converts the Lua value at the given index to a C string in the appropriate format. The result string will not only be pushed on the stack, but will also be returned by the function. If `len` is not `NULL`, it also sets the string length to `* len.

If the value has a meta table with `__tostring` field, the luaL_tolstring calls the corresponding meta method with the value as the parameter and returns the value as the result.

* * *

#### `luaL_traceback`

\[-0, +1, _e_\]

void luaL\_traceback (lua\_State \*L, lua\_State \*L1, const char \*msg,
                     int level);

Stack backtracking information of stack `L1` is pressed. If `msg` is not `NULL`, it will be appended to the stack before the backtrace information. The `level` parameter indicates the layer from which to do stack backtracking.

* * *

#### `luaL_typename`

\[-0, +0, –\]

const char \*luaL\_typename (lua\_State \*L, int index);

Returns the type name of the value at the given index.

* * *

#### `luaL_unref`

\[-0, +0, –\]

void luaL\_unref (lua\_State \*L, int t, int ref);

Releases the `ref` reference object for the table at index `t` (see [luaL_ref](#luaL_ref) ). This entry is removed from the table to make the object it references available for garbage collection. The reference `ref` is also recycled for reuse.

If `ref` is [LUA_NOREF](#pdf-LUA_NOREF) or [LUA_REFNIL](#pdf-LUA_REFNIL), [luaL_unref](#luaL_unref) does nothing.

* * *

#### `luaL_where`

\[-0, +1, _e_\]

void luaL\_where (lua\_State \*L, int lvl);

A string representing the control point location of the `lvl` layer stack is stacked. This string follows the following format：

     _chunkname_:_currentline_:

0 Layer refers to the currently running function, layer 1 refers to the function that calls the running function, and so on.

This function is used to construct the prefix of the error message.

## 6 – Standard Library

The standard library provides some useful functions that are implemented directly in the C API. Some of these functions provide services that are native to the language (e. g.,[`type`](#pdf-type) and [`getmetatable`](#pdf-getmetatable)); Others provide services that deal with "external" (e. g. I/O ); Still others could have been implemented in Lua itself, but implemented in C can meet key performance requirements (e. g. [`table.sort`](#pdf-table.sort)）。

All libraries are implemented directly in the C API and are provided as separate C modules. Currently, Lua has the following standard libraries：

*   Base Library ([§6.1](#6.1));
*   Synergy Library ([§6.2](#6.2));
*   Package Management Library ([§6.3](#6.3));
*   String Control ([§6.4](#6.4));
*   Basic UTF-8 support ([§6.5](#6.5));
*   Table Control ([§6.6](#6.6));
*   Mathematical functions ([§ 6.7](#6.7)) (sin ,log, etc.);
*   Input Output ([§6.8](#6.8));
*   Operating System Library ([§6.9](#6.9));
*   Debug Library ([§6.10](#6.10)).

With the exception of the base library and the package management library, all libraries place their functions in a domain of a global table, or provide them as object methods.

To use these libraries, the C host program needs to call the [luaL_openlibs](#luaL_openlibs) function, which opens all the standard libraries. Or the host program can use [`luaL_requiref`](#luaL_requiref) to open these libraries respectively: `luaopen_base` (basic library), `luaopen_package` (package management library), `luaopen_coroutine` (coroutine library), `luaopen_string` (string library), `luaopen_utf8` (UTF8 library), `luaopen_table` (table processing library), `luaopen_math` (math library), luaopen_io (I/O library), luaopen_ OS (operating system library), luaopen_debug (debug library). These functions are defined in `lualib.h.

### 6.1 – basic function

The base library provides the Lua core functions. If you don't include this library in your program, you need to be careful to check whether the program needs to provide its own implementation of some of these features.

* * *

#### `assert (v [, message])`

If the value of its parameter `v` is false (**nil** or **false**), it calls [`error`](#pdf-error); otherwise, it returns all the parameters. In the case of an error, `message` refers to that error object; if this parameter is not supplied, the parameter defaults "`assertion failed!`" 。

* * *

#### `collectgarbage ([opt [, arg]])`

This function is a common interface to the garbage collector. With the parameter `opt` it provides a different set of functions：

*   **"`collect`":** Do a full garbage collection cycle. This is the default option.
*   **"`stop`":** Stops the garbage collector from running. The collector will only run as a result of an explicit call before a restart is called.
*   **"`restart`":** Restart the automatic run of the garbage collector.
*   **"`count`":** Returns the total memory used by Lua in K bytes. This value has a decimal part, so you only need to multiply 1024 to get the exact number of bytes used by Lua (unless overflow）。
*   **"`step`":** Step into the garbage collector. The step size is controlled by `arg. When 0 is passed in, the collector is stepped (indivisible) by one step. Passing in a non-zero value, the collector collects the equivalent of Lua allocating these multiple (K bytes) of memory. If the collector ends a loop will return **true** 。
*   **"`setpause`":** Set `arg` to the collector`s_intermittent rate_(see [§ 2.5](#2.5)). Returns the previous value of_intermittent rate.
*   **"`setstepmul`":** Set `arg` to the_step factor_of the collector (see [§ 2.5](#2.5)). Returns the previous value of_step magnification.
*   **"`isrunning`":** Returns a Boolean value that indicates whether the collector is working (that is, not stopped.）。

* * *

#### `dofile ([filename])`

Open the file with that name and execute the Lua code block in the file. When called without parameters, `dofile` performs the contents of the standard input (`stdin`). Returns all return values for this code block. For the case of errors, `dofile` feeds back the error to the caller (ie, `dofile` is not running in protected mode）。

* * *

#### `error (message [, level])`

Aborts the last protection function call, returning the error object `message. The function `error` never returns.

When the message is a string, `error` usually prepends some information about where the error occurred. The `level` parameter indicates how to obtain the error location. For level 1 (the default), the error location refers to the location of the `error` function call. Level 2 points the error location to the function calling the function of `error`; and so on. Pass-in level 0 to avoid adding error location information before the message.

* * *

#### `_G`

A global variable (not a function) that internally stores the global environment (see [§ 2.2](#2.2)). Lua does not use this variable itself; changing the value of this variable does not affect any environment, and vice versa.

* * *

#### `getmetatable (object)`

If `object` does not contain a meta table, **nil** is returned * *. Otherwise, return its associated value if there is a `"__metatable"` field in the object`s meta table, and return the object`s meta table if not.

* * *

#### `ipairs (t)`

returns three values (iterated function, table `t`, and 0 ), so the following code

     for i,v in ipairs(t) do _body_ end

The key-value pairs (`1,t[1]`) ,(`2,t[2]`),... will be iterated until the first null value.

* * *

#### `load (chunk [, chunkname [, mode [, env]]])`

Load a block of code.

If `chunk` is a string, the code block refers to this string. If `chunk` is a function, `load` keeps calling it to get snippets of code blocks. Each call to `chunk` must return a string immediately after the return string of the previous call. When an empty string is returned, **nil**, or no value is returned, the code block is over.

Returns a compiled block of code as a function if there are no syntax errors; otherwise, returns **nil** plus an error message.

If the result function has an upper value, `env` is set to the first upper value. If this parameter is not supplied, the global environment overrides it. All other upper values are initialized to **nil * *. (When you load the main code block, the result function must have only one upper value `_ENV` (see [§ 2.2](#2.2))). However, if you load a block of binary code created with a function (see [`string.dump`](#pdf-string.dump), the resulting function can have any number of upper values), all the upper values are newly created. This means that they are not shared with any other functions.

`chunkname` In error messages and debug messages (see [§ 4.9](#4.9)), the name used for the code block. If this parameter is not supplied, it defaults to the string `chunk `. If `chunk` is not a string, otherwise "`=(load)`" 。

The string `mode` is used to control whether the code block is text or binary (that is, a precompiled code block). It can be the string "`B`" (can only be a binary code block), "`t`" (can only be a text code block), or "`bt`" (can be either binary or text). The default value is "`bt`"。

Lua Binary code blocks are not checked for robustness. Maliciously constructing a binary block has the potential to crash the interpreter.

* * *

#### `loadfile ([filename [, mode [, env]]])`

Similar to [`load`](#pdf-load), but gets the code block from the file `filename` or standard input (if a file name is not provided).

* * *

#### `next (table [, index])`

Run the program to traverse all the fields in the table. The first parameter is the table to be traversed, and the second parameter is a key in the table. `next` returns the next key of the key and its associated value. Calling `next` with **nil** as the second argument returns the initial key and its associated value. When called with the last key, or an empty table is called with **nil**, `next` returns **nil * *. If you do not provide the second argument, it will be considered to be **nil * *. In particular, you can use `next(t)`to determine whether a table is empty.

The order of indexes during traversal is undefined,_even for numeric indexes_. (If you want to traverse the table in numerical order, you can use **for** 。）

When you assign a value to a field that does not exist in the table during traversal, the behavior of `next` is undefined. However, you can modify existing fields. In particular, you can clear some existing domains.

* * *

#### `pairs (t)`

If `t` has meta method `__pairs`, call it with `t` as an argument and return the first three values it returns.

Otherwise, three values are returned:[`next`](#pdf-next) function, table `t`, and **nil * *. So the following code

     for k,v in pairs(t) do _body_ end

Can represent all key-value pairs in`t.

See the function [`next`](#pdf-next) for the risk of modifying a table during an iteration.

* * *

#### `pcall (f [, arg1, ···])`

Pass in the parameter to call the function `f` in_protected mode `. This means that any error in `f` will not be thrown; instead, `pcall` will catch the error and return a status code. The first return value is the status code (a boolean), which is true when there are no errors. In this case, `pcall` will also return the results of all calls after the status code. When there is an error, `pcall` returns **false** plus the error message.

* * *

#### `print (···)`

Receive any number of parameters and print their values to `stdout `. It uses the [`tostring`](#pdf-tostring) function to convert each argument to a string. `print` is not used to do formatted output. Just as a shortcut to look at a value. More for debugging. For complete control of the output, use [`string.format`](#pdf-string.format) and [`io.write`](#pdf-io.write)。

* * *

#### `rawequal (v1, v2)`

Check if `v1` is equal to `v2` without triggering any meta-methods. Returns a Boolean quantity.

* * *

#### `rawget (table, index)`

Get the value of `table[index]`without triggering any meta-methods. `table` must be a table; `index` can be any value.

* * *

#### `rawlen (v)`

Returns the length of object `v` without triggering any meta-methods. `v` can be a table or a string. It returns an integer.

* * *

#### `rawset (table, index, value)`

Set `table[index]`to `value` without triggering any meta-methods`. `table` must be a table, and `index` can be any value other than **nil** or NaN. `value` can be any Lua value.

This function returns `table`。

* * *

#### `select (index, ···)`

If `index` is a number, the part of the argument after the `index` is returned; negative numbers are indexed backwards and forwards (-1 refers to the last argument). Otherwise, `index` must be the string `# "`, where `select` returns the number of arguments.

* * *

#### `setmetatable (table, metatable)`

Sets the meta table for the specified table. (You can`t change the meta table for other types of values in Lua, those can only be done in C.) If `metatable` is **nil**, the meta table of the specified table is removed. If the original meta table has `__metatable` field, an error is thrown.

This function returns `table`。

* * *

#### `tonumber (e [, base])`

If there is no `base` at the time of the call, `number` tries to convert the argument to a number. If the argument is already a number, or a string that can be converted to a number, `number` returns that number; otherwise **nil**。

The result of the string conversion may be an integer or a floating point number, depending on Lua's conversion grammar (see [§ 3.1](#3.1)). (Strings can have leading and trailing spaces, and can be signed。）

When `base` is passed in to call it, `e` must be an integer string represented in that base. The base can be any integer between 2 and 36, inclusive. When greater than 10, the letter ``A`` (both upper and lower case) means 10 , ``B`` means 11, and ``Z`` means 35. If the string `e` is not a legal number in this base, the function returns **nil**。

* * *

#### `tostring (v)`

Can receive any type, it converts it to human-readable string form. Floating-point numbers are always converted to the representation of floating-point numbers (decimal or exponential). (If you want full control over how numbers are converted, you can use [`string.format`](#pdf-string.format)。）

If `v` has a metable`__tostring "`field, `tostring` will call it with `v` as an argument. and use its result as the return value.

* * *

#### `type (v)`

Encodes the type of the argument as a string returned. The possible return value of the function is "`nil`" (a string, not a **nil** value）， "`number`"， "`string`"， "`boolean`"， "`table`"， "`function`"， "`thread`"， "`userdata`"。

* * *

#### `_VERSION`

A global variable (not a function) that contains the version number of the current interpreter. The current value of this variable is "`Lua 5.3`"。

* * *

#### `xpcall (f, msgh [, arg1, ···])`

This function is similar to [`pcall`](#pdf-pcall). However, it can set up an additional message processor. `msgh`。

### 6.2 – Synergy Management

Operations on coroutines are placed in a separate table `coroutine` as a sublibrary of the base library. For the introduction of the association, see [§2.6](#2.6) 。

* * *

#### `coroutine.create (f)`

Create a new coprocess with a body function of `f. `f` must be a Lua function. Returns this new coprocess, which is an object of type `"thread.

* * *

#### `coroutine.isyieldable ()`

Returns true if the running coroutes can yield.

The current coroutation is transferable when it is not in the main thread or in a C function that cannot be transferred.

* * *

#### `coroutine.resume (co [, val1, ···])`

Start or continue the operation of the coroutine `co. When you first continue a coroutine, it will start running from the main function. `val1`,... These values are passed in as arguments to the body function. If the coroutine is relinquished, `resume` will restart it; `val1`,... these parameters will be used as the return value of the relinquished point.

If the coroutine runs without errors, `resume` returns **true** plus all the values passed to` yield` (when the coroutine gives up), or all the return values of the subject function (when the coroutine aborts). If any error occurs, `resume` returns **false** plus the error message.

* * *

#### `coroutine.running ()`

Returns the current running coroutation plus a boolean. This is true if the currently running coroute is the main thread.

* * *

#### `coroutine.status (co)`

Return the status of the coroutine `co` in the form of a string: when the coroutine is running (it is the one that call` status`), return` "running" `; If the coroutine call` yield` pending or has not yet started running, return` "suspended" `; If the coroutine is active but not running (I .e. it is continuing other coroutines), return` "`normal"`; if the coroutine finishes running the body function or stops due to an error, returns `"dead"`。

* * *

#### `coroutine.wrap (f)`

Create a new coprocess with a body function of `f. `f` must be a Lua function. Returns a function that continues the coroutation each time it is called. The arguments passed to this function will be extra arguments to `resume. and `resume` return the same value, just without the first boolean. If any error occurs, throw this error.

* * *

#### `coroutine.yield (···)`

Suspends execution of the calling corouty. Any parameter passed to `yield` is converted to an additional return value of `resume.

### 6.3 – Module

The package management library provides the base for loading modules from Lua. Only one exported function is placed directly in the global environment: [`require`](#pdf-require). All other parts are exported in the table `package.

* * *

#### `require (modname)`

Load a module. This function first looks up the [`package.loaded`](#pdf-package.loaded) table to check if the `modname` has been loaded. If loaded, `require` returns the value saved in `package.loaded[modname]. Otherwise, it tries to find the_loader for the module._ 。

`require` Follow the [`package.searchers`](#pdf-package.searchers) sequence to find the loader. If we change this sequence, we can change how `require` looks for a module. The following description is based on the default configuration of [`package.searchers`](#pdf-package.searchers).

First `require` looks for `package.preload[modname]`. If there is a value here, this value (which must be a function) is the loader. Otherwise `require` uses the Lua loader to find the path of [`package.path`](#pdf-package.path). If the lookup fails, then use the C loader to find the path of [`package.cpath`](#pdf-package.cpath). If all fail, try the_integration_loader again (see [`package.searchers`](#pdf-package.searchers)）。

Each time a loader is found, `require` calls the loader with two parameters: `modname` and a parameter obtained during the acquisition of the loader. (If the loader is obtained by looking for a file, this extra parameter is the file name.) If the loader returns a non-null value, `require` assigns this value to `package.loaded[modname]`. If the loader fails to return a non-null value to assign to `package.loaded[modname]`, `require` will be set to **true** there * *. Whatever the case, `require` returns the final value of `package.loaded[modname].

If there is an error loading or running the module, or if the loader cannot be found for the module, `require` will throw an error.

* * *

#### `package.config`

A string that describes some compile-time configuration information prepared for package management. This string consists of a series of lines：

*   The first line is a directory split string. The default is '''' for Windows, and for other systems. '`/`' 。
*   The second line is the separator used in the path. The default value is '`;`' 。
*   The third line is the string used to mark the template replacement point. The default is '`?`' 。
*   The fourth line is the string that will be replaced in the Windows with the path of the directory where the executable is located. The default is '`!`' 。
*   The fifth line is a token, and all text after this token will be ignored when constructing the `luaopen_`function name. The default is '`-`'。

* * *

#### `package.cpath`

This path is used by [`require`](#pdf-require) when doing a search in the C loader.

Lua The C path [`package.cpath`](#pdf-package.path) is initialized in the same way as the Lua path [`package. pdf-package.cpath](#) is initialized. It is initialized with the environment variable LUA_CPATH_5_3 or the environment variable LUA_CPATH. Either take the default path defined in `luaconf.h.

* * *

#### `package.loaded`

A table used for [`require`](#pdf-require) to control which modules have been loaded. When you request a `modname` module and `package.loaded[modname]`is not false, [`require`](#pdf-require) simply returns the stored value.

This variable is only a reference to the actual table; changing this value does not change the table used by [`require`](#pdf-require).

* * *

#### `package.loadlib (libname, funcname)`

Let the host program dynamically link the C library. `libname` 。

When `funcname` is "`*`", it only connects to the library and allows the symbols in the library to be exported to other dynamic link libraries. Otherwise, it looks for the function `funcname` in the library and returns it as a C function. Therefore, `funcname` must follow the prototype [`lua_CFunction](#lua_CFunction) (cf. [`lua_CFunction`](#lua_CFunction)）。

This is a low-order function. It completely bypasses the package module system. Unlike [`require`](#pdf-require), it does not do any path lookup and does not automatically add an extension. `libname` must be the full filename required by a C library, providing the path and extension if necessary. `funcname` must be the exact name required by the C library (depending on the C compiler and linker used）。

This function is not supported in standard C. Therefore, it is only valid on some platforms (Windows ,Linux ,Mac OS X, Solaris, BSD, plus Unix systems that support the` dlfcn` standard）。

* * *

#### `package.path`

This path is used by [`require`](#pdf-require) when searching in the Lua loader.

At startup, Lua initializes this variable with the environment variable LUA_PATH_5_3 or the environment variable LUA_PATH. Or take the default path in `luaconf.h. All occurrences of "`;;`" in the environment variable are replaced with the default path.

* * *

#### `package.preload`

holds a loader with some special modules (see [`require`](#pdf-require)）。

This variable is only a reference to the actual table; changing this value does not change the table used by [`require`](#pdf-require).

* * *

#### `package.searchers`

Table for [`require`](#pdf-require) to control how modules are loaded.

Each item in this table is a_finder function_. When looking up a module, [`require`](#pdf-require) calls these finder in order, passing in the module name (an argument to the [`require`](#pdf-require)) as the only argument. This function can return another function (the module`s_loader_) plus another argument that will be passed to this loader. Or return a string describing why the module was not found (or return **nil** and don`t want to say anything）。

Lua Initialize this table with four finder functions.

The first finder is simply a loader in the [`package.preload`](#pdf-package.preload) table.

The second finder is used to find loaded libraries for Lua libraries. It uses the path stored in [`package.path`](#pdf-package.path) to do the lookup. The search process is consistent with the description of the function [`package.searchpath`](#pdf-package.searchpath).

The third finder is used to find the loaded library for the C library. It uses the path stored in [`package.cpath`](#pdf-package.path) to do the lookup. Similarly, the lookup process is identical to that described by the function [`package.searchpath`](#pdf-package.searchpath). For example, if the C path is such a string

     "./?.so;./?.dll;/usr/local/?/init.so"

The finder lookup module `foo` attempts to open the files `./foo.so`,`./foo.dll`, and `/usr/local/foo/init.so`. Once it finds a C library, the finder first connects to the library using a dynamic linking mechanism. Then try to find a C function in that library that you can use as a loader. The name of this C function is "`luaopen_`" a string immediately following the module name, where all underscores in the string are replaced with dots. In addition, if there is a horizontal line in the module name, the part after the horizontal line (including the horizontal line) is removed. For example, if the module is named `a. B. c-v2.1, `the function name is `luaopen_a_b_c`。

The fourth searcher is the_all-in-one loader_. It looks up the root name of the specified module from the C path. For example, when requesting `a. B. c`, it will look for `a` which is the C library. If it can find it, it will find the loading function of the submodule in it. In our case, we are looking for "luaopen_a_ B _c `. Using this mechanism, several C submodules can be packaged into a single library. Each submodule can have an original load function name.

Except for the first (preloaded) searcher, each searcher returns the filename of the module it finds. This is the same as the return value of [`package.searchpath`](#pdf-package.searchpath). The first searcher has no return value.

* * *

#### `package.searchpath (name, path [, sep [, rep]])`

Searches the specified `path` for the specified `name` 。

A path is a string consisting of a series of_templates_separated by semicolon. For each template, each question mark in it (if any) is replaced with `name. And replace the `sep` (default is a dot) with `rep` (default is the directory separator of the system). Then try to open this file name.

For example, if the path is a string

     "./?.lua;./?.lc;/usr/local/?/init.lua"

Searching for the name `foo.a` will attempt to open the files `./foo/a.lua`, `./foo/a.lc`, and `/usr/local/foo/a/init.lua`。

Returns the name of the first file that can be opened in read mode (and closed immediately). If no such file exists, return **nil** plus an error message. (This error message lists all the file names you tried to open.。）

### 6.4 – String processing

This library provides generic functions for string processing. For example, string lookup, substring, pattern matching, etc. When indexing strings in Lua, the first character is counted from 1 (not 0 in C). The index can be a negative number, which refers to the reverse parsing from the end of the string. That is, the last character is at the -1 position, and so on.

All functions in the string library are in the table `string. It also sets it to the `__index` field of the string meta table. Therefore, you can use string functions in object-oriented form. For example, `string.byte(s, I) `can be written `s:byte(i)`。

String libraries assume single-byte character encoding.

* * *

#### `string.byte (s [, i [, j]])`

Returns the internal numeric encoding of the characters `[I]`, `[I 1]`,... ,`[j]. The default value for `I` is 1; the default value for `j` is `I`. These indexes are modified by the rules of the function [`string.sub`](#pdf-string.sub).

There is no need for digital encoding to be cross-platform.

* * *

#### `string.char (···)`

Receives zero or more integers. Returns a string of the same length as the number of arguments. The internal encoding value of each character is equal to the corresponding parameter value.

There is no need for digital encoding to be cross-platform.

* * *

#### `string.dump (function [, strip])`

Returns a string containing the specified function in binary representation (a_binary code block_). You can then call this string with [`load`](#pdf-load) to get a copy of the function (but bind the new upper value). If `strip` is true, the binary code block does not carry debugging information for the function (local variable name, line number, etc.。）。

The function with values only holds the number of values. When loaded (again), these upper values are updated to instances of **nil. (You can use the debug library to serialize the upper value in the way you need and overload it into the function.）

* * *

#### `string.find (s, pattern [, init [, plain]])`

Find the matching `pattern` in the first string `s` (see [§ 6.4.1](#6.4.1)). If a match is found, `find` returns the index in `s` about where it starts and ends; otherwise, **nil** is returned * *. The third optional numeric parameter `init` indicates where to start the search; it defaults to 1 and can be negative. When the fourth optional parameter `plain` is **true**, the pattern matching mechanism is disabled. At this point, the function only does the direct "find substring" operation, and no characters in `pattern` are regarded as magic characters. Note that if `plain` is given, it must be written on `init` 。

If capture is defined in the schema, several values captured are also returned after two indexes.

* * *

#### `string.format (formatstring, ···)`

Returns a formatted version of an indefinite number of arguments, formatted as the first argument (must be a string). The formatted string follows the rules of the ISO C function `sprintf. The difference is that the options `*`, `h`, `L`, `l`, `n`, `p` are not supported, and an option `q` is added `. The `q` option formats a string as a string surrounded by two double quotes, with appropriate escaping of internal characters. This string can be safely read back by the Lua interpreter. For example, calling

     string.format('%q', 'a string with "quotes" and \\n new line')

will produce the string：

     "a string with \\"quotes\\" and \\
      new line"

Options `A` and `a` (if any), `E`, `e`, `f`, `G`, and `g` all expect a corresponding numeric argument. The options `c`, `d`, `I`, `o`, `u`, `x`, and `x` expect an integer. Option `q` expects a string; option `s` expects a string without embedded zeros. If the parameter corresponding to the option `s` is not a string, it will be converted to a string according to the rules of [`tostring`](#pdf-tostring).

* * *

#### `string.gmatch (s, pattern)`

Returns an iterator function. Each call to this function will continue to match `s` with `pattern` (see [§ 6.4.1](#6.4.1)) and return all captured values. If no capture is specified in `pattern`, the entire `pattern`。

The following example iterates over all the words in the string `s` and prints them line by line.：

     s = "hello world from Lua"
     for w in string.gmatch(s, "%a+") do
       print(w)
     end

The next example collects all key-value pairs `key = value` from the specified string and puts them into a table.：

     t = {}
     s = "from=world, to=Lua"
     for k, v in string.gmatch(s, "(%w+)=(%w+)") do
       t\[k\] = v
     end

For this function, the ``^`` that starts before the template will not be used as an anchor. Because this prevents iteration.

* * *

#### `string.gsub (s, pattern, repl [, n])`

Replace all (or the first `n` when `n` is given) `pattern` (see [§ 6.4.1](#6.4.1)) in the string `s` with `repl`, and return its copy. `repl` can be a string, a table, or a function. `gsub` will also return the total number of matches in the second return value. The name `gsub` comes from _Global SUBstitution_ 。

If `repl` is a string, then use this string as the replacement. The character `%` is an escape character: all strings of the form `%_d_` in `repl` represent the_d_th captured substring, which can be 1 through 9. The string `%0` represents the entire match. The string `%%` represents a single `%`。

If `repl` is a table, each match will use the first catch as a key to look up the table.

If `repl` is a function, this function is called every time a match occurs. All captured substrings are passed in as parameters in turn.

In any case, no capture set in the template is considered to capture the entire template.

If the query result of the table or the return result of the function is a string or a number, it is used as a replacement string. However, when returning **false** or **nil**, no replacement is made (that is, the original string before matching is retained）。

Here are some use cases：

     x = string.gsub("hello world", "(%w+)", "%1 %1")
     --> x="hello hello world world"

     x = string.gsub("hello world", "%w+", "%0 %0", 1)
     --> x="hello hello world"

     x = string.gsub("hello world from Lua", "(%w+)%s\*(%w+)", "%2 %1")
     --> x="world hello Lua from"

     x = string.gsub("home = $HOME, user = $USER", "%$(%w+)", os.getenv)
     --> x="home = /home/roberto, user = roberto"

     x = string.gsub("4+5 = $return 4+5$", "%$(.-)%$", function (s)
           return load(s)()
         end)
     --> x="4+5 = 9"

     local t = {name="lua", version="5.3"}
     x = string.gsub("$name-$version.tar.gz", "%$(%w+)", t)
     --> x="lua-5.3.tar.gz"

* * *

#### `string.len (s)`

Receives a string and returns its length. The length of the empty string `""` is 0. Embedded zeros are also counted, so the length of `"a\000bc\000"` is 5 。

* * *

#### `string.lower (s)`

Receives a string, converts all uppercase characters in it to lowercase, and returns a copy of it. Other strings do not change. The definition of uppercase characters depends on the current locale.

* * *

#### `string.match (s, pattern [, init])`

Find the first part of the string `s` that can be matched with `pattern` (see [§ 6.4.1](#6.4.1)). If found, `match` returns the catch; otherwise, it returns **nil * *. If no capture is specified in `pattern`, the entire string captured by `pattern` is returned. The third optional numeric parameter `init` indicates where to start the search; it defaults to 1 and can be negative.

* * *

#### `string.pack (fmt, v1, v2, ···)`

Returns a binary string packed (I. e., serialized in binary form) with the values `v1`, `v2`, etc. The string `fmt` is in packed format (see. [§6.4.2](#6.4.2)）。

* * *

#### `string.packsize (fmt)`

Returns the length of a string packed with ['string.pack'](#pdf-string.pack) in the specified format. The variable-length options ''s'' or ''z'' are not allowed in the formatted string (see. [§6.4.2](#6.4.2)）。

* * *

#### `string.rep (s, n [, sep])`

Returns a string of `n` strings `s` concatenated with the string `sep` as the delimiter. The default `sep` value is an empty string (I. e. no delimiter). Returns an empty string if `n` is not a positive number.

* * *

#### `string.reverse (s)`

Returns the flipped string of the string`s.

* * *

#### `string.sub (s, i [, j])`

Returns a substring of `s`, starting with `I` and ending with `j`; both `I` and `j` can be negative. If `j` is not given, it is assumed to be -1 (the same length as the string). In particular, the call `string.sub(s,1,j)`can return a prefix string of length `j` for `s`, while `string.sub(s, -I)` returns a suffix string of length `I.

If `I` is less than 1 after escaping the negative index, it is corrected back to 1. If `j` is greater than the length of the string, it is corrected to the length of the string. If, after the correction, `I` is greater than `j`, the function returns an empty string.

* * *

#### `string.unpack (fmt, s [, pos])`

Returns a value packaged in a string `s` (see [`string.pack`](#pdf-string.pack)) in the format `fmt` (see [§ 6.4.2](#6.4.2)). The option `pos` (default is 1) marks where to start reading from`s. After reading all the values, the function returns the position of the first unread byte in`s.

* * *

#### `string.upper (s)`

Receives a string, converts all lowercase characters to uppercase, and returns a copy. Other strings do not change. The definition of lowercase characters depends on the current locale.

#### 6.4.1 – Match mode

Lua The matching pattern in is described directly with a regular string. It is used for pattern matching functions [`string.find`](#pdf-string.find), [`string.gmatch`](#pdf-string.gmatch), [`string.gsub`](#pdf-string.gsub), [`string.match`](#pdf-string.match). This section describes the syntax and meaning of these strings (I. e., what it can match）。

##### Character Class：

_ Character class_is used to represent a collection of characters. The following combinations are available for character classes：

*   **_x_:** （Here_x_cannot be a member of the_magic character_`^$()%.[]* +-?`) represents the character_x_itself.
*   **`.`:** （A dot) can represent any character.
*   **`%a`:** Indicates any letter.
*   **`%c`:** Represents any control character.
*   **`%d`:** Represents any number.
*   **`%g`:** Represents any printable character other than a white-space character.
*   **`%l`:** Indicates all lowercase letters.
*   **`%p`:** Represents all punctuation marks.
*   **`%s`:** Represents all white space characters.
*   **`%u`:** Indicates all uppercase letters.
*   **`%w`:** Represents all letters and numbers.
*   **`%x`:** Represents all hexadecimal numeric symbols.
*   **`%_x_`:** （Here_x_is any non-alphabetic or numeric character) represents the character_x_. This is the standard method for escaping magic characters. All non-alphabetic or numeric characters (including all punctuation marks and also non-magic characters) can represent themselves by being preceded by a '%' in the pattern string.
*   **`[_set_]`:** Represents the union of all characters in_set. Characters at both ends of a range can be written in ascending order to represent a range of character sets. The `%`_x_form mentioned above can also be used in_set_to represent one of the elements. Other characters that appear in_set_represent themselves. For example, `[%w_]` (or `[_%w]`) means all alphanumerics plus underscores), `[0-7]` means octuary digits, and `[0-7% l%-]` means octuary digits plus lowercase letters and ``-`` characters.

    The behavior of cross-using classes and scopes is undefined. Therefore, pattern strings like `[%a-z]` or `[a-%%]` have no meaning.

*   **`[^_set_]`:** Represents the complement of_set_, where_set_is as explained above.

All categories represented by a single letter (`%a`,`%c`, etc.), if their letters are changed to uppercase, represent the corresponding complement. For example, `%S` represents all non-space characters.

How you define letters, spaces, or other groups of characters depends on your current locale. Special note: `[a-z]` is not necessarily equivalent `%l` 。

##### Mode Entry：

_ Mode Entries_can be

*   A single character class matches any single character in that class；
*   A single character class followed by a ''*'' will match zero or more characters of that class. This entry always matches the longest possible string；
*   A single character class followed by a '''''' will match one or more characters of that class. This entry always matches the longest possible string；
*   A single character class followed by a ``-`` will match zero or more characters of that class. Unlike ``*``, this entry always matches the shortest possible string；
*   A single character class followed by a ''?'' will match zero or one character of that class. Whenever possible, it will match a；
*   `%_n_`， The_n_here can be from 1 to 9; this entry matches a substring equal to the_n_catch (described later).
*   `%b_xy_`， Here_x_and_y_are two explicit characters; this entry matches a string that starts with_x_and ends with_y_, and where_x_and_y_remain_balanced. This means that if you read this string from left to right, every time you read a_x_, you_+1_, and when you read a_y_, you_\-1_, and the_y_at the end is the first_y_that counts to 0_. For example, the entry `% B ()` can be matched to a parenthetically balanced expression.
*   `%f[_set_]`， Refers to the_border pattern_; This entry will match an empty string before a character in_set_, and the previous character in this position does not belong to_set_. The meaning of set_set_is as described earlier. The start and end points of the matched empty string are calculated as if there is a character '\0' there.

##### Mode：

_ Patters_refers to a sequence of pattern entries. Adding the symbol ``^`` to the front of the pattern will anchor the match from the beginning of the string. Adding the symbol ``$`` at the end of the pattern will anchor the matching process to the end of the string. If ``^`` and ``$`` appear elsewhere, they have no special meaning, only themselves.

##### Capture：

A pattern can be internally bracketed with a sub-pattern; these sub-patterns are called_catch_. When the match is successful, the substring in the string matched by the_catch_is saved for future use. The catches are numbered in the order of their opening brackets. For example, for the pattern `"(a *(.)%w(%s *))"`, the part of the string matching `"a *(.)%w(%s *)"` is stored in the first catch (hence the number 1 ); the character matched by "`.`" is the number 2 catch, the part that matches "`%s *`" is number 3.

As a special case, an empty capture `()` will capture the position of the current string (it is a number). For example, if the pattern `"()aa()" "is applied to the string`" flaaap "`, two captures will be produced: 3 and 5 。

#### 6.4.2 – Format string used for packaging and unpacking

The first argument for [`string.pack`](#pdf-string.pack), [`string.packsize`](#pdf-string.packsize), [`string.unpack`](#pdf-string.unpack). It is a layout that describes the structure that needs to be created or read.

A format string is a sequence of conversion options. These conversion options are listed in：

*   **`<`:** Set to Little Ended
*   **`>`:** Set to Big Endian
*   **`=`:** Size side follows local settings
*   **`![_n_]`:** Set the maximum number of alignments to `n` (default follows local alignment settings）
*   **`b`:** A signed byte (`char`)
*   **`B`:** An unsigned byte (`char`)
*   **`h`:** A signed `short` (local size）
*   **`H`:** An unsigned `short` (local size）
*   **`l`:** A signed `long` (local size）
*   **`L`:** An unsigned `long` (local size）
*   **`j`:** One `lua_Integer`
*   **`J`:** One `lua_Unsigned`
*   **`T`:** a `size_t` (local size）
*   **`i[_n_]`:** One `n` byte long (default to local size) signed `int`
*   **`I[_n_]`:** One `n` byte long (default to local size) unsigned `int`
*   **`f`:** a `float` (local size）
*   **`d`:** a `double` (local size）
*   **`n`:** One `lua_Number`
*   **`c_n_`:** `n`Byte fixed-length string
*   **`z`:** Zero-terminated string
*   **`s[_n_]`:** The length plus content string is encoded as an unsigned integer of `n` bytes (by default, `size_t `).
*   **`x`:** A byte of padding
*   **`X_op_`:** An empty entry aligned as the option `op` (ignoring other aspects of it)
*   **'  ':** （spaces) ignore

（ "`[_n_]`" Represents an optional integer.) Except padding, spaces, configuration items (option "`xX <=>! `"), each option is associated with a parameter (for [`string.pack`](#pdf-string.pack)) or a result ( [`string.unpack`](#pdf-string.unpack)）。

For option "`! _n_`", "` s_n_`", "` I_n_`", "` I_n_`", `n` can be an integer between 1 and 16. All integer options will be checked for overflow; [`string.pack`](#pdf-string.pack) checks whether the supplied value can be expressed in the specified word length; [`string.unpack`](#pdf-string.unpack) checks whether the read value can be placed in a Lua integer.

Any format string assumes a "`! 1 = `"prefix, that is, the maximum alignment is 1 (no alignment) and the local size side setting is used.

Alignment works according to the following rules: for each option, formatting will be filled with some bytes until the data starts at a specific offset, which is a multiple of the smaller of the size and maximum alignment number of the option; This smaller value must be 2 integer powers. Options "`c`" and "`z`" do not do alignment; option "`s`" follows its beginning integer for alignment.

[`string.pack`](#pdf-string.pack) Padding with zeros ([`string.unpack`](#pdf-string.unpack) is ignored）。

### 6.5 – UTF-8 Support

This library provides basic support for UTF-8 coding. All functions are placed in the table `utf8. This library does not provide any Unicode support other than encoding processing. All operations that require understanding the meaning of characters, such as character classification, are not in this category.

Unless otherwise stated, when a function requires an argument for a byte position, it is assumed that the position is calculated either from the beginning of the byte sequence or from the position of the string length plus one. As with string libraries, negative indexes start at the end of the string.

* * *

#### `utf8.char (···)`

Receives zero or more integers, converts each integer into a corresponding sequence of UTF-8 bytes, and returns a string of these sequences concatenated together.

* * *

#### `utf8.charpattern`

A pattern (is a string, not a function) for exact matching to a UTF-8 byte sequence "`[\0-\x7F\xC2-\xF4][\x80-\xBF]*`" (see [§ 6.4.1](#6.4.1)). It assumes that the object being processed is a legal UTF-8 string.

* * *

#### `utf8.codes (s)`

Returns a series of values that allow

     for p, c in utf8.codes(s) do _body_ end

Iterate out all the characters in the string`s. Here `p` is the position (in bytes) and `c` is the number of each character. If an illegal sequence of bytes is processed, an error will be thrown.

* * *

#### `utf8.codepoint (s [, i [, j]])`

Returns the number of all characters in `s` from position `I` to `j` (both ends inclusive) as an integer. The default `I` is 1 and the default `j` is `I`. If an illegal byte sequence is encountered, an error is thrown.

* * *

#### `utf8.len (s [, i [, j]])`

Returns the number of UTF-8 characters in the string `s` from position `I` to `j` (both ends inclusive). The default `I` is 1, and the default `j` is -1. If it finds any illegal byte sequence, it returns a false value plus the position of the first illegal byte.

* * *

#### `utf8.offset (s, n [, i])`

Returns the starting position (in bytes) of the `n` character encoded in `s` (counted from position `I`). Negative `n` takes the character before position `I. The default `I` is 1 when `n` is non-negative, otherwise it defaults to `#s 1`. Therefore, `utf8.offset(s, -n)`takes the position of the last `n` character of the string. If the specified character is not in it or after the end point, the function returns **nil**。

As a special case, when `n` is equal to 0, this function returns the starting position of the character containing the `I` byte of`s.

This function assumes that `s` is a legal UTF-8 string.

### 6.6 – Table Processing

This library provides generic functions for table processing. All functions are placed in the table `table.

Remember that whenever an operation needs to take the length of a table, the table must be a true sequence or have a `__len` meta method (see [§ 3.4.7](#3.4.7) ). All functions ignore the non-numeric keys in the table of incoming arguments.

* * *

#### `table.concat (list [, sep [, i [, j]]])`

Provide a list whose elements are all strings or numbers, and return the string `list [I] .. sep .. list [I 1] · · · sep .. list[j]`. The default value of `sep` is an empty string, the default value of `I` is 1, and the default value of `j` is `#list`. If `I` is greater than `j`, an empty string is returned.

* * *

#### `table.insert (list, [pos,] value)`

Insert the element`value` at the position `pos` of`list`, and move the element`list [pos], list[pos 1],..., list[#list]`backward`. The default value of `pos` is `#list 1`, so calling `table.insert(t,x)`will insert `x` at the end of the list`t.

* * *

#### `table.move (a1, f, e, t [,a2])`

Move element from table `a1` to table `a2 `. This function does the equivalent of the following multiple assignment: `a2[t],... = a1[f],..., a1[e]`. The default value for `a2` is `a1 `. The target interval may overlap with the source interval. Index `f` must be a positive number.

* * *

#### `table.pack (···)`

Returns a new table populated with all parameters with keys 1,2, etc., and sets the field "`n`" to the total number of parameters. Note that the returned table is not necessarily a sequence.

* * *

#### `table.remove (list [, pos])`

Removes the element at the `pos` position in `list` and returns the removed value. When `pos` is an integer between 1 and `#list`, it moves the elements `list[pos 1], list[pos 2],..., list[#list]` forward and deletes the element `list[#list]`; the index `pos` can be`#list 1 `, or 0 when`#list `is 0; in these cases, the function deletes the element `list[pos]`。

`pos` The default is `#list`, so calling `table.remove(l)` will remove the last element of table `l.

* * *

#### `table.sort (list [, comp])`

Sorts elements from `list[1]`to `list[#list]` within a table in a specified order. If `comp` is provided, it must be a function that can receive two list elements as parameters. When the first element needs to be ranked before the second element, it returns true (so `not comp(list [I 1],list [I])`will be true after sorting). If `comp` is not provided, the standard Lua operation `<` will be used as an alternative.

The sorting algorithm is not stable; that is, when two elements are in equal order, their relative positions after sorting may change.

* * *

#### `table.unpack (list [, i [, j]])`

Returns the elements in a list. This function is equivalent

     return list\[i\], list\[i+1\], ···, list\[j\]

`i` Default is 1 ,`j` defaults `#list`。

### 6.7 – mathematical function

This library provides basic math functions. So the functions are all placed in the table `math. Functions annotated with "`integer/float`" return integer results for integer arguments and floating-point results for floating-point (or mixed) arguments. The Circle Entire functions ([`math.ceil`](#pdf-math.ceil), [`math.floor`](#pdf-math.floor), [`math.modf`](#pdf-math.modf)) return an integer if the result is within an integer range, otherwise it returns a floating point number.

* * *

#### `math.abs (x)`

Returns the absolute value of `x`。(integer/float)

* * *

#### `math.acos (x)`

Returns the inverse cosine of `x` (in radians）。

* * *

#### `math.asin (x)`

Returns the arcsine of `x` (in radians）。

* * *

#### `math.atan (y [, x])`

Returns the arctangent of `y/x` in radians. It uses the sign of the two arguments to find which quadrant the result falls in. (Even if `x` is zero, it can be handled correctly。）

The default `x` is 1, so calling `math.atan(y)`will return the arctangent of `y.

* * *

#### `math.ceil (x)`

Returns the smallest integer value not less than `x.

* * *

#### `math.cos (x)`

Returns the cosine of `x` (assuming the argument is radians）。

* * *

#### `math.deg (x)`

Converts angle `x` from radians to angles.

* * *

#### `math.exp (x)`

Returns the value of_ex_(`e` is the base of the natural logarithm.）。

* * *

#### `math.floor (x)`

Returns the largest integer value not greater than `x.

* * *

#### `math.fmod (x, y)`

Returns the remainder after dividing `x` by `y` and rounding the quotient to zero。 (integer/float)

* * *

#### `math.huge`

Floating-point number HUGE_VAL, this number is greater than any numeric value.

* * *

#### `math.log (x [, base])`

Returns the logarithm of `x` at the specified base. The default `base` is_e_ (so this function returns the natural logarithm of `x`）。

* * *

#### `math.max (x, ···)`

Returns the largest value in the parameter, the size is determined by the Lua operation `<`。 (integer/float)

* * *

#### `math.maxinteger`

The integer of the maximum value.

* * *

#### `math.min (x, ···)`

Returns the smallest value in the parameter, the size is determined by the Lua operation `<`。 (integer/float)

* * *

#### `math.mininteger`

Integer of the minimum value.

* * *

#### `math.modf (x)`

Returns the integer and fractional parts of `x. The second result must be a floating point number.

* * *

#### `math.pi`

_π_ The value.

* * *

#### `math.rad (x)`

Converts angle `x` from angles to radians.

* * *

#### `math.random ([m [, n]])`

When called without arguments, returns a uniformly distributed floating-point pseudo-random number in the interval_\[0,1). When called with two integers `m` and `n`, `math.random` returns a uniformly distributed integer pseudo-random number in the interval_\[m, n\]. (The value_n-m_cannot be negative and must be within the representation of a Lua integer.) Calling `math.random(n)`is equivalent `math.random(1,n)`。

This function is an encapsulation of the bit random number function provided by C. There is no guarantee of its statistical properties.

* * *

#### `math.randomseed (x)`

Set `x` as the "seed" of the pseudo-random number generator: the same seed produces the same random number sequence.

* * *

#### `math.sin (x)`

Returns the sine of `x` (assuming the argument is radians）。

* * *

#### `math.sqrt (x)`

Returns the square root of `x. (You can also use the power `x ^ 0.5 `to calculate this value。）

* * *

#### `math.tan (x)`

Returns the tangent of `x` (assuming the argument is radian）。

* * *

#### `math.tointeger (x)`

If `x` can be converted to an integer, return that integer. Otherwise return **nil**。

* * *

#### `math.type (x)`

Returns "`integer`" if `x` is an integer, "`float`" if it is a floating point, and "`float`" if `x` is not a number **nil**。

* * *

#### `math.ult (m, n)`

If the integers `m` and `n` are compared as unsigned integers, and `m` is below `n`, return Boolean true; otherwise return false.

### 6.8 – input and output library

I/O The library provides two different styles of file handling interfaces. The first style uses implicit file handles; it provides operations to set default input files and default output files, and all input and output operations are against these default files. The second style uses explicit file handles.

When implicit file handles are used, all operations are provided by the table `io. If an explicit file handle is used, [`io.open`](#pdf-io.open) returns a file handle, and all operations are provided by the file handle`s methods.

The table `io` also provides three predefined file handles with the same meaning as in C: `io.stdin`, `io.stdout`, and `io.stderr `. The I/O library never closes these files.

Unless otherwise noted, I/O functions return **nil** when an error occurs (the second return value is the error message, and the third return value is the system-related error code). A value different from **nil** is returned on success. On non-POSIX systems, the process of fetching error messages based on error codes may not be thread-safe because it uses C's global variables `errno` 。

* * *

#### `io.close ([file])`

Equivalent to `file:close()`. The default output file is closed when `file` is not given.

* * *

#### `io.flush ()`

equivalent `io.output():flush()`。

* * *

#### `io.input ([file])`

When it is called with a file name, it opens the file of that name (in text mode) and sets the file handle as the default input file. If you call it with a file handle, simply set the handle to the default input file. If called without passing parameters, it returns the current default input file.

In the case of an error, the function throws an error instead of returning an error code.

* * *

#### `io.lines ([filename ···])`

Opens the specified file name in read mode and returns an iterated function. This iterator function works in the same way as the iterator obtained by calling `file:lines (...) `with an open file. When the iterator function detects the end of the file, it returns no value (let the loop end) and automatically closes the file.

Calling `io.lines()`(without passing a file name) is equivalent to `io.input():lines("* l")`; that is, it will iterate the standard input file by line. In this case, it does not close the file after the loop ends.

In the case of an error, the function throws an error instead of returning an error code.

* * *

#### `io.open (filename [, mode])`

This function opens a file in the mode specified by the string `mode. Returns the new file handle. When an error occurs, returns **nil** plus an error message.

`mode` The string can be any of the following values：

*   **"`r`":** Read Mode (Default）；
*   **"`w`":** Write Mode；
*   **"`a`":** Append mode；
*   **"`r+`":** Update mode, all previous data is retained；
*   **"`w+`":** Update mode, all previous data is deleted；
*   **"`a+`":** In the append update mode, all previous data is retained and only writes are allowed at the end of the file.

`mode` The string can be appended with a ``B`` at the end, which opens the file in binary on some systems.

* * *

#### `io.output ([file])`

Similar to [`io.input`](#pdf-io.input). However, both operate on the default output file.

* * *

#### `io.popen (prog [, mode])`

This function is related to the system and is not available on all platforms.

Start the program `prog` with a separate process, and the returned file handle can be used to read data from the program (if `mode` is `"r"`, which is the default) or write input to the program (when `mode` is `"w"`）。

* * *

#### `io.read (···)`

equivalent `io.input():read(···)`。

* * *

#### `io.tmpfile ()`

If successful, returns a handle to the temporary file. This file is opened in update mode and is automatically deleted at the end of the program.

* * *

#### `io.type (obj)`

Check if `obj` is a valid file handle. If `obj` it is an open file handle, return the string `"file"`. If `obj` is a closed file handle, return the string `"closed file"`. If `obj` is not a file handle, return **nil** 。

* * *

#### `io.write (···)`

equivalent `io.output():write(···)`。

* * *

#### `file:close ()`

Close `file `. Note that the file is automatically closed when the handle is garbage collected, but how long after that happens is unpredictable.

When closing a file handle created with [`io. pop`](#pdf-io.popen), [`file:close`](#pdf-file:close) returns the same value that [`OS. execute`](#pdf-os.execute) would return.

* * *

#### `file:flush ()`

Save the written data to `file.

* * *

#### `file:lines (···)`

Returns an iterator function that reads data from a file in the specified format each time the iterator is called. If no format is specified, the default value "`l`" is used `". look at an example

     for c in file:lines(1) do _body_ end

The characters are continuously read out from the current position in the file. Unlike [`io.lines`](#pdf-io.lines), this function does not close the file after the loop ends.

In the case of an error, the function throws an error instead of returning an error code.

* * *

#### `file:read (···)`

Read the file `file`, the specified format determines what to read. For each format, the function returns the string or number corresponding to the read character. **nil** is returned if the data cannot be read in this format * *. (For this last case, the function does not read the subsequent format.) When called without passing the format, it reads the next line using the default format (see description below.）。

The formats provided are

*   **"`n`":** Reads a number, depending on Lua`s conversion grammar, may return a floating point number or an integer. (Numbers can have leading or trailing spaces, as well as symbols.) As long as it can form a legal number, this format always reads as long as possible. If the read prefix cannot form a legal number (such as an empty string, "`0x`" or "` 3.4e-`"), the function will be aborted and returned **nil**。
*   **"`a`":** Reads the entire file from the current location. Returns an empty string if it is already at the end of the file.
*   **"`l`":** Reads a row and ignores the end-of-line tag. When at the end of the file, returns **nil** which is the default format.
*   **"`L`":** Reads a line and retains the end-of-line tag (if any), when at the end of the file, returns **nil**。
*   **_number_:** Reads a string that does not exceed this number of bytes. When at the end of the file, returns **nil * *. If `number` is zero, it reads nothing and returns an empty string. When at the end of the file, returns **nil**。

Formats "`l`" and "`L`" can only be used in text files.

* * *

#### `file:seek ([whence [, offset]])`

Sets and gets the position calculated based on the beginning of the file. The location of the setting is determined by the base point specified by the `offset` and `whence` strings `whence. The base point can be：

*   **"`set`":** Base point is 0 (beginning of document）；
*   **"`cur`":** Base point is current position；
*   **"`end`":** Base point is end of document；

When `seek` succeeds, returns the position of the file finally calculated from the beginning of the file. When `seek` fails, return **nil** plus an error description string.

`whence` The default value of is `"cur"` and `offset` defaults to 0. Therefore, calling `file:seek()`returns the current location of the file without changing it; calling `file:seek("set")` sets the location to the beginning of the file (and returns 0); calling `file:seek("end") sets the location to the end of the file and returns the file size.

* * *

#### `file:setvbuf (mode [, size])`

Sets the buffer mode for the output file. There are three modes：

*   **"`no`":** Not buffered; the output operation takes effect immediately.
*   **"`full`":** Full buffering; output is only really done when the cache is full or when you explicitly call `flush` on the file (see [`io.flush`](#pdf-io.flush)).
*   **"`line`":** Line buffering; output will be buffered before each newline, and for some special files (such as terminal devices) before any input.

For the latter two cases, `size` specifies the buffer size in bytes. There will be an appropriate size by default.

* * *

#### `file:write (···)`

Write the values of the parameters to `file` one by one `. Argument must be a string or a number.

On success, the function returns `file `. Otherwise, **nil** plus the error description string is returned.

### 6.9 – Operating System Library

This library is implemented through the table `OS.

* * *

#### `os.clock ()`

Returns an approximation of the CPU time in seconds used by the program.

* * *

#### `os.date ([format [, time]])`

Returns a string or table containing the date and time of day. The formatting method depends on the given string `format`。

If the `time` parameter is provided, the time is formatted (see the [`OS. time`](#pdf-os.time) function for the meaning of this value). Otherwise, `date` formats the current time.

If 'format' ''! ''At the beginning, the date is formatted in Coordinated Universal Time. After this optional character item, if' format' is the string "'* t'", 'date' returns a table with subsequent fields: 'year' (four digits), 'month' (1-12),' day' (1-31), 'hour' (0-23),' min' (0-59), 'sec' (0-61),' wday' (day of the week, Sunday of the year), and 'isdst' (daylight saving time marker, a boolean). For the last field, it does not exist if the information is not provided.

If `format` is not "`* t`", `date` is returned as a string, and the formatting method follows the rules of the ISO C function `strftime.

If called without parameters, `date` returns a reasonable date-time string in a format that depends on the host program and the current locale (I. e., `OS. date()` is equivalent `os.date("%c")`）。

On non-POSIX systems, this function may not be thread-safe because it relies on the C functions `gmtime` and `localtime.

* * *

#### `os.difftime (t2, t1)`

Returns the difference in seconds from time `t1` to `t2. (Here the time is the value returned by [`OS. time`](#pdf-os.time)). On POSIX,Windows, and some other systems, this value is equal `t2`_\-_`t1`。

* * *

#### `os.execute ([command])`

This function is equivalent to the ISO C function `system `. It calls the system interpreter to execute `command `. If the command runs successfully, the first return value is **true**, otherwise it is **nil * *. After the first return value, the function returns a string plus a number. as follows：

*   **"`exit`":** The command ends normally; the next number is the exit status code for the command.
*   **"`signal`":** The command is interrupted by a signal; the next number is the signal that interrupts the command.

If called without arguments, `OS .exe cute` returns true if the system interpreter exists.

* * *

#### `os.exit ([code [, close]])`

Call the ISO C function `exit` to terminate the host program. If `code` is **true**, the returned status code is EXIT_SUCCESS. If `code` is **false**, the returned status code is EXIT_FAILURE. If `code` is a number, the returned status code is the number. The default value for `code` is **true**。

If the second optional argument `close` is true, close the Lua state machine before exiting.

* * *

#### `os.getenv (varname)`

Returns the value of the process environment variable `varname`, if the variable is not defined **nil** 。

* * *

#### `os.remove (filename)`

Delete the file with the specified name (it can be an empty directory on POSIX systems). If the function fails, return **nil** plus an error description string and error code.

* * *

#### `os.rename (oldname, newname)`

Renames the file or directory named `oldname` to `newname `. If the function fails, return **nil** plus an error description string and error code.

* * *

#### `os.setlocale (locale [, category])`

Sets the current region of the program. `locale` is a locale-dependent system string; `category` is an optional string that describes which classification has changed: `"all"`, `"collate"`, `"ctype"`, `"monetary"`, `"numeric"`, or `"time"`; the default classification is `"all"`. This function returns the name of the new region. If the request is not granted, return **nil** 。

When `locale` is an empty string, the current region is set to a local region defined in the implementation. When `locale` is the string "`C`", the current area is set to the standard C area.

When the first parameter is **nil**, this function returns only the name of the specified category in the current region.

Since this function relies on the C function `setlocale`, it may not be thread-safe.

* * *

#### `os.time ([table])`

When the parameter is not passed, the current time is returned. If a table is passed in, the time represented by this table is returned. The table must contain the fields `year`,`month`, and `day`; it can contain `hour` (default 12 ), `min` (default 0), `sec` (default 0), and `isdst` (default **nil**). For a detailed description of these fields, see the [`OS. date`](#pdf-os.date) function.

The return value is a number whose meaning is determined by your system. In POSIX,Windows, and some other systems, this number counts the number of seconds that have elapsed since a specified time ("epoch"). For other systems, the meaning is undefined, and you can only use the return number of `time` for the parameters of [`OS. date`](#pdf-os.date) and [` OS. difftime`](#pdf-os.difftime).

* * *

#### `os.tmpname ()`

Returns a filename string that can be used for a temporary file. This file must be explicitly opened before use and explicitly deleted when no longer in use.

On POSIX systems, this function creates a file with this file name to avoid security risks. (Someone else may have created this file without permission at the time between getting the file name and creating the file.) You still need to open it first and delete it at last (even if you don't use it）。

Only possibly, you should use [`io.tmpfile`](#pdf-io.tmpfile) because the file can be automatically deleted at the end of the program.

### 6.10 – Debug Library

This library provides the functionality of the Lua program debugging interface ([§ 4.9](#4.9)). Some of these functions violate the basic assumptions of Lua code (for example, local variables of functions are not accessed from outside the function; meta tables of user data are not modified by Lua code; Lua programs do not crash), so they may compromise the security of other code. In addition, some functions in the library may run very slowly.

All functions in this library are provided in the table `debug. For all functions that operate on threads, the optional first parameter is for the thread. The default value is always the current thread.

* * *

#### `debug.debug ()`

Enter a user interaction mode and run each string entered by the user. Using simple commands and other debugging settings, users can review global and local variables, change the value of variables, evaluate some expressions, and so on. Entering a line containing only `cont` will end the function so that the caller can continue to run down.

Note that the command entered by `debug.debug` is not embedded in any function in the grammar, so it cannot directly access local variables.

* * *

#### `debug.gethook ([thread])`

Returns three values representing thread hook settings: the current hook function, the current hook mask, and those set by the current hook count ([`debug.sethook`](#pdf-debug.sethook)）。

* * *

#### `debug.getinfo ([thread,] f [, what])`

Returns a table of information about a function. You can provide the function directly, or you can use a number `f` to represent the function. The number `f` represents the function that runs on the corresponding level of the call stack of the specified thread: level 0 represents the current function (`getinfo` itself); level 1 represents the function that calls `getinfo` (unless it is a tail call, which is not included in the stack); and so on. If `f` is a number larger than the number of active functions, `getinfo` returns **nil**。

Only if the string 'what' has a description of which items to populate, the returned table can contain all the items that [lua_getinfo](#lua_getinfo) can return. By default, 'what' returns all the information provided except the table of legal row numbers. For option ''f'', the 'func' field is added to save the function itself, if possible. For option ''L'', the 'activelines' field is added to hold the table of legal line numbers, if possible.

For example, the expression `debug.getinfo(1,"n")`returns a table with information about the current function name (if the name can be found), and the expression `debug.getinfo(print)` returns a table with information about the [`print`](#pdf-print) function.

* * *

#### `debug.getlocal ([thread,] f, local)`

This function returns the name and value of the function`s local variable with index `local` at the `f` level of the stack. This function is not only used to access explicitly defined local variables, but also includes formal parameters, temporary variables, etc.

The index of the first parameter or the first local variable defined is 1, and then follows the order defined in the code, and so on. where only the active variables of the current scope of the function are evaluated. Negative index refers to the variable parameter; -1 refers to the first variable parameter. If there is no variable at that index, the function returns **nil * *. If the specified level is out of bounds, an error is thrown. (You can call [`debug.getinfo`](#pdf-debug.getinfo) to check if the hierarchy is legal。）

Variable names beginning with '''' ((open brackets) represent variables without names (e. g. control variables used in loop control, or code blocks with debugging information removed)）。

The parameter `f` can also be a function. In this case, `getlocal` returns only the name of the function parameter.

* * *

#### `debug.getmetatable (value)`

Returns the meta table for the given `value. Returns if it does not have a meta table **nil** 。

* * *

#### `debug.getregistry ()`

return to the registry (see [§4.5](#4.5)）。

* * *

#### `debug.getupvalue (f, up)`

This function returns the name and value of the `up` top value of function `f. If the function does not have that upper value, return **nil** 。

Variable names beginning with '''' ((open brackets) represent variables without names (code blocks with debug information removed)）。

* * *

#### `debug.getuservalue (u)`

Returns the Lua value associated on `u. If `u` is not user data, return **nil**。

* * *

#### `debug.sethook ([thread,] hook, mask [, count])`

Set a function as a hook function. The string `mask` and the number `count` determine when the hook will be called. A mask is a string of the following characters, each of which has a meaning：

*   **'`c`':** Whenever Lua calls a function, the hook is called；
*   **'`r`':** Whenever Lua returns from within a function, the hook is called；
*   **'`l`':** Whenever Lua enters a new line, the hook is called.

In addition, a non-zero `count` is passed in, and the hook will be called every time the `count` instruction is run.

If no parameters are passed in, [`debug.sethook`](#pdf-debug.sethook) closes the hook.

When the hook is called, the first parameter is the event that triggered the call: `"call"` (or `"tail call"`), `"return"`, `"line"`, `"count"`. For row events, the second argument to the hook is the new row number. Within the hook, you can call` getinfo` and specify layer 2 to get the details of the running function (layer 0 refers to` getinfo` function and layer 1 refers to hook function）。

* * *

#### `debug.setlocal ([thread,] level, local, value)`

This function assigns `value` to the `local` local variable of the `level` level function on the stack. If there is no such variable, the function returns **nil * *. If `level` is out of bounds, an error is thrown. (You can call [`debug.getinfo`](#pdf-debug.getinfo) to check if the hierarchy is legal.) Otherwise, it returns the name of the local variable.

For variable indexes and names, see [`debug.getlocal`](#pdf-debug.getlocal)。

* * *

#### `debug.setmetatable (value, table)`

Set the meta table for `value` to `table` (can be **nil**). Return `value`。

* * *

#### `debug.setupvalue (f, up, value)`

This function sets `value` to the `up` top value of function `f. If the function does not have that upper value, return **nil** otherwise, return the name of the upper value.

* * *

#### `debug.setuservalue (udata, value)`

Set `value` to the associated value of `udata. `udata` must be a full user data.

Return `udata`。

* * *

#### `debug.traceback ([thread,] [message [, level]])`

If `message` is present and is not a string or **nil**, the function returns `message` without any processing `. Otherwise, it returns stack backtrace information for the call stack. The string option `message` is added at the beginning of the stack backtrace information. The number option`level` indicates from which level of the stack to backtrack (the default is 1, where `traceback` is called）。

* * *

#### `debug.upvalueid (f, n)`

Returns the unique identifier of the `n` upper value of the specified function (a lightweight user data）。

This unique identifier allows the program to check whether two different closures share the upper value. If Lua closures share the same up value (that is, point to an external local variable), the same identifier is returned.

* * *

#### `debug.upvaluejoin (f1, n1, f2, n2)`

Let the `n1` upper value of Lua closure `f1` refer to the `n2` upper value of Lua closure `f2.

## 7 – Independent Edition Lua

Although Lua is designed to be an extension language for embedding a host program. It is often used as a separate language. The stand-alone version of the Lua language interpreter is released with the standard package and is called `lua `. The standalone version of the interpreter retains all of the standard and debug libraries. Its command line usage is：

     lua \[options\] \[script \[args\]\]

The options are：

*   **`-e _stat_`:** Execute a string _stat_ ；
*   **`-l _mod_`:** “Request Module” _mod_ ；
*   **`-i`:** Enter interactive mode after running_script_；
*   **`-v`:** Print version information；
*   **`-E`:** Ignore environment variables；
*   **`--`:** Abort processing of subsequent options；
*   **`-`:** Run `stdin` as a file and abort processing of the following options.

After processing the options, `lua` runs the specified_script_. If called without parameters, `lua` behaves the same as `lua -v-I `when the standard input (`stdin`) is a terminal. otherwise equivalent `lua -` 。

If the call is made without the `-E` option, the interpreter checks the environment variable` LUA_INIT_5_3 `(or` LUA_INIT `if the version name is undefined) before running any parameters. If the variable memory format is `@_filename_`, `lua` executes the file. Otherwise, `lua` executes the string.

If there is an option `-E` in the call, Lua ignores the values of` LUA_PATH `and` LUA_CPATH `in addition to ignoring` LUA_INIT. Set the values of [`package.path`](#pdf-package.path) and [`package.cpath`](#pdf-package.cpath) to the default path defined in `luaconf.h.

All options except `-I` and `-E` are processed in order. For example, this call

     $ lua -e'a=1' -e 'print(a)' script.lua

You will first set `a` to 1, then print the value of `a`, and finally run the file `script.lua` without parameters. (The `$` here is the command line prompt. Your command line prompt may not be the same。）

Before running any code, `lua` will put all the parameters passed in from the command line into a global table, `arg. The name of the script is placed at index 0, the first parameter immediately after the script name is at index 1, and so on. Any parameters that precede the script name (I. e., the name of the interpreter and options) are placed at the negative index. For example, calling

     $ lua -la b.lua t1 t2

This table is like this：

     arg = { \[-2\] = "lua", \[-1\] = "-la",
             \[0\] = "b.lua",
             \[1\] = "t1", \[2\] = "t2" }

If the script name is not provided in the call, the interpreter name is placed at index 0, followed by other parameters. For example, calling

     $ lua -e "print(arg\[1\])"

"`-e`" will be printed out` ". If a script name is provided, the script is called with `arg[1]`,..., `arg[#arg]` as arguments. (Like all Lua code blocks, the script is compiled into a variable parameter function.。）

In interactive mode, Lua continuously displays the prompt and waits for the next line of input. Once you read a line, first try to interpret the line as an expression. If successfully interpreted, the value of the expression is printed. Otherwise, the line is interpreted as a statement. If you write an unfinished line, the interpreter will use a different prompt to wait for you to finish.

When an unprotected error occurs in a script, the interpreter reports the error to the standard error stream. If the error object is not a string, but there is a meta method `__tostring`, the interpreter will call this meta method to generate the final message. Otherwise, the interpreter converts the error object to a string and prepends the stack backtrace information.

If the run ends normally, the interpreter shuts down the main Lua state machine (see [lua_close](#lua_close)). The script can be ended by calling [`OS. exit`](#pdf-os.exit) to circumvent this step.

To make Lua a script interpreter for Unix systems. The standalone interpreter ignores the first line of the code block that is headed. Therefore, Lua scripts can be passed through `chmod x` as well `#! `Form becomes an executable file. something like this

     #!/usr/local/bin/lua

（Of course, the location of the Lua interpreter may not be the same for your machine. If `lua` is in your `PATH`, write

     #!/usr/bin/env lua

More generic。）

## 8 – Incompatibility with previous versions

Here we list the incompatibilities that you will encounter when migrating a program from Lua 5.2 to Lua 5.3. You can circumvent some incompatibilities by defining appropriate options when compiling Lua (see the file `luaconf.h`). However, these compatibility options will be removed later.

Lua A version change always modifies some C API and involves changes to the source code. For example, some constant numeric values, using macros to implement some functions. Therefore, you cannot assume binary compatibility between different versions of Lua. When you use the new version, be sure to recompile the client program that uses the Lua API.

Similarly, Lua versioning changes the internal rendering of precompiled code blocks; precompiled code blocks are not compatible between different Lua versions.

The standard path to the official release may also change depending on the version.

### 8.1 – Change of language

*   Lua 5.2 The biggest change to Lua 5.3 is the introduction of integer subtypes for numbers. Although this change does not affect "general" calculations, some calculations (mainly involving overflow) will get different results.

    You can eliminate the difference by forcing all numbers to float (in Lua 5.2, all numbers are floats). For example, you can make all constants end in `.0`, or use `x = x 0.0 `to convert a variable. (This advice is only used to quickly resolve incompatibilities occasionally; it`s not a good programming guideline. If you write the program well, you should use floating points where you need them and integers where you need them.。）

*   Where floating-point numbers are converted into strings, floating-point numbers equal to integers are now suffixed with `.0. (For example, floating point numbers 2.0 be printed as `2.0 `instead of `2`.) If you need to customize the format of numbers, you must explicitly format them.

    （To be precise, this is not a compatibility issue, because Lua does not specify how numbers are formatted into strings, but some programs assume that they follow a particular format.。）

*   The generational garbage collector is gone. (It is an experimental feature in Lua 5.2。）

### 8.2 – Changes to the library

*   `bit32` The library is abandoned. It`s easy to use an external compatibility library, but it`s best to replace it directly with the corresponding bit operator. (Note that `bit32` can only be operated on 32-bit integers, while the bit operations in standard Lua can be used on 64-bit integers。）
*   The table processing library now considers meta methods when reading and writing elements within them.
*   [`ipairs`](#pdf-ipairs) This iterator will also consider the meta-method, and the `__ipairs` meta-method is discarded.
*   [`io.read`](#pdf-io.read) The option name of is no longer first. However, for compatibility reasons, Lua will continue to ignore this character.
*   These functions in the math library are obsolete: `atan2`, `cosh`, `sinh`, `tanh`, `pow`, `frexp`, and `ldexp `. You can replace `math.pow(x,y)`with `x ^ y`; you can replace `math.atan2` with `math.atan`, which can now take one or two arguments; you can replace `math.ldexp(x,exp)` with `x * 2.0 ^ exp `. If you use other operations, you can write an extension library or implement them in Lua.
*   [`require`](#pdf-require) The way version numbers are handled when searching for the C loader has changed. Now, the version number should follow the module name (as most other tools do). For compatibility reasons, if the loader cannot be found using the new format, the searcher will still try the old format. (Lua 5.2 has already handled it this way, but it is not written in the document。）

### 8.3 – API The change

*   The continuation function now receives the parameters originally obtained with the lua_getctx, so the lua_getctx is removed. Rewrite your code as needed.
*   The function [lua_dump](#lua_dump) has an extra parameter `strip `. If you want to be consistent with the previous behavior, this value passes 0 。
*   Functions for passing in and out of unsigned integers (lua_pushunsigned, lua_tounsigned, lua_tounsignedx, luaL_checkunsigned, luaL_optunsigned) are deprecated. Do type conversions directly from the signed version.
*   Macros that handle input of non-default integer types (luaL_checkint, luaL_optint, luaL_checklong, luaL_optlong) are deprecated. Use [`lua_Integer`](#lua_Integer) directly plus a type conversion to replace (or use it in your code whenever possible [`lua_Integer`](#lua_Integer)）。

## 9 – Lua The full syntax

This is the complete syntax of Lua using the extended BNF description. In an extended BNF, {A} represents zero or more A's, and \[A\] represents an optional A. (Operator precedence, see [§ 3.4.8](#3.4.8); for an explanation of final symbols, names, numbers, string literals, see [§3.1](#3.1)。）

	chunk ::= block

	block ::= {stat} \[retstat\]

	stat ::=  ‘**;**’ |
		 varlist ‘**\=**’ explist |
		 functioncall |
		 label |
		 **break** |
		 **goto** Name |
		 **do** block **end** |
		 **while** exp **do** block **end** |
		 **repeat** block **until** exp |
		 **if** exp **then** block {**elseif** exp **then** block} \[**else** block\] **end** |
		 **for** Name ‘**\=**’ exp ‘**,**’ exp \[‘**,**’ exp\] **do** block **end** |
		 **for** namelist **in** explist **do** block **end** |
		 **function** funcname funcbody |
		 **local** **function** Name funcbody |
		 **local** namelist \[‘**\=**’ explist\]

	retstat ::= **return** \[explist\] \[‘**;**’\]

	label ::= ‘**::**’ Name ‘**::**’

	funcname ::= Name {‘**.**’ Name} \[‘**:**’ Name\]

	varlist ::= var {‘**,**’ var}

	var ::=  Name | prefixexp ‘**\[**’ exp ‘**\]**’ | prefixexp ‘**.**’ Name

	namelist ::= Name {‘**,**’ Name}

	explist ::= exp {‘**,**’ exp}

	exp ::=  **nil** | **false** | **true** | Numeral | LiteralString | ‘**...**’ | functiondef |
		 prefixexp | tableconstructor | exp binop exp | unop exp

	prefixexp ::= var | functioncall | ‘**(**’ exp ‘**)**’

	functioncall ::=  prefixexp args | prefixexp ‘**:**’ Name args

	args ::=  ‘**(**’ \[explist\] ‘**)**’ | tableconstructor | LiteralString

	functiondef ::= **function** funcbody

	funcbody ::= ‘**(**’ \[parlist\] ‘**)**’ block **end**

	parlist ::= namelist \[‘**,**’ ‘**...**’\] | ‘**...**’

	tableconstructor ::= ‘**{**’ \[fieldlist\] ‘**}**’

	fieldlist ::= field {fieldsep field} \[fieldsep\]

	field ::= ‘**\[**’ exp ‘**\]**’ ‘**\=**’ exp | Name ‘**\=**’ exp | exp

	fieldsep ::= ‘**,**’ | ‘**;**’

	binop ::=  ‘**+**’ | ‘**\-**’ | ‘**\***’ | ‘**/**’ | ‘**//**’ | ‘**^**’ | ‘**%**’ |
		 ‘**&**’ | ‘**~**’ | ‘**|**’ | ‘**\>>**’ | ‘**<<**’ | ‘**..**’ |
		 ‘**<**’ | ‘**<=**’ | ‘**\>**’ | ‘**\>=**’ | ‘**\==**’ | ‘**~=**’ |
		 **and** | **or**

	unop ::= ‘**\-**’ | **not** | ‘**#**’ | ‘**~**’
