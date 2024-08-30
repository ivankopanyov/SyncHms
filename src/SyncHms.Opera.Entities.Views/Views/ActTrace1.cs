namespace SyncHms.Opera.Entities.Views;
	
public partial class ActTrace1
{
    public decimal? TraceId { get; set; }
    public string? Tracecode { get; set; }
    public string? Tracegroup { get; set; }
    public string? Tablename { get; set; }
    public string? Fieldname { get; set; }
    public string? Fielddesc { get; set; }
    public string? OnCreateYn { get; set; }
    public string? OnChangeYn { get; set; }
    public string? OnDeleteYn { get; set; }
    public string? Resort { get; set; }
    public string? ActType { get; set; }
    public string? ActPurpose { get; set; }
    public string? ActDate { get; set; }
    public DateTime? ActTime { get; set; }
    public decimal? ActSrep { get; set; }
    public string? Condition { get; set; }
    public string? Operator { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public string? ActClass { get; set; }
    public DateTime? InactiveDate { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<ActTrace1>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("ACT_TRACE");

            entity.Property(e => e.ActClass)
                .HasColumnName("ACT_CLASS")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ActDate)
                .HasColumnName("ACT_DATE")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.ActPurpose)
                .IsRequired()
                .HasColumnName("ACT_PURPOSE")
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.ActSrep)
                .HasColumnName("ACT_SREP")
                .HasColumnType("NUMBER");

            entity.Property(e => e.ActTime)
                .HasColumnName("ACT_TIME")
                .HasColumnType("DATE");

            entity.Property(e => e.ActType)
                .IsRequired()
                .HasColumnName("ACT_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Condition)
                .HasColumnName("CONDITION")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Fielddesc)
                .HasColumnName("FIELDDESC")
                .IsUnicode(false);

            entity.Property(e => e.Fieldname)
                .HasColumnName("FIELDNAME")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.InactiveDate)
                .HasColumnName("INACTIVE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertDate)
                .HasColumnName("INSERT_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.InsertUser)
                .HasColumnName("INSERT_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.OnChangeYn)
                .HasColumnName("ON_CHANGE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.OnCreateYn)
                .HasColumnName("ON_CREATE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.OnDeleteYn)
                .HasColumnName("ON_DELETE_YN")
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Operator)
                .HasColumnName("OPERATOR")
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.Property(e => e.Resort)
                .IsRequired()
                .HasColumnName("RESORT")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Tablename)
                .IsRequired()
                .HasColumnName("TABLENAME")
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.Property(e => e.TraceId)
                .HasColumnName("TRACE_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.Tracecode)
                .IsRequired()
                .HasColumnName("TRACECODE")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.Tracegroup)
                .HasColumnName("TRACEGROUP")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");
        });
	}
}
