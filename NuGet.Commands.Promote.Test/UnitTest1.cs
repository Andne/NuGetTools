using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NuGet.Commands.Promote.Test
{
    [TestClass]
    public class UnitTest1
    {
        private string _workingDirectory;

        [TestInitialize]
        public void LoadWorkingDirectory()
        {
            string full_path = Assembly.GetExecutingAssembly().Location;
            this._workingDirectory = Path.GetDirectoryName(full_path);
        }

        private Process GetNuGetProcess(string Arguments)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo();

            p.StartInfo.FileName = "nuget.exe";
            p.StartInfo.Arguments = Arguments;
            p.StartInfo.WorkingDirectory = this._workingDirectory;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            return p;
        }

        [TestMethod]
        public void TestCommandRegistered()
        {
            var p = GetNuGetProcess("help promote");

            p.Start();

            p.WaitForExit();
            Assert.AreEqual(0, p.ExitCode);
        }

        [TestMethod]
        public void TestHelpCommand()
        {
            var p = GetNuGetProcess("help promote");

            p.Start();

            var output = p.StandardOutput.ReadToEnd();
            Assert.AreEqual("usage: NuGet promote <packageId> [options]", output.Substring(0, output.IndexOf("\r")));
        }

        [TestMethod]
        public void TestTestCommand()
        {
            var expected_file = Path.Combine(this._workingDirectory, "DestRepo", "ExamplePackage.1.0.0-CI01.nupkg");
            if (File.Exists(expected_file))
            {
                File.Delete(expected_file);
            }

            var p = GetNuGetProcess(
                String.Format(@"promote ExamplePackage -Source ""{0}\SourceRepo"" -Destination ""{0}\DestRepo""",
                this._workingDirectory)
                );

            p.Start();
            var output = p.StandardOutput.ReadToEnd();

            Assert.AreEqual("Promoting package ExamplePackage\r\n", output);
            Assert.IsTrue(File.Exists(expected_file));
        }

        [TestMethod]
        public void TestStripVersion()
        {
            var expected_file = Path.Combine(this._workingDirectory, "DestRepo", "ExamplePackage.1.0.0.nupkg");
            if (File.Exists(expected_file))
            {
                File.Delete(expected_file);
            }

            var p = GetNuGetProcess(
                String.Format(@"promote ExamplePackage -Source ""{0}\SourceRepo"" -Destination ""{0}\DestRepo"" -StripPrereleaseVersion",
                this._workingDirectory)
                );

            p.Start();
            var output = p.StandardOutput.ReadToEnd();

            Assert.AreEqual("Promoting package ExamplePackage\r\n", output);
            Assert.IsTrue(File.Exists(expected_file));
        }
    }
}
