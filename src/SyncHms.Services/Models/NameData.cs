namespace SyncHms.Services;

/// <summary>
/// Класс, описывающий модель, используемую для получения результата вызова процедур
/// <c>hrs_dev.hrs_sh_sens.dob</c> и <c>hrs_dev.hrs_sh_sens.pass_id</c> в базе данных <c>OPERA</c>
/// </summary>
public class NameData
{
    /// <summary>
    /// Дата рождения.<br/>
    /// Результат вызова процедуры <c>hrs_dev.hrs_sh_sens.dob</c>
    /// </summary>
    public string BirthDay { get; set; }

    /// <summary>
    /// Номер паспорта.<br/>
    /// Результат вызова процедуры <c>hrs_dev.hrs_sh_sens.pass_id</c>
    /// </summary>
    public string PassId { get; set; }

    /// <summary>
    /// Метод, описывающий логику построения модели.<br/>
    /// Вызывается в методе <see cref="OperaDbContextBase.OnModelCreating"/>
    /// </summary>
    /// <param name="modelBuilder">Экземпляр построителя моделей.</param>
    /// <param name="types">
    /// Множество типов, определенных в классе <see cref="OperaDbContext"/>
    /// в свойствах типа <see cref="DbSet{TEntity}"/>.
    /// </param>
    public static void OnModelCreating(ModelBuilder modelBuilder, ISet<Type> types)
    {
        modelBuilder.Entity<NameData>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.BirthDay)
                .HasColumnName("BIRTHDAY")
                .IsUnicode(false);

            entity.Property(e => e.PassId)
                .HasColumnName("PASS_ID")
                .IsUnicode(false);
        });
    }
}
