# Basic Grammar

## Video Tutorial

<div class="admonition">
    <p class="title">Lua Quick Start</p>
    <iframe id="spjc" src="//player.bilibili.com/player.html?aid=291542005&bvid=BV1vf4y1L7Rb&cid=368202399&page=1" scrolling="no" border="0" frameborder="no" framespacing="0" allowfullscreen="true" width="100%"> </iframe>
    <script type="text/javascript">
    document.getElementById("spjc").style.height=document.getElementById("spjc").scrollWidth*0.76+"px";
    </script>
</div>

> After learning the introductory tutorial, you can continue to learn [advanced tutorial](https://www.bilibili.com/video/BV1WR4y1E7ud)

## Text version tutorial

### First understanding Lua

Lua It is a lightweight and compact scripting language, which is written in standard C language and is open in source code form. This means that the Lua virtual machine can be easily embedded in other programs, providing flexible extension and customization capabilities for applications. However, the entire Lua virtual machine is only over 100 K after compilation, which can be smaller after proper tailoring, which is very suitable for embedded development.

At the same time, in the current script engine, Lua's running speed has an absolute advantage. These all determine that Lua is the best choice for embedded scripts.

Before we write the code, we need to make some preparations, that is: * * make sure you are not using Chinese input method when entering punctuation marks。**

--------

### The first Lua program

The first line of code in almost all languages is the output `hello world`, and this tutorial is not surprising.

In `Lua`, you only need to use the `print` function to print the result. At the same time, if you need to use `function`, just add double parentheses after the function name and pass in the value you want to pass in.

So, let`s execute the following code and print` hello world`！

```lua
print("hello world!")
```

--------

### Output data

In the last part, we learned that in `Lua`, you can use the `print` function to print the result you want.

It is also known that `function` refers to a subroutine that can implement certain functions and can be executed using the `function name (parameter).

Let's try to output something else! Use multiple print functions to output the data you want to output.

```lua
print("Test")
print("aabcdefg")
print("xxxxx","second parameter "," third parameter")
```

--------

### Code Comments

Code "comments" are the parts of the code that will not run. `Annotation` will not be run at all.

This is partly for better immediate meaning of existing code when viewing it.

We can start with `--` and write a one-line comment.`

You can also start with `--[[`, `]]` and end with a multi-line comment.

Below is an example of a comment：

```lua
print("This code will run")
--print("I was commented out, so it won't run.")
--[[
    I am a multi-line comment
    No matter how many lines I write
    will not affect the code running
]]
```

--------

## Variable

### number Variable

`Variable`, can be regarded as a bucket, in which you want to put the content. These can be all legal types that Lua contains.

For example, I want to create a new bucket named bucket and put the number 233 in it, which can be as follows：

```lua
bucket = 233
```

Let's try to create a few variables ourselves.！

- Create a new variable year and set the value of the variable 1926
- Create a new variable month and set the value of the variable 8
- Create a new variable day and set the value of the variable 7

```lua
--Create three new variables and assign values
year = 1926
month = 8
day = 7
--Print out the values of the three variables
print(year,month,day)
```

--------

### Understanding nil

`nil`The type indicates that there is no valid value, as long as it is an undeclared value, it is.`nil`

For example, if I print a value without a declaration, it will output`nil`：

```lua
ccc = 233
print(ccc)
print(aaa)
```

**Here you need to think about what will be output by running the following code.？**

```lua
a = 1
b = '2'
c = a
print(a,b,c,d)
```

--------

### assignment statement

`Assignment` is the most basic way to change the value of a variable.

As below, use the `equal sign` to assign the `variable` on the `left`

```lua
n = 2
n = 3
n = n + 1
b = n
```

`Lua`You can assign values to multiple variables at the same time. The variables are separated by `commas`. The value on the right side of the `assignment` statement will be assigned to the variable on the left side in turn.`。

```lua
n = 1
a, b = 10, 2*n
```

When the number of left and right values is inconsistent, Lua will perform the following settings：

- Number of variables> number of values: complement by number of variables nil
- Number of variables <number of values: Excess values are ignored

The following example shows this setting.：

```lua
a, b, c = 0, 1
print(a,b,c)
--Output 0   1   nil

a, b = a+1, b+1, b+2
print(a,b)
--Output 1   2

a, b, c = 0
print(a,b,c)
--Output 0   nil   nil
```

**Here you need to think about what will be output by running the following code.？**

```lua
a,b,c = 1,2,3
a,c = a+1,b
d = c,b
print(a,b,c,d)
```

--------

### Swap Variables

This part requires you to complete a task on your own.：

Knowing the following code, and knowing the values of `a` and `B`, please exchange their values to make the printout.`12  34`

```lua
a = 34
b = 12

--You need to swap variables here.
--Tip: Create a new variable to hold the temporary value

print(a,b)
```

--------

### output variable

We already know that in Lua, you can use the print function to print the result you want.

Also in the previous section, we learned to create new variables and set their values.

Let`s try to output a variable! Use the `print function` to output known variables. We know that the variable `num` is a number, try to output its value.！

```lua
num = 123
--Please complete the code and output the value of num
print(You're going to fill in here)
```

--------

### arithmetic operator

An operator is a special symbol that tells the interpreter to perform a particular mathematical or logical operation.

In the previous section, we created a new numeric variable, which we call a `number` type variable.

In this section we learn to use `arithmetic operators`, as follows：

```
+ Addition
- subtraction
* Multiplication
/ division
% Take the remainder and find the remainder of the division.
^ power, calculate the power
- Negative sign, take negative value
```

We can understand the application of arithmetic operators through the following examples：

```lua
a = 21
b = 10
c = a + b
print('a + b The value ', c )
c = a - b
print('a - b The value ', c )
c = a * b
print('a * b The value ', c )
c = a / b
print('a / b The value ', c )
c = a % b
print('a % b The value ', c )
c = a^2
print('a^2 The value ', c )
c = -a
print('-a The value ', c )
c = a * (b - a)
print('a * (b - a) The value ', c )
```

**You need to complete the following tasks：**

It is known that the length, width and height of a cuboid are A, B and C (in meters) respectively, and the weight of this object is M (in grams）

Please print out the density of the object (unit g/m³）

Note: density calculation formula density = mass/volume

```lua
a,b,c = 1,2,3 --length, width and height
m = 10        --Weight
--Please print out the density of the object
```

--------

## String

### string Type variable

`String `(I. e. `string`) is a string of text data that can store the text you want.

In the second section, the data from `print` is a string.

Lua Strings in the language can be represented in the following three ways：

1. A string of characters between single quotation marks
2. A string of characters between double quotes
3. [[a string of characters between and]]

You can refer to the following example to understand in depth：

```lua
--A string of characters between double quotes
str1 = "Lua"
--A string of characters between single quotation marks
str2 = 'Lua'
--[[and]]-- a string of characters
str3 = [[Lua]]
str4 = [[When using double parentheses, you can even include newline data.
Line break
Last line]]

--Output all strings
print(str1)
print(str2)
print(str3)
print(str4)
```

**Next you need to complete the following exercises：**

Create three new variables`s1`、`s2`、`s3`

Store string data respectively: `str`, `abc`, `233` to make the output print correctly

```lua
--Please complete the code in the blank

print("s1,s2,s3 The value：",s1,s2,s3)
print("s1,s2,s3 The type：",type(s1),type(s2),type(s3))
```

--------

### escape character

In the previous section, we learned how to declare strings.

But we sometimes encounter some special problems, such as: **How to output single quotation marks and double quotation marks? How to output carriage return and line feed？**

Perhaps we can simply circumvent it in the following way, when outputting single quotes, the declaration string is enclosed in double quotes, like the following

```lua
str = "'"
```

Similarly, when outputting double quotes, the declaration string is enclosed in single quotes, like the following

```lua
str = '"'
```

However, this will cause a problem: how to display single and double quotes at the same time? Here we need the` escape character.

Escape characters are used to represent characters that cannot be displayed directly, such as the back key, the enter key, etc.

All the characters beginning with `\` are escape characters, which are commonly used in the following escape character format：

escape character | meaning
-|-
\n|Line feed (LF) to move the current position to the beginning of the next line
\r|Enter (CR) to move the current position to the beginning of the line
\\\\|backslash character\
\'|Single quotation mark
\"|Double quotes
\0|null character(NULL)
\ddd|1 to any character represented by a 3-digit octal number.
\xhh|1 to any character represented by 2 hexadecimal

For example, if we want to assign str a single quote and a double quote ('"), then we can write：

```lua
str = "\'\""
```

**The following needs you to complete a simple task：**

Create a new variable `str` and assign str a value

`ab\cd"ef'g\h]]`

and print it out

```lua
str = --completion code
print(str)
--The output should be ab\cd"ef'g\h]]
```

--------

### string Splicing

Can strings and strings be added? Yes! We can use the splice symbol to put together two independent strings.

We use `..` to represent the string concatenation symbol, as in the following example code：

```lua
print('abc'..'def')
str1 = '123'
str2 = '999'
print(str1..str2)
```

**Next you will complete this task：**

Three string variables are known`s1`、`s2`、`s3`

Please splice them in order, store `all`, and use print to output the result.

```lua
s1,s2,s3 = "aaa","bbb","ccc"
all = --Please complete the code
print(all)
```

--------

### number Turn string

The above section learned how to splice strings. This method is certainly very useful, but sometimes we will encounter a requirement, that is, to splice the` number` type variable and` string` type variable to form a new one.`string`

For example, the following variables `n` and `s`, how to join together？

```lua
n = 123
s = 'm/s'
```

We can directly convert the variable `n` of type `number` to a value of type `string`, so that we can concatenate

This can be done using the `tostring(value)`function：

```lua
n = 123
s = 'm/s'

result = tostring(n)..s
print(result)
```

**Next you will complete this task：**

Three known variables`n1`、`s`、`n2`

Then splice them in order and store them in the variable `result` to make the output correct.

Tip: In some cases, Lua will automatically convert the number type to the string type

```lua
n1,s,n2 = 1,"abc",2
result = --Please complete the code
print(result)
```

--------

### string Turn number

The above section learned how to convert number to string. In this section, we will learn how to convert string to string.number

For example, the following variable `s`, the stored content is a string, but represents a number, how to convert it into number and add it to n？

```lua
n = 123
s = '2333'
```

We can directly convert the variable `s` of type `string` to a value of type `number`, so that we can calculate

This can be done using the `tolumber (value)`function：

```lua
n = 123
s = '2333'

result = tonumber(s) + n
print(result)
```

**Next you will complete this task：**

Three string variables `s1`, `s2`, `s3` are known, and their contents are pure numbers

Please calculate their arithmetic and assign to the new variable `result` so that the following code outputs the correct result.

```lua
s1,s2,s3 = "11","12","100"
result = --Please complete the code
print(result)
```

--------

## logical operation

### Boolean and comparison operations

Boolean (boolean) has only two optional values: `true` (true) and `false` (false)）

Lua False and nil are regarded as` false`, and the others are true (including the value of 0, which is also equivalent`true`）

Lua There are also many `relational operators` for comparing sizes or whether comparisons are equal. The symbols and their meanings are as follows：

|symbol | meaning|
|-|-|
|==|Equal to, detects whether two values are equal, equality returns true, otherwise returns false|
|~=|Not equal, check whether two values are equal, equal return false, otherwise return true|
|>|Greater than, returns true if the value on the left is greater than the value on the right, otherwise false|
|<|Less than, returns false if the value on the left is greater than the value on the right, otherwise true|
|>=|Greater than or equal to, returns true if the left value is greater than or equal to the right value, otherwise false|
|<=|Less than or equal to, returns true if the left value is less than or equal to the right value, otherwise false|

We can use the following examples to better understand the application of relational operators：

```lua
a = 21
b = 10
print('==The result',a==b)
print('~=The result',a~=b)
print('>The result',a>b)
print('<The result',a<b)
print('>=The result',a>=b)
print('<=The result',a<=b)
```

**Here comes the question. What will be the output of running the following code? Please think for yourself.**

```lua
a = 1
b = '1'
c = a
d = 2

print(a == b)
print(c == a)
print(a ~= b)
print(d <= c)
```

--------

### Logical operators

Logical operators are calculated based on Boolean values and give results. The following table lists common logical operators in Lua language：

|symbol | meaning|
|-|-|
|and|Logical and operator. Returns A if A is false, otherwise B|
|or|Logical or operator. Returns A if A is true, otherwise B|
|not|Logical non-operator. Contrary to the result of a logical operation, if the condition is true, the logical non is false|

We can use the following examples to better understand the application of logical operators：

```lua
print('true and false The result',true and false)
print('true or false The result',true or false)
print('true and true The result',true and true)
print('false or false The result',false or false)
print('not false The result',not false)
print('123 and 345 The result',123 and 345)
print('nil and true The result',nil and true)
```

**Here comes the following question. What result will be output by running the following code？**

```lua
a = 1
b = '1'
c = 0

print(a and b)
print(c or a)
print(not b)
print(d and c)
print(1 < 2 and 3 > 2)
```

--------

### Test size (self-test questions）

Topic: If the number variable` n` is known, then if it is necessary to judge whether n meets the following conditions：

3<n≤10

The following four lines of judgment code, correct is？

（A return of true indicates that the variable `n` meets the requirements.）

```lua
n = 1--This number can be any number.
print(n > 10 or n <= 3)
print(n <= 10 or n > 3)
print(n < 3 and n >= 10)
print(n <= 10 or n > 3)
```

--------

## branch judgment

### condition judgment

The above section learned about Boolean types, so where does this need to be used? We need to use it to make some judgments.

In Lua, you can use the` if` statement to judge, such as the code shown below, you can judge whether the` n is a number less than 10`：

```lua
n = 5
if n < 10 then
    print('n Less 10')
end
```

Let's sort it out. In fact, the if statement is structured as follows：

```lua
if Condition then
    Eligible Code
end
```

**Here's what you need to do.：**

Known variable `n`, please judge whether `n` is odd, if so, please add `n` to the value of `n`1

If you find it difficult, please check the following tips：

- Find the remainder of `n` divided by 2：n % 2
- Add the value of `n`1：n = n + 1

```lua
--A number variable is known n
n = 5   --This number is a few possible
print("n Value before judgment",n)

if Your condition then
    Things to do
end
print("n Value after judgment",n)
```

--------

### multi-condition judgment

The above section learned how to write a simple if statement. In this section, we will learn a multi-conditional branch statement.

In Lua, you can use the` if` statement to judge, and you can use the` else` statement to judge multiple branches.

```lua
if Condition 1 then
    meet the condition 1
elseif Condition 2 then
    Condition 1 is not met, but the condition is met 2
else
    All previous conditions are not met
end
```

For example, there is a number.n`：

- When it is greater than or equal to 0 and less than 5, the output` is too small`，
- When it is greater than or equal to 5 and less than 10, the output is moderate.`，
- When it is greater than or equal to 10, the output` is too large`，

Then the code is like the following：

```lua
n = 1--Try changing this number a few more times.
if n >= 0 and n < 5 then
    print('Too small')
elseif n >= 5 and n < 10 then
    print('Moderate')
elseif n >= 10 then
    print('Too big')
end
```

Note: `else` and `elseif` are optional, optional, but `end` cannot be omitted

**Here's what you need to do.：**

Known variable `n`, please judge whether `n` is odd，

- If yes, add the value of `n`1
- If not, change the value of `n` to twice the original

```lua
--A number variable is known n
n = 5   --This number is a few possible
print("n Value before judgment",n)

if Your condition then
    Things to do
else
    Things to do
end
print("n Value after judgment",n)
```

--------

### Judging the legality of triangles (self-test questions）

You need to use the knowledge of the previous chapters to complete the following topics

It is known that three variables of type number A, B and C represent the lengths of three sticks respectively.

Please judge, using these three sticks, whether you can form a triangle (the sum of the two short sides is greater than the third side）

- If it can be composed, print it out.true
- If not, print it.false

```lua
a,b,c = 1,2,3--Change a few more values to test yourself.

--completion code
```

--------

### if The basis of judgment (self-test questions）

As we learned earlier, Lua regards `false` and` nil` as` false`, and the rest is` true` (including the value of` 0 `, which is also equivalent`true`）

**Then the question arises, what will be output by executing the following code？**

```lua
result = ''
if 0 then
    result = result..'T,'
else
    result = result..'F,'
end
if a then
    result = result..'T'
else
    result = result..'F'
end
print(result)
```

--------

## Function

### initial knowledge function

`A function` refers to a program that can do something together, also called a subroutine.

In the previous content, we have already touched the call of the function, which is used many times before.`print(...)`。

The call function only needs to be in the following format：

`Function name (parameter 1, parameter 2, parameter 3,......)`

Why use functions? Because many things are repetitive operations, we use functions to quickly complete these operations.

Let's take an example of the simplest function, which has no incoming parameters and no return value.

It implements a simple function, which is to output`Hello world!`：

```lua
function hello()
    print('Hello world!')
end
```

This function is called `hello`, and we can call it as follows (execute）：

```lua
function hello()
    print('Hello world!')
end
hello()
```

This line of code will output`Hello world!`。

At the same time, in Lua, a function is also a variable type, that is to say, `hello` is actually a variable, which stores a function, we can use the following code to understand：

```lua
function hello()
    print('Hello world!')
end
a = hello
--Assign the hello function to the variable at the same time.
a()
hello()
--a and hello variable pointing to the same function
--So the execution result is the same as hello()
```

Because a function is just a variable, you can even declare a `hello` function like this at the beginning.：

```lua
hello = function()
    print('Hello world!')
end
hello()
```

**Here's a simple thing you need to do：**

- Create a new function variable `biu` to print the string `biubiu` after execution.，
- Create a new function variable `pong` so that it is the same as the function pointed to by `biu`

```lua
--Please complete the code here

--Please make the following call print out normally biubiubiu
biu()
pong()
print("biu Is it equal to pong?？",biu==pong)
```

---------

### local Variable

The variables we created before are global variables, which are not destroyed from beginning to end in the code run cycle and can be called anywhere.

However, when our code volume increases, a large number of new global variables will often lead to a surge in memory. We need a variable that can be used temporarily and can be automatically destroyed to release memory resources. How should we solve this problem?？

We can use the `local` flag to create a new temporary variable, and use `local` to create a local variable. Unlike global variables, local variables are only valid within the code block being declared.

Refer to the code below：

```lua
a = 123
function add()
    local n = a+2
    print(n)
end
add()
print(n)
```

In the above code, `n` is a local variable, which is only valid in `this functionon`, and this part of memory will be automatically reclaimed after the function runs.

We should use local variables as much as possible to facilitate the automatic reclaiming of memory space by the lua virtual machine, while reducing resource consumption and improving the running speed.

Please read the following code below and think about what the correct output is.：

```lua
str = 'abc'
function connect()
    local s = str..'def'
end
print(s,str)
```

--------

### Function Parameters

In the use of the previous chapters, we know that functions can pass in parameters, such`print(123)`

So, how do we write a function that can pass in parameters?

```lua
function Function name (parameter 1, parameter 2,...)
    Code Content
end
```

The parameters passed in here are equivalent to creating a new `local` variable inside the function. Modifying these data will not affect the external data (except for the `table` and other types that have not been mentioned later）

For example, the following function, for example, can print out the sum of two incoming values：

```lua
function add(a,b)
    print(a+b)
end
add(1,2)
--will output 3
```

This code is actually equivalent：

```lua
function add()
    local a = 1
    local b = 2
    print(a+b)
end
add()
```

The following problem comes, please design a function` p`, which can print out the density of the object according to the following call method：

```lua
--Complete the code of this function to meet the requirements of the topic
function p(a,b,c,m)

    print()
end

--The length, width and height of a cuboid are A, B and C respectively (in meters）
a = 1
b = 2
c = 3
--The weight of this object is m (in grams）
m = 230
--Print out the density below
--Note: density calculation formula density = mass/volume
p(a,b,c,m)
```

--------

### function return value

In the previous code, we implemented a function that inputs variables `a` and `B`, and the function automatically outputs the sum of the two values.

But generally speaking, our needs are far more than these, we may need a function with the following functions：

`Execute the function, enter two values, get the sum of the two values`

If we still follow the contents of the above sections, we will only output this value and will not pass this value to other variables for subsequent use. How can we solve this requirement?？

We can use the return value of the function to achieve this requirement, combined with the above requirements, we can use the following code to achieve：

```lua
function add(a,b)
    return a+b
end
all = add(1,2)
--Here the value of all is 3
print(all)
```

The `return` here means to return a value, `and end the function immediately.`

At the same time, just as there can be multiple input values, there can also be multiple return values.

```lua
function add(a,b)
    return a+b,"ok"
end
all, result = add(1,2)
--Here the value of all is 3
--The value of result here is string "ok"
print(all,result)
```

Here comes the following problem. Please design a function` p`, which can return the density of the object according to the following call method. The return value is` number` type：

```lua
function p(a,b,c,m)
    --Please complete the code
end

--The length, width and height of a cuboid are A, B and C respectively (in meters）
a = 1
b = 2
c = 3
--The weight of this object is m (in grams）
m = 230
--The following returns the density value
--Note: density calculation formula density = mass/volume
result = p(a,b,c,m)
print(result)
```

--------

### Judging the legality of triangles 2 (self-test questions）

You need to use the knowledge of the previous chapters to complete the following topics

- Three variables of type number are known, representing the lengths of three sticks respectively.
- Please judge, using these three sticks, whether you can form a triangle (the sum of the two short sides is greater than the third side）
- Please create a new function triangle and call it in the following form (if it can be composed, return true）：

```lua
function triangle(a,b,c)
    --Please complete the code
end

result = triangle(1,2,3)--The incoming value is the length of three sides, change it to a few more tests.
print(result)
```

--------

### Returns multiple values (self-test questions）

You need to use the knowledge of the previous chapters to complete the following topics

- Two variables of type number are known, representing the length and width of a cuboid respectively.
- Please calculate the perimeter and area of this rectangle
- Please create a new function `rectangle` and call it in the following form：

```lua
function rectangle(a,b)
    --completion code
end

area,len = rectangle(1,2)
--Results：
--area is 2
--The perimeter len is 6
print(area,len)
```

--------

## table

### Understanding the array

Array, using a variable name, to store a series of values

Many languages have the concept of arrays. In Lua, we can use `table` (table) to implement this function.

In Lua, table is a collection of a series of elements, represented by curly brackets, separated by commas, similar to the following code：

```lua
t = {1,3,8,5,4}
```

We can directly use the `subscript` of the `element` to access or assign values to the `element.

In the `table` variable `t` above, the subscript of the first element is `1`, the second is `2`, and so on.

We can access or change this `element` with `subscription` in the brackets, as in the following example：

```lua
t = {1,3,8,5,4}
print(t[1]) --Print 1
print(t[3]) --Print 8

t[2] = 99 --Change the value of the second element
print(t[2]) --Print 99

t[6] = 2 --Create a new sixth element out of thin air and assign a value
print(t[6]) --Print 2

print(t[10])
--Because it does not exist, print nil
```

The above is the simplest example of table, which is to be used as an array (note that arrays in general languages are basically of immutable length, and table here is of variable length）

**Below you need to complete：**

- Create a new table, called cards, and store 1-10 numbers.
- Exchange the 3rd element with the 7th element
- Swap the 9th element with the 2nd element
- Add an 11th variable with a value 23

```lua
--Please complete the code
cards =
```

--------

### Simple table

In the previous section, we will `table` to signify `array`, in fact, `table` can include `any type of data.`

For example, we can put `number` and`string` data in `table`, similar to the following code：

```lua
t = {"abc",223,",..a",123123}
```

We can even put `function` variables in it.

```lua
t = {
    function() return 123 end,
    function() print("abc") end,
    function(a,b) return a+b end,
    function() print("hello world") end,
}
t[1]()
t[2]()
t[3](1,2)
t[4]()
```

The way these `table` accesses each element is still directly with subscripts and can also be modified with subscripts.

**Below you need to complete：**

- Create a new `table` named `funcList` and implement the following functions
- Call `funcList[1](a, B) `to return the product of a and B
- Call `funcList[2](a, B) `to return the difference between a and B.
- Call `funcList[3](a)`to return the inverse of a（-a）

```lua
--Please complete the code
funcList = {

}

a,b = 1,2--Provide two numbers
print("a,b Value is",a,b)
print("a Product of and B：",funcList[1](a,b))
print("a and the difference of B：",funcList[2](a,b))
print("a and the opposite number：",funcList[3](a))
```

--------

### table Subscript

In the first two sections, our `table` is just some simple List (list), and the `subscription` of each element is automatically arranged from 1

In fact, in Lua, subscripts can be specified directly at the time of declaration, like the following：

```lua
t = {6,7,8,9}
--The code above and below are equivalent
t = {
    [1] = 6,
    [2] = 7,
    [3] = 8,
    [4] = 9,
}

--You can even skip certain subscripts
t = {
    [1] = 6,
    [3] = 7,
    [5] = 8,
    [7] = 9,
}
print(t[7])
--Output 9

--It is also possible to give element values after declaration
t = {}--Empty table
t[101] = 10
print(t[101])
--Output 10
```

**Below you need：**

- Create a new variable `t` and declare it in the following format
- Element with subscript `1`, value is`123`（number）
- The element with subscript `13` has a value`"abc"`（string）
- The element with subscript `666` has a value`"666"`（string）

```lua
--Please complete the code
t = {

}

print("Element with subscript 1：",t[1],type(t[1]))
print("Element with subscript 13：",t[13],type(t[13]))
print("Element with subscript 666：",t[666],type(t[666]))
```

--------

### Subscript Advanced

In the previous section, we learned how to customize subscripts, but in Lua, subscripts can also be strings, as in the following example

```lua
t = {
    ["apple"] = 10,
    banana = 12,
    pear = 6,
}
--Use ["subscript"] = value
--and subscript = value
--It's all written correctly.
--When the second way is ambiguous, the first way should be used.

--It can be accessed in two ways.：
print(t["apple"])
--Output 10
print(t.apple)
--Output 10
--When the second way is ambiguous, the first way should be used.
```

It can be seen that when `string` is used as the index, the flexibility of `table` is improved by an order of magnitude.

`string`As a substandard, you can also assign values dynamically.：

```lua
t = {} -- Empty table
t["new"] = "New value"
print(t.new)
--Output new value
```

**Below you need to complete：**

- New `table` variable`t`
- The element with the subscript `apple` has a value`123`（number）
- The element with the subscript `banana` has a value`"abc"`（string）
- Element with subscript `1 @ 1 `, value is`"666"`（string）

```lua
--Please complete the code
t = {

}

print("Elements with subscript apple：",t["apple"],type(t["apple"]))
print("Element with subscript banana：",t["banana"],type(t["banana"]))
print("Element with subscript 1 @ 1：",t["1@1"],type(t["1@1"]))
```

--------

### table Quiz

**The following code will print what？**

```lua
t = {
    apple = {
        price = 7.52,
        weight = 2.1,
    },
    banana = {
        price = 8.31,
        weight = 1.4,
        year = '2018'
    },
    year = '2019'
}
print(
    t.price,
    t.apple.price,
    t.banana.weight,
    t.year
)
```

--------

### table Quiz 2

**The following code will print what？**

```lua
t = {
    {
        price = 7.52,
        weight = 2.1,
    },
    {
        price = 8.31,
        weight = 1.4,
        year = '2018'
    },
    year = '2019'
}
print(
    t["price"],
    t[1].price,
    t[2].weight,
    t["year"]
)
```

--------

### Lua Global variables and table

As we learned earlier, in the` table`, elements can be accessed directly with` table name [subscript] `or` table name. string subscription`

In fact, in Lua, all global variables are stored in a large `table`, which is called：`_G`

We can use the following example to demonstrate：

```lua
n = 123--New Variable
print(n)--Output 123
print(_G.n)--Output 123

_G.abc = 1--Equivalent to creating a new global variable
print(abc)--Output 1

_G["def"] = 23--Equivalent to creating a new global variable
print(def)--Output 23

--You can even look like the following
_G.print("hello")
_G["print"]("world")
```

Now, you see why it`s said, "Everything is based on a table."？

**You need to complete the following tasks：**

- It is known that there is a global variable named`@#$`
- Please create a new variable`result`
- Assign the value in the `@#$` variable`result`

```lua
_G["@#$"] = 123

result = --Please complete the code
print("result Value is",result)
```

--------

### table Small test 3

**Create a new table named t that meets the following requirements**

- `t[10]`Data of type `number` can be obtained`100`
- `t.num`Data of type `number` can be obtained`12`
- `t.abc[3]``string` type data is available`abcd`
- `t.a.b.c`Data of type `number` can be obtained`789`

```lua
--Please complete the code
t = {

}

print("t[10]Number type data can be obtained 100:",t[10],type(t[10]))
print("t.num Number type data can be obtained 12:",t.num,type(t.num))
print("t.abc[3]String type data can be obtained abcd:",t.abc[3],type(t.abc[3]))
print("t.a.b.c Number type data can be obtained 789:",t.a.b.c,type(t.a.b.c))
```

--------

### table.concat

table.concat (table [, sep [, i [, j ] ] ])

The elements are `table` of type `string` or `number`, and each element is concatenated into a string and returned.

The optional parameter `sep`, which represents the connection spacer, is empty by default.

`i`and `j` represent the subscripts of the beginning and end of the element.

Below is an example：

```lua
local a = {1, 3, 5, "hello" }
print(table.concat(a))
print(table.concat(a, "|"))

-->Printed Results：
--135hello
--1|3|5|hello
```

**Please complete the following tasks：**

- Known table variables t，
- Connect all the results in t.
- Spacer use`,`
- and print it out using print

```lua
t = {"a","b","c","d"}
print("Connection Results：")
--completion code
```

--------

### table Deletion

table.insert (table, [pos ,] value)

Insert value into the pos index position of the (array) table, and move other elements backward to empty places. The default value of pos is the length of the table plus one, that is, the default is inserted at the end of the table.

table.remove (table [, pos])

Delete the element with index pos(pos can only be number type) in the table table, and return the deleted element, and the index value of all elements after it will be reduced by one. The default value of pos is the length of the table, that is, the default is to delete the last element of the table.

Below is an example：

```lua
local a = {1, 8}             --a[1] = 1,a[2] = 8
table.insert(a, 1, 3)   --Insert at table index 1 3
print(a[1], a[2], a[3])
table.insert(a, 10)    --Insert at the end of the table 10
print(a[1], a[2], a[3], a[4])

-->Printed Results：
--3    1    8
--3    1    8    10


local a = { 1, 2, 3, 4}
print(table.remove(a, 1)) --Delete elements with speed index 1
print(a[1], a[2], a[3], a[4])

print(table.remove(a))   --Delete last element
print(a[1], a[2], a[3], a[4])

-->Printed Results：
--1
--2    3    4    nil
--4
--2    3    nil    nil
```

**Please complete the following tasks：**

- Known table variables`t`，
- Remove the first element in t
- Then, in front of the third element of t, add a number variable with a value 810

```lua
t = {1,2,3,4,5,6,7,8,9}

--completion code

print("The first element should be 2：",t[1])
print("The third element should be 810：",t[3])
```

--------

## Cycle

### while Cycle

In the actual function implementation, we often encounter code that needs to be run in a loop, such as filling table data from 1 to 100. We can directly use loop statements to implement

Let`s first learn the loop syntax of` while`. The overall format is as follows：

```lua
while Judgment basis for continuing cycle do
    Code executed
end
```

Here is an example where we calculate the result of adding from 1 to 100：

```lua
local result = 0
local num = 1

while num <= 100 do
    result = result + num
    num = num + 1
end

print(result)
```

The above code is that when num ≤ 100, result continuously adds num, and num adds itself after each cycle.1

**After understanding the above code, let's complete the following simple task：**

- Two variables of type number `min` and`max`
- Please calculate the sum of all `multiples of 3` between `min` and `max`
- Print out results

```lua
min,max = 114,514 --This result should be 42009
result = 0--The results are stored in this variable.

while Please perfect do
    --completion code
end

print("Results：",result)
```

--------

### for Cycle

for Loops are similar to while loops to some extent, but for loops can express the amount of intermediate accumulation more concisely.

First of all, let`s learn the loop syntax of` for`. The overall format is as follows：

```lua
for Temporary variable name = start value, end value, step size do
    Loop code
end
```

where `step` can be omitted and defaults 1

`The temporary variable name `can be used directly in the code area (but cannot be changed), each loop will automatically add the `step value`, and stop the loop after reaching the `end value`

Here is an example where we calculate the result of adding from 1 to 100：

```lua
local result = 0

for i=1,100 do
    result = result + i
end

print(result)
```

The above code is that when i≤ 100, result continuously adds I, and I increases after each loop.1

**After understanding the above code, let's complete the following simple task：**

- Two variables of type number `min` and`max`
- Please calculate the sum of all multiples of 7 between `min` and `max`
- Print out results

```lua
min,max = 114,514 --This result should be 17955
result = 0--The results are stored in this variable.

for --completion code


print("Results：",result)
```

--------

### Interrupt Loop

Earlier we learned the loop statement, sometimes the loop runs to half, we don't want to continue to run, how to do？

We can use `break` in a loop body to immediately end the loop and continue to run the following code

For example, like the following, calculate the maximum sum of` less than 100 `on the way to the addition of 1-100`：

```lua
result = 0
for i=1,100 do
    result = result + i
    if result > 100 then
        result = result - i
        break
    end
end

print(result)
```

As you can see, when the sum is found to be greater than 100, the code immediately restores the value of` result` to the state before adding the current number, and calls the` break` statement, which immediately exits the loop

In `while`, we can also use`break`:

```lua
result = 0
c = 1
while true do
    result = result + c
    if result > 100 then
        result = result - c  
        break
    end
    c = c + 1
end

print(result)
```

We directly used the dead loop here (because` while` is always judged on the basis of` true`), and the overall logic is consistent with the previous for code. when the sum is found to be greater than 100, the code immediately restores the value of` result` to the state before adding the current number, and calls the` break` statement to exit the loop immediately.

**Now you need to complete a task：**

- Request the maximum value that is less than a multiple of 13 of variable max (max is greater 0）
- and print out the results
- In theory, this topic can be implemented without a loop, but in order to practice the technique, please use the for loop to implement it.

```lua
max = 810 --The result should be 806
result = 0

for --Please complete the code

print(result)
```

--------

### Cycle test question 1 (self-test question）

Earlier we learned the loop statement, we need to complete the following tasks

We know that the `print` function can print a complete line of output

Then, given the variable a, please print out the following result：

（a is an integer greater than 0, and a row of data needs to be output. the data starts from 1, and the difference between each row and the previous row is 2）

```lua
1
3
5
7
9
（The above example is the case when a is 5）
```

Problem-making area：

```lua
a = 10
--You need to use print to output the required results.
print("Output Results：")


for --Please complete the code
```

--------

### Cycle test question 2 (self-test question）

We need to complete the following tasks

Then, given the variable a, please print out the following result：

（a is an integer greater than 0, and a row of data needs to be output, with one * for the first row and one more for each subsequent row.*）

```lua
*
**
***
****
*****
（The above example is the case when a is 5）
```

Problem-making area：

```lua
a = 10
--You need to use print to output the required results.
print("Output Results：")


for --Please complete the code
```

--------

### Cycle test question 3 (self-test question）

We need to complete the following tasks

Then, given the variable a, please print out the following result：

（a is an integer greater than 0, and a row of data needs to be output, which is output according to the rule shown in the figure.）

```lua
1
12
123
1234
12345
123456
1234567
12345678
123456789
12345678910
1234567891011
（The above example is the case when a is 11）
```

Problem-making area：

```lua
a = 20
--You need to use print to output the required results.
print("Output Results：")


for --Please complete the code
```

--------

### Cycle test question 4 (self-test question）

- There was a monkey who picked several peaches on the first day and ate half of them immediately, but he still felt that it was not enough, so he ate another one.
- The next morning, I ate half of the remaining peaches, but I still didn't feel satisfied, so I ate two more.
- After that, I ate the remaining half of the previous day plus the number of days every morning (for example, I ate the remaining half of the previous day plus 5 on the 5th day）。
- By the time I wanted to eat again on the morning of the n th day, there was only one peach left.
- Then, given that the variable a is the number of days on the last day, please print out the number of peaches on the first day.
- For example, when a is 5, the output 114

Problem-making area：

```lua
a = 6
--You need to use print to output the required results.
print("Output Results：")


for --Please complete the code
```

--------

## Explain the string library in detail.

### string.sub

The next few sections will explain the various interfaces of the string library.

```lua
string.sub(s, i [, j])
```

Returns the substring from index `I` to index `j` in the string`s.

i Can be a negative number that represents the penultimate character.

When j is defaulted, it defaults to -1, which is the last position of the string s.

When index I is at the position of string s after index j, an empty string is returned.

Below is an example：

```lua
print(string.sub("Hello Lua", 4, 7))
print(string.sub("Hello Lua", 2))
print(string.sub("Hello Lua", 2, 1))
print(string.sub("Hello Lua", -3, -1))

-->Printed Results：
lo L
ello Lua

Lua
```

It is worth noting that we can use colons to simplify the syntax, like the following：

```lua
s = "12345"
s1 = string.sub(s, 4, 7)
s2 = s:sub(4, 7)
--The two ways of writing are equivalent relations.
print(s1,s2)
```

**Please complete the following tasks：**

- Known string variable`s`, please print out separately (one line for each）：
- `s`Values from `4th character to the last`
- `s`The value from the first character to the third character from the bottom.
- `s`The value from `starting from the fifth last character to the second last character`

```lua
s = "1919810"

--completion code
print()
print()
print()
```

--------

### string.rep

string.rep(s, n)

Returns n copies of string s.

Sample code：

```lua
print(string.rep("abc", 3))
--Output Results：
--abcabcabc
```

**Please complete the following tasks：**

**Print a row of data with 810 data contents 114514**

```lua
--completion code
print()
```

--------

### string.len

string.len(s)

Receives a string and returns its length.

Sample code：

```lua
s = "hello lua"
print(string.len(s))
--Output Results：
9

--You can also use a simple syntax
print(s:len())
```

**Please complete the following tasks：**

- Create a new variable s so that the data content is 810 114514
- and print out the length of the string s

```lua
s = --completion code
print()
```

--------

### Case Conversion

string.lower(s)

Receives a string s and returns a string that turns all uppercase letters into lowercase letters.

string.upper(s)

Receives a string s and returns a string that turns all lowercase letters into uppercase letters.

Sample code：

```lua
s = "hello lua"
print(string.upper(s))
print(string.lower(s))
--Output Results：
HELLO LUA
hello lua

--You can also use a simple syntax
print(s:upper())
print(s:lower())
```

**Please complete the following tasks：**

Given a variable `s`, print out the S string with all uppercase letters.

```lua
s = "asd8938KJjsidiajdl;(()k)"
print --completion code
```

--------

### string.format

string.format(formatstring, ...)

Follow the formatting parameter `formatstring` to return the formatted version of the content of the following.

The rules for writing a formatted string are basically the same as the rules for the printf function in the standard c language：

It consists of regular text and instructions that control where and how each parameter should be placed in the formatted result.

An indication consists of the character `%` plus a letter that specifies how to format parameters, such as `d` for decimal numbers, `x` for hexadecimal numbers, `o` for octal numbers, `f` for floating point numbers, `s` for strings, and so on.

Sample code：

```lua
print(string.format("%.4f", 3.1415926))     -- Keep 4 decimal places
print(string.format("%d %x %o", 31, 31, 31))-- Decimal number 31 is converted to different decimal.
d,m,y = 29,7,2015
print(string.format("%s %02d/%02d/%d", "today is:", d, m, y))
--Control the output of 2 digits, and in front of the complement.0

-->Output
-- 3.1416
-- 31 1f 37
-- today is: 29/07/2015
```

**Please complete the following tasks：**

- A variable `n` is known as an integer of type `number`
- Prints the string of `n:`with the value `n`

```lua
n = 810

print --completion code
```

--------

### string the essence

In this section we explain the nature of strings

`String `, is used to store a string of characters,` but its essence is a string of numeres `. How to use a string of numbers to represent a string of characters？

In computers, each symbol corresponds to a number, but before explaining this knowledge, let's learn about supplementary knowledge.：

```lua
In most programming languages, we start with 0x to indicate that the number is hexadecimal.
Such
10 equivalent 0x0a
256 equivalent 0xff
```

Next, you need to understand that each symbol corresponds to a number, such：

`0`Corresponding to `0x30`, `1` corresponds`0x31`
`a`Corresponding to `0x61`, `B` corresponds`0x62`
`A`Corresponding to `0x41`, `B` corresponds`0x42`

The above coding rules, we call ascii code, specifically want to know can open the following URL to view：

[http://ascii.911cha.com/](http://ascii.911cha.com/)

Of course, the maximum size of 1 byte is 0xff, that is, 256, and only some symbols can be saved. Most Chinese characters are coded according to certain codes, and one Chinese takes up 2 or 3 bytes.

How the computer parses these data, we don't need to understand, when you know the above knowledge, you should be able to understand the following description：

```lua
The string "apple" is actually the following string of numbers：
0x61,0x70,0x70,0x6c,0x65
```

At the same time, lua's string can save any value, even 0x 00, which does not represent any meaning, can also be saved.

```lua
Added: In other languages (such as C),0x 00 represents the end of a string, but this is not the case in lua.
lua Each byte of the string can store an arbitrary byte of data.
```

For example, the following description：

```lua
There is a string of lua strings in which the data is：
0x01,0x02,0x30,0x00,0x44
What can be seen by actual people (invisible characters are replaced）：
��0�D
Of course, you can't say that the data you can't see does not exist, they are all intact in this string
```

Next you need to think about a question: a string of string data is as follows, what is its actual content (refers to the string content that people can see, such abcd）？

0x62,0x61,0x6e,0x61,0x6e,0x61

--------

### string.char

string.char (...)

Receives 0 or more integers (integer range: 0 to 255) and returns a string of ASCII characters corresponding to these integers. When the parameter is empty, the default is a 0。

If you have studied the previous chapter carefully, this passage should be well understood. In essence, it is to turn a string of numbers known by the computer into a string variable, and the data in the string is the string of data to be stored.

Sample code：

```lua
str1 = string.char(0x30,0x31,0x32,0x33)
str2 = string.char(0x01,0x02,0x30,0x03,0x44)
print(str1)
print(str2)

-->Output (invisible characters are replaced）
--0123
--��0�D
```

**Please complete the following tasks：**

- It is known that each character of a string is arranged in order in the array t
- Please print out the string content (a line of data according to the value of t）
- Note: This string stores characters that are not necessarily visible.

```lua
t = {0x79,0x6F,0x75,0x20,0x61,0x72,0x65,0x20,0x72,0x69,0x67,0x68,0x74}
print("Real string content：")
--completion code
```

--------

### string.byte

string.byte(s [, i [, j ] ])

Returns the ASCII code for the characters s [I], s [I 1], s [I 2],..., s[j]. The default value of I is 1, the first byte, and the default value of j is i 。

This function function is just the opposite of the previous string.char, which is to extract the actual value in the string.

Sample code：

```lua
str = "12345"
print(string.byte(str,2))
print(str:byte(2))--Can also be so
print(str:byte())--Do not fill in the default is 1

-->Output (decimal data）
--50
--50
--49
```

**Please complete the following tasks：**

- Known String s
- Please add up all the data represented in s and print it out.

```lua
s = string.char(1,2,3,4,5,6,7,8,9)
print("s The sum of the internal data is：")
--completion code
```

--------

### string.find

string.find(s, p [, init [, plain] ])

This function looks for data in the string `s` that matches the string `p. If it is successfully found, it will return the start and end positions of the string `p` in the string `s`; If it is not found, it will return`nil`。

The third parameter `init` defaults to `1`, which indicates that the match starts from the first character. When `init` is a negative number, it indicates that the match starts from the last `-init` character of the `s` string.

The fourth parameter `plain` defaults to `false`. When it is `true`, it will only treat `p` as a string.

> You may wonder, is there any need for the fourth parameter? Isn't p supposed to be a string?？
In fact, the default meaning of matching in lua is regular matching, and the regular here is slightly different from other languages.

> Due to space constraints, this section and the following sections will not consider the use of regular expressions when it comes to matching content, and the Lua regular tutorial will be listed in detail separately in the last few sections.

When the fourth parameter is `true`, the regular function is not used.

Sample code：

```lua
--will only match up to the first
print(string.find("abc abc", "ab"))
-- Match string starting at index 2：ab
print(string.find("abc abc", "ab", 2))
-- Match string starting at index 5：ab
print(string.find("abc abc", "ab", -3))

-->Output
--1  2
--5  6
--5  6
```

**Please complete the following tasks：**

- known string s, with many identical strings inside
- Please find the position of all strings awsl in string s.
- Use print to print the results, one for each line.
- such as string 12awslawslaw, output 3 and 7

```lua
s = "12awsaslwlaawsllslllswasllalssawwlawslaw"
print("The positions of the two awsl are：")
--completion code
```

--------

### string.gsub

string.gsub(s, p, r [, n])

Replace all substrings `p` in target string `s` with strings r。

Optional parameter `n`, which means to limit the number of replacements.

There are two return values, the first is the replaced string and the second is how many times it has been replaced.

> Special note: the target string s of this function also supports regular

Below is an example：

```lua
print(string.gsub("Lua Lua Lua", "Lua", "hello"))
print(string.gsub("Lua Lua Lua", "Lua", "hello", 2)) --Indicates the fourth parameter

-->Printed Results：
-- hello hello hello   3
-- hello hello Lua     2
```

Similarly, we can also use colons to simplify the syntax, like the following：

```lua
s = "12345"
r = s:gsub("2","b")
print(r)
```

**Please complete the following tasks：**

- Known string variable s, please print out separately (one line for each）：
- Replace the first 5 a's in the string s b
- Replace the first 3 c's in the string s xxx
- Print the results, a line of data

```lua
s = "asdicagydausckfugdaflgscdabgsdbahhacbshbsd"
print("s Value before transformation：",s)
--completion code
```

--------

### Regular matching related content waiting for update

-------

## Cross-file call

When writing code, as the logic becomes more complex, our code volume will become larger. Although there are `functions` to encapsulate part of the code logic, it is obviously not a good idea to put all the code in one file.

So we need to put some code into different files and distinguish the functions of these codes through files.

For example, we have the following function：

```lua
---Function Function：
-- Generate from 1-max table
-- @Input value: maximum value of table
-- @Return: table Results
-- @Examples：  local list = getNumberList(10)
function getNumberList(max)
    local t = {}
    for i=1,max do
        table.insert(t,i)
    end
    return t
end
```

Let`s create a new file called` tools.lua` and put this function in it. Now, the whole file is as follows：

`tools.lua`

```lua
---Function Function：
-- Generate from 1-max table
-- @Input value: maximum value of table
-- @Return: table Results
-- @Examples：  local list = getNumberList(10)
local function getNumberList(max)
    local t = {}
    for i=1,max do
        table.insert(t,i)
    end
    return t
end

--Manually returns a table containing the above function
return {
    getNumberList = getNumberList,
}
```

Now, the function we encapsulated can be called in other files. The specific code is as follows：

```lua
--Reference the tools.lua file and load
local tool = require("tools")

local list = tool.getNumberList(12)
```

When the` require` interface is called, Lua virtual machine will automatically load the file you call, `execute the contents of the file`, and then` return the result of return in your file`。

To better understand this passage, we can look at the following two files, where run.lua is the entry file that is run.

`test.lua`

```lua
--In order to return to use for a while table
local temp = {}

--changed the global variable a
a = 1

--local Variable cannot be called externally
--but can be called within a file
local b = 2

--When the file is require, it will be executed.
--changed the global variable c
c = a + b

--Make the function in table
function temp.addB()
    --Variables can be called inside the file b
    b = b + 1
    return b
end

--Return table
return temp
```

`run.lua`

```lua
local test = require("test")
print(a)--Output 1
print(b)--Output nil because B is a local variable
print(c)--Output 3

print(test.addB())--Output 3
print(test.addB())--Output 4
print(test.addB())--Output 5
```

At the same time, each file will only be `require` once at most, if there are multiple `require`, only the first time will be executed.

## To be added

- table  -> sort Sort
- string -> regular、gsub、find、match
- Iterator  for xx in xx  、string.gmatch
- Meta Table
- coroutine

↓Various interfaces↓

- math Library
- os Library
- io Library
- bit operation （>> <<）
- Miscellaneous Interface
- debug Library

<script>
window.onload = function(){
    //Add quick test code link near code block
    $("pre").each(function () {
        if($(this).text().indexOf("print") >= 0)
            $(this).before('<a class="run-code-btn" href="https://openluat.github.io/luatos-wiki-en/_static/luatos-emulator/lua.html?'+escape($(this).text())+'" target="_blank">Point me to quickly test the following code</a>');
    });
}
</script>
