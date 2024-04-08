using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME
{
    /// <summary>
    /// Секция конфиуграции
    /// </summary>
    public abstract class Config : IConfig
    {
        /// <summary>
        /// Список параметров конфигурации
        /// </summary>
        protected readonly Dictionary<string, object?> Params = [];
        /// <inheritdoc/>
        public string SectionName => _sectName;
        
        /// <summary>
        /// Имя секции
        /// </summary>
        protected string _sectName = string.Empty;

        /// <summary>
        /// Событие обновления данных
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
        {
            return Params.GetEnumerator();
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetKeys()
        {
            return Params.Keys;
        }

        /// <summary>
        /// Это костыль преобразования
        /// </summary>
        /// <remarks>
        /// Многие значения нужно использовать как bool в формах
        /// а серверу DAYZ подавай значения 0 и 1 как целые.
        /// </remarks>
        /// <param name="key">Имя параметра</param>
        /// <returns></returns>
        protected bool? GetValueAsBol(string key)
        {
            var val = GetValue(key) as int?;
            if (val.HasValue)
                return val.Value > 0;
            return null;
        }

        /// <summary>
        /// Это костыль преобразования
        /// </summary>
        /// <remarks>
        /// Многие значения нужно использовать как bool в формах
        /// а серверу DAYZ подавай значения 0 и 1 как целые.
        /// </remarks>
        /// <param name="key">Имя параметра</param>
        /// <param name="value">Значение параметра</param>
        protected void SetValue(string key, bool? value)
        {
            if (value.HasValue)
            {
                if (value.Value)
                {
                    SetValue(key, (int)1);
                }
                else
                {
                    SetValue(key, (int)0);
                }
            }
            else
            {
                SetValue(key, null);
            }
        }

        /// <inheritdoc/>
        public object? GetValue(string key)
        {
            if (Params.ContainsKey(key))
                return Params[key];
            return null;
        }


        /// <inheritdoc/>
        public void SetValue(string key, object? value)
        {
            if (Params.ContainsKey(key))
            {
                if (Params[key] != value)
                {
                    Params[key] = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));
                }
            }
            else
            {
                Params.Add(key, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));
            }
        }
        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Params.GetEnumerator();
        }
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            var o = obj as Config;
            if (o!=null)
            {
                if (Params.Count!= o.Params.Count)
                    return false;
                var keys = GetKeys();
                foreach (var key in keys)
                {
                    var actual = GetValue(key);
                    var expection = o.GetValue(key);
                    if (actual==null)
                    {
                        if (expection != null)
                            return false;
                    }
                    else
                    {
                        if (!actual.Equals(expection))
                            return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
