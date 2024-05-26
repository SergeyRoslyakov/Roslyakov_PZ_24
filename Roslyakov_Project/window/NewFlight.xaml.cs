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
    /// Логика взаимодействия для NewFlight.xaml
    /// </summary>
    public partial class NewFlight : Window
    {
        internal event EventHandler<Flights> FlightAddEvent; //событие добавления рейса

        internal void OnFlightAddEvent(Flights flight) //  OnFlightAddEvent событие
        {
            FlightAddEvent?.Invoke(this, flight);
        }

        public NewFlight(Window owner)
        {
            InitializeComponent();
            this.Owner = owner;
        }

        /// <summary>
        /// Добавление нового рейса
        /// </summary>
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            int i = Convert.ToInt32(Places.Text); // количество св. мест в инт
            OnFlightAddEvent(new Flights(DateAndTime.Text, RouteA.Text, RouteB.Text, Price.Text, i)); ;
            this.Close();
        }

        /// <summary>
        /// Закрытие окна
        /// </summary>
        private void Button_Back(object sender, RoutedEventArgs e)
         {
            this.Close();
         }
    }
}
