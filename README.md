SeleniumExtensions
==================

[![Build status](https://ci.appveyor.com/api/projects/status/5ll4qq8v24c6cvjh)](https://ci.appveyor.com/project/rcasady616/seleniumextensions) [![Selenium Test Status](https://saucelabs.com/browser-matrix/richardcasady.svg)](https://saucelabs.com/u/richardcasady)

Selenium Extensions is a project that contains commonly use functionality that is missing in Selenium2 WebDriver. The functionalities in this project are not specific to any single web application. 

## Key Features ##
* IWebElements 
 * 14 WaitUntil conditions like: WaitUntilExists, WaitUntilVisible
 * ElementExists method 
* IWebDriver 
 * Switching browser windows
 * Alert handling 
* HTML typed Elements
 * TableElement, takes the complexity out of tables
* Easy Browser and WebDriver management
 * Local Drivers
 * Selenium Grid
 * SauceLabs 

## Description ##
These Selenium Extensions contains commonly use functionality for any web site 

## Requirements ##
* Visual Studio 2008
* .NET Framework 4
* NuGet

## Build ##
To build you will need to get the NuGet packages for the project and build in the Visual Studio

## Tests ##
You will need to download the Selenium Server witch you can get from here http://docs.seleniumhq.org/download/ Its required for running some of the tests
