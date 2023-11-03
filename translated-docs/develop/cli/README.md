# LuatOS CLI - Command Line Tools

In order to meet the project management, package management, IDE integration and many other tool chain problems, is now designed.LuatOS CLI

## Terminology

* CLI - Command-line tools, different from GUI graphical interface
* Project Management-refers to the management of "user/client" projects, from the perspective of the user, not from the perspective of the LuatOS developer.
* Package management-firmware management, library file management, dependency management
* IDE Integration-Provide identifiable logs or interactive interfaces (such as socket and http) that are easy to analyze by the IDE.

## Basic Design Principles

* Based on command line
* Commands should be concise, unambiguous and require only necessary parameters
* Repeatable, testable
* Harmless, non-destructive, shall not cause damage to the user code
* The version is controllable, and customers are urged to use tools such as git/svn.

