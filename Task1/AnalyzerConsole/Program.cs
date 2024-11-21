using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AnalyzerConsole.Interfaces;
using AnalyzerConsole.Implementations;
using AnalyzerConsole.UseCases;

namespace AnalyzerConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IXmlFileLoader, XmlFileLoaderImpl>();
                    services.AddTransient<IXmlAnalyzer, XmlAnalyzerImpl>();
                    services.AddTransient<AnalyzeXmlUseCase>();
                })
                .Build();

            var useCase = host.Services.GetRequiredService<AnalyzeXmlUseCase>();

            if (args.Length == 0)
            {
                Console.WriteLine("Пожалуйста, укажите путь к XML-файлу в качестве аргумента командной строки.");
                return;
            }

            string filePath = args[0];
            useCase.Execute(filePath);
        }
    }
}
