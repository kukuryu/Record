using Record.Infrastucture.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Record.Models
{
    public class Code
	{
		[Key]
		public Guid Id { get; set; }
		public string CodeName { get; set; }
		public string CodeValue { get; set; }
		public bool IsActive { get; set; }
	}
}
