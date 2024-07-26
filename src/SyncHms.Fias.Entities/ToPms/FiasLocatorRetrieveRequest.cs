namespace SyncHms.Fias.Entities;

/// <summary>
/// To send <see cref="FiasLocatorRetrieveRequest"/> to FIAS, send an instance of
/// <see cref="FiasLinkRecord"/> to FIAS with an instance of
/// <see cref="FiasLocatorRetrieveOptions"/> passed to the constructor.<br/>
/// Чтобы отправить <see cref="FiasLocatorRetrieveRequest"/> в FIAS, отправьте экземпляр
/// <see cref="FiasLinkRecord"/> в FIAS с переданным в конструктор экземпляром
/// <see cref="FiasLocatorRetrieveOptions"/>.
/// </summary>
public partial class FiasLocatorRetrieveRequest : FiasMessageToPms
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

    public DateTime? DateTime { get; set; }

    /// <summary>
    /// Max length is 8. Can be longer with Suite8 or OPERA PMS.<br/>
    /// Максимальная длина = 8. Может быть длиннее с Suite8 или OPERA PMS.
    /// </summary>
    public string? RoomNumber { get; set; }
}

