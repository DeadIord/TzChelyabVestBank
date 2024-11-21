using System.Xml.Linq;

namespace AnalyzerConsole.Interfaces;

/// <summary>
/// Интерфейс, определяет методы для анализа XML документа
/// </summary>
public interface IXmlAnalyzer
{
    /// <summary>
    /// Вычисляет общее количество элементов в предоставленном XML документе 
    /// </summary>
    /// <param name="document">XML документ для анализа </param>
    /// <returns>Общее количество элементов в XML документе </returns>
    int GetElementCount(XDocument document);

    /// <summary>
    /// Выводит в консоль имена всех элементов содержащихся в XML документе
    /// </summary>
    /// <param name="document">XML документ для анализа </param>
    void PrintElementNames(XDocument document);

    /// <summary>
    /// Суммирует числовые значения указанного атрибута для всех элементов XML документа 
    /// </summary>
    /// <param name="document">XML документ для анализа </param>
    /// <param name="attributeName">Имя атрибута, значения которого нужно суммировать </param>
    /// <returns>Сумма числовых значений указанного атрибута </returns>
    double SumAttributeValues(XDocument document, string attributeName);
}
