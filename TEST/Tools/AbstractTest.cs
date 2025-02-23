using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.Tools;

/// <summary>
/// Абстрактный класс тестирования
/// </summary>
public abstract class AbstractTest
{
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
    }
}
