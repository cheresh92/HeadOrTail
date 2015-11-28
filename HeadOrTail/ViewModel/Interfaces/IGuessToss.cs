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
    public interface IGuessToss : INotifyPropertyChanged
    {
        /// <summary>
        /// Команда попытки угадывания стороны монеты
        /// </summary>
        ICommand TryToGuessCommand { get; }

        /// <summary>
        /// Сторона которую мы вибираем для угадывания
        /// </summary>
        HeadsOrTailsEnum? GuessSideOfCoin { get; }

        /// <summary>
        ///  Угададали ли монету.
        /// null - еще не угадывали
        /// </summary>
        bool? IsCorrectGuess { get; }

        /// <summary>
        /// Анимация броска монеты
        /// </summary>
        AnimationSprite GuessCointAnimation { get; }
    }
}
