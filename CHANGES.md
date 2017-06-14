# Change History

## Changes for v0.10

- Visual Studio 2017 now supported by the nuget package (Issue #27)
  - How the tooling finds `NuGet.exe` and `TextTemplate.exe` was changed (again) to support the new (random) location for the latter.
- Tooling no longer tries to locate `tf.exe` and checkout using *TFS* by default (Issue #22)

## Changes for v0.9

- Automatic CI builds now working in AppVeyor
    - Including capturing the artifacts for deployment!
- NuGet.PackageNPublish the package now no longer requires NuGet package restore (Issue #4)
    - Also finds NuGet.exe and TextTemplate.exe automagically for all versions to VS2015 (Issue #14)
- NuGet.PackageNPublish the package now adds a *works-out-of-the-box* templated NuSpec file - just rename it!
- Template NuSpec now uses embedded C# classes to simplify them (Thanks to Steve Grattan) (Issue #25)
    - Also doesn't include development dependencies (Issue #18)
- NuGet.PackageNPublish the package is now a development dependency (Issue #18 ish)
- Error message for missing TextTransform.exe is fixed (Issue #21)

## Changes for v0.8

- Package Version is now optionally auto-generated (Issue #16)
- Tooling includes XmlDocs in the output package (Issue #15)
- Fix for bad error message when dependencies cannot be found (Issue #13)
- Fix Dependencies not being found correctly for VS2013 (Issue #14)

## Changes for v0.7

- No longer automatically adds all references by default (See issues 3, 7 & 11 - https://github.com/JoelHT-Landmark/NuGet-PackageNPublish/issues )
- Includes source for "parent" library package by default.
- Updated to use NuGet v2.5 or above
