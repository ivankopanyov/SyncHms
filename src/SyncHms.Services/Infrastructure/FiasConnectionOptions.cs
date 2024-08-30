namespace SyncHms.Services.Infrastructure;

/// <summary>
/// Класс, описывающий модель опций для установления подключения с удаленным сервисом <c>FIAS</c>
/// </summary>
internal class FiasConnectionOptions
{
    /// <summary>Поле, хранящее опции, определяющие модели сообщений бронирования.</summary>
    private static readonly HashSet<FiasOptions> _reservation = [
        new FiasGuestCheckInOptions
        {
            DateTime = true,
            ProfileNumber = true,
            GuestArrivalDate = true,
            GuestDepartureDate = true,
            GuestFirstName = true,
            GuestGroupNumber = true,
            GuestLanguage = true,
            GuestName = true,
            GuestTitle = true,
            GuestVipStatus = true,
            NoPostStatus = true,
            SwapFlag = true
        },
        new FiasGuestCheckOutOptions
        {
            DateTime = true,
            SwapFlag = true
        },
        new FiasGuestChangeOptions
        {
            DateTime = true,
            ProfileNumber = true,
            GuestArrivalDate = true,
            GuestDepartureDate = true,
            GuestFirstName = true,
            GuestGroupNumber = true,
            GuestLanguage = true,
            GuestName = true,
            GuestTitle = true,
            GuestVipStatus = true,
            NoPostStatus = true,
            OldRoomNumber = true
        }
    ];

    /// <summary>Поле, хранящее опции, определяющие модели сообщений денежных начислений.</summary>
    private static readonly HashSet<FiasOptions> _posting = [
        new FiasPostingSimpleOptions
        {
            SalesOutlet = true,
            TotalPostingAmount = true,
            PmsPaymentMethod = true,
            PostingSequenceNumber = true,
            CheckNumber = true,
            Discounts = true,
            Subtotals = true,
            ServiceCharge = true,
            Taxes = true,
        },
        new FiasPostingRequestOptions
        {
            PostingInquiry = true,
            MaximumGuests = true,
            SalesOutlet = true,
            UserId = true,
            DateTime = true,
            ReservationNumber = true,
            GuestName = true,
            RoomNumber = true,
            TotalPostingAmount = true,
            CheckNumber = true,
            Covers = true,
            ProfileNumber = true,
            Discounts = true,
            Subtotals = true,
            ServiceCharge = true,
            Taxes = true,
            Tip = true,
            CreditLimitOverrideFlag = true,
        },
        new FiasPostingListOptions
        {
            DateTime = true,
            CheckNumber = true,
            GuestArrivalDate = true,
            GuestDepartureDate = true,
            GuestFirstName = true,
            PmsPaymentMethod = true,
            NoPostStatus = true
        },
        new FiasPostingAnswerOptions
        {
            CheckNumber = true
        }
    ];

    /// <summary>Опции, определяющие модели сообщений бронирования.</summary>
    public static ISet<FiasOptions> Reservation => _reservation;

    /// <summary>Опции, определяющие модели сообщений денежных начислений.</summary>
    public static ISet<FiasOptions> Posting => _posting;
}