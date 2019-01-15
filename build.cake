#tool "nuget:?package=NUnit.ConsoleRunner"
#tool "nuget:?package=OpenCover"
//#tool coveralls.net
//#addin Cake.Coveralls

var configuration="Release";
var solution="./Selenium.WebDriver.Equip.sln";
var testProjectDir="./Selenium.WebDriver.Equip.Tests/bin/" + configuration;
var projProjectDir="./Selenium.WebDriver.Equip/bin/" + configuration;
var dirNugetPackage="./nuget";
var dirTestResults="./TestResults";
var dirReleaseTesting = "./ReleaseTesting";
var envVars = EnvironmentVariables();
var WhereNotLocalTests = "cat != LocalOnly and cat != HeadLess";

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
    OutputFile = dirTestResults + "/Selenium.WebDriver.Equip.Tests.xml"
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
    Test = "Selenium.WebDriver.Equip.Tests.Elements,Selenium.WebDriver.Equip.Tests.Extensions,Selenium.WebDriver.Equip.Tests.PageNotLoadedExceptionTests",
    WorkingDirectory = testProjectDir,
    OutputFile = dirTestResults + "/Selenium.WebDriver.Equip.Tests.xml"
    });
  },
  new FilePath("./OcResult.xml"),
  new OpenCoverSettings()
    .WithFilter("+[Selenium.WebDriver.Equip]*")
    .WithFilter("+[Selenium.WebDriver]*")
    .WithFilter("+[Equip]*"));
});

Task("Test_LocalOnly")
.Does(()=>{
    if (!DirectoryExists(dirTestResults))
    {
        CreateDirectory(dirTestResults);
    }
    OpenCover(tool => {
  tool.NUnit3(testProjectDir + "/*.Tests.dll",
  new NUnit3Settings {
    Test = "Selenium.WebDriver.Equip.Tests.IWebDriverFactoryTests",
    WorkingDirectory = testProjectDir,
    OutputFile = dirTestResults + "/Selenium.WebDriver.Equip.Tests.xml"
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
var resultFile = dirTestResults + "/Selenium.WebDriver.Equip.Tests.xml";    
OpenCover(tool => {
  tool.NUnit3(testProjectDir + "/*.Tests.dll",
  new NUnit3Settings {
    Test = "Selenium.WebDriver.Equip.Tests.Elements,Selenium.WebDriver.Equip.Tests.Extensions,Selenium.WebDriver.Equip.Tests.PageNotLoadedExceptionTests",
    Where = WhereNotLocalTests, // this removes all test catagories that cant run on build server
    WorkingDirectory = testProjectDir,
    //OutputFile = resultFile,
    Results = new[] {new NUnit3Result { FileName = resultFile }}
    Workers = 5
    });
  },
  new FilePath("./OcResult.xml"),
  new OpenCoverSettings()
    .WithFilter("+[Selenium.WebDriver.Equip]*")
    .WithFilter("+[Selenium.WebDriver]*")
    .WithFilter("+[Equip]*"));

if (AppVeyor.IsRunningOnAppVeyor) {
  AppVeyor.UploadTestResults(resultFile, AppVeyorTestResultsType.NUnit3);
}
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
    //CoverallsIo("./OcResult.xml", new CoverallsIoSettings()
    //{
    //    RepoToken = key
    // #});
  });

Task("TestRelease")
  .IsDependentOn("Build")
  .Does(()=>{
    //copy testProjectDir
     if (!DirectoryExists(dirReleaseTesting))
    {
        CreateDirectory(dirReleaseTesting);
    }
    var testList = "Selenium.WebDriver.Equip.Tests.Elements,Selenium.WebDriver.Equip.Tests.Extensions,Selenium.WebDriver.Equip.Tests.PageNotLoadedExceptionTests";
    CopyFiles(testProjectDir+"/*",  dirReleaseTesting);
    // replace the binaries
    CopyFiles("./ReleaseBinaries/*",  dirReleaseTesting);
     // run the tests chrome
    var file = File(dirReleaseTesting +"/Selenium.WebDriver.Equip.Tests.dll.config");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteBrowserName']/@value", "chrome");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteBrowserVersion']/@value", "54");
    NUnit3(dirReleaseTesting + "/*.Tests.dll",
      new NUnit3Settings {
      Test = testList,
      WorkingDirectory = dirReleaseTesting,
      OutputFile = dirTestResults + "/Selenium.WebDriver.Equip.Tests.Chrome.54.xml",
      StopOnError = false
    });

    // run the tests ie
    file = File(dirReleaseTesting +"/Selenium.WebDriver.Equip.Tests.dll.config");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteBrowserName']/@value", "internet explorer");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteBrowserVersion']/@value", "11.103");
    NUnit3(dirReleaseTesting + "/*.Tests.dll",
      new NUnit3Settings {
      Test = testList,
      WorkingDirectory = dirReleaseTesting,
      OutputFile = dirTestResults + "/Selenium.WebDriver.Equip.Tests.IE.11.xml",
      StopOnError = false      
    });

     // run the tests edge
    file = File(dirReleaseTesting +"/Selenium.WebDriver.Equip.Tests.dll.config");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteBrowserName']/@value", "MicrosoftEdge");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteBrowserVersion']/@value", "14.14393");
    NUnit3(dirReleaseTesting + "/*.Tests.dll",
      new NUnit3Settings {
      Test = testList,
      WorkingDirectory = dirReleaseTesting,
      OutputFile = dirTestResults + "/Selenium.WebDriver.Equip.Tests.Edge.14.xml",
      StopOnError = false      
    });

        // run the tests safari
    file = File(dirReleaseTesting +"/Selenium.WebDriver.Equip.Tests.dll.config");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteBrowserName']/@value", "Safari");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteBrowserVersion']/@value", "10.0");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteOsPlatform']/@value", "OS X 10.11");
    NUnit3(dirReleaseTesting + "/*.Tests.dll",
      new NUnit3Settings {
      Test = testList,
      WorkingDirectory = dirReleaseTesting,
      OutputFile = dirTestResults + "/Selenium.WebDriver.Equip.Tests.Mac.Safari.10.xml",
      StopOnError = false      
    });

        // run the tests safari
    file = File(dirReleaseTesting +"/Selenium.WebDriver.Equip.Tests.dll.config");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteBrowserName']/@value", "Firefox");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteBrowserVersion']/@value", "45.0");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteOsPlatform']/@value", "Linux");
    NUnit3(dirReleaseTesting + "/*.Tests.dll",
      new NUnit3Settings {
      Test = testList,
      WorkingDirectory = dirReleaseTesting,
      OutputFile = dirTestResults + "/Selenium.WebDriver.Equip.Tests.Mac.Safari.10.xml",
      StopOnError = false      
    });
});

