using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

using NuGet;
using NuGet.Commands;

// Use NUGET_EXTENSIONS_PATH environment variable for testing

namespace NuGet.Commands
{
    [Command(typeof(Promote.PromoteResources), "promote", "Description", MaxArgs = 1)]
    public class PromoteCommand : Command
    {
        private readonly IPackageRepositoryFactory _repositoryFactory;
        private readonly IPackageSourceProvider _sourceProvider;

        [ImportingConstructor]
        public PromoteCommand(IPackageRepositoryFactory RepositoryFactory,
                              IPackageSourceProvider SourceProvider)
        {
            this._repositoryFactory = RepositoryFactory;
            this._sourceProvider = SourceProvider;
        }

        /*
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
        */

        public override void ExecuteCommand()
        {
            var packageId = base.Arguments[0];

            Console.WriteLine("Promoting package {0}", packageId);
        }
    }
}
