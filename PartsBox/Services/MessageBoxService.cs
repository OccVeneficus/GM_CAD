using PartsBox.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Windows;

namespace PartsBox.Services
{
    /// <summary>
    /// Реализация интерфейса для работы с окном сообщения. ъ
    /// </summary>
    public class MessageBoxService : IMessageBoxService
    {
        /// <summary>
        /// Словарь с парами ключ-значение перечислений кнопок окна сообщения.
        /// </summary>
        private Dictionary<MessageButtons, MessageBoxButton> _buttonsDictionary = new Dictionary<MessageButtons, MessageBoxButton>
        {
            {MessageButtons.Ok, MessageBoxButton.OK},
            {MessageButtons.OkCancel, MessageBoxButton.OKCancel}
        };

        /// <summary>
        /// Словарь с парами ключ-значение перечислений картинок окна сообщения.
        /// </summary>
        private Dictionary<MessageIcon, MessageBoxImage> _iconDictionary = new Dictionary<MessageIcon, MessageBoxImage>
        {
            {MessageIcon.Warning, MessageBoxImage.Warning},
            {MessageIcon.Error, MessageBoxImage.Error},
            {MessageIcon.Info, MessageBoxImage.Information}
        };

        /// <inheritdoc/>
        public bool Show(string messageBoxText, string caption, MessageButtons button, MessageIcon icon)
        {
            var result = MessageBox.Show(messageBoxText, caption, _buttonsDictionary[button], _iconDictionary[icon]);
            if(result == MessageBoxResult.OK || result == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
