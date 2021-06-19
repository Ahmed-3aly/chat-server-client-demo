namespace CA.Converters
{
	using System;
	using System.Globalization;
	using System.Windows.Data;
	public class BoolReverseConverter :
		IValueConverter
	{
		public object ConvertBack
		(
			object value,
			Type targetType,
			object parameter,
			CultureInfo culture
		) =>
			(bool)value;
		public object Convert
		(
			object value,
			Type targetType,
			object parameter,
			CultureInfo culture
		) =>
			!(bool)value;
	}
}
