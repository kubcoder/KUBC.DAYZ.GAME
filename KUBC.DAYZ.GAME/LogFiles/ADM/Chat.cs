using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Игрок написал в чат
    /// </summary>
    public class Chat : PlayerLogEntity
    {
        /// <summary>
        /// Текст чата
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
    /// <summary>
    /// Парсер игрового чата
    /// </summary>
    public class ChatParser : ADMPlayerParser
    {
        private const string START = "Chat";

        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (logLine.Contains(START))
            {
                if (Init(logLine, cancellation))
                {
                    if (!SkipToChar(':', cancellation))
                        return null;

                    if (Reader != null)
                    {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                        return new Chat()
                        {
                            Player = Player,
                            Text = Reader.ReadToEnd().Trim(),
                            Time = logTime.GetValueOrDefault()
                        };
#pragma warning restore CS8601 
                    }
                }
            }
            return null;
        }
    }
}
