using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PartsBox.Models;
using PartsBox.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PartsBox.ViewModels
{
    /// <summary>
    /// Модель представление главного окна.
    /// </summary>
    public class MainVM : ViewModelBase
    {
        /// <summary>
        /// Хранит команду сброса параметров в стандартное значение.
        /// </summary>
        private RelayCommand _setDefaultCommand;

        /// <summary>
        /// Хранит сервис для работы с окном сообщения.
        /// </summary>
        private readonly IMessageBoxService _messageBoxService;

        /// <summary>
        /// Хранит объект с параметрами для построения модели ящика для деталей.
        /// </summary>
        private PartsBoxParameters _currentPartsBoxParameters = new PartsBoxParameters();

        /// <summary>
        /// Предоставляет доступ к параметрам для построения ящика для деталей.
        /// </summary>
        public PartsBoxParameters CurrentPartsBoxParametes
        {
            get => _currentPartsBoxParameters;
            set => Set(ref _currentPartsBoxParameters, value);
        }

        /// <summary>
        /// Дает доступ к команде сброса параметров на стандартные значения.
        /// </summary>
        public RelayCommand SetDefaultCommand
        {
            get
            {
                return _setDefaultCommand ??
                    ( _setDefaultCommand = new RelayCommand(() =>
                        {
                            if(_messageBoxService.Show("Set all values to default?", "Set default values",
                                MessageButtons.OkCancel, MessageIcon.Warning))
                            {
                                _currentPartsBoxParameters.SetDefaultValues();
                            }
                        } 
                    ));
            }
        }

        /// <summary>
        /// Конструктор класса с сервисом.
        /// </summary>
        /// <param name="messageBoxService">Сервис для работы с окном сообщения.</param>
        public MainVM(IMessageBoxService messageBoxService)
        {
            _messageBoxService = messageBoxService;
        }
    }
}
