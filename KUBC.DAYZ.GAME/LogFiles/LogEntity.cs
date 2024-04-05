using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Элемент лога
    /// </summary>
    public abstract class LogEntity:ILogEntity
    {
        /// <summary>
        /// Время события
        /// </summary>
        /// <remarks>
        /// Фактический экземпляр времени данных.
        /// При создании класса инициализируется как текущее время и дата
        /// </remarks>
        protected DateTime EventTime = DateTime.Now;

        /// <inheritdoc/>
        [XmlAttribute]
        public DateTime Time
        {
            get
            {
                return EventTime;
            }
            set 
            {
                EventTime = value;
            }
        }
        /// <inheritdoc/>
        public abstract bool AppendLine(string Line);
        /// <inheritdoc/>
        public string GetXML()
        {
            var sb = new StringWriter();
            System.Xml.XmlWriter wrt = System.Xml.XmlWriter.Create(sb, new System.Xml.XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true
            });
            var x = new XmlSerializer(this.GetType());
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            x.Serialize(wrt, this, xns);
            wrt.Close();
            return sb.ToString();
        }

        /// <inheritdoc/>
        public abstract bool IsEndRead();


    }
}
