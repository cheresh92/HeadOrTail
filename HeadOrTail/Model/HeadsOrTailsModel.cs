using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadOrTail.Model
{
    /// <summary>
    /// Модель для игры Орел или решка
    /// </summary>
    class HeadsOrTailsModel : IHeadsOrTailsModel
    {
        #region private
        #region private Methods
        /// <summary>
        /// Получить True или False с шансами 50 на 50
        /// </summary>
        /// <returns></returns>
        bool RandomBool()
        {
            return random.Next() % 2 == 0;
        }
        #endregion

        #region private Field
        /// <summary>
        /// Генератор случайных чисел
        /// </summary>
        private Random random;
        #endregion
        #endregion

        #region Constructor
        public HeadsOrTailsModel()
        {
            // инициализировать новое значение
            random = new Random(DateTime.Now.Millisecond);
        }
        #endregion

        #region Реализация IHeadsOrTailsModel
        
        #region ThrowCoin
        /// <summary>
        /// Бросить монетку
        /// </summary>
        /// <returns>Результат броска монеты</returns>
        public HeadsOrTailsEnum ThrowCoin()
        {
            return RandomBool() ? HeadsOrTailsEnum.Heads : HeadsOrTailsEnum.Tails;
        }
        #endregion

        #region ThrowSeveralCoin
        /// <summary>
        /// Несколько раз бросить монету
        /// </summary>
        /// <param name="throwCount">Колечество бросков монеты</param>
        /// <returns>Результат бросков монеты</returns>
        public List<HeadsOrTailsEnum> ThrowSeveralCoin(int throwCount)
        {
            var listOfThrows = new List<HeadsOrTailsEnum>();
            for (int i = 0; i < throwCount; i++)
            {
                listOfThrows.Add(ThrowCoin());
            }
            return listOfThrows;
        }
        #endregion

        #region TryToGuess
        /// <summary>
        /// Попытка угадать монету
        /// </summary>
        /// <param name="guessSide">То что мы думаем выпадет</param>
        /// <param name="realSize">То что выпало</param>
        /// <returns>Результат выподения монеты</returns>
        public bool TryToGuess(HeadsOrTailsEnum guessSide, out HeadsOrTailsEnum realSize)
        {
            realSize = ThrowCoin();
            return (guessSide == realSize);
        }
        #endregion
        #endregion
    }
}
