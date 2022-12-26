using dotnetdevs.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using dotnetdevs.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using dotnetdevs.Models;
using dotnetdevs.Services;
using Stripe;
using Westwind.AspNetCore.Markdown;
using dotnetdevs.ViewModels;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);
var connectionString = ConnectionHelper.GetConnectionString();
builder.Services.AddDbContext<ApplicationDbContext>(options => options
    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

// customize identity urls
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/login");
	options.LogoutPath = new PathString("/logout");
});

// lowercase urls
builder.Services.Configure<RouteOptions>(options =>
{
	options.LowercaseUrls = true;
});

builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/{0}.cshtml");
    });

builder.Services.AddTransient<UserManager<ApplicationUser>>();
builder.Services.AddTransient<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<DeveloperService>();
builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<ConversationService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<SearchStatusService>();
builder.Services.AddScoped<ExperienceLevelService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddServerSideBlazor();
builder.Services.AddAutoMapper(typeof(MapperProfile));
var app = builder.Build();

StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("STRIPE_SECRET");

// run migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// app.UseMvc();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
