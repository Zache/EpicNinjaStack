using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicNinjaStack.MVVM.Converters
{
	public class PrefixConverter : BaseConverter
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var valueAsString = value as string;
			var paramAsString = parameter as string;

			return paramAsString + " " + valueAsString;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
