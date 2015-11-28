using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HeadOrTail.LoaderSaver;
using HeadOrTail.Model;
using HeadOrTail.ViewModel.Interfaces;

namespace HeadOrTail.ViewModel
{
    public class HeadsOrTailsViewModel : IHeadsOrTailsViewModel
    {
        private IHeadsOrTailsModel headsOrTailsModel;
        
        public HeadsOrTailsViewModel()
        {
            headsOrTailsModel = new HeadsOrTailsModel();
            var statistic = new Statistic();
            TossCoin = new TossCoin(headsOrTailsModel, statistic);
            GuessToss = new GuessToss(headsOrTailsModel, statistic);
            TossSeveralCoins = new TossSeveralCoins(headsOrTailsModel, statistic);
            Statistic = statistic;
        }

        public ITossCoin TossCoin { get; private set; }

        public IGuessToss GuessToss { get; private set; }

        public ITossSeveralCoins TossSeveralCoins { get; private set; }

        public IStatistic Statistic { get; private set; }
        
    }
}
