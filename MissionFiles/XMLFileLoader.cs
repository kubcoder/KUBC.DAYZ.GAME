using KUBC.DAYZ.GAME.MissionFiles.Config.Economy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles;

/// <summary>
/// Универсальный загрузчик файла XML
/// </summary>
/// <typeparam name="T">Тип данных файла</typeparam>
/// <param name="file">Имя файла</param>
public abstract class XMLFileLoader<T> where T : IXmlSerializable
{
    /// <summary>
    /// Получить информацию о файле для загрузки
    /// </summary>
    protected abstract FileInfo GetFile();

    /// <summary>
    /// Загрузить файл
    /// </summary>
    /// <returns>Экземпляр данных</returns>
    public T Load()
    {
        var result = Activator.CreateInstance<T>();
        using (var file = GetFile().OpenRead())
        {
            using (var reader = XmlReader.Create(file))
            {
                result.ReadXml(reader);
            }
        }
        return result;
    }
}
