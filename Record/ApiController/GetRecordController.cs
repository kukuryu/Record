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
using Microsoft.EntityFrameworkCore;
using Record.Infrastucture.Requester;
using Record.ViewModels;
using Record.Infrastucture.Enums;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Record.ApiController
{
    [Route("API/GetRecord")]
    public class GetRecordController : Controller
    {
		private readonly RecordDbContext _context;
		private ILoggerFactory _Factory;

		public GetRecordController(ILoggerFactory factory, RecordDbContext context)
		{
			_Factory = factory;
			_context = context;
		}

		[HttpGet("{userId}")]
        public UserStatViewModel GetUserStat(string userId, Region region = Region.KR)
        {
			var userStat = new UserStatViewModel();
			try
			{
				//호출할곳
				userStat = UserStatHelper.GetUserStat_Async(_context,userId, (Region)region).GetAwaiter().GetResult();
				//var value = req.RequestUserStat_Async(userId, 0).GetAwaiter().GetResult();

				//recordList.Content = value;
			}
			catch (Exception ex)
			{
				var logger = _Factory.CreateLogger("Program");
			}

			return userStat;
		}
	}
}
