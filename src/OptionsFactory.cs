/*
 * Folder2Iso Copyright (c) 2023 Ruzsinszki Gábor
 */

using System;
using System.IO;
using System.Text;

namespace Folder2Iso
{
    internal static class OptionsFactory
    {
        public static Options Create(string[] arguments)
        {
            Options result = new Options();

            for (int i = 0; i < arguments.Length; i++)
            {
                string next = i + 1 < arguments.Length ? arguments[i + 1] : string.Empty;

                switch (arguments[i])
                {
                    case "-u":
                    case "--udf":
                        result.CreateUdf = true;
                        break;
                    case "-v":
                    case "--volume":
                        result.VolumeName = next;
                        break;
                    case "-i":
                    case "--input":
                        result.InputDirectory = next;
                        break;
                    case "-o":
                    case "--output":
                        result.OutputFile = next;
                        break;
                }
            }

            Validate(result);
            SetDefaultVolumeNameIfNotSet(result);

            return result;
        }

        private static void Validate(Options options)
        {
            var issues = new StringBuilder();

            if (string.IsNullOrEmpty(options.InputDirectory))
            {
                issues.AppendLine("Input directory name can't be null or empty");
            }
            else if (!Directory.Exists(options.InputDirectory))
            {
                issues.AppendFormat("Input directory doesn't exist: {0}", options.InputDirectory);
            }

            if (string.IsNullOrEmpty(options.OutputFile))
                issues.AppendLine("Output file name can't be null or empty");

            if (issues.Length > 0)
                throw new ArgumentException(issues.ToString());
        }

        private static void SetDefaultVolumeNameIfNotSet(Options options)
        {
            if (string.IsNullOrEmpty(options.VolumeName))
                options.VolumeName = Path.GetDirectoryName(options.InputDirectory);
        }
    }
}
