namespace SyncHms.Opera.Entities.Tables;

public partial class AllotmentHeader
{
    public AllotmentHeader()
    {
        AllotmentAlternateDates = new HashSet<AllotmentAlternateDates>();
        AllotmentChanges = new HashSet<AllotmentChanges>();
        AllotmentChangesNet = new HashSet<AllotmentChangesNet>();
        AllotmentCutoffSchedule = new HashSet<AllotmentCutoffSchedule>();
        AllotmentDateProtection = new HashSet<AllotmentDateProtection>();
        AllotmentDetail = new HashSet<AllotmentDetail>();
        AllotmentOffsetRevenueDtl = new HashSet<AllotmentOffsetRevenueDtl>();
        AllotmentOffsetType = new HashSet<AllotmentOffsetType>();
        AllotmentProducts = new HashSet<AllotmentProducts>();
        AllotmentRemoteOwners = new HashSet<AllotmentRemoteOwners>();
        AllotmentRestrictions = new HashSet<AllotmentRestrictions>();
        AllotmentTlgo = new HashSet<AllotmentTlgo>();
        AllotmentTraces = new HashSet<AllotmentTraces>();
        BlockAccessExclusions = new HashSet<BlockAccessExclusions>();
        BookNotes = new HashSet<BookNotes>();
        BookOwner = new HashSet<BookOwner>();
        BookProfileLink = new HashSet<BookProfileLink>();
        BookingAttendees = new HashSet<BookingAttendees>();
        BookingLink = new HashSet<BookingLink>();
        ContractSend = new HashSet<ContractSend>();
        EventChanges = new HashSet<EventChanges>();
        EventReservation = new HashSet<EventReservation>();
        EventStatusChanges = new HashSet<EventStatusChanges>();
        FbAgenda = new HashSet<FbAgenda>();
        FitContractRateDetails = new HashSet<FitContractRateDetails>();
        GemEvent = new HashSet<GemEvent>();
        GemEventgroup = new HashSet<GemEventgroup>();
        Lead = new HashSet<Lead>();
        LeadContacts = new HashSet<LeadContacts>();
        LeadDestination = new HashSet<LeadDestination>();
        LeadHistory = new HashSet<LeadHistory>();
        MeetingRoomInventory = new HashSet<MeetingRoomInventory>();
    }

