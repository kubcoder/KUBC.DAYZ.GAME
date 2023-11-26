using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Информация о получении игроком дамажа
    /// </summary>
    public class PlayerDamage:AdmEntity
    {
        
        /// <summary>
        /// Игрок который выхватил дамажу
        /// </summary>
        public PlayerInfo Player { get; set; } = new();
        /// <summary>
        /// Место где игрок выхватил пиздюлю
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Здоровье игрока на момент получения урона
        /// </summary>
        public float HP { get; set; } = 0;
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
        /// Куда игроку прилетел урон
        /// </summary>
        public string Into { get; set; } = string.Empty;
        /// <summary>
        /// Сколько дамажа прилетело
        /// </summary>
        public float Damage { get; set; } = 0;
        /// <summary>
        /// Каким боеприпасом нахлабучили игрока
        /// </summary>
        public string Ammo { get; set; } = string.Empty;
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
        public static PlayerDamage? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(PlayerDamage)) as PlayerDamage;
        }

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("hit by"))
            {
                base.Init(Line, cancellation);
                var style = System.Globalization.NumberStyles.Number;
                var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
                var p = ReadPlayer(' ', cancellation);
                
                if (p != null)
                {
                    Player = p;
                    var pos = ReadPosition(')', cancellation);
                    if (pos!=null)
                    {
                        Position = pos;
                        if (!SkipToChar(':', cancellation))
                            return false;
                        var hp = ReadToChar(']', true, cancellation);
                        if (float.TryParse(hp, style, culture, out var fHP))
                        {
                            HP = fHP;
                            var w = ReadToChar(' ', true, cancellation);
                            w = ReadToChar(' ', true, cancellation);
                            w = ReadToChar(' ', true, cancellation);
                            if (w!=null)
                            {
                                if (w == "Player")
                                {
                                    var sp = ReadPlayer(' ', cancellation);
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
                                switch(w)
                                {
                                    case "into":
                                        w = ReadToChar(' ', true, cancellation);
                                        if (w != null)
                                            Into = w;
                                        break;
                                    case "with":
                                        w = ReadToChar(' ', true, cancellation);
                                        if (w != null)
                                            Weapon = w;
                                        break;
                                }
                                w = ReadToChar(' ', true, cancellation);
                                if (w!= null)
                                {
                                    if (w=="for")
                                    {
                                        w = ReadToChar(' ', true, cancellation);
                                        if (float.TryParse(w, style, culture, out var f))
                                        {
                                            Damage = f;
                                        }
                                    }
                                    w = ReadToChar(' ', true, cancellation); 
                                    if (w!=null)
                                    {
                                        if (w=="damage")
                                        {
                                            w = ReadToChar(' ', true, cancellation);
                                            if (w!=null)
                                            {
                                                Ammo = w;
                                            }
                                        }
                                        w = ReadToChar(' ', true, cancellation);
                                        if (w!=null)
                                        {
                                            if (w== "with")
                                            {
                                                bool eWeapon = false;
                                                w = ReadToChar(' ', true, cancellation);
                                                if (w!=null)
                                                {
                                                    Weapon = w;
                                                    while (!eWeapon)
                                                    {
                                                        w = ReadToChar(' ', true, cancellation);
                                                        if (!string.IsNullOrEmpty(w))
                                                        {
                                                            if (w!="from")
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
                                                    if ((w!=null)&&(w=="from"))
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
                                    }
                                }
                                return true;
                            }
                            
                        }
                        
                    }
                }
            }
            return false;
        }

    }
}
