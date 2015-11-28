using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HeadOrTail.Model;
using HeadOrTail.ViewModel.Interfaces;

namespace HeadOrTail.ViewModel
{
    class TossSeveralCoins : ITossSeveralCoins
    {

        public TossSeveralCoins(IHeadsOrTailsModel _headsOrTailsModel, Statistic _statistic)
        {
            headsOrTailsModel = _headsOrTailsModel;
            statistic = _statistic;

            CollectionOfFlippedCoins = new ObservableCollection<HeadsOrTailsEnum>();


            #region Команда броска нескольких монет
            var Command = new ViewModelCommand(this, "ThrowCount");
            Command.Action = o => ThrowSeveralCoin();
            Command.Predicate = o => ThrowCount > 0;    // Если больше чем 0 бросков
            ThrowSeveralCoinCommand = Command;
            #endregion
        }


        #region Properties

        #region ThrowCount
        /// <summary>
        /// Сколько бросить монет
        /// </summary>
        private int _ThrowCount;

        public int ThrowCount
        {
            get { return _ThrowCount; }
            set
            {
                if (!Equals(_ThrowCount, value))
                {
                    _ThrowCount = value;
                    OnPropertyChanged("ThrowCount");
                }
            }
        }
        #endregion

        #region CollectionOfFlippedCoins
        /// <summary>
        /// Колекция после команды ThrowSeveralCoinCommand
        /// </summary>
        public ObservableCollection<HeadsOrTailsEnum> CollectionOfFlippedCoins { get; private set; }
        #endregion

        /// <summary>
        /// Команда броска нескольких монет
        /// </summary>
        public ICommand ThrowSeveralCoinCommand { get; private set; }

        #endregion


        private void ThrowSeveralCoin()
        {
            CollectionOfFlippedCoins.Clear();
            var flippedCoins = headsOrTailsModel.ThrowSeveralCoin(ThrowCount);
            foreach (HeadsOrTailsEnum headsOrTails in flippedCoins)
            {
                CollectionOfFlippedCoins.Add(headsOrTails);
            }

            statistic.AllCoinThrowCount += 1;
        }


        #region Реализация INotifyProperyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Возбудить событие PropertyChanged
        /// </summary>
        /// <param name="propertyName">Имя свойство которое изменилось</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        #region private Fileds

        private IHeadsOrTailsModel headsOrTailsModel;

        private Statistic statistic;

        #endregion
    }
}
