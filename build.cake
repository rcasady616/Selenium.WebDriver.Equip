#tool "nuget:?package=NUnit.ConsoleRunner"

var configuration="Debug";

var target = Argument("target", "Default");

Task("Default")
  .IsDependentOn("Test");

Task("Build")
  .Does(() =>
{
  MSBuild("./Selenium.WebDriver.Equip.sln", new MSBuildSettings {
    Verbosity = Verbosity.Minimal,
    ToolVersion = MSBuildToolVersion.VS2015,
    Configuration = configuration,
    PlatformTarget = PlatformTarget.MSIL
    });
});

Task("Test")
.IsDependentOn("Build")
.Does(()=>{
  NUnit3("./Selenium.WebDriver.Equip.Tests/bin/" + configuration + "/*.Tests.dll");
});

RunTarget(target);