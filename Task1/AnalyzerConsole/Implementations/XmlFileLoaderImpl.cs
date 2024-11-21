using AnalyzerConsole.Interfaces;
using System.Xml.Linq;

namespace AnalyzerConsole.Implementations;

internal class XmlFileLoaderImpl : IXmlFileLoader
{
    public XDocument Load(string filePath)
    {
        return XDocument.Load(filePath);
    }
}
