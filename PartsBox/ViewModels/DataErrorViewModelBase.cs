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
    public class DataErrorViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        public bool HasErrors
        {
            get
            {
                return GetErrors(null).OfType<object>().Any();
            }
        }

        public bool IsErrorsEmpty => !HasErrors;

        public virtual void ForceValidation()
        {
            RaisePropertyChanged(null);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Вызывает срабатывание события ErrorsChanged.
        /// </summary>
        /// <param name="propertyName">Имя свойства, у которого изменились ошибки.</param>
        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(IsErrorsEmpty));
        }

        public virtual IEnumerable GetErrors([CallerMemberName] string propertyName = null)
        {
            var result = Enumerable.Empty<object>();
            OnErrorsChanged(propertyName);
            return result;
        }

        //protected void OnErrorsChanged([CallerMemberName] string propertyName = null)
        //{
        //    OnErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        //}

        //protected virtual void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        //{
        //    var handler = ErrorsChanged;
        //    if (handler != null)
        //    {
        //        handler(sender, e);
        //    }
        //}

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(sender), e);
        }
    }
}
