using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DWA_AU24_Lab2_Group_11.Data;
using DWA_AU24_Lab2_Group_11.Services;
using DWA_AU24_Lab2_Group_11.Configuration;
using Microsoft.AspNetCore.Identity;
using DWA_AU24_Lab2_Group_11.Models;
using Task = System.Threading.Tasks.Task;

namespace DWA_AU24_Lab2_Group_11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Weather API options from configuration (User Secrets in dev, env vars in prod)
            builder.Services.Configure<WeatherApiOptions>(
                builder.Configuration.GetSection(WeatherApiOptions.SectionName));

            // Configure Admin Seed options from configuration (User Secrets in dev, env vars in prod)
            // IMPORTANT: Set credentials via User Secrets, NOT in appsettings.json
            builder.Services.Configure<AdminSeedOptions>(
                builder.Configuration.GetSection(AdminSeedOptions.SectionName));

            builder.Services.AddDbContext<FarmTrackContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("FarmTrackContext") ?? throw new InvalidOperationException("Connection string 'FarmTrackContext' not found.")));

            builder.Services.AddDbContext<DWA_AU24_Lab2_Group_11Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DWA_AU24_Lab2_Group_11Context") ?? throw new InvalidOperationException("Connection string 'DWA_AU24_Lab2_Group_11Context' not found.")));

            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DWA_AU24_Lab2_Group_11Context>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            });

            builder.Services.AddHostedService<NotificationService>();
            builder.Services.AddHttpClient<WeatherApiService>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Seed the admin user from configuration (User Secrets in dev, env vars in prod)
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var adminOptions = builder.Configuration
                    .GetSection(AdminSeedOptions.SectionName)
                    .Get<AdminSeedOptions>() ?? new AdminSeedOptions();
                SeedAdminUser(services, adminOptions).Wait();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }

        /// <summary>
        /// Seeds the initial admin user from configuration.
        /// Credentials must be configured via User Secrets (development) or environment variables (production).
        /// </summary>
        /// <param name="serviceProvider">The service provider for resolving dependencies.</param>
        /// <param name="options">Admin seed configuration options.</param>
        public static async Task SeedAdminUser(IServiceProvider serviceProvider, AdminSeedOptions options)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            // Always ensure Admin role exists
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                logger.LogInformation("Created 'Admin' role");
            }

            // Validate configuration - skip user creation if not configured
            if (!options.IsConfigured)
            {
                logger.LogWarning(
                    "Admin seed credentials not configured. " +
                    "Set AdminSeed:Email and AdminSeed:Password via User Secrets or environment variables. " +
                    "Run: dotnet user-secrets set \"AdminSeed:Email\" \"admin@farmtrack.local\" && " +
                    "dotnet user-secrets set \"AdminSeed:Password\" \"YourSecurePassword123!\"");
                return;
            }

            var adminUser = await userManager.FindByEmailAsync(options.Email);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = options.Email,
                    Email = options.Email,
                    EmailConfirmed = true,
                    FirstName = options.FirstName,
                    LastName = options.LastName,
                    Location = options.Location,
                    Latitude = options.Latitude,
                    Longitude = options.Longitude
                };

                var result = await userManager.CreateAsync(adminUser, options.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    logger.LogInformation("Admin user '{Email}' created successfully", options.Email);
                }
                else
                {
                    logger.LogError(
                        "Failed to create admin user '{Email}': {Errors}",
                        options.Email,
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                logger.LogInformation("Admin user '{Email}' already exists", options.Email);
            }
        }
    }
}
