using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

namespace Roslyakov_Project.window
{
    /// <summary>
    /// Логика взаимодействия для NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        public string selectedflight;
        public int passengers;

        internal event EventHandler<Orders> FlightsOrderEvent; //событие добавления заказа билета

        internal void OnFlightsOrderEvent(Orders order) // событие OnFlightsOrderEvent
        {
            FlightsOrderEvent?.Invoke(this, order);
        }

        public NewOrder(Window owner)
        {
            InitializeComponent();
            this.Owner = owner;
        }
        /// <summary>
        ///  Добавлене заказа
        /// </summary>
        private void Add_Order(object sender, RoutedEventArgs e)
        {
            // конвертируем нужные переменные в инт
            int intPassengersNow = Convert.ToInt32(PassengersNow.Text);
            int intPassengers = Convert.ToInt32(Passengers.Text);

            // производим сравнение для того, чтобы пользователь не написал больше мест, чем есть
            if (intPassengersNow >= intPassengers)
            {
                // новое значение мест
                passengers = intPassengersNow - intPassengers;
                selectedflight = RouteA.Text + " " + RouteB.Text; // маршрут, складываем две точки
                OnFlightsOrderEvent(new Orders(NameAndSurname.Text, Passengers.Text, selectedflight));
                this.Close();
            }
            else
            {
                MessageBox.Show("Столько мест нет!");
            }
        }
        /// <summary>
        /// Закрытие окна
        /// </summary>
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
