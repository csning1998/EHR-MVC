using DotNetEnv;
using EHR_MVC.Repositories;
using EHR_MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Load .env file
Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
Console.WriteLine($"Connection String: {connectionString}");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PatientRepository>(provider => new PatientRepository(connectionString));
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<UserRepository>(provider => new UserRepository(connectionString));
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Add middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Setup the route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();