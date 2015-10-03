using System;

namespace MimeDetective
{
    /// <summary>
    /// Little data structure to hold information about file types. 
    /// Holds information about binary header at the start of the file
    /// </summary>
    public class FileType
    {
        /*internal byte?[] Header { get; set; }    // most of the times we only need first 8 bytes, but sometimes extend for 16
        internal int HeaderOffset { get; set; }
        internal string Extension { get; set; }
        internal string Mime { get; set; }*/

        public byte?[] Header { get; set; }
        public int HeaderOffset { get; set; }
        public string Extension { get; set; }
        public string Mime { get; set; }

        public FileType()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileType"/> class.
        /// Default construction with the header offset being set to zero by default
        /// </summary>
        /// <param name="header">Byte array with header.</param>
        /// <param name="extension">String with extension.</param>
        /// <param name="mime">The description of MIME.</param>
        public FileType(byte?[] header, string extension, string mime)
        {
            Header = header;
            Extension = extension;
            Mime = mime;
            HeaderOffset = 0;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FileType"/> struct.
        /// Takes the details of offset for the header
        /// </summary>
        /// <param name="header">Byte array with header.</param>
        /// <param name="offset">The header offset - how far into the file we need to read the header</param>
        /// <param name="extension">String with extension.</param>
        /// <param name="mime">The description of MIME.</param>
        public FileType(byte?[] header, int offset, string extension, string mime)
        {
            this.Header = null;
            this.Header = header;
            this.HeaderOffset = offset;
            this.Extension = extension;
            this.Mime = mime;
        }


        public override bool Equals(object other)
        {
            if (!(other is FileType)) return false;

            FileType otherType = (FileType)other;

            if (this.Extension == otherType.Extension && this.Mime == otherType.Mime) return true;

            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Extension;
        }
    }
}
