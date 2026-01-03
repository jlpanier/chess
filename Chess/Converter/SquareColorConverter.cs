using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Converter
{
    public class SquareColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int index)
            {

                int row = index / 8;
                int col = index % 8;

                bool isDark = (row + col) % 2 == 1;

                //return isDark ? Color.FromArgb("#769656") : Color.FromArgb("#EEEED2");
                //return isDark ? Color.FromArgb("#A67C00") : Color.FromArgb("#F2E9D0");
                //return isDark ? Color.FromArgb("#5A6E7F") : Color.FromArgb("#C7D3DD");
                //return isDark ? Color.FromArgb("#4A5568") : Color.FromArgb("#DCE1E3");
                //return isDark ? Color.FromArgb("#4A752C") : Color.FromArgb("#F0F0F0");
                //return isDark ? Color.FromArgb("#EEEED2") : Color.FromArgb("#769656");
                //return isDark ? Color.FromArgb("#8B5A2B") : Color.FromArgb("#EEDFCC");
                return isDark ? Color.FromArgb("#B58863") : Color.FromArgb("#F0D9B5");
            }

            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
