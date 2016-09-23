using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.PackageNPublish.NuGetPackage.T4Support;

namespace NuGet.PackageNPublish.NuGetPackageTests.T4Support
{
    [TestClass]
    public class ProjectPropertiesTests
    {
        [TestMethod]
        public void ProjectPropertiesTest()
        {
            var hostDirectory = @"..\..\";

            var pp = new ProjectProperties(hostDirectory);

            Assert.AreEqual("NuGet.PackageNPublish.NuGetPackageTests", pp.Name);
        }
    }
}