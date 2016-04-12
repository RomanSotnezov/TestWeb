using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using TestAspWebApp.Model;

namespace TestAspWebApp.DAO
{
    /// <summary>
    /// Реализация источника данных - текстовые файлы.
    /// </summary>
    public class FileDataProvider : BaseDataProvider
    {
        /// <summary>
        /// Путь до файла с данными.
        /// </summary>
        private readonly string filePath;
        /// <summary>
        /// Разделитель.
        /// </summary>
        private const char SEPARATOR = ';';
        /// <summary>
        /// Заголовоу
        /// </summary>
        private const string CAPTION = "Код товара (ключ); Описание;Количество;Цена";

        public FileDataProvider()
        {
            filePath = HttpContext.Current.Server.MapPath("~/App_Data" + ConfigurationManager.AppSettings["FilePath"]);
        }

        public override void Save(IEnumerable<OrderItem> items)
        {
            var strArray = new List<string> {CAPTION};
            strArray.AddRange(items.Select(item => 
                item.Code.ToString(CultureInfo.InvariantCulture) + SEPARATOR + 
                item.Description + SEPARATOR + 
                item.Quantity + SEPARATOR + item.Price));
            File.WriteAllLines(filePath, strArray, Encoding.Default);
        }

        public override IEnumerable<OrderItem> Read()
        {
            var lines = File.ReadAllLines(filePath, Encoding.Default);
            return lines.Skip(1).Select(line => line.Split(SEPARATOR)).
                Select(t => new OrderItem
                {
                    Code = int.Parse(t[0]), 
                    Description = t[1],
                    Quantity = float.Parse(t[2], CultureInfo.InvariantCulture),
                    Price = float.Parse(t[3], CultureInfo.InvariantCulture)                 
                });
        }
    }
}