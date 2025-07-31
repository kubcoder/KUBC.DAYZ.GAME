using KUBC.DAYZ.GAME.Tools;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Xunit.Abstractions;

namespace KUBC.DAYZ.GAME.MissionFiles.Config.RandomPresets;

public class File(ITestOutputHelper output) : AbstractTest
{
    [Fact]
    public void TestPresetItem()
    {
        var item = new PresetItem()
        {
            Chance = 0.2,
            Name = "testItem"
        };
        string xmlString = WriteXML(item);
        output.WriteLine(xmlString);
        var readItem = new PresetItem();
        ReadXML(readItem, xmlString);
        Assert.Equal(item.Name, readItem.Name);
        Assert.Equal(item.Chance, readItem.Chance);
    }

    private string WriteXML(IXmlSerializable item)
    {
        using (var stream = new MemoryStream())
        {
            var encoding = Encoding.UTF8;
            var preamble = encoding.GetPreamble();
            var settings = new XmlWriterSettings()
            {
                Indent = true,
                Encoding = encoding
            };
            using (var xmlWriter = XmlWriter.Create(stream, settings))
            {

                item.WriteXml(xmlWriter);
                xmlWriter.Flush();
                var preambleLen = encoding.GetPreamble().Length;
                var bytes = stream.ToArray();
                var xml = Encoding.UTF8.GetString(bytes, preambleLen, bytes.Length - preambleLen);
                return xml;
            }
        }
    }

    private void ReadXML(IXmlSerializable item, string xmlString)
    {
        using (var stream = new StringReader(xmlString))
        {
            using (var xmlReader = XmlReader.Create(stream))
            {
                item.ReadXml(xmlReader);
            }
        }
    }

    private List<PresetItem> testItems = [];

    private void CreateTestItems()
    {
        testItems.Clear();
        testItems.Add(new PresetItem()
        {
            Name = "test1",
            Chance = 0.1
        });
        testItems.Add(new PresetItem()
        {
            Name = "test2",
            Chance = 0.2
        });
    }

    private void CheckItemList(List<PresetItem> readItems)
    {
        Assert.Equal(testItems.Count, readItems.Count);
        for (int i = 0; i < testItems.Count; i++)
        {
            Assert.Equal(testItems[i].Name, readItems[i].Name);
            Assert.Equal(testItems[i].Chance, readItems[i].Chance);
        }
    }

    /// <summary>
    /// Тестируем сохранение в карго
    /// </summary>
    [Fact]
    public void TestCargo()
    {
        CreateTestItems();
        var cargo = new Cargo()
        {
            Name = "cargo4",
            Chance = 0.4,
            Items = testItems
        };
        var xmlString = WriteXML(cargo);
        output.WriteLine(xmlString);
        var readCargo = new Cargo();
        ReadXML(readCargo, xmlString);
        Assert.Equal(cargo.Name, readCargo.Name);
        Assert.Equal(cargo.Chance, readCargo.Chance);
        CheckItemList(readCargo.Items);
    }

    /// <summary>
    /// Тестируем сохранение в карго
    /// </summary>
    [Fact]
    public void Attachments()
    {
        CreateTestItems();
        var atach = new Attachments()
        {
            Name = "attach1",
            Chance = 0.4,
            Items = testItems
        };
        var xmlString = WriteXML(atach);
        output.WriteLine(xmlString);
        var readCargo = new Cargo();
        ReadXML(readCargo, xmlString);
        Assert.Null(readCargo.Name);
        var readAttach = new Attachments();
        ReadXML(readAttach, xmlString);
        Assert.Equal(atach.Name, readAttach.Name);
        Assert.Equal(atach.Chance, readAttach.Chance);
        CheckItemList(readAttach.Items);
    }

    [Fact]
    public void WriteReadFile()
    {
        var fileInfo = new FileInfo("MissionFiles\\Config\\RandomPresets\\cfgrandompresets.xml");
        Assert.True(fileInfo.Exists);
        var cfg = new CfgRandomPresets();
        using (var file = fileInfo.OpenRead())
        {
            using (var reader = XmlReader.Create(file))
            {
                cfg.ReadXml(reader);
            }
        }
        var extFile = new FileInfo("cfgrandompresets.xml");
        using (var file = extFile.Create())
        {
            using (var writer = XmlWriter.Create(file, new() { Indent = true }))
            {
                cfg.WriteXml(writer);
            }
        }
        var readCfg = new CfgRandomPresets();
        using (var file = extFile.OpenRead())
        {
            using (var reader = XmlReader.Create(file))
            {
                readCfg.ReadXml(reader);
            }
        }
        Assert.Equal(cfg.Count, readCfg.Count);
    }
}
