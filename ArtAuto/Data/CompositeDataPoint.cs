using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtAuto.Data
{
    /// <summary>
    /// Интерфейс составной точки данных
    /// </summary>
    public interface ICompositeDataPoint
    {
        /// <summary>
        /// Число точек данных входящих в составную точку
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Добавить точку данных
        /// </summary>
        /// <param name="name">Идентификатор точки данных для добавления</param>
        /// <returns>true - если точка данных добавлена</returns>
        /// <returns>false  - если точка данных не была добавлена</returns>
        bool Add(string name);

        /// <summary>
        /// Удалить точку данных
        /// </summary>
        /// <param name="name">Идентификатор точки данных для удаления</param>
        /// <returns>true - если точка данных добавлена</returns>
        /// <returns>false  - если точка данных не была добавлена</returns>
        bool Remove(string name);

    }
}
