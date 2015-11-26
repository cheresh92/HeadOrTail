using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using HeadOrTail.Model;

namespace HeadOrTail.View.Convertors
{
    class TextCoinConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return "";
            HeadsOrTailsEnum Coin = (HeadsOrTailsEnum) value;
            if (Coin == HeadsOrTailsEnum.Heads)
                return "Oрел";
            else
                return "Решка";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
