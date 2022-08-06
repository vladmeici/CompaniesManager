using System;
using System.Collections.Generic;
using System.Text;

namespace DataImporter
{
    internal interface IFileReader
    {
        List<string> ReadLinesInFile(string path);
    }
}
