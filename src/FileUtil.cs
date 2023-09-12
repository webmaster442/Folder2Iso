/*
 * Folder2Iso Copyright (c) 2023 Ruzsinszki Gábor
 */

using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace Folder2Iso
{
    internal static class FileUtil
    {
        public static long WriteIStreamToFile(object i, string fileName)
        {
            IStream inputStream = i as IStream;
            long result = 0;
            using (FileStream outputFileStream = File.OpenWrite(fileName))
            {
                int bytesRead = 0;
                byte[] data;
                do
                {
                    data = Read(inputStream, 2048, out bytesRead);
                    outputFileStream.Write(data, 0, bytesRead);
                    result += bytesRead;
                }
                while (bytesRead == 2048);
                outputFileStream.Flush();
            }
            return result;
        }

        private static unsafe byte[] Read(IStream stream, int toRead, out int read)
        {
            byte[] buffer = new byte[toRead];
            int bytesRead = 0;
            int* ptr = &bytesRead;
            stream.Read(buffer, toRead, (IntPtr)ptr);
            read = bytesRead;
            return buffer;
        }
    }
}
