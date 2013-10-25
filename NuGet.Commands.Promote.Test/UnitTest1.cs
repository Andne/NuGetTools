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

        [TestMethod]
        public void TestCommandRegistered()
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo();

            p.StartInfo.FileName = "nuget.exe";
            p.StartInfo.Arguments = "help promote";   // Easy way to test, see if the help can be found
            p.StartInfo.WorkingDirectory = this._workingDirectory;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.EnvironmentVariables.Add("NUGET_EXTENSIONS_PATH", this._workingDirectory);

            p.Start();

            p.WaitForExit();
            var output = p.StandardOutput.ReadToEnd();
            Console.Write(p.StandardOutput.ReadToEnd());
            Assert.AreEqual(0, p.ExitCode);
        }
    }
}
