using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Net;
using System.Runtime.InteropServices;

namespace SeriloWebApi.Controllers
{
    public class SystemInfoController : Controller
    {
        public IActionResult Index()
        {
            // Отримання інформації про систему користувача
            string osVersion = Environment.OSVersion.VersionString;
            string processorArchitecture = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
            string frameworkVersion = RuntimeInformation.FrameworkDescription;
            string machineName = Environment.MachineName;
            string currentUser = Environment.UserName;
            string browser = Request.Headers["User-Agent"].ToString();
            string userTime = GetUserTime();
            string publicIPAddress = GetPublicIPAddress();

            // Передача інформації на сторінку для відображення
            ViewData["OSVersion"] = osVersion;
            ViewData["ProcessorArchitecture"] = processorArchitecture;
            ViewData["FrameworkVersion"] = frameworkVersion;
            ViewData["MachineName"] = machineName;
            ViewData["CurrentUser"] = currentUser;
            ViewData["Browser"] = browser;
            ViewData["UserTime"] = userTime;
            ViewData["PublicIPAddress"] = publicIPAddress;

            return View();
        }

        private string GetUserTime()
        {
            Log.Debug("Getting user time at {Time}", DateTime.Now);

            return DateTime.Now.ToString();
        }

        private string GetPublicIPAddress()
        {
            Log.Debug("Getting public IP address at {Time}", DateTime.Now);

            try
            {
                using (WebClient client = new WebClient())
                {
                    string publicIPAddress = client.DownloadString("https://api.ipify.org");

                    Log.Debug("Received public IP address: {IPAddress}", publicIPAddress);

                    return publicIPAddress;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error getting public IP address");

                return "Public IP empty";
            }
        }
    }
}