namespace NuGet.PackageNPublish.NuGetPackage.T4Support
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Encapsulates information from various sources about the project being built.
    /// </summary>
    public class NuGetPackageAndPublish
    {
        private readonly string hostDirectory;

        private readonly IEnumerable<string> moduleNames;

        private const string DefaultAssemblyVersion = "1.0.0.0";

        /// <summary>
        /// Builds a project, assembly and package instance
        /// </summary>
        /// <param name="hostDirectory">a directory to act as the root for file searches</param>
        /// <param name="moduleNames">a list of modules that will be included in a package</param>
        public NuGetPackageAndPublish(string hostDirectory, IEnumerable<string> moduleNames)
        {
            this.hostDirectory = hostDirectory;
            this.moduleNames = moduleNames;

            BuildProjectProperties();
            BuildAssemblyProperties();
            BuildPackageProperties();
        }

        public NuGetPackageAndPublish(string hostDirectory)
            : this(hostDirectory, new string[0])
        {
        }

        public ProjectProperties Project { get; private set; }

        public AssemblyProperties Assembly { get; private set; }

        public PackageProperties Package { get; private set; }

        /// <summary>
        /// Builds a ProjectProperties instance from a C# project file
        /// </summary>
        private void BuildProjectProperties()
        {
            Project = new ProjectProperties(hostDirectory);
        }

        /// <summary>
        /// Builds an AssemblyProperties instance from all C# files found 
        /// in the 'properties' directory of the current project
        /// </summary>
        private void BuildAssemblyProperties()
        {
            // Read and concatenate all info from C# files in the 'properties' directory
            var rawAssemblyInfo = new StringBuilder();

            foreach (var assemblyInfoFileName in
                Directory.EnumerateFiles(hostDirectory + "\\Properties", "*.cs", SearchOption.TopDirectoryOnly))
            {
                rawAssemblyInfo.AppendLine(File.ReadAllText(assemblyInfoFileName));
            }

            // default assembly properties that are not present
            EnsurePropertyIsPresent(rawAssemblyInfo, "AssemblyTitle", Project.Name);
            EnsurePropertyIsPresent(rawAssemblyInfo, "AssemblyDescription", Project.Name);
            EnsurePropertyIsPresent(rawAssemblyInfo, "AssemblyVersion", DefaultAssemblyVersion);
            EnsurePropertyIsPresent(rawAssemblyInfo, "AssemblyFileVersion", DefaultAssemblyVersion);
            EnsurePropertyIsPresent(rawAssemblyInfo, "AssemblyInformationalVersion", DefaultAssemblyVersion);

            Assembly = new AssemblyProperties(rawAssemblyInfo.ToString());
        }

        private void EnsurePropertyIsPresent(StringBuilder rawAssemblyInfo, string name, string value)
        {
            if (!rawAssemblyInfo.ToString().Contains(name))
            {
                var property = string.Format("[assembly: {0}(\"{1}\")]", name, value);
                rawAssemblyInfo.AppendLine(property);
            }
        }

        /// <summary>
        /// Builds a PackageProperties instance based on information in the AssemblyProperties instance.
        /// A package version is determined from a number of versions in the AssemblyProperties.
        /// </summary>
        private void BuildPackageProperties()
        {
            // trim off any default naming for package items
            var packageId = Assembly.Title.Replace(".NuGetPackage", string.Empty);
            var packageTitle = Assembly.Title.Replace(".NuGetPackage", string.Empty);
            var packageDescription = Assembly.Description;

            // provide any available versions for package versioning
            var versionsToConsider = new List<string>
                                         {
                                             Assembly.Version,
                                             Assembly.FileVersion,
                                             Assembly.InformationalVersion
                                         };

            Package = new PackageProperties(
                hostDirectory,
                moduleNames,
                packageId,
                packageTitle,
                packageDescription,
                versionsToConsider);
        }
    }
}
