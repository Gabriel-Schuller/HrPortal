using HrPortal.Employees;
using HrPortal.Holidays;
using HrPortal.BonusSalaries;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using HrPortal.Data;
using HrPortal.Localization;
using HrPortal.Menus;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp;
using Volo.Abp.Uow;
using Volo.Abp.Account;
using Volo.Abp.Account.Pro.Admin.Blazor.Server;
using Volo.Abp.Account.Pro.Public.Blazor.Server;
using Volo.Abp.Account.Public.Web;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Components.Server.LeptonXTheme;
using Volo.Abp.AspNetCore.Components.Server.LeptonXTheme.Bundling;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonX;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonX.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.Blazor.Server;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Caching;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.Blazor.Server;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Identity.Pro.Blazor.Server;
using Volo.Abp.Commercial.SuiteTemplates;
using Volo.Abp.Gdpr;
using Volo.Abp.Gdpr.Blazor.Server;
using Volo.Abp.Emailing;
using Volo.Abp.Gdpr.Blazor.Extensions;
using Volo.Abp.Identity.Pro.Blazor;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LanguageManagement.Blazor.Server;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.LeptonX.Shared;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.OpenIddict.Pro.Blazor.Server;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Blazor.Server;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.OpenIddict;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Blazor.Server;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TextTemplateManagement;
using Volo.Abp.TextTemplateManagement.Blazor.Server;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Host;
using Volo.Saas.Host.Blazor;
using Volo.Saas.Host.Blazor.Server;

namespace HrPortal;

[DependsOn(
    // ABP Framework packages
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAutofacModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpCachingModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(VoloAbpCommercialSuiteTemplatesModule),

    // LeptonX Theme module packages
    typeof(AbpAspNetCoreMvcUiLeptonXThemeModule),
    typeof(AbpAspNetCoreComponentsServerLeptonXThemeModule),

    // Account module packages
    typeof(AbpAccountPublicWebOpenIddictModule),
    typeof(AbpAccountPublicHttpApiModule),
    typeof(AbpAccountPublicApplicationModule),
    typeof(AbpAccountPublicBlazorServerModule),

    typeof(AbpAccountAdminBlazorServerModule),
    typeof(AbpAccountAdminHttpApiModule),
    typeof(AbpAccountAdminApplicationModule),

    // Identity module packages
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpPermissionManagementDomainOpenIddictModule),
    typeof(AbpIdentityProBlazorServerModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpIdentityProEntityFrameworkCoreModule),

    typeof(AbpOpenIddictProBlazorServerModule),
    typeof(AbpOpenIddictProHttpApiModule),
    typeof(AbpOpenIddictProApplicationModule),
    typeof(AbpOpenIddictProEntityFrameworkCoreModule),

    // Audit logging module packages
    typeof(AbpAuditLoggingBlazorServerModule),
    typeof(AbpAuditLoggingHttpApiModule),
    typeof(AbpAuditLoggingApplicationModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),

    // Permission Management module packages
    typeof(AbpPermissionManagementBlazorServerModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),

    // Saas Management module packages
    typeof(SaasHostBlazorServerModule),
    typeof(SaasHostHttpApiModule),
    typeof(SaasHostApplicationModule),
    typeof(SaasEntityFrameworkCoreModule),

    // Feature Management module packages
    typeof(AbpFeatureManagementBlazorServerModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),

    // Setting Management module packages
    typeof(AbpSettingManagementBlazorServerModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),

    // Text Template Management module packages
    typeof(TextTemplateManagementBlazorServerModule),
    typeof(TextTemplateManagementHttpApiModule),
    typeof(TextTemplateManagementApplicationModule),
    typeof(TextTemplateManagementEntityFrameworkCoreModule),

    // Language Management module packages
    typeof(LanguageManagementBlazorServerModule),
    typeof(LanguageManagementHttpApiModule),
    typeof(LanguageManagementApplicationModule),
    typeof(LanguageManagementEntityFrameworkCoreModule),

    // GDPR module packages
    typeof(AbpGdprBlazorServerModule),
    typeof(AbpGdprHttpApiModule),
    typeof(AbpGdprApplicationModule),
    typeof(AbpGdprEntityFrameworkCoreModule),

    // Blob Storing
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule)
)]
public class HrPortalModule : AbpModule
{
    /* Single point to enable/disable multi-tenancy */
    private const bool IsMultiTenant = true;

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(HrPortalResource)
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("HrPortal");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        if (hostingEnvironment.IsDevelopment())
        {
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
        }

