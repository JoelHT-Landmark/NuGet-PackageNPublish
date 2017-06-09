using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.PackageNPublish.NuGetPackage.T4Support;

namespace NuGet.PackageNPublish.NuGetPackageTests.T4Support
{
    [TestClass]
    public class PackagePropertiesTests
    {
        [TestMethod]
        public void PackagePropertiesHighestVersionTest()
        {
            var hostDirectory = @"..\..\";
            var moduleNames = new List<string> {"module1", "module2", "module3"};
            var id = "id";
            var title = "title";
            var description = "description";
            var versions = new List<string> {"1.4.0.0", "1.5.0.0", "1.6.0.0"};

            var pp = new PackageProperties(hostDirectory, moduleNames, id, title, description, versions);

            Assert.AreEqual("title", pp.Title);
            Assert.AreEqual(0, pp.Dependencies.Count());
            Assert.AreEqual("description", pp.Description);
            Assert.AreEqual("id", pp.Id);
            Assert.AreEqual("1.6.0.0", pp.Version);
        }

        [TestMethod]
        public void PackagePropertiesPreReleaseVersionTest()
        {
            var hostDirectory = @"..\..\";
            var moduleNames = new List<string> {"module1", "module2", "module3"};
            var id = "id";
            var title = "title";
            var description = "description";
            var versions = new List<string> {"1.4.0.0", "1.5.0.0", "1.3.0.0-prerelease"};

            var pp = new PackageProperties(hostDirectory, moduleNames, id, title, description, versions);

            Assert.AreEqual("title", pp.Title);
            Assert.AreEqual(0, pp.Dependencies.Count());
            Assert.AreEqual("description", pp.Description);
            Assert.AreEqual("id", pp.Id);
            Assert.AreEqual("1.3.0.0-prerelease", pp.Version);
        }
    }
}