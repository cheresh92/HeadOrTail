using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadOrTail.Model
{
    interface IHeadsOrTailsModel
    {
        /// <summary>
        /// Бросить монетку
        /// </summary>
        /// <returns>Результат броска монеты</returns>
        HeadsOrTailsEnum ThrowCoin();

        /// <summary>
        /// Несколько раз бросить монету
        /// </summary>
        /// <param name="throwCount">Колечество бросков монеты</param>
        /// <returns>Результат бросков монеты</returns>
        List<HeadsOrTailsEnum> ThrowSeveralCoin(int throwCount);

        /// <summary>
        /// Попытка угадать монету
        /// </summary>
        /// <param name="guessSide">То что мы думаем выпадет</param>
        /// <param name="realSize">То что выпало</param>
        /// <returns>Результат выподения монеты</returns>
        bool TryToGuess(HeadsOrTailsEnum guessSide, out HeadsOrTailsEnum realSide);
    }
}
