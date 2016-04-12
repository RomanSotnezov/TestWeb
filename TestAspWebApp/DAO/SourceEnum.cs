using System.ComponentModel;
using System.IO;

namespace TestAspWebApp.DAO
{
    /// <summary>
    /// Тип источника данных.
    /// </summary>
    public enum SourceTypes
    {
        /// <summary>
        /// Файл.
        /// </summary>
        File,
        /// <summary>
        /// База данных.
        /// </summary>
        DataBase 
    }

    public static class SourceTypesExtensions
    {
        /// <summary>
        /// Преобразовать enum в пользовательский вид. Можно было сделать через атрибуты самого enum.
        /// </summary>
        public static string ToGUIString(this SourceTypes sourceType)
        {
            switch (sourceType)
            {
                case SourceTypes.File:
                    return "Файл";
                case SourceTypes.DataBase:
                    return "БД";
                default:
                    throw new InvalidEnumArgumentException("Invalid enum value: " + sourceType);

            }
        }

        /// <summary>
        /// По строке пользовательского представления получить значение enum.
        /// </summary>
        /// <param name="sourceString"></param>
        public static SourceTypes ParseFromGuiString(string sourceString)
        {
            if (sourceString.Equals("Файл"))
                return SourceTypes.File;
            if (sourceString.Equals("БД"))
                return SourceTypes.DataBase;
            throw new InvalidDataException("Invalid GUI representation of enum SourceTypes: " + sourceString);
        }
    }
}