using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HeadOrTail.Model;

namespace HeadOrTail.ViewModel
{
    interface IHeadsOrTailsViewModel : INotifyPropertyChanged
    {
        /*********************************************************************************
            Данный интерфейс можно разделить на 4 части.
         * 1: Простой бросок монеты. Интересен только результат броска
         * 2: Бросок монетки с попыткой угадать монетку
         * 3: Бросок нескольких монет 
         * 4: Сброс данных и сохранение данных (не команда, так как сохранятся нужно не из интерфейса)
        **********************************************************************************/

        #region Простой бросок монеты. Интересен только результат броска
        /// <summary>
        /// Команда броска монеты
        /// </summary>
        ICommand ThrowCoinCommand { get; }

        /// <summary>
        /// Выпавшая сторона монеты
        /// </summary>
        HeadsOrTailsEnum? SideOfCoin { get; }

        /// <summary>
        /// Все броски монеты (считаются только броски, если бросить несколько монет, то засчитывается за 1)
        /// </summary>
        uint AllCoinThrowCount { get; }
        #endregion

        #region Бросок монетки с попыткой угадать монетку
        /// <summary>
        /// Команда попытки угадывания стороны монеты
        /// </summary>
        ICommand TryToGuessCommand { get; }

        /// <summary>
        /// Сторона которую мы вибираем для угадывания
        /// </summary>
        HeadsOrTailsEnum? GuessSideOfCoin { get; }

        /// <summary>
        /// Колличество бросков монеты при попытки его угодать
        /// </summary>
        uint GuessCoinCount { get; }

        /// <summary>
        /// Колличество верно угаданных бросков
        /// </summary>
        uint CorrectlyGuessCoinCount { get; }

        /// <summary>
        ///  Угададали ли монету.
        /// null - еще не угадывали
        /// </summary>
        bool? IsCorrectGuess { get; }
        #endregion

        #region Бросок нескольких монет
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
        #endregion

        #region Сброс данных и сохранение данных
        /// <summary>
        /// Сброс данных. Обнуление AllCoinThrowCount, GuessCoinCount, CorrectlyGuessCoinCount, ThrowCount
        /// </summary>
        ICommand ResetData { get; }

        /// <summary>
        /// Сохранение данных
        /// </summary>
        void SaveData();
        #endregion
    }
}
