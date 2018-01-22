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
using PUBGSharp;
using PUBGSharp.Data;
using PUBGSharp.Exceptions;
using PUBGSharp.Helpers;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Record.ApiController
{
    [Route("API/GetRecord")]
    public class GetRecordController : Controller
    {
		private readonly RecordDbContext _context;
		private ILoggerFactory _Factory;
		private ILogger _Logger;

		public GetRecordController(ILoggerFactory factory, ILogger logger, RecordDbContext context)
		{
			_Factory = factory;
			_Logger = logger;
			_context = context;
		}



		[HttpGet("{userId}")]
        public UserRecord Get(string userId)
        {
			var recordList = new UserRecord();
			try
			{
				recordList = _context.UserRecords.FirstOrDefault(p => p.UserId == userId);
			}
			catch (Exception ex)
			{
				//var logger = services.GetRequiredService<ILogger<Program>>();
			}

			return recordList;
		}


		private async Task MainAsync()
		{
			// Create client and send a stats request You can either use the "using" keyword or
			// dispose the PUBGStatsClient manually with the Dispose method.
			using (var statsClient = new PUBGStatsClient("api-key-here"))
			{
				var stats = await statsClient.GetPlayerStatsAsync("Mithrain", Region.AGG).ConfigureAwait(false);

				// Print out player name and date the stats were last updated at.
				Console.WriteLine($"{stats.nickname}, last updated at: {stats.LastUpdated}");

				try
				{
					//Print out Region chosen with mode selected
					// Stats[0] = the region u selected, eg EU in this example
					// Stats[1] = region AGG. The API also outputs the AGG values even when you select a specific region.
					var latestKillstats = stats.Stats[0].Stats.Find(x => x.Stat == Stats.Kills);

					//The Find method from APIv1 still works in APIv2

					// Print out amount of players KDR (Stats.KDR) in DUO mode (Mode.Duo) in ALL
					// regions (Region.AGG) in SEASON 1 (Seasons.EASeason1).
					var kdr = stats.Stats.Find(x => x.Mode == Mode.Duo && x.Region == Region.AGG && x.Season == Seasons.EASeason1).Stats.Find(x => x.Stat == Stats.KDR).Value;
					Console.WriteLine($"Duo KDR: {kdr}");
					// Print out amount of headshots kills in SOLO mode in NA region in SEASON 2.
					var headshotKills = stats.Stats.Find(x => x.Mode == Mode.Solo && x.Region == Region.NA && x.Season == Seasons.EASeason2).Stats.Find(x => x.Stat == Stats.HeadshotKills);
					// You can also display the stats by using .ToString() on the stat object, e.g:
					Console.WriteLine(headshotKills.ToString());

					// Print out amount of kills in the last season player has played in:
					var latestSeasonSoloStats = stats.Stats.FindLast(x => x.Mode == Mode.Solo);
					var kills = latestSeasonSoloStats.Stats.Find(x => x.Stat == Stats.Kills);
					Console.WriteLine($"Season: {latestSeasonSoloStats.Season}, kills: {kills.Value}");
				}
				/* IMPORTANT STUFF ABOUT EXCEPTIONS:
                 The LINQ and other selector methods (e.g. .Find) will throw NullReferenceException in case the stats don't exist.
                 So if player has no stats in specified region or game mode, it will throw NullReferenceException.
                 For example, if you only have played in Europe and try to look up your stats in the Asia server, instead of showing 0's everywhere it throws this. */
				catch (PUBGSharpException ex)
				{
					Console.WriteLine($"Could not retrieve stats for {stats.nickname}, error: {ex.Message}");
				}
				catch (NullReferenceException)
				{
					Console.WriteLine($"Could not retrieve stats for {stats.nickname}.");
					Console.WriteLine("The player might not exist or have stats in the specified mode or region.");
				}

				/* Outputs:
                Mithrain, last updated at: 2017-09-07T19:53:40.3611629Z
                Duo KDR: 2.87
                Stat: Headshot Kills, value: 69, Rank: #
                Season: 2017-pre4, kills: 32
                */
			}

			await Task.Delay(-1);
		}
	}
}
