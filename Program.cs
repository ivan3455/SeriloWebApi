using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;

var builder = WebApplication.CreateBuilder(args);

var currentTime = DateTime.Now;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Seq("http://localhost:5341/")
            .CreateLogger();

Log.Debug("This is a debug message"); // використовується під час розробки та налагодження
Log.Information("This is an information message");  // фіксація загальної інформації про роботу програми, яка не є критичною
Log.Warning("This is a warning message");   //  фіксація потенційних проблем, які можуть виникнути, але які не призводять до відмови програми
Log.Error("This is an error message");  // фіксація помилки в програмі, яка потребує уваги та виправлення
Log.Fatal("This is a fatal error message"); // фіксація критичних помилок, які перешкоджають роботі програми

builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SystemInfo}/{action=Index}/{id?}");

app.Run();
