using System.Xml;

namespace KUBC.DAYZ.GAME.MissionFiles.Db.Types;

/// <summary>
/// Класс тестирования инструментов
/// работы с файлом types.xml
/// </summary>
public class TypesFile
{
    [Fact]
    public void ReadFile()
    {
        var fileInfo = new FileInfo("MissionFiles\\Db\\Types\\types.xml");
        Assert.True(fileInfo.Exists);
        var types = new File();
        using (var file = fileInfo.OpenRead())
        {
            using(var reader = XmlReader.Create(file))
            {
                types.ReadXml(reader);
            }
        }
        
        
    }
}
