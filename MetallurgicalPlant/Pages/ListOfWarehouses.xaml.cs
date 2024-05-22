using LoggerLibrary;
using MetallurgicalPlant.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MetallurgicalPlant.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListOfWarehouses.xaml
    /// </summary>
    public partial class ListOfWarehouses : Page
    {
        private ObservableCollection<Warehouse> _warehouses;
        private Database _database;
        private Logger _logger;
        public ListOfWarehouses()
        {
            InitializeComponent();
            _database = Database.GetDatabase();
            _warehouses = new ObservableCollection<Warehouse>(_database.GetListOfWarehouses());
            _logger = new Logger();

            TableOfWarehouses.ItemsSource = _warehouses;
        }

        /// <summary>
        /// Метод для обновления данных в таблице
        /// </summary>
        private void UpdateTable()
        {
            _warehouses.Clear();
            foreach (var warehouse in _database.GetListOfWarehouses())
            {
                _warehouses.Add(warehouse);
            }

        }

        /// <summary>
        /// Метод добавления нового склада
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void AddNewWarehouse(object sender, RoutedEventArgs e)
        {

            var addingNewWarehouseWindow = new AddingNewWarehouse();
            addingNewWarehouseWindow.ShowDialog();

            if (addingNewWarehouseWindow.ValueKP80 == null ||
                addingNewWarehouseWindow.ValueKP90 == null ||
                addingNewWarehouseWindow.ValueKP100 == null)
            {
                MessageBox.Show("Ошибка добавления.");
            }
            else
            {
                _database.AddNewWarehouse(addingNewWarehouseWindow.ValueKP80, addingNewWarehouseWindow.ValueKP90, addingNewWarehouseWindow.ValueKP100);
                UpdateTable();
                _logger.MakeLog("Добавлен новый склад");
            }

            

        }
        
        /// <summary>
        /// Метод удаления выбранного склада
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void DeleteSelectedWarehouse(object sender, RoutedEventArgs e)
        {
            Warehouse selectedWarehouse = TableOfWarehouses.SelectedItem as Warehouse;

            if (selectedWarehouse == null)
            {
                MessageBox.Show("Для удаления нужно выбрать склад из списка.");
            }
            else
            {
                _database.DeleteRowFromTable(selectedWarehouse.Id, "Warehouses");
                UpdateTable();
                _logger.MakeLog("Удалён склад");
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

            _logger.MakeLog("Переход на предыдущую страницу");
        }

        /// <summary>
        /// Метод, срабатываемый при нажатии на кнопку "Создать рельсы"
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void MakeProducts(object sender, RoutedEventArgs e)
        {
            Warehouse selectedWarehouse = (Warehouse)TableOfWarehouses.SelectedItem;

            if (selectedWarehouse != null)
            {
                CountingProductsWindow countingProductsWindow = new CountingProductsWindow(selectedWarehouse);
                countingProductsWindow.ShowDialog();
                UpdateTable();
            }

            _logger.MakeLog("Нажата кнопка создания рельс");
        }
    }
}
