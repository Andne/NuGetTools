using System;

using NuGet;

namespace NuGet.MergedRepo
{
    public class MergedPackagePathResolver : DefaultPackagePathResolver
    {
        public MergedPackagePathResolver(string path) :
            base(path)
        {
        }

        public MergedPackagePathResolver(IFileSystem fileSystem) :
            base(fileSystem)
        {
        }

        public override string GetPackageDirectory(string packageId, SemanticVersion version)
        {
            // Return an empty directory so the package is unpacked directly into the repository directory
            return "";
        }
    }
}
