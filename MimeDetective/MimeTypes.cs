using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;

namespace MimeDetective
{
    /// <summary>
    /// Helper class to identify file type by the file header, not file extension.
    /// </summary>
    public static class MimeTypes
    {

        // all the file types to be put into one list
        public static List<FileType> types = new List<FileType> {PDF, WORD, EXCEL, JPEG, ZIP, RAR, RTF, PNG, PPT, GIF, DLL_EXE, MSDOC,
                BMP, DLL_EXE, ZIP_7z, ZIP_7z_2, GZ_TGZ, TAR_ZH, TAR_ZV, OGG, ICO, XML, MIDI, FLV, WAVE, DWG, LIB_COFF, PST, PSD,
                AES, SKR, SKR_2, PKR, EML_FROM, ELF};

        #region Constants

        // file headers are taken from here:
        //http://www.garykessler.net/library/file_sigs.html
        //mime types are taken from here:
        //http://www.webmaster-toolkit.com/mime-types.shtml

        #region office, excel, ppt and cocuments, xml, pdf, rtf, msdoc
        // office and documents
        public readonly static FileType WORD = new FileType(new byte?[] { 0xEC, 0xA5, 0xC1, 0x00 }, 512, "doc", "application/msword");
        public readonly static FileType EXCEL = new FileType(new byte?[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 }, 512, "xls", "application/excel");
        public readonly static FileType PPT = new FileType(new byte?[] { 0xFD, 0xFF, 0xFF, 0xFF, null, 0x00, 0x00, 0x00 }, 512, "ppt", "application/mspowerpoint");

        // common documents
        public readonly static FileType RTF = new FileType(new byte?[] { 0x7B, 0x5C, 0x72, 0x74, 0x66, 0x31 }, "rtf", "application/rtf");
        public readonly static FileType PDF = new FileType(new byte?[] { 0x25, 0x50, 0x44, 0x46 }, "pdf", "application/pdf");
        public readonly static FileType MSDOC = new FileType(new byte?[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }, "", "application/octet-stream");
        //application/xml text/xml
        public readonly static FileType XML = new FileType(new byte?[] { 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22, 0x3F, 0x3E },
                                                            "xml,xul", "text/xml");

        #endregion

        // graphics
        #region Graphics jpeg, png, gif, bmp, ico

