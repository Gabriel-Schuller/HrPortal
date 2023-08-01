using HrPortal.Employees;
using HrPortal.Holidays;
using HrPortal.BonusSalaries;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Gdpr;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;

namespace HrPortal.Data;

public class HrPortalDbContext : AbpDbContext<HrPortalDbContext>
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Holiday> Holidays { get; set; }
    public DbSet<BonusSalary> BonusSalaries { get; set; }
    public const string DbTablePrefix = "App";
    public const string DbSchema = null;

    public HrPortalDbContext(DbContextOptions<HrPortalDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        builder.ConfigureFeatureManagement();
        builder.ConfigurePermissionManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();

        /* Configure your own entities here */
        if (builder.IsHostDatabase())
        {
            builder.Entity<BonusSalary>(b =>
{
    b.ToTable(DbTablePrefix + "BonusSalaries", DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Ammount).HasColumnName(nameof(BonusSalary.Ammount));
    b.Property(x => x.AppliedDate).HasColumnName(nameof(BonusSalary.AppliedDate));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Holiday>(b =>
{
    b.ToTable(DbTablePrefix + "Holidays", DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.DaysRemainedLastYear).HasColumnName(nameof(Holiday.DaysRemainedLastYear));
    b.Property(x => x.DaysUsedThisYear).HasColumnName(nameof(Holiday.DaysUsedThisYear));
    b.Property(x => x.DaysRemained).HasColumnName(nameof(Holiday.DaysRemained));
});

        }

        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Employee>(b =>
{
    b.ToTable(DbTablePrefix + "Employees", DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.TotalNumberOfDaysThisYear).HasColumnName(nameof(Employee.TotalNumberOfDaysThisYear));
    b.Property(x => x.Name).HasColumnName(nameof(Employee.Name)).IsRequired();
    b.Property(x => x.CNP).HasColumnName(nameof(Employee.CNP)).IsRequired().HasMaxLength(EmployeeConsts.CNPMaxLength);
    b.Property(x => x.InformationsCI).HasColumnName(nameof(Employee.InformationsCI));
    b.Property(x => x.Rezidence).HasColumnName(nameof(Employee.Rezidence));
    b.Property(x => x.SendingAddress).HasColumnName(nameof(Employee.SendingAddress));
    b.Property(x => x.RelevancePhoneNumber).HasColumnName(nameof(Employee.RelevancePhoneNumber)).HasMaxLength(EmployeeConsts.RelevancePhoneNumberMaxLength);
    b.Property(x => x.PersonalPhoneNumber).HasColumnName(nameof(Employee.PersonalPhoneNumber)).HasMaxLength(EmployeeConsts.PersonalPhoneNumberMaxLength);
    b.Property(x => x.HiringDate).HasColumnName(nameof(Employee.HiringDate));
    b.Property(x => x.BirthDay).HasColumnName(nameof(Employee.BirthDay));
    b.Property(x => x.StartingSalary).HasColumnName(nameof(Employee.StartingSalary));
    b.Property(x => x.PaysProgrammerTaxes).HasColumnName(nameof(Employee.PaysProgrammerTaxes));
});

        }
    }
}