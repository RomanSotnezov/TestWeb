using System.Collections.Generic;
using TestAspWebApp.Model;

namespace TestAspWebApp.DAO
{
    /// <summary>
    /// Интерфейс доступа к данным по строкам заказа.
    /// </summary>
    interface IDataProvider
    {
        /// <summary>
        /// Добавить новые строки заказа.
        /// </summary>
        void Add(IEnumerable<OrderItem> items);
        /// <summary>
        /// Обновить существующие строки заказа.
        /// </summary>
        void Save(IEnumerable<OrderItem> items);
        /// <summary>
        /// Прочить из источника строки заказа.
        /// </summary>
        IEnumerable<OrderItem> Read();
    }
}
