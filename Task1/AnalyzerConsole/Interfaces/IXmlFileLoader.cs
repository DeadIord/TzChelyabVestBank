using System.Xml.Linq;

namespace AnalyzerConsole.Interfaces;

/// <summary>
/// Интерфейс,  определяет метод для загрузки XML документа из файла
/// </summary>
public interface IXmlFileLoader
{
    /// <summary>
    /// Загружает XML документ из указанного файла.
    /// </summary>
    /// <param name="filePath">Путь к файлу, содержащему XML документ</param>
    /// <returns>Загруженный XML документ</returns>
    XDocument Load(string filePath);
}
