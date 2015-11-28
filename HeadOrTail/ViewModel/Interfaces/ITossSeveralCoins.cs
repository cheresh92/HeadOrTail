using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HeadOrTail.Model;

namespace HeadOrTail.ViewModel.Interfaces
{
    public interface ITossSeveralCoins : INotifyPropertyChanged
    {
        /// <summary>
        /// Команда броска нескольких монет
        /// </summary>
        ICommand ThrowSeveralCoinCommand { get; }

        /// <summary>
        /// Колекция после команды ThrowSeveralCoinCommand
        /// </summary>
        ObservableCollection<HeadsOrTailsEnum> CollectionOfFlippedCoins { get; }

        /// <summary>
        /// Сколько бросить монет
        /// </summary>
        int ThrowCount { get; set; }
    }
}
