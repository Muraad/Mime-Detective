using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimeDetective
{
    public static class BaseExtensions
    {
        /// <summary>
        /// Read header of bytes and depending on the information in the header
        /// return object FileType.
        /// Return null in case when the file type is not identified. 
        /// Throws Application exception if the file can not be read or does not exist
        /// </summary>
        /// <remarks>
        /// A temp file is written to get a FileInfo from the given bytes.
        /// If this is not intended use 
        /// 
        ///     GetFileType(() => bytes); 
        ///     
        /// </remarks>
        /// <param name="file">The FileInfo object.</param>
        /// <returns>FileType or null not identified</returns>
        public static FileType GetFileType(this byte[] bytes)
        {
            return MimeTypes.GetFileType(() => bytes);
        }

        /// <summary>
        /// Read header of a stream and depending on the information in the header
        /// return object FileType.
        /// Return null in case when the file type is not identified. 
        /// Throws Application exception if the file can not be read or does not exist
        /// </summary>
        /// <param name="file">The FileInfo object.</param>
        /// <param name="stream">The stream from which to read for determining the MIME type.</param>
        /// <returns>FileType or null not identified</returns>
        public static FileType GetFileType(this Stream stream)
        {
            if (stream.CanSeek)
            {
                long currentLocation = stream.Position;

                stream.Seek(0, SeekOrigin.Begin);

                byte[] buffer = new byte[MimeTypes.MaxHeaderSize];

                stream.Read(buffer, 0, MimeTypes.MaxHeaderSize);

                stream.Seek(currentLocation, SeekOrigin.Begin);

                return MimeTypes.GetFileType(() => buffer);
            }

            return new FileType();
        }

        /// <summary>
        /// Read header of a file and depending on the information in the header
        /// return object FileType.
        /// Return null in case when the file type is not identified. 
        /// Throws Application exception if the file can not be read or does not exist
        /// </summary>
        /// <param name="file">The FileInfo object.</param>
        /// <returns>FileType or null not identified</returns>
        public static FileType GetFileType(this FileInfo file)
        {
            return MimeTypes.GetFileType(() => MimeTypes.ReadFileHeader(file), file.FullName);
        }

        /// <summary>
        /// Determines whether the specified file is of provided type
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="type">The FileType</param>
        /// <returns>
        ///   <c>true</c> if the specified file is type; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsType(this FileInfo file, FileType type)
        {
            FileType actualType = file.GetFileType();

            if (null == actualType)
                return false;

            return (actualType.Equals(type));
        }
    }
}
