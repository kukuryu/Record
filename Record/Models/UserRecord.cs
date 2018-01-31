using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Record.Models
{
    public class UserRecord
    {
		[Key]
		public Guid Id { get; set; }
		public string UserId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
	}
}
