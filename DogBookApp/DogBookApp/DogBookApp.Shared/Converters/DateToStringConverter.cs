using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace DogBookApp.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(DateTime))
            {
                return null;
            }

            DateTime date;
            if ((DateTime)value != null)
	        {
                date = (DateTime)value;
	        }
            else
            {
                date = DateTime.Now;
            }

            return string.Format("Posted on:{0:MM/dd/yy} at:{0:H:mm:ss zzz}", date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
