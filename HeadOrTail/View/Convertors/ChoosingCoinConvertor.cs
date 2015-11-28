using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using HeadOrTail.Model;

namespace HeadOrTail.View.Convertors
{
    class ChoosingCoinConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var par = (HeadsOrTailsEnum) (int) parameter;
            var val = (HeadsOrTailsEnum?) value;

            if (par == val)
                return true;
            
            return false;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool isChecked = (bool) value;

            if (isChecked)
            {
                return (HeadsOrTailsEnum)(int)parameter;
            }
            return null;
        }
    }
}
