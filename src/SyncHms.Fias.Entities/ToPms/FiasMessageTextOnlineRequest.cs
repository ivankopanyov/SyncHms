namespace SyncHms.Fias.Entities;

/// <summary>
/// To send <see cref="FiasMessageTextOnlineRequest"/> to FIAS, send an instance of
/// <see cref="FiasLinkRecord"/> to FIAS with an instance of
/// <see cref="FiasMessageTextOnlineOptions"/> passed to the constructor.<br/>
/// Чтобы отправить <see cref="FiasMessageTextOnlineRequest"/> в FIAS, отправьте экземпляр
/// <see cref="FiasLinkRecord"/> в FIAS с переданным в конструктор экземпляром
/// <see cref="FiasMessageTextOnlineOptions"/>.
/// </summary>
public partial class FiasMessageTextOnlineRequest : FiasMessageToPms
{
    /// <summary>
    /// <b>Required</b><br/>
    /// Valid values range from <see cref="FiasEnvironment.Min10"/> to <see cref="FiasEnvironment.Max10"/>.<br/>
    /// <b>Обязательно.</b><br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min10"/> до <see cref="FiasEnvironment.Max10"/>.
    /// </summary>
    [Required]
    [Range(FiasEnvironment.Min10, FiasEnvironment.Max10)]
    public long ReservationNumber { get; set; }

    /// <summary>
    /// <b>Required.</b><br/>
    /// Max length is 2000.<br/>
    /// <b>Обязательно.</b><br/>
    /// Максимальная длина = 2000.
    /// </summary>
    [Required]
    [StringLength(2000)]
    public string MessageText { get; set; }

    /// <summary>
    /// <b>Required.</b><br/>
    /// Max length is 8. Can be longer with Suite8 or OPERA PMS.<br/>
    /// <b>Обязательно.</b><br/>
    /// Максимальная длина = 8. Может быть длиннее с Suite8 или OPERA PMS.
    /// </summary>
    [Required]
    public string RoomNumber { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min8"/> to <see cref="FiasEnvironment.Max8"/>.<br/>
    /// Mandatory for the creation of messages.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min8"/> до <see cref="FiasEnvironment.Max8"/>.<br/>
    /// Обязателен для создания сообщений.
    /// </summary>
    [Range(FiasEnvironment.Min8, FiasEnvironment.Max8)]
    public int? ExternalMessageId { get; set; }

    public DateTime? DateTime { get; set; }
}

