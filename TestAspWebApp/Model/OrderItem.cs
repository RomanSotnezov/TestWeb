namespace TestAspWebApp.Model
{
    /// <summary>
    /// Строка заказа.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Код товара (ключ).
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Количество.
        /// </summary>
        public float Quantity { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Заполнить значения на основе объекта.
        /// </summary>
        public void SetValues(OrderItem source)
        {
            Code = source.Code;
            Description = source.Description;
            Quantity = source.Quantity;
            Price = source.Price;
        }
    }
}