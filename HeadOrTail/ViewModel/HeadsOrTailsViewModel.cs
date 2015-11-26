using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HeadOrTail.Annotations;
using HeadOrTail.LoaderSaver;
using HeadOrTail.Model;

namespace HeadOrTail.ViewModel
{
    public class HeadsOrTailsViewModel : IHeadsOrTailsViewModel
    {
        private IHeadsOrTailsModel headsOrTailsModel;
        private LoaderSaver.LoaderSaver loaderSaver;

        public AnimationSprite AnimationSprite { get; set; }


        public HeadsOrTailsViewModel()
        {
            headsOrTailsModel = new HeadsOrTailsModel();
            CollectionOfFlippedCoins = new ObservableCollection<HeadsOrTailsEnum>();
            loaderSaver = new LoaderSaver.LoaderSaver();

            AnimationSprite = new AnimationSprite();
            AnimationSprite.AnimationStoped += (sender, sprite) =>
            {
                SideOfCoin = sprite.HeadsOrTails;
                AllCoinThrowCount += 1;
            };
            
            #region Команда броска монеты
            var Command = new ViewModelCommand(AnimationSprite, "IsStopped");
            // Бросить монету
            Command.Action = (o) => ThrowCoin();
            //Выполнятся может когда анимация остановлена
            Command.Predicate = o => AnimationSprite.IsStopped;
            ThrowCoinCommand = Command;
            #endregion

            #region Команда броска угадывания монет
            Command = new ViewModelCommand(this, "GuessSideOfCoin");
            Command.Action = o => TryToGuess();
            Command.Predicate = o => GuessSideOfCoin != null;   // Если монета выбрана
            TryToGuessCommand = Command;
            #endregion

            #region Команда броска нескольких монет
            Command = new ViewModelCommand(this, "ThrowCount");
            Command.Action = o => ThrowSeveralCoin();
            Command.Predicate = o => ThrowCount > 0;    // Если больше чем 0 бросков
            ThrowSeveralCoinCommand = Command;
            #endregion

            // Загружаем данные
            var data = loaderSaver.Load();
            AllCoinThrowCount = data.AllCoinThrowCount;
            CorrectlyGuessCoinCount = data.CorrectlyGuessCoinCount;
            GuessCoinCount = data.GuessCoinCount;

            

        }


        #region Properties

        #region SideOfCoin

        /// <summary>
        /// Выпавшая сторона монеты
        /// </summary>
        private HeadsOrTailsEnum? _SideOfCoin;
        public HeadsOrTailsEnum? SideOfCoin
        {
            get { return _SideOfCoin; }
            set
            {
                if (!Equals(_SideOfCoin, value))
                {
                    _SideOfCoin = value;
                    OnPropertyChanged("SideOfCoin");
                }
            }
        }

        #endregion
        
        #region GuessSideOfCoin
        /// <summary>
        /// Сторона которую мы вибираем для угадывания
        /// </summary>
        private HeadsOrTailsEnum?  _GuessSideOfCoin;
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
        
        #region AllCoinThrowCount
        /// <summary>
        /// Все броски монеты (считаются только броски, если бросить несколько монет, то засчитывается за 1)
        /// </summary>
        private uint _AllCoinThrowCount;
        public uint AllCoinThrowCount
        {
            get { return _AllCoinThrowCount; }
            set
            {
                if (!Equals(_AllCoinThrowCount, value))
                {
                    _AllCoinThrowCount = value;
                    OnPropertyChanged("AllCoinThrowCount");
                }
            }
        }
        #endregion

        #region GuessCoinCount
        /// <summary>
        /// Броски монеты при попытки его угодать
        /// </summary>
        private uint _GuessCoinCount;
        public uint GuessCoinCount
        {
            get { return _GuessCoinCount; }
            set
            {
                if (!Equals(_GuessCoinCount, value))
                {
                    _GuessCoinCount = value;
                    OnPropertyChanged("GuessCoinCount");
                }
            }
        }
        #endregion

        #region CorrectlyGuessCoinCount
        /// <summary>
        /// Колличество верно угаданных бросков
        /// </summary>
        private uint _CorrectlyGuessCoinCount;
        public uint CorrectlyGuessCoinCount
        {
            get { return _CorrectlyGuessCoinCount; }
            set
            {
                if (!Equals(_CorrectlyGuessCoinCount, value))
                {
                    _CorrectlyGuessCoinCount = value;
                    OnPropertyChanged("CorrectlyGuessCoinCount");
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

        #region CollectionOfFlippedCoins
        /// <summary>
        /// Колекция после команды ThrowSeveralCoinCommand
        /// </summary>
        public ObservableCollection<HeadsOrTailsEnum> CollectionOfFlippedCoins { get; private set; }
        #endregion

        #endregion

        #region Commands
        /// <summary>
        /// Команда броска монеты
        /// </summary>
        public ICommand ThrowCoinCommand { get; private set; }
        
        /// <summary>
        /// Команда броска нескольких монет
        /// </summary>
        public ICommand ThrowSeveralCoinCommand { get; private set; }

        /// <summary>
        /// Команда попытки угадывания стороны монеты
        /// </summary>
        public ICommand TryToGuessCommand { get; private set; }

        /// <summary>
        /// Сброс данных. Обнуление AllCoinThrowCount, GuessCoinCount, CorrectlyGuessCoinCount, ThrowCount
        /// </summary>
        public ICommand ResetData { get; private set; }
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

        #region Private Methods


        /// <summary>
        /// Простой бросок монеты. 
        /// </summary>
        private void ThrowCoin()
        {
            var tmp = headsOrTailsModel.ThrowCoin();
            SideOfCoin = null;
            // Начинаем анимацию. Обработка конца анимации находится в конструкторе
            AnimationSprite.StartAnimation(tmp);
        }

        private void TryToGuess()
        {
            HeadsOrTailsEnum droppedSide;
            IsCorrectGuess = headsOrTailsModel.TryToGuess((HeadsOrTailsEnum)GuessSideOfCoin, out droppedSide);
            GuessCoinCount += 1;
            if ((bool)IsCorrectGuess)
            {
                CorrectlyGuessCoinCount += 1;
            }
            SideOfCoin = droppedSide;
        }
        
        private void ThrowSeveralCoin()
        {
            CollectionOfFlippedCoins.Clear();
            var flippedCoins = headsOrTailsModel.ThrowSeveralCoin(ThrowCount);
            foreach (HeadsOrTailsEnum headsOrTails in flippedCoins)
            {
                CollectionOfFlippedCoins.Add(headsOrTails);
            }

            AllCoinThrowCount += 1;
        }

        #endregion

        public void SaveData()
        {
            var data = new HeadsOrTailsData(this);
            loaderSaver.Save(data);

        }


    }
}
