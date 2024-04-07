using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Информация для администраторов от игрока
    /// </summary>
    public class Report : OneLineEntity
    {
        /// <summary>
        /// Идентификатор игрока в DAYZ
        /// </summary>
        public string DayzID { get; set; } = string.Empty;
        /// <summary>
        /// Текст жалобы
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
    /// <summary>
    /// Парсер события <see cref="Report"/>
    /// </summary>
    public class ReportParser: ADMStringParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "PLAYER REPORT:";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
                var id = ReadToChar('>', true, cancellation);
                if (!string.IsNullOrEmpty(id))
                {
                    var res = new Report()
                    {
                        Time = logTime.GetValueOrDefault(),
                        DayzID = id
                    };
                    if (!SkipToChar(':', cancellation))
                    {
                        return null;
                    }
                    if (Reader != null)
                    {
                        res.Text = Reader.ReadToEnd().Trim();
                        Dispose();
                        if (!string.IsNullOrEmpty(res.Text))
                        {
                            return res;
                        }

                    }
                }
            }
            return null;
        }
    }
}
