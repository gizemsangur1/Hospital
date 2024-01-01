using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;
using WebApplication7.Utility;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity.UI.Services;

var supportedCultures = new[]
{
    new CultureInfo("tr-TR"),
    new CultureInfo("en-US")
};

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.AddRazorPages();

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});

// Connection String
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserRolesManager>();

// Dependency Injection
builder.Services.AddScoped<IDoctorBransRepository, DoctorBransRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

builder.Services.AddTransient<IEmailSender, DummyEmailSender>();

var app = builder.Build();

// Dil Değişimi Middleware
app.UseRequestLocalization();

app.Use(async (context, next) =>
{
    var userLangHeader = context.Request.Headers["Accept-Language"].ToString();
    var userLanguages = userLangHeader.Split(',').Select(x => x.Trim()).ToList();

    // Kullanıcının tarayıcı tercih ettiği ilk dil
    var userLanguage = userLanguages.FirstOrDefault();

    // Kullanıcının tarayıcı dilini uygulama kültürü olarak ayarlama
    if (!string.IsNullOrEmpty(userLanguage))
    {
        var supportedCultures = new[]
        {
            new CultureInfo("tr-TR"),
            new CultureInfo("en-US")
        };

        if (supportedCultures.Any(c => c.Name == userLanguage))
        {
            var cultureInfo = new CultureInfo(userLanguage);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Log
            Console.WriteLine($"Selected culture: {userLanguage}");
        }
        else
        {
            // Log
            Console.WriteLine($"Unsupported culture: {userLanguage}");
        }
    }

    await next();
});

// Middleware ve Configure
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.SeedRoles(serviceScope.ServiceProvider.GetRequiredService<UserRolesManager>());
}

app.Run();