    public string? Resort { get; set; }
    public decimal? AllotmentHeaderId { get; set; }
    public string? AllotmentType { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public DateTime? ShoulderBeginDate { get; set; }
    public DateTime? ShoulderEndDate { get; set; }
    public string? AllotmentCode { get; set; }
    public decimal? MasterNameId { get; set; }
    public decimal? CompanyNameId { get; set; }
    public decimal? AgentNameId { get; set; }
    public decimal? SourceNameId { get; set; }
    public DateTime? InactiveDate { get; set; }
    public string? CancelRule { get; set; }
    public string? RateCode { get; set; }
    public string? BookingStatus { get; set; }
    public decimal? BookingStatusOrder { get; set; }
    public string? Status { get; set; }
    public decimal? Elastic { get; set; }
    public DateTime? InvCutoffDate { get; set; }
    public decimal? InvCutoffDays { get; set; }
    public decimal? TentativeLevel { get; set; }
    public string? Info { get; set; }
    public string? MarketCode { get; set; }
    public string? Source { get; set; }
    public string? Channel { get; set; }
    public decimal? AvgPeoplePerRoom { get; set; }
    public string? OriginalRateCode { get; set; }
    public string? BookingId { get; set; }
    public decimal? CancellationNo { get; set; }
    public string? CancellationCode { get; set; }
    public DateTime? CancellationDate { get; set; }
    public string? CancellationDesc { get; set; }
    public string? GuaranteeCode { get; set; }
    public decimal? RoomsPerDay { get; set; }
    public decimal? AverageRate { get; set; }
    public string? ReserveInventoryYn { get; set; }
    public string? AllotmentOrigion { get; set; }
    public int? SuperBlockId { get; set; }
    public string? SuperBlockResort { get; set; }
    public decimal? ActionId { get; set; }
    public decimal? DmlSeqNo { get; set; }
    public decimal? ContactNameId { get; set; }
    public string? Alias { get; set; }
    public string? SalesId { get; set; }
    public string? PaymentMethod { get; set; }
    public string? RivMarketSegment { get; set; }
    public string? ExchangePostingType { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal? ExchangeRate { get; set; }
    public DateTime? DateOpenedForPickup { get; set; }
    public string? Mainmarket { get; set; }
    public string? Tracecode { get; set; }
    public string? OwnerResort { get; set; }
    public decimal? Owner { get; set; }
    public string? OwnerCode { get; set; }
    public string? RmsOwnerResort { get; set; }
    public decimal? RmsOwner { get; set; }
    public string? RmsOwnerCode { get; set; }
    public string? CatOwnerResort { get; set; }
    public decimal? CatOwner { get; set; }
    public string? CatOwnerCode { get; set; }
    public string? Bookingtype { get; set; }
    public string? Bookingmethod { get; set; }
    public DateTime? MethodDue { get; set; }
    public DateTime? RmsDecision { get; set; }
    public DateTime? RmsFollowup { get; set; }
    public string? RmsCurrency { get; set; }
    public string? RmsQuoteCurr { get; set; }
    public decimal? RmsExchange { get; set; }
    public decimal? Attendees { get; set; }
    public string? CatStatus { get; set; }
    public DateTime? CatDecision { get; set; }
    public DateTime? CatFollowup { get; set; }
    public DateTime? CatCutoff { get; set; }
    public string? CatCurrency { get; set; }
    public string? CatQuoteCurr { get; set; }
    public decimal? CatExchange { get; set; }
    public decimal? CatCanxNo { get; set; }
    public string? CatCanxCode { get; set; }
    public DateTime? CatCanxDate { get; set; }
    public string? CatCanxDesc { get; set; }
    public string? Infoboard { get; set; }
    public string? BfstYn { get; set; }
    public decimal? BfstPrice { get; set; }
    public string? BfstDesc { get; set; }
    public string? PorterageYn { get; set; }
    public decimal? PorteragePrice { get; set; }
    public string? Commission { get; set; }
    public string? DetailsOkYn { get; set; }
    public string? DistributedYn { get; set; }
    public string? ContractNr { get; set; }
    public string? Functiontype { get; set; }
    public string? Representative { get; set; }
    public decimal? DefaultPmResvNameId { get; set; }
    public string? CateringonlyYn { get; set; }
    public string? EventsGuaranteedYn { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal? ServiceCharge { get; set; }
    public string? Udfc01 { get; set; }
    public string? Udfc02 { get; set; }
    public string? Udfc03 { get; set; }
    public string? Udfc04 { get; set; }
    public string? Udfc05 { get; set; }
    public string? Udfc06 { get; set; }
    public string? Udfc07 { get; set; }
    public string? Udfc08 { get; set; }
    public string? Udfc09 { get; set; }
    public string? Udfc10 { get; set; }
    public DateTime? Udfd01 { get; set; }
    public DateTime? Udfd02 { get; set; }
    public DateTime? Udfd03 { get; set; }
    public DateTime? Udfd04 { get; set; }
    public DateTime? Udfd05 { get; set; }
    public DateTime? Udfd06 { get; set; }
    public DateTime? Udfd07 { get; set; }
    public DateTime? Udfd08 { get; set; }
    public DateTime? Udfd09 { get; set; }
    public DateTime? Udfd10 { get; set; }
    public decimal? Udfn01 { get; set; }
    public decimal? Udfn02 { get; set; }
    public decimal? Udfn03 { get; set; }
    public decimal? Udfn04 { get; set; }
    public decimal? Udfn05 { get; set; }
    public decimal? Udfn06 { get; set; }
    public decimal? Udfn07 { get; set; }
    public decimal? Udfn08 { get; set; }
    public decimal? Udfn09 { get; set; }
    public decimal? Udfn10 { get; set; }
    public string? DownloadResort { get; set; }
    public decimal? DownloadSrep { get; set; }
    public DateTime? DownloadDate { get; set; }
    public DateTime? UploadDate { get; set; }
    public byte? LaptopChange { get; set; }
    public string? ExternalReference { get; set; }
    public string? ExternalLocked { get; set; }
    public decimal? ProfileId { get; set; }
    public string? ResortBooked { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? ManualCutoff { get; set; }
    public string? SnapshotSetup { get; set; }
    public string? TbdRates { get; set; }
    public string? Destination { get; set; }
    public string? LeadSource { get; set; }
    public string? Program { get; set; }
    public string? Competition { get; set; }
    public string? ControlBlockYn { get; set; }
    public string? CrsGtdYn { get; set; }
    public string? Udfc11 { get; set; }
    public string? Udfc12 { get; set; }
    public string? Udfc13 { get; set; }
    public string? Udfc14 { get; set; }
    public string? Udfc15 { get; set; }
    public string? Udfc16 { get; set; }
    public string? Udfc17 { get; set; }
    public string? Udfc18 { get; set; }
    public string? Udfc19 { get; set; }
    public string? Udfc20 { get; set; }
    public string? Udfc21 { get; set; }
    public string? Udfc22 { get; set; }
    public string? Udfc23 { get; set; }
    public string? Udfc24 { get; set; }
    public string? Udfc25 { get; set; }
    public string? Udfc26 { get; set; }
    public string? Udfc27 { get; set; }
    public string? Udfc28 { get; set; }
    public string? Udfc29 { get; set; }
    public string? Udfc30 { get; set; }
    public string? Udfc31 { get; set; }
    public string? Udfc32 { get; set; }
    public string? Udfc33 { get; set; }
    public string? Udfc34 { get; set; }
    public string? Udfc35 { get; set; }
    public string? Udfc36 { get; set; }
    public string? Udfc37 { get; set; }
    public string? Udfc38 { get; set; }
    public string? Udfc39 { get; set; }
    public string? Udfc40 { get; set; }
    public DateTime? Udfd11 { get; set; }
    public DateTime? Udfd12 { get; set; }
    public DateTime? Udfd13 { get; set; }
    public DateTime? Udfd14 { get; set; }
    public DateTime? Udfd15 { get; set; }
    public DateTime? Udfd16 { get; set; }
    public DateTime? Udfd17 { get; set; }
    public DateTime? Udfd18 { get; set; }
    public DateTime? Udfd19 { get; set; }
    public DateTime? Udfd20 { get; set; }
    public decimal? Udfn11 { get; set; }
    public decimal? Udfn12 { get; set; }
    public decimal? Udfn13 { get; set; }
    public decimal? Udfn14 { get; set; }
    public decimal? Udfn15 { get; set; }
    public decimal? Udfn16 { get; set; }
    public decimal? Udfn17 { get; set; }
    public decimal? Udfn18 { get; set; }
    public decimal? Udfn19 { get; set; }
    public decimal? Udfn20 { get; set; }
    public decimal? Udfn21 { get; set; }
    public decimal? Udfn22 { get; set; }
    public decimal? Udfn23 { get; set; }
    public decimal? Udfn24 { get; set; }
    public decimal? Udfn25 { get; set; }
    public decimal? Udfn26 { get; set; }
    public decimal? Udfn27 { get; set; }
    public decimal? Udfn28 { get; set; }
    public decimal? Udfn29 { get; set; }
    public decimal? Udfn30 { get; set; }
    public decimal? Udfn31 { get; set; }
    public decimal? Udfn32 { get; set; }
    public decimal? Udfn33 { get; set; }
    public decimal? Udfn34 { get; set; }
    public decimal? Udfn35 { get; set; }
    public decimal? Udfn36 { get; set; }
    public decimal? Udfn37 { get; set; }
    public decimal? Udfn38 { get; set; }
    public decimal? Udfn39 { get; set; }
    public decimal? Udfn40 { get; set; }
    public string? SynchronizeYn { get; set; }
    public DateTime? OriginalBeginDate { get; set; }
    public DateTime? OriginalEndDate { get; set; }
    public decimal? MtgRevenue { get; set; }
    public decimal? MtgBudget { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public DateTime? DepartureTime { get; set; }
    public string? CompRoomsFixedYn { get; set; }
    public decimal? CompRooms { get; set; }
    public string? CompPerStayYn { get; set; }
    public decimal? CompRoomValue { get; set; }
    public string? Udescription { get; set; }
    public string? Xdescription { get; set; }
    public string? Xudescription { get; set; }
    public decimal? RmCommission1 { get; set; }
    public decimal? RmCommission2 { get; set; }
    public decimal? FbCommission1 { get; set; }
    public decimal? FbCommission2 { get; set; }
    public string? CateringPkgsYn { get; set; }
    public decimal? AgentContactNameId { get; set; }
    public string? ShowRateAmountYn { get; set; }
    public string? PrintRateYn { get; set; }
    public string? LeadType { get; set; }
    public string? LeadOrigin { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Leadstatus { get; set; }
    public string? SentYn { get; set; }
    public DateTime? SentDate { get; set; }
    public string? SentVia { get; set; }
    public decimal? SentBy { get; set; }
    public string? ReplyStatus { get; set; }
    public DateTime? ReplyDate { get; set; }
    public string? ReplyVia { get; set; }
    public decimal? ReplyBy { get; set; }
    public DateTime? DatePel { get; set; }
    public DateTime? DateAcl { get; set; }
    public DateTime? DateTdl { get; set; }
    public DateTime? DateCfl { get; set; }
    public DateTime? DateLsl { get; set; }
    public string? TdlReason { get; set; }
    public string? LeadNewYn { get; set; }
    public string? LeadReceivedYn { get; set; }
    public string? Leadsend1 { get; set; }
    public string? Leadsend2 { get; set; }
    public string? Leadsend3 { get; set; }
    public string? ComMethod1 { get; set; }
    public string? ComMethod2 { get; set; }
    public string? ComMethod3 { get; set; }
    public string? ComAddress1 { get; set; }
    public string? ComAddress2 { get; set; }
    public string? ComAddress3 { get; set; }
    public string? LeadError { get; set; }
    public decimal? RespTime { get; set; }
    public string? RespTimeCode { get; set; }
    public DateTime? UpdateDateExternal { get; set; }
    public string? HideAccInfoYn { get; set; }
    public string? PendingSendYn { get; set; }
    public string? SendToCentralYn { get; set; }
    public decimal? CreditCardId { get; set; }
    public string? SyncContractYn { get; set; }
    public string? ExclusionMessage { get; set; }
    public decimal? PotRoomNights { get; set; }
    public decimal? PotRoomRevenue { get; set; }
    public decimal? PotFbRevenue { get; set; }
    public decimal? PotOtherRevenue { get; set; }
    public string? CommissionableYn { get; set; }
    public decimal? CommissionablePerc { get; set; }
    public decimal? FitDiscountPerc { get; set; }
    public decimal? FitDiscountLevel { get; set; }
    public string? BfstInclYn { get; set; }
    public decimal? BfstInclPrice { get; set; }
    public string? ServiceInclYn { get; set; }
    public decimal? ServicePerc { get; set; }
    public string? DblRmSupplementYn { get; set; }
    public decimal? DblRmSupplementPrice { get; set; }
    public string? TaxIncludedYn { get; set; }
    public decimal? TaxIncludedPerc { get; set; }
    public string? CentralOwner { get; set; }
    public string? RateOverride { get; set; }
    public string? SellThruYn { get; set; }
    public string? ServiceFeeYn { get; set; }
    public decimal? ServiceFee { get; set; }
    public decimal? CatItemDiscount { get; set; }
    public DateTime? BeginDateOriginal { get; set; }
    public DateTime? EndDateOriginal { get; set; }
    public string? RoomingListRules { get; set; }
    public string? FlatRateYn { get; set; }
    public string? Tourcode { get; set; }
    public string? BlockType { get; set; }
    public string? GreekContractNr { get; set; }
    public string? TaRecordLocator { get; set; }
    public string? Ualias { get; set; }
    public string? RateOverrideReason { get; set; }
    public DateTime? OriginalBeginDateHolidex { get; set; }
    public string? PublishRatesYn { get; set; }
    public string? TaxType { get; set; }
    public string? SubAllotmentYn { get; set; }
    public DateTime? BeoLastPrint { get; set; }
    public string? AttachmentUrl { get; set; }
    public string? LeadchangeBypropertyYn { get; set; }
    public string? KeepLeadControlYn { get; set; }
    public DateTime? DueDateOrd { get; set; }
    public string? AllowAlternateDatesYn { get; set; }
    public string? RegeneratedLeadYn { get; set; }
    public string? OrmsBlockClass { get; set; }
    public string? LostToProperty { get; set; }
    public decimal? CxlPenalty { get; set; }
    public string? OrmsFinalBlock { get; set; }
    public string? FitDiscountType { get; set; }
    public string? OrmsTransientBlock { get; set; }
    public decimal? HlxDepositDays { get; set; }
    public decimal? HlxCanxNoticeDays { get; set; }
    public string? HlxReturnEachDayYn { get; set; }
    public string? HlxCommissionableYn { get; set; }
    public string? HlxDiSecuredYn { get; set; }
    public string? HlxDdSecuredYn { get; set; }
    public string? HlxRatesGnrSecuredYn { get; set; }
    public string? HlxRateAllSecuredYn { get; set; }
    public string? HlxHousinginfoSecuredYn { get; set; }
    public string? IsacOpptyId { get; set; }
    public DateTime? LinkDate { get; set; }
    public string? TlpResponseid { get; set; }
    public string? TlpUrl { get; set; }
    public DateTime? DistributedDate { get; set; }
    public string? FbAgendaCurr { get; set; }
    public string? FitContractMode { get; set; }
    public string? ProposalShowSpacenameYn { get; set; }
    public string? ProposalShowEventpriceYn { get; set; }
    public string? ProposalOwnerSelection { get; set; }
    public string? ProposalDecisionSelection { get; set; }
    public DateTime? ProposalSentDate { get; set; }
    public string? ProposalViewToken { get; set; }
    public string? AllotmentClassification { get; set; }
    public string? SuperSearchIndexText { get; set; }
    public string? RateProtection { get; set; }
    public string? NonCompete { get; set; }
    public string? ConversionCode { get; set; }
    public string? RankingCode { get; set; }
    public string? NonCompeteCode { get; set; }
    public string? RateGuaranteedYn { get; set; }
    public string? ProposalShowPmsRoomTypeYn { get; set; }
    public string? OffsetType { get; set; }
    public string? ScQuoteId { get; set; }
    public string? ProposalFollowupSelection { get; set; }
    public string? ProposalInclAltNamesYn { get; set; }
    public decimal? OrigAllotmentHeaderId { get; set; }
    public string? WebBookableYn { get; set; }
    public string? ProposalCombineEventsYn { get; set; }
    public string? ProposalSpaceMeasurement { get; set; }
    public string? AutoLoadForecastYn { get; set; }
    public string? BwiLeadId { get; set; }
    public string? BwiUrl { get; set; }

    public virtual Name AgentName { get; set; }
    public virtual Name CompanyName { get; set; }
    public virtual Name MasterName { get; set; }
    public virtual Name SourceName { get; set; }
    public virtual ICollection<AllotmentAlternateDates> AllotmentAlternateDates { get; set; }
    public virtual ICollection<AllotmentChanges> AllotmentChanges { get; set; }
    public virtual ICollection<AllotmentChangesNet> AllotmentChangesNet { get; set; }
    public virtual ICollection<AllotmentCutoffSchedule> AllotmentCutoffSchedule { get; set; }
    public virtual ICollection<AllotmentDateProtection> AllotmentDateProtection { get; set; }
    public virtual ICollection<AllotmentDetail> AllotmentDetail { get; set; }
    public virtual ICollection<AllotmentOffsetRevenueDtl> AllotmentOffsetRevenueDtl { get; set; }
    public virtual ICollection<AllotmentOffsetType> AllotmentOffsetType { get; set; }
    public virtual ICollection<AllotmentProducts> AllotmentProducts { get; set; }
    public virtual ICollection<AllotmentRemoteOwners> AllotmentRemoteOwners { get; set; }
    public virtual ICollection<AllotmentRestrictions> AllotmentRestrictions { get; set; }
    public virtual ICollection<AllotmentTlgo> AllotmentTlgo { get; set; }
    public virtual ICollection<AllotmentTraces> AllotmentTraces { get; set; }
    public virtual ICollection<BlockAccessExclusions> BlockAccessExclusions { get; set; }
    public virtual ICollection<BookNotes> BookNotes { get; set; }
    public virtual ICollection<BookOwner> BookOwner { get; set; }
    public virtual ICollection<BookProfileLink> BookProfileLink { get; set; }
    public virtual ICollection<BookingAttendees> BookingAttendees { get; set; }
    public virtual ICollection<BookingLink> BookingLink { get; set; }
    public virtual ICollection<ContractSend> ContractSend { get; set; }
    public virtual ICollection<EventChanges> EventChanges { get; set; }
    public virtual ICollection<EventReservation> EventReservation { get; set; }
    public virtual ICollection<EventStatusChanges> EventStatusChanges { get; set; }
    public virtual ICollection<FbAgenda> FbAgenda { get; set; }
    public virtual ICollection<FitContractRateDetails> FitContractRateDetails { get; set; }
    public virtual ICollection<GemEvent> GemEvent { get; set; }
    public virtual ICollection<GemEventgroup> GemEventgroup { get; set; }
    public virtual ICollection<Lead> Lead { get; set; }
    public virtual ICollection<LeadContacts> LeadContacts { get; set; }
    public virtual ICollection<LeadDestination> LeadDestination { get; set; }
    public virtual ICollection<LeadHistory> LeadHistory { get; set; }
    public virtual ICollection<MeetingRoomInventory> MeetingRoomInventory { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<AllotmentHeader>(entity =>
        {
            entity.HasKey(e => new { e.AllotmentHeaderId, e.Resort })
                .HasName("ALLOTMENT_HEADER_PK");

            entity.ToTable("ALLOTMENT$HEADER");

            entity.HasIndex(e => e.AgentContactNameId)
                .HasName("AH_AGENT_CONTACT_NAME_ID_IND");

            entity.HasIndex(e => e.AgentNameId)
                .HasName("AH_AGENT_NAME_ID_IND");

            entity.HasIndex(e => e.AllotmentCode)
                .HasName("AH_ALLOTMENT_CODE_IDX");

            entity.HasIndex(e => e.BookingId)
                .HasName("ALLOTMENT_HEADER_IND2");

            entity.HasIndex(e => e.BookingStatusOrder)
                .HasName("AH_BOOKING_STATUS_IDX");

            entity.HasIndex(e => e.CompanyNameId)
                .HasName("AH_COMPANY_NAME_ID_IND");

            entity.HasIndex(e => e.ContactNameId)
                .HasName("AH_CONTACT_NAME_ID_IND");

            entity.HasIndex(e => e.DueDateOrd)
                .HasName("AH_DUE_DATE_IDX");

            entity.HasIndex(e => e.GuaranteeCode)
                .HasName("GUARANTEE_CODES_FK_IDX");

            entity.HasIndex(e => e.InsertDate)
                .HasName("AH_INSERT_DATE_IND");

            entity.HasIndex(e => e.InsertUser)
                .HasName("AH_INSERT_USER_IND");

            entity.HasIndex(e => e.IsacOpptyId)
                .HasName("AH_ISAC_OPPTY_IDX");

            entity.HasIndex(e => e.MarketCode)
                .HasName("ALLOTMENT_HEADER_MARKET_IND");

            entity.HasIndex(e => e.MasterNameId)
                .HasName("AH_MASTER_NAME_ID_IND");

            entity.HasIndex(e => e.Owner)
                .HasName("AH_OWNER_IDX");

            entity.HasIndex(e => e.OwnerCode)
                .HasName("AH_OWNER_CODE_IDX");

            entity.HasIndex(e => e.RateCode)
                .HasName("AH_RATE_CODE_IND");

            entity.HasIndex(e => e.SourceNameId)
                .HasName("AH_SOURCE_NAME_ID_IND");

            entity.HasIndex(e => e.Tourcode)
                .HasName("AH_TOURCODE_IDX");

            entity.HasIndex(e => e.Udescription)
                .HasName("ALLOTMENT_HEADER_DESC_SRCH");

            entity.HasIndex(e => e.Xudescription)
                .HasName("AH_UDESC_IDX");

            entity.HasIndex(e => new { e.BeginDate, e.EndDate })
                .HasName("ALLOTMENT_HEADER_IND3");

            entity.HasIndex(e => new { e.EndDate, e.Resort })
                .HasName("AH_END_DATE");

            entity.HasIndex(e => new { e.Resort, e.UpdateDate })
                .HasName("AH_UPDATE_DATE_IDX");

            entity.HasIndex(e => new { e.SuperBlockId, e.SuperBlockResort })
                .HasName("AH_SUPER_IDX");

            entity.HasIndex(e => new { e.Resort, e.BlockType, e.OrmsBlockClass })
                .HasName("AH_ORMS_TPCLS_IDX");

            entity.HasIndex(e => new { e.Resort, e.EndDate, e.BeginDate, e.BookingStatus })
                .HasName("ALLOTMENT_HEADER_IND1");

            entity.Property(e => e.AllotmentHeaderId)
                .HasColumnName("ALLOTMENT_HEADER_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Resort)
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ActionId)
                .HasColumnName("ACTION_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.AgentContactNameId)
                .HasColumnName("AGENT_CONTACT_NAME_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AgentNameId)
                .HasColumnName("AGENT_NAME_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Alias)
                .HasColumnName("ALIAS")
                .HasMaxLength(2000)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AllotmentClassification)
                .HasColumnName("ALLOTMENT_CLASSIFICATION")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.AllotmentCode)
                .IsRequired()
                .HasColumnName("ALLOTMENT_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AllotmentOrigion)
                .HasColumnName("ALLOTMENT_ORIGION")
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql(@"'PMS'");

            entity.Property(e => e.AllotmentType)
                .IsRequired()
                .HasColumnName("ALLOTMENT_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AllowAlternateDatesYn)
                .HasColumnName("ALLOW_ALTERNATE_DATES_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ArrivalTime)
                .HasColumnName("ARRIVAL_TIME")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AttachmentUrl)
                .HasColumnName("ATTACHMENT_URL")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.Attendees)
                .HasColumnName("ATTENDEES")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AutoLoadForecastYn)
                .HasColumnName("AUTO_LOAD_FORECAST_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.AverageRate)
                .HasColumnName("AVERAGE_RATE")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AvgPeoplePerRoom)
                .HasColumnName("AVG_PEOPLE_PER_ROOM")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BeginDate)
                .HasColumnName("BEGIN_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BeginDateOriginal)
                .HasColumnName("BEGIN_DATE_ORIGINAL")
                .HasColumnType("DATE");

            entity.Property(e => e.BeoLastPrint)
                .HasColumnName("BEO_LAST_PRINT")
                .HasColumnType("DATE");

            entity.Property(e => e.BfstDesc)
                .HasColumnName("BFST_DESC")
                .HasMaxLength(40)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BfstInclPrice)
                .HasColumnName("BFST_INCL_PRICE")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BfstInclYn)
                .HasColumnName("BFST_INCL_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BfstPrice)
                .HasColumnName("BFST_PRICE")
                .HasColumnType("NUMBER(9,2)")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BfstYn)
                .HasColumnName("BFST_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BlockType)
                .HasColumnName("BLOCK_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BookingId)
                .HasColumnName("BOOKING_ID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BookingStatus)
                .IsRequired()
                .HasColumnName("BOOKING_STATUS")
                .HasMaxLength(10)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BookingStatusOrder)
                .HasColumnName("BOOKING_STATUS_ORDER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Bookingmethod)
                .HasColumnName("BOOKINGMETHOD")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Bookingtype)
                .HasColumnName("BOOKINGTYPE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BwiLeadId)
                .HasColumnName("BWI_LEAD_ID")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.BwiUrl)
                .HasColumnName("BWI_URL")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.CancelRule)
                .HasColumnName("CANCEL_RULE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CancellationCode)
                .HasColumnName("CANCELLATION_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CancellationDate)
                .HasColumnName("CANCELLATION_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CancellationDesc)
                .HasColumnName("CANCELLATION_DESC")
                .HasMaxLength(2000)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CancellationNo)
                .HasColumnName("CANCELLATION_NO")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatCanxCode)
                .HasColumnName("CAT_CANX_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatCanxDate)
                .HasColumnName("CAT_CANX_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatCanxDesc)
                .HasColumnName("CAT_CANX_DESC")
                .HasMaxLength(2000)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatCanxNo)
                .HasColumnName("CAT_CANX_NO")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatCurrency)
                .HasColumnName("CAT_CURRENCY")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.CatCutoff)
                .HasColumnName("CAT_CUTOFF")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatDecision)
                .HasColumnName("CAT_DECISION")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatExchange)
                .HasColumnName("CAT_EXCHANGE")
                .HasColumnType("NUMBER(9,2)")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatFollowup)
                .HasColumnName("CAT_FOLLOWUP")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatItemDiscount)
                .HasColumnName("CAT_ITEM_DISCOUNT")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CatOwner)
                .HasColumnName("CAT_OWNER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatOwnerCode)
                .HasColumnName("CAT_OWNER_CODE")
                .HasMaxLength(10)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatOwnerResort)
                .HasColumnName("CAT_OWNER_RESORT")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatQuoteCurr)
                .HasColumnName("CAT_QUOTE_CURR")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CatStatus)
                .HasColumnName("CAT_STATUS")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CateringPkgsYn)
                .HasColumnName("CATERING_PKGS_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CateringonlyYn)
                .HasColumnName("CATERINGONLY_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql(@"'N'");

            entity.Property(e => e.CentralOwner)
                .HasColumnName("CENTRAL_OWNER")
                .HasMaxLength(2000)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Channel)
                .HasColumnName("CHANNEL")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ComAddress1)
                .HasColumnName("COM_ADDRESS1")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.ComAddress2)
                .HasColumnName("COM_ADDRESS2")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.ComAddress3)
                .HasColumnName("COM_ADDRESS3")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.ComMethod1)
                .HasColumnName("COM_METHOD1")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ComMethod2)
                .HasColumnName("COM_METHOD2")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ComMethod3)
                .HasColumnName("COM_METHOD3")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Commission)
                .HasColumnName("COMMISSION")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CommissionablePerc)
                .HasColumnName("COMMISSIONABLE_PERC")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CommissionableYn)
                .HasColumnName("COMMISSIONABLE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.CompPerStayYn)
                .HasColumnName("COMP_PER_STAY_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.CompRoomValue)
                .HasColumnName("COMP_ROOM_VALUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CompRooms)
                .HasColumnName("COMP_ROOMS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.CompRoomsFixedYn)
                .HasColumnName("COMP_ROOMS_FIXED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.CompanyNameId)
                .HasColumnName("COMPANY_NAME_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Competition)
                .HasColumnName("COMPETITION")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.ContactNameId)
                .HasColumnName("CONTACT_NAME_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ContractNr)
                .HasColumnName("CONTRACT_NR")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ControlBlockYn)
                .HasColumnName("CONTROL_BLOCK_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ConversionCode)
                .HasColumnName("CONVERSION_CODE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.CreditCardId)
                .HasColumnName("CREDIT_CARD_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CrsGtdYn)
                .HasColumnName("CRS_GTD_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.CurrencyCode)
                .HasColumnName("CURRENCY_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CxlPenalty)
                .HasColumnName("CXL_PENALTY")
                .HasColumnType("NUMBER");

            entity.Property(e => e.DateAcl)
                .HasColumnName("DATE_ACL")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DateCfl)
                .HasColumnName("DATE_CFL")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DateLsl)
                .HasColumnName("DATE_LSL")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DateOpenedForPickup)
                .HasColumnName("DATE_OPENED_FOR_PICKUP")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DatePel)
                .HasColumnName("DATE_PEL")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DateTdl)
                .HasColumnName("DATE_TDL")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DblRmSupplementPrice)
                .HasColumnName("DBL_RM_SUPPLEMENT_PRICE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.DblRmSupplementYn)
                .HasColumnName("DBL_RM_SUPPLEMENT_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.DefaultPmResvNameId)
                .HasColumnName("DEFAULT_PM_RESV_NAME_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.DepartureTime)
                .HasColumnName("DEPARTURE_TIME")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Destination)
                .HasColumnName("DESTINATION")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.DetailsOkYn)
                .HasColumnName("DETAILS_OK_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DistributedDate)
                .HasColumnName("DISTRIBUTED_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.DistributedYn)
                .HasColumnName("DISTRIBUTED_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DmlSeqNo)
                .HasColumnName("DML_SEQ_NO")
                .HasColumnType("NUMBER");

            entity.Property(e => e.DownloadDate)
                .HasColumnName("DOWNLOAD_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.DownloadResort)
                .HasColumnName("DOWNLOAD_RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.DownloadSrep)
                .HasColumnName("DOWNLOAD_SREP")
                .HasColumnType("NUMBER");

            entity.Property(e => e.DueDate)
                .HasColumnName("DUE_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DueDateOrd)
                .HasColumnName("DUE_DATE_ORD")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Elastic)
                .HasColumnName("ELASTIC")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.EndDate)
                .HasColumnName("END_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.EndDateOriginal)
                .HasColumnName("END_DATE_ORIGINAL")
                .HasColumnType("DATE");

            entity.Property(e => e.EventsGuaranteedYn)
                .HasColumnName("EVENTS_GUARANTEED_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ExchangePostingType)
                .HasColumnName("EXCHANGE_POSTING_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ExchangeRate)
                .HasColumnName("EXCHANGE_RATE")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ExclusionMessage)
                .HasColumnName("EXCLUSION_MESSAGE")
                .IsUnicode(false);

            entity.Property(e => e.ExternalLocked)
                .HasColumnName("EXTERNAL_LOCKED")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ExternalReference)
                .HasColumnName("EXTERNAL_REFERENCE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.FbAgendaCurr)
                .HasColumnName("FB_AGENDA_CURR")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.FbCommission1)
                .HasColumnName("FB_COMMISSION_1")
                .HasColumnType("NUMBER");

            entity.Property(e => e.FbCommission2)
                .HasColumnName("FB_COMMISSION_2")
                .HasColumnType("NUMBER");

            entity.Property(e => e.FitContractMode)
                .HasColumnName("FIT_CONTRACT_MODE")
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.Property(e => e.FitDiscountLevel)
                .HasColumnName("FIT_DISCOUNT_LEVEL")
                .HasColumnType("NUMBER");

            entity.Property(e => e.FitDiscountPerc)
                .HasColumnName("FIT_DISCOUNT_PERC")
                .HasColumnType("NUMBER");

            entity.Property(e => e.FitDiscountType)
                .HasColumnName("FIT_DISCOUNT_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.FlatRateYn)
                .HasColumnName("FLAT_RATE_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Functiontype)
                .HasColumnName("FUNCTIONTYPE")
                .HasMaxLength(60)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.GreekContractNr)
                .HasColumnName("GREEK_CONTRACT_NR")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.GuaranteeCode)
                .HasColumnName("GUARANTEE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.HideAccInfoYn)
                .HasColumnName("HIDE_ACC_INFO_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.HlxCanxNoticeDays)
                .HasColumnName("HLX_CANX_NOTICE_DAYS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.HlxCommissionableYn)
                .HasColumnName("HLX_COMMISSIONABLE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.HlxDdSecuredYn)
                .HasColumnName("HLX_DD_SECURED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.HlxDepositDays)
                .HasColumnName("HLX_DEPOSIT_DAYS")
                .HasColumnType("NUMBER");

            entity.Property(e => e.HlxDiSecuredYn)
                .HasColumnName("HLX_DI_SECURED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.HlxHousinginfoSecuredYn)
                .HasColumnName("HLX_HOUSINGINFO_SECURED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.HlxRateAllSecuredYn)
                .HasColumnName("HLX_RATE_ALL_SECURED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.HlxRatesGnrSecuredYn)
                .HasColumnName("HLX_RATES_GNR_SECURED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.HlxReturnEachDayYn)
                .HasColumnName("HLX_RETURN_EACH_DAY_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Info)
                .HasColumnName("INFO")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.Infoboard)
                .HasColumnName("INFOBOARD")
                .HasMaxLength(50)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InvCutoffDate)
                .HasColumnName("INV_CUTOFF_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InvCutoffDays)
                .HasColumnName("INV_CUTOFF_DAYS")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.IsacOpptyId)
                .HasColumnName("ISAC_OPPTY_ID")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.KeepLeadControlYn)
                .HasColumnName("KEEP_LEAD_CONTROL_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.LaptopChange)
                .HasColumnName("LAPTOP_CHANGE")
                .HasColumnType("NUMBER(2)");

            entity.Property(e => e.LeadError)
                .HasColumnName("LEAD_ERROR")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.LeadNewYn)
                .HasColumnName("LEAD_NEW_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.LeadOrigin)
                .HasColumnName("LEAD_ORIGIN")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.LeadReceivedYn)
                .HasColumnName("LEAD_RECEIVED_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.LeadSource)
                .HasColumnName("LEAD_SOURCE")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.LeadType)
                .HasColumnName("LEAD_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.LeadchangeBypropertyYn)
                .HasColumnName("LEADCHANGE_BYPROPERTY_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Leadsend1)
                .HasColumnName("LEADSEND1")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.Leadsend2)
                .HasColumnName("LEADSEND2")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.Leadsend3)
                .HasColumnName("LEADSEND3")
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.Leadstatus)
                .HasColumnName("LEADSTATUS")
                .HasMaxLength(10)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.LinkDate)
                .HasColumnName("LINK_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.LostToProperty)
                .HasColumnName("LOST_TO_PROPERTY")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Mainmarket)
                .HasColumnName("MAINMARKET")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ManualCutoff)
                .HasColumnName("MANUAL_CUTOFF")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.MarketCode)
                .HasColumnName("MARKET_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.MasterNameId)
                .HasColumnName("MASTER_NAME_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.MethodDue)
                .HasColumnName("METHOD_DUE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.MtgBudget)
                .HasColumnName("MTG_BUDGET")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MtgRevenue)
                .HasColumnName("MTG_REVENUE")
                .HasColumnType("NUMBER");

            entity.Property(e => e.NonCompete)
                .HasColumnName("NON_COMPETE")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.NonCompeteCode)
                .HasColumnName("NON_COMPETE_CODE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.OffsetType)
                .HasColumnName("OFFSET_TYPE")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.OrigAllotmentHeaderId)
                .HasColumnName("ORIG_ALLOTMENT_HEADER_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OriginalBeginDate)
                .HasColumnName("ORIGINAL_BEGIN_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OriginalBeginDateHolidex)
                .HasColumnName("ORIGINAL_BEGIN_DATE_HOLIDEX")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OriginalEndDate)
                .HasColumnName("ORIGINAL_END_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OriginalRateCode)
                .HasColumnName("ORIGINAL_RATE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OrmsBlockClass)
                .HasColumnName("ORMS_BLOCK_CLASS")
                .HasMaxLength(80)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OrmsFinalBlock)
                .HasColumnName("ORMS_FINAL_BLOCK")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OrmsTransientBlock)
                .HasColumnName("ORMS_TRANSIENT_BLOCK")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Owner)
                .HasColumnName("OWNER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OwnerCode)
                .IsRequired()
                .HasColumnName("OWNER_CODE")
                .HasMaxLength(10)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OwnerResort)
                .HasColumnName("OWNER_RESORT")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PaymentMethod)
                .HasColumnName("PAYMENT_METHOD")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PendingSendYn)
                .HasColumnName("PENDING_SEND_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PorteragePrice)
                .HasColumnName("PORTERAGE_PRICE")
                .HasColumnType("NUMBER(9,2)")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PorterageYn)
                .HasColumnName("PORTERAGE_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PotFbRevenue)
                .HasColumnName("POT_FB_REVENUE")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PotOtherRevenue)
                .HasColumnName("POT_OTHER_REVENUE")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PotRoomNights)
                .HasColumnName("POT_ROOM_NIGHTS")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PotRoomRevenue)
                .HasColumnName("POT_ROOM_REVENUE")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PrintRateYn)
                .HasColumnName("PRINT_RATE_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ProfileId)
                .HasColumnName("PROFILE_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Program)
                .HasColumnName("PROGRAM")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.ProposalCombineEventsYn)
                .HasColumnName("PROPOSAL_COMBINE_EVENTS_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ProposalDecisionSelection)
                .HasColumnName("PROPOSAL_DECISION_SELECTION")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ProposalFollowupSelection)
                .HasColumnName("PROPOSAL_FOLLOWUP_SELECTION")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ProposalInclAltNamesYn)
                .HasColumnName("PROPOSAL_INCL_ALT_NAMES_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ProposalOwnerSelection)
                .HasColumnName("PROPOSAL_OWNER_SELECTION")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ProposalSentDate)
                .HasColumnName("PROPOSAL_SENT_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ProposalShowEventpriceYn)
                .HasColumnName("PROPOSAL_SHOW_EVENTPRICE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ProposalShowPmsRoomTypeYn)
                .HasColumnName("PROPOSAL_SHOW_PMS_ROOM_TYPE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ProposalShowSpacenameYn)
                .HasColumnName("PROPOSAL_SHOW_SPACENAME_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ProposalSpaceMeasurement)
                .HasColumnName("PROPOSAL_SPACE_MEASUREMENT")
                .HasMaxLength(8)
                .IsUnicode(false);

            entity.Property(e => e.ProposalViewToken)
                .HasColumnName("PROPOSAL_VIEW_TOKEN")
                .HasMaxLength(2000)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PublishRatesYn)
                .HasColumnName("PUBLISH_RATES_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RankingCode)
                .HasColumnName("RANKING_CODE")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.RateCode)
                .HasColumnName("RATE_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RateGuaranteedYn)
                .HasColumnName("RATE_GUARANTEED_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.RateOverride)
                .HasColumnName("RATE_OVERRIDE")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RateOverrideReason)
                .HasColumnName("RATE_OVERRIDE_REASON")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.RateProtection)
                .HasColumnName("RATE_PROTECTION")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.RegeneratedLeadYn)
                .HasColumnName("REGENERATED_LEAD_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.ReplyBy)
                .HasColumnName("REPLY_BY")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ReplyDate)
                .HasColumnName("REPLY_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ReplyStatus)
                .HasColumnName("REPLY_STATUS")
                .HasMaxLength(10)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ReplyVia)
                .HasColumnName("REPLY_VIA")
                .HasMaxLength(10)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Representative)
                .HasColumnName("REPRESENTATIVE")
                .HasMaxLength(40)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ReserveInventoryYn)
                .HasColumnName("RESERVE_INVENTORY_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ResortBooked)
                .HasColumnName("RESORT_BOOKED")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.RespTime)
                .HasColumnName("RESP_TIME")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RespTimeCode)
                .HasColumnName("RESP_TIME_CODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RivMarketSegment)
                .HasColumnName("RIV_MARKET_SEGMENT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.RmCommission1)
                .HasColumnName("RM_COMMISSION_1")
                .HasColumnType("NUMBER");

            entity.Property(e => e.RmCommission2)
                .HasColumnName("RM_COMMISSION_2")
                .HasColumnType("NUMBER");

            entity.Property(e => e.RmsCurrency)
                .HasColumnName("RMS_CURRENCY")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RmsDecision)
                .HasColumnName("RMS_DECISION")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RmsExchange)
                .HasColumnName("RMS_EXCHANGE")
                .HasColumnType("NUMBER(9,2)")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RmsFollowup)
                .HasColumnName("RMS_FOLLOWUP")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RmsOwner)
                .HasColumnName("RMS_OWNER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RmsOwnerCode)
                .HasColumnName("RMS_OWNER_CODE")
                .HasMaxLength(10)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RmsOwnerResort)
                .HasColumnName("RMS_OWNER_RESORT")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RmsQuoteCurr)
                .HasColumnName("RMS_QUOTE_CURR")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RoomingListRules)
                .HasColumnName("ROOMING_LIST_RULES")
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.RoomsPerDay)
                .HasColumnName("ROOMS_PER_DAY")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SalesId)
                .HasColumnName("SALES_ID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ScQuoteId)
                .HasColumnName("SC_QUOTE_ID")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.SellThruYn)
                .HasColumnName("SELL_THRU_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.SendToCentralYn)
                .HasColumnName("SEND_TO_CENTRAL_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.SentBy)
                .HasColumnName("SENT_BY")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SentDate)
                .HasColumnName("SENT_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SentVia)
                .HasColumnName("SENT_VIA")
                .HasMaxLength(10)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SentYn)
                .HasColumnName("SENT_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ServiceCharge)
                .HasColumnName("SERVICE_CHARGE")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ServiceFee)
                .HasColumnName("SERVICE_FEE")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ServiceFeeYn)
                .HasColumnName("SERVICE_FEE_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ServiceInclYn)
                .HasColumnName("SERVICE_INCL_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ServicePerc)
                .HasColumnName("SERVICE_PERC")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ShoulderBeginDate)
                .HasColumnName("SHOULDER_BEGIN_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ShoulderEndDate)
                .HasColumnName("SHOULDER_END_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ShowRateAmountYn)
                .HasColumnName("SHOW_RATE_AMOUNT_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SnapshotSetup)
                .HasColumnName("SNAPSHOT_SETUP")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.Source)
                .HasColumnName("SOURCE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SourceNameId)
                .HasColumnName("SOURCE_NAME_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Status)
                .HasColumnName("STATUS")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SubAllotmentYn)
                .HasColumnName("SUB_ALLOTMENT_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SuperBlockId)
                .HasColumnName("SUPER_BLOCK_ID")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SuperBlockResort)
                .HasColumnName("SUPER_BLOCK_RESORT")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SuperSearchIndexText)
                .HasColumnName("SUPER_SEARCH_INDEX_TEXT")
                .HasMaxLength(100)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SyncContractYn)
                .HasColumnName("SYNC_CONTRACT_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.SynchronizeYn)
                .HasColumnName("SYNCHRONIZE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.TaRecordLocator)
                .HasColumnName("TA_RECORD_LOCATOR")
                .HasMaxLength(60)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.TaxAmount)
                .HasColumnName("TAX_AMOUNT")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.TaxIncludedPerc)
                .HasColumnName("TAX_INCLUDED_PERC")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.TaxIncludedYn)
                .HasColumnName("TAX_INCLUDED_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.TaxType)
                .HasColumnName("TAX_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.TbdRates)
                .HasColumnName("TBD_RATES")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.TdlReason)
                .HasColumnName("TDL_REASON")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.TentativeLevel)
                .HasColumnName("TENTATIVE_LEVEL")
                .HasColumnType("NUMBER");

            entity.Property(e => e.TlpResponseid)
                .HasColumnName("TLP_RESPONSEID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TlpUrl)
                .HasColumnName("TLP_URL")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.Tourcode)
                .HasColumnName("TOURCODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Tracecode)
                .HasColumnName("TRACECODE")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Ualias)
                .HasColumnName("UALIAS")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.Udescription)
                .HasColumnName("UDESCRIPTION")
                .HasMaxLength(80)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfc01)
                .HasColumnName("UDFC01")
                .HasMaxLength(200)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfc02)
                .HasColumnName("UDFC02")
                .HasMaxLength(200)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfc03)
                .HasColumnName("UDFC03")
                .HasMaxLength(200)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfc04)
                .HasColumnName("UDFC04")
                .HasMaxLength(200)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfc05)
                .HasColumnName("UDFC05")
                .HasMaxLength(200)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfc06)
                .HasColumnName("UDFC06")
                .HasMaxLength(200)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfc07)
                .HasColumnName("UDFC07")
                .HasMaxLength(200)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfc08)
                .HasColumnName("UDFC08")
                .HasMaxLength(200)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfc09)
                .HasColumnName("UDFC09")
                .HasMaxLength(200)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfc10)
                .HasColumnName("UDFC10")
                .HasMaxLength(200)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfc11)
                .HasColumnName("UDFC11")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc12)
                .HasColumnName("UDFC12")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc13)
                .HasColumnName("UDFC13")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc14)
                .HasColumnName("UDFC14")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc15)
                .HasColumnName("UDFC15")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc16)
                .HasColumnName("UDFC16")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc17)
                .HasColumnName("UDFC17")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc18)
                .HasColumnName("UDFC18")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc19)
                .HasColumnName("UDFC19")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc20)
                .HasColumnName("UDFC20")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc21)
                .HasColumnName("UDFC21")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc22)
                .HasColumnName("UDFC22")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc23)
                .HasColumnName("UDFC23")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc24)
                .HasColumnName("UDFC24")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc25)
                .HasColumnName("UDFC25")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc26)
                .HasColumnName("UDFC26")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc27)
                .HasColumnName("UDFC27")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc28)
                .HasColumnName("UDFC28")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc29)
                .HasColumnName("UDFC29")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc30)
                .HasColumnName("UDFC30")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc31)
                .HasColumnName("UDFC31")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc32)
                .HasColumnName("UDFC32")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc33)
                .HasColumnName("UDFC33")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc34)
                .HasColumnName("UDFC34")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc35)
                .HasColumnName("UDFC35")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc36)
                .HasColumnName("UDFC36")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc37)
                .HasColumnName("UDFC37")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc38)
                .HasColumnName("UDFC38")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc39)
                .HasColumnName("UDFC39")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfc40)
                .HasColumnName("UDFC40")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Udfd01)
                .HasColumnName("UDFD01")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfd02)
                .HasColumnName("UDFD02")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfd03)
                .HasColumnName("UDFD03")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfd04)
                .HasColumnName("UDFD04")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfd05)
                .HasColumnName("UDFD05")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfd06)
                .HasColumnName("UDFD06")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfd07)
                .HasColumnName("UDFD07")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfd08)
                .HasColumnName("UDFD08")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfd09)
                .HasColumnName("UDFD09")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfd10)
                .HasColumnName("UDFD10")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfd11)
                .HasColumnName("UDFD11")
                .HasColumnType("DATE");

            entity.Property(e => e.Udfd12)
                .HasColumnName("UDFD12")
                .HasColumnType("DATE");

            entity.Property(e => e.Udfd13)
                .HasColumnName("UDFD13")
                .HasColumnType("DATE");

            entity.Property(e => e.Udfd14)
                .HasColumnName("UDFD14")
                .HasColumnType("DATE");

            entity.Property(e => e.Udfd15)
                .HasColumnName("UDFD15")
                .HasColumnType("DATE");

            entity.Property(e => e.Udfd16)
                .HasColumnName("UDFD16")
                .HasColumnType("DATE");

            entity.Property(e => e.Udfd17)
                .HasColumnName("UDFD17")
                .HasColumnType("DATE");

            entity.Property(e => e.Udfd18)
                .HasColumnName("UDFD18")
                .HasColumnType("DATE");

            entity.Property(e => e.Udfd19)
                .HasColumnName("UDFD19")
                .HasColumnType("DATE");

            entity.Property(e => e.Udfd20)
                .HasColumnName("UDFD20")
                .HasColumnType("DATE");

            entity.Property(e => e.Udfn01)
                .HasColumnName("UDFN01")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfn02)
                .HasColumnName("UDFN02")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfn03)
                .HasColumnName("UDFN03")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfn04)
                .HasColumnName("UDFN04")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfn05)
                .HasColumnName("UDFN05")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfn06)
                .HasColumnName("UDFN06")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfn07)
                .HasColumnName("UDFN07")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfn08)
                .HasColumnName("UDFN08")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfn09)
                .HasColumnName("UDFN09")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfn10)
                .HasColumnName("UDFN10")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Udfn11)
                .HasColumnName("UDFN11")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn12)
                .HasColumnName("UDFN12")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn13)
                .HasColumnName("UDFN13")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn14)
                .HasColumnName("UDFN14")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn15)
                .HasColumnName("UDFN15")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn16)
                .HasColumnName("UDFN16")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn17)
                .HasColumnName("UDFN17")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn18)
                .HasColumnName("UDFN18")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn19)
                .HasColumnName("UDFN19")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn20)
                .HasColumnName("UDFN20")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn21)
                .HasColumnName("UDFN21")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn22)
                .HasColumnName("UDFN22")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn23)
                .HasColumnName("UDFN23")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn24)
                .HasColumnName("UDFN24")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn25)
                .HasColumnName("UDFN25")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn26)
                .HasColumnName("UDFN26")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn27)
                .HasColumnName("UDFN27")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn28)
                .HasColumnName("UDFN28")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn29)
                .HasColumnName("UDFN29")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn30)
                .HasColumnName("UDFN30")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn31)
                .HasColumnName("UDFN31")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn32)
                .HasColumnName("UDFN32")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn33)
                .HasColumnName("UDFN33")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn34)
                .HasColumnName("UDFN34")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn35)
                .HasColumnName("UDFN35")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn36)
                .HasColumnName("UDFN36")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn37)
                .HasColumnName("UDFN37")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn38)
                .HasColumnName("UDFN38")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn39)
                .HasColumnName("UDFN39")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Udfn40)
                .HasColumnName("UDFN40")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdateDateExternal)
                .HasColumnName("UPDATE_DATE_EXTERNAL")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UploadDate)
                .HasColumnName("UPLOAD_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.WebBookableYn)
                .HasColumnName("WEB_BOOKABLE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Xdescription)
                .HasColumnName("XDESCRIPTION")
                .HasMaxLength(120)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Xudescription)
                .HasColumnName("XUDESCRIPTION")
                .HasMaxLength(120)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

			if (!types.Contains(typeof(Name)))
				entity.Ignore(e => e.AgentName);
			else
	            entity.HasOne(d => d.AgentName)
	                .WithMany(p => p.AllotmentHeaderAgentName)
	                .HasForeignKey(d => d.AgentNameId)
	                .HasConstraintName("ALLOTMENT_AGENT_NAME_ID");

			if (!types.Contains(typeof(Name)))
				entity.Ignore(e => e.CompanyName);
			else
	            entity.HasOne(d => d.CompanyName)
	                .WithMany(p => p.AllotmentHeaderCompanyName)
	                .HasForeignKey(d => d.CompanyNameId)
	                .HasConstraintName("ALLOTMENT_COMPANY_NAME_ID");

			if (!types.Contains(typeof(Name)))
				entity.Ignore(e => e.MasterName);
			else
	            entity.HasOne(d => d.MasterName)
	                .WithMany(p => p.AllotmentHeaderMasterName)
	                .HasForeignKey(d => d.MasterNameId)
	                .HasConstraintName("ALLOTMENT_MASTER_NAME_ID");

			if (!types.Contains(typeof(Name)))
				entity.Ignore(e => e.SourceName);
			else
	            entity.HasOne(d => d.SourceName)
	                .WithMany(p => p.AllotmentHeaderSourceName)
	                .HasForeignKey(d => d.SourceNameId)
	                .HasConstraintName("ALLOTMENT_SOURCE_NAME_ID");
        
			if (!types.Contains(typeof(AllotmentAlternateDates)))
				entity.Ignore(e => e.AllotmentAlternateDates);

			if (!types.Contains(typeof(AllotmentChanges)))
				entity.Ignore(e => e.AllotmentChanges);

			if (!types.Contains(typeof(AllotmentChangesNet)))
				entity.Ignore(e => e.AllotmentChangesNet);

			if (!types.Contains(typeof(AllotmentCutoffSchedule)))
				entity.Ignore(e => e.AllotmentCutoffSchedule);

			if (!types.Contains(typeof(AllotmentDateProtection)))
				entity.Ignore(e => e.AllotmentDateProtection);

			if (!types.Contains(typeof(AllotmentDetail)))
				entity.Ignore(e => e.AllotmentDetail);

			if (!types.Contains(typeof(AllotmentOffsetRevenueDtl)))
				entity.Ignore(e => e.AllotmentOffsetRevenueDtl);

			if (!types.Contains(typeof(AllotmentOffsetType)))
				entity.Ignore(e => e.AllotmentOffsetType);

			if (!types.Contains(typeof(AllotmentProducts)))
				entity.Ignore(e => e.AllotmentProducts);

			if (!types.Contains(typeof(AllotmentRemoteOwners)))
				entity.Ignore(e => e.AllotmentRemoteOwners);

			if (!types.Contains(typeof(AllotmentRestrictions)))
				entity.Ignore(e => e.AllotmentRestrictions);

			if (!types.Contains(typeof(AllotmentTlgo)))
				entity.Ignore(e => e.AllotmentTlgo);

			if (!types.Contains(typeof(AllotmentTraces)))
				entity.Ignore(e => e.AllotmentTraces);

			if (!types.Contains(typeof(BlockAccessExclusions)))
				entity.Ignore(e => e.BlockAccessExclusions);

			if (!types.Contains(typeof(BookNotes)))
				entity.Ignore(e => e.BookNotes);

			if (!types.Contains(typeof(BookOwner)))
				entity.Ignore(e => e.BookOwner);

			if (!types.Contains(typeof(BookProfileLink)))
				entity.Ignore(e => e.BookProfileLink);

			if (!types.Contains(typeof(BookingAttendees)))
				entity.Ignore(e => e.BookingAttendees);

			if (!types.Contains(typeof(BookingLink)))
				entity.Ignore(e => e.BookingLink);

			if (!types.Contains(typeof(ContractSend)))
				entity.Ignore(e => e.ContractSend);

			if (!types.Contains(typeof(EventChanges)))
				entity.Ignore(e => e.EventChanges);

			if (!types.Contains(typeof(EventReservation)))
				entity.Ignore(e => e.EventReservation);

			if (!types.Contains(typeof(EventStatusChanges)))
				entity.Ignore(e => e.EventStatusChanges);

			if (!types.Contains(typeof(FbAgenda)))
				entity.Ignore(e => e.FbAgenda);

			if (!types.Contains(typeof(FitContractRateDetails)))
				entity.Ignore(e => e.FitContractRateDetails);

			if (!types.Contains(typeof(GemEvent)))
				entity.Ignore(e => e.GemEvent);

			if (!types.Contains(typeof(GemEventgroup)))
				entity.Ignore(e => e.GemEventgroup);

			if (!types.Contains(typeof(Lead)))
				entity.Ignore(e => e.Lead);

			if (!types.Contains(typeof(LeadContacts)))
				entity.Ignore(e => e.LeadContacts);

			if (!types.Contains(typeof(LeadDestination)))
				entity.Ignore(e => e.LeadDestination);

			if (!types.Contains(typeof(LeadHistory)))
				entity.Ignore(e => e.LeadHistory);

			if (!types.Contains(typeof(MeetingRoomInventory)))
				entity.Ignore(e => e.MeetingRoomInventory);
		});
	}
}
