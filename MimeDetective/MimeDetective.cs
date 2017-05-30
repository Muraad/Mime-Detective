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

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MimeDetective
{
    public class MimeDetective
    {
        public static FileType LearnMimeType(FileInfo file, string mimeType, int headerSize, int offset = 0)
        {
            byte?[] data = new byte?[headerSize];
            using (FileStream stream = file.OpenRead())
            {
                int b = 0;
                for (int i = 0; i < headerSize; i++)
                {
                    data[i] = (byte)((b = stream.ReadByte()) == -1 ? 0 : b);
                    if (b == -1)
                        break;
                }
            }
            return new FileType(data, offset, file.Extension, mimeType);
        }

        public static FileType LearnMimeType(FileInfo first, FileInfo second, string mimeType, int maxHeaderSize = 12, int minMatches = 2, int maxNonMatch = 3)
        {
            byte?[] header = null;

            List<byte?> headerList = new List<byte?>();

            using (Stream firstFile = first.OpenRead())
            using (Stream secondFile = second.OpenRead())
            {
                bool match = false;
                int missmatchCounter = 0;       // missmatches after first match

                int bFst = 0, bSnd = 0;         // current bytes
                int index = 0;
                int offset = 0;             // index of first match

                // Read from both files until one of the file streams reaches the end.
                while ((bFst = firstFile.ReadByte()) != -1 &&
                      (bSnd = secondFile.ReadByte()) != -1)
                {

                    bFst = firstFile.ReadByte();
                    bSnd = secondFile.ReadByte();

                    if (bFst == bSnd)
                    {
                        if (!match)
                        {
                            match = true;       // first match
                            offset = index;
                        }

                        headerList.Add((byte)bFst);     // add match to header 
                    }
                    else
                    {
                        if (match)
                        {      // if there was a match before 

                            // no more matching

                            if (missmatchCounter < maxNonMatch)
                            {
                                headerList.Add(null);       // Add a null header, this could be non generic, file size for example
                                missmatchCounter++;
                            }
                            else
                                break;  // too much missmatches after the first match 
                        }
                    }
                    if (headerList.Count == maxHeaderSize)
                        break;
                    index++;
                }

                FileType type = null;

                if (headerList.Count((b) => b != null) >= minMatches)       // check for enough non null byte? ´s.
                {
                    header = headerList.ToArray();
                    type = new FileType(header, offset, first.Extension, mimeType);
                }

                return type;
            }
        }
    }
}
