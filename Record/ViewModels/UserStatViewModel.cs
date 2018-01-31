using Newtonsoft.Json;
using Record.Infrastucture;
using Record.Infrastucture.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Record.ViewModels
{
	public class UserStatViewModel
	{
		public long Id { get; set; }
		public long AccountId { get; set; }
		public string Name { get; set; }
		public int ProfileIconId { get; set; }
		public long RevisionDate { get; set; }
		public DateTime RevisionDateTime { get {
				DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				DateTime date = start.AddMilliseconds(RevisionDate).ToLocalTime();
				return date; } }
		public long SummonerLevel { get; set; }
	}

	public static class UserStatHelper{
		public static async Task<UserStatViewModel> GetUserStat_Async(RecordDbContext _context, string playerName, Region region)
		{
			try
			{
				var _client = new HttpClient();
				var apiItem = _context.APIKeys.FirstOrDefault(p => p.IsActive == true);
				var apiKey = "";
				if (apiItem != null)
				{
					apiKey = apiItem.KeyValue;
				}
				string request = $"https://{region.ToString().ToLower()}.api.riotgames.com/lol/summoner/v3/summoners/by-name/{playerName}?api_key={apiKey}";

				using (var response = await _client.GetAsync(request).ConfigureAwait(true))
				{
					var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
					var userModel = JsonConvert.DeserializeObject<UserStatViewModel>(responseData);
					//if (userModel != null)
					//{
					//	UserId = userModel.UserId;
					//}
					return userModel;
				}
			}
			catch (JsonException ex)
			{
				throw new Exception($"Failed to deserialize data: {ex.Message}", ex);
			}
		}
	}
	
}
