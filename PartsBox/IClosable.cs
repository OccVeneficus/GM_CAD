namespace PartsBox
{
    /// <summary>
    /// Интерфейс для закрытия окна по MVVM.
    /// </summary>
    public interface IClosable
    {
        /// <summary>
        /// Закрывает окно.
        /// </summary>
        void Close();

        bool? DialogResult { get; set; }
    }
}
