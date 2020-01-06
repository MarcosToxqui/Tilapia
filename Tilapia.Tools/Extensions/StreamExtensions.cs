namespace Tilapia.Tools.Extensions
{
    using System;
    using System.IO;

    public static class StreamExtensions
    {
        /// <summary>
        /// Dump a stream into file.
        /// </summary>
        /// <param name="streamContent"><see cref="Stream"/> object.</param>
        /// <param name="saveTo">Destination path to save file</param>
        /// <param name="fileName">Name for file.</param>
        public static void ToFile(this Stream streamContent, string saveTo, string fileName)
        {
            string path = string.Empty;
            string directorySeparatorChar = Path.DirectorySeparatorChar.ConvertToString();

            if (saveTo.EndsWith(directorySeparatorChar))
            {
                path = $"{saveTo}{fileName}";
            }
            else
            {
                path = $"{saveTo}{directorySeparatorChar}{fileName}";
            }

            using (var outputStream = new FileStream(path, FileMode.Create))
            {
                streamContent.Seek(0, SeekOrigin.Begin);
                streamContent.CopyTo(outputStream);
            }
        }

        /// <summary>
        /// Dump a stream into file.
        /// </summary>
        /// <param name="streamContent"><see cref="MemoryStream"/> object.</param>
        /// <param name="saveTo">Destination path to save file</param>
        /// <param name="fileName">Name for file.</param>
        public static void ToFile(this MemoryStream streamContent, string saveTo, string fileName)
        {
            string path = string.Empty;
            string directorySeparatorChar = Path.DirectorySeparatorChar.ConvertToString();

            if (saveTo.EndsWith(directorySeparatorChar))
            {
                path = $"{saveTo}{fileName}";
            }
            else
            {
                path = $"{saveTo}{directorySeparatorChar}{fileName}";
            }

            using (var outputStream = new FileStream(path, FileMode.Create))
            {
                streamContent.Seek(0, SeekOrigin.Begin);
                streamContent.CopyTo(outputStream);
            }
        }

        /// <summary>
        /// Convert a stream content to a byte array 
        /// </summary>
        /// <param name="streamContent">Stream object to convert.</param>
        /// <returns>A byte array</returns>
        public static byte[] ToByteArray(this Stream streamContent)
        {
            if (streamContent.IsNull())
            {
                return new byte[0];
            }

            var buffer = new byte[16 * 1024];

            using (var memoryStream = new MemoryStream())
            {
                int read;
                while ((read = streamContent.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, read);
                }

                return memoryStream.ToArray();
            }
        }
    }
}
