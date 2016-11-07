namespace NuGet.PackageNPublish.NuGetPackage.T4Support
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Encapsulates information about the assembly for the current project.
    /// </summary>
    public class AssemblyProperties
    {
        public AssemblyProperties(string rawAssemblyInfo)
        {
            Title = ReadInfo("AssemblyTitle", rawAssemblyInfo);
            Description = ReadInfo("AssemblyDescription", rawAssemblyInfo);
            Version = ReadInfo("AssemblyVersion", rawAssemblyInfo);
            FileVersion = ReadInfo("AssemblyFileVersion", rawAssemblyInfo);
            InformationalVersion = ReadInfo("AssemblyInformationalVersion", rawAssemblyInfo);
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string Version { get; private set; }

        public string FileVersion { get; private set; }

        public string InformationalVersion { get; private set; }

        private string ReadInfo(string tag, string rawAssemblyInfo)
        {
            var matcher = new Regex("(" + tag + "\\(\")(.*)(\"\\))");
            var value = matcher.Match(rawAssemblyInfo).Groups[2].ToString();

            return value;
        }
    }
}