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
            set 
            {
                Set(ref _width, value);
            } 
        }

        /// <summary>
        /// Свойство для доступа к длине коробки.
        /// </summary>
        [DefaultValue(150.0)]
        public double Length
        {
            get => _lenght;
            set 
            {
                Set(ref _lenght, value);
            } 
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

        /// <inheritdoc/>
        public override IEnumerable GetErrors(string propertyName)
        {
            //TODO: большой метод, повторяющийся код, подумать как переделать
            foreach (var obj in base.GetErrors(propertyName))
            {
                yield return obj;
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(Width))
            {
                if (!Validator.ValidateRange(150.0, 700.0, Width))
                {
                    yield return $"{nameof(Width)} must be between 150.0 and 700.0 mm.";
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(Length))
            {
                if (!Validator.ValidateRange(150.0, 700.0, Length))
                {
                    yield return $"{nameof(Length)} must be between 150.0 and 700.0 mm.";
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(Height))
            {
                if (!Validator.ValidateRange(50.0, 150.0, Height))
                {
                    yield return $"{nameof(Height)} must be between 50.0 and 150.0 mm.";
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(OuterWallWidth))
            {
                if (!Validator.ValidateRange(5.0, 10.0, OuterWallWidth))
                {
                    yield return $"{nameof(OuterWallWidth)} must be between 5.0 and 10.0 mm.";
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(InnerWallWidth))
            {
                if (!Validator.ValidateRange(2.0, 5.0, InnerWallWidth))
                {
                    yield return $"{nameof(InnerWallWidth)} must be between 2.0 and 5.0 mm.";
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(BoxBottomWidth))
            {
                if (!Validator.ValidateRange(5.0, 10.0, BoxBottomWidth))
                {
                    yield return $"{nameof(BoxBottomWidth)} must be between 5.0 and 10.0 mm.";
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(CellsInWidth))
            {
                if(!Validator.ValidateCellsNumber(Width, InnerWallWidth, OuterWallWidth, CellsInWidth))
                {
                    yield return $"{nameof(CellsInWidth)} incorrect ";
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(CellsInLength))
            {
                if (!Validator.ValidateCellsNumber(Width, InnerWallWidth, OuterWallWidth, CellsInLength))
                {
                    yield return $"{nameof(CellsInLength)} incorrect ";
                }
            }
        }

        #endregion

    }
}
