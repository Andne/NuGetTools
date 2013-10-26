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
         Assert.AreEqual("usage: NuGet promote <packageId>", output.Substring(0, output.IndexOf("\r")));
      }

      [TestMethod]
      public void TestTestCommand()
      {
         var p = GetNuGetProcess("promote Package1");

         p.Start();
         var output = p.StandardOutput.ReadToEnd();

         Assert.AreEqual("Promoting package Package1\r\n", output);
      }
   }
}
