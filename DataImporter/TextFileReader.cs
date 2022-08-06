using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataImporter
{
    internal class TextFileReader : IFileReader
    {

        public List<string> ReadLinesInFile(string path)
        {
            return File.Exists(path) ? new List<string>(File.ReadAllLines(path)) : new List<string>();
        }
    }
}
