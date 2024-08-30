namespace SyncHms.Fias.Entities;

/// <summary>
/// To send <see cref="FiasPostingSimple"/> to FIAS, send an instance of
/// <see cref="FiasLinkRecord"/> to FIAS with an instance of
/// <see cref="FiasPostingSimpleOptions"/> passed to the constructor.<br/>
/// Чтобы отправить <see cref="FiasPostingSimple"/> в FIAS, отправьте экземпляр
/// <see cref="FiasLinkRecord"/> в FIAS с переданным в конструктор экземпляром
/// <see cref="FiasPostingSimpleOptions"/>.
/// </summary>
public partial class FiasPostingSimple : FiasMessageToPms
{
    /// <summary>
    /// <b>Required.</b><br/>
    /// <b>Обязательно.</b>
    /// </summary>
    [Required]
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Max length is 20.<br/>
    /// If the <see cref="PostingType"/> is <see cref="FiasPostingTypes.TelephoneCharge"/>
    /// and charge costing is done by PMS using <see cref="Duration"/>, must be sent.
    /// (numeric characters ONLY, like: "004989920920" (i.e. no separators or spaces).<br/>
    /// Максимальная длина = 20.<br/>
    /// Если <see cref="PostingType"/> — <see cref="FiasPostingTypes.TelephoneCharge"/>,
    /// а калькуляция затрат выполняется PMS с использованием <see cref="Duration"/>,
    /// должно быть отправлено. (ТОЛЬКО числовые символы, например: "004989920920"
    /// (т.е. без разделителей и пробелов).
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
    /// Valid values range from <see cref="FiasEnvironment.Min4"/> to <see cref="FiasEnvironment.Max4"/>.<br/>
    /// Required if <see cref="PostingType"/> is <see cref="FiasPostingTypes.MinibarCharge"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min4"/> до <see cref="FiasEnvironment.Max4"/>.<br/>
    /// Требуется, если <see cref="PostingType"/> - <see cref="FiasPostingTypes.MinibarCharge"/>.
    /// </summary>
    [Range(FiasEnvironment.Min4, FiasEnvironment.Max4)]
    public int? MinibarArticle { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min2"/> to <see cref="FiasEnvironment.Max2"/>.<br/>
    /// Required if <see cref="PostingType"/> is <see cref="FiasPostingTypes.MinibarCharge"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min2"/> до <see cref="FiasEnvironment.Max2"/>.<br/>
    /// Требуется, если <see cref="PostingType"/> — <see cref="FiasPostingTypes.MinibarCharge"/>.
    /// </summary>
    [Range(FiasEnvironment.Min2, FiasEnvironment.Max2)]
    public int? NumberOfArticles { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min10"/> to <see cref="FiasEnvironment.Max10"/>.<br/>
    /// Required if <see cref="PostingType"/> is <see cref="FiasPostingTypes.TelephoneCharge"/>
    /// and charge costing is done by PMS using meter pulses.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min10"/> до <see cref="FiasEnvironment.Max10"/>.<br/>
    /// Требуется, если <see cref="PostingType"/> — <see cref="FiasPostingTypes.TelephoneCharge"/>,
    /// а калькуляция затрат выполняется PMS с использованием метровых импульсов.
    /// </summary>
    [Range(FiasEnvironment.Min10, FiasEnvironment.Max10)]
    public long? MeterOrTaxPulse { get; set; }

    /// <summary>
    /// <b>Required.</b><br/>
    /// <b>Обязательно.</b>
    /// </summary>
    [Required]
    public FiasPostingTypes PostingType { get; set; }

    /// <summary>
    /// <b>Required.</b><br/>
    /// Max length is 10. Can be longer with Suite8 or OPERA PMS.<br/>
    /// <b>Обязательно.</b><br/>
    /// Максимальная длина = 10. Может быть длиннее с Suite8 или OPERA PMS.
    /// </summary>
    [Required(AllowEmptyStrings = true)]
    public string RoomNumber { get; set; }

    public string? CheckNumber { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min5"/> to <see cref="FiasEnvironment.Max5"/>.<br/>
    /// Required if the same interface uses more than one <see cref="PostingType"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min5"/> до <see cref="FiasEnvironment.Max5"/>.<br/>
    /// Требуется, если один и тот же интерфейс использует более одного <see cref="PostingType"/>.
    /// </summary>
    [Range(FiasEnvironment.Min5, FiasEnvironment.Max5)]
    public int? SalesOutlet { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min15"/> to <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Whole number only, no decimal places.<br/>
    /// Required if <see cref="PostingType"/> is <see cref="FiasPostingTypes.DirectCharge"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min15"/> до <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Только целое число без десятичных знаков.<br/>
    /// Требуется, если <see cref="PostingType"/> — <see cref="FiasPostingTypes.DirectCharge"/>.
    /// </summary>
    [FiasInteger]
    [Range(typeof(decimal), FiasEnvironment.Min15, FiasEnvironment.Max15)]
    public decimal? TotalPostingAmount { get; set; }

    /// <summary>
    /// Valid values range from <see cref="FiasEnvironment.Min15"/> to <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Whole number only, no decimal places.<br/>
    /// POS Cash Rounding Difference amount only if <see cref="Subtotals"/> is already used.<br/>
    /// POS Cash rounding difference amount(i.e., when occurring in Cash Postings with rounded
    /// <see cref="TotalPostingAmount"/> containing discounts).<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min15"/> до <see cref="FiasEnvironment.Max15"/>.<br/>
    /// Только целое число без десятичных знаков.<br/>
    /// Сумма разницы округления наличных средств POS, только если <see cref="Subtotals"/> уже используется.<br/>
    /// Сумма разницы округления наличных средств POS (т.е. при возникновении в постинге наличных средств с
    /// округленным <see cref="TotalPostingAmount"/>, содержащим скидки).
    /// </summary>
    [FiasInteger]
    [Range(typeof(decimal), FiasEnvironment.Min15, FiasEnvironment.Max15)]
    public decimal? SubtotalForCashPosRoundingDifferences { get; set; }

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
    /// Max length is 16.<br/>
    /// Максимальная длина = 16.
    /// </summary>
    [StringLength(16)]
    public string? UserId { get; set; }

    /// <summary>
    /// Valid values range from 1 to <see cref="FiasEnvironment.Max8"/>.<br/>
    /// Допустимое значение в диапазоне от 1 до <see cref="FiasEnvironment.Max8"/>.
    /// </summary>
    [Range(1, FiasEnvironment.Max8)]
    public int? PostingSequenceNumber { get; set; }

    public FiasPostingTypes? PostingCallType { get; set; }

    /// <summary>
    /// Max length is 5.<br/>
    /// Максимальная длина = 5.
    /// </summary>
    [StringLength(5)]
    public string? PmsPaymentMethod { get; set; }

    /// <summary>
    /// i.e. Trunk ID<br/>
    /// Valid values range from <see cref="FiasEnvironment.Min6"/> to <see cref="FiasEnvironment.Max6"/>.<br/>
    /// Допустимое значение в диапазоне от <see cref="FiasEnvironment.Min6"/> до <see cref="FiasEnvironment.Max6"/>.
    /// </summary>
    [Range(FiasEnvironment.Min6, FiasEnvironment.Max6)]
    public int? PostingRoute { get; set; }

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
    /// Max length is 16.<br/>
    /// Максимальная длина = 16.
    /// </summary>
    [StringLength(16)]
    public string? WorkStationId { get; set; }

    /// <summary>
    /// Max length is 25.<br/>
    /// Additional Posting information.<br/>
    /// Максимальная длина = 25.
    /// Дополнительная информация о постинге.
    /// </summary>
    [StringLength(25)]
    public string? CrossReferenceData { get; set; }
}
