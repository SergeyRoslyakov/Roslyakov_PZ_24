using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roslyakov_Library
{
    public class Users : IComparable<Users>
    {
        public string nameandsurname;
        public string email;
        public string password;

        public DateTime CreationTime { get; private set; } = DateTime.Now;
        public string NameAndSurname
        {
            get => nameandsurname;
            set => nameandsurname = value;
        }
        public string Email
        {
            get => email;
            set => email = value;
        }
        public string Password
        {
            get => password;
            set => password = value;
        }

        /// <summary>
        /// для корректной десиреализации обязательно
        /// должен присутствовать конструктор без параметров
        /// </summary>
        public Users() { }
        public Users(string nameandsurname, string email, string password)
        {
            CreationTime = DateTime.Now;
            this.nameandsurname = nameandsurname;
            this.email = email;
            this.password = password;
        }
        public Users(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
        public int CompareTo(Users other)
        {
            return nameandsurname.CompareTo(other.nameandsurname);   // сравнение по имени фамилии
        }
        public override string ToString()
        {
            return $"{CreationTime} : {NameAndSurname} : {email} : {Email} : {Password}";
        }
    }
}
