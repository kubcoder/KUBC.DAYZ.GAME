using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Messages
{
    /// <summary>
    /// Элемент сообщения. Описывает одно сообщение
    /// </summary>
    [Serializable]
    [XmlType("message")]
    public class MessageEntity
    {
        /// <summary>
        /// Задержка перед отправка сообщения
        /// </summary>
        [XmlElement("delay", IsNullable = true)]
        public int? Delay { get; set; }
        /// <summary>
        /// Костыль отключающий запись нулевых значений
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeDelay() { return Delay.HasValue; }
        /// <summary>
        /// Повторение сообщения
        /// </summary>
        [XmlElement("repeat")]
        public int? Repeat { get; set; }
        /// <summary>
        /// Отключаем запись нулевых значений для Reapeat
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeRepeat() { return Repeat.HasValue; }

        /// <summary>
        /// Счетчик остановки сервера
        /// </summary>
        [XmlElement("deadline")]
        public int? DeadLine { get; set; }
        /// <summary>
        /// Отключаем запись нулевых значений для DeadLine
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeDeadLine() { return DeadLine.HasValue; }

        /// <summary>
        /// Сообщение необходимо отображать при подключении
        /// </summary>
        [XmlElement("onConnect")]
        public int? OnConnect { get; set; }
        /// <summary>
        /// Отключаем запись нулевых значений для OnConnect
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeOnConnect() { return OnConnect.HasValue; }
        /// <summary>
        /// Признак что по завершению счета <see cref="DeadLine"/> необходимо остановить сервер
        /// </summary>
        [XmlElement("shutdown")]
        public int? ShutDown { get; set; }
        /// <summary>
        /// Отключаем запись нулевых значений для ShutDown
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeShutDown() { return ShutDown.HasValue; }
        /// <summary>
        /// Текст сообщения. Т.е. что именно отображается игроку
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <listheader>В тексте допускаются следующие служебные переменные</listheader>
        /// <item>
        /// <term>#name</term>
        /// <description>Будет заменено на имя сервера</description>
        /// </item>
        /// <item>
        /// <term>#port</term>
        /// <description>Будет заменено на порт сервера</description>
        /// </item>
        /// <item>
        /// <term>#tmin</term>
        /// <description>Будет заменено на время до рестарта. Работает если установлен флаг <see cref="ShutDown"/></description>
        /// </item>
        /// </list>
        /// </remarks>
        [XmlElement("text")]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Получить событие о рестарте сервера
        /// </summary>
        /// <param name="Minutes">Через сколько минут выполнить рестарт</param>
        /// <param name="Message">Текст сообщения о рестарте</param>
        /// <returns></returns>
        public static MessageEntity GetRestart(string Message, int Minutes)
        {
            return new MessageEntity()
            {
                DeadLine = Minutes,
                ShutDown = 1,
                Text = Message
            };
        }

        /// <summary>
        /// Получить сообщение которое повторяется для всех игроков
        /// </summary>
        /// <param name="Minutes">Частота повторений</param>
        /// <param name="Message">Текст сообщения</param>
        /// <returns></returns>
        public static MessageEntity GetRepeat(string Message, int Minutes)
        {
            return new MessageEntity()
            {
                Repeat = Minutes,
                Text = Message
            };
        }

        /// <summary>
        /// Получить сообщение которое повторяется для игроков после подключения
        /// </summary>
        /// <param name="Delay">Сколько минут ждать после подключения перед отображением мессаги</param>
        /// <param name="Message">Текст сообщения</param>
        /// <param name="Repeat">Через сколько минут повторять, если 0 то повторения не будет</param>
        /// <returns></returns>
        public static MessageEntity GetOnConnect(string Message, int Delay, int Repeat = 0)
        {
            var msg = new MessageEntity()
            {
                Delay = Delay,
                Text = Message,
                OnConnect = 1
            };
            if (Repeat > 0)
            {
                msg.Repeat = Repeat;
            }
            return msg;
        }
    }
}