        public readonly static FileType JPEG = new FileType(new byte?[] { 0xFF, 0xD8, 0xFF }, "jpg", "image/jpeg");
        public readonly static FileType PNG = new FileType(new byte?[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, "png", "image/png");
        public readonly static FileType GIF = new FileType(new byte?[] { 0x47, 0x49, 0x46, 0x38, null, 0x61 }, "gif", "image/gif");
        public readonly static FileType BMP = new FileType(new byte?[] { 66, 77 }, "bmp", "image/gif");
        public readonly static FileType ICO = new FileType(new byte?[] { 0, 0, 1, 0 }, "ico", "image/x-icon");

        #endregion

        //bmp, tiff
        #region Zip, 7zip, rar, dll_exe, tar, bz2, gz_tgz

        public readonly static FileType GZ_TGZ = new FileType(new byte?[] { 0x1F, 0x8B, 0x08 }, "gz, tgz", "application/x-gz");

        public readonly static FileType ZIP_7z = new FileType(new byte?[] { 66, 77 }, "7z", "application/x-compressed");
        public readonly static FileType ZIP_7z_2 = new FileType(new byte?[] { 0x37, 0x7A, 0xBC, 0xAF, 0x27, 0x1C }, "7z", "application/x-compressed");

        public readonly static FileType ZIP = new FileType(new byte?[] { 0x50, 0x4B, 0x03, 0x04 }, "zip", "application/x-compressed");
        public readonly static FileType RAR = new FileType(new byte?[] { 0x52, 0x61, 0x72, 0x21 }, "rar", "application/x-compressed");
        public readonly static FileType DLL_EXE = new FileType(new byte?[] { 0x4D, 0x5A }, "dll, exe", "application/octet-stream");

        //Compressed tape archive file using standard (Lempel-Ziv-Welch) compression
        public readonly static FileType TAR_ZV = new FileType(new byte?[] { 0x1F, 0x9D }, "tar.z", "application/x-tar");

        //Compressed tape archive file using LZH (Lempel-Ziv-Huffman) compression
        public readonly static FileType TAR_ZH = new FileType(new byte?[] { 0x1F, 0xA0 }, "tar.z", "application/x-tar");

        //bzip2 compressed archive
        public readonly static FileType BZ2 = new FileType(new byte?[] { 0x42, 0x5A, 0x68 }, "bz2,tar,bz2,tbz2,tb2", "application/x-bzip2");


        #endregion


        #region Media ogg, midi, flv, dwg, pst, psd

        // media 
        public readonly static FileType OGG = new FileType(new byte?[] { 103, 103, 83, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0 }, "oga,ogg,ogv,ogx", "application/ogg");
        //MID, MIDI	 	Musical Instrument Digital Interface (MIDI) sound file
        public readonly static FileType MIDI = new FileType(new byte?[] { 0x4D, 0x54, 0x68, 0x64 }, "midi,mid", "audio/midi");

        //FLV	 	Flash video file
        public readonly static FileType FLV = new FileType(new byte?[] { 0x46, 0x4C, 0x56, 0x01 }, "flv", "application/unknown");

        //WAV	 	Resource Interchange File Format -- Audio for Windows file, where xx xx xx xx is the file size (little endian), audio/wav audio/x-wav
        public readonly static FileType WAVE = new FileType(new byte?[] { 0x52, 0x49, 0x46, 0x46, null, null, null, null, 
                                                            0x57, 0x41, 0x56, 0x45, 0x66, 0x6D, 0x74, 0x20	}, "wav", "audio/wav");

        public readonly static FileType PST = new FileType(new byte?[] { 0x21, 0x42, 0x44, 0x4E }, "pst", "application/octet-stream");

        //eneric AutoCAD drawing image/vnd.dwg  image/x-dwg application/acad
        public readonly static FileType DWG = new FileType(new byte?[] { 0x41, 0x43, 0x31, 0x30 }, "dwg", "application/acad");

        //Photoshop image file
        public readonly static FileType PSD = new FileType(new byte?[] { 0x38, 0x42, 0x50, 0x53 }, "psd", "application/octet-stream");

        #endregion

        public readonly static FileType LIB_COFF = new FileType(new byte?[] { 0x21, 0x3C, 0x61, 0x72, 0x63, 0x68, 0x3E, 0x0A }, "lib", "application/octet-stream");
 
        #region Crypto aes, skr, skr_2, pkr

        //AES Crypt file format. (The fourth byte is the version number.)
        public readonly static FileType AES = new FileType(new byte?[] { 0x41, 0x45, 0x53 }, "aes", "application/octet-stream");

        //SKR	 	PGP secret keyring file
        public readonly static FileType SKR = new FileType(new byte?[] { 0x95, 0x00 }, "skr", "application/octet-stream");

        //SKR	 	PGP secret keyring file
        public readonly static FileType SKR_2 = new FileType(new byte?[] { 0x95, 0x01 }, "skr", "application/octet-stream");

        //PKR	 	PGP public keyring file
        public readonly static FileType PKR = new FileType(new byte?[] { 0x99, 0x01 }, "pkr", "application/octet-stream");

        
        #endregion

        /*
         * 46 72 6F 6D 20 20 20 or	 	From
        46 72 6F 6D 20 3F 3F 3F or	 	From ???
        46 72 6F 6D 3A 20	 	From:
        EML	 	A commmon file extension for e-mail files. Signatures shown here
        are for Netscape, Eudora, and a generic signature, respectively.
        EML is also used by Outlook Express and QuickMail.
         */
        public readonly static FileType EML_FROM = new FileType(new byte?[] { 0x4D, 0x54, 0x68, 0x64 }, "midi,mid", "audio/midi");


        //EVTX	 	Windows Vista event log file
        public readonly static FileType ELF = new FileType(new byte?[] { 0x45, 0x6C, 0x66, 0x46, 0x69, 0x6C, 0x65, 0x00 }, "elf", "text/plain");

        /*
        //ISO	 	ISO-9660 CD Disc Image
        public readonly static FileType FLV = new FileType(new byte?[] { 0x46, 0x4C, 0x56, 0x01}, "flv", "application/unknown");


        43 44 30 30 31	 	CD001

        This signature usually occurs at byte offset 32769 (0x8001),
        34817 (0x8801), or 36865 (0x9001).
        More information can be found at MacTech or at ECMA.

        */


        /*
        00 00 00 14 66 74 79 70
        69 73 6F 6D	 	....ftyp
        isom
        MP4	 	ISO Base Media file (MPEG-4) v1

        00 00 00 14 66 74 79 70
        71 74 20 20	 	....ftyp
        qt
        MOV	 	QuickTime movie file

        00 00 00 18 66 74 79 70
        33 67 70 35	 	....ftyp
        3gp5
        MP4	 	MPEG-4 video files
        video/mpeg
        .m2a	audio/mpeg
        .m2v	video/mpeg

        00 00 00 18 66 74 79 70
        6D 70 34 32	 	....ftyp
        mp42
        M4V	 	MPEG-4 video/QuickTime file

        00 00 00 1C 66 74 79 70
        4D 53 4E 56 01 29 00 46
        4D 53 4E 56 6D 70 34 32	 	....ftyp
        MSNV.).F
        MSNVmp42
        MP4	 	MPEG-4 video file

        00 00 00 20 66 74 79 70
        4D 34 41 20	 	... ftyp
        M4A
        M4A	 	Apple Lossless Audio Codec file

        00 00 01 00	 	....
        ICO	 	Windows icon file
        SPL	 	Windows NT/2000/XP printer spool file

        00 00 01 Bx	 	....
        MPEG, MPG	 	MPEG video file
        Trailer:
        00 00 01 B7 (...·)

        00 00 01 BA	 	....º
        MPG, VOB	 	DVD Video Movie File (video/dvd, video/mpeg) or DVD MPEG2
        Trailer:
        00 00 01 B9 (...¹)


        67 69 6d 70 20 78 63 66
        20	 	gimp xcf
        XCF	 	GNU Image Manipulation Program (GIMP) eXperimental Computing Facility (XCF)
        image file

        64 6E 73 2E	 	dns.
        AU	 	Audacity audio file

        66 49 00 00	 	fI..
        SHD	 	Windows NT printer spool file

        66 4C 61 43 00 00 00 22	 	fLaC..."
        FLAC	 	Free Lossless Audio Codec file

        [4 byte offset]
        66 74 79 70 33 67 70 35	 	[4 byte offset]
        ftyp3gp5
        MP4	 	MPEG-4 video files

        [4 byte offset]
        66 74 79 70 4D 34 41 20	 	[4 byte offset]
        ftypM4A
        M4A	 	Apple Lossless Audio Codec file

        [4 byte offset]
        66 74 79 70 4D 53 4E 56	 	[4 byte offset]
        ftypMSNV
        MP4	 	MPEG-4 video file

        [4 byte offset]
        66 74 79 70 69 73 6F 6D	 	[4 byte offset]
        ftypisom
        MP4	 	ISO Base Media file (MPEG-4) v1

        [4 byte offset]
        66 74 79 70 6D 70 34 32	 	[4 byte offset]
        ftypmp42
        M4V	 	MPEG-4 video/QuickTime file

        [4 byte offset]
        66 74 79 70 71 74 20 20	 	[4 byte offset]
        ftypqt


        5F 27 A8 89	 	_'¨‰
        JAR	 	Jar archive

        50 4B 03 04 14 00 08 00
        08 00	 	PK......
        ..
        JAR	 	Java archive

        52 65 74 75 72 6E 2D 50
        61 74 68 3A 20	 	Return-P
        ath:
        EML	 	A commmon file extension for e-mail files.



        50 4B 03 04	 	PK..
        ZIP	 	PKZIP archive file (Ref. 1 | Ref. 2)
        Trailer: filename 50 4B 17 characters 00 00 00
        Trailer: (filename PK 17 characters ...)
        Note: PK are the initals of Phil Katz, co-creator of the ZIP file format and author of PKZIP.
        ZIP	 	Apple Mac OS X Dashboard Widget, Aston Shell theme, Oolite eXpansion Pack,
        Opera Widget, Pivot Style Template, Rockbox Theme package, Simple Machines
        Forums theme, SubEthaEdit Mode, Trillian zipped skin, Virtual Skipper skin
        JAR	 	Java archive; compressed file package for classes and data
        KMZ	 	Google Earth saved working session file
        KWD	 	KWord document
        ODT, ODP, OTT	 	OpenDocument text document, presentation, and text document template, respectively.
        SXC, SXD, SXI, SXW	 	OpenOffice spreadsheet (Calc), drawing (Draw), presentation (Impress),
        and word processing (Writer) files, respectively.
        SXC	 	StarOffice spreadsheet
        WMZ	 	Windows Media compressed skin file
        XPI	 	Mozilla Browser Archive
        XPS	 	XML paper specification file
        XPT	 	eXact Packager Models

        application/x-compress
        .z	application/x-compressed
        .zip	application/x-compressed
        .zip	application/x-zip-compressed
        .zip	application/zip
        .zip	multipart/x-zip


        50 4B 03 04 0A 00 02 00	 	PK......
        EPUB	 	Open Publication Structure eBook file. (Should also include the string:
        mimetypeapplication/epub+zip)

        50 4B 03 04 14 00 01 00
        63 00 00 00 00 00	 	PK......
        c.....
        ZIP	 	ZLock Pro encrypted ZIP

        50 4B 03 04 14 00 06 00	 	PK......
        DOCX, PPTX, XLSX	 	Microsoft Office Open XML Format (OOXML) Document
        NOTE: There is no subheader for MS OOXML files as there is with
        DOC, PPT, and XLS files. To better understand the format of these files,
        rename any OOXML file to have a .ZIP extension and then unZIP the file;
        look at the resultant file named [Content_Types].xml to see the content
        types. In particular, look for the <Override PartName= tag, where you
        will find word, ppt, or xl, respectively.

        Trailer: Look for 50 4B 05 06 (PK..) followed by 18 additional bytes
        at the end of the file.

        50 4B 03 04 14 00 08 00
        08 00	 	PK......
        ..
        JAR	 	Java archive

        50 4B 05 06	 	PK..
        50 4B 07 08	 	PK..
        ZIP	 	PKZIP empty and multivolume archive file, respectively

        [30 byte offset]
        50 4B 4C 49 54 45	 	[30 byte offset]
        PKLITE
        ZIP	 	PKLITE compressed ZIP archive (see also PKZIP)

        [526 byte offset]
        50 4B 53 70 58	 	[526 byte offset]
        PKSFX
        ZIP	 	PKSFX self-extracting executable compressed file (see also PKZIP)
        */

        // number of bytes we read from a file
        private const int MaxHeaderSize = 560;  // some file formats have headers offset to 512 bytes

        

        #endregion

        #region Main Methods

        public static void SaveToXmlFile(string path)
        {
            using (FileStream file = File.OpenWrite(path))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(types.GetType());
                serializer.Serialize(file, types);
            }
        }

