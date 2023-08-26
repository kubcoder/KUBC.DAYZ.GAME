using System.Text.Json;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEffectArea
{
    /// <summary>
    /// Файл настройки геймплея
    /// </summary>
    public class File
    {
        /// <summary>
        /// Список зон заражения
        /// </summary>
        public List<Area> Areas { get; set; } = new();
        /// <summary>
        /// Список безопасных точек
        /// </summary>
        public List<Vector2D> SafePositions { get; set; } = new();

        /// <summary>
        /// Имя файла настроек в папке миссии
        /// </summary>
        public const string FileName = "cfgeffectarea.json";
        /// <summary>
        /// Получить имя файла конфигурации геймплея
        /// </summary>
        /// <param name="MissionPath">Расположение папки миссии</param>
        /// <returns></returns>
        public static FileInfo GetFile(DirectoryInfo MissionPath)
        {
            return new FileInfo($"{MissionPath.FullName}\\{FileName}");
        }

        /// <summary>
        /// Загрузить файл из папки миссии
        /// </summary>
        /// <param name="MissionPath">Путь к папке миссии</param>
        /// <returns></returns>
        public static File? Load(DirectoryInfo MissionPath)
        {
            return Create(GetFile(MissionPath));
        }

        /// <summary>
        /// Создать объект из строки с разметкой json
        /// </summary>
        /// <param name="json">Строка с разметкой JSON</param>
        public static File? Create(string json)
        {
            return JsonSerializer.Deserialize<File>(json);
        }

        /// <summary>
        /// Создать объект из строки с разметкой json
        /// </summary>
        /// <param name="File">Файл из которого нужно прочитать данные</param>
        public static File? Create(FileInfo File)
        {
            if (!File.Exists)
                return null;
            var reader = File.OpenText();
            var json = reader.ReadToEnd();
            reader.Close();
            return Create(json);
        }
        /// <summary>
        /// Создать объект из строки с разметкой json
        /// </summary>
        /// <param name="Path">Папка миссии</param>
        public static File? Create(DirectoryInfo Path)
        {
            return Create(GetFile(Path));
        }


        /// <summary>
        /// Выполнить запись в строку json
        /// </summary>
        /// <param name="Formated">Вставлять символы форматирования, если передать истину то 
        /// файл получится красивым с отступами и прочим, очень удобен для чтения людями
        /// но занимает больше места в байтах</param>
        /// <returns>Строка с разметкой JSON</returns>
        public string ToJson(bool Formated = false)
        {
            var o = new JsonSerializerOptions() { WriteIndented = Formated };
            return JsonSerializer.Serialize(this, o);
        }

        /// <summary>
        /// Сохранить JSON файл
        /// </summary>
        /// <param name="File">Описание файла в который нужно писать данные</param>
        /// <param name="Formated">Форматировать файлик, подробно <see cref="ToJson(bool)"/></param>
        public void Save(FileInfo File, bool Formated = false)
        {
            var json = ToJson(Formated);
            var stream = File.CreateText();
            stream.Write(json);
            stream.Flush();
            stream.Close();
        }
        /// <summary>
        /// Сохранить JSON файл
        /// </summary>
        /// <param name="Path">Папка миссии </param>
        /// <param name="Formated">Форматировать файлик, подробно <see cref="ToJson(bool)"/></param>
        public void Save(DirectoryInfo Path, bool Formated = false)
        {
            Save(GetFile(Path), Formated);
        }
    }
}
