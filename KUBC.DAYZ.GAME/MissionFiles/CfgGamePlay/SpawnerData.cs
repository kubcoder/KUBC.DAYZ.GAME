﻿using System.Text.Json;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Файл настройки спавна на старте сервера
    /// </summary>
    public class SpawnerData
    {
        /// <summary>
        /// Список объектов для спавна
        /// </summary>
        public List<SpawnerDataObject> Objects { get; set; } = new();

        /// <summary>
        /// Создание пустого файла настроек спавна
        /// </summary>
        public SpawnerData() { }

        /// <summary>
        /// Создать объект из строки с разметкой json
        /// </summary>
        /// <param name="json">Строка с разметкой JSON</param>
        public static SpawnerData? Create(string json)
        {
            return JsonSerializer.Deserialize<SpawnerData>(json);
        }
        /// <summary>
        /// Создать объект из строки с разметкой json
        /// </summary>
        /// <param name="File">Файл из которого нужно прочитать данные</param>
        public static SpawnerData? Create(FileInfo File)
        {
            if (!File.Exists)
                return null;
            var reader = File.OpenText();
            var json = reader.ReadToEnd();
            reader.Close();
            return JsonSerializer.Deserialize<SpawnerData>(json);
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
            return JsonSerializer.Serialize<SpawnerData>(this, o);
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
    }
}
