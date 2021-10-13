using System;
using System.Net;
using System.Net.Sockets;
using ServerApp.Data;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    public class Logger
    {
        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public static void log (TasinmazContext _context,string message, bool success)
        {
            try
            {
                 _context.Log.Add(new Log {
                    logDetails = message,
                    logTime = DateTime.Now,
                    logSuccess = success,
                    ipAdress = GetLocalIPAddress()
                });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}