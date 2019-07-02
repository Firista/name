using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.BDClasses
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int age { get; set; }

        public Employee(int id, string name, string surname, int age)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.age = age;
        }

        public Employee(string name, string surname, int age)
        {
            this.name = name;
            this.surname = surname;
            this.age = age;
        }
    }
}
