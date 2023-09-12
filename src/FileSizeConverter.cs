/*
 * Folder2Iso Copyright (c) 2023 Ruzsinszki Gábor
 */
namespace Folder2Iso
{
    internal static class FileSizeConverter
    {
        private const int KiB = 1024;
        private const int MiB = 1024 * KiB;
        private const int GiB = 1024 * MiB;

        public static string ToFileSize(long bytes)
        {
            double value = bytes;
            string unit = "bytes";
            if (bytes > GiB)
            {
                value /= GiB;
                unit = nameof(GiB);
            }
            else if (bytes > MiB)
            {
                value /= MiB;
                unit = nameof(MiB);
            }
            else if (bytes > KiB)
            {
                value /= KiB;
                unit = nameof(KiB);
            }
            return $"{value:0.00} {unit}";
        }
    }
}
