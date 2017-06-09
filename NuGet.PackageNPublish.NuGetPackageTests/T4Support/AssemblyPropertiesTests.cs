using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NuGet.PackageNPublish.NuGetPackage.T4Support.Tests
{
    [TestClass]
    public class AssemblyPropertiesTests
    {
        [TestMethod]
        public void FullAssemblyInfoTest()
        {
            var ap = new AssemblyProperties(BuildFullAssemblyInfo("1.1.0.0", "1.1.1.0", "1.1.1.1"));

            Assert.AreEqual("AssemblyTitle", ap.Title);
            Assert.AreEqual("AssemblyDescription", ap.Description);
            Assert.AreEqual("1.1.0.0", ap.Version);
            Assert.AreEqual("1.1.1.0", ap.FileVersion);
            Assert.AreEqual("1.1.1.1", ap.InformationalVersion);
        }

        [TestMethod]
        public void PartialAssemblyInfoTest()
        {
            var ap = new AssemblyProperties(BuildPartialAssemblyInfo("1.1.0.0", "1.1.1.0", "1.1.1.1"));

            Assert.AreEqual("", ap.Title);
            Assert.AreEqual("", ap.Description);
            Assert.AreEqual("1.1.0.0", ap.Version);
            Assert.AreEqual("1.1.1.0", ap.FileVersion);
            Assert.AreEqual("1.1.1.1", ap.InformationalVersion);
        }

        private string BuildFullAssemblyInfo(string version, string fileVersion, string informationalVersion)
        {
            var sb = new StringBuilder();

            sb.AppendLine("using System.Reflection;");
            sb.AppendLine("using System.Runtime.CompilerServices;");
            sb.AppendLine("using System.Runtime.InteropServices;");
            sb.AppendLine("");
            sb.AppendLine("// General Information about an assembly is controlled through the following ");
            sb.AppendLine("// set of attributes. Change these attribute values to modify the information");
            sb.AppendLine("// associated with an assembly.");
            sb.AppendLine("[assembly: AssemblyTitle(\"AssemblyTitle\")]");
            sb.AppendLine("[assembly: AssemblyDescription(\"AssemblyDescription\")]");
            sb.AppendLine("[assembly: AssemblyConfiguration(\"AssemblyConfiguration\")]");
            sb.AppendLine("[assembly: AssemblyCompany(\"AssemblyCompany\")]");
            sb.AppendLine("[assembly: AssemblyProduct(\"AssemblyProduct\")]");
            sb.AppendLine("[assembly: AssemblyCopyright(\"AssemblyCopyright\")]");
            sb.AppendLine("[assembly: AssemblyTrademark(\"AssemblyTrademark\")]");
            sb.AppendLine("[assembly: AssemblyCulture(\"AssemblyCulture\")]");
            sb.AppendLine("");
            sb.AppendLine("// Setting ComVisible to false makes the types in this assembly not visible ");
            sb.AppendLine("// to COM components.  If you need to access a type in this assembly from ");
            sb.AppendLine("// COM, set the ComVisible attribute to true on that type.");
            sb.AppendLine("[assembly: ComVisible(false)]");
            sb.AppendLine("");
            sb.AppendLine("// The following GUID is for the ID of the typelib if this project is exposed to COM");
            sb.AppendLine("[assembly: Guid(\"9dd61ba0-4fbc-452e-a876-05952b6d8a92\")]");
            sb.AppendLine("");
            sb.AppendLine("// Version information for an assembly consists of the following four values:");
            sb.AppendLine("//");
            sb.AppendLine("//      Major Version");
            sb.AppendLine("//      Minor Version ");
            sb.AppendLine("//      Build Number");
            sb.AppendLine("//      Revision");
            sb.AppendLine("//");
            sb.AppendLine("// You can specify all the values or you can default the Build and Revision Numbers ");
            sb.AppendLine("// by using the '*' as shown below:");
            sb.AppendLine("[assembly: AssemblyVersion(\"1.0.0.0\")]");
            sb.AppendLine("[assembly: AssemblyFileVersion(\"1.0.0.0\")]");
            sb.AppendLine("[assembly: AssemblyInformationalVersion(\"1.0.0.0\")]");

            var rawAssemblyProperties = sb.ToString();

            rawAssemblyProperties = ReplaceVersionNumber(rawAssemblyProperties, "AssemblyVersion", version);
            rawAssemblyProperties = ReplaceVersionNumber(rawAssemblyProperties, "AssemblyFileVersion", fileVersion);
            rawAssemblyProperties = ReplaceVersionNumber(
                rawAssemblyProperties,
                "AssemblyInformationalVersion",
                informationalVersion);

            return rawAssemblyProperties;
        }

        private string BuildPartialAssemblyInfo(string version, string fileVersion, string informationalVersion)
        {
            var sb = new StringBuilder();

            sb.AppendLine("using System.Reflection;");
            sb.AppendLine("");
            sb.AppendLine("[assembly: AssemblyVersion(\"1.0.0.0\")]");
            sb.AppendLine("[assembly: AssemblyFileVersion(\"1.0.0.0\")]");
            sb.AppendLine("[assembly: AssemblyInformationalVersion(\"1.0.0.0\")]");

            var rawAssemblyProperties = sb.ToString();

            rawAssemblyProperties = ReplaceVersionNumber(rawAssemblyProperties, "AssemblyVersion", version);
            rawAssemblyProperties = ReplaceVersionNumber(rawAssemblyProperties, "AssemblyFileVersion", fileVersion);
            rawAssemblyProperties = ReplaceVersionNumber(
                rawAssemblyProperties,
                "AssemblyInformationalVersion",
                informationalVersion);

            return rawAssemblyProperties;
        }

        private string ReplaceVersionNumber(string raw, string id, string version)
        {
            var version1 = string.Format("{0}(\"{1}\")", id, "1.0.0.0");
            var version2 = string.Format("{0}(\"{1}\")", id, version);

            return raw.Replace(version1, version2);
        }
    }
}