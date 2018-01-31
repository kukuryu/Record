using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Record.Infrastucture.Enums
{
	public enum KeyType
	{
		/// <summary>
		/// 개발단계 키
		/// 20 requests every 1 second
		/// 100 requests every 2 minutes
		/// </summary>
		Dev = 0,

		/// <summary>
		/// 제품단계 키
		/// 3,000 requests every 10 seconds
		/// 180,000 requests every 10 minutes
		/// </summary>
		Pro = 1
	}
}
