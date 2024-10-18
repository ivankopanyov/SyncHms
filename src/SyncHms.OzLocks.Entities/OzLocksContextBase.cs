namespace SyncHms.OzLocks.Entities;

public abstract class OzLocksContextBase : DbContext
{
    public OzLocksContextBase()
    {
    }

    public OzLocksContextBase(DbContextOptions<OzLocksContextBase> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditUser> AuditUsers { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<DictCatAudit> DictCatAudits { get; set; }

    public virtual DbSet<DictCountry> DictCountries { get; set; }

    public virtual DbSet<DictDocumenttype> DictDocumenttypes { get; set; }

    public virtual DbSet<DictEntrygoal> DictEntrygoals { get; set; }

    public virtual DbSet<DictOfficialorganFm> DictOfficialorganFms { get; set; }

    public virtual DbSet<DictOfficialorganKpp> DictOfficialorganKpps { get; set; }

    public virtual DbSet<DictTimeout> DictTimeouts { get; set; }

    public virtual DbSet<DictTypeActUser> DictTypeActUsers { get; set; }

    public virtual DbSet<DictTypeCard> DictTypeCards { get; set; }

    public virtual DbSet<DictTypePermission> DictTypePermissions { get; set; }

    public virtual DbSet<DictVisacategory> DictVisacategories { get; set; }

    public virtual DbSet<DictVisitpurpose> DictVisitpurposes { get; set; }

    public virtual DbSet<Floor> Floors { get; set; }

    public virtual DbSet<GroupUser> GroupUsers { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<PermToGr> PermToGrs { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomAction> RoomActions { get; set; }

    public virtual DbSet<RoomPhoto> RoomPhotos { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<TemplatesGroup> TemplatesGroups { get; set; }

    public virtual DbSet<Tur> Turs { get; set; }

    public virtual DbSet<TurGr> TurGrs { get; set; }

    public virtual DbSet<TypesRoom> TypesRooms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserToGr> UserToGrs { get; set; }

    public virtual DbSet<Version> Versions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditUser>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActId).HasColumnName("ActID");
            entity.Property(e => e.CatId).HasColumnName("CatID");
            entity.Property(e => e.ObjId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ObjID");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.BuserId).HasColumnName("BUser_ID");
            entity.Property(e => e.Com).HasMaxLength(50);
            entity.Property(e => e.GuestId).HasColumnName("Guest_ID");
            entity.Property(e => e.Prepayment).HasColumnType("money");
            entity.Property(e => e.RoomId).HasColumnName("Room_ID");
            entity.Property(e => e.TimeIn).HasColumnType("datetime");
            entity.Property(e => e.TimeOut).HasColumnType("datetime");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.TurGrId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("TurGr_ID");
            entity.Property(e => e.Used).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.Property(e => e.BuildingId).HasColumnName("Building_ID");
            entity.Property(e => e.BuildingComent)
                .HasMaxLength(256)
                .HasColumnName("Building_Coment");
            entity.Property(e => e.BuildingName)
                .HasMaxLength(20)
                .HasColumnName("Building_NAME");
            entity.Property(e => e.BuildingNum).HasColumnName("Building_Num");
            entity.Property(e => e.BuserId).HasColumnName("BUser_ID");
            entity.Property(e => e.CanDelete).HasDefaultValueSql("((1))");
            entity.Property(e => e.Hidden).HasDefaultValueSql("((0))");
            entity.Property(e => e.HotelId).HasColumnName("Hotel_ID");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.Property(e => e.CardId).HasColumnName("Card_ID");
            entity.Property(e => e.BuildingId).HasColumnName("Building_ID");
            entity.Property(e => e.FloorId).HasColumnName("Floor_ID");
            entity.Property(e => e.RoomId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Room_ID");
            entity.Property(e => e.SerialNo).HasMaxLength(10);
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.Uid)
                .HasMaxLength(20)
                .HasColumnName("UID");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
        });

        modelBuilder.Entity<DictCatAudit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CatAudit");

            entity.ToTable("dict_CatAudit");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<DictCountry>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dict_country");

            entity.Property(e => e.ActualStatus).HasMaxLength(50);
            entity.Property(e => e.CodeAlpha3)
                .HasMaxLength(3)
                .HasColumnName("Code_Alpha3");
            entity.Property(e => e.Name).HasMaxLength(80);
            entity.Property(e => e.NameLat).HasMaxLength(80);
        });

        modelBuilder.Entity<DictDocumenttype>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dict_documenttype");

            entity.Property(e => e.DocType).HasMaxLength(150);
            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .HasColumnName("ID");
            entity.Property(e => e.State).HasMaxLength(10);
        });

        modelBuilder.Entity<DictEntrygoal>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dict_entrygoal");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.VisaCategoryId)
                .HasMaxLength(10)
                .HasColumnName("VisaCategory_ID");
        });

        modelBuilder.Entity<DictOfficialorganFm>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dict_officialorgan_fms");

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.EndDate).HasMaxLength(30);
            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(450);
        });

        modelBuilder.Entity<DictOfficialorganKpp>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dict_officialorgan_kpp");

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.EndDate)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("End_Date");
            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<DictTimeout>(entity =>
        {
            entity.ToTable("dict_timeout");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TimeDay).HasDefaultValueSql("((0))");
            entity.Property(e => e.TimeHour).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<DictTypeActUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TypeActUsers");

            entity.ToTable("dict_TypeActUsers");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CatId).HasColumnName("CatID");
            entity.Property(e => e.NameAction).HasMaxLength(50);
        });

        modelBuilder.Entity<DictTypeCard>(entity =>
        {
            entity.HasKey(e => e.TypeCardId).HasName("PK_TypeCard");

            entity.ToTable("dict_TypeCard");

            entity.Property(e => e.TypeCardId)
                .ValueGeneratedNever()
                .HasColumnName("TypeCardID");
            entity.Property(e => e.TypeCardName).HasMaxLength(30);
        });

        modelBuilder.Entity<DictTypePermission>(entity =>
        {
            entity.HasKey(e => e.TpermissionId).HasName("PK_Type_Permissions");

            entity.ToTable("dict_Type_Permissions");

            entity.Property(e => e.TpermissionId).HasColumnName("TPermission_ID");
            entity.Property(e => e.Descr).HasMaxLength(50);
            entity.Property(e => e.PermissionName)
                .HasMaxLength(30)
                .HasColumnName("Permission_Name");
            entity.Property(e => e.Visible).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<DictVisacategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dict_visacategory");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Status).HasMaxLength(10);
        });

        modelBuilder.Entity<DictVisitpurpose>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dict_visitpurpose");

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(10);
        });

        modelBuilder.Entity<Floor>(entity =>
        {
            entity.Property(e => e.FloorId).HasColumnName("Floor_ID");
            entity.Property(e => e.BuildingId).HasColumnName("Building_ID");
            entity.Property(e => e.CanDelete).HasDefaultValueSql("((1))");
            entity.Property(e => e.FloorComent)
                .HasMaxLength(256)
                .HasColumnName("Floor_Coment");
            entity.Property(e => e.FloorName)
                .HasMaxLength(10)
                .HasColumnName("Floor_NAME");
            entity.Property(e => e.FloorNum).HasColumnName("Floor_Num");
            entity.Property(e => e.FuserId).HasColumnName("FUser_ID");
            entity.Property(e => e.Hidden).HasDefaultValueSql("((0))");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<GroupUser>(entity =>
        {
            entity.HasKey(e => e.GroupId);

            entity.ToTable("Group_Users");

            entity.Property(e => e.GroupId).HasColumnName("Group_ID");
            entity.Property(e => e.CanDelete).HasDefaultValueSql("((1))");
            entity.Property(e => e.GroupName)
                .HasMaxLength(50)
                .HasColumnName("Group_Name");
            entity.Property(e => e.Hidden).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.Property(e => e.GuestId).HasColumnName("Guest_ID");
            entity.Property(e => e.ActionId).HasColumnName("Action_ID");
            entity.Property(e => e.BirthArea).HasMaxLength(50);
            entity.Property(e => e.BirthCity).HasMaxLength(50);
            entity.Property(e => e.BirthCountryId)
                .HasMaxLength(6)
                .HasColumnName("BirthCountryID");
            entity.Property(e => e.BirthDay).HasMaxLength(10);
            entity.Property(e => e.BirthPoint).HasMaxLength(50);
            entity.Property(e => e.BirthRegion).HasMaxLength(50);
            entity.Property(e => e.CardHanded).HasDefaultValueSql("((0))");
            entity.Property(e => e.CardHandedTime)
                .HasColumnType("datetime")
                .HasColumnName("CardHanded_Time");
            entity.Property(e => e.CardId).HasColumnName("Card_ID");
            entity.Property(e => e.CardIssued).HasDefaultValueSql("((0))");
            entity.Property(e => e.CardIssuedTime)
                .HasColumnType("datetime")
                .HasColumnName("CardIssued_Time");
            entity.Property(e => e.CitizenshipId)
                .HasMaxLength(6)
                .HasColumnName("CitizenshipID");
            entity.Property(e => e.DocIssuedDate).HasMaxLength(10);
            entity.Property(e => e.DocIssuedId)
                .HasMaxLength(7)
                .HasColumnName("DocIssuedID");
            entity.Property(e => e.DocNo).HasMaxLength(10);
            entity.Property(e => e.DocSeries).HasMaxLength(5);
            entity.Property(e => e.DocValidity).HasMaxLength(10);
            entity.Property(e => e.EntryGoalId)
                .HasMaxLength(10)
                .HasColumnName("EntryGoalID");
            entity.Property(e => e.Exported).HasDefaultValueSql("((0))");
            entity.Property(e => e.Fam)
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.Foreigner).HasDefaultValueSql("((0))");
            entity.Property(e => e.GuserId).HasColumnName("GUser_ID");
            entity.Property(e => e.InRoom).HasDefaultValueSql("((0))");
            entity.Property(e => e.LrbirthDay)
                .HasMaxLength(10)
                .HasColumnName("LRBirthDay");
            entity.Property(e => e.LrdocIssueDate)
                .HasMaxLength(10)
                .HasColumnName("LRDocIssueDate");
            entity.Property(e => e.LrdocNum)
                .HasMaxLength(10)
                .HasColumnName("LRDocNum");
            entity.Property(e => e.LrdocSeries)
                .HasMaxLength(4)
                .HasColumnName("LRDocSeries");
            entity.Property(e => e.Lrfam)
                .HasMaxLength(50)
                .HasColumnName("LRFam");
            entity.Property(e => e.Lrname)
                .HasMaxLength(50)
                .HasColumnName("LRName");
            entity.Property(e => e.Lrotch)
                .HasMaxLength(50)
                .HasColumnName("LROtch");
            entity.Property(e => e.LrtypeDocId)
                .HasMaxLength(10)
                .HasColumnName("LRTypeDocID");
            entity.Property(e => e.McdateIn)
                .HasMaxLength(10)
                .HasColumnName("MCDateIn");
            entity.Property(e => e.McdateOut)
                .HasMaxLength(10)
                .HasColumnName("MCDateOut");
            entity.Property(e => e.MckppId)
                .HasMaxLength(10)
                .HasColumnName("MCKppID");
            entity.Property(e => e.Mcnum)
                .HasMaxLength(10)
                .HasColumnName("MCNum");
            entity.Property(e => e.Mcseries)
                .HasMaxLength(4)
                .HasColumnName("MCSeries");
            entity.Property(e => e.MigProgCode)
                .HasMaxLength(10)
                .HasColumnName("Mig_progCode");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.Otch)
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.ProfessionId)
                .HasMaxLength(10)
                .HasColumnName("Profession_ID");
            entity.Property(e => e.RegApartment).HasMaxLength(10);
            entity.Property(e => e.RegArea).HasMaxLength(50);
            entity.Property(e => e.RegBuilding).HasMaxLength(10);
            entity.Property(e => e.RegBuilding2).HasMaxLength(10);
            entity.Property(e => e.RegCity).HasMaxLength(50);
            entity.Property(e => e.RegPoint).HasMaxLength(50);
            entity.Property(e => e.RegRegion).HasMaxLength(50);
            entity.Property(e => e.RegStreet).HasMaxLength(50);
            entity.Property(e => e.ResApartment).HasMaxLength(10);
            entity.Property(e => e.ResArea).HasMaxLength(50);
            entity.Property(e => e.ResBuilding).HasMaxLength(10);
            entity.Property(e => e.ResBuilding2).HasMaxLength(10);
            entity.Property(e => e.ResCity).HasMaxLength(50);
            entity.Property(e => e.ResPoint).HasMaxLength(50);
            entity.Property(e => e.ResRegion).HasMaxLength(50);
            entity.Property(e => e.ResStreet).HasMaxLength(50);
            entity.Property(e => e.RoomId).HasColumnName("Room_ID");
            entity.Property(e => e.RsdateIn)
                .HasMaxLength(10)
                .HasColumnName("RSDateIn");
            entity.Property(e => e.RsdateIssue)
                .HasMaxLength(10)
                .HasColumnName("RSDateIssue");
            entity.Property(e => e.RsdateOut)
                .HasMaxLength(10)
                .HasColumnName("RSDateOut");
            entity.Property(e => e.RsdocNum)
                .HasMaxLength(10)
                .HasColumnName("RSDocNum");
            entity.Property(e => e.RsdocSeries)
                .HasMaxLength(4)
                .HasColumnName("RSDocSeries");
            entity.Property(e => e.RstypeDocId)
                .HasMaxLength(10)
                .HasColumnName("RSTypeDocID");
            entity.Property(e => e.RstypeVisaCatId)
                .HasMaxLength(10)
                .HasColumnName("RSTypeVisaCatID");
            entity.Property(e => e.RsvisaId)
                .HasMaxLength(10)
                .HasColumnName("RSVisaID");
            entity.Property(e => e.RsvisaMulId)
                .HasMaxLength(10)
                .HasColumnName("RSVisaMulID");
            entity.Property(e => e.Sex)
                .HasMaxLength(2)
                .HasDefaultValueSql("(N'М')");
            entity.Property(e => e.SigurId).HasColumnName("SigurID");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.TurGrId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("TurGr_ID");
            entity.Property(e => e.TypeDocId)
                .HasMaxLength(10)
                .HasColumnName("TypeDoc_ID");
            entity.Property(e => e.VisitPurposeCode).HasMaxLength(10);
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.Property(e => e.HotelId).HasColumnName("Hotel_ID");
            entity.Property(e => e.BankName).HasMaxLength(200);
            entity.Property(e => e.BankName2).HasMaxLength(200);
            entity.Property(e => e.Bik)
                .HasMaxLength(20)
                .HasColumnName("BIK");
            entity.Property(e => e.Fiobuh)
                .HasMaxLength(100)
                .HasColumnName("FIOBUH");
            entity.Property(e => e.Fioruc)
                .HasMaxLength(100)
                .HasColumnName("FIORuc");
            entity.Property(e => e.HotelAdress)
                .HasMaxLength(256)
                .HasColumnName("Hotel_Adress");
            entity.Property(e => e.HotelEmail)
                .HasMaxLength(50)
                .HasColumnName("Hotel_EMail");
            entity.Property(e => e.HotelGlobalId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Hotel_Global_ID");
            entity.Property(e => e.HotelName)
                .HasMaxLength(50)
                .HasColumnName("Hotel_Name");
            entity.Property(e => e.HotelPhone)
                .HasMaxLength(50)
                .HasColumnName("Hotel_Phone");
            entity.Property(e => e.HotelWeb)
                .HasMaxLength(50)
                .HasColumnName("Hotel_Web");
            entity.Property(e => e.HuserId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("HUser_ID");
            entity.Property(e => e.Inn)
                .HasMaxLength(20)
                .HasColumnName("INN");
            entity.Property(e => e.Kor)
                .HasMaxLength(30)
                .HasColumnName("KOR");
            entity.Property(e => e.Kpp)
                .HasMaxLength(20)
                .HasColumnName("KPP");
            entity.Property(e => e.Nds).HasColumnName("NDS");
            entity.Property(e => e.Ogrn)
                .HasMaxLength(20)
                .HasColumnName("OGRN");
            entity.Property(e => e.OverTime1).HasMaxLength(50);
            entity.Property(e => e.OverTime2).HasMaxLength(50);
            entity.Property(e => e.Ras)
                .HasMaxLength(30)
                .HasColumnName("RAS");
            entity.Property(e => e.TimeIn)
                .HasMaxLength(50)
                .HasColumnName("Time_IN");
            entity.Property(e => e.TimeOut)
                .HasMaxLength(50)
                .HasColumnName("Time_OUT");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.UrAdress).HasMaxLength(100);
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PermToGr>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Perm_TO_GR");

            entity.Property(e => e.IdGr).HasColumnName("ID_GR");
            entity.Property(e => e.IdTperm).HasColumnName("ID_TPERM");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.ToTable("prices");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EndPeriod).HasColumnType("datetime");
            entity.Property(e => e.PriceDay).HasColumnType("money");
            entity.Property(e => e.PriceHour).HasColumnType("money");
            entity.Property(e => e.PriceHourOv).HasColumnType("money");
            entity.Property(e => e.StartPeriod).HasColumnType("datetime");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.TypeRoomId).HasColumnName("TypeRoom_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(e => e.RoomId).HasColumnName("Room_ID");
            entity.Property(e => e.BuildingId).HasColumnName("Building_ID");
            entity.Property(e => e.CanDelete).HasDefaultValueSql("((1))");
            entity.Property(e => e.CurActionId).HasColumnName("Cur_Action_ID");
            entity.Property(e => e.FactTimeOut).HasColumnType("datetime");
            entity.Property(e => e.FidelioId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("FidelioID");
            entity.Property(e => e.FloorId).HasColumnName("Floor_ID");
            entity.Property(e => e.Hidden).HasDefaultValueSql("((0))");
            entity.Property(e => e.HotelId).HasColumnName("Hotel_ID");
            entity.Property(e => e.NotCleared).HasDefaultValueSql("((0))");
            entity.Property(e => e.NotUsed).HasDefaultValueSql("((0))");
            entity.Property(e => e.Repair).HasDefaultValueSql("((0))");
            entity.Property(e => e.RoomCom)
                .HasMaxLength(50)
                .HasColumnName("Room_Com");
            entity.Property(e => e.RoomEbed).HasColumnName("Room_EBed");
            entity.Property(e => e.RoomEbedPrice)
                .HasColumnType("money")
                .HasColumnName("Room_EBed_Price");
            entity.Property(e => e.RoomGuestCount)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Room_GuestCount");
            entity.Property(e => e.RoomName)
                .HasMaxLength(10)
                .HasColumnName("Room_Name");
            entity.Property(e => e.RoomNum).HasColumnName("Room_Num");
            entity.Property(e => e.RoomStateId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Room_State_ID");
            entity.Property(e => e.RoomType)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Room_Type");
            entity.Property(e => e.RuserId).HasColumnName("RUser_ID");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.TurGrId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("TurGr_ID");
            entity.Property(e => e.Vip)
                .HasDefaultValueSql("((0))")
                .HasColumnName("VIP");
        });

        modelBuilder.Entity<RoomAction>(entity =>
        {
            entity.HasKey(e => e.ActionId);

            entity.ToTable("RoomAction");

            entity.Property(e => e.ActionId).HasColumnName("Action_ID");
            entity.Property(e => e.AuserId).HasColumnName("AUser_ID");
            entity.Property(e => e.Day1).HasDefaultValueSql("((0))");
            entity.Property(e => e.Day2).HasDefaultValueSql("((0))");
            entity.Property(e => e.DayHour).HasDefaultValueSql("((0))");
            entity.Property(e => e.DayS)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Day_S");
            entity.Property(e => e.FactDeposit).HasColumnType("money");
            entity.Property(e => e.FactMoney).HasColumnType("money");
            entity.Property(e => e.FactTimeOut).HasColumnType("datetime");
            entity.Property(e => e.Hour1).HasDefaultValueSql("((0))");
            entity.Property(e => e.Hour2).HasDefaultValueSql("((0))");
            entity.Property(e => e.HourS)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Hour_S");
            entity.Property(e => e.LastProlongType).HasDefaultValueSql("((0))");
            entity.Property(e => e.RoomId).HasColumnName("Room_ID");
            entity.Property(e => e.Summ).HasColumnType("money");
            entity.Property(e => e.SummS)
                .HasColumnType("money")
                .HasColumnName("Summ_S");
            entity.Property(e => e.Summa1)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money");
            entity.Property(e => e.Summa2)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money");
            entity.Property(e => e.TimeIn).HasColumnType("datetime");
            entity.Property(e => e.TimeIn1).HasColumnType("datetime");
            entity.Property(e => e.TimeIn2).HasColumnType("datetime");
            entity.Property(e => e.TimeOut).HasColumnType("datetime");
            entity.Property(e => e.TimeOut1).HasColumnType("datetime");
            entity.Property(e => e.TimeOut2).HasColumnType("datetime");
            entity.Property(e => e.TimeOutS)
                .HasColumnType("datetime")
                .HasColumnName("TimeOut_S");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.Used).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<RoomPhoto>(entity =>
        {
            entity.ToTable("Room_photo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Com).HasMaxLength(255);
            entity.Property(e => e.Photo).HasColumnType("image");
            entity.Property(e => e.RoomId).HasColumnName("Room_ID");
            entity.Property(e => e.TypeRoomId).HasColumnName("TypeRoom_ID");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.Rtf).HasColumnType("image");
            entity.Property(e => e.TemlateName).HasMaxLength(50);
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<TemplatesGroup>(entity =>
        {
            entity.ToTable("TemplatesGroup");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GroupName).HasMaxLength(50);
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<Tur>(entity =>
        {
            entity.ToTable("Tur");

            entity.Property(e => e.TurId).HasColumnName("Tur_ID");
            entity.Property(e => e.CanDelete).HasDefaultValueSql("((1))");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(30)
                .HasColumnName("Company_Name");
            entity.Property(e => e.Hidden).HasDefaultValueSql("((0))");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.TurAdress)
                .HasMaxLength(50)
                .HasColumnName("Tur_Adress");
            entity.Property(e => e.TurComment)
                .HasMaxLength(50)
                .HasColumnName("Tur_Comment");
            entity.Property(e => e.TurCpemail)
                .HasMaxLength(20)
                .HasColumnName("Tur_CPEmail");
            entity.Property(e => e.TurCpname)
                .HasMaxLength(30)
                .HasColumnName("Tur_CPName");
            entity.Property(e => e.TurCpphone)
                .HasMaxLength(20)
                .HasColumnName("Tur_CPPhone");
            entity.Property(e => e.TurName)
                .HasMaxLength(30)
                .HasColumnName("Tur_Name");
            entity.Property(e => e.TurUserId).HasColumnName("TurUser_ID");
        });

        modelBuilder.Entity<TurGr>(entity =>
        {
            entity.ToTable("TurGr");

            entity.Property(e => e.TurGrId).HasColumnName("TurGr_ID");
            entity.Property(e => e.Booked).HasDefaultValueSql("((0))");
            entity.Property(e => e.CanDelete).HasDefaultValueSql("((1))");
            entity.Property(e => e.Hidden).HasDefaultValueSql("((0))");
            entity.Property(e => e.Out).HasDefaultValueSql("((0))");
            entity.Property(e => e.TgrUserId).HasColumnName("TGR_User_ID");
            entity.Property(e => e.TimeIn).HasColumnType("datetime");
            entity.Property(e => e.TimeOut).HasColumnType("datetime");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.TurGrComment)
                .HasMaxLength(50)
                .HasColumnName("TurGr_Comment");
            entity.Property(e => e.TurGrGidDocNo)
                .HasMaxLength(20)
                .HasColumnName("TurGr_GidDocNo");
            entity.Property(e => e.TurGrGidDocS)
                .HasMaxLength(20)
                .HasColumnName("TurGr_GidDocS");
            entity.Property(e => e.TurGrGidEmail)
                .HasMaxLength(50)
                .HasColumnName("TurGr_GidEmail");
            entity.Property(e => e.TurGrGidName)
                .HasMaxLength(20)
                .HasColumnName("TurGr_GidName");
            entity.Property(e => e.TurGrGidPhone)
                .HasMaxLength(20)
                .HasColumnName("TurGr_GidPhone");
            entity.Property(e => e.TurGrGidTypeDoc).HasColumnName("TurGr_GidTypeDoc");
            entity.Property(e => e.TurGrGuestCount).HasColumnName("TurGr_GuestCount");
            entity.Property(e => e.TurGrGuestIn)
                .HasDefaultValueSql("((0))")
                .HasColumnName("TurGr_GuestIn");
            entity.Property(e => e.TurGrName)
                .HasMaxLength(30)
                .HasColumnName("TurGr_Name");
            entity.Property(e => e.TurGrTur).HasColumnName("TurGr_Tur");
        });

        modelBuilder.Entity<TypesRoom>(entity =>
        {
            entity.HasKey(e => e.TypeRoomId);

            entity.ToTable("Types_Rooms");

            entity.Property(e => e.TypeRoomId).HasColumnName("Type_Room_ID");
            entity.Property(e => e.CanDelete).HasDefaultValueSql("((1))");
            entity.Property(e => e.Hidden).HasDefaultValueSql("((0))");
            entity.Property(e => e.NumRooms).HasDefaultValueSql("((1))");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.TruserId).HasColumnName("TRUser_ID");
            entity.Property(e => e.TypeRoomBed)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Type_Room_Bed");
            entity.Property(e => e.TypeRoomCom)
                .HasMaxLength(256)
                .HasColumnName("Type_Room_Com");
            entity.Property(e => e.TypeRoomDeposit)
                .HasColumnType("money")
                .HasColumnName("Type_Room_Deposit");
            entity.Property(e => e.TypeRoomName)
                .HasMaxLength(100)
                .HasColumnName("Type_Room_Name");
            entity.Property(e => e.TypeRoomPriceDay)
                .HasColumnType("money")
                .HasColumnName("Type_Room_PriceDay");
            entity.Property(e => e.TypeRoomPriceHour)
                .HasColumnType("money")
                .HasColumnName("Type_Room_PriceHour");
            entity.Property(e => e.TypeRoomPriceHourOv)
                .HasColumnType("money")
                .HasColumnName("Type_Room_PriceHourOv");
            entity.Property(e => e.TypeRoomSize).HasColumnName("Type_Room_Size");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.SigurId).HasColumnName("SigurID");
            entity.Property(e => e.UcanDelete)
                .HasDefaultValueSql("((1))")
                .HasColumnName("UCanDelete");
            entity.Property(e => e.Uhidden)
                .HasDefaultValueSql("((0))")
                .HasColumnName("UHidden");
            entity.Property(e => e.UserIdE).HasColumnName("User_ID_E");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("User_Name");
            entity.Property(e => e.UserPass).HasColumnName("User_Pass");
            entity.Property(e => e.UtimeStamp)
                .HasColumnType("datetime")
                .HasColumnName("UTimeStamp");
        });

        modelBuilder.Entity<UserToGr>(entity =>
        {
            entity.ToTable("User_To_Gr");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdGr).HasColumnName("ID_GR");
            entity.Property(e => e.IdUsers).HasColumnName("ID_Users");
        });

        modelBuilder.Entity<Version>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Version");

            entity.Property(e => e.Block)
                .HasMaxLength(50)
                .HasColumnName("block");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Major).HasColumnName("major");
            entity.Property(e => e.Minor).HasColumnName("minor");
        });
    }
}
