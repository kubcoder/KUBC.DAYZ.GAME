using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace KUBC.DAYZ.GAME.Tools;

/// <summary>
/// Прототип приложения автоматической
/// обработки файлов сервера
/// </summary>
public class ProtoConsole
{
    /// <summary>
    /// Имя файла конфигурации
    /// </summary>
    private const string configFile = "config.json";

    private const string CONFIG_PATH = "Path";

    /// <summary>
    /// Конфигурация автоматической обработки
    /// </summary>
    private IConfiguration config;

    private MissionFiles.ServerConfigFiles serverFiles;

    private ILoggerFactory LoggerFactory;

    ITestOutputHelper TestOutput;

    public ProtoConsole(ITestOutputHelper output)
    {
        TestOutput = output;
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddJsonFile(configFile);
        config = configBuilder.Build();
        var userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Assert.NotNull(userDocs);
        var serverPath = config.GetValue<string>(CONFIG_PATH);
        Assert.NotNull(serverPath);
        serverFiles = new(new DirectoryInfo($"{userDocs}\\{serverPath}"));
        Assert.True(serverFiles.RootPath.Exists);
        Console.OutputEncoding = Encoding.UTF8;
        LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(options =>
        {
            options.AddConsole();
            options.SetMinimumLevel(LogLevel.Trace);
        });
    }

    [Fact]
    public void RemoveFood()
    {
        var removeSection = config.GetSection("RemoveFood");
        Assert.NotNull(removeSection);
        var toolConfig = new Tools.ItemTypes.Config();
        removeSection.Bind(toolConfig);
        var tool = new Tools.ItemTypes.Remover.RemoveTool(LoggerFactory.CreateLogger("R"), toolConfig, serverFiles);
        tool.Apply();
        foreach(var line in tool.Report)
        {
            TestOutput.WriteLine("{0}:{1}", line.Key, line.Value);
        }
    }

    [Fact]
    public void TransportLifeTime()
    {
        var lifeTimeSection = config.GetSection("TransportLifeTime");
        Assert.NotNull(lifeTimeSection);
        LongLife(lifeTimeSection);
    }

    [Fact]
    public void BaseItemsLifeTime()
    {
        var lifeTimeSection = config.GetSection("BaseItemsLifeTime");
        Assert.NotNull(lifeTimeSection);
        LongLife(lifeTimeSection);
    }

    [Fact]
    public void ExplosiveLifeTime()
    {
        var lifeTimeSection = config.GetSection("ExplosiveLifeTime");
        Assert.NotNull(lifeTimeSection);
        LongLife(lifeTimeSection);
    }

    private void LongLife(IConfigurationSection section)
    {
        var toolConfig = new Tools.ItemTypes.LifeTime.Config();
        section.Bind(toolConfig);
        var tool = new Tools.ItemTypes.LifeTime.LifeTimeTool(LoggerFactory.CreateLogger("LT"), toolConfig, serverFiles);
        tool.Apply();
        foreach (var line in tool.Report)
        {
            TestOutput.WriteLine("{0}:{1}=>{2}", line.ItemName, line.OldLifeTime, line.NewLifeTime);
        }
    }

}
