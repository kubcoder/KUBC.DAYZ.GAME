using KUBC.DAYZ.GAME.MissionFiles.DB.Economy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Types
{
    /// <summary>
    /// Класс настройки игрового предмета
    /// </summary>
    public class Item : XMLConfig
    {
        /// <summary>
        /// Имя узла в XML файле
        /// </summary>
        public const string NODENAME = "type";
        /// <summary>
        /// Аттрибут указывающий на имя класса итема
        /// </summary>
        public const string ATTRNAME = "name";
        /// <summary>
        /// Флаги обработки итема центральной экономикой
        /// </summary>
        public CEFlags Flags { get; set; }
        /// <summary>
        /// Имя узла категории
        /// </summary>
        private const string CATEGORYNODE = "category";
        /// <summary>
        /// Категории итема
        /// </summary>
        public ObservableCollection<string> Categories { get; set; }
        /// <summary>
        /// Имя узла количества
        /// </summary>
        private const string VALUENODE = "value";
        /// <summary>
        /// Категории итема
        /// </summary>
        public ObservableCollection<string> Values { get; set; }
        /// <summary>
        /// Имя узла назначения итема
        /// </summary>
        private const string USAGESNODE = "usage";
        /// <summary>
        /// Имя назначения итема
        /// </summary>
        public ObservableCollection<string> Usages { get; set; }
        /// <summary>
        /// Имя узла назначения итема
        /// </summary>
        private const string TAGNODE = "tag";
        /// <summary>
        /// Имя назначения итема
        /// </summary>
        public ObservableCollection<string> Tags { get; set; }
        /// <summary>
        /// Создаем пустой итем
        /// </summary>
        public Item(string? name = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _sectName = name;
            }
            //TODO: Тут возможно придется прикручивать отслеживание событий и перенаправление их наверх
            Flags = new();
            Categories = new();
            Values = new();
            Usages = new();
            Tags = new();
        }
        /// <inheritdoc/>
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(ATTRNAME, _sectName);
            base.WriteXml(writer);
            writer.WriteStartElement(CEFlags.NODENAME);
            Flags.WriteXml(writer);
            foreach(var cat  in Categories) 
            {
                writer.WriteStartElement(CATEGORYNODE);
                writer.WriteAttributeString(ATTRNAME, cat);
                writer.WriteEndElement();
            }
            foreach (var val in Values)
            {
                writer.WriteStartElement(VALUENODE);
                writer.WriteAttributeString(ATTRNAME, val);
                writer.WriteEndElement();
            }
            foreach (var usage in Usages)
            {
                writer.WriteStartElement(USAGESNODE);
                writer.WriteAttributeString(ATTRNAME, usage);
                writer.WriteEndElement();
            }
            foreach (var tag in Tags)
            {
                writer.WriteStartElement(USAGESNODE);
                writer.WriteAttributeString(ATTRNAME, tag);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        /// <inheritdoc/>
        public override void ReadXml(XmlReader reader)
        {
            Params.Clear();
            Categories.Clear();
            Values.Clear();
            Usages.Clear();
            Tags.Clear();
            reader.Read();
            while (!reader.EOF)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case NODENAME:
                            var className = reader.GetAttribute(ATTRNAME);
                            if (!string.IsNullOrEmpty(className))
                            { 
                                this._sectName = className;
                            }
                            reader.Read();
                            break;
                        case CEFlags.NODENAME:
                            Flags.ReadXml(reader);
                            reader.Read();
                            break;
                        case CATEGORYNODE:
                            var category = reader.GetAttribute(ATTRNAME);
                            if (!string.IsNullOrEmpty(category)) 
                            {
                                Categories.Add(category);
                            }
                            reader.Read();
                            break;
                        case VALUENODE:
                            var val = reader.GetAttribute(ATTRNAME);
                            if (!string.IsNullOrEmpty(val))
                            {
                                Values.Add(val);
                            }
                            reader.Read();
                            break;
                        case USAGESNODE:
                            var usage = reader.GetAttribute(ATTRNAME);
                            if (!string.IsNullOrEmpty(usage))
                            {
                                Usages.Add(usage);
                            }
                            reader.Read();
                            break;
                        case TAGNODE:
                            var tag = reader.GetAttribute(ATTRNAME);
                            if (!string.IsNullOrEmpty(tag))
                            {
                                Tags.Add(tag);
                            }
                            reader.Read();
                            break;
                        default:
                            AddParametr(reader.Name, reader.ReadElementContentAsString());
                            break;
                    }
                }
                else
                    reader.Read();
            }
        }

        #region Поля для обратной совместимости
        //TODO: добавить поля для обратной совместимости
        #endregion
    }
}
