using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Настройки индикации повреждений
    /// </summary>
    public class HitIndicationDataEntity
    {
        /// <summary>
        /// Decides whether the values get used or not. Since anything undefined in the 'HitIndicationData' class (or any class in json file) is considered zero, allows us to determine that some valid data had been loaded.
        /// Решает, будут ли значения использоваться или нет. Поскольку все, что не определено в классе «HitIndicationData» (или любом классе в файле json), считается нулевым, это позволяет нам определить, что были загружены некоторые допустимые данные.
        /// </summary>
        [JsonPropertyName("hitDirectionOverrideEnabled")]
        public bool HitDirectionOverrideEnabled { get; set; } = false;

        /// <summary>
        /// Dictates general behaviour of the hit indicator. 0 == Disabled, 1 == Static, 2 == Dynamic (moving when displayed, WIP)
        /// Определяет общее поведение индикатора попаданий. 0 == Отключено, 1 == Статично, 2 == Динамично (перемещение при отображении, WIP)
        /// </summary>
        [JsonPropertyName("hitDirectionBehaviour")]
        public int HitDirectionBehaviour { get; set; } = 1;

        /// <summary>
        /// Dictates which type of indicator gets used. Set of images and position calculations. 0 == 'splash', 1 == 'spike', 2 == 'arrow'
        /// Определяет, какой тип индикатора используется. Набор изображений и вычислений положения. 0 == «всплеск», 1 == «шип», 2 == «стрелка»
        /// </summary>
        [JsonPropertyName("hitDirectionStyle")]
        public int HitDirectionStyle { get; set; } = 1;

        /// <summary>
        /// Цвет индикатора
        /// </summary>
        [JsonIgnore]
        public ARGB HitDirectionIndicatorColor = new() { A = 0xff, R = 0xbb, G = 0x0a, B = 0x1e };

        /// <summary>
        /// Строкове представление цвета индикатора
        /// </summary>
        [JsonPropertyName("hitDirectionIndicatorColorStr")]
        public string HitDirectionIndicatorColorStr
        {
            get
            {
                return HitDirectionIndicatorColor.ValueAsString;
            }
            set
            {
                HitDirectionIndicatorColor.ValueAsString = value;
            }
        }

        /// <summary>
        /// Maximal duration of the hit indicator. Actual duration is between 0.6..1.0 of the defined value, depending on the severity of the hit (which generally means heavier hits == longer indication)
        /// Индикатор максимальной продолжительности попадания. Фактическая продолжительность составляет от 0,6 до 1,0 от заданного значения, в зависимости от серьезности удара (что обычно означает более сильные удары == более длинная индикация).
        /// </summary>
        [JsonPropertyName("hitDirectionMaxDuration")]
        public float HitDirectionMaxDuration { get; set; } = 2.0f;

        /// <summary>
        /// Fraction of the actual duration, after which the indicator begins to recede (currently fade-out only), 0.0 = fades from the beginning, 0.5 == fades after 50% duration has elapsed, 1.0 == no fading
        /// Доля фактической продолжительности, после которой индикатор начинает уменьшаться (в настоящее время только затухание), 0,0 = затухает с самого начала, 0,5 == затухает по истечении 50% продолжительности, 1,0 == затухание отсутствует
        /// </summary>
        [JsonPropertyName("hitDirectionBreakPointRelative")]
        public float HitDirectionBreakPointRelative { get; set; } = 0.2f;
        /// <summary>
        /// Amount of scatter to induce inaccuracy to the indication. Actual scatter is randomized by the amount of degrees in both directions (+- value, so value od 10 gives a potential scatter of 20 DEG)
        /// Величина разброса, вызывающая неточность индикации. Фактический разброс случайным образом определяется количеством градусов в обоих направлениях (значение +-, поэтому значение od 10 дает потенциальный разброс в 20 градусов).
        /// </summary>
        [JsonPropertyName("hitDirectionScatter")]
        public float HitDirectionScatter { get; set; } = 10;

        /// <summary>
        /// Allows for disabling of the old hit effect (red flash)
        /// Позволяет отключить старый эффект попадания (красная вспышка)
        /// </summary>
        [JsonPropertyName("hitIndicationPostProcessEnabled")]
        public bool HitIndicationPostProcessEnabled { get; set; } = true;
    }
}
