﻿using GalaSoft.MvvmLight;
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
    public class PartsBoxParameters : ViewModelBase, INotifyDataErrorInfo
    {
        #region PrivateFields

        /// <summary>
        /// Хранит ширину коробки.
        /// </summary>
        private double _width;

        /// <summary>
        /// Хранит длину коробки.
        /// </summary>
        private double _lenght;

        /// <summary>
        /// Хранит высоту коробки.
        /// </summary>
        private double _height;

        /// <summary>
        /// Хранит ширину внешней стенки.
        /// </summary>
        private double _outerWallWidth;

        /// <summary>
        /// Хранит ширину внутренних перегородок.
        /// </summary>
        private double _innerWallWidth;

        /// <summary>
        /// Хранит толщину днища коробки.
        /// </summary>
        private double _boxBottomWidth;

        /// <summary>
        /// Количество ячеек, которое приходится на ширину (строки).
        /// </summary>
        private int _cellsInWidth;

        /// <summary>
        /// Количество ячеек, которое приходится на длину (столбцы).
        /// </summary>
        private int _cellsInLength;

        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

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
                ClearErrors(nameof(Width));
                Set(ref _width, value);
                if(value < 150.0)
                {
                    AddError(nameof(Width), "Can't be less than 150.0");
                }
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
                ClearErrors(nameof(Length));
                Set(ref _lenght, value);
                if (value < 150.0)
                {
                    AddError(nameof(Length), "Can't be less than 150.0");
                }
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

        public bool IsErrorsEmpty => !HasErrors;

        #endregion

        #region Public Methods

        /// <summary>
        /// Добавить в словарь сообщение об ошибке для указанного свойства.
        /// </summary>
        /// <param name="propertyName">Свойство с ошибкой.</param>
        /// <param name="errorMessage">Сообщение об ошибке.</param>
        public void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }
            _propertyErrors[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Вызывает срабатывание события ErrorsChanged.
        /// </summary>
        /// <param name="propertyName">Имя свойства, у которого изменились ошибки.</param>
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(IsErrorsEmpty));
        }

        private void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }

        #endregion

        #region INotifyDataErrorInfo Implementation

        public bool HasErrors => _propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                return null;
            }
            return _propertyErrors[propertyName];
        }

        #endregion

    }
}
