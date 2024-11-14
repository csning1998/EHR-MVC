using EHR_MVC.Repositories;
using EHR_MVC.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PatientRepository>(provider => new PatientRepository(
    "Data Source = CSNING\\SQLEXPRESS; Integrated Security = True; Persist Security Info=False; Pooling = False; Multiple Active Result Sets=False; Connect Timeout = 60; Encrypt = True; Trust Server Certificate=True; Command Timeout = 0"
));
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