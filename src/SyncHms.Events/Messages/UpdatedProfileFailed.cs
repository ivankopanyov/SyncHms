namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий модель ошибки во время обработки обновленного профиля.</summary>
public class UpdatedProfileFailed
{
    /// <summary>Номер профиля.</summary>
    public decimal ProfileNumber { get; set; }

    /// <summary>Текст ошибки.</summary>
    public string ErrorMessage { get; set; }
}