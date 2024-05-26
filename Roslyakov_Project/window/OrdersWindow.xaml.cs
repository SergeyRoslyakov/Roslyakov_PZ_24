using Newtonsoft.Json;
using Roslyakov_Library;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Roslyakov_Project.window
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        private List<Orders> _orders; //список заказов
        private const string _jsonFilePath = "orders.json"; //путь к файлу сохранения
        public OrdersWindow()
        {
            InitializeComponent();
            _orders = new List<Orders>(); //инициализация списка заказов
            ListOrders.ItemsSource = _orders; // заполнение списка листом
        }


        /// <summary>
        /// Удаление заказа
        /// </summary>
        private void Delete_Orders(object sender, RoutedEventArgs e)
        {
            // Получаем выделенные элементы из ListBox
            var selectedItems = ListOrders.SelectedItems.Cast<Orders>().ToList();

            // Удаляем выделенные элементы из списка заказов
            foreach (var order in selectedItems)
            {
                _orders.Remove(order);
            }

            // Обновляем отображение ListBox
            ListOrders.ItemsSource = null;
            ListOrders.ItemsSource = _orders;
        }

        /// <summary>
        /// Вернуться
        /// </summary>
        private void Button_Back(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            this.Close();
        }

        #region события окна: открытие/закрытие окна, нажатие кнопок

        /// <summary>
        /// событие загрузки окна - загружаются данные из файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataFromJson();
        }

        /// <summary>
        /// события закрытия окна - сохраняются данные в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveDataToJson();
        }

        #endregion

        #region работа с файлом
        /// <summary>
        /// загрузка из файла и десериализация данных
        /// </summary>
        internal void LoadDataFromJson()
        {
            if (File.Exists(_jsonFilePath))
            {
                string serializationData = File.ReadAllText(_jsonFilePath); //считывание содержимого файла
                if (serializationData != null)
                {
                    var orders = JsonConvert.DeserializeObject<List<Orders>>(serializationData); //десериализация содержимого файла и запись в список задач
                    foreach (var order in orders)
                    {
                        _orders.Add(order);
                    }
                }

            }
        }
        /// <summary>
        /// сериализация и сохранение данных в файл
        /// </summary>
        internal void SaveDataToJson()
        {
            if (!File.Exists(_jsonFilePath))
            {
                File.Create(_jsonFilePath).Close();
            }
            string serializationData = JsonConvert.SerializeObject(_orders);
            File.WriteAllText(_jsonFilePath, serializationData);
        }
        #endregion
    }
}
