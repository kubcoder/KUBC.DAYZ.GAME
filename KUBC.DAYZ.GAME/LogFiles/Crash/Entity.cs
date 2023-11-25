using KUBC.DAYZ.GAME.LogFiles.RPT;
using KUBC.DAYZ.GAME.MissionFiles.CfgEconomyCore;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.Crash
{
    /// <summary>
    /// Описание краша. Т.е. что за краш, где случился. Т.е. вся информация из лога ошибок
    /// </summary>
    public class Entity:LogEntity
    {

        /// <summary>
        /// Заголовок сервера
        /// </summary>
        public string HeaderLine { get; set; } = string.Empty;
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
        /// Проверяем что краш критический, и мешает запуску игры
        /// </summary>
        /// <returns>Истина если был краш невозможности запустить игру</returns>
        public bool ChekNotCompile()
        {
            foreach (var msg in Messages)
                if (msg.Contains("Can't compile"))
                    return true;
            return false;
        }
        /// <summary>
        /// Получить время краша
        /// </summary>
        /// <returns></returns>
        public DateTime? GetCrashTime()
        {
            if (!string.IsNullOrEmpty(HeaderLine))
            {
                var tokens = HeaderLine.Split(',');
                if (tokens.Length > 1)
                {
                    if (DateTime.TryParse(tokens[1], out var crashTime))
                        return crashTime;
                }
            }
            return null;
        }
        /// <summary>
        /// В какой фазе находится процесс чтения
        /// </summary>
        private ParsePhase Phase = ParsePhase.NotInit;
        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            base.Init(Line, cancellation);
            if (Line == "------------------------------------\r\n")
            {
                this.Phase = ParsePhase.Header;
                Dispose();
                return true;
            }
            Dispose();
            return false;
        }
        /// <inheritdoc/>
        public override bool IsReadSucces()
        {
            return Phase == ParsePhase.EndRead;
        }

        /// <inheritdoc/>
        public override bool AppendLine(string Line, CancellationToken? cancellation = null)
        {
            switch(Phase) 
            {
                case ParsePhase.Header:
                    HeaderLine = Line;
                    Phase++;
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


        /// <summary>
        /// Создать элемент данных краша из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Entity? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Entity)) as Entity;
        }

    }
}
