using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicNinjaStack.Domain
{
	public class Person
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }

		public DateTime DateOfBirth { get; set; }
		public Gender Gender { get; set; }
	}

	public enum Gender
	{
		Yes,
		No,
		Maybe
	}
}
