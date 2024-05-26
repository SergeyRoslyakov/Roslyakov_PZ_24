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
using System.IO;

namespace Roslyakov_Project.window
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        private List<Users> _users; //список пользователей
        private const string _jsonFilePath = "users.json"; //путь к файлу сохранения
        public Autorization()
        {
            InitializeComponent();
            _users = new List<Users>(); //инициализация списка пользователей
        }

        /// <summary>
        /// Переход к регистрации
        /// </summary>
        private void Button_Registration(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }

        /// <summary>
        /// Логика окна авторизации
        /// </summary>
        private void Button_Autorization(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;

            Users user = _users.FirstOrDefault(u => u.Email == login && u.Password == password); // использование лямбда выражений,
                                                                                                                // проверка присутствия такого пользователя в листе
            if (user != null)
            {

                if (user.Email == "1" && user.Password == "1") // вход для админа
                {
                    AdminWindow admin = new AdminWindow();
                    admin.Show();
                    this.Close();
                }
                else // вход для пользователя
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            }
            else // пароль/логин неверный
            {
                MessageBox.Show("Пользователь не найден: пароль или логин введен неверно!");
            }
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
                    var users = JsonConvert.DeserializeObject<List<Users>>(serializationData); //десериализация содержимого файла и запись в список задач
                    foreach (var user in users)
                    {
                        _users.Add(user);
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
            string serializationData = JsonConvert.SerializeObject(_users);
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
