namespace SyncHms.Fias.Entities;

/// <summary>
/// To send <see cref="FiasMessageRequest"/> to FIAS, send an instance of
/// <see cref="FiasLinkRecord"/> to FIAS with an instance of
/// <see cref="FiasMessageRequestOptions"/> passed to the constructor.<br/>
/// Чтобы отправить <see cref="FiasMessageRequest"/> в FIAS, отправьте экземпляр
/// <see cref="FiasLinkRecord"/> в FIAS с переданным в конструктор экземпляром
/// <see cref="FiasMessageRequestOptions"/>.
/// </summary>
public partial class FiasMessageRequest : FiasMessageToPms
{
    /// <summary>
    /// <b>Required.</b><br/>
    /// Valid values range from <see cref="FiasEnvironment.Min10"/> to <see cref="FiasEnvironment.Max10"/>.<br/>
    /// <b>Обязательно.</b><br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min10"/> до <see cref="FiasEnvironment.Max10"/>.
    /// </summary>
    [Required]
    [Range(FiasEnvironment.Min10, FiasEnvironment.Max10)]
    public long ReservationNumber { get; set; }

    /// <summary>
    /// <b>Required.</b><br/>
    /// Max length is 8. Can be longer with Suite8 or OPERA PMS.<br/>
    /// <b>Обязательно.</b><br/>
    /// Максимальная длина = 8. Может быть длиннее с Suite8 или OPERA PMS.
    /// </summary>
    [Required]
    public string RoomNumber { get; set; }

    public DateTime? DateTime { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min8"/> to <see cref="FiasEnvironment.Max8"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min8"/> до <see cref="FiasEnvironment.Max8"/>.
    /// </summary>
    [Range(FiasEnvironment.Min8, FiasEnvironment.Max8)]
    public int? MessageId { get; set; }

    /// <summary>
    /// Change or do not change message status to "received" during request action.<br/>
    /// Менять или не менять статус сообщения на "получено" во время действия запроса.
    /// </summary>
    public bool? ChangeStatus { get; set; }
}

