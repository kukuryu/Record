using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Record.Infrastucture;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Record.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Record.ApiController
{
    [Route("API/GetRecord")]
    public class GetRecordController : Controller
    {
        [HttpGet("{userId}")]
        public UserRecord Get(string userId)
        {
			var host = BuildWebHost();
			var recordList = new UserRecord();

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var context = services.GetRequiredService<RecordDbContext>();
					recordList = context.UserRecords.FirstOrDefault(p => p.UserId == userId);
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred while seeding the database.");
				}
			}

			return recordList;
		}
		public IWebHost BuildWebHost() =>
			WebHost.CreateDefaultBuilder()
				.UseStartup<Startup>()
				.Build();
	}
}
