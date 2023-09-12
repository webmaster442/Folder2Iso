/*
 * Folder2Iso Copyright (c) 2023 Ruzsinszki Gábor
 */

using System;

namespace Folder2Iso
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Banner();
            try
            {
                Options options = OptionsFactory.Create(args);
                var fsi = new IMAPI2FS.MsftFileSystemImage
                {
                    FreeMediaBlocks = int.MaxValue,
                    FileSystemsToCreate = IMAPI2FS.FsiFileSystems.FsiFileSystemISO9660 | IMAPI2FS.FsiFileSystems.FsiFileSystemJoliet,
                    VolumeName = options.VolumeName
                };
                if (options.CreateUdf)
                {
                    fsi.FileSystemsToCreate = IMAPI2FS.FsiFileSystems.FsiFileSystemUDF;
                }
                Info("Processing directory {0}...", options.InputDirectory);
                fsi.Root.AddTree(options.InputDirectory, false);
                Info("Found {0} files", fsi.FileCount);

                var image = fsi.CreateResultImage().ImageStream;

                Info("Writing image to {0}...", options.OutputFile);
                long size = FileUtil.WriteIStreamToFile(image, options.OutputFile);
                Info("Wrote {0} to image", FileSizeConverter.ToFileSize(size));

            }
            catch (ArgumentException ex)
            {
                Error(ex.Message);
                PrintUsage();
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }

        }

        private static void Banner()
        {
            Console.WriteLine(" ______    _     _         ___  _____           ");
            Console.WriteLine("|  ____|  | |   | |       |__ \\|_   _|          ");
            Console.WriteLine("| |__ ___ | | __| | ___ _ __ ) | | |  ___  ___  ");
            Console.WriteLine("|  __/ _ \\| |/ _` |/ _ \\ '__/ /  | | / __|/ _ \\ ");
            Console.WriteLine("| | | (_) | | (_| |  __/ | / /_ _| |_\\__ \\ (_) |");
            Console.WriteLine("|_|  \\___/|_|\\__,_|\\___|_||____|_____|___/\\___/ ");
            Console.WriteLine("Version: 1.0.0");
            Console.WriteLine("https://github.com/webmaster442/Folder2Iso");
            Console.WriteLine();
        }

        private static void PrintUsage()
        {
            var previous = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Usage:");
            Console.WriteLine("Folder2Iso [-u] -i {input folder} -o {output.iso}");
            Console.WriteLine("Folder2Iso [--udf] --input {input folder} --output {output.iso}");
            Console.WriteLine("-u or --udf is optional. When specified will create UDF file system image");
            Console.ForegroundColor = previous;
        }

        private static void Info(string format, params object[] arguments)
        {
            var previous = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(format, arguments);
            Console.ForegroundColor = previous;
        }

        private static void Error(string message)
        {
            var previous = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = previous;
        }
    }
}