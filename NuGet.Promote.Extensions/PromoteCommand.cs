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

            Console.WriteLine("Promoting package {0}", packageId);
        }
    }
}
