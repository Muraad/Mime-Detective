Mime-Detective
==============

Mime type for files.

Based on https://filetypedetective.codeplex.com/


Usage 

```csharp

// Both ways are writing the data to a temp file
// to get a FileInfo. GetFileType are extension methods
byte[] fileData = ...;
FileType fileType = fileData.GetFileType();
   
// or 
Stream fileDataStream = ...;
FileType fileType = fileDataStream.GetFileType();

// If writing to a temp file is not practicable use it like this
byte[] fileData = ...;
FileType fileType = MimeTypes.GetFileType(() => fileData);
   
```
