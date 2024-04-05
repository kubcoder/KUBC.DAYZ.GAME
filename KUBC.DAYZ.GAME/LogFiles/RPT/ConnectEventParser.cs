using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Парсер события подключения игрока
    /// </summary>
    public class ConnectEventParser : RPTStringParser
    {
        private const string START = "is connected";

        private const string SteamID = "steamID";

        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (logLine.Contains(START))
            {
                if (logLine.Contains(SteamID))
                {
                    if (Init(logLine, cancellation))
                    {
                        if (!SkipToChar('"', cancellation))
                        {
                            return null;
                        }
                        var rNickName = ReadToChar('"', false, cancellation);
                        if (string.IsNullOrEmpty(rNickName))
                        {
                            return null;
                        }
                        if (!SkipToChar('=', cancellation))
                        {
                            return null;
                        }
                        var steamID = ReadToChar(')', true, cancellation);
                        if (steamID == null)
                        {
                            return null;
                        }
                        if (long.TryParse(steamID, out var pSteamID))
                        {
                            Dispose();
                            if (logTime!=null)
                            {
                                return new ConnectEvent()
                                {
                                    Time = logTime.Value,
                                    NickName = rNickName,
                                    SteamID = pSteamID
                                };
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
