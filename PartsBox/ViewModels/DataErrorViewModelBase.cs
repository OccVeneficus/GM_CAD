using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PartsBox.ViewModels
{
    /// <summary>
    /// Базовый класс ViewModel с реализацией интерфейса INotifyDataErrorInfo.
    /// <see cref="INotifyDataErrorInfo"/>
    /// <see cref="ViewModelBase"/>
    /// </summary>
    public class DataErrorViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        /// <inheritdoc/>
        public bool HasErrors => GetErrors(null).OfType<object>().Any();

        /// <summary>
        /// Вызвать проверку данных из кода.
        /// </summary>
        public virtual void ForceValidation()
        {
            RaisePropertyChanged(null);
        }

        /// <inheritdoc/>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <inheritdoc/>
        public virtual IEnumerable GetErrors([CallerMemberName] string propertyName = null) => Enumerable.Empty<object>();

        /// <inheritdoc/>
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(sender), e);
        }
    }
}
