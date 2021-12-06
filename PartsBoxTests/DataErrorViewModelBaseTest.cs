using System.Linq;
using NUnit.Framework;
using PartsBox.ViewModels;

namespace PartsBoxTests
{
    /// <summary>
    /// Тесты для <see cref="DataErrorViewModelBase"/>.
    /// </summary>
    [TestFixture]
    public class DataErrorViewModelBaseTest
    {
        private static DataErrorViewModelBase DataErrorViewModelBase => new DataErrorViewModelBase();

        [TestCase(null,TestName = "Позитивный тест базового класса модели представления с реализацией" +
                                  " INotifyDataErrorInfo")]
        public void GetErrors_BasePositiveTest(string propertyName)
        {
            //Arrange
            var viewModel = DataErrorViewModelBase;
            var expected = Enumerable.Empty<object>();

            //Act
            var actual = viewModel.GetErrors(propertyName);

            //Assert
            Assert.AreEqual(expected, actual, "Полученный список ошибок отличается от ожидаемого.");
        }
    }
}
