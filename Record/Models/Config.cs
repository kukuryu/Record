using Record.Infrastucture.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Record.Models
{
    public class Config
	{
		[Key]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public bool IsActive { get; set; }
		//public KeyType KeyType { get; set; }
	}
}
