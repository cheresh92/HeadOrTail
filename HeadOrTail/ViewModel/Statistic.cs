using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HeadOrTail.LoaderSaver;
using HeadOrTail.Model;
using HeadOrTail.ViewModel.Interfaces;

namespace HeadOrTail.ViewModel
{
    /// <summary>
    /// Реализации интерфейса статистики сбора данных о броске монет
    /// </summary>
    class Statistic : IStatistic
    {
        private LoaderSaver.LoaderSaver loaderSaver;

        public Statistic()
        {
            var Command = new ViewModelCommand();
            Command.Predicate = (o) => true;
            Command.Action = (o) =>
            {
                HeadsCount              = 0;
                TailsCount              = 0;
                AllCoinThrowCount       = 0;
                GuessCoinCount          = 0;
                CorrectlyGuessCoinCount = 0;
            };

            ResetData = Command;
            loaderSaver = new LoaderSaver.LoaderSaver();

            var data = loaderSaver.Load();

            HeadsCount              = data.HeadsCount             ;
            TailsCount              = data.TailsCount             ;
            AllCoinThrowCount       = data.AllCoinThrowCount      ;
            GuessCoinCount          = data.GuessCoinCount         ;
            CorrectlyGuessCoinCount = data.CorrectlyGuessCoinCount;
        }


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
        
        #region HeadsCount
        /// <summary>
        ///  Количество выпаших орлов
        /// </summary>
        private uint _HeadsCount;
        public uint HeadsCount
        {
            get { return _HeadsCount; }
            set
            {
                if (!Equals(_HeadsCount, value))
                {
                    _HeadsCount = value;
                    OnPropertyChanged("HeadsCount");
                }
            }
        }

        #endregion

        #region TailsCount
        /// <summary>
        ///  Количество выпаших решек
        /// </summary>
        private uint _TailsCount;
        public uint TailsCount
        {
            get { return _TailsCount; }
            set
            {
                if (!Equals(_TailsCount, value))
                {
                    _TailsCount = value;
                    OnPropertyChanged("TailsCount");
                }
            }
        }
        #endregion

        /// <summary>
        /// Сброс данных. Обнуление AllCoinThrowCount, GuessCoinCount, CorrectlyGuessCoinCount, ThrowCount
        /// </summary>
        public ICommand ResetData { get; private set; }

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

        public void Save()
        {
            loaderSaver.Save(new HeadsOrTailsData(this));
        }
    }
}