        public static void LoadFromXmlFile(string path)
        {
            using (FileStream file = File.OpenRead(path))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(types.GetType());
                types = (List<FileType>)serializer.Deserialize(file);
            }
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
            // read first n-bytes from the file
            return GetFileType(() => ReadFileHeader(file, MaxHeaderSize));
        }

        /// <summary>
        /// Read header of a file and depending on the information in the header
        /// return object FileType.
        /// Return null in case when the file type is not identified. 
        /// Throws Application exception if the file can not be read or does not exist
        /// </summary>
        /// <param name="fileHeaderReadFunc">A function which returns the bytes found</param>
        /// <returns>FileType or null not identified</returns>
        public static FileType GetFileType(Func<byte[]> fileHeaderReadFunc)
        {
            // read first n-bytes from the file
            Byte[] fileHeader = fileHeaderReadFunc();

            // compare the file header to the stored file headers
            foreach (FileType type in types)
            {
                int matchingCount = 0;
                for (int i = 0; i < type.Header.Length; i++)
                {
                    // if file offset is not set to zero, we need to take this into account when comparing.
                    // if byte in type.header is set to null, means this byte is variable, ignore it
                    if (type.Header[i] != null && type.Header[i] != fileHeader[i + type.HeaderOffset])
                    {
                        // if one of the bytes does not match, move on to the next type
                        matchingCount = 0;
                        break;
                    }
                    else
                    {
                        matchingCount++;
                    }
                }
                if (matchingCount == type.Header.Length)
                {
                    // if all the bytes match, return the type
                    return type;
                }
            }
            // if none of the types match, return null
            return null;
        }

