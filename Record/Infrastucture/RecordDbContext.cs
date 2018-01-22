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
		//public RecordDbContext() : base("DefaultConnection")
		public RecordDbContext(DbContextOptions<RecordDbContext> options) : base(options)
		{
		}

		public DbSet<UserRecord> UserRecords { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserRecord>().ToTable("UserRecord");
		}
	}
}
