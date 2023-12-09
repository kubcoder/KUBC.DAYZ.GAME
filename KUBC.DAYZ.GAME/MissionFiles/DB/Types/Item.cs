using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Types
{
    /// <summary>
    /// Класс описания итема в types.xml
    /// </summary>
    [Serializable]
    [XmlType("type")]
    public class Item
    {
        /// <summary>
        /// Элемент был изменен
        /// </summary>
        [XmlIgnore]
        public bool IsModified = false;

        /// <summary>
        /// Узнать были ли изменения в элементе.
        /// </summary>
        /// <remarks>
        /// Не отображает изменения если поле <see cref="Flags"/> было обнулено, или создано без изменений.
        /// </remarks>
        /// <returns>
        /// Истина если были изменения с момента создания или сброса флага <see cref="IsModified"/>
        /// </returns>
        public bool HasChanges()
        {
            if (IsModified) return true;
            if (Flags!=null)
            {
                return Flags.IsModify;
            }
            return false;
        }
        
        /// <summary>
        /// Название класса итема
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string? Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (value!=_Name)
                {
                    _Name = value;
                    IsModified = true;
                }
            }
        }
        private string? _Name;
        

        /// <summary>
        /// Номинальное кол-во итема на карте
        /// </summary>
        [XmlElement("nominal")]
        public int? Nominal
        {
            get { return _Nominal;}
            set 
            {
                if (value != _Nominal)
                {
                    _Nominal = value;
                    IsModified = true;
                }
            }
        }
        private int? _Nominal;
        /// <summary>
        /// Проверка на разрешение сериализации <see cref="Nominal"/>
        /// </summary>
        /// <returns>Если поле null то сериализация не выполняется</returns>
        public bool ShouldSerializeNominal() { return Nominal.HasValue; }


        /// <summary>
        /// Время жизни итема в секундах. 
        /// Т.е. сколько он живет на сервере от момента появления. По истечении этого срока обычно удаляется.
        /// </summary>
        [XmlElement("lifetime")]
        public int? LifeTime
        {
            get { return _LifeTime; }
            set
            {
                if (value != _LifeTime)
                {
                    _LifeTime = value;
                    IsModified = true;
                }
            }
        }
        private int? _LifeTime;
        /// <summary>
        /// Проверка на разрешение сериализации <see cref="LifeTime"/>
        /// </summary>
        /// <returns>Если поле null то сериализация не выполняется</returns>
        public bool ShouldSerializeLifeTime() { return LifeTime.HasValue; }


        /// <summary>
        /// Через сколько времени выполнить переразмещение элементов. Используется центральной экономикой.
        /// </summary>
        [XmlElement("restock")]
        public int? Restock
        {
            get { return _Restock; }
            set
            {
                if (value != _Restock)
                {
                    _Restock = value;
                    IsModified = true;
                }
            }
        }
        private int? _Restock;
        /// <summary>
        /// Проверка на разрешение сериализации <see cref="Restock"/>
        /// </summary>
        /// <returns>Если поле null то сериализация не выполняется</returns>
        public bool ShouldSerializeRestock() { return Restock.HasValue; }

        /// <summary>
        /// Минимальное число итема в игре после снижения до этого кол-ва центральная экономика начинает спавн данного итема 
        /// </summary>
        [XmlElement("min")]
        public int? Min
        {
            get { return _Min; }
            set
            {
                if (value != _Min)
                {
                    _Min = value;
                    IsModified = true;
                }
            }
        }
        private int? _Min;
        /// <summary>
        /// Проверка на разрешение сериализации <see cref="Min"/>
        /// </summary>
        /// <returns>Если поле null то сериализация не выполняется</returns>
        public bool ShouldSerializeMin() { return Min.HasValue; }

        /// <summary>
        /// Минимальное наполнение итема при спавне (т.е. наполнение итема)
        /// </summary>
        [XmlElement("quantmin")]
        public int? QuantMin
        {
            get { return _QuantMin; }
            set
            {
                if (value != _QuantMin)
                {
                    _QuantMin = value;
                    IsModified = true;
                }
            }
        }
        private int? _QuantMin;
        /// <summary>
        /// Проверка на разрешение сериализации <see cref="QuantMin"/>
        /// </summary>
        /// <returns>Если поле null то сериализация не выполняется</returns>
        public bool ShouldSerializeQuantMin() { return QuantMin.HasValue; }
        /// <summary>
        /// Максимальное наполнение итема при спавне (т.е. наполнение итема)
        /// </summary>
        [XmlElement("quantmax")]
        public int? QuantMax
        {
            get { return _QuantMax; }
            set
            {
                if (value != _QuantMax)
                {
                    _QuantMax = value;
                    IsModified = true;
                }
            }
        }
        private int? _QuantMax;
        /// <summary>
        /// Проверка на разрешение сериализации <see cref="QuantMax"/>
        /// </summary>
        /// <returns>Если поле null то сериализация не выполняется</returns>
        public bool ShouldSerializeQuantMax() { return QuantMax.HasValue; }
        /// <summary>
        /// Цена итема при спавне лута. Чем выше число тем чаще экономика будет заниматься данным итемом
        /// </summary>
        [XmlElement("cost")]
        public int? Cost
        {
            get { return _Cost; }
            set
            {
                if (value != _Cost)
                {
                    _Cost = value;
                    IsModified = true;
                }
            }
        }
        private int? _Cost;
        /// <summary>
        /// Проверка на разрешение сериализации <see cref="Cost"/>
        /// </summary>
        /// <returns>Если поле null то сериализация не выполняется</returns>
        public bool ShouldSerializeCost() { return Cost.HasValue; }

        /// <summary>
        /// Флаги спавна.
        /// </summary>
        /// <remarks>
        /// В каких расположениях учитывается кол-во итемов, разрешено ли удаление несобранного лута. детально описано в классе типа
        /// </remarks>
        [XmlElement(ElementName = "flags", IsNullable = false)]
        public Flags? Flags { get; set; }

        /// <summary>
        /// Категория итема. Возможные категории определены в файле cfglimitsdefinition.xml
        /// </summary>
        [XmlElement(ElementName = "category", IsNullable = false)]
        public List<Category>? Category { get; set; }
        /// <summary>
        /// Обновляем список категорий
        /// </summary>
        /// <param name="nCategories">Новый набор категорий</param>
        public void UpdateCategory(IEnumerable<string> nCategories)
        {
            if (Category != null)
            {
                int i = 0;
                while (i < Category.Count)
                {
                    if (nCategories.Contains(Category[i].Name))
                    {
                        i++;
                    }
                    else
                    {
                        IsModified = true;
                        Category.RemoveAt(i);
                    }
                }
            }
            if (nCategories.Any())
            {
                Category ??= [];
                foreach (var nCat in nCategories)
                {
                    if (!Category.Where(x => x.Name == nCat).Any())
                    {
                        Category.Add(new Category() { Name = nCat });
                        IsModified = true;
                    }
                }
            }
            if ((Category != null) && (Category.Count==0))
                Category = null;
        }


        /// <summary>
        /// В каких зонах Tier разрешен спавн данного итема
        /// </summary>
        /// <remarks>
        /// Возможные значения определены в файле cfglimitsdefinition.xml
        /// </remarks>
        [XmlElement(ElementName = "value", IsNullable = false)]
        public List<Value>? Values { get; set; }
        /// <summary>
        /// Обновляем список зон спавна
        /// </summary>
        /// <param name="nValues">Новый набор зон</param>
        public void UpdateValues(IEnumerable<string> nValues)
        {
            if (Values != null)
            {
                int i = 0;
                while (i < Values.Count)
                {
                    if (nValues.Contains(Values[i].Name))
                    {
                        i++;
                    }
                    else
                    {
                        IsModified = true;
                        Values.RemoveAt(i);
                    }
                }
            }
            if (nValues.Any())
            {
                Values ??= [];
                foreach (var nValue in nValues)
                {
                    if (!Values.Where(x => x.Name == nValue).Any())
                    {
                        Values.Add(new Value() { Name = nValue });
                        IsModified = true;
                    }
                }
            }
            if ((Values != null) && (Values.Count==0))
                Values = null;
        }

        /// <summary>
        /// В каких объектах может спавнится данный лут, например военные объекты, полиция или что то еще.
        /// </summary>
        /// <remarks>
        /// Возможные значения определены в файле cfglimitsdefinition.xml
        /// </remarks>
        [XmlElement(ElementName = "usage", IsNullable = false)]
        public List<Usage>? Usages { get; set; }
        /// <summary>
        /// Обновляем список использований
        /// </summary>
        /// <param name="nUsages">Новый набор использований</param>
        public void UpdateUsages(IEnumerable<string> nUsages)
        {
            if (Usages != null)
            {
                int i = 0;
                while (i < Usages.Count)
                {
                    if (nUsages.Contains(Usages[i].Name))
                    {
                        i++;
                    }
                    else
                    {
                        IsModified = true;
                        Usages.RemoveAt(i);
                    }
                }
            }
            if (nUsages.Any())
            {
                Usages ??= [];
                foreach (var nUsage in nUsages)
                {
                    if (!Usages.Where(x => x.Name == nUsage).Any())
                    {
                        Usages.Add(new Usage() { Name = nUsage });
                        IsModified = true;
                    }
                }
            }
            if ((Usages != null) && (Usages.Count == 0))
                Usages = null;
        }

        /// <summary>
        /// Таг спавна. Т.е. где размещать, на полу или прямо на земле.
        /// </summary>
        /// <remarks>
        /// Возможные значения определены в файле cfglimitsdefinition.xml
        /// </remarks>
        [XmlElement(ElementName = "tag", IsNullable = false)]
        public List<Tag>? Tags { get; set; }
        /// <summary>
        /// Обновляем список тэгов
        /// </summary>
        /// <param name="ntags">Новый набор тэгов</param>
        public void UpdateTags(IEnumerable<string> ntags)
        {
            if (Tags != null) 
            {
                int i = 0;
                while (i < Tags.Count)
                {
                    if (ntags.Contains(Tags[i].Name))
                    {
                        i++;
                    }
                    else
                    {
                        IsModified = true;
                        Tags.RemoveAt(i);
                    }
                }
            }
            if (ntags.Any())
            {
                Tags ??= [];
                foreach(var ntag in ntags)
                {
                    if (!Tags.Where(x=> x.Name == ntag).Any())
                    {
                        Tags.Add(new Tag() { Name = ntag });
                        IsModified = true;
                    }
                }
            }
            if ((Tags!=null)&&(Tags.Count == 0))
                Tags = null;
        }
    }
}
