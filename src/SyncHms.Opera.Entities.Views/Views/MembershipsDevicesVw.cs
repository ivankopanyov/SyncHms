namespace SyncHms.Opera.Entities.Views;
	
public partial class MembershipsDevicesVw
{
    public decimal? DeviceId { get; set; }
    public string? DeviceCode { get; set; }
    public decimal? DeviceCodeNum { get; set; }
    public decimal? MembershipId { get; set; }
    public string? DeviceType { get; set; }
    public DateTime? InsertDate { get; set; }
    public decimal? InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? UpdateUser { get; set; }
    public DateTime? InactiveDate { get; set; }
    public string? MembershipCardNo { get; set; }
    public string? MembershipType { get; set; }
    public string? ChainCode { get; set; }
    public string? UserNote { get; set; }
    public string? ProcessingMessage { get; set; }

	public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
	{
		modelBuilder.Entity<MembershipsDevicesVw>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("MEMBERSHIPS_DEVICES_VW");

            entity.Property(e => e.ChainCode)
                .HasColumnName("CHAIN_CODE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.DeviceCode)
                .IsRequired()
                .HasColumnName("DEVICE_CODE")
                .HasMaxLength(240)
                .IsUnicode(false);

            entity.Property(e => e.DeviceCodeNum)
                .HasColumnName("DEVICE_CODE_NUM")
                .HasColumnType("NUMBER");

            entity.Property(e => e.DeviceId)
                .HasColumnName("DEVICE_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.DeviceType)
                .IsRequired()
                .HasColumnName("DEVICE_TYPE")
                .HasMaxLength(20)
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

            entity.Property(e => e.MembershipCardNo)
                .HasColumnName("MEMBERSHIP_CARD_NO")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.MembershipId)
                .HasColumnName("MEMBERSHIP_ID")
                .HasColumnType("NUMBER");

            entity.Property(e => e.MembershipType)
                .HasColumnName("MEMBERSHIP_TYPE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ProcessingMessage)
                .HasColumnName("PROCESSING_MESSAGE")
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate)
                .HasColumnName("UPDATE_DATE")
                .HasColumnType("DATE");

            entity.Property(e => e.UpdateUser)
                .HasColumnName("UPDATE_USER")
                .HasColumnType("NUMBER");

            entity.Property(e => e.UserNote)
                .HasColumnName("USER_NOTE")
                .IsUnicode(false);
        });
	}
}
