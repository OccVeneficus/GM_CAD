using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PartsBox.Models;

namespace PartsBoxTests
{
    /// <summary>
    /// Тесты для <see cref="CellInfo"/>.
    /// </summary>
    [TestFixture]
    public class TestCellInfo
    {
        /// <summary>
        /// Экземпляр информации о клетке для тестирования.
        /// </summary>
        private static CellInfo Cell => new CellInfo();

        [TestCase(true, TestName = "Проверка получения корректного значения свойства IsMerge.")]
        public void TestIsMergeProperty_CorrectGetValue(bool correctTestValue)
        {
            // Arrange
            var cell = Cell;
            var value = correctTestValue;
            var expected = correctTestValue;

            // Act
            cell.IsMerge = value;
            var actual = cell.IsMerge;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(Cell.IsMerge)} вернул некорректное значение.");
        }

        [TestCase(true, TestName = "Проверка получения корректного значения свойства HasNeighbor.")]
        public void TestHasNeighborProperty_CorrectGetValue(bool correctTestValue)
        {
            // Arrange
            var cell = Cell;
            var value = correctTestValue;
            var expected = correctTestValue;

            // Act
            cell.HasNeighbor = value;
            var actual = cell.HasNeighbor;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(Cell.HasNeighbor)} вернул некорректное значение.");
        }

        [TestCase(1, 1, TestName = "Проверка получения корректного значения свойства Index.")]
        public void TestIndexProperty_CorrectGetValue(int height, int width)
        {
            // Arrange
            var cell = Cell;
            var value = (height, width);
            var expected = (height, width);

            // Act
            cell.Index = value;
            var actual = cell.Index;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(Cell.Index)} вернул некорректное значение.");
        }
    }
}
