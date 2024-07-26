namespace SyncHms.Fias.Entities;

/// <summary>
/// To send <see cref="FiasRemoteCheckOutRequest"/> to FIAS, send an instance of
/// <see cref="FiasLinkRecord"/> to FIAS with an instance of
/// <see cref="FiasRemoteCheckOutOptions"/> passed to the constructor.<br/>
/// Чтобы отправить <see cref="FiasRemoteCheckOutRequest"/> в FIAS, отправьте экземпляр
/// <see cref="FiasLinkRecord"/> в FIAS с переданным в конструктор экземпляром
/// <see cref="FiasRemoteCheckOutOptions"/>.
/// </summary>
public partial class FiasRemoteCheckOutRequest : FiasMessageToPms
{
    /// <summary>
    /// <b>Required.</b><br/>
    /// Valid values range from <see cref="FiasEnvironment.Min20"/> to <see cref="FiasEnvironment.Max20"/>.<br/>
    /// Whole number only, no decimal places.<br/>
    /// <b>Обязательно.</b><br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min20"/> до <see cref="FiasEnvironment.Max20"/>.<br/>
    /// Только целое число без десятичных знаков.
    /// </summary>
    [Required]
    [FiasInteger]
    [Range(typeof(decimal), FiasEnvironment.Min20, FiasEnvironment.Max20)]
    public decimal BalanceAmount { get; set; }

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
}

