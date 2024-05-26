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

namespace Roslyakov_Project.window
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Открытие окна со списком рейсов
        /// </summary>
        private void Button_Flights(object sender, RoutedEventArgs e)
        {
            FlightsWindow flightsWindow = new FlightsWindow();
            flightsWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Открытие окна со списком заказов
        /// </summary>
        private void Button_Orders(object sender, RoutedEventArgs e)
        {
            OrdersWindow ordersWindow = new OrdersWindow();
            ordersWindow.Show();
            this.Close();
        }
    }
}
