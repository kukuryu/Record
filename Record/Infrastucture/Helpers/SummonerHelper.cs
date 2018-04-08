using Newtonsoft.Json;
using Record.Infrastucture.Enums;
using Record.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Record.Infrastucture.Helpers
{
    public static class SummonerHelper
    {
		static HttpClient _client;
		static SummonerHelper()
		{
			if (_client == null)
			{
				_client = new HttpClient();
			}
		}

		public static List<Summoner> SearchSummoner(RecordDbContext _context, string playerName, Region region)
		{
			try
			{
				var summoners = _context.Summoners.Where(p => p.Name.Contains(playerName)).ToList();

				return summoners;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to Runtime: {ex.Message}", ex);
			}
		}
	}
}
