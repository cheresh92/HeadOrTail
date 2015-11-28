using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using HeadOrTail.Annotations;
using HeadOrTail.Model;

namespace HeadOrTail.ViewModel
{
    /// <summary>
    /// Неравномерная анимация. Анимация делится на несколько шагов, которые выполняются с разной скоросьтю
    /// </summary>
    public class AnimationSprite : INotifyPropertyChanged
    {

        #region private Feilds

        private DispatcherTimer timer;
        
        private Random r = new Random();
        
        /// <summary>
        /// Всего шагов
        /// </summary>
        private const int StepCount = 8;

        /// <summary>
        /// Текущий кадр анимации в спрайте
        /// </summary>
        private int currentFrame = 0;

        /// <summary>
        /// Текущий шаг анимации. Шаг меняется раз в <see cref="NumberOfFrames"/> / 2 раз.
        /// То есть, шаг меняется когда поялвется изображение "Орла" или "Решки".
        /// </summary>
        private int counterStep = 0;

        /// <summary>
        /// Количество тиков таймера в зависимости от текущего шага анимации.
        /// По сути, идет замедление анимации. Значения подбирались опытным путем и не
        /// связны с физическими законами
        /// </summary>
        private int[] counterTicksPerStep = {0, 0, 1, 1, 2, 2, 4, 4, 6, 6 };

        /// <summary>
        /// Счетчик отсчета тиков таймера. Нужен нам, так как анимация не равномерная
        /// </summary>
        private int counter = 0;

        /// <summary>
        /// Значение отвечающее за сдвиг шагов в зависимоти от выпашей монеты
        /// </summary>
        private int headsOrTailsNum;
        #endregion
        
        #region private Methods
        /// <summary>
        /// Таймер тикнул
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="o"></param>
        private void TimerTick(object sender, object o)
        {
            counter++;
            if (counter >= counterTicksPerStep[counterStep])    // Если счеткик больше или равен максимальному числу тиков в данном шаге
            {
                NextSprite();                                   // Меняем кадр
                if (IsNextStep)                                 // Настал ли следующий шаг
                {
                    counterStep++;                              // Увеличиваем счетчик
                    // Если прошли все шаги анимации. 
                    // headsOrTailsNum - отвечает за значение которое останится в конце анимации
                    if (counterStep == (StepCount - headsOrTailsNum))
                    {
                        timer.Stop();                           // Останавливаем таймер
                        IsStopped = true;                       // Помечаем это свойством
                        OnAnimationStoped();                    // Вызываем событие остановки
                    }
                }
                counter = 0;
            }
        }
        
        /// <summary>
        /// Следующий кадр
        /// </summary>
        private void NextSprite()
        {
            currentFrame++;
            if (currentFrame == NumberOfFrames)
                currentFrame = 0;

            XOffset = -FrameWidth * currentFrame;
        }
        #endregion
        
        /// <summary>
        /// Создать анимацию спрайта. Анимация работает в основном потоке, так как влияет на визульную состовляющую.
        /// Спрайт дожен быть горизонтальным озображением, где в начале находится "Орел" а в середине "Решка".
        /// </summary>
        /// <param name="frameWidth">Ширина кадра с монетой</param>
        /// <param name="numbersOfFrames">Колличество кадров</param>
        public AnimationSprite(double frameWidth = 200, int numbersOfFrames = 12)
        {
            FrameWidth = frameWidth;
            NumberOfFrames = numbersOfFrames;
            timer = new DispatcherTimer();
            timer.Tick += TimerTick;
            IsStopped = true;
        }

        #region Properties
        
        #region XOffset
        /// <summary>
        /// Смещение области видимости изображения по горизонтали
        /// </summary>
        private double _XOffset;
        public double XOffset
        {
            get { return _XOffset; }
            set
            {
                if (!Equals(_XOffset, value))
                {
                    _XOffset = value;
                    OnPropertyChanged("XOffset");
                }
            }
        }
        #endregion

        #region IsStopped
        /// <summary>
        /// Остановлена ли анимация
        /// </summary>
        private bool _IsStopped;

        public bool IsStopped
        {
            get { return _IsStopped; }
            set
            {
                if (!Equals(_IsStopped, value))
                {
                    _IsStopped = value;
                    OnPropertyChanged("IsStopped");
                }
            }
        }
        #endregion

        #region FrameWidth
        /// <summary>
        /// Ширина кадра в спрайте
        /// </summary>
        public double FrameWidth { get; set; }
        #endregion

        #region NumberOfFrames
        /// <summary>
        /// Количесвто кадров
        /// </summary>
        public int NumberOfFrames { get; set; }
        #endregion

        #region HeadsOrTails
        /// <summary>
        /// Монета которая должна была выпасть
        /// </summary>
        public HeadsOrTailsEnum HeadsOrTails { get; set; }
        #endregion

        #region IsNextStep
        /// <summary>
        /// Настал ли следующий шаг. Настает когда у нас 0-ой или средний кадр
        /// То есть кадр с орлом или решкой
        /// </summary>
        private bool IsNextStep
        {
            get
            {
                return (currentFrame == 0) || (currentFrame == (NumberOfFrames / 2));
            }
        }
        #endregion
        #endregion

        //TODO Передалать в Asinc?
        #region StartAnimation
        /// <summary>
        /// Начать анимацию броска монеты
        /// </summary>
        /// <param name="dropedCoin">Какая сторона монеты должна выпасть</param>
        public void StartAnimation(HeadsOrTailsEnum dropedCoin)
        {
            // Выбираем интервал времени. Значения 13 и 20 выбирались опытным путем
            timer.Interval = TimeSpan.FromMilliseconds(r.Next(13, 20));
            counter = 0;
            counterStep = 0;
            currentFrame = 0;
            headsOrTailsNum = dropedCoin == HeadsOrTailsEnum.Heads ? 0 : 1;
            IsStopped = false;
            HeadsOrTails = dropedCoin;
            timer.Start();
        }
        #endregion
        
        #region AnimationStoped Implementation
        /// <summary>
        /// Событие окончания анимации
        /// </summary>
        public event EventHandler<AnimationSprite> AnimationStoped;

        protected void OnAnimationStoped()
        {
            if (AnimationStoped != null)
            {
                AnimationStoped(this, this);
            }
        }
        #endregion

        #region INotifyPropertyChanged Implemintation
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
