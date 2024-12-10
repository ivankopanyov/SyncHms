namespace SyncHms.Domain;

/// <summary>Класс, описывающий модель опций планируемого события.</summary>
public class Schedule
{
    /// <summary>Имя планируемого события.</summary>
    public string Name { get; set; }

    /// <summary>Интервал выполнения планируемого события в секундах.</summary>
    public int IntervalSeconds { get; set; }

    /// <summary>Дата и время первой обработки события после изменения опций.</summary>
    public DateTime First { get; set; }

    /// <summary>Дата и время последней удачной обарботки планируемого события.</summary>
    public DateTime Last { get; set; }
}
