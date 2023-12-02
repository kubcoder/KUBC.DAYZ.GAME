using KUBC.DAYZ.GAME.MissionFiles.CfgSpawnableTypes;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие убйства игрока
    /// </summary>
    public class PlayerKilled : AdmEntity
    {
        /// <summary>
        /// Игрок который был убит
        /// </summary>
        public PlayerInfo Player { get; set; } = new();
        /// <summary>
        /// Место где игрок выхватил пиздюлю
        /// </summary>
        public Vector Position { get; set; } = new();

        /// <summary>
        /// Источник урона
        /// </summary>
        /// <remarks>
        /// Если заполнен ID значит источник другой игрок
        /// </remarks>
        public PlayerInfo Source { get; set; } = new();
        /// <summary>
        /// Где находился нападающий
        /// </summary>
        public Vector SPosition { get; set; } = new();

        /// <summary>
        /// Какое оружие использовал нападающий
        /// </summary>
        public string Weapon { get; set; } = string.Empty;
        /// <summary>
        /// С какой дистанции был нанесен урон
        /// </summary>
        public float Distance { get; set; } = 0;


        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static PlayerKilled? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(PlayerKilled)) as PlayerKilled;
        }

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("killed by"))
            {
                base.Init(Line, cancellation);
                var style = System.Globalization.NumberStyles.Number;
                var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
                var p = ReadPlayer(cancellation);
                if (p != null)
                {
                    Player = p;
                    var pos = ReadPosition(')', cancellation);
                    if (pos != null)
                    {
                        Position = pos;
                        var w = ReadToChar(' ', true, cancellation);
                        w = ReadToChar(' ', true, cancellation);
                        w = ReadToChar(' ', true, cancellation);
                        if (w != null)
                        {
                            if (w == "Player")
                            {
                                var sp = ReadPlayer(cancellation);
                                if (sp != null)
                                {
                                    Source = sp;
                                }
                                var spos = ReadPosition(')', cancellation);
                                if (spos != null)
                                {
                                    SPosition = spos;
                                }
                            }
                            else
                            {
                                Source.NickName = w;
                            }
                            w = ReadToChar(' ', true, cancellation);
                            if (w != null)
                            {
                                if (w == "with")
                                {
                                    bool eWeapon = false;
                                    w = ReadToChar(' ', true, cancellation);
                                    if (w != null)
                                    {
                                        Weapon = w;
                                        while (!eWeapon)
                                        {
                                            w = ReadToChar(' ', true, cancellation);
                                            if (!string.IsNullOrEmpty(w))
                                            {
                                                if (w != "from")
                                                {
                                                    Weapon += " ";
                                                    Weapon += w;
                                                }
                                                else
                                                {
                                                    eWeapon = true;
                                                }
                                            }
                                            else
                                            {
                                                eWeapon = true;
                                            }
                                        }
                                        if ((w != null) && (w == "from"))
                                        {
                                            var d = ReadToChar(' ', true, cancellation);
                                            if (float.TryParse(d, style, culture, out var dst))
                                            {
                                                Distance = dst;
                                            }
                                        }
                                    }

                                }
                            }
                            return true;
                        }

                    }
                }
            }
            return false;
        }


    }
}