        /// <summary>
        /// Reads the file header - first (16) bytes from the file
        /// </summary>
        /// <param name="file">The file to work with</param>
        /// <returns>Array of bytes</returns>
        private static Byte[] ReadFileHeader(FileInfo file, int MaxHeaderSize)
        {
            Byte[] header = new byte[MaxHeaderSize];
            try  // read file
            {
                using (FileStream fsSource = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    // read first symbols from file into array of bytes.
                    fsSource.Read(header, 0, MaxHeaderSize);
                }   // close the file stream

            }
            catch (Exception e) // file could not be found/read
            {
                throw new ApplicationException("Could not read file : " + e.Message);
            }

            return header;
        }

        /// <summary>
        /// Determines whether provided file belongs to one of the provided list of files
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="requiredTypes">The required types.</param>
        /// <returns>
        ///   <c>true</c> if file of the one of the provided types; otherwise, <c>false</c>.
        /// </returns>
        public static bool isFileOfTypes(this FileInfo file, List<FileType> requiredTypes)
        {
            FileType currentType = file.GetFileType();

            if (null == currentType)
            {
                return false;
            }

            return requiredTypes.Contains(currentType);
        }

        /// <summary>
        /// Determines whether provided file belongs to one of the provided list of files,
        /// where list of files provided by string with Comma-Separated-Values of extensions
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="requiredTypes">The required types.</param>
        /// <returns>
        ///   <c>true</c> if file of the one of the provided types; otherwise, <c>false</c>.
        /// </returns>
        public static bool isFileOfTypes(this FileInfo file, String CSV)
        {
            List<FileType> providedTypes = GetFileTypesByExtensions(CSV);

            return file.isFileOfTypes(providedTypes);
        }

