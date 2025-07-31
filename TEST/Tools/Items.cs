using Xunit.Abstractions;

namespace KUBC.DAYZ.GAME.Tools;

/// <summary>
/// Тестируем инструменты 
/// настроек игровых предметов
/// </summary>
public class Items(ITestOutputHelper output) : AbstractTest
{


    [Fact]
    public void TestRemover()
    {
        var configServer = new MissionFiles.ServerConfigFiles(GetInstance(2));
        var configRemover = new KUBC.DAYZ.GAME.Tools.ItemTypes.Config()
        {
            ItemNames = [
                "Van_Jetski_Black",
                "dzn_athena_planning",
                "AK101"
                ],
            Enable = true
        };
        var remover = new ItemTypes.Remover.RemoveTool(LoggerFactory.CreateLogger("R"), configRemover, configServer);
        remover.Apply();
        foreach (var line in remover.Report)
        {
            output.WriteLine("{0}:{1}", line.Key, line.Value);
        }
        foreach (var fileName in configServer.TypesFiles)
        {
            var loader = new MissionFiles.Db.Types.TypeFileLoader(fileName);
            var fileData = loader.Load();
            foreach (var itemName in configRemover.ItemNames)
            {
                Assert.Null(fileData.Where(x => x.Name == itemName).FirstOrDefault());
            }
        }
    }


    [Fact]
    public void TestLifeTime()
    {
        var configServer = new MissionFiles.ServerConfigFiles(GetInstance(2));
        var configTool = new KUBC.DAYZ.GAME.Tools.ItemTypes.LifeTime.Config()
        {
            ItemNames = [
                "AK101"
                ],
            Enable = true
        };
        var lifeTimeToll = new ItemTypes.LifeTime.LifeTimeTool(LoggerFactory.CreateLogger("R"), configTool, configServer);
        lifeTimeToll.Apply();
        foreach (var line in lifeTimeToll.Report)
        {
            output.WriteLine("{0}:{1}=>{2}", line.ItemName, line.OldLifeTime, line.NewLifeTime);
        }
        foreach (var fileName in configServer.TypesFiles)
        {
            var loader = new MissionFiles.Db.Types.TypeFileLoader(fileName);
            var fileData = loader.Load();
            foreach (var itemName in configTool.ItemNames)
            {
                var item = fileData.Where(x => x.Name == itemName).FirstOrDefault();
                if (item != null)
                {
                    Assert.Equal(configTool.LifeTime, item.LifeTime);
                }
            }
        }
    }
}
