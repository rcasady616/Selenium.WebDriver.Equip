#tool "nuget:?package=NUnit.ConsoleRunner"
#tool "nuget:?package=OpenCover"
#tool coveralls.net
#addin Cake.Coveralls

var configuration="Release";
var solution="./Selenium.WebDriver.Equip.sln";
var testProjectDir="./Selenium.WebDriver.Equip.Tests/bin/" + configuration;
var projProjectDir="./Selenium.WebDriver.Equip/bin/" + configuration;
var dirNugetPackage="./nuget";
var dirTestResults="./TestResults";
var envVars = EnvironmentVariables();

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
   if (!DirectoryExists(dirTestResults))
    {
        CreateDirectory(dirTestResults);
    }
    OpenCover(tool => {
  tool.NUnit3(testProjectDir + "/*.Tests.dll",
  new NUnit3Settings {
    WorkingDirectory = testProjectDir,
    Results = dirTestResults + "/Selenium.WebDriver.Equip.Tests.xml"
    });
  },
  new FilePath("./OcResult.xml"),
  new OpenCoverSettings()
    .WithFilter("+[Selenium.WebDriver.Equip]*")
    .WithFilter("+[Selenium.WebDriver]*")
    .WithFilter("+[Equip]*"));
});

Task("Test_s")
.Does(()=>{
    if (!DirectoryExists(dirTestResults))
    {
        CreateDirectory(dirTestResults);
    }
    OpenCover(tool => {
  tool.NUnit3(testProjectDir + "/*.Tests.dll",
  new NUnit3Settings {
    Test = "Selenium.WebDriver.Equip.Tests.IPageTests.TestIsPageLoaded",
    WorkingDirectory = testProjectDir,
    Results = dirTestResults + "/Selenium.WebDriver.Equip.Tests.xml"
    });
  },
  new FilePath("./OcResult.xml"),
  new OpenCoverSettings()
    .WithFilter("+[Selenium.WebDriver.Equip]*")
    .WithFilter("+[Selenium.WebDriver]*")
    .WithFilter("+[Equip]*"));
});

Task("Test_BuildServer")
.Does(()=>{
  if (!DirectoryExists(dirTestResults))
    {
        CreateDirectory(dirTestResults);
    }
OpenCover(tool => {
  tool.NUnit3(testProjectDir + "/*.Tests.dll",
  new NUnit3Settings {
    Test = "Selenium.WebDriver.Equip.Tests.Elements,Selenium.WebDriver.Equip.Tests.Extensions,Selenium.WebDriver.Equip.Tests.PageNotLoadedExceptionTests",
    WorkingDirectory = testProjectDir,
    Results = dirTestResults + "/Selenium.WebDriver.Equip.Tests.xml"
    });
  },
  new FilePath("./OcResult.xml"),
  new OpenCoverSettings()
    .WithFilter("+[Selenium.WebDriver.Equip]*")
    .WithFilter("+[Selenium.WebDriver]*")
    .WithFilter("+[Equip]*"));
});

Task("Package")
  .Does(()=>{

    if (!DirectoryExists(dirNugetPackage))
    {
        CreateDirectory(dirNugetPackage);
    }
    
    var assemblyInfo = ParseAssemblyInfo("./Selenium.WebDriver.Equip/Properties/AssemblyInfo.cs");
    var nuGetPackSettings   = new NuGetPackSettings {                              
                                Version                 = assemblyInfo.AssemblyFileVersion,
                                Copyright               = "EQUIP 2016",
                                // ReleaseNotes            = new [] {"Bug fixes", "Issue fixes", "Typos"},
                                OutputDirectory         = "./nuget"
                                };
            
    NuGetPack("./Selenium.WebDriver.Equip/Selenium.WebDriver.Equip.Package.nuspec", nuGetPackSettings);
    NuGetPack("./Equip/Equip.Package.nuspec", nuGetPackSettings);
  });

  Task("LoadCoverage")
  .Does(()=>{
    string key;
    envVars.TryGetValue("COVERALLS_ACCESSKEY", out key);
    CoverallsIo("./OcResult.xml", new CoverallsIoSettings()
    {
        RepoToken = key
    });
  });

Task("Clean")
  .Does(()=>{
    CleanDirectories(dirNugetPackage);
    CleanDirectories(dirTestResults);
  });

RunTarget(target);
