﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicNinjaStack.MVVM
{
	public interface IAdd<T>
	{
		object DataContext { get; }
		int? Id { get; }
		Task LoadAsync();
	}
}
