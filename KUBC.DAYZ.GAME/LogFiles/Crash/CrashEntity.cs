using KUBC.DAYZ.GAME.LogFiles.ADM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.Crash
{
    /// <summary>
    /// Представление ошибки сервера
    /// </summary>
    public class CrashEntity: LogEntity
    {

        /// <summary>
        /// Заголовок сервера
        /// </summary>
        public string ServerName { get; set; } = string.Empty;
        /// <summary>
        /// Строки сообщения об ошибке
        /// </summary>
        public List<string> Messages { get; set; } = new List<string>();
        /// <summary>
        /// Стак вызова
        /// </summary>
        public List<string> StackTrace { get; set; } = new List<string>();
        /// <summary>
        /// Параметры запуска
        /// </summary>
        public string CLIParams { get; set; } = string.Empty;

        /// <summary>
        /// В какой фазе находится процесс чтения
        /// </summary>
        private ParsePhase Phase = ParsePhase.Header;

        /// <summary>
        /// Проверить есть ли краш проблема на запуске сервера
        /// </summary>
        /// <returns>Истина если сервер не смог запустится</returns>
        public bool ChekNotCompile()
        {
            foreach (var msg in Messages)
                if (msg.Contains("Can't compile"))
                    return true;
            return false;
        }
        /// <inheritdoc/>
        public override bool IsEndRead()
        {
            return Phase == ParsePhase.EndRead;
        }

        /// <inheritdoc/>
        public override bool AppendLine(string Line)
        {
            switch (Phase)
            {
                case ParsePhase.Header:
                    var tokens = Line.Split(',');
                    if (tokens.Length > 1)
                    {
                        ServerName = tokens[0];
                        if (DateTime.TryParse(tokens[1], out var crashTime))
                        {
                            EventTime = crashTime;
                        }
                        Phase++;
                    }
                    else
                    {
                        Phase = ParsePhase.EndRead;
                    }
                    break;
                case ParsePhase.Message:
                    if (Line.Contains("Stack trace"))
                    {
                        Phase++;
                    }
                    else
                    {
                        if (Line.Contains("Runtime mode"))
                        {
                            Phase = ParsePhase.CLIParams;
                        }
                        else
                        {
                            var mLine = Line.Trim();
                            if (!string.IsNullOrEmpty(mLine))
                                Messages.Add(mLine);
                        }

                    }
                    break;
                case ParsePhase.StackTrace:
                    if (Line.Contains("Runtime mode"))
                    {
                        Phase++;
                    }
                    else
                    {
                        var sLine = Line.Trim();
                        if (!string.IsNullOrEmpty(sLine))
                            StackTrace.Add(sLine);
                    }
                    break;
                case ParsePhase.CLIParams:
                    CLIParams = Line.Trim();
                    Phase = ParsePhase.EndRead;
                    return true;
            }
            return Phase == ParsePhase.EndRead;
        }


    }
}
