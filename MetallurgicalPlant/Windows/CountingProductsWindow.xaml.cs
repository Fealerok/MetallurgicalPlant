using MetallurgicalPlantLibrary;
using System;
using System.Windows;
using LoggerLibrary;


namespace MetallurgicalPlant.Windows
{
    /// <summary>
    /// Логика взаимодействия для CountingProductsWindow.xaml
    /// </summary>
    public partial class CountingProductsWindow : Window
    {

        private int _valueKP80 = 0;
        private int _valueKP90 = 0;
        private int _valueKP100 = 0;

        private Warehouse _warehouse;

        public CountingProductsWindow(Warehouse warehouse)
        {
            InitializeComponent();
            _warehouse = warehouse;

            numberOfWarehouse.Text = $"Склад номер {warehouse.Id}";

            valueOfKP80.Text = $"{CountingProducts.GetValueOfProducts(warehouse.ValueKP80, "KP80")} штук";
            valueOfKP90.Text = $"{CountingProducts.GetValueOfProducts(warehouse.ValueKP90, "KP90")} штук";
            valueOfKP100.Text = $"{CountingProducts.GetValueOfProducts(warehouse.ValueKP100, "KP100")} штук";
        }

        /// <summary>
        /// Метод, срабатываемый при нажатии на кнопку "Создать рельсы"
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void MakeProducts(object sender, RoutedEventArgs e)
        {

            Database.GetDatabase().UpdateWarehousesTable(_warehouse);
            MessageBox.Show("Рельсы созданы");
            this.Close();

            new Logger().MakeLog("Созданы рельсы");

        }
    }
}
