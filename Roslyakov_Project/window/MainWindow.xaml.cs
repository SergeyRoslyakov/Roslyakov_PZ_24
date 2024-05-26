using Newtonsoft.Json;
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
using Roslyakov_Library;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Roslyakov_Project.window
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Flights> _flights; //список рейсов
        private const string _jsonFilePath = "flights.json"; //путь к файлу сохранения

        private List<Orders> _order; //список заказов
        private const string _jsonFilePath2 = "orders.json"; //путь к файлу сохранения

        public MainWindow()
        {
            InitializeComponent();
            _flights = new List<Flights>(); //инициализация списка рейсов
            _flights.Sort(); // применение интерфейса
            ListFlights.ItemsSource = _flights; // список рейсов в лист
            _order = new List<Orders>(); //инициализация списка заказов
        }

        /// <summary>
        /// Клик кнопка заказа
        /// </summary>
        private void Button_Flights(object sender, RoutedEventArgs e)
        {
            if(ListFlights.SelectedItem != null)
            {
                // нужные нам переменные 2ух типов
                string selectedflight = ListFlights.SelectedItem.ToString();
                var selectedflightlist = (Flights)ListFlights.SelectedItem;

                // создание дочернего окна (this)
                NewOrder newOrder = new NewOrder(this);

                // обмен данными
                newOrder.PassengersNow.Text = selectedflightlist.Places.ToString();
                newOrder.selectedflight = selectedflight;
                newOrder.Show();
                newOrder.RouteA.Text = selectedflightlist.RouteA;
                newOrder.RouteB.Text = selectedflightlist.RouteB;

                //подписка на событие добавления заказа в дочернем окне
                newOrder.FlightsOrderEvent += delegate (object senser, Orders order)
                {
                    _order.Add(order);
                    selectedflightlist.places = newOrder.passengers; // меняем количество свободных мест на рейсе после
                                                                       // добавления нового обьекта класса заказ
                    ListFlights.Items.Refresh(); // обновление листа

                };
            }
            else
            {
                MessageBox.Show("Вы не выбрали рейс!");
            }
        }

        /// <summary>
        /// Открытие окна информации по клику
        /// </summary>
        private void Button_Info(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            info.Show();
            this.Close();
        }

        #region работа с файлами json
        /// <summary>
        /// Загрузка из json и добавление обьектов в лист
        /// </summary>
        internal void LoadDataFromJson()
        {
            if (File.Exists(_jsonFilePath))
            {
                string serializationData = File.ReadAllText(_jsonFilePath); //считывание содержимого файла
                if (serializationData != null)
                {
                    var flights = JsonConvert.DeserializeObject<List<Flights>>(serializationData); //десериализация содержимого файла и запись в список задач
                    foreach (var flight in flights)
                    {
                        _flights.Add(flight);
                    }
                }

            }

            if (File.Exists(_jsonFilePath2))
            {
                string serializationData2 = File.ReadAllText(_jsonFilePath2); //считывание содержимого файла
                if (serializationData2 != null)
                {
                    var orders = JsonConvert.DeserializeObject<List<Orders>>(serializationData2); //десериализация содержимого файла и запись в список задач
                    foreach (var order in orders)
                    {
                        _order.Add(order);
                    }
                }

            }
        }



        /// <summary>
        /// Загрузка в json и добавление туда обьектов из листа
        /// </summary>
        internal void SaveDataToJson()
        {
            if (!File.Exists(_jsonFilePath))
            {
                File.Create(_jsonFilePath).Close();
            }
            string serializationData = JsonConvert.SerializeObject(_flights);
            File.WriteAllText(_jsonFilePath, serializationData);

            if (!File.Exists(_jsonFilePath2))
            {
                File.Create(_jsonFilePath2).Close();
            }
            string serializationData2 = JsonConvert.SerializeObject(_order);
            File.WriteAllText(_jsonFilePath2, serializationData2);
        }

        #endregion

        #region закрытие/загрузка окна
        /// <summary>
        /// Открытие и закрытие окна с логикой сохранения изменений и загрузки содержимого файлов json
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataFromJson();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveDataToJson();
        }
        #endregion
    }
}
