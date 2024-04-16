// BluetoothController.cs
using InTheHand.Net.Sockets;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeriloWebApi.Controllers
{
    public class BluetoothController : Controller
    {
        public IActionResult Index()
        {
            var connectedDevices = GetConnectedBluetoothDevices();
            ViewData["ConnectedDevices"] = connectedDevices;
            return View();
        }

        private List<string> GetConnectedBluetoothDevices()
        {
            Log.Debug("Fetching connected Bluetooth devices at {Time}", DateTime.Now);

            var client = new BluetoothClient();
            var devices = client.DiscoverDevices();
            var connectedDevices = devices.Where(d => d.Connected).Select(d => d.DeviceName).ToList();

            Log.Debug("Found {Count} connected Bluetooth devices", connectedDevices.Count);

            return connectedDevices;
        }
    }
}
