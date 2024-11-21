using AnalyzerConsole.Interfaces;
using System.Globalization;
using System.Xml.Linq;

namespace AnalyzerConsole.Implementations;

public class XmlAnalyzerImpl : IXmlAnalyzer
{
    public int GetElementCount(XDocument document)
    {
        return document.Descendants().Count();
    }

    public void PrintElementNames(XDocument document)
    {
        Console.WriteLine("Список элементов:");
        foreach (var elem in document.Descendants())
        {
            Console.WriteLine($"Элемент: {elem.Name}");
        }
    }

    public double SumAttributeValues(XDocument document, string attributeName)
    {
        return document.Descendants()
            .Attributes(attributeName)
             .Where(attr => double.TryParse(attr.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            .Sum(attr => double.Parse(attr.Value, CultureInfo.InvariantCulture));


    }
}
