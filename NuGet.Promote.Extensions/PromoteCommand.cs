using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

using NuGet;
using NuGet.Commands;

namespace NuGet.Promote
{
    [Command(typeof(PromoteResources), "promote", "Description", MaxArgs = 1,
        UsageSummaryResourceName="UsageSummaryDescription")]
    public class PromoteCommand : Command
    {
        [Option(typeof(Promote.PromoteResources), "SourceDescription", AltName = "src")]
        public string Source { get; set; }

        [Option(typeof(Promote.PromoteResources), "DestinationDescription", AltName = "dest")]
        public string Destination { get; set; }

        [Option(typeof(Promote.PromoteResources), "VersionDescription")]
        public string Version { get; set; }

        [Option(typeof(Promote.PromoteResources), "StripPrereleaseVersionDescription")]
        public bool StripPrereleaseVersion { get; set; }

        [Option(typeof(Promote.PromoteResources), "ApiKeyDescription")]
        public string ApiKey { get; set; }

        public override void ExecuteCommand()
        {
            var packageId = base.Arguments[0];

            var repo = RepositoryFactory.CreateRepository(this.Source);
            IPackage package;

            /*if (this.StripPrereleaseVersion)
            {
                
            }
            else*/
            {
                package = repo.FindPackage(packageId);
            }

            var package_server = new PackageServer(this.Destination, "NuGet Promote Command");
            //package_server.PushPackage("", package, Convert.ToInt32(TimeSpan.FromMinutes(5.0).TotalMilliseconds));
            var stream = new System.IO.MemoryStream();
            var package_bytes = package.GetStream().ReadAllBytes();
            stream.Write(package_bytes, 0, package_bytes.Length);
            stream.Seek(0, System.IO.SeekOrigin.Begin);

            package_server.PushPackage("", stream, package_bytes.Length, Convert.ToInt32(TimeSpan.FromMinutes(5.0).TotalMilliseconds));

            Console.WriteLine("Promoting package {0}", packageId);
        }
    }
}
