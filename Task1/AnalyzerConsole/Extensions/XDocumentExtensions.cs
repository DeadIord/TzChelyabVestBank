using System.Xml.Linq;

namespace AnalyzerConsole.Extensions;

public static class XDocumentExtensions
{
    public static int CountUniqueElements(this XDocument document)
    {
        return document.Descendants()
            .Select(e => e.Name)
            .Distinct()
            .Count();
    }
}
