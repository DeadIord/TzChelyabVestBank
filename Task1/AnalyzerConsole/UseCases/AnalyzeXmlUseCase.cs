using AnalyzerConsole.Extensions;
using AnalyzerConsole.Interfaces;
using System.Xml.Linq;

namespace AnalyzerConsole.UseCases
{
    public class AnalyzeXmlUseCase
    {
        private readonly IXmlFileLoader _fileLoader;
        private readonly IXmlAnalyzer _analyzer;

        public AnalyzeXmlUseCase(IXmlFileLoader fileLoader, IXmlAnalyzer analyzer)
        {
            _fileLoader = fileLoader;
            _analyzer = analyzer;
        }

        public void Execute(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл по пути '{filePath}' не найден.");

                return;
            }

            try
            {
                XDocument xmlDoc = _fileLoader.Load(filePath);

                int elementCount = _analyzer.GetElementCount(xmlDoc);
                Console.WriteLine($"Общее количество элементов в XML-файле: {elementCount}");

                _analyzer.PrintElementNames(xmlDoc);

                double sumValues = _analyzer.SumAttributeValues(xmlDoc, "value");
                Console.WriteLine($"Сумма значений атрибута 'value': {sumValues}");

                int uniqueElementCount = xmlDoc.CountUniqueElements();
                Console.WriteLine($"Количество уникальных элементов: {uniqueElementCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при обработке файла: {ex.Message}");
            }
        }
    }
}
