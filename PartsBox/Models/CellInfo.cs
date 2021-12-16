using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace PartsBox.Models
{
    public class CellInfo : ViewModelBase
    {
        private bool _isMerge;

        private (int, int) _index;

        private RelayCommand _changeMergeStatusCommand;

        public bool IsMerge
        {
            get => _isMerge;
            set => Set(ref _isMerge, value);
        }

        public (int, int) Index
        {
            get => _index;
            set => Set(ref _index, value);
        }

        public RelayCommand ChangeMergeStatusCommand
        {
            get
            {
                return _changeMergeStatusCommand ?? new RelayCommand(() =>
                {
                    IsMerge = !IsMerge;
                });
            }
        }
    }
}
