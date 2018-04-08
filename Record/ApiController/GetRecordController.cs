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
using Record.Infrastucture.Enums;
using Record.Infrastucture.Helpers;
using System.Net.Http;
using Newtonsoft.Json;
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

		//[HttpGet("{userId}")]
  //      public Summoner GetUserStat(string userId, Region region = Region.KR)
  //      {
		//	var userStat = new Summoner();
		//	try
		//	{
		//		//호출할곳
		//		userStat = SummonerHelper.GetSummonerInfo_Async(_context, userId, (Region)region).GetAwaiter().GetResult();
		//	}
		//	catch (Exception ex)
		//	{
		//		var logger = _Factory.CreateLogger("GetUserStat");
		//		logger.LogError(ex.Message);
		//	}

		//	return userStat;
		//}

		//[HttpGet("{userId}")]
		//public async Task<Summoner> GetSummonerInfo_Async(string userId, Region region = Region.KR)
		//{
		//	try
		//	{
		//		var _client = new HttpClient();
		//		var code = _context.Codes.FirstOrDefault(p => p.IsActive == true && p.CodeName == "APIKey");
		//		var apiItem = _context.APIKeys.FirstOrDefault(p => p.IsActive == true);
		//		var apiKey = "";
		//		if (apiItem != null)
		//		{
		//			apiKey = apiItem.KeyValue;
		//		}

		//		string request = $"https://{region.ToString().ToLower()}.api.riotgames.com/lol/summoner/v3/summoners/by-name/{userId}?api_key={apiKey}";

		//		using (var response = await _client.GetAsync(request).ConfigureAwait(true))
		//		{
		//			var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
		//			var summoner = JsonConvert.DeserializeObject<Summoner>(responseData);
		//			if (string.IsNullOrEmpty(summoner.Name))
		//			{
		//				return null;
		//			}
		//			else
		//			{
		//				_context.Summoners.Add(summoner);
		//				return summoner;
		//			}
		//		}
		//	}
		//	catch (JsonException ex)
		//	{
		//		throw new Exception($"Failed to deserialize data: {ex.Message}", ex);
		//	}
		//}
	}
}
