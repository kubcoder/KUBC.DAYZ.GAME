using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие убйства игрока
    /// </summary>
    public class PlayerKilled : PositionLogEntity
    {
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
    }

    /// <summary>
    /// Парсер события <see cref="PlayerKilled"/>
    /// </summary>
    public class PlayerKilledParser : ADMPositionParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "killed by";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                var res = new PlayerKilled()
                {
                    Player = Player,
                    Position = Position,
                    Time = logTime.GetValueOrDefault()
                };
#pragma warning restore CS8601
                ReadToChar(' ', true, cancellation);
                ReadToChar(' ', true, cancellation);
                var w = ReadToChar(' ', true, cancellation);
                if (w != null)
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
                        if (w == "with")
                        {
                            w = ReadToChar(' ', true, cancellation);
                            while (!string.IsNullOrEmpty(w))
                            {
                                res.Source.NickName = $"{res.Source.NickName} {w.Trim()}";
                                w = ReadToChar(' ', true, cancellation);
                            }
                            return res;
                        }
                        else
                        {
                            res.Source.NickName = w;
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
                    return res;
                }
            }
            return null;
        }
    }
}
