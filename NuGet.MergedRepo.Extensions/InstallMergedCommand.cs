using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NuGet;
using NuGet.Commands;

namespace NuGet.MergedRepo
{
    /*[Command(typeof(), "installmerged", "Description", MaxArgs = 1,
        UsageSummaryResourceName = "UsageSummaryDescription")] */
    [Command("installmerged", "Install without the package names in the path")]
    public class InstallMergedCommand : InstallCommand
    {
        protected override IPackageManager CreatePackageManager(IFileSystem packagesFolderFileSystem, bool useSideBySidePaths, bool checkDowngrade = true)
        {
            var repository = CreateRepository();
            var pathResolver = new MergedPackagePathResolver(packagesFolderFileSystem);
            IPackageRepository localRepository = new LocalPackageRepository(pathResolver, packagesFolderFileSystem);
            if (EffectivePackageSaveMode != PackageSaveModes.None)
            {
                localRepository.PackageSaveMode = EffectivePackageSaveMode;
            }

            var packageManager = new PackageManager(repository, pathResolver, packagesFolderFileSystem, localRepository)
            {
                Logger = Console,
                CheckDowngrade = checkDowngrade
            };

            return packageManager;
        }
    }
}
