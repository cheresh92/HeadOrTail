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
    class TossCoin : ITossCoin
    {
        #region Constructors
        /// <summary>
        /// Реализация броска монеты
        /// </summary>
        /// <param name="_headsOrTailsModel">Модель</param>
        /// <param name="_statistic">Ссылка на статистику</param>
        public TossCoin(IHeadsOrTailsModel _headsOrTailsModel, Statistic _statistic)
        {
            headsOrTailsModel = _headsOrTailsModel;
            statistic = _statistic;

            ThrownCointAnimation = new AnimationSprite();
            ThrownCointAnimation.AnimationStoped += (sender, sprite) =>
            {
                SideOfCoin = sprite.HeadsOrTails;
                statistic.AllCoinThrowCount++;

                if (sprite.HeadsOrTails == HeadsOrTailsEnum.Heads)
                    statistic.HeadsCount++;
                else
                {
                    statistic.TailsCount++;
                }
            };

            #region Команда броска монеты
            var Command = new ViewModelCommand(ThrownCointAnimation, "IsStopped");
            // Бросить монету
            Command.Action = (o) => ThrowCoinMethod();
            //Выполнятся может когда анимация остановлена
            Command.Predicate = o => ThrownCointAnimation.IsStopped;
            ThrowCoinCommand = Command;
            #endregion

        }
        #endregion

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

        /// <summary>
        /// Команда броска монеты
        /// </summary>
        public ICommand ThrowCoinCommand { get; private set; }

        /// <summary>
        /// Анимация вращения монеты
        /// </summary>
        public AnimationSprite ThrownCointAnimation { get; private set; }
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
        
        #region private Methods
        /// <summary>
        /// Простой бросок монеты. 
        /// </summary>
        private void ThrowCoinMethod()
        {
            var tmp = headsOrTailsModel.ThrowCoin();
            SideOfCoin = null;
            // Начинаем анимацию. Обработка конца анимации находится в конструкторе
            ThrownCointAnimation.StartAnimation(tmp);
        }
        #endregion

        #region private Fileds

        private IHeadsOrTailsModel headsOrTailsModel;

        private Statistic statistic;

        #endregion
    }
}
