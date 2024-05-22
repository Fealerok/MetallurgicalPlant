using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using LoggerLibrary;

namespace MetallurgicalPlant.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddingNewWarehouse.xaml
    /// </summary>
    public partial class AddingNewWarehouse : Window
    {
        public int ValueKP80 { get; set; }
        public int ValueKP90 { get; set; }
        public int ValueKP100 { get; set; }

        public AddingNewWarehouse()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод, срабатываемый при нажатии кнопки "Добавить склад"
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void AddNewWarehouse(object sender, RoutedEventArgs e)
        {
            if (kp80TextBox.Text == string.Empty || kp90TextBox.Text == string.Empty || kp100TextBox.Text == string.Empty)
            {
                MessageBox.Show("Не все поля ввода заполнены!");
            }
            else
            {
                ValueKP80 = int.Parse(kp80TextBox.Text);
                ValueKP90 = int.Parse(kp90TextBox.Text);
                ValueKP100 = int.Parse(kp100TextBox.Text);
                this.Close();
            }

            new Logger().MakeLog("Записаны данные о складе");
        }
    }
}
