using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Roslyakov_Library
{
    public class Flights : IComparable<Flights>
    {
        public string dateAndTime;
        public string routeA;
        public string routeB;
        public string price;
        public int places;
        public string newflight;

        public string DateAndTime
        {
            get => dateAndTime;
            set => dateAndTime = value;
        }

        public string RouteA
        {
            get => routeA;
            set => routeA = value;
        }

        public string RouteB
        {
            get => routeB;
            set => routeB = value;
        }

        public string Price
        {
            get => price;
            set => price = value;
        }

        public int Places
        {
            get => places;
            set => places = value;
        }

        public Flights() { }

        public Flights(string dateAndTime, string routeA, string routeB, string price, int places)
        {
            this.dateAndTime = dateAndTime;
            this.RouteA = routeA;
            this.RouteB = routeB;
            this.price = price;
            this.places = places;
        }

        public Flights(string newflight)
        {
            this.newflight = newflight;
        }
        // реализация интерфейса
        public int CompareTo(Flights other)
        {
            return DateAndTime.CompareTo(other.DateAndTime);   // сравнение по времени
        }
        public override string ToString()
        {
            return $"Время: {DateAndTime}, Маршрут: {RouteA} -- {RouteB}, Цена: {Price}, Места: {Places}";
        }


    }
}
