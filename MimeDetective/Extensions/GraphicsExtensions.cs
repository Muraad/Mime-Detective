//
// The MIT License (MIT)
// 
// Copyright (C) 2014  Muraad Nofal and the contributors
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 

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