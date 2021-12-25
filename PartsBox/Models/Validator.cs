using System;

namespace PartsBox.Models
{
    /// <summary>
    /// Проводит проверку данных для класса параметров.
    /// <seealso cref="PartsBoxParameters"/>
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Минимальный размер одной ячейки в мм.
        /// </summary>
        private const double MinCellWidht = 10.0;

        /// <summary>
        /// Проверяет, входит ли значение в указанный диапазон (включительно).
        /// </summary>
        /// <param name="min">Начало диапазона.</param>
        /// <param name="max">Конец диапазона.</param>
        /// <param name="value">Значение для сравнения.</param>
        /// <returns>True - входит, False - не входит в диапазон.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool ValidateRange(double min, double max, double value)
        {
            if(max < min)
            {
                throw new ArgumentException($"Argument {nameof(min)} " +
                                            $"must be less than {nameof(max)}.");
            }
            return min <= value && value <= max;
        }

        /// <summary>
        /// Проверяет минимальный размер одной ячейки при текущих параметрах.
        /// </summary>
        /// <param name="oneCellSizeCalculationFunc">Функция расчета размера длины или
        /// ширины одной ячейки.</param>
        /// <param name="dimensionSize">Длина или ширина коробки.</param>
        /// <param name="innerWallWidth">Ширина внутренних стенок.</param>
        /// <param name="outerWallWidth">Ширина внешних стенок.</param>
        /// <param name="userValue">Пользовательское значение.</param>
        /// <returns>True если число ячеек позволяет их построить с соблюдением условия
        /// минимальной ширины одной ячейки.</returns>
        public static bool ValidateCellsNumber(
            Func<double, double, double, int, double> oneCellSizeCalculationFunc,
            double dimensionSize, double innerWallWidth, double outerWallWidth, int userValue)
        {
            var oneCellWidth =
                oneCellSizeCalculationFunc(dimensionSize, innerWallWidth, outerWallWidth, userValue);
            return oneCellWidth > MinCellWidht;
        }
    }
}
