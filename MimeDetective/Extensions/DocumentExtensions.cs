namespace MimeDetective.Extension.Documents
{
    using System.IO;

    /// <summary>
    /// A set of extension methods for use with document formats.
    /// </summary>
    public static class DocumentExtensions
    {   
        /// <summary>
        /// Determines whether the specified file is RTF document.
        /// </summary>
        /// <param name="fileInfo">The FileInfo.</param>
        /// <returns>
        ///   <c>true</c> if the specified file is RTF; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRtf(this FileInfo fileInfo)
        {
            return fileInfo.IsType(MimeTypes.RTF);
        }

        /// <summary>
        /// Determines whether the specified file is PDF.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file is PDF; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPdf(this FileInfo file)
        {
            return file.IsType(MimeTypes.PDF);
        }


        /// <summary>
        /// Determines whether the specified file info is ms-word document file
        /// </summary>
        /// <param name="fileInfo">The file info.</param>
        /// <returns>
        ///   <c>true</c> if the specified file info is doc; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWord(this FileInfo fileInfo)
        {
            return fileInfo.IsType(MimeTypes.WORD);
        }
    }
}