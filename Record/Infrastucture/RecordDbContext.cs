using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Record.Models;

namespace Record.Infrastucture
{
    public class RecordDbContext : DbContext
	{
		public RecordDbContext(DbContextOptions<RecordDbContext> options) : base(options)
		{
		}

		//public DbSet<Summoner> Summoners { get; set; }
		public DbSet<SummonersId> SummonersIds { get; set; }
		public DbSet<APIKey> APIKeys { get; set; }
		public DbSet<Code> Codes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<Summoner>().ToTable("RECORD_Summoner");
			modelBuilder.Entity<SummonersId>().ToTable("RECORD_SummonersId");
			modelBuilder.Entity<APIKey>().ToTable("SYS_APIKey");
			modelBuilder.Entity<Code>().ToTable("SYS_Code");
			
		}
	}
}
