using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roslyakov_Library
{
    public class Orders
    {
        public string nameAndSurname;
        public string passengers;
        public string flight;
        public DateTime OrderTime { get; private set; } = DateTime.Now;


        public string NameAndSurname
        {
            get => nameAndSurname;
            set => nameAndSurname = value;
        }

        public string Passengers
        {
            get => passengers;
            set => passengers = value;
        }

        public string Flight
        {
            get => flight;
            set => flight = value;
        }

        /// <summary>
        /// для корректной десиреализации обязательно
        /// должен присутствовать конструктор без параметров
        /// </summary>
        public Orders() { }

        public Orders(string nameandsurname, string passengers, string flight)
        {
            this.NameAndSurname = nameandsurname;
            this.passengers = passengers;
            this.flight = flight;
        }

        public override string ToString()
        {
            return $"{OrderTime} : {NameAndSurname} : {Passengers} : {Flight}";
        }
    }
}
