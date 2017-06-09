**Getting Started**

There's a handy screencast demonstrating how to
get started on YouTube:

[http://www.youtube.com/watch?v=R6e4kV5dfIQ](http://www.youtube.com/watch?v=R6e4kV5dfIQ)

**Requirements**

·       
Visual Studio 2013.

·       
Visual Studio 2015.

·       
NuGet Package Manager 2.5 or above.

**How to create a NuGetPackage using the
NuGet.Package'n'Publish extension**

Install the extension into Visual Studio

- EITHER from a pre-built .visx file (from the
Visual Studio Gallery)

 -OR by
building the NuGet.PackageNPublish.sln solution (and then installing the
generated .visx file)

Add a new NuGet.PackageNPublish project to your
solution, perferably at the Solution folder level - e.g.

c:\temp\MySolution

 -- c:\temp\MySolution\MySolution.sln

 -- c:\temp\MySolution\MyLibrary

 ----
c:\temp\MySolution\MyLibrary\MyLibrary.csproj

 --
c:\temp\MySolution\MyLibrary.NuGetPackage

 ----
c:\temp\MySolution\MyLibrary.NuGetPackage.csproj 

Give the project sensible name ending in .NuGetPackage - e.g. "MyLibrary.NuGetPackage"

If named like this, then the assembly from the
packaging project is removed from the package automatically,

Add references to the assemblies you want to
include in your package

By default, if you name your package "MyLibrary.NuGetPackage" (or similar), and
reference "MyLibrary" then you're already good to go, AND the
packaging project will include your "MyLibrary.dll" automagically,
AND it will find and package the source in a symbols package - again
automagically.

Any assembly ending in .Silverlight.dll will be
put in the lib/SL40 folder within the package.

Any other assembly will be put in the lib/Net40 folder within the package.

Don't forget to add a line to the .tt file for
the library for any additional references other than the one to
"MyLibrary"

Add dependencies on other NuGet packages

The dependencies will be included automatically
in your NuGet package

Set the AssemblyTitle, AssemblyFileVersion and
AssemblyDescription inProperties\AssemblyInfo.cs

Build the solution

You now have NuGet (.npkg) and a Symbols
(.symbols.nupkg) packages built in your project directory!

**How to publish a package using the
NuGet.Package'n'Publish extension**

Build your solution with the /p:PublishNuGetPackage=true
msbuild switch

To change the target repository from the default
([http://nuget.org](http://nuget.org/)) use the /p:PublishNuGetPackageTarget="http://myrepo.org"
switch

If you've not cached the API key for your custom
repository, use the /p:PublishNuGetPackageTargetKey="MySecretAPIKey"
switch

**How to publish a Symbols package using the
NuGet.Package'n'Publish extension**

Build your solution with the /p:PublishSymbolPackage=true msbuild switch

To change the target repository from the default
([http://symbolsource.org](http://symbolsource.org/)) use the /p:PublishSymbolPackageTarget="http://mysymbolserver.org"
switch

If you've not cached the API key for your custom
repository, use the /p:PublishSymbolPackageTargetKey="MySecretAPIKey"
switch