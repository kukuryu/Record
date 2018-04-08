using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Record.Models;
using Record.Infrastucture.Enums;
using System.Net.Http;
using Record.Infrastucture;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Record.Controllers
{
    public class SummonerController : Controller
    {
		private readonly RecordDbContext _Context;
		private ILoggerFactory _Factory;
		private IConfiguration _Configuration;

		public SummonerController(ILoggerFactory factory, RecordDbContext context, IConfiguration configuration)
		{
			_Factory = factory;
			_Context = context;
			_Configuration = configuration;
		}

		public async Task<IActionResult> MainInfo(string summonerName, Region region = Region.KR)
		{
			try
			{
				//var _client = new HttpClient();

				var summoner = await GetSummonerInfo_Async(summonerName, region);

				if (summoner != null)
				{
					var summonersId = await _Context.SummonersIds.FirstOrDefaultAsync(p => p.Id == summoner.Id);
				}
				var profile = _Configuration.GetSection("Logging").ToString() + summoner.ProfileIconId;

				return View(summoner);
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to Load Page: {ex.Message}", ex); 
			}
		}

		public async Task<Summoner> GetSummonerInfo_Async(string summonerName, Region region = Region.KR)
		{
			try
			{
				var _client = new HttpClient();
				var code = _Context.Codes.FirstOrDefault(p => p.IsActive == true && p.CodeName == "APIKey");
				var apiItem = _Context.APIKeys.FirstOrDefault(p => p.IsActive == true);
				var apiKey = "";
				if (apiItem != null)
				{
					apiKey = apiItem.KeyValue;
				}

				string request = $"https://{region.ToString().ToLower()}.api.riotgames.com/lol/summoner/v3/summoners/by-name/{summonerName}?api_key={apiKey}";

				using (var response = await _client.GetAsync(request).ConfigureAwait(true))
				{
					var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
					var summoner = JsonConvert.DeserializeObject<Summoner>(responseData);
					if (string.IsNullOrEmpty(summoner.Name))
					{
						return null;
					}
					else
					{
						if (_Context.SummonersIds.Count(p=>p.Id == summoner.Id) == 0)
						{
							_Context.SummonersIds.Add(new SummonersId { Id = summoner.Id } );
						}
						return summoner;
					}
				}
			}
			catch (JsonException ex)
			{
				throw new Exception($"Failed to deserialize data: {ex.Message}", ex);
			}
		}
	}
}