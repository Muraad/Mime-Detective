namespace MimeDetective.Extensions.Graphics
{
    using System.IO;

    /// <summary>
    /// A set of extension methods for use with graphics formats.
    /// </summary>
    public static class GraphicsExtensions
    {
        /// <summary>
        /// Determines whether the specified file is PNG.
        /// </summary>
        /// <param name="fileInfo">The FileInfo object</param>
        /// <returns>
        ///   <c>true</c> if the specified file info is PNG; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPng(this FileInfo fileInfo)
        {
            return fileInfo.IsType(MimeTypes.PNG);
        }

        /// <summary>
        /// Determines whether the specified file is GIF image
        /// </summary>
        /// <param name="fileInfo">The FileInfo object</param>
        /// <returns>
        ///   <c>true</c> if the specified file info is GIF; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGif(this FileInfo fileInfo)
        {
            return fileInfo.IsType(MimeTypes.GIF);
        }

        /// <summary>
        /// Determines whether the specified file is JPEG image
        /// </summary>
        /// <param name="fileInfo">The FileInfo.</param>
        /// <returns>
        ///   <c>true</c> if the specified file info is JPEG; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsJpeg(this FileInfo fileInfo)
        {
            return fileInfo.IsType(MimeTypes.JPEG);
        }
    }
}