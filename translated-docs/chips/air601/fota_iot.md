# Upgrade with the co-IoT platform

Although this article is in the Air601 directory, it is actually applicable to any other module..

This article may be relatively long and has many details. Please read it patiently.

## Reasons for using the IOT platform and some problems

1. First of all, it is currently free and its stability is good.
2. The rules are a bit complicated and strange, and the previous documents are not well described..

## What is the upgrade process?

A typical upgrade process would look like this:

1. The user registers an account on the IoT platform and creates a project.
2. Get the key of the project, called "PRODUCT_KEY" in lua code, and the URL parameter is `project_key`
3. libfota/libfota2 libraries, synthetic upgraded URL
4. http The library accesses the URL, downloads the upgrade file, and automatically writes it to a specific ota/fota area.
5. Restart, module enters upgrade mode
6. Automatically complete the upgrade, end of process

Step by step to speak below

### IOT The platform registers an account and creates a project.

1. Visit the website [Heszhou IOT Platform](https://iot.openluat.com/)
2. Click the big "IoT" button to enter the login interface.
3. Then the ordinary registration/login process, no account to register an account, forget the password to retrieve the password, this belongs to the basic operation, not to expand.
4. After logging in, click "New Project", select a name, fill in the input box of "Project Name", and click "Submit". If there is no accident, a new project will be added.

### Get the item's key

1. After the previous login operation is completed, find the line of the project name you need in the project list.
2. In the "item key" column of the list, click "view/copy key" to get it, which will be used later.

### synthetic upgrade URL

This is a bit of a technical description. If you only use the libfota/libfota2 library, find demo, modify the' PRODUCT_KEY 'value in the code, and then swipe the machine.

With the IOT platform, a typical upgrade URL is like this
```
http://iot.openluat.com/api/site/firmware_upgrade?imei=C81234567830&project_key=b0wlT3yrmIpTSPLi3e7Fob5nvBJxO6pM&firmware_name=fotademo_LuatOS-SoC_AIR601&version=1025.1.0
```

This URL is divided into several parts:

1. URL The main body 'http://iot.openluat.com/api/site/firmware_upgrade', visible is http protocol, domain name is' iot.openluat.com', path is `/api/site/firmware_upgrade`
2. The parameter' imei', although the name is imei, can actually be IMEI/MAC address, etc., as long as it can uniquely identify a device
3. parameter 'project_key', obtained in the previous step, is used to identify which project of "co-account iot platform"
4. the parameter "firmware_name", which is called "firmware name" on the "cozhou iot platform", will be synthesized in a rather strange way in lua.
5. Parameter 'version', version number, three-segment type, maximum of each segment 999

### Access the URL, download the upgrade file, and write it to a specific ota/fota region.

This is also done by libfota/libfota2. The technical details are as follows:

1. Access the previously synthesized URL to determine the server's response code
2. If> 299, it means there is no need to upgrade and exit directly.
3. Start downloading data to the local and writing data to the specified ota/fota region
4. Check while writing. If the check is wrong, exit directly.
5. Write complete, verify complete, execute callback, notify user code
6. The user code decides the timing of the restart, and of course it can restart immediately.

### Restart, enter upgrade mode

This is a matter completed by the bottom code. It will not be expanded. If you are interested, please check the source code of each module and luat_fota the function at the beginning.

## So, where are the pits? What are the pits?

### Equipment Ownership Issues

The IOT platform, an IMEI (actually calculated by the imei parameter in the URL), can only belong to one item of one account.

Take the device with IMEI of' 861234 'as an example, user' AAA' registers the account and create' Project PPP', and then uses the' Project key' of the project to make an upgrade request, then:

1. The IMEI does not exist under the "any project" of "any customer". the first time the equipment initiates an upgrade request, it will "automatically" become the equipment under the "PPP" project of "AAA" customer.
2. The IMEI is under any item in the account of user 'BBB', and the server returns "25 no permission"-account does not match
3. The IMEI is under the 'ZZZZZ' item of the user's 'AAA' account, and the server returns "26 invalid items"-the items do not match.

### Problems with Upgrade Package File Name and URL Firmware Name Parameters

The "firmware_name" parameter reported must be consistent with the "firmware name" in the upgrade package upload interface, otherwise the "27 invalid firmware" will be reported.`

The only function of the file name of the upgrade package is to automatically identify 2 parameters in the "Create Firmware" dialog box in the "My Firmware" interface.

Look at the picture! [Upload screenshots of the firmware interface](img/ota_upload.jpg)

Reminder:

1. The parameter corresponding to the firmware name in the figure is' firmware_name ', which can be modified
2. The parameter corresponding to the version number in the figure is' version', which can also be modified.
3. By default, these two parameters are automatically generated based on the file name. However, due to various historical problems, the LuaTools synthesized file name may not meet the requirements
4. If the upgrade fails to prompt '27 invalid firmware', the first thing to check is the' firmware_name 'parameter, then the' version' parameter

In the most flexible terms, this is the case.:

1. The file name of the upgrade file can be started casually, such as 123.bin, as long as it is bin suffix, and then it must be a real upgrade file.
2. After selecting the file in the upload interface, the "firmware name" and "version number" cannot be identified naturally, and can only be filled in manually.
3. Fill in the required Firmware Name and Version Number"

### What's wrong with the parameters of the module?

Upgrade URL, a total of 4 parameters`imei`/`project_key`/`firmware_name`/`version`

1. Using libfota library, only project_key can be configured, others are automatically generated and identified
2. Using the libfota2 library, 4 parameters can be customized configuration

So, if:

1. I just can't understand the generation law of firmware_name and version parameters in the libfota library.
2. Or you don't like that rule at all and want to customize it.

Then, use the libfota2 library to ensure that the "firmware_name" and "version" parameters are customized and consistent with the "firmware name" and "version number" on the interface, so there is no problem.

A typical log for a libfota2 library is like this:

```
[2024-04-10 13:44:46.559] I/user.fota.url	GET	http://iot.openluat.com/api/site/firmware_upgrade?imei=C81234567830&project_key=b0wlT3yrmIpTSPLi3e7Fob5nvBJxO6pM&firmware_name=fotademo_LuatOS-SoC_AIR601&version=1.2.99
[2024-04-10 13:44:46.559] I/user.fota.imei	C81234567830
[2024-04-10 13:44:46.559] I/user.fota.project_key	b0wlT3yrmIpTSPLi3e7Fob5nvBJxO6pM
[2024-04-10 13:44:46.559] I/user.fota.firmware_name	fotademo_LuatOS-SoC_AIR601
[2024-04-10 13:44:46.559] I/user.fota.version	1.2.99
```

Reviewing these four parameters again:

1. imei Is the device identification number, can be the imei of 4G equipment, can also be wifi mac address, can also be other unique identification number, or you write 12345, unique
2. project_key It is the key of the joint IOT platform project, and each "joint IOT project" has a unique value.
3. firmware_name It is the firmware name of the IOT platform and the "firmware name" on the interface. it is not strongly related to the file name.
4. version Is the firmware version number of the joint IOT platform, and is the "version number" on the interface."

## Say a few more words about the upgrade file

### 8910 module of the scheme, such Air724UG, Air722UG, Air820UG

1. When LuaTools the production file, check "include core" and "include script""
2. in the production file directory, you can find files with the '.bin' suffix. this is the upgrade file to be uploaded on the iot platform.

### EC618/EC718 module of the scheme, such Air780E,Air780EP

1. Only upgrade the script, select '.bin' in the production folder, but require the bottom layer to be completely consistent
2. Upgrade the script and the bottom layer, prepare the new and old SOC files, then select "SoC differential upgrade tool" in the menu to generate a new '.bin' file, and then upload it

### XT804 Programmes, such Air601,Air101,Air103

1. For the upgrade script, select '.bin' in the production folder. The firmware enables OTA function and will use the file system to store the upgrade package.
2. If the FOTA function is enabled at the bottom layer, select the '.fota' file, change the suffix and upload it. remember to change the "firmware name" and "version number" and use the exclusive FOTA area to store the upgrade package.
3. if you only upgrade the underlying script and the firmware has the FOTA function enabled, you can select the '.sota' file, change the suffix to. bin and upload it to the iot platform, but it is usually not required.

Note that the default firmware released is without FOTA, because it takes up a lot of space and there are few libraries that can be enabled.

if you really need the fota function, please compile a copy

## What a trouble, then I will build my own OTA server

Yes, but you need to know some skills, such:

1. What is HTTP and Server?
2. Learn how to handle http parameters
3. Understand point interface programming

For the module, the process of OTA/FOTA is to request URL, continue to download data and verify it if it is 200, and exit if it is not 200..

Can't understand? Forget it
