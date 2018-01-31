using Record.Infrastucture.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Record.Models
{
    public class APIKey
	{
		[Key]
		public string KeyValue { get; set; }
		public bool IsActive { get; set; }
		public KeyType KeyType { get; set; }
	}
}
