using WindowHelper;
using System.Windows;
using System.Windows.Input;

namespace WebChatClient
{
    public class MainWindowVM
    {
        // Окно, которым управляет эта модель представления
        private Window _view;

        // Помощник по изменению размера окна, который поддерживает правильный размер окна в различных состояниях.
        private WindowResizer _windowResizer;

        public MainWindowVM(Window view)
        {
            _view = view;

            var activeScreen = System.Windows.Forms.Screen.FromPoint(System.Windows.Forms.Cursor.Position);
            _view.MaxHeight = activeScreen.WorkingArea.Height;
            _view.MaxWidth = activeScreen.WorkingArea.Width;

            // Создание команд
            MinimizeCommand = new Command((o) => _view.WindowState = WindowState.Minimized);
            MaximizeCommand = new Command((o) => _view.WindowState ^= WindowState.Maximized);
            CloseCommand = new Command((o) => _view.Close());
            MenuCommand = new Command((o) => SystemCommands.ShowSystemMenu(_view, GetMousePosition()));
            WorkingAreaCommand = new Command((o) =>
            {
                // размеры активного экрана, то есть там где находится окно приложения
                var activeScreen = System.Windows.Forms.Screen.FromPoint(System.Windows.Forms.Cursor.Position);
                _view.MaxHeight = activeScreen.WorkingArea.Height;
                _view.MaxWidth = activeScreen.WorkingArea.Width;
            });

            // Fix window resize issue
            _windowResizer = new WindowResizer(_view);
        }

        #region Открытые свойства

        //Наименьшая ширина окна.
        public double WindowMinimumWidth { get; set; } = 800;

        //Наименьшая высота окна.
        public double WindowMinimumHeight { get; set; } = 500;

        // Высота строки заголовка окна
        public int TitleHeight { get; set; } = 50;

        #endregion

        #region Команды

        // Определение рабочей области активного экрана
        public ICommand WorkingAreaCommand { get; set; }

        // Команда сворачивания окна
        public ICommand MinimizeCommand { get; set; }

        // Команда максимизации окна
        public ICommand MaximizeCommand { get; set; }

        // Команда закрытия окна
        public ICommand CloseCommand { get; set; }

        // Команда показа системного меню окна
        public ICommand MenuCommand { get; set; }

        #endregion

        //Получает текущую позицию мыши на экране
        private Point GetMousePosition()
        {
            return _windowResizer.GetCursorPosition();
        }
    }
}
