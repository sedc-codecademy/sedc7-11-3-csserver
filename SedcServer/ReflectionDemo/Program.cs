using System;

namespace ReflectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            NormalAccess();
            ReflectedConstruction();
            ReflectionFullThrottle();
        }

        static void NormalAccess()
        {
            var person = new Person { FirstName = "Wekoslav", LastName = "Stefanovski" };
            Console.WriteLine($"{person.LastName}, {person.FirstName}");
            Console.WriteLine(person);
        }

        static void ReflectedConstruction()
        {
            var person = (Person)Activator.CreateInstance(typeof(Person));
            person.FirstName = "Wekoslav";
            person.LastName = "Stefanovski";
            Console.WriteLine($"{person.LastName}, {person.FirstName}");
            Console.WriteLine(person);
        }

        static void ReflectionFullThrottle()
        {
            var person = Activator.CreateInstance(typeof(Person));
            typeof(Person).GetProperty("FirstName").GetSetMethod().Invoke(person, new object[] { "Wekoslav" });
            typeof(Person).GetProperty("LastName").GetSetMethod().Invoke(person, new object[] { "Stefanovski" });
            var name = typeof(Person).GetProperty("FirstName").GetGetMethod().Invoke(person, null);
            var surname = typeof(Person).GetProperty("LastName").GetGetMethod().Invoke(person, null);
            Console.WriteLine($"{surname}, {name}");
            Console.WriteLine(person);
        }

    }


    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
