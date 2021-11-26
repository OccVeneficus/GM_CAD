using GalaSoft.MvvmLight;
using PartsBox.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

        #endregion

        #region Properties

        /// <summary>
        /// Свойство для доступа к ширине коробки.
        /// </summary>
        [DefaultValue(150.0)]
        public double Width
        {
            get => _width;
            set => Set(ref _width, value);
        }

        /// <summary>
        /// Свойство для доступа к длине коробки.
        /// </summary>
        [DefaultValue(150.0)]
        public double Length
        {
            get => _lenght;
            set => Set(ref _lenght, value);
        }

        /// <summary>
        /// Свойство для доступа к высоте коробки.
        /// </summary>
        [DefaultValue(50.0)]
        public double Height
        {
            get => _height;
            set => Set(ref _height, value);
        }

        /// <summary>
        /// Свойство для доступа к ширине внешних стенок.
        /// </summary>
        [DefaultValue(5.0)]
        public double OuterWallWidth
        {
            get => _outerWallWidth;
            set => Set(ref _outerWallWidth, value);
        }

        /// <summary>
        /// Свойство для доступа к ширине внутренних стенок.
        /// </summary>
        [DefaultValue(2.0)]
        public double InnerWallWidth
        {
            get => _innerWallWidth;
            set => Set(ref _innerWallWidth, value);
        }

        /// <summary>
        /// Свойство для доступа к толщине днища коробки.
        /// </summary>
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
            set => Set(ref _cellsInWidth, value);
        }

        /// <summary>
        /// Свойство для доступа к количеству ячеек на длину коробки (столбцы).
        /// </summary>
        [DefaultValue(1)]
        public int CellsInLength
        {
            get => _cellsInLength;
            set => Set(ref _cellsInLength, value);
        }

        /// <summary>
        /// Рассчитать ширину одной ячейки.
        /// </summary>
        public double GetOneCellWidth
        {
            get
            {
                var cellsCombinedWidth = Width - 2 * OuterWallWidth - (CellsInWidth - 1) * InnerWallWidth;
                return cellsCombinedWidth / CellsInWidth;
            }
        }

        /// <summary>
        /// Рассчитать длину одной ячейки.
        /// </summary>
        public double GetOneCellLength
        {
            get
            {
                var cellsCombinedWidth = Length - 2 * OuterWallWidth - (CellsInLength - 1) * InnerWallWidth;
                return cellsCombinedWidth / CellsInLength;
            }
        }

        #endregion

        #region PublicMethods

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
        private IEnumerable GetDimensionsError(double min, double max, double value, string propertyName)
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

        /// <inheritdoc/>
        public override IEnumerable GetErrors(string propertyName)
        {
            // Дальше поделить не получается. Метод при не указанном propertyName
            // должен проверить все свойства на ошибки
            foreach (var obj in base.GetErrors(propertyName))
            {
                yield return obj;
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(Width))
            {
                foreach(var value in GetDimensionsError(150.0, 700.0, Width, nameof(Width))) 
                {
                    yield return value;
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(Length))
            {
                foreach (var value in GetDimensionsError(150.0, 700.0, Length, nameof(Length)))
                {
                    yield return value;
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(Height))
            {
                foreach (var value in GetDimensionsError(50.0, 150.0, Height, nameof(Height)))
                {
                    yield return value;
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(OuterWallWidth))
            {
                foreach (var value in GetDimensionsError(5.0, 10.0, OuterWallWidth, nameof(OuterWallWidth)))
                {
                    yield return value;
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(InnerWallWidth))
            {
                foreach (var value in GetDimensionsError(2.0, 5.0, InnerWallWidth, nameof(InnerWallWidth)))
                {
                    yield return value;
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(BoxBottomWidth))
            {
                foreach (var value in GetDimensionsError(5.0, 10.0, BoxBottomWidth, nameof(BoxBottomWidth)))
                {
                    yield return value;
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(CellsInWidth))
            {
                if (!Validator.ValidateCellsNumber(Width, InnerWallWidth, OuterWallWidth, CellsInWidth))
                {
                    yield return $"{nameof(CellsInWidth)} incorrect ";
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(CellsInLength))
            {
                if (!Validator.ValidateCellsNumber(Length, InnerWallWidth, OuterWallWidth, CellsInLength))
                {
                    yield return $"{nameof(CellsInLength)} incorrect ";
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
