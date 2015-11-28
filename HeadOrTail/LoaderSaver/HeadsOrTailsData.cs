using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeadOrTail.ViewModel;
using HeadOrTail.ViewModel.Interfaces;

namespace HeadOrTail.LoaderSaver
{
    /// <summary>
    /// Класс для сохранения данных
    /// </summary>
    public class HeadsOrTailsData
    {
        /// <summary>
        /// Колличество бросков монеты при попытки его угодать
        /// </summary>
        public uint GuessCoinCount;

        /// <summary>
        /// Колличество верно угаданных бросков
        /// </summary>
        public uint CorrectlyGuessCoinCount;

        /// <summary>
        /// Все броски монеты (считаются только броски, если бросить несколько монет, то засчитывается за 1)
        /// </summary>
        public uint AllCoinThrowCount;

        /// <summary>
        /// Колличество выпаших орлов
        /// </summary>
        public uint HeadsCount;

        /// <summary>
        /// Колличество выпавших решок
        /// </summary>
        public uint TailsCount;
        

        public HeadsOrTailsData(IStatistic statistic)
        {
            GuessCoinCount = statistic.GuessCoinCount;
            CorrectlyGuessCoinCount = statistic.CorrectlyGuessCoinCount;
            AllCoinThrowCount = statistic.AllCoinThrowCount;
            HeadsCount = statistic.HeadsCount;
            TailsCount = statistic.TailsCount;
        }

        public HeadsOrTailsData() { }

    }
}
