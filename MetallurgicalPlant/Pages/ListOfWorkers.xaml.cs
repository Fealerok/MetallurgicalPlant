using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MetallurgicalPlant.Windows;
using MetallurgicalPlantLibrary;
using LoggerLibrary;


namespace MetallurgicalPlant.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListOfWorkers.xaml
    /// </summary>
    public partial class ListOfWorkers : Page
    {
        private Database database;
        private ObservableCollection<Worker> _workers;
        public ListOfWorkers()
        {
            InitializeComponent();

            database = Database.GetDatabase();
            _workers = new ObservableCollection<Worker>(database.GetListOfWorkers());

            TableOfWorkers.ItemsSource = _workers;
        }

        /// <summary>
        /// Метод для обновления данных в таблице
        /// </summary>
        private void UpdateTable()
        {
            _workers.Clear();
            foreach (var worker in database.GetListOfWorkers())
            {
                _workers.Add(worker);
            }

        }

        /// <summary>
        /// Метод добавления нового сотрудника
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void AddNewWorker(object sender, RoutedEventArgs e)
        {

            var addingNewWorkerWindow = new AddingNewWorker();
            addingNewWorkerWindow.ShowDialog();

            if (addingNewWorkerWindow.FullName == null ||
                addingNewWorkerWindow.Department == null || 
                addingNewWorkerWindow.Post == null)
            {
                MessageBox.Show("Ошибка добавления.");
            }
            else
            {
                database.AddNewWorker(addingNewWorkerWindow.FullName, addingNewWorkerWindow.Department, addingNewWorkerWindow.Post);
                UpdateTable();
                new Logger().MakeLog("Добавлен новый сотрудник");
            }


        }


        /// <summary>
        /// Метод удаления выбранного сотрудника
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void DeleteSelectedWorker(object sender, RoutedEventArgs e)
        {
            Worker selectedWorker = TableOfWorkers.SelectedItem as Worker;

            if (selectedWorker == null)
            {
                MessageBox.Show("Для удаления нужно выбрать сотрудника из списка.");
            }
            else
            {
                database.DeleteRowFromTable(selectedWorker.Id, "Workers");
                UpdateTable();
                new Logger().MakeLog("Удалён сотрудник");

            }
        }

        /// <summary>
        /// Метод перехода на прошлую страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransferToBack(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            NavigationFrame.Navigate(mainPage);

            new Logger().MakeLog("Переход на предыдущую страницу");
        }
    }
}