        /// <summary>
        /// Gets the list of FileTypes based on list of extensions in Comma-Separated-Values string
        /// </summary>
        /// <param name="CSV">The CSV String with extensions</param>
        /// <returns>List of FileTypes</returns>
        private static List<FileType> GetFileTypesByExtensions(String CSV)
        {
            String[] extensions = CSV.ToUpper().Replace(" ", "").Split(',');

            List<FileType> result = new List<FileType>();

            foreach (FileType type in types)
            {
                if (extensions.Contains(type.Extension.ToUpper()))
                {
                    result.Add(type);
                }
            }
            return result;
        }

        #endregion

        #region isType functions


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
            FileType actualType = GetFileType(file);

            if (null == actualType)
                return false;

            return (actualType.Equals(type));
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
            return file.IsType(PDF);
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
            return fileInfo.IsType(WORD);
        }


        /// <summary>
        /// Determines whether the specified file is zip archive
        /// </summary>
        /// <param name="fileInfo">The file info.</param>
        /// <returns>
        ///   <c>true</c> if the specified file info is zip; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsZip(this FileInfo fileInfo)
        {
            return fileInfo.IsType(ZIP);
        }

        /// <summary>
        /// Determines whether the specified file is MS Excel spreadsheet
        /// </summary>
        /// <param name="fileInfo">The FileInfo</param>
        /// <returns>
        ///   <c>true</c> if the specified file info is excel; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsExcel(this FileInfo fileInfo)
        {
            return fileInfo.IsType(EXCEL);
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
            return fileInfo.IsType(JPEG);
        }

        /// <summary>
        /// Determines whether the specified file is RAR-archive.
        /// </summary>
        /// <param name="fileInfo">The FileInfo.</param>
        /// <returns>
        ///   <c>true</c> if the specified file info is RAR; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRar(this FileInfo fileInfo)
        {
            return fileInfo.IsType(RAR);
        }

        /// <summary>
        /// Determines whether the specified file is RTF document.
        /// </summary>
        /// <param name="fileInfo">The FileInfo.</param>
        /// <returns>
        ///   <c>true</c> if the specified file is RTF; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRtf(this FileInfo fileInfo)
        {
            return fileInfo.IsType(RTF);
        }

        /// <summary>
        /// Determines whether the specified file is PNG.
        /// </summary>
        /// <param name="fileInfo">The FileInfo object</param>
        /// <returns>
        ///   <c>true</c> if the specified file info is PNG; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPng(this FileInfo fileInfo)
        {
            return fileInfo.IsType(PNG);
        }

        /// <summary>
        /// Determines whether the specified file is Microsoft PowerPoint Presentation
        /// </summary>
        /// <param name="fileInfo">The FileInfo object.</param>
        /// <returns>
        ///   <c>true</c> if the specified file info is PPT; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPpt(this FileInfo fileInfo)
        {
            return fileInfo.IsType(PPT);
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
            return fileInfo.IsType(GIF);
        }


        /// <summary>
        /// Checks if the file is executable
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static bool IsExe(this FileInfo fileInfo)
        {
            return fileInfo.IsType(DLL_EXE);
        }


        /// <summary>
        /// Check if the file is Microsoft Installer.
        /// Beware, many Microsoft file types are starting with the same header. 
        /// So use this one with caution. If you think the file is MSI, just need to confirm, use this method. 
        /// But it could be MSWord or MSExcel, or Powerpoint... 
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static bool IsMsi(this FileInfo fileInfo)
        {
            // MSI has a generic DOCFILE header. Also it matches PPT files
            return fileInfo.IsType(PPT) || fileInfo.IsType(MSDOC);
        }
        #endregion
    }
}
