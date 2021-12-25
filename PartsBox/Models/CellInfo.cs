using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace PartsBox.Models
{
    /// <summary>
    /// Информация о ячейке ящика.
    /// </summary>
    public class CellInfo : ViewModelBase
    {
        /// <summary>
        /// Выбрана для слияния.
        /// </summary>
        private bool _isMerge;

        /// <summary>
        /// Индекс ячейки по высоте и ширине.
        /// </summary>
        private (int, int) _index;

        /// <summary>
        /// Имеет соседей.
        /// </summary>
        private bool _hasNeighbor;

        /// <summary>
        /// Иммеет соседей.
        /// </summary>
        public bool HasNeighbor
        {
            get => _hasNeighbor;
            set => Set(ref _hasNeighbor, value);
        }

        /// <summary>
        /// Выбрана для слияния.
        /// </summary>
        public bool IsMerge
        {
            get => _isMerge;
            set => Set(ref _isMerge, value);
        }

        /// <summary>
        /// Индекс ячейки по высоте и ширине.
        /// </summary>
        public (int, int) Index
        {
            get => _index;
            set => Set(ref _index, value);
        }

        /// <summary>
        /// Команда смены статуса слияния ячекйки.
        /// </summary>
        public RelayCommand ChangeMergeStatusCommand { get; }

        public CellInfo()
        {
            ChangeMergeStatusCommand = new RelayCommand(() =>
            {
                IsMerge = !IsMerge;
            });
        }
    }
}
