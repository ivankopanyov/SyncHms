﻿namespace SyncHms.Fias.Entities;

/// <summary>
/// To send <see cref="FiasKeyAnswer"/> to FIAS, send an instance of
/// <see cref="FiasLinkRecord"/> to FIAS with an instance of
/// <see cref="FiasKeyAnswerOptions"/> passed to the constructor.<br/>
/// Чтобы отправить <see cref="FiasKeyAnswer"/> в FIAS, отправьте экземпляр
/// <see cref="FiasLinkRecord"/> в FIAS с переданным в конструктор экземпляром
/// <see cref="FiasKeyAnswerOptions"/>.
/// </summary>
public partial class FiasKeyAnswer : FiasMessageToPms
{
    /// <summary>
    /// <b>Required..</b><br/>
    /// <b>Обязательно.</b>
    /// </summary>
    [Required]
    public FiasAnswerStatuses AnswerStatus { get; set; }

    /// <summary>
    /// <b>Required.</b><br/>
    /// Max length is 40.<br/>
    /// <b>Обязательно.</b><br/>
    /// Максимальная длина = 40.
    /// </summary>
    [Required]
    [StringLength(40)]
    public string ClearText { get; set; }

    /// <summary>
    /// <b>Required.</b><br/>
    /// Max length is 8.<br/>
    /// Can only contain characters from the ASCII table in the range 32 to 127, inclusive.<br/>
    /// <b>Обязательно.</b><br/>
    /// Максимальная длина = 8.<br/>
    /// Может содержать только символы из таблицы ASCII в диапазоне от 32 до 127 включительно.
    /// </summary>
    [Required]
    [StringLength(8)]
    [RegularExpression(FiasEnvironment.AnPattern)]
    public string KeyCoder { get; set; }

    /// <summary>
    /// <b>Required.</b><br/>
    /// Max length is 16.<br/>
    /// <b>Обязательно.</b><br/>
    /// Максимальная длина = 16.
    /// </summary>
    [Required]
    [StringLength(16)]
    public string WorkStationId { get; set; }

    /// <summary>
    /// Max length is 19.<br/>
    /// Максимальная длина = 19.
    /// </summary>
    [StringLength(19)]
    public string? Track2Data { get; set; }

    /// <summary>
    /// Max length is 200.<br/>
    /// Максимальная длина = 200.
    /// </summary>
    [StringLength(200)]
    public string? Track3Data { get; set; }

    public DateTime? DateTime { get; set; }

    /// <summary>
    /// Valid values ​​range from <see cref="FiasEnvironment.Min10"/> to <see cref="FiasEnvironment.Max10"/>.<br/>
    /// Mandatory for On-Line key systems.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min10"/> до <see cref="FiasEnvironment.Max10"/>.<br/>
    /// Обязательно для систем ключей On-Line.
    /// </summary>
    [Range(FiasEnvironment.Min10, FiasEnvironment.Max10)]
    public long? ReservationNumber { get; set; }
}

