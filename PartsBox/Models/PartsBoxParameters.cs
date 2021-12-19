using GalaSoft.MvvmLight;
using PartsBox.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PartsBox.Models
{
    /// <summary>
    /// Класс для хранения параметров построения объекта коробки для деталей.
    /// </summary>
    public class PartsBoxParameters : DataErrorViewModelBase
    {
        #region PrivateFields

        /// <summary>
        /// Хранит ширину коробки.
        /// </summary>
        private double _width = 150.0;

        /// <summary>
        /// Хранит длину коробки.
        /// </summary>
        private double _lenght = 150.0;

        /// <summary>
        /// Хранит высоту коробки.
        /// </summary>
        private double _height = 50.0;

        /// <summary>
        /// Хранит ширину внешней стенки.
        /// </summary>
        private double _outerWallWidth = 5.0;

        /// <summary>
        /// Хранит ширину внутренних перегородок.
        /// </summary>
        private double _innerWallWidth = 2.0;

        /// <summary>
        /// Хранит толщину днища коробки.
        /// </summary>
        private double _boxBottomWidth = 5.0;

        /// <summary>
        /// Количество ячеек, которое приходится на ширину (строки).
        /// </summary>
        private int _cellsInWidth = 1;

        /// <summary>
        /// Количество ячеек, которое приходится на длину (столбцы).
        /// </summary>
        private int _cellsInLength = 1;

        /// <summary>
        /// Коллекция с ифнормацией о ячейках для слияния.
        /// </summary>
        private ObservableCollection<CellInfo> _cells = new ObservableCollection<CellInfo>();

        #endregion

        #region Properties

        /// <summary>
        /// Свойство для доступа к ширине коробки.
        /// </summary>
        ///
        [Range(150.0,700.0)]
        [DefaultValue(150.0)]
        public double Width
        {
            get => _width;
            set => Set(ref _width, value);
        }

        /// <summary>
        /// Свойство для доступа к длине коробки.
        /// </summary>
        [Range(150.0,700.0)]
        [DefaultValue(150.0)]
        public double Length
        {
            get => _lenght;
            set => Set(ref _lenght, value);
        }

        /// <summary>
        /// Свойство для доступа к высоте коробки.
        /// </summary>
        [Range(50.0, 150.0)]
        [DefaultValue(50.0)]
        public double Height
        {
            get => _height;
            set => Set(ref _height, value);
        }

        /// <summary>
        /// Свойство для доступа к ширине внешних стенок.
        /// </summary>
        [Range(5.0, 10.0)]
        [DefaultValue(5.0)]
        public double OuterWallWidth
        {
            get => _outerWallWidth;
            set => Set(ref _outerWallWidth, value);
        }

        /// <summary>
        /// Свойство для доступа к ширине внутренних стенок.
        /// </summary>
        [Range(2.0, 5.0)]
        [DefaultValue(2.0)]
        public double InnerWallWidth
        {
            get => _innerWallWidth;
            set => Set(ref _innerWallWidth, value);
        }

        /// <summary>
        /// Свойство для доступа к толщине днища коробки.
        /// </summary>
        [Range(5.0, 10.0)]
        [DefaultValue(5.0)]
        public double BoxBottomWidth
        {
            get => _boxBottomWidth;
            set => Set(ref _boxBottomWidth, value);
        }

        /// <summary>
        /// Свойство для доступа к количеству ячеек на ширину коробки (строки).
        /// </summary>
        [DefaultValue(1)]
        public int CellsInWidth
        {
            get => _cellsInWidth;
            set
            {
                Set(ref _cellsInWidth, value);
                RepopulateCellsInfos();
            } 
        }

        /// <summary>
        /// Свойство для доступа к количеству ячеек на длину коробки (столбцы).
        /// </summary>
        [DefaultValue(1)]
        public int CellsInLength
        {
            get => _cellsInLength;
            set
            {
                Set(ref _cellsInLength, value);
                RepopulateCellsInfos();
            }
        }

        /// <summary>
        /// Коллекция с ифнормацией о ячейках для слияния.
        /// </summary>
        public ObservableCollection<CellInfo> Cells
        {
            get => _cells;
            set => Set(ref _cells, value);
        }

        /// <summary>
        /// Рассчитать ширину одной ячейки.
        /// </summary>
        public double GetOneCellWidth => CalculateOneCellSize(Width, OuterWallWidth,
            InnerWallWidth, CellsInWidth);

        /// <summary>
        /// Рассчитать длину одной ячейки.
        /// </summary>
        public double GetOneCellLength => CalculateOneCellSize(Length, OuterWallWidth,
            InnerWallWidth, CellsInLength);

        #endregion

        #region Private Methods

        /// <summary>
        /// Обновить коллекцию информации о ячейках для слияния.
        /// </summary>
        private void RepopulateCellsInfos()
        {
            Cells.Clear();
            for (var widthIndex = 0; widthIndex < CellsInWidth; widthIndex++)
            {
                for (var lengthIndex = 0; lengthIndex < CellsInLength; lengthIndex++)
                {
                    Cells.Add(new CellInfo
                    {
                        IsMerge = false,
                        Index = (lengthIndex, widthIndex)
                    });
                }
            }
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Метод расчета длины или ширины одной ячейки.
        /// </summary>
        /// <param name="dimensionSize">Длина или ширина.</param>
        /// <param name="outerWallWidth">Длина внешней стенки.</param>
        /// <param name="innerWallWidth">Длина внутренней стенки.</param>
        /// <param name="cellsInDimension">Ячеек в длине или ширине.</param>
        /// <returns>Длина или ширина одной ячейки.</returns>
        public double CalculateOneCellSize(double dimensionSize,
            double outerWallWidth, 
            double innerWallWidth, int cellsInDimension)
        {
            var cellsCombinedWidth = dimensionSize - 2 *
                outerWallWidth - (cellsInDimension - 1) * innerWallWidth;
            return cellsCombinedWidth / cellsInDimension;
        }

        /// <summary>
        /// Устанавливает стандартные (минимальные) значения параметрам ящика.
        /// </summary>
        public void SetDefaultValues()
        {
            var properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                AttributeCollection attributes =
                        TypeDescriptor.GetProperties(this)[property.Name].Attributes;
                DefaultValueAttribute attribute =
                        (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
                if (attribute == null)
                {
                    continue;
                }
                property.SetValue(this, attribute.Value);
            }
        }

        #endregion

        #region INotifyDataErrorInfo Implementation

        /// <summary>
        /// Валидация для свойств с ограниченным диапазоном значений.
        /// </summary>
        /// <param name="min">Начало диапазона.</param>
        /// <param name="max">Конец диапазона.</param>
        /// <param name="value">Значение.</param>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Список ошибок.</returns>
        private static IEnumerable GetDimensionsError(double min, double max,
            double value, string propertyName)
        {
            if(!Validator.ValidateRange(min, max, value))
            {
               yield return GetRangeErrorString(min, max, propertyName);
            }
        }

        /// <summary>
        /// Генерация строки с информацией об ошибке диапазона.
        /// </summary>
        /// <param name="min">Начало диапазона.</param>
        /// <param name="max">Конец диапазона.</param>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Строка с сообщением об ошибке.</returns>
        private static string GetRangeErrorString(double min, double max, string propertyName)
        {
            return $"{propertyName} must be between {min} and {max} mm.";
        }

        /// <summary>
        /// Получить ошибки свойств с атрибутом Range.
        /// </summary>
        /// <param name="property">Свойство, ошибки которого нужно получить.</param>
        /// <returns>Ошибки валидации указанного свойства.</returns>
        private IEnumerable GetRangeErrors(PropertyInfo property)
        {
            AttributeCollection attributes =
                TypeDescriptor.GetProperties(this)[property.Name].Attributes;
            RangeAttribute attribute =
                (RangeAttribute)attributes[typeof(RangeAttribute)];
            if (attribute?.Minimum != null)
            {
                var min = (double)attribute.Minimum;
                var max = (double)attribute.Maximum;
                foreach (var value in GetDimensionsError(min, max,
                    (double)property.GetValue(this), property.Name))
                {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Получить ошибки для полей с количеством ячеек в длине/ширине.
        /// </summary>
        /// <param name="property">Свойство количества ячеек в длине/ширине.</param>
        /// <returns>Ошибки свойства количества ячеек в длине/ширине.</returns>
        private IEnumerable GetCellsNumberErrors(PropertyInfo property)
        {
            var dimensionValue = property.Name == nameof(CellsInWidth) ? Width : Length;
            if (!Validator.ValidateCellsNumber(CalculateOneCellSize, dimensionValue,
                InnerWallWidth, OuterWallWidth, (int)property.GetValue(this)))
            {
                yield return $"{property.Name} incorrect ";
            }
        }

        /// <inheritdoc/>
        public override IEnumerable GetErrors(string propertyName = null)
        {
            foreach (var obj in base.GetErrors(propertyName))
            {
                yield return obj;
            }

            var properties = GetType().GetProperties();

            if (!string.IsNullOrEmpty(propertyName))
            {
                properties = properties.Where(x => x.Name == propertyName).ToArray();
            }

            foreach (var property in properties)
            {
                if (property.Name == nameof(CellsInLength) || property.Name == nameof(CellsInWidth))
                {
                    foreach (var rangeError in GetCellsNumberErrors(property))
                    {
                        yield return rangeError;
                    }
                }
                foreach (var rangeError in GetRangeErrors(property))
                {
                    yield return rangeError;
                }
            }
        }

        #endregion

        #region EqualsOverride

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is PartsBoxParameters toCompareWith))
            {
                return false;
            }

            return Equals(toCompareWith);
        }

        /// <summary>
        /// Проверка равенства двух объектов <see cref="PartsBoxParameters"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(PartsBoxParameters other)
        {
            return _width.Equals(other._width) 
                   && _lenght.Equals(other._lenght) 
                   && _height.Equals(other._height) 
                   && _outerWallWidth.Equals(other._outerWallWidth) 
                   && _innerWallWidth.Equals(other._innerWallWidth) 
                   && _boxBottomWidth.Equals(other._boxBottomWidth) 
                   && _cellsInWidth == other._cellsInWidth 
                   && _cellsInLength == other._cellsInLength;
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _width.GetHashCode();
                hashCode = (hashCode * 397) ^ _lenght.GetHashCode();
                hashCode = (hashCode * 397) ^ _height.GetHashCode();
                hashCode = (hashCode * 397) ^ _outerWallWidth.GetHashCode();
                hashCode = (hashCode * 397) ^ _innerWallWidth.GetHashCode();
                hashCode = (hashCode * 397) ^ _boxBottomWidth.GetHashCode();
                hashCode = (hashCode * 397) ^ _cellsInWidth;
                hashCode = (hashCode * 397) ^ _cellsInLength;
                return hashCode;
            }
        }

        #endregion

    }
}
