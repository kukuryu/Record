using Newtonsoft.Json;
using Record.Infrastucture.Enums;
using Record.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Record.Infrastucture.Requester
{
	public class Requester : IDisposable
	{
		private readonly RecordDbContext _context;
		private readonly HttpClient _client;

		public Requester(RecordDbContext context)
		{
			_context = context;
			_client = new HttpClient();
		}

		public async Task<string> RequestUserStat_Async(string playerName, Region region)
		{
			try
			{
				var apiKey = "";
				var	apiItem = _context.APIKeys.FirstOrDefault(p => p.IsActive == true);
				if (apiItem != null)
				{
					apiKey = apiItem.KeyValue;
				}
				var codes = _context.Codes.FirstOrDefault(p => p.IsActive == true && p.CodeName == "MainDomain");


				string request = $"https://{region.ToString().ToLower()}."+ codes.CodeValue+ "/lol/summoner/v3/summoners/by-name/{playerName}?api_key={apiKey}";
				
				using (var response = await _client.GetAsync(request).ConfigureAwait(true))
				{
					var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
					return responseData;
				}
			}
			catch (JsonException ex)
			{
				throw new Exception($"Failed to deserialize data: {ex.Message}", ex);
			}
		}
		

		public void Dispose()
		{
			_client?.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
