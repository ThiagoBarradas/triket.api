﻿using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Trinket.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls("http://*:5002")
                .Build();

            host.Run();
        }
    }
}