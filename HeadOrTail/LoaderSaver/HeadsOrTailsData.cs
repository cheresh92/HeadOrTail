using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        

        public HeadsOrTailsData(ViewModel.HeadsOrTailsViewModel headsOrTailsViewModel)
        {
            GuessCoinCount = headsOrTailsViewModel.GuessCoinCount;
            CorrectlyGuessCoinCount = headsOrTailsViewModel.CorrectlyGuessCoinCount;
            AllCoinThrowCount = headsOrTailsViewModel.AllCoinThrowCount;
        }

        public HeadsOrTailsData() { }

    }
}
