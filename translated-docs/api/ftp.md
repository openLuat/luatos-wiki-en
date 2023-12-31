# ftp - ftp Client

{bdg-success}`Adapted` {bdg-primary}`Air101/Air103` {bdg-primary}`Air601` {bdg-primary}`Air105` {bdg-primary}`ESP32C3` {bdg-primary}`ESP32S3` {bdg-primary}`Air780E/Air700E` {bdg-primary}`Air780EP`

```{note}
This page document is automatically generated by [this file](https://gitee.com/openLuat/LuatOS/tree/master/luat/../components/network/libftp/luat_lib_ftp.c). If there is any error, please submit issue or help modify pr, thank you！
```

```{tip}
This library has its own demo,[click this link to view ftp demo examples](https://gitee.com/openLuat/LuatOS/tree/master/demo/ftp)
```

## ftp.login(adapter,ip_addr,port,username,password)



FTP Client

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|int|The adapter serial number can only be socket.ETH0, socket.STA, socket.AP. If it is not filled in, the platform's own method will be selected, and then the last registered adapter will be selected.|
|string|ip_addr Address|
|string|port port, default 21|
|string|username User Name|
|string|password Password|
|bool/table|whether it is an ssl encrypted connection, default is no encryption, true is the simplest encryption without certificate, table is encrypted with certificate <br>server_cert server ca certificate data <br>client_cert client ca certificate data <br>client_key client private key encrypted data <br>client_password client private key password data|

**Return Value**

|return value type | explanation|
|-|-|
|bool/string|Return true on success Return true on failure string|

**Examples**

```lua
ftp_login = ftp.login(nil,"xxx")

```

---

## ftp.command(cmd)



FTP Command

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|cmd Commands currently supported:NOOP SYST TYPE PWD MKD CWD CDUP RMD DELE LIST|

**Return Value**

|return value type | explanation|
|-|-|
|string|Return true on success Return true on failure string|

**Examples**

```lua
    print(ftp.command("NOOP").wait())
    print(ftp.command("SYST").wait())
    print(ftp.command("TYPE I").wait())
    print(ftp.command("PWD").wait())
    print(ftp.command("MKD QWER").wait())
    print(ftp.command("CWD /QWER").wait())
    print(ftp.command("CDUP").wait())
    print(ftp.command("RMD QWER").wait())
	print(ftp.command("DELE /1/12222.txt").wait())

```

---

## ftp.pull(local_name,remote_name)



FTP File Download

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|local_name Local File|
|string|remote_name Server Files|

**Return Value**

|return value type | explanation|
|-|-|
|bool/string|Return true on success Return true on failure string|

**Examples**

```lua
ftp.pull("/1222.txt","/1222.txt").wait()

```

---

## ftp.push(local_name,remote_name)



FTP File Upload

**Parameters**

|Incoming Value Type | Explanation|
|-|-|
|string|local_name Local File|
|string|remote_name Server Files|

**Return Value**

|return value type | explanation|
|-|-|
|bool/string|Return true on success Return true on failure string|

**Examples**

```lua
ftp.push("/1222.txt","/1222.txt").wait()

```

---

## ftp.close()



FTP Client Shutdown

**Parameters**

None

**Return Value**

|return value type | explanation|
|-|-|
|bool/string|Return true on success Return true on failure string|

**Examples**

```lua
ftp.close().wait()

```

---

