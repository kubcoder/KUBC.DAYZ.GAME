using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.Tools.ItemTypes.Remover;

/// <summary>
/// Конфигурация очистки игровых предметов
/// </summary>
public class Config
{
    /// <summary>
    /// Выполнять очистку в файлах.
    /// Если установлено false будет выполнена
    /// проверка наличия игровых предметов в файле
    /// и выеден журнал отчета но удаление произведено не будет
    /// </summary>
    public bool Enable { get; set; } = false;

    /// <summary>
    /// Названия классов игровых 
    /// предметов которые необходимо исключить 
    /// из списка
    /// </summary>
    public List<string> ItemNames { get; set; } = [];
}
