using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace white_cloud.data.migrations
{
    internal class MigrationVersionAttribute : MigrationAttribute
    {
        public int Major { get; }
        public int Minor { get; }
        public int Patch { get; }
        public int Migration { get; }

        public MigrationVersionAttribute(int major, int minor, int patch, int migration, string description)
            : base(CalculateValue(major, minor, patch, migration), FormatDescription(major, minor, patch, migration, description))
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Migration = migration;
        }
        private static long CalculateValue(int major, int minor, int patch, int migration)
        {
            if (IsUsingObsoleteVersionNumbering(major, minor))
            {
                return CalculateValueObsolete(major, minor, patch, migration);
            }

            return major * 10000000L + minor * 100000L + patch * 1000L + migration * 10;
        }

        private static bool IsUsingObsoleteVersionNumbering(int major, int minor)
        {
            return major == 6 && minor <= 10;
        }

        private static long CalculateValueObsolete(int major, int minor, int patch, int migration)
        {
            migration = migration > 10 ? migration : migration * 10;
            patch = patch > 10 ? patch : patch * 10;
            minor = minor > 10 ? minor : minor * 10;
            major = major > 10 ? major : major * 10;
            return major * 1000000L + minor * 10000L + patch * 100L + migration;
        }

        private static string FormatDescription(int major, int minor, int patch, int migration, string description)
        {
            return $"[{major}.{minor}.{patch}.{migration}] {description}";
        }
    }
}