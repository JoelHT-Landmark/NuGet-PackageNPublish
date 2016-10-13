@echo on
REM // Build the T4 include file from the C# classes in the T4Support folder.
REM // This is necessary because the dll itself (NuGetPackageAndPublish.dll) 
REM // cannot be referenced in a T4 file - it is part of the build and therefore
REM // the dll will not be available until the build is complete- i.e. too late!
REM // Note a new line is not necessary but eaids readability

set SOLUTION_DIRECTORY=..\

set BUILD_DIRECTORY=%SOLUTION_DIRECTORY%T4Builder\
set TT_FILE=%BUILD_DIRECTORY%NuGetPackageAndPublish.tt.include

echo SOLUTION_DIRECTORY=%SOLUTION_DIRECTORY%  
echo BUILD_DIRECTORY=%BUILD_DIRECTORY%
echo TT_FILE=%TT_FILE%

REM // Add any necessary references for the T4 
type %BUILD_DIRECTORY%start.tt.txt > %TT_FILE%

REM // Add the Project class
type %SOLUTION_DIRECTORY%NuGet.PackageNPublish.NuGetPackage\T4Support\ProjectProperties.cs >> %TT_FILE%

REM // Add the Assembly class
type %SOLUTION_DIRECTORY%NuGet.PackageNPublish.NuGetPackage\T4Support\AssemblyProperties.cs >> %TT_FILE%

REM // Add the Package class
type %SOLUTION_DIRECTORY%NuGet.PackageNPublish.NuGetPackage\T4Support\PackageProperties.cs >> %TT_FILE%

REM // Add the controlling class
type %SOLUTION_DIRECTORY%NuGet.PackageNPublish.NuGetPackage\T4Support\NuGetPackageAndPublish.cs >> %TT_FILE%

REM // Finish the template file
type %BUILD_DIRECTORY%end.tt.txt >> %TT_FILE%

REM // Copy the generated tt file to all necessary locations
copy %TT_FILE% %SOLUTION_DIRECTORY%NuGet.PackageNPublish.ProjectTemplate\_MSBuild
copy %TT_FILE% %SOLUTION_DIRECTORY%NuGet.PackageNPublish.NuGetPackage\_MSBuild
copy %TT_FILE% %SOLUTION_DIRECTORY%NuGet.PackageNPublish.NuGetPackage\Package\content\_MSBuild
copy %TT_FILE% %SOLUTION_DIRECTORY%NuGet.PackageNPublish.NuGetPackageTests\_MSBuild

REM // Copy the example tt file to the package
copy %SOLUTION_DIRECTORY%NuGet.PackageNPublish.ProjectTemplate\Project.NuGetPackage.tt %SOLUTION_DIRECTORY%NuGet.PackageNPublish.NuGetPackage\Package\content\NuGetPackageAndPublish.tt.Example
pause
