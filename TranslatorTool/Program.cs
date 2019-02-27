using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TranslatorTool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string addressWithPort = "http://0.0.0.0:5001/";
            BuildWebHost(args, addressWithPort).Run();
        }

        public static IWebHost BuildWebHost(string[] args, string inServerAddress) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseUrls(inServerAddress)
                .Build();
    }
}
