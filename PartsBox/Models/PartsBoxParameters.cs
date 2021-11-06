using GalaSoft.MvvmLight;
using PartsBox.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        public double Height
        {
            get => _height;
            set => Set(ref _height, value);
        }

        /// <summary>
        /// Свойство для доступа к ширине внешних стенок.
        /// </summary>
        public double OuterWallWidth
        {
            get => _outerWallWidth;
            set => Set(ref _outerWallWidth, value);
        }

        /// <summary>
        /// Свойство для доступа к ширине внутренних стенок.
        /// </summary>
        public double InnerWallWidth
        {
            get => _innerWallWidth;
            set => Set(ref _innerWallWidth, value);
        }

        /// <summary>
        /// Свойство для доступа к толщине днища коробки.
        /// </summary>
        public double BoxBottomWidth
        {
            get => _boxBottomWidth;
            set => Set(ref _boxBottomWidth, value);
        }

        /// <summary>
        /// Свойство для доступа к количеству ячеек на ширину коробки (строки).
        /// </summary>
        public int CellsInWidth
        {
            get => _cellsInWidth;
            set => Set(ref _cellsInWidth, value);
        }

        /// <summary>
        /// Свойство для доступа к количеству ячеек на длину коробки (столбцы).
        /// </summary>
        public int CellsInLength
        {
            get => _cellsInLength;
            set => Set(ref _cellsInLength, value);
        }


        #endregion

        #region INotifyDataErrorInfo Implementation

        /// <inheritdoc/>
        public override IEnumerable GetErrors(string propertyName)
        {
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
        }

        #endregion

    }
}
