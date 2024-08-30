﻿namespace SyncHms.Fias.Entities;

/// <summary>
/// To send <see cref="FiasPostingRequest"/> to FIAS, send an instance of
/// <see cref="FiasLinkRecord"/> to FIAS with an instance of
/// <see cref="FiasPostingRequestOptions"/> passed to the constructor.<br/>
/// Чтобы отправить <see cref="FiasPostingRequest"/> в FIAS, отправьте экземпляр
/// <see cref="FiasLinkRecord"/> в FIAS с переданным в конструктор экземпляром
/// <see cref="FiasPostingRequestOptions"/>.
/// </summary>
public partial class FiasPostingRequest : FiasMessageToPms
{
    /// <summary>
    /// Required only after guest selection.<br/>
    /// Требуется только после выбора гостя.
    /// </summary>
    public DateTime? DateTime { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min10"/> to <see cref="FiasEnvironment.Max10"/>.<br/>
    /// Required only after guest selection.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min10"/> до <see cref="FiasEnvironment.Max10"/>.<br/>
    /// Требуется только после выбора гостя.
    /// </summary>
    [Range(FiasEnvironment.Min10, FiasEnvironment.Max10)]
    public long? ReservationNumber { get; set; }

    /// <summary>
    /// Max length is 200.<br/>
    /// Required only after guest selection.<br/>
    /// Максимальная длина = 200.<br/>
    /// Требуется только после выбора гостя.
    /// </summary>
    [StringLength(200)]
    public string? GuestName { get; set; }

    /// <summary>
    /// Max length is 10.<br/>
    /// Required only for inquiries with no guest selection.<br/>
    /// Максимальная длина = 10.<br/>
    /// Требуется только для запросов без выбора гостя.
    /// </summary>
    [StringLength(10)]
    public string? PostingInquiry { get; set; }

    /// <summary>
    /// <b>Required.</b><br/>
    /// Max length is 5.<br/>
    /// <b>Обязательно.</b><br/>
    /// Максимальная длина = 5.
    /// </summary>
    [Required]
    [StringLength(5)]
    public string PmsPaymentMethod { get; set; }

    /// <summary>
    /// Max length is 8. Can be longer with Suite8 or OPERA PMS.<br/>
    /// Mandatory in postings, but not in inquiries.<br/>
    /// Максимальная длина = 8. Может быть длиннее с Suite8 или OPERA PMS.<br/>
    /// Обязательно в сообщениях, но не в запросах.
    /// </summary>
    public string? RoomNumber { get; set; }

    /// <summary>
    /// <b>Required.</b><br/>
    /// Valid values range from 1 to <see cref="FiasEnvironment.Max8"/>.<br/>
    /// Shall be unique as per message sent and shall not be the value from
    /// <see cref="FiasPostingRequest.PostingInquiry"/> in corresponding <see cref="FiasPostingRequest"/>.<br/>
    /// <b>Обязательно.</b><br/>
    /// Допустимое значение в диапазоне от 1 до <see cref="FiasEnvironment.Max8"/>.<br/>
    /// Порядковый номер публикации должен быть уникальным для каждого отправленного
    /// сообщения и не должен иметь значение из <see cref="FiasPostingRequest.PostingInquiry"/> в
    /// соответствующем <see cref="FiasPostingRequest"/>.
    /// </summary>
    [Required]
    [Range(1, FiasEnvironment.Max8)]
    public int PostingSequenceNumber { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min15"/> to <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Whole number only, no decimal places.<br/>
    /// Required only after guest selection.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min15"/> до <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Только целое число без десятичных знаков.<br/>
    /// Требуется только после выбора гостя.
    /// </summary>
    [FiasInteger]
    [Range(typeof(decimal), FiasEnvironment.Min15, FiasEnvironment.Max15)]
    public decimal? TotalPostingAmount { get; set; }

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
    /// Valid values range from <see cref="FiasEnvironment.Min19"/> to <see cref="FiasEnvironment.Max19"/>.<br/>
    /// Whole number only, no decimal places.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min19"/> до <see cref="FiasEnvironment.Max19"/>.<br/>
    /// Только целое число без десятичных знаков.
    /// </summary>
    [FiasInteger]
    [Range(typeof(decimal), FiasEnvironment.Min19, FiasEnvironment.Max19)]
    public decimal? FidelioStandardTrack2Format { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min15"/> to <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Whole number only, no decimal places.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min15"/> до <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Только целое число без десятичных знаков.
    /// </summary>
    [FiasInteger]
    [Range(typeof(decimal), FiasEnvironment.Min15, FiasEnvironment.Max15)]
    public decimal? SubtotalForCashPosRoundingDifferences { get; set; }

    public string? CheckNumber { get; set; }

    /// <summary>
    /// This flag defines if the PMS Credit limit should be ignored for this
    /// posting. Normally only allowed for POS supervisors.<br/>
    /// Этот флаг определяет, следует ли игнорировать кредитный лимит для этого
    /// постинга. Обычно разрешено только для POS супервайзеров.
    /// </summary>
    public bool? CreditLimitOverrideFlag { get; set; }

    /// <summary>
    /// Max length is 20.<br/>
    /// Максимальная длина = 20.
    /// </summary>
    [StringLength(20)]
    public string? ClearText { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min5"/> to <see cref="FiasEnvironment.Max5"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min5"/> до <see cref="FiasEnvironment.Max5"/>.
    /// </summary>
    [Range(FiasEnvironment.Min5, FiasEnvironment.Max5)]
    public int? Covers { get; set; }

    /// <summary>
    /// Maximum array length is 9.<br/>
    /// Valid values for each item range from <see cref="FiasEnvironment.Min15"/> to <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Whole number only for each item, no decimal places.<br/>
    /// Максимальная длина массива = 9.<br/>
    /// Допустимое значение для каждого элемента в диапазоне от
    /// <see cref="FiasEnvironment.Min15"/> до <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Только целое число без десятичных знаков для каждого элемента.
    /// </summary>
    [MaxLength(9)]
    [FiasInteger]
    public decimal?[] Discounts { get; set; }

    /// <summary>
    /// Max length is 20.<br/>
    /// Numeric characters ONLY, like: "004989920920" (i.e. no separators or spaces).<br/>
    /// Максимальная длина = 20.<br/>
    /// ТОЛЬКО числовые символы, например: "004989920920" (т.е. без разделителей и пробелов).
    /// </summary>
    [StringLength(20)]
    [RegularExpression(@"^\d+$", ErrorMessage = "The field DialedDigits must contain only numeric characters.")]
    public string? DialedDigits { get; set; }

    /// <summary>
    /// Rounded to the nearest second.<br/>
    /// Округляется до секунд.
    /// </summary>
    public TimeOnly? Duration { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min10"/> to <see cref="FiasEnvironment.Max10"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min10"/> до <see cref="FiasEnvironment.Max10"/>.
    /// </summary>
    [Range(FiasEnvironment.Min10, FiasEnvironment.Max10)]
    public long? ProfileNumber { get; set; }

    /// <summary>
    /// Max length is 16.<br/>
    /// Максимальная длина = 16.
    /// </summary>
    [StringLength(16)]
    public string? UserId { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min4"/> to <see cref="FiasEnvironment.Max4"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min4"/> до <see cref="FiasEnvironment.Max4"/>.
    /// </summary>
    [Range(FiasEnvironment.Min4, FiasEnvironment.Max4)]
    public int? MinibarArticle { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min2"/> to <see cref="FiasEnvironment.Max2"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min2"/> до <see cref="FiasEnvironment.Max2"/>.
    /// </summary>
    [Range(FiasEnvironment.Min2, FiasEnvironment.Max2)]
    public int? NumberOfArticles { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min1"/> to <see cref="FiasEnvironment.Max1"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min1"/> до <see cref="FiasEnvironment.Max1"/>.
    /// </summary>
    [Range(FiasEnvironment.Min1, FiasEnvironment.Max1)]
    public int? MaximumGuests { get; set; }

    public FiasPostingTypes? PostingCallType { get; set; }

    /// <summary>
    /// Except <see cref="FiasPostingTypes.TelephoneCharge"/>.<br/>
    /// Кроме <see cref="FiasPostingTypes.TelephoneCharge"/>.
    /// </summary>
    public FiasPostingTypes? PostingType { get; set; }

    /// <summary>
    /// Maximum array length is 9.<br/>
    /// Valid values for each item range from <see cref="FiasEnvironment.Min15"/> to <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Whole number only for each item, no decimal places.<br/>
    /// Максимальная длина массива = 9.<br/>
    /// Допустимое значение для каждого элемента в диапазоне от
    /// <see cref="FiasEnvironment.Min15"/> до <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Только целое число без десятичных знаков для каждого элемента.
    /// </summary>
    [MaxLength(9)]
    [FiasInteger]
    public decimal?[] Subtotals { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min15"/> to <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Whole number only, no decimal places.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min15"/> до <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Только целое число без десятичных знаков.
    /// </summary>
    [FiasInteger]
    [Range(typeof(decimal), FiasEnvironment.Min15, FiasEnvironment.Max15)]
    public decimal? ServiceCharge { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min5"/> to <see cref="FiasEnvironment.Max5"/>.<br/>
    /// Required if the same interface uses more than one <see cref="PostingType"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min5"/> до <see cref="FiasEnvironment.Max5"/>.<br/>
    /// Требуется, если один и тот же интерфейс использует более одного <see cref="PostingType"/>.
    /// </summary>
    [Range(FiasEnvironment.Min5, FiasEnvironment.Max5)]
    public int? SalesOutlet { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min4"/> to <see cref="FiasEnvironment.Max4"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min4"/> до <see cref="FiasEnvironment.Max4"/>.
    /// </summary>
    [Range(FiasEnvironment.Min4, FiasEnvironment.Max4)]
    public int? ServingTime { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min4"/> to <see cref="FiasEnvironment.Max4"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min4"/> до <see cref="FiasEnvironment.Max4"/>.
    /// </summary>
    [Range(FiasEnvironment.Min4, FiasEnvironment.Max4)]
    public int? TableNumber { get; set; }

    /// <summary>
    /// Maximum array length is 9.<br/>
    /// Valid values for each item range from <see cref="FiasEnvironment.Min15"/> to <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Whole number only for each item, no decimal places.<br/>
    /// Максимальная длина массива = 9.<br/>
    /// Допустимое значение для каждого элемента в диапазоне от
    /// <see cref="FiasEnvironment.Min15"/> до <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Только целое число без десятичных знаков для каждого элемента.
    /// </summary>
    [MaxLength(9)]
    [FiasInteger]
    public decimal?[] Taxes { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min15"/> to <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Whole number only, no decimal places.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min15"/> до <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Только целое число без десятичных знаков.
    /// </summary>
    [FiasInteger]
    [Range(typeof(decimal), FiasEnvironment.Min15, FiasEnvironment.Max15)]
    public decimal? Tip { get; set; }

    /// <summary>
    /// Max length is 25.<br/>
    /// Additional Posting information.<br/>
    /// Максимальная длина = 25.<br/>
    /// Дополнительная информация о постинге.
    /// </summary>
    [StringLength(25)]
    public string? CrossReferenceData { get; set; }
}
