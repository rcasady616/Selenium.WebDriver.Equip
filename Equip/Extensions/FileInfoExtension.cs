//using ICSharpCode.SharpZipLib.Zip;
using System.IO.Compression;
using System.Net;

namespace System.IO
{
    public static class FileInfoExtension
    {
        //public static void UnZip(this FileInfo fileInfo, string destiantionFolder)
        //{
        //    using (var fileStreamIn = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
        //    {
        //        using (var zipInStream = new ZipInputStream(fileStreamIn))
        //        {
        //            var entry = zipInStream.GetNextEntry();
        //            FileStream fileStreamOut = null;
        //            while (entry != null)
        //            {
        //                fileStreamOut = new FileStream(destiantionFolder + @"\" + entry.Name, FileMode.Create, FileAccess.Write);
        //                int size;
        //                byte[] buffer = new byte[4096];
        //                do
        //                {
        //                    size = zipInStream.Read(buffer, 0, buffer.Length);
        //                    fileStreamOut.Write(buffer, 0, size);
        //                } while (size > 0);

        //                fileStreamOut.Close();
        //                entry = zipInStream.GetNextEntry();
        //            }

        //            if (fileStreamOut != null)
        //                fileStreamOut.Close();
        //            zipInStream.Close();
        //        }
        //        fileStreamIn.Close();
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        public static void Decompress(this FileInfo fileInfo)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fileInfo.OpenRead())
            {
                // Get original file extension, for example
                // "doc" from report.doc.gz.
                string curFile = fileInfo.FullName;
                string origName = curFile.Remove(curFile.Length -
                        fileInfo.Extension.Length);

                //Create the decompressed file.
                using (FileStream outFile = File.Create(origName))
                {
                    using (GZipStream Decompress = new GZipStream(inFile,
                            CompressionMode.Decompress))
                    {
                        // Copy the decompression stream 
                        // into the output file.
                        Decompress.CopyTo(outFile);

                        Console.WriteLine("Decompressed: {0}", fileInfo.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Download a specific URL to a file
        /// </summary>
        /// <param name="url">The URL to be downloaded</param>
        /// <returns></returns>
        public static FileInfo DownloadUrl(this FileInfo fileInfo, string url)
        {
            new WebClient().DownloadFile(url, fileInfo.FullName);
            return fileInfo;
        }
    }
}
