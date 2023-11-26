using KUBC.DAYZ.GAME.MissionFiles.CfgPlayerSpawnPoints;
using System;
using System.Numerics;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Информация для администраторов от игрока
    /// </summary>
    public class Report:AdmEntity
    {
        

        /// <summary>
        /// Идентификатор игрока в DAYZ
        /// </summary>
        public string DayzID { get; set; } = string.Empty;
        /// <summary>
        /// Текст жалобы
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("PLAYER REPORT:"))
            {
                base.Init(Line, cancellation);
                if (!SkipToChar('<', cancellation))
                {
                    return false;
                }
                var DateString = ReadToChar('_', true, cancellation);
                var TimeString = ReadToChar('>', true, cancellation);
                if (TimeString!=null)
                {
                    var dtStr = $"{DateString} {TimeString.Replace('-', ':')}";
                    if (DateTime.TryParse(dtStr, out var t))
                    {
                        Time = t;
                    }
                    if (!SkipToChar('<', cancellation))
                    {
                        return false;
                    }
                    var id = ReadToChar('>', true, cancellation);
                    if (!string.IsNullOrEmpty(id))
                    {
                        DayzID = id;
                        if (!SkipToChar(':', cancellation))
                        {
                            return false;
                        }
                        if (Reader!=null)
                        {
                            Text = Reader.ReadToEnd().Trim();
                            Dispose();
                            if (!string.IsNullOrEmpty(Text))
                            {
                                return true;
                            }
                            
                        }
                    }
                }
                

            }
            return false;
        }


        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Report? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Report)) as Report;
        }
    }
}
