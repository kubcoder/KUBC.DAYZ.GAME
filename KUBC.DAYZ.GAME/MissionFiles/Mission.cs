using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MissionFiles
{
    /// <summary>
    /// Класс миссии сервера
    /// </summary>
    public class Mission
    {
        /// <summary>
        /// Имя папки с файлами настройки базы данных игры
        /// </summary>
        public const string PATH_DB = "db";
        /// <summary>
        /// Файл скрипта запуска миссии
        /// </summary>
        public const string FILE_INIT = "init.c";
        /// <summary>
        /// Имя папки с миссиями
        /// </summary>
        public const string PATH_MISSIONS = "mpmissions";

        /// <summary>
        /// Папка размещения миссии
        /// </summary>
        public DirectoryInfo MissionPath;
        /// <summary>
        /// Папка файлов настройки БД
        /// </summary>
        public DirectoryInfo DBPath;
        /// <summary>
        /// Файл стартового скрипта
        /// </summary>
        public FileInfo InitFile;
        /// <summary>
        /// Название миссии
        /// </summary>
        public string MissionName
        {
            get
            {
                var t = MissionPath.Name.Split('.');
                return t[0];
            }

        }
        /// <summary>
        /// Получить название карты
        /// </summary>
        public string MapName
        {
            get
            {
                var t = MissionPath.Name.Split('.');
                if (t.Length > 1)
                    return t[1];
                return string.Empty;
            }
        }
        /// <summary>
        /// Создать экземпляр миссии
        /// </summary>
        /// <param name="missionPath"></param>
        public Mission(DirectoryInfo missionPath)
        {
            MissionPath = missionPath;
            DBPath = new DirectoryInfo($"{missionPath.FullName}\\{PATH_DB}");
            InitFile = new FileInfo($"{missionPath.FullName}\\{FILE_INIT}");
            FindSotrtages();
        }
        /// <summary>
        /// Список игровых хранилищь
        /// </summary>
        public List<int> Sortages { get; set; } = new();

        /// <summary>
        /// Признак что это папка миссии
        /// </summary>
        public bool IsMission
        {
            get
            {
                if (!DBPath.Exists)
                    return false;
                if (!InitFile.Exists)
                    return false;
                return true;
            }
        }



        /// <summary>
        /// Получить список миссий сервера
        /// </summary>
        /// <param name="ServerPath">Путь к серверу</param>
        /// <returns></returns>
        public static List<Mission> GetMissions(DirectoryInfo ServerPath)
        {
            var m = new List<Mission>();
            var pMissions = new DirectoryInfo($"{ServerPath.FullName}\\{PATH_MISSIONS}");
            if (pMissions.Exists)
            {
                var ds = pMissions.EnumerateDirectories();
                foreach (var d in ds)
                {
                    var md = new Mission(d);
                    if (md.IsMission)
                        m.Add(md);
                }
            }
            return m;
        }
        /// <summary>
        /// Патерн для поиска хранилищ игровой ситуации
        /// </summary>
        public const string PATERN_SORTAGE = "storage_";
        /// <summary>
        /// Найти все хранилища данных
        /// </summary>
        private void FindSotrtages()
        {
            this.Sortages = new();
            if (MissionPath.Exists)
            {
                var sortages = MissionPath.EnumerateDirectories();
                foreach (var s in sortages)
                {
                    if (s.Name.Contains(PATERN_SORTAGE))
                    {
                        var t = s.Name.Split('_');
                        if (t.Length > 1)
                        {
                            if (int.TryParse(t[1], out int si))
                            {
                                if (!Sortages.Contains(si))
                                    Sortages.Add(si);
                            }
                        }
                    }
                }
            }
        }
    }
}
