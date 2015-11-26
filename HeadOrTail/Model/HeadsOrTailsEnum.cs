using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadOrTail.Model
{
    /// <summary>
    /// Перечисление с орлом или решкой
    /// </summary>
    public enum HeadsOrTailsEnum : uint
    {
        /// <summary>
        /// Решка
        /// </summary>
        Tails = 0,

        /// <summary>
        /// Орел
        /// </summary>
        Heads = 1
    }
}
