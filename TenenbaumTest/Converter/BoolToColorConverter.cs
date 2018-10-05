using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace TenenbaumTest.Converter
{
    public class BoolToColorConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The boolean value</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">If <paramref name="value"/> is true, this is the value to return. Otherwise, return transparent. </param>
        /// <param name="language"></param>
        /// <returns>If <paramref name="value"/> is true, <paramref name="parameter"/> is the value to return. Otherwise, return transparent. </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var condition = (value as bool?).GetValueOrDefault();
            if (condition)
            {
                return parameter;            
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

      
    }
}
