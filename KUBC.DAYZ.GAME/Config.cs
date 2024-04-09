using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <inheritdoc/>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;


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
        /// <param name="AttrName">Имя явно указанного аттрибута в коллекции</param>
        protected void SetValue(string key, bool? value, string? AttrName = null)
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
        public void SetValue(string key, object? value, string? AttrName = null)
        {
            if (Params.ContainsKey(key))
            {
                if (Params[key] != value)
                {
                    Params[key] = value;
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, Params[key]));
                    SendNotify(key, AttrName);
                }
            }
            else
            {
                Params.Add(key, value);
                if (!string.IsNullOrEmpty(AttrName))
                    SendNotify(key, AttrName);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
            }
        }
        /// <summary>
        /// Послать уведомление о изменении элемента
        /// </summary>
        /// <param name="key"></param>
        /// <param name="AttrName"></param>
        protected void SendNotify(string key, string? AttrName = null)
        {
            if (string.IsNullOrEmpty(AttrName))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));
            else
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(AttrName));
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
