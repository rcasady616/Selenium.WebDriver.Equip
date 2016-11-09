![Equip Logo](http://rickcasady.com/SeleniumExtentions/v1.0/TestWebPages/EquipLogo.svg) [![Build Status](https://saucelabs.com/browser-matrix/richardcasady.svg)](https://saucelabs.com/beta/builds/0592bfbc727a45ae9562cc25319ce9f6)
Selenium.WebDriver.Equip 
==================

[![Build status](https://ci.appveyor.com/api/projects/status/5ll4qq8v24c6cvjh)](https://ci.appveyor.com/project/rcasady616/seleniumextensions) [![NuGet Badge](https://buildstats.info/nuget/Selenium.WebDriver.Equip)](https://www.nuget.org/packages/Selenium.WebDriver.Equip/) [![Build Status](https://saucelabs.com/buildstatus/richardcasady?saucy)](https://saucelabs.com/beta/builds/0592bfbc727a45ae9562cc25319ce9f6) 

Selenium.WebDriver.Equip or Equip for short is a project that contains commonly use functionality that is missing in Selenium WebDriver. The functionalities in this project are not specific to any single web application. 

## NuGet ##
NuGet package URL https://www.nuget.org/packages/Selenium.WebDriver.Equip/
To install the lastest Selenium.WebDriver.Equip , run the following command in the Package Manager Console
```powershell
PM> Install-Package Selenium.WebDriver.Equip
```

## Key Features ##
* IWebElements 
 * WaitUntil conditions like: WaitUntilExists, WaitUntilVisible
 * ElementExists method 
* IWebDriver 
 * Switching browser windows
 * Alert handling 
* HTML typed Elements
 * Table, List, Checkbox
* Easy Browser and WebDriver management
 * Local Drivers
 * Selenium Grid
 * SauceLabs 

## Description ##
These Selenium.WebDriver.Equip contains commonly use functionality for any web site 

## Requirements ##
* Visual Studio 2015 free Community Edition 
* .NET Framework 4.6
* NuGet

## Build ##
To build you will need to get the NuGet packages for the project and build in the Visual Studio

## Tests ##
To run Sauace Labs account you need to add you userName and access keys to t he enviroment varables 
You will need to download the Selenium Server witch you can get from here http://docs.seleniumhq.org/download/ Its required for running some of the tests
