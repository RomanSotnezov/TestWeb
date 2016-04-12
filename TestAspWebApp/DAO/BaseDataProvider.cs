using System.Collections.Generic;
using System.Linq;
using TestAspWebApp.Model;

namespace TestAspWebApp.DAO
{
    /// <summary>
    /// Абстрактный класс доступа к источнику данных.
    /// </summary>
    public abstract class BaseDataProvider : IDataProvider
    {
        public void Add(IEnumerable<OrderItem> items)
        {
            var list = Read().ToList();
            foreach (var i in items)
            {
                var find = list.FirstOrDefault(f => Equals(f.Code, i.Code));
                if (find != null)
                {
                    find.Description = i.Description;
                    find.Price = i.Price;
                    find.Quantity = i.Quantity;
                }
                else list.Add(i);
            }
            Save(list);
        }
        public abstract void Save(IEnumerable<OrderItem> items);
        public abstract IEnumerable<OrderItem> Read();
    }
}