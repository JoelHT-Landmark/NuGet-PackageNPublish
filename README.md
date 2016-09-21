# NuGet-PackageNPublish

This project creates a Visual Studio extension (VISX) containing Project templates and a packaging MSBuild targets
file to allow easier Package&#39;n&#39;Publish integration with TFS (or other CI) builds.

It was presented at Developer! Developer! Developer! South West 4 ( http://www.dddsouthwest.com ) on 26th May 2012, and
in an updated form at DDDNorth 2 ( http://developerdeveloperdeveloper.com/north2/ ) on 13th October 2012.

#### Current Build Status

 - VSO ![](https://nugetpackagenpublish.visualstudio.com/_apis/public/build/definitions/d270dcb4-c4e4-4ce0-811b-780550fc7bda/1/badge)
 - AppVeyor [![Build status](https://ci.appveyor.com/api/projects/status/5pgdecx34ipxi0tc?svg=true)](https://ci.appveyor.com/project/JoelHT-Landmark/nuget-packagenpublish)
  
## Changes for v0.7

- No longer automatically adds all references by default (See issues 3, 7 & 11 - https://github.com/JoelHT-Landmark/NuGet-PackageNPublish/issues )
- Includes source for "parent" library package by default.
- Updated to use NuGet v2.5 or above

## Requirements

- Visual Studio 2012 SP1.
- Visual Studio 2012 SP1 SDK.
- NuGet Package Manager 2.5 or above.

## Getting Started

There's a handy screencast demonstrating how to get started on YouTube:
- http://www.youtube.com/watch?v=R6e4kV5dfIQ

### How to create a NuGetPackage using the NuGet.Package'n'Publish extension

- Install the extension into Visual Studio 
 - EITHER from a pre-built .visx file
 - OR by building the **NuGet.PackageNPublish.sln** solution (and then installing the generated .visx file)  

- Add a new **NuGet.PackageNPublish** project to your solution, perferably at the Solution folder level - e.g.

    c:\temp\MySolution 
    -- c:\temp\MySolution\MySolution.sln 
    -- c:\temp\MySolution\MyLibrary  
    ---- c:\temp\MySolution\MyLibrary\MyLibrary.csproj 
    -- c:\temp\MySolution\MyLibrary.NuGetPackage 
    ---- c:\temp\MySolution\MyLibrary.NuGetPackage.csproj 

 - Give the project sensible name ending in .NuGetPackage - e.g. "MyLibrary.NuGetPackage"
  - If named like this, then the assembly from the packaging project is removed from the package automatically,

- Add references to the assemblies you want to include in your package
  - By default, if you name your package "MyLibrary.NuGetPackage" (or similar), and reference "MyLibrary" then 
    you're already good to go,
    AND the packaging project will include your "MyLibrary.dll" automagically,
    AND it will find and package the source in a symbols package - again automagically. 
 - Any assembly ending in **.Silverlight.dll** will be put in the **lib/SL40** folder within the package.
 - Any other assembly will be put in the **lib/Net40** folder within the package.
 - Don't forget to add a line to the **.tt** file for the library for any additional references other than the one to "MyLibrary"  

- Add dependencies on other NuGet packages
 - The dependencies will be included automatically in your NuGet package

- Set the **AssemblyTitle**, **AssemblyFileVersion** and **AssemblyDescription** in **Properties\AssemblyInfo.cs**  

- Build the solution

- You now have NuGet (.npkg) and a Symbols (.symbols.nupkg) packages built in your project directory!

### How to publish a package using the NuGet.Package'n'Publish extension

- Build your solution with the **/p:PublishNuGetPackage=true** msbuild switch
 - To change the target repository from the default (http://nuget.org) use the **/p:PublishNuGetPackageTarget="http://myrepo.org"** switch
 - If you've not cached the API key for your custom repository, use the **/p:PublishNuGetPackageTargetKey="MySecretAPIKey"** switch

### How to publish a Symbols package using the NuGet.Package'n'Publish extension

- Build your solution with the **/p:PublishSymbolPackage=true** msbuild switch
 - To change the target repository from the default (http://symbolsource.org) use the **/p:PublishSymbolPackageTarget="http://mysymbolserver.org"** switch
 - If you've not cached the API key for your custom repository, use the **/p:PublishSymbolPackageTargetKey="MySecretAPIKey"** switch

## Still to do

Lots!

- A project template for "meta" packages (i.e. those that only contain dependencies on other NuGet packages)
- Documentation! Documentation! Documentation!

## Get Involved

Fork it... Fix it... Raise Issues... Generate Pull requests... Use it!

For general chat about the NuGet.PackageNPublish tooling, there's also the JabbR channel - http://jabbr.net/#/rooms/NuGetPackageNPublish

## Thanks

Thanks have to go to my company, Landmark Information Group ( http://www.landmark.co.uk / http://twitter.com/LandmarkUK ) for allowing this

