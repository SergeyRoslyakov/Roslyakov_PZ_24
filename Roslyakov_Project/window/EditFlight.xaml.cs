using Roslyakov_Library;
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

namespace Roslyakov_Project.window
{
    /// <summary>
    /// Логика взаимодействия для EditFlight.xaml
    /// </summary>
    public partial class EditFlight : Window
    {
        internal event EventHandler<Flights> EditFlightEvent; // событие добавления рейса

        internal void OnEditFlightEvent(Flights flight) // активация события OnEditFlightEvent
        {
            EditFlightEvent?.Invoke(this, flight);
        }
        public EditFlight(Window owner)
        {
            InitializeComponent();
            this.Owner = owner;
        }

        /// <summary>
        /// Кнопка редактировать рейс - переход на старое окно
        /// </summary>
        private void Edit(object sender, RoutedEventArgs e)
        {
            var i = Convert.ToInt32(Places.Text); // текст из текстблока в числовой тип
            OnEditFlightEvent(new Flights(DateAndTime.Text, RouteA.Text, RouteB.Text, Price.Text, i)); 
            this.Close();
        }
        /// <summary>
        /// Выход
        /// </summary>
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
