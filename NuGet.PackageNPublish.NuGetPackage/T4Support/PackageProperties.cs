namespace NuGet.PackageNPublish.NuGetPackage.T4Support
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Encapsulates information about the package to be built for the current project
    /// </summary>
    public class PackageProperties
    {
        private const string DefaultPackageVersion = "1.0.0.0";

        private readonly string hostDirectory;

        private readonly IEnumerable<string> moduleNames;

        public PackageProperties(
            string hostDirectory,
            IEnumerable<string> moduleNames,
            string id,
            string title,
            string description,
            IEnumerable<string> versions)
        {
            this.hostDirectory = hostDirectory;
            this.moduleNames = moduleNames;

            Id = id;
            Title = title;
            Description = description;
            Version = BuildPackageVersion(versions);
            Dependencies = BuildDependencies();
        }

        public string Id { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string Version { get; private set; }

        public IEnumerable<XElement> Dependencies { get; private set; }

        /// <summary>
        /// Uses an algorithm for determining the 'best fit' version to use.
        /// It assumes the version numbers are in *.*.*.* format as per Assembly Info versions.
        /// If a number cannot be determined set the version to the default.
        /// </summary>
        /// <param name="versions">the versions to consider when determining the 'best fit'</param>
        /// <returns></returns>
        private string BuildPackageVersion(IEnumerable<string> versions)
        {
            // Convert the four digits of each version field into a number 
            // and map the resulting number to the string representing the version
            var versionNumbers = new Dictionary<long, string>();

            foreach (var version in versions)
            {
                versionNumbers[ConvertToLong(version)] = version;
            }

            // Use the version info that has the 'highest' number
            var packageVersion = versionNumbers[versionNumbers.Keys.Max()];

            // Use the default if the version could not be determined
            if (string.IsNullOrEmpty(packageVersion))
            {
                packageVersion = DefaultPackageVersion;
            }

            return packageVersion;
        }

        /// <summary>
        /// Determines a numeric value for a version info item in the assembly
        /// </summary>
        /// <param name="version">The assembly version item under consideration</param>
        /// <returns></returns>
        private long ConvertToLong(string version)
        {
            // Split the version info into individual parts e.g. 1.5.0.1
            var parts = version.Split('.');

            // Establish the ordinal multiplier by starting at a thousand
            long ordinal = 1000;
            long result = 0;

            // If the version number ends with a dash the increase the ordinal mulitplier to millions.
            // This will force any 'prerelease' version number to take priority.
            if (parts.Length >= 4 && version.Contains('-') && !version.EndsWith("-"))
            {
                ordinal += 1000;
            }

            // Multiply each number by the ordinal multiplier of the number - thousands, hundreds, tens, units
            foreach (var number in version.Trim('-').Split('.'))
            {
                long l;
                long.TryParse(number, out l);

                result += l * ordinal;
                ordinal /= 10;
            }

            return result;
        }

        private IEnumerable<XElement> BuildDependencies()
        {
            var packagesDoc = XDocument.Load(hostDirectory + "\\packages.config");

            IEnumerable<XElement> packages = new List<XElement>();

            if (!moduleNames.Any())
            {
                packages = GetModulePackages(packagesDoc);
            }

            foreach (var moduleName in moduleNames)
            {
                var modulePackagesPath = Path.Combine(Directory.GetParent(hostDirectory).ToString(), moduleName);

                var packagesConfigFileName = modulePackagesPath + "\\packages.config";

                if (File.Exists(packagesConfigFileName))
                {
                    packagesDoc = XDocument.Load(packagesConfigFileName);

                    var modulePackages = GetModulePackages(packagesDoc);

                    packages =
                        packages.Union(
                            modulePackages.Where(
                                p => !packages.Any(c => c.Attribute("id").Value == p.Attribute("id").Value)))
                            .OrderBy(p => p.Attribute("id").Value)
                            .ToList();
                }
            }

            return packages;
        }

        private static IEnumerable<XElement> GetModulePackages(XDocument packagesDoc)
        {
            return
                packagesDoc.Descendants("package")
                    .Where(
                        p =>
                        !p.Attribute("id").Value.StartsWith("NuGet.") && p.Attribute("developmentDependency") == null)
                    .ToList();
        }
    }
}