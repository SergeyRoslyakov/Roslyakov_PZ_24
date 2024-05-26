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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Roslyakov_Project.window
{
    /// <summary>
    /// Логика взаимодействия для Flights.xaml
    /// </summary>
    public partial class FlightsWindow : Window
    {
        private List<Flights> _flights; //список рейсов
        private const string _jsonFilePath = "flights.json"; //путь к файлу сохранения
        public FlightsWindow()
        {
            InitializeComponent();
            _flights = new List<Flights>(); //инициализация списка рейсов
            ListFlights.ItemsSource = _flights; // список рейсов в лист
        }

        /// <summary>
        /// Добавление рейса через событие
        /// </summary>
        private void Button_Add_Flight(object sender, RoutedEventArgs e)
        {
            NewFlight newFlight = new NewFlight(this); // открытие дочернего окна (this)
            newFlight.Show();

            //подписка на событие добавления рейса в дочернем окне
            newFlight.FlightAddEvent += delegate (object senser, Flights flight)
            {
                _flights.Add(flight);
                _flights.Sort(); // применение интерфейса
                ListFlights.Items.Refresh(); // обновление содержимого листа 
            };
        }


        /// <summary>
        /// Удаление рейса
        /// </summary>
        private void Button_Delete_Flight(object sender, RoutedEventArgs e)
        {
            // Получаем выделенные элементы из ListBox
            var selectedItems = ListFlights.SelectedItems.Cast<Flights>().ToList();

            // Удаляем выделенные элементы из списка рейсов
            foreach (var flight in selectedItems)
            {
                _flights.Remove(flight);
            }

            // Обновляем отображение ListBox
            ListFlights.ItemsSource = null;
            ListFlights.ItemsSource = _flights;
        }

        /// <summary>
        /// Редактирование рейса
        /// </summary>
        private void Button_Edit_Flight(object sender, RoutedEventArgs e)
        {
            if(ListFlights.SelectedItem != null)
            {
                // нужные нам переменные двух типов для дальнейшей работы
                string selectedflight = ListFlights.SelectedItem.ToString();
                var selecteditem = (Flights)ListFlights.SelectedItem;
                // открываем дочернее окно
                EditFlight editFlight = new EditFlight(this);
                editFlight.Show();
                // обмен данными с дочерним окном для редактирования рейса
                editFlight.DateAndTime.Text = selecteditem.DateAndTime;
                editFlight.RouteA.Text = selecteditem.RouteA;
                editFlight.RouteB.Text = selecteditem.RouteB;
                editFlight.Price.Text = selecteditem.Price;
                editFlight.Places.Text = selecteditem.Places.ToString();

                //подписка на событие добавления рейса в дочернем окне
                editFlight.EditFlightEvent += delegate (object senser, Flights flight)
                {
                    _flights.Add(flight);
                    _flights.Remove(selecteditem); // удаляем старый рейс и добавляем новый отредактированный
                    _flights.Sort(); // применение интерфейса
                    ListFlights.Items.Refresh(); // обновление листа
                };
            }
            else
            {
                MessageBox.Show("Вы не выбрали рейс");
            }
        }

        /// <summary>
        /// Выход
        /// </summary>
        private void Exit(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
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

