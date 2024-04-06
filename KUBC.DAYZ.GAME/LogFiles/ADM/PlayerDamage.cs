using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Информация о получении игроком дамажа
    /// </summary>
    public class PlayerDamage:PositionLogEntity
    {
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
    }

    /// <summary>
    /// Парсер события <see cref="PlayerDamage"/>
    /// </summary>
    public class PlayerDamageParser : ADMPositionParser
    {
        private const string START = "hit by";

        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (logLine.Contains(START))
            {
                if (Init(logLine, cancellation))
                {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                    var res = new PlayerDamage()
                    {
                        Player = Player,
                        Position = Position,
                        Time = logTime.GetValueOrDefault()
                    };
#pragma warning restore CS8601
                    if (!SkipToChar(':', cancellation))
                        return null;
                    var hp = ReadFloat(']', true, cancellation);
                    if (hp.HasValue)
                    {
                        res.HP = hp.Value;
                        ReadToChar(' ', true, cancellation);
                        ReadToChar(' ', true, cancellation);
                        var w = ReadToChar(' ', true, cancellation);
                        if (w!=null)
                        {
                            if (w == "Player")
                            {
                                var sp = ReadPlayer(cancellation);
                                if (sp != null)
                                {
                                    res.Source = sp;
                                }
                                var spos = ReadPosition(')', cancellation);
                                if (spos != null)
                                {
                                    res.SPosition = spos;
                                }
                            }
                            else
                            {
                                res.Source.NickName = w;
                            }
                            w = ReadToChar(' ', true, cancellation);
                            switch (w)
                            {
                                case "into":
                                    w = ReadToChar(' ', true, cancellation);
                                    if (w != null)
                                        res.Into = w;
                                    break;
                                case "with":
                                    w = ReadToChar(' ', true, cancellation);
                                    if (w != null)
                                        res.Weapon = w;
                                    break;
                            }
                            w = ReadToChar(' ', true, cancellation);
                            if (w != null)
                            {
                                if (w == "for")
                                {
                                    w = ReadToChar(' ', true, cancellation);
                                    if (float.TryParse(w, style, culture, out var f))
                                    {
                                        res.Damage = f;
                                    }
                                }
                                w = ReadToChar(' ', true, cancellation);
                                if (w != null)
                                {
                                    if (w == "damage")
                                    {
                                        w = ReadToChar(' ', true, cancellation);
                                        if (w != null)
                                        {
                                            res.Ammo = w;
                                        }
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
                                                res.Weapon = w;
                                                while (!eWeapon)
                                                {
                                                    w = ReadToChar(' ', true, cancellation);
                                                    if (!string.IsNullOrEmpty(w))
                                                    {
                                                        if (w != "from")
                                                        {
                                                            res.Weapon += " ";
                                                            res.Weapon += w;
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
                                                    var d = ReadFloat(' ', true, cancellation);
                                                    if (d.HasValue)
                                                    {
                                                        res.Distance = d.Value;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                            return res;
                        }
                    }
                }
            }
            return null;
        }
    }
}
