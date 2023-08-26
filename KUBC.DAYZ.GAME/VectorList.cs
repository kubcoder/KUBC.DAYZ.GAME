using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME
{
    /// <summary>
    /// Массив объектов <see cref="Vector"/>
    /// </summary>
    public class VectorList : List<Vector>
    {
        /// <summary>
        /// Преобразовать в JSON
        /// </summary>
        /// <returns>Массив байт с разметкой JSON</returns>
        public byte[] ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.SerializeToUtf8Bytes(this, options);
        }

        /// <summary>
        /// Возвращаем список точек без пустых значений
        /// </summary>
        /// <returns></returns>
        public VectorList GetValues()
        {
            var res = new VectorList();
            foreach (var v in this)
            {
                if (!v.IsEmpty)
                    res.Add(v);
            }
            return res;
        }
        /// <summary>
        /// Строка с разметкой JSON описывающая данный массив координат
        /// </summary>
        [JsonIgnore]
        public string AsJSON
        {
            get
            {
                return Encoding.UTF8.GetString(ToJson());
            }
        }
        /// <summary>
        /// Создать массив координат из JSON
        /// </summary>
        /// <param name="jsonString">Строка с разметкой JSON</param>
        /// <returns>Массив координат, возможен NULL если прочитать данные не удалось</returns>
        public static VectorList? FromJson(string jsonString)
        {
            try
            {
                return JsonSerializer.Deserialize<VectorList>(jsonString);
            }
            catch
            {
                return null;
            }

        }
    }
}