Task("Clean")
  .Does(()=>{
    CleanDirectories(dirNugetPackage);
    CleanDirectories(dirTestResults);
    CleanDirectories(dirReleaseTesting);    
  });

Task("TryThis")
  .IsDependentOn("Build")
  .Does(()=>{
    //copy testProjectDir
    if (!DirectoryExists(dirReleaseTesting))
    {
        CreateDirectory(dirReleaseTesting);
    }
    // copy tests
    //CopyFiles(testProjectDir+"/*",  dirReleaseTesting);
    // replace the binaries
    //CopyFiles("./ReleaseBinaries/*",  dirReleaseTesting);
    
    var os="Windows 10";
    var browser = "Firefox";
    var version = "47";
    var testFolder = "./testrun/" + "Win10" + "/" + browser + "/" + version;
    //CreateDirectory(testFolder);
    //CopyFiles(dirReleaseTesting+"/*", testFolder);
     // run the tests chrome
    var file = File(testFolder +"/Selenium.WebDriver.Equip.Tests.dll.config");
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteBrowserName']/@value", browser);
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteBrowserVersion']/@value", version);
    XmlPoke(file, "/configuration/appSettings/add[@key = 'RemoteOsPlatform']/@value", os);
    StartAndReturnProcess("ping 127.0.0.1");
    //StartAndReturnProcess("./tools/NUnit.ConsoleRunner/tools/nunit3-console.exe", 
    //  new ProcessSettings{ Arguments = "'" + testFolder+"/Selenium.WebDriver.Equip.Tests.dll' --test='Selenium.WebDriver.Equip.Tests.PageNotLoadedExceptionTests'"}
    //);
  });

RunTarget(target);
