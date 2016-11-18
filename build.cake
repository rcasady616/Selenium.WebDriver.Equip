#tool "nuget:?package=NUnit.ConsoleRunner"

var configuration="Debug";
var solution="./Selenium.WebDriver.Equip.sln";
var testProjectDir="./Selenium.WebDriver.Equip.Tests/bin/" + configuration;

var target = Argument("target", "Default");

Task("Default")
  .IsDependentOn("Build");

Task("Build")
  .Does(() =>
{
  NuGetRestore(solution);
  MSBuild(solution , new MSBuildSettings {
    Verbosity = Verbosity.Minimal,
    ToolVersion = MSBuildToolVersion.VS2015,
    Configuration = configuration,
    PlatformTarget = PlatformTarget.MSIL
    });
});

Task("Test_all")
.Does(()=>{
  NUnit3(testProjectDir + "/*.Tests.dll",
  new NUnit3Settings {
    WorkingDirectory = testProjectDir
    });
});

Task("Test_s")
.Does(()=>{
  NUnit3(testProjectDir + "/*.Tests.dll",
  new NUnit3Settings {
    Test = "GetSauceTest",
    WorkingDirectory = testProjectDir
    });
});

Task("Test_BuildServer")
.Does(()=>{
  NUnit3(testProjectDir + "/*.Tests.dll",
  new NUnit3Settings {
    Test = "Selenium.WebDriver.Equip.Tests.Elements,Selenium.WebDriver.Equip.Tests.Extensions,Selenium.WebDriver.Equip.Tests.PageNotLoadedExceptionTests",
    WorkingDirectory = testProjectDir
    });
});
RunTarget(target);