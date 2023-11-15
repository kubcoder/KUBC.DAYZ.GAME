namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Строчка про игрока в журнале
    /// </summary>
    public class PlayerPosition
    {
        /// <summary>
        /// Ник игрока
        /// </summary>
        public string NickName { get; set; } = String.Empty;
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public string DAYZID { get; set; } = String.Empty;
        /// <summary>
        /// Положение игрока в мире
        /// </summary>
        public Vector Position { get; set; } = [];
        /// <summary>
        /// Признак что игрок мертвый
        /// </summary>
        public bool IsDead { get; set; } = false;
        /// <summary>
        /// Получить данные положения игрока из строчки лога
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <returns>Данные игрока, или NULL если не удалось прочитать</returns>
        public static PlayerPosition? FromLog(string Line)
        {
            if (Line.Length > 7)
            {
                var lData = Line[7..];
                if (lData[0] == '"')
                {
                    var Reader = new StringReader(lData);
                    Reader.Read();
                    var rSym = Reader.Read();
                    var res = new PlayerPosition();
                    while ((rSym > 0) && (rSym != '"'))
                    {
                        res.NickName += (char)rSym;
                        rSym = Reader.Read();
                    }
                    var tText = string.Empty;
                    while ((rSym > 0) && (rSym != '='))
                    { 
                        rSym = Reader.Read();
                        tText+= (char)rSym;
                    }
                    if (tText.Contains("DEAD"))
                    {
                        res.IsDead = true;
                    }
                    while ((rSym > 0) && (rSym != ' '))
                    {
                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != ' '))
                        {
                            res.DAYZID += (char)rSym;
                        }

                    }
                    while ((rSym > 0) && (rSym != '=')) { rSym = Reader.Read(); }
                    var sPos = string.Empty;

                    while ((rSym > 0) && (rSym != ')'))
                    {

                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != ')'))
                            sPos += (char)rSym;

                    }
                    Reader.Close();
                    res.Position = Vector.FromLogString(sPos);
                    return res;
                }
            }
            return null;
        }
    }
}
