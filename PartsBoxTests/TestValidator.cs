using System;
using NUnit.Framework;
using PartsBox.Models;

namespace PartsBoxTests
{
    [TestFixture]
    public class TestValidator
    {
        /// <summary>
        /// Экземпляр параметров коробки для тестирования.
        /// </summary>
        private PartsBoxParameters BoxParameters => new PartsBoxParameters();

        /// <summary>
        /// Экземпляр параметров коробки с неверными значениями в свойствах.
        /// </summary>
        private static PartsBoxParameters WrongBoxParameters => new PartsBoxParameters
        {
            Width = 149.0,
            Length = 149.0,
            Height = 1000.0,
            InnerWallWidth = 50.0,
            OuterWallWidth = 50.0,
            BoxBottomWidth = 1400.0,
            CellsInLength = 200,
            CellsInWidth = 200
        };

        [TestCase(20, false, TestName = "Негативный тест валидации количества ячеек в коробке.")]
        [TestCase(2, true, TestName = "Позитивный тест валидации количества ячеек в коробке.")]
        public void TestValidateCellsNumbers_WrongValues(int userValue, bool expected)
        {
            // Arrange
            var partsBoxParameters = BoxParameters;

            // Act
            var actual = Validator.ValidateCellsNumber(BoxParameters.CalculateOneCellSize,partsBoxParameters.Length,
                partsBoxParameters.InnerWallWidth, partsBoxParameters.OuterWallWidth, userValue);

            // Assert
            Assert.AreEqual(expected, actual, "Валидация проведена неверно.");
        }

        [TestCase(TestName = "Проверяет срабатывание исключения при случае, когда " +
                             "минимум больше максимума.")]
        public void TestValidateRange_ThrowsException()
        {
            //Arrange
            const double min = 100.0;
            const double max = 10.0;
            const double value = 50.0;

            //Assert
            Assert.Throws<ArgumentException>(() => Validator.ValidateRange(min, max, value));
        }
    }
}
