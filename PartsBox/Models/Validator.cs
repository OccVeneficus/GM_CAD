using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsBox.Models
{
    /// <summary>
    /// Проводит проверку данных.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Проверяет, входит ли значение в указанный диапазон (включительно).
        /// </summary>
        /// <param name="min">Начало диапазона.</param>
        /// <param name="max">Конец диапазона.</param>
        /// <param name="value">Значение для сравнения.</param>
        /// <returns>True - входит, False - не входит в диапазон.</returns>
        public static bool ValidateRange(double min, double max, double value)
        {
            return min <= value && value <= max;
        }
    }
}
