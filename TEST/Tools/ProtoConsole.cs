using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using Xunit.Abstractions;

namespace KUBC.DAYZ.GAME.Tools;

/// <summary>
/// Прототип приложения автоматической
/// обработки файлов сервера
/// </summary>
public class ProtoConsole : AbstractTest
{
    

    private const string CONFIG_PATH = "Path";

    

    private MissionFiles.ServerConfigFiles serverFiles;

    ITestOutputHelper TestOutput;

    public ProtoConsole(ITestOutputHelper output)
    {
        TestOutput = output;
        
        var userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Assert.NotNull(userDocs);
        var serverPath = Config.GetValue<string>(CONFIG_PATH);
        Assert.NotNull(serverPath);
        serverFiles = new(new DirectoryInfo($"{userDocs}\\{serverPath}"));
        Assert.True(serverFiles.RootPath.Exists);
        
    }

    [Fact]
    public void RemoveFood()
    {
        var removeSection = Config.GetSection("RemoveFood");
        Assert.NotNull(removeSection);
        var toolConfig = new Tools.ItemTypes.Config();
        removeSection.Bind(toolConfig);
        var tool = new Tools.ItemTypes.Remover.RemoveTool(LoggerFactory.CreateLogger("R"), toolConfig, serverFiles);
        tool.Apply();
        foreach (var line in tool.Report)
        {
            TestOutput.WriteLine("{0}:{1}", line.Key, line.Value);
        }
    }

    [Fact]
    public void TransportLifeTime()
    {
        var lifeTimeSection = Config.GetSection("TransportLifeTime");
        Assert.NotNull(lifeTimeSection);
        LongLife(lifeTimeSection);
    }

    [Fact]
    public void BaseItemsLifeTime()
    {
        var lifeTimeSection = Config.GetSection("BaseItemsLifeTime");
        Assert.NotNull(lifeTimeSection);
        LongLife(lifeTimeSection);
    }

    [Fact]
    public void ExplosiveLifeTime()
    {
        var lifeTimeSection = Config.GetSection("ExplosiveLifeTime");
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
