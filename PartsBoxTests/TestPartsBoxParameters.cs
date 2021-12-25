using NUnit.Framework;
using PartsBox.Models;

namespace PartsBoxTests
{
    //TODO: кодировка
    /// <summary>
    /// Òåñòû äëÿ <see cref="PartsBoxParameters"/>.
    /// </summary>
    [TestFixture]
    public class TestPartsBoxParameters
    {
        /// <summary>
        /// Ýêçåìïëÿð ïàðàìåòðîâ êîðîáêè äëÿ òåñòèðîâàíèÿ.
        /// </summary>
        private static PartsBoxParameters BoxParameters => new PartsBoxParameters();

        /// <summary>
        /// Ýêçåìïëÿð ïàðàìåòðîâ êîðîáêè ñ íåâåðíûìè çíà÷åíèÿìè â ñâîéñòâàõ.
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

        [TestCase(50.0, TestName = "Ïðîâåðêà ïîëó÷åíèÿ êîððåêòíîãî çíà÷åíèÿ ñâîéñòâà Height.")]
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
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.Height)} âåðíóë íåêîððåêòíîå çíà÷åíèå.");
        }

        [TestCase(50.0, TestName = "Ïðîâåðêà ïîëó÷åíèÿ êîððåêòíîãî çíà÷åíèÿ ñâîéñòâà Width.")]
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
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.Width)} âåðíóë íåêîððåêòíîå çíà÷åíèå.");
        }

        [TestCase(50.0, TestName = "Ïðîâåðêà ïîëó÷åíèÿ êîððåêòíîãî çíà÷åíèÿ ñâîéñòâà Length.")]
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
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.Length)} âåðíóë íåêîððåêòíîå çíà÷åíèå.");
        }

        [TestCase(50.0, TestName = "Ïðîâåðêà ïîëó÷åíèÿ êîððåêòíîãî çíà÷åíèÿ ñâîéñòâà OuterWallWidth.")]
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
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.OuterWallWidth)} âåðíóë íåêîððåêòíîå çíà÷åíèå.");
        }

        [TestCase(50.0, TestName = "Ïðîâåðêà ïîëó÷åíèÿ êîððåêòíîãî çíà÷åíèÿ ñâîéñòâà InnerWallWidth.")]
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
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.InnerWallWidth)} âåðíóë íåêîððåêòíîå çíà÷åíèå.");
        }

        [TestCase(50.0, TestName = "Ïðîâåðêà ïîëó÷åíèÿ êîððåêòíîãî çíà÷åíèÿ ñâîéñòâà BoxBottomWidth.")]
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
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.BoxBottomWidth)} âåðíóë íåêîððåêòíîå çíà÷åíèå.");
        }

        [TestCase(1, TestName = "Ïðîâåðêà ïîëó÷åíèÿ êîððåêòíîãî çíà÷åíèÿ ñâîéñòâà CellsInWidth.")]
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
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.CellsInWidth)} âåðíóë íåêîððåêòíîå çíà÷åíèå.");
        }

        [TestCase(1, TestName = "Ïðîâåðêà ïîëó÷åíèÿ êîððåêòíîãî çíà÷åíèÿ ñâîéñòâà CellsInLength.")]
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
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.CellsInLength)} âåðíóë íåêîððåêòíîå çíà÷åíèå.");
        }

        [TestCase(69.0,TestName = "Ïðîâåðÿåò êîððåêòíîñòü ðàñ÷åòà øèðèíû îäíîé ÿ÷åéêè.")]
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
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.GetOneCellWidth)} íåêîððåêòíî ðàññ÷èòàë øèðèíó îäíîé ÿ÷åéêè.");
        }

        [TestCase(69.0, TestName = "Ïðîâåðÿåò êîððåêòíîñòü ðàñ÷åòà øèðèíû îäíîé ÿ÷åéêè.")]
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
            Assert.AreEqual(expected, actual, $"{nameof(boxParameters.GetOneCellLength)} íåêîððåêòíî ðàññ÷èòàë øèðèíó îäíîé ÿ÷åéêè.");
        }

        [TestCase("", TestName = "Ïðîâåðêà ìåòîäà GetErrors ñ propertyName ðàâíûì ïóñòîé ñòðîêå")]
        [TestCase(null, TestName = "Ïðîâåðêà ìåòîäà GetErrors ñ propertyName ðàâíûì null")]
        public void TestGetErrors_NoErrorsValue(string testPropertyName)
        {
            // Arrange
            var boxParameters = BoxParameters;

            // Act
            var actual = boxParameters.GetErrors(testPropertyName);

            // Assert
            Assert.IsEmpty(actual, "Ìåòîä âåðíóë íå ïóñòîé íàáîð ñîîáùåíèé îá îøèáêàõ ïðè êîððåêòíûõ äàííûõ.");
        }

        [TestCase(nameof(PartsBoxParameters.Height), "Height must be between 50 and 150 mm.",
            TestName = "Ïðîâåðêà ìåòîäà GetErrors ñ propertyName ðàâíûì Height è íàëè÷èåì ñòðîêè îøèáêè.")]
        [TestCase(nameof(PartsBoxParameters.Length), "Length must be between 150 and 700 mm.",
            TestName = "Ïðîâåðêà ìåòîäà GetErrors ñ propertyName ðàâíûì Length è íàëè÷èåì ñòðîêè îøèáêè.")]
        [TestCase(nameof(PartsBoxParameters.Width), "Width must be between 150 and 700 mm.",
            TestName = "Ïðîâåðêà ìåòîäà GetErrors ñ propertyName ðàâíûì Width è íàëè÷èåì ñòðîêè îøèáêè.")]
        [TestCase(nameof(PartsBoxParameters.OuterWallWidth), "OuterWallWidth must be between 5 and 10 mm.",
            TestName = "Ïðîâåðêà ìåòîäà GetErrors ñ propertyName ðàâíûì OuterWallWidth è íàëè÷èåì ñòðîêè îøèáêè.")]
        [TestCase(nameof(PartsBoxParameters.InnerWallWidth), "InnerWallWidth must be between 2 and 5 mm.",
            TestName = "Ïðîâåðêà ìåòîäà GetErrors ñ propertyName ðàâíûì InnerWallWidth è íàëè÷èåì ñòðîêè îøèáêè.")]
        [TestCase(nameof(PartsBoxParameters.BoxBottomWidth), "BoxBottomWidth must be between 5 and 10 mm.",
            TestName = "Ïðîâåðêà ìåòîäà GetErrors ñ propertyName ðàâíûì BoxBottomWidth è íàëè÷èåì ñòðîêè îøèáêè.")]
        [TestCase(nameof(PartsBoxParameters.CellsInLength), "CellsInLength incorrect ",
            TestName = "Ïðîâåðêà ìåòîäà GetErrors ñ propertyName ðàâíûì CellsInLength è íàëè÷èåì ñòðîêè îøèáêè.")]
        [TestCase(nameof(PartsBoxParameters.CellsInWidth), "CellsInWidth incorrect ",
            TestName = "Ïðîâåðêà ìåòîäà GetErrors ñ propertyName ðàâíûì CellsInWidth è íàëè÷èåì ñòðîêè îøèáêè.")]
        public void TestGetErrors_HaveErrorsValue(string testPropertyName, string correctErrorString)
        {
            // Arrange
            var boxParameters = WrongBoxParameters;

            // Act
            foreach (var error in boxParameters.GetErrors(testPropertyName))
            {
                var actual = error as string;
                // Assert
                Assert.AreEqual(correctErrorString, actual, "Ñòðîêà ñ îøèáêîé íå ðàâíà îæèäàåìîé.");
            }

        }

        [TestCase(TestName = "Ïðîâåðêà ìåòîäà óñòàíîâêè ñòàíäàðòíûõ çíà÷åíèé ñâîéñòâàì.")]
        public void TestSetDefaultValues_Positive()
        {
            // Arrange
            var defaultBoxParametes = BoxParameters;
            var modifiedBoxParameters = WrongBoxParameters;

            // Act
            modifiedBoxParameters.SetDefaultValues();

            // Assert
            Assert.AreEqual(defaultBoxParametes, modifiedBoxParameters, "Ñòàíäàðòíûå ïàðàìåòðû óñòàíîâëåíû íåâåðíî.");
        }

        [TestCase(TestName = "Ïðîâåðêà ïåðåãðóçêè îïåðàòîðà ïðîâåðêè ðàâåíñòâà" +
                             " äâóõ ýêçåìïëÿðîâ îáúåêòà ñ íåêîððåêòíûì çíà÷åíèåì.")]
        public void TestEquals_NullValue()
        {
            // Arrange
            var defaultBoxParametes = BoxParameters;

            // Act
            var areEqual = defaultBoxParametes.Equals(null);

            // Assert
            Assert.False(areEqual, "Íåêîððåêòíûé ðåçóëüòàò ñðàâíåíèÿ îáúåêòà ñ null.");
        }
    }
}