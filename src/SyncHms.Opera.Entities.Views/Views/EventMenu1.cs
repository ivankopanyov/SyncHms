namespace SyncHms.Opera.Entities.Views;
	
public partial class EventMenu1
{
    public decimal? ActualCovers { get; set; }
    public decimal? ActualNumber { get; set; }
    public string? BeverageClass { get; set; }
    public decimal? BookId { get; set; }
    public string? ConsumptionYn { get; set; }
    public string? Description { get; set; }
    public decimal? EventId { get; set; }
    public decimal? ExpectedNumber { get; set; }
    public string? GratuityYn { get; set; }
    public decimal? GuaranteedNumber { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public decimal? MenuId { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public string? Restriction { get; set; }
    public string? Serving { get; set; }
    public DateTime? ServingEnd { get; set; }
    public DateTime? ServingStart { get; set; }
    public decimal? SetNumber { get; set; }
    public string? Tickets { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public decimal? BilledNumber { get; set; }
    public decimal? ComplimentaryNumber { get; set; }
    public decimal? EventMenuId { get; set; }
    public decimal? PersonsPerTable { get; set; }
    public string? Resort { get; set; }
    public decimal? OrderBy { get; set; }
    public decimal? PkgId { get; set; }
    public string? MultiChoiceYn { get; set; }
    public string? MenuResort { get; set; }
    public decimal? PkgExpNumber { get; set; }
    public decimal? PkgGuaNumber { get; set; }
    public decimal? PkgActNumber { get; set; }
    public decimal? PkgBilledNumber { get; set; }
    public decimal? Discount { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<EventMenu1>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("EVENT_MENU");

            entity.Property(e => e.ActualCovers)
                .HasColumnName("ACTUAL_COVERS")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ActualNumber)
                .HasColumnName("ACTUAL_NUMBER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BeverageClass)
                .HasColumnName("BEVERAGE_CLASS")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BilledNumber)
                .HasColumnName("BILLED_NUMBER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BookId)
                .HasColumnName("BOOK_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ComplimentaryNumber)
                .HasColumnName("COMPLIMENTARY_NUMBER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ConsumptionYn)
                .HasColumnName("CONSUMPTION_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(2000)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Discount)
                .HasColumnName("DISCOUNT")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.EventId)
                .HasColumnName("EVENT_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.EventMenuId)
                .HasColumnName("EVENT_MENU_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ExpectedNumber)
                .HasColumnName("EXPECTED_NUMBER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.GratuityYn)
                .HasColumnName("GRATUITY_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.GuaranteedNumber)
                .HasColumnName("GUARANTEED_NUMBER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.MenuId)
                .HasColumnName("MENU_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.MenuResort)
                .HasColumnName("MENU_RESORT")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.MultiChoiceYn)
                .HasColumnName("MULTI_CHOICE_YN")
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .HasColumnName("NAME")
                .HasMaxLength(200)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OrderBy)
                .HasColumnName("ORDER_BY")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PersonsPerTable)
                .HasColumnName("PERSONS_PER_TABLE")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PkgActNumber)
                .HasColumnName("PKG_ACT_NUMBER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PkgBilledNumber)
                .HasColumnName("PKG_BILLED_NUMBER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PkgExpNumber)
                .HasColumnName("PKG_EXP_NUMBER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PkgGuaNumber)
                .HasColumnName("PKG_GUA_NUMBER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PkgId)
                .HasColumnName("PKG_ID")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Price)
                .HasColumnName("PRICE")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Restriction)
                .HasColumnName("RESTRICTION")
                .HasMaxLength(400)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Serving)
                .HasColumnName("SERVING")
                .HasMaxLength(400)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ServingEnd)
                .HasColumnName("SERVING_END")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ServingStart)
                .HasColumnName("SERVING_START")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.SetNumber)
                .HasColumnName("SET_NUMBER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Tickets)
                .HasColumnName("TICKETS")
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER")
                .ValueGeneratedOnAdd();
        });
	}
}
