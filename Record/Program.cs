﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Record
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

			var host = new WebHostBuilder()
			.UseKestrel()
			.UseContentRoot(Directory.GetCurrentDirectory())
			.UseIISIntegration()
			.UseStartup<IWebHost>()
			.Build();

			host.Run();
		}

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
