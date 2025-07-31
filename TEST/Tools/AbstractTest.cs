using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace KUBC.DAYZ.GAME.Tools;

/// <summary>
/// Абстрактный класс тестирования
/// </summary>
public abstract class AbstractTest
{

    /// <summary>
    /// Имя файла конфигурации
    /// </summary>
    private const string configFile = "config.json";

    /// <summary>
    /// Конфигурация автоматической обработки
    /// </summary>
    public readonly IConfiguration Config;
    /// <summary>
    /// Получаем папку тестового
    /// экземпляра файлов сервера
    /// </summary>
    /// <param name="number">Номер экземпляра</param>
    /// <returns></returns>
    public DirectoryInfo GetInstance(int number)
    {
        return new DirectoryInfo($"TestInstances\\{number.ToString("D2")}");
    }

    public ILoggerFactory LoggerFactory;

    public AbstractTest()
    {
        Console.OutputEncoding = Encoding.UTF8;
        LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(options =>
        {
            options.AddConsole();
            options.SetMinimumLevel(LogLevel.Trace);
        });
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddJsonFile(configFile);
        Config = configBuilder.Build();
    }
}
