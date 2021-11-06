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
        private bool _isErrorsEmpty;

        public bool HasErrors
        {
            get
            {
                var result = GetErrors(null).OfType<object>().Any();
                IsErrorsEmpty = !result;
                return result;
            }
        }

        public bool IsErrorsEmpty
        {
            get => _isErrorsEmpty;
            set => Set(ref _isErrorsEmpty, value);
        }

        public virtual void ForceValidation()
        {
            RaisePropertyChanged(null);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public virtual IEnumerable GetErrors([CallerMemberName] string propertyName = null)
        {
            return Enumerable.Empty<object>();
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(sender), e);
        }
    }
}
