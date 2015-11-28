using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HeadOrTail.Model;

namespace HeadOrTail.ViewModel.Interfaces
{
    public interface ITossCoin : INotifyPropertyChanged
    {
        /// <summary>
        /// Команда броска монеты
        /// </summary>
        ICommand ThrowCoinCommand { get; }

        /// <summary>
        /// Выпавшая сторона монеты
        /// </summary>
        HeadsOrTailsEnum? SideOfCoin { get; }
    }
}
