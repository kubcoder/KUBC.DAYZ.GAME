using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Упаковка объекта для переноски
    /// </summary>
    public class Packed : AdmEntity
    {
        /// <summary>
        /// Игрок который упаковал
        /// </summary>
        public PlayerInfo Player { get; set; } = new();

        /// <summary>
        /// Место где он это делал
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Что имено упаковал игрок
        /// </summary>
        public string ItemName { get; set; } = string.Empty;
        /// <summary>
        /// Инструмент которым пользовался игрок
        /// </summary>
        /// <remarks>
        /// На данный момент в коде игры жестко задано что
        /// это всегда hands, но раз появилось значит возможно это расширят...
        /// но не факт.
        /// </remarks>
        public string Tool { get; set; } = string.Empty;

        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Packed? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Packed)) as Packed;
        }
        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("packed"))
            {
                base.Init(Line, cancellation);
                var p = ReadPlayer(' ', cancellation);
                if (p != null)
                {
                    Player = p;
                    var pos = ReadPosition(')', cancellation);
                    if (pos != null)
                    {
                        Position = pos;
                        var w = ReadToChar(' ', true, cancellation);
                        w = ReadToChar(' ', true, cancellation);
                        if (!string.IsNullOrEmpty(w))
                        {
                            ItemName = w.Trim();
                        }
                        while (!string.IsNullOrEmpty(w))
                        {
                            w = ReadToChar(' ', true, cancellation);
                            if (w == "with")
                            {
                                w = null;
                            }
                            else
                            {
                                ItemName += " ";
                                ItemName += w;
                            }
                        }
                        w = ReadToChar(' ', true, cancellation);
                        if (!string.IsNullOrEmpty(w))
                        {
                            Tool = w.Trim();
                        }
                        while (!string.IsNullOrEmpty(w))
                        {
                            w = ReadToChar(' ', true, cancellation);
                            Tool += " ";
                            Tool += w; 
                        }
                        Tool = Tool.TrimEnd();
                        return true;
                    }
                }

            }
            return false;
        }
    }
}
