using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Converters
{
    /// <summary>
    /// Класс вторичного преобразователя c выбором из таблицы  
    /// </summary>
    public class TableConverter : BaseConverter
    {
        public TableConverter(string name, string description) : base (name, description)
        {

        }
        /// <summary>
        /// Прямое преобразование x -> y
        /// </summary>
        /// <param name="x">Входная величина для преобразования</param>
        /// <returns>Сконвертированная величина</returns>
        public override double Convert(double x)
        {
            return 0.0;
        }

        /// <summary>
        /// Обратное преобразование y -> x
        /// </summary>
        /// <param name="y">Входная величина для обратного преобразования</param>
        /// <returns>Сконвертированная в обратном направлении величина</returns>
        public override double ConvertBack(double y)
        {
            return 0.0;
        }
    }
}
