Properties {
    $config = "Debug"
    $dir_base = resolve-path ..
    $dir_build = "$dir_base\build"
    $dir_sweTests = "$dir_base\Selenium.WebDriver.Equip.Tests"
    $dir_sweTestsoutput = "$dir_sweTests\bin\$config"
    $dir_teststage = "$dir_build\teststage"
    $dir_testrun = "$dir_teststage\testrun"
    $dir_latesttests = "$dir_teststage\tests"
    $dir_latestrelease = "$dir_teststage\release"
    $file_config = "$dir_build\Tests.config"
    [xml] $xmlConfig = $null
}

Task default -Depends Build, Test

Task Build { 
    #assert (Test-Path $file_config) "File not found, file name: $file_config"
    if((Test-Path $file_config) -eq $false) {$(throw "Config file dosent exist")}
    $xmlconfig = Get-Content $file_config
}
#'C:\Users\Rick\Documents\GitHub\SeleniumExtensions\Selenium.WebDriver.Equip\bin\Debug C:\Users\Rick\Documents\GitHub\SeleniumExtensions\build\teststage\tests' because it does not exist.
function MakeCopies()
{
    Create-Directory($dir_teststage);
    Create-Directory($dir_latesttests);
    Write-Host "swe dir $dir_sweoutput"
    Copy-Files "$dir_sweTestsoutput\*" $dir_latesttests 
    # manually get the release binaries place them in the Release folder
    Copy-Files $dir_latestrelease $dir_latesttests
     #assert (Test-Path $file_config) "File not found, file name: $file_config"
    if((Test-Path $file_config) -eq $false) {$(throw "Config file dosent exist")}
    [xml] $xmlconfig = Get-Content $file_config
    $config = $xmlconfig.config

    foreach($con in $config.ChildNodes)
	{
        $element = $($con.Get_Name())
		#Write-Host "Processing: $element"
      #  Write-Host "$element"
       # Write-Host "$($item.version)"
        $d = "$dir_testrun";
        Create-Directory($d);
		#$con = $($item.Get_Name())
		#Write-Host "Processing: $con"
		if($con -eq "#comment"){continue}
        $os= "$d\$($con.os)"
        $os = $os.Replace(' ','_')
		#Write-Host "Processing: $os"
        Create-Directory($os);
		$browser= "$os/$($con.browser)"
        $browser = $browser.Replace(' ','_')
        Create-Directory($browser);
		$version= "$browser/$($con.version)"
        Create-Directory($version);
        Copy-Files $dir_latesttests $version
        
	}
}

function Test-CommandExists($command){
    ((Get-Command $command -ea SilentlyContinue) | Test-Path) -contains $true
}

Task Test {
    MakeCopies
    #Write-Host "Hello, World!" 

}

### stuff ###
function Create-Archive($name) {
    Write-Host "Creating archive '$name.zip'..." -ForegroundColor "Green"
    Remove-Directory $temp_dir
    Create-Zip "$build_dir\$name.zip" "$build_dir"
}

function Create-Zip($file, $dir){
    if (Test-Path -path $file) { Remove-Item $file }
    Create-Directory $dir
    Exec { & $7zip a -mx -tzip $file $dir\* } 
}

function Create-Directory($dir) {
    if((Test-Path "$dir") -eq $false)
	{
		Write-Host "Creating $dir"
		New-Item -Path $dir -Type Directory -Force > $null
    }
}

function Clean-Directory($dir) {
    If (Test-Path $dir) {
        Write-Host "Cleaning up '$dir'..." -ForegroundColor "DarkGray"
        Remove-Item "$dir\*" -Recurse -Force
    }
}

function Remove-File($file) {
    if (Test-Path $file) {
        Write-Host "Removing '$file'..." -ForegroundColor "DarkGray"
        Remove-Item $file -Force
    }
}

function Remove-Directory($dir) {
    if (Test-Path $dir) {
        Write-Host "Removing '$dir'..." -ForegroundColor "DarkGray"
        Remove-Item $dir -Recurse -Force
    }
}

function Copy-Files($source, $destination) {
		Write-Host "Source $source"
    assert (Test-path $source) "fail"
    Copy-Item "$source" $destination -Force > $null
}

function copy_files($source,$destination,$exclude=@()){    
    #create_directory $destination
    Get-ChildItem $source -Recurse -Exclude $exclude | Copy-Item -Destination {Join-Path $destination $_.FullName.Substring($source.length)} 
}

function Move-Files($source, $destination) {
    Move-Item "$source" $destination -Force > $null
}

function Replace-Content($file, $pattern, $substring) {
    (gc $file) -Replace $pattern, $substring | sc $file
}