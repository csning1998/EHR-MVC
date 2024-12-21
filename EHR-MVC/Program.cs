using DotNetEnv;
using EHR_MVC.Repositories;
using EHR_MVC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Retrive parameters from JwtSettings in appsetting.json
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
var issuer = jwtSettingsSection["Issuer"];
var audience = jwtSettingsSection["Audience"];
var secretKey = jwtSettingsSection["SecretKey"];
var expiresMinutes = Convert.ToInt32(jwtSettingsSection["ExpiresMinutes"]);

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


// Authentication + JWT Bearer
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.FromMinutes(5),

            RoleClaimType = "UserId",
            NameClaimType = "Email" 
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("Authentication failed.");
                Console.WriteLine($"Message: {context.Exception.Message}");
                Console.WriteLine($"Inner Exception: {context.Exception.InnerException}");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine("Unauthorized access attempt.");
                Console.WriteLine($"Challenge context: {context.ErrorDescription}");
                context.HandleResponse();
                context.Response.Redirect("/Error/Unauthorized");
                return Task.CompletedTask;
            }
        };
    });

// 3.add Controllers, Views
builder.Services.AddControllersWithViews();

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

// AuthENTication first and then AuthORization
app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePages(context =>
{
    var response = context.HttpContext.Response;
    if (response.StatusCode == 401)
        response.Redirect("/Error/Status401");
    
    else if (response.StatusCode == 403)
        response.Redirect("/Error/Status403");
    return Task.CompletedTask;
});

// Setup the route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();