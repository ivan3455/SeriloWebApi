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

Log.Debug("This is a debug message"); // ��������������� �� ��� �������� �� ������������
Log.Information("This is an information message");  // �������� �������� ���������� ��� ������ ��������, ��� �� � ���������
Log.Warning("This is a warning message");   //  �������� ����������� �������, �� ������ ���������, ��� �� �� ���������� �� ������ ��������
Log.Error("This is an error message");  // �������� ������� � �������, ��� ������� ����� �� �����������
Log.Fatal("This is a fatal error message"); // �������� ��������� �������, �� ������������� ����� ��������

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
