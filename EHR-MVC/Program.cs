using DotNetEnv;
using EHR_MVC.Repositories;
using EHR_MVC.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
Console.WriteLine($"Connection String: {connectionString}");

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PatientRepository>(provider => new PatientRepository(connectionString));
builder.Services.AddScoped<PatientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();