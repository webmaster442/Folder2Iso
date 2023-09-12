/*
 * Folder2Iso Copyright (c) 2023 Ruzsinszki Gábor
 */

namespace Folder2Iso
{
    internal sealed class Options
    {
        public bool CreateUdf { get; set; }
        public string VolumeName { get; set; }
        public string InputDirectory { get; set; }
        public string OutputFile { get; set; }
    }
}
