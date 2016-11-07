using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.PackageNPublish.NuGetPackage.T4Support;

namespace NuGet.PackageNPublish.NuGetPackageTests.T4Support
{
    [TestClass]
    public class NuGetPackageAndPublishTests
    {
        [TestMethod]
        public void NuGetPackageAndPublishTest()
        {
            var hostDirectory = @"..\..\";
            var moduleNames = new List<string> {"module1", "module2", "module3"};

            var pnp = new NuGetPackageAndPublish(hostDirectory, moduleNames);

            Assert.AreEqual("NuGet.PackageNPublish.NuGetPackageTests", pnp.Project.Name);

            Assert.AreEqual("AssemblyTitle", pnp.Assembly.Title);
            Assert.AreEqual("AssemblyDescription", pnp.Assembly.Description);
            Assert.AreEqual("1.0.*", pnp.Assembly.Version);
            //Assert.AreEqual("1.4.16235.3", pnp.Assembly.Version);
            Assert.AreEqual("1.6.16235.2", pnp.Assembly.FileVersion);
            Assert.AreEqual("1.3.16235.1", pnp.Assembly.InformationalVersion);

            Assert.AreEqual("1.6.16235.2", pnp.Package.Version);
            Assert.AreEqual(0, pnp.Package.Dependencies.Count());
            Assert.AreEqual("AssemblyDescription", pnp.Package.Description);
            Assert.AreEqual("AssemblyTitle", pnp.Package.Id);
            Assert.AreEqual("AssemblyTitle", pnp.Package.Title);
        }
    }
}