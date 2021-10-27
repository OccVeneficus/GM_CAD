using GalaSoft.MvvmLight;
using PartsBox.Models;
using PartsBox.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsBox.ViewModels
{
    /// <summary>
    /// Модель представление главного окна.
    /// </summary>
    public class MainVM : ViewModelBase
    {
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
        /// Конструктор класса с сервисом.
        /// </summary>
        /// <param name="messageBoxService">Сервис для работы с окном сообщения.</param>
        public MainVM(IMessageBoxService messageBoxService)
        {
            _messageBoxService = messageBoxService;
        }
    }
}
