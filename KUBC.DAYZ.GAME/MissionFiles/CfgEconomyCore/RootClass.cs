using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEconomyCore
{
    /// <summary>
    /// Элемент описания корневого класса
    /// </summary>
    [XmlType("rootclass")]
    public class RootClass
    {
        /// <summary>
        /// Имя корневого класса
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Тут какое то действие
        /// </summary>
        [XmlAttribute("act")]
        public string? Act { get; set; }

        /// <summary>
        /// Какая то настройка о каких то репортах
        /// </summary>
        [XmlAttribute("reportMemoryLOD")]
        public string? ReportMemoryLOD { get; set; }


    }
}
