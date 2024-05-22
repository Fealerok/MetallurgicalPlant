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
    /// Логика взаимодействия для AddingNewWorker.xaml
    /// </summary>
    public partial class AddingNewWorker : Window
    {
        public string FullName {  get; set; }
        public string Department {  get; set; }
        public string Post {  get; set; }

        public AddingNewWorker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод, срабатываемый при нажатии кнопки "Добавить сотрудника"
        /// </summary>
        /// <param name="sender">Объект кнопки Button</param>
        /// <param name="e">Объект события</param>
        private void AddNewWorker(object sender, RoutedEventArgs e)
        {
            if (fullNameTextBox.Text == string.Empty || departmentTextBox.Text == string.Empty || postTextBox.Text == string.Empty)
            {
                MessageBox.Show("Не все поля ввода заполнены!");
            }
            else
            {
                FullName = fullNameTextBox.Text;
                Department = departmentTextBox.Text;
                Post = postTextBox.Text;
                this.Close();
            }

            new Logger().MakeLog("Записаны данные о сотруднике");
        }
    }
}
