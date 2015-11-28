using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HeadOrTail.ViewModel.Interfaces
{
    public interface IStatistic : INotifyPropertyChanged
    {
        /// <summary>
        /// Сброс данных. Обнуление AllCoinThrowCount, GuessCoinCount, CorrectlyGuessCoinCount, ThrowCount
        /// </summary>
        ICommand ResetData { get; }

        /// <summary>
        /// Количество выпаших орлов
        /// </summary>
        uint HeadsCount { get; }

        /// <summary>
        /// Количество выпавших решок
        /// </summary>
        uint TailsCount { get; }

        /// <summary>
        /// Все броски монеты (считаются только броски, если бросить несколько монет, то засчитывается за 1)
        /// </summary>
        uint AllCoinThrowCount { get; }

        /// <summary>
        /// Колличество бросков монеты при попытки его угодать
        /// </summary>
        uint GuessCoinCount { get; }

        /// <summary>
        /// Колличество верно угаданных бросков
        /// </summary>
        uint CorrectlyGuessCoinCount { get; }

        /// <summary>
        /// Сохранить данные
        /// </summary>
        void Save();
    }
}
