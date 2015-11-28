using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HeadOrTail.Model;
using HeadOrTail.ViewModel.Interfaces;

namespace HeadOrTail.ViewModel.Interfaces
{
    public interface IHeadsOrTailsViewModel
    {
        /*********************************************************************************
            Данный интерфейс делится на 4 части.
         * 1: Простой бросок монеты. Интересен только результат броска
         * 2: Бросок монетки с попыткой угадать монетку
         * 3: Бросок нескольких монет 
         * 4: Статистика всех бросков
        **********************************************************************************/
        
        /// <summary>
        /// Простой бросок монеты. Интересен только результат броска
        /// </summary>
        ITossCoin TossCoin { get; }

        /// <summary>
        /// Бросок монетки с попыткой угадать монетку
        /// </summary>
        IGuessToss GuessToss { get; }
        
        /// <summary>
        /// Бросок нескольких монет
        /// </summary>
        ITossSeveralCoins TossSeveralCoins { get; }

        /// <summary>
        /// Сброс данных и сохранение данных
        /// </summary>
        IStatistic Statistic { get; }
    }
}