        ConfigureAuthentication(context);
        ConfigureBundles();
        ConfigureBlazorise(context);
        ConfigureRouter(context);
        ConfigureMultiTenancy();
        ConfigureMenu(context);
        ConfigureUrls(configuration);
        ConfigureImpersonation(context, configuration);
        ConfigureAutoMapper(context);
        ConfigureSwagger(context.Services);
        ConfigureAutoApiControllers();
        ConfigureLocalization();
        ConfigureVirtualFiles(hostingEnvironment);
        ConfigureEfCore(context);
        ConfigureCookieConsent(context);
        ConfigureTheme();
    }

    private void ConfigureCookieConsent(ServiceConfigurationContext context)
    {
        context.Services.AddAbpCookieConsent(options =>
        {
            options.IsEnabled = true;
            options.CookiePolicyUrl = "/CookiePolicy";
            options.PrivacyPolicyUrl = "/PrivacyPolicy";
        });
    }

    private void ConfigureTheme()
    {
        Configure<LeptonXThemeOptions>(options =>
        {
            options.DefaultStyle = LeptonXStyleNames.System;
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            // MVC UI
            options.StyleBundles.Configure(
                LeptonXThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );

            //BLAZOR UI
            options.StyleBundles.Configure(
                BlazorLeptonXThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/blazor-global-styles.css");
                    //You can remove the following line if you don't use Blazor CSS isolation for components
                    bundle.AddFiles("/HrPortal.styles.css");
                }
            );
        });
    }

    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(HrPortalModule).Assembly;
        });
    }

    private void ConfigureMultiTenancy()
    {
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = IsMultiTenant;
        });
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new HrPortalMenuContributor());
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());
        });
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<HrPortalResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/HrPortal");

            options.DefaultResourceType = typeof(HrPortalResource);

            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic", "is"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский", "ru"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
            options.Languages.Add(new LanguageInfo("es", "es", "Español"));
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("HrPortal", typeof(HrPortalResource));
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(HrPortalModule).Assembly);
        });
    }

    private void ConfigureSwagger(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "HrPortal API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    private void ConfigureImpersonation(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.Configure<SaasHostBlazorOptions>(options =>
        {
            options.EnableTenantImpersonation = true;
        });
        context.Services.Configure<AbpIdentityProBlazorOptions>(options =>
        {
            options.EnableUserImpersonation = true;
        });
        context.Services.Configure<AbpAccountOptions>(options =>
        {
            options.TenantAdminUserName = "admin";
            options.ImpersonationTenantPermission = SaasHostPermissions.Tenants.Impersonation;
            options.ImpersonationUserPermission = IdentityPermissions.Users.Impersonation;
        });
    }

    private void ConfigureAutoMapper(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<HrPortalModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            /* Uncomment `validate: true` if you want to enable the Configuration Validation feature.
             * See AutoMapper's documentation to learn what it is:
             * https://docs.automapper.org/en/stable/Configuration-validation.html
             */
            options.AddMaps<HrPortalModule>(/* validate: true */);
        });
    }

    private void ConfigureVirtualFiles(IWebHostEnvironment hostingEnvironment)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<HrPortalModule>();
            if (hostingEnvironment.IsDevelopment())
            {
                /* Using physical files in development, so we don't need to recompile on changes */
                options.FileSets.ReplaceEmbeddedByPhysical<HrPortalModule>(hostingEnvironment.ContentRootPath);
            }
        });
    }

    private void ConfigureEfCore(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<HrPortalDbContext>(options =>
        {
            /* You can remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots
             * Documentation: https://docs.abp.io/en/abp/latest/Entity-Framework-Core#add-default-repositories
             */
            options.AddDefaultRepositories(includeAllEntities: true);
            options.AddRepository<BonusSalary, BonusSalaries.EfCoreBonusSalaryRepository>();

            options.AddRepository<Holiday, Holidays.EfCoreHolidayRepository>();

            options.AddRepository<Employee, Employees.EfCoreEmployeeRepository>();

        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(configurationContext =>
            {
                configurationContext.UseSqlServer();
            });
        });

    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseAbpSecurityHeaders();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (IsMultiTenant)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "HrPortal API");
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}