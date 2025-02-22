using System.Xml;

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
        var types = new File();
        using (var file = fileInfo.OpenRead())
        {
            using(var reader = XmlReader.Create(file))
            {
                types.ReadXml(reader);
                Assert.Equal(4, types.Count);
                var asval = types.Where(x => x.Name == "ASVAL").FirstOrDefault();
                Assert.NotNull(asval);
            }
        }
        var extFile = new FileInfo("types.xml");
        using(var file = extFile.Create())
        {
            using(var writer = XmlWriter.Create(file, new() { Indent = true}))
            {
                types.WriteXml(writer);
            }
        }
        
    }
}
