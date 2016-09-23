namespace NuGet.PackageNPublish.NuGetPackage.T4Support
{
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Encapsulates information from various sources about the project being built.
    /// </summary>
    public class ProjectProperties
    {
        /// <summary>
        /// Builds information about the project
        /// </summary>
        /// <param name="hostDirectory">a directory to act as the root for file searches</param>
        public ProjectProperties(string hostDirectory)
        {
            // find the first (and only?) project file in the host directory to get a meaningful name if required
            var projectFileName = Directory.EnumerateFiles(hostDirectory, "*.csproj").FirstOrDefault();

            if (projectFileName != null)
            {
                Name = Path.GetFileNameWithoutExtension(projectFileName);
            }
            else
            {
                Name = "unknown";
            }
        }

        public string Name { get; private set; }
    }
}