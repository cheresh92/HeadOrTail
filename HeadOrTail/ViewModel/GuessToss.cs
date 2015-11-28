using System;
using System.Collections.Generic;
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
    class GuessToss : IGuessToss
    {
        
        #region Constructors
        /// <summary>
        /// Реализация угадования броска монеты
        /// </summary>
        /// <param name="_headsOrTailsModel">Модель</param>
        /// <param name="_statistic">Ссылка на статистику</param>
        public GuessToss(IHeadsOrTailsModel _headsOrTailsModel, Statistic _statistic)
        {
            headsOrTailsModel = _headsOrTailsModel;
            statistic = _statistic;

            GuessCointAnimation = new AnimationSprite();
            GuessCointAnimation.AnimationStoped += (sender, sprite) =>
            {
                throw new Exception();
            };
            
            #region Команда броска угадывания монет
            var Command = new ViewModelCommand(this, "GuessSideOfCoin");
            Command.Action = o => TryToGuess();
            Command.Predicate = o => GuessSideOfCoin != null;   // Если монета выбрана
            TryToGuessCommand = Command;
            #endregion
        }
        #endregion

        #region Public Properties

        #region GuessSideOfCoin
        /// <summary>
        /// Сторона которую мы вибираем для угадывания
        /// </summary>
        private HeadsOrTailsEnum? _GuessSideOfCoin;
        public HeadsOrTailsEnum? GuessSideOfCoin
        {
            get { return _GuessSideOfCoin; }
            set
            {
                if (!Equals(_GuessSideOfCoin, value))
                {
                    _GuessSideOfCoin = value;
                    OnPropertyChanged("GuessSideOfCoin");
                }
            }
        }
        #endregion
        
        #region IsCorrectGuess
        /// <summary>
        ///  Угададали ли монету.
        /// null - еще не угадывали
        /// </summary>
        private bool? _IsCorrectGuess;

        public bool? IsCorrectGuess
        {
            get { return _IsCorrectGuess; }
            set
            {
                if (!Equals(_IsCorrectGuess, value))
                {
                    _IsCorrectGuess = value;
                    OnPropertyChanged("IsCorrectGuess");
                }
            }
        }
        #endregion

        #region GuessCointAnimation
        /// <summary>
        /// Анимация броска монеты
        /// </summary>
        public AnimationSprite GuessCointAnimation { get; private set; }
        #endregion

        #region TryToGuessCommand
        /// <summary>
        /// Команда попытки угадывания стороны монеты
        /// </summary>
        public ICommand TryToGuessCommand { get; private set; }
        #endregion

        #endregion

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

        #region private Methods
        private void TryToGuess()
        {
            HeadsOrTailsEnum droppedSide;
            IsCorrectGuess = headsOrTailsModel.TryToGuess((HeadsOrTailsEnum)GuessSideOfCoin, out droppedSide);
            statistic.GuessCoinCount ++;
            if ((bool)IsCorrectGuess)
            {
                statistic.CorrectlyGuessCoinCount++;
            }
        }
        #endregion
    }
}
