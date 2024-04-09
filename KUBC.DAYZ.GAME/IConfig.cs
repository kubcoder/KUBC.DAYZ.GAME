using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME
{
    /// <summary>
    /// Секция конфигурации
    /// </summary>
    public interface IConfig : INotifyPropertyChanged, IEnumerable<KeyValuePair<string, object?>>
    {
        /// <summary>
        /// Имя секции
        /// </summary>
        public string SectionName {  get; }
        /// <summary>
        /// Получить значение параметра
        /// </summary>
        /// <param name="key">Ключ параметра</param>
        /// <returns></returns>
        public object? GetValue(string key);
        /// <summary>
        /// Установить значение параметра
        /// </summary>
        /// <param name="key">Ключ параметра</param>
        /// <param name="value">Значение параметра</param>
        public void SetValue(string key, object? value);

        /// <summary>
        /// Получить список имен параметров коллекции
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetKeys();
        
        
    }
}
