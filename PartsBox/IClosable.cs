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

        /// <summary>
        /// Свойство хранит результат диалога.
        /// </summary>
        bool? DialogResult { get; set; }
    }
}
