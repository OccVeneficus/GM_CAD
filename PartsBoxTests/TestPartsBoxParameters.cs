using System.Dynamic;
using NUnit.Framework;
using PartsBox.Models;

namespace PartsBoxTests
{
    /// <summary>
    /// ����� ��� <see cref="PartsBoxParameters"/>.
    /// </summary>
    [TestFixture]
    public class TestPartsBoxParameters
    {
        /// <summary>
        /// ��������� ���������� ������� ��� ������������.
        /// </summary>
        private static PartsBoxParameters BoxParameters => new PartsBoxParameters();

        /// <summary>
        /// ��������� ���������� ������� � ��������� ���������� � ���������.
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

        [TestCase(50.0, TestName = "�������� ��������� ����������� �������� �������� Height.")]
        public void TestBoxHeigthProperty_CorrectGetValue(double correctTestValue)
        {
            // Arrange
            var boxParameters = BoxParameters;
            var value = correctTestValue;
            var expected = correctTestValue;

            // Act
            boxParameters.Height = value;
            var actual = boxParameters.Height;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.Height)} ������ ������������" +
                                              $" ��������.");
        }

        [TestCase(50.0, TestName = "�������� ��������� ����������� �������� �������� Width.")]
        public void TestBoxWidthProperty_CorrectGetValue(double correctTestValue)
        {
            // Arrange
            var boxParameters = BoxParameters;
            var value = correctTestValue;
            var expected = correctTestValue;

            // Act
            boxParameters.Width = value;
            var actual = boxParameters.Width;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.Width)} ������ ������������ " +
                                              $"��������.");
        }

        [TestCase(50.0, TestName = "�������� ��������� ����������� �������� �������� Length.")]
        public void TestBoxLengthProperty_CorrectGetValue(double correctTestValue)
        {
            // Arrange
            var boxParameters = BoxParameters;
            var value = correctTestValue;
            var expected = correctTestValue;

            // Act
            boxParameters.Length = value;
            var actual = boxParameters.Length;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.Length)} ������ ������������ " +
                                              $"��������.");
        }

        [TestCase(50.0, TestName = "�������� ��������� ����������� �������� �������� OuterWallWidth.")]
        public void TestBoxOuterWallWidthProperty_CorrectGetValue(double correctTestValue)
        {
            // Arrange
            var boxParameters = BoxParameters;
            var value = correctTestValue;
            var expected = correctTestValue;

            // Act
            boxParameters.OuterWallWidth = value;
            var actual = boxParameters.OuterWallWidth;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.OuterWallWidth)} ������ " +
                                              $"������������ ��������.");
        }

        [TestCase(50.0, TestName = "�������� ��������� ����������� �������� �������� InnerWallWidth.")]
        public void TestBoxInnerWallWidthProperty_CorrectGetValue(double correctTestValue)
        {
            // Arrange
            var boxParameters = BoxParameters;
            var value = correctTestValue;
            var expected = correctTestValue;

            // Act
            boxParameters.InnerWallWidth = value;
            var actual = boxParameters.InnerWallWidth;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.InnerWallWidth)} ������ " +
                                              $"������������ ��������.");
        }

        [TestCase(50.0, TestName = "�������� ��������� ����������� �������� �������� BoxBottomWidth.")]
        public void TestBoxBoxBottomWidthProperty_CorrectGetValue(double correctTestValue)
        {
            // Arrange
            var boxParameters = BoxParameters;
            var value = correctTestValue;
            var expected = correctTestValue;

            // Act
            boxParameters.BoxBottomWidth = value;
            var actual = boxParameters.BoxBottomWidth;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.BoxBottomWidth)} ������" +
                                              $" ������������ ��������.");
        }

        [TestCase(1, TestName = "�������� ��������� ����������� �������� �������� CellsInWidth.")]
        public void TestBoxCellsInWidthProperty_CorrectGetValue(int correctTestValue)
        {
            // Arrange
            var boxParameters = BoxParameters;
            var value = correctTestValue;
            var expected = correctTestValue;

            // Act
            boxParameters.CellsInWidth = value;
            var actual = boxParameters.CellsInWidth;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.CellsInWidth)} ������" +
                                              $" ������������ ��������.");
        }

        [TestCase(1, TestName = "�������� ��������� ����������� �������� �������� CellsInLength.")]
        public void TestBoxCellsInLengthProperty_CorrectGetValue(int correctTestValue)
        {
            // Arrange
            var boxParameters = BoxParameters;
            var value = correctTestValue;
            var expected = correctTestValue;

            // Act
            boxParameters.CellsInLength = value;
            var actual = boxParameters.CellsInLength;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.CellsInLength)} ������" +
                                              $" ������������ ��������.");
        }

        [TestCase(69.0,TestName = "��������� ������������ ������� ������ ����� ������.")]
        public void TestGetOneCellWidthProperty_CorrectValue(double correctTestValue)
        {
            // Arrange
            var boxParameters = BoxParameters;
            boxParameters.CellsInLength = 2;
            boxParameters.CellsInWidth = 2;
            var expected = correctTestValue;

            // Act
            var actual = boxParameters.GetOneCellWidth;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.GetOneCellWidth)} �����������" +
                                              $" ��������� ������ ����� ������.");
        }

        [TestCase(69.0, TestName = "��������� ������������ ������� ������ ����� ������.")]
        public void TestGetOneCellLengthProperty_CorrectValue(double correctTestValue)
        {
            // Arrange
            var boxParameters = BoxParameters;
            boxParameters.CellsInLength = 2;
            boxParameters.CellsInWidth = 2;
            var expected = correctTestValue;

            // Act
            var actual = boxParameters.GetOneCellLength;

            // Assert
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.GetOneCellLength)} �����������" +
                                              $" ��������� ������ ����� ������.");
        }

        [TestCase("", TestName = "�������� ������ GetErrors � propertyName ������ ������ ������")]
        [TestCase(null, TestName = "�������� ������ GetErrors � propertyName ������ null")]
        public void TestGetErrors_NoErrorsValue(string testPropertyName)
        {
            // Arrange
            var boxParameters = BoxParameters;

            // Act
            var actual = boxParameters.GetErrors(testPropertyName);

            // Assert
            Assert.IsEmpty(actual, "����� ������ �� ������ ����� ��������� �� ������� ��� ����������" +
                                   " ������.");
        }


        [TestCase(nameof(PartsBoxParameters.Height), "Height must be between 50 and 150 mm.",
            TestName = "�������� ������ GetErrors � propertyName ������ Height � �������� ������ ������.")]
        [TestCase(nameof(PartsBoxParameters.Length), "Length must be between 150 and 700 mm.",
            TestName = "�������� ������ GetErrors � propertyName ������ Length � �������� ������ ������.")]
        [TestCase(nameof(PartsBoxParameters.Width), "Width must be between 150 and 700 mm.",
            TestName = "�������� ������ GetErrors � propertyName ������ Width � �������� ������ ������.")]
        [TestCase(nameof(PartsBoxParameters.OuterWallWidth), "OuterWallWidth must be between 5 and 10 mm.",
            TestName = "�������� ������ GetErrors � propertyName ������ OuterWallWidth � �������� ������ ������.")]
        [TestCase(nameof(PartsBoxParameters.InnerWallWidth), "InnerWallWidth must be between 2 and 5 mm.",
            TestName = "�������� ������ GetErrors � propertyName ������ InnerWallWidth � �������� ������ ������.")]
        [TestCase(nameof(PartsBoxParameters.BoxBottomWidth), "BoxBottomWidth must be between 5 and 10 mm.",
            TestName = "�������� ������ GetErrors � propertyName ������ BoxBottomWidth � �������� ������ ������.")]
        [TestCase(nameof(PartsBoxParameters.CellsInLength), "CellsInLength incorrect ",
            TestName = "�������� ������ GetErrors � propertyName ������ CellsInLength � �������� ������ ������.")]
        [TestCase(nameof(PartsBoxParameters.CellsInWidth), "CellsInWidth incorrect ",
            TestName = "�������� ������ GetErrors � propertyName ������ CellsInWidth � �������� ������ ������.")]
        public void TestGetErrors_HaveErrorsValue(string testPropertyName, string correctErrorString)
        {
            // Arrange
            var boxParameters = WrongBoxParameters;

            // Act
            foreach (var error in boxParameters.GetErrors(testPropertyName))
            {
                var actual = error as string;
                // Assert
                Assert.AreEqual(correctErrorString, actual, "������ � ������� �� ����� ���������.");
            }

        }

        [TestCase(TestName = "�������� ������ ��������� ����������� �������� ���������.")]
        public void TestSetDefaultValues_Positive()
        {
            // Arrange
            var defaultBoxParametes = BoxParameters;
            var modifiedBoxParameters = WrongBoxParameters;

            // Act
            modifiedBoxParameters.SetDefaultValues();

            // Assert
            Assert.AreEqual(defaultBoxParametes, modifiedBoxParameters, "����������� ��������� " +
                                                                        "����������� �������.");
        }

        [TestCase(TestName = "�������� ���������� ��������� �������� ���������" +
                             " ���� ����������� ������� � ������������ ���������.")]
        public void TestEquals_NullValue()
        {
            // Arrange
            var defaultBoxParametes = BoxParameters;

            // Act
            var areEqual = defaultBoxParametes.Equals(null);

            // Assert
            Assert.False(areEqual, "������������ ��������� ��������� ������� � null.");
        }

        [TestCase(4, 4, 33.5,TestName = "���������� ���� ������ ��������� ����� ����� ������.")]
        public void TestGetOneCellSize_PositiveAndNegative(int cellsInLength, int cellsInWidth,
            double expected)
        {
            //Assert
            var boxParameters = BoxParameters;
            boxParameters.CellsInLength = cellsInLength;
            boxParameters.CellsInWidth = cellsInWidth;

            //Act
            var actual = boxParameters.CalculateOneCellSize(boxParameters.Length,
                boxParameters.OuterWallWidth, boxParameters.InnerWallWidth,
                boxParameters.CellsInLength);

            //Assert
            Assert.AreEqual(expected, actual, "������������ ����� ������ �� ������� � ���������.");
        }
    }
}