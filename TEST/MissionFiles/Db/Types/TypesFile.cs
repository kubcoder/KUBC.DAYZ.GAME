namespace KUBC.DAYZ.GAME.MissionFiles.Db.Types;

/// <summary>
/// Класс тестирования инструментов
/// работы с файлом types.xml
/// </summary>
public class TypesFile
{
    [Fact]
    public void ReadWriteFile()
    {
        var fileInfo = new FileInfo("MissionFiles\\Db\\Types\\types.xml");
        Assert.True(fileInfo.Exists);
        var loader = new TypeFileLoader(fileInfo);

        var types = loader.Load();
        Assert.Equal(4, types.Count);
        var asval = types.Where(x => x.Name == "ASVAL").FirstOrDefault();
        Assert.NotNull(asval);


        var extFile = new FileInfo("types.xml");
        loader = new TypeFileLoader(extFile);
        loader.Save(types);

    }
}
