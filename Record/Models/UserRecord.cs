using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Record.Models
{
    public class UserRecord
    {
		public Guid Id { get; set; }
		public string UserId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
	}
}
