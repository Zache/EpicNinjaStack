using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicNinjaStack.MVVM
{
	public interface ICreateEditViewModel<T> : IEditableObject
	{
		bool IsDirty { get; }
		int? Id { get; set; }
	}
}
