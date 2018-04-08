using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Record.Models
{
    public class Summoner
    {
		[Key]
		public long Id { get; set; }
		public long AccountId { get; set; }
		public string Name { get; set; }
		public int ProfileIconId { get; set; }
		public long RevisionDate { get; set; }
		public DateTime RevisionDateTime
		{
			get
			{
				DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				DateTime date = start.AddMilliseconds(RevisionDate).ToLocalTime();
				return date;
			}
		}
		public long SummonerLevel { get; set; }
	}
}
