using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace ReflectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting measurements");

            Stopwatch swFullOn = Stopwatch.StartNew();
            for (int i = 0; i < 1_000_000; i++)
            {
                ReflectionFullThrottle();
            }
            swFullOn.Stop();

            Stopwatch swFullOn2 = Stopwatch.StartNew();
            for (int i = 0; i < 1_000_000; i++)
            {
                ReflectionFullThrottle2();
            }
            swFullOn2.Stop();

            Stopwatch swFullOn3 = Stopwatch.StartNew();
            for (int i = 0; i < 1_000_000; i++)
            {
                ReflectionFullThrottle3();
            }
            swFullOn3.Stop();

            Stopwatch swConstruction = Stopwatch.StartNew();
            for (int i = 0; i < 1_000_000; i++)
            {
                ReflectedConstruction();
            }
            swConstruction.Stop();

            Stopwatch swNormal = Stopwatch.StartNew();
            for (int i = 0; i < 1_000_000; i++)
            {
                NormalAccess();
            }

            swNormal.Stop();

            Console.WriteLine($"Normal Access          : {swNormal.ElapsedMilliseconds}ms");
            Console.WriteLine($"Reflected construction : {swConstruction.ElapsedMilliseconds}ms");
            Console.WriteLine($"Full on reflection     : {swFullOn.ElapsedMilliseconds}ms");
            Console.WriteLine($"Full on reflection2    : {swFullOn2.ElapsedMilliseconds}ms");
            Console.WriteLine($"Full on reflection3    : {swFullOn3.ElapsedMilliseconds}ms");
        }

        static (string, string) NormalAccess()
        {
            var person = new Person { FirstName = "Wekoslav", LastName = "Stefanovski" };
            var surname = person.LastName;
            var name = person.FirstName;
            return (name, surname);
        }

        static (string, string) ReflectedConstruction()
        {
            var person = (Person)Activator.CreateInstance(typeof(Person));
            person.FirstName = "Wekoslav";
            person.LastName = "Stefanovski";
            var surname = person.LastName;
            var name = person.FirstName;
            return (name, surname);
        }

        static (object, object) ReflectionFullThrottle()
        {
            var person = Activator.CreateInstance(typeof(Person));
            typeof(Person).GetProperty("FirstName").GetSetMethod().Invoke(person, new object[] { "Wekoslav" });
            typeof(Person).GetProperty("LastName").GetSetMethod().Invoke(person, new object[] { "Stefanovski" });
            var name = typeof(Person).GetProperty("FirstName").GetGetMethod().Invoke(person, null);
            var surname = typeof(Person).GetProperty("LastName").GetGetMethod().Invoke(person, null);
            return (name, surname);
        }

        static (object, object) ReflectionFullThrottle2()
        {
            var person = Activator.CreateInstance(typeof(Person));
            var fnProp = typeof(Person).GetProperty("FirstName");
            var lnProp = typeof(Person).GetProperty("LastName");
            fnProp.GetSetMethod().Invoke(person, new object[] { "Wekoslav" });
            lnProp.GetSetMethod().Invoke(person, new object[] { "Stefanovski" });
            var name = fnProp.GetGetMethod().Invoke(person, null);
            var surname = lnProp.GetGetMethod().Invoke(person, null);
            return (name, surname);
        }


        static Dictionary<Type, Dictionary<string, (PropertyInfo Prop, MethodInfo Getter, MethodInfo Setter)>> propInfos =
            new Dictionary<Type, Dictionary<string, (PropertyInfo Prop, MethodInfo Getter, MethodInfo Setter)>>();
            

        static (object, object) ReflectionFullThrottle3()
        {
            if (!propInfos.ContainsKey(typeof(Person)))
            {
                var typePropsLoad = new Dictionary<string, (PropertyInfo Prop, MethodInfo Getter, MethodInfo Setter)>();
                propInfos.Add(typeof(Person), typePropsLoad);
                foreach (var propertyInfo in typeof(Person).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    typePropsLoad.Add(propertyInfo.Name, (Prop: propertyInfo, Getter: propertyInfo.GetGetMethod(), Setter: propertyInfo.GetSetMethod()));
                }
            }
            var typeProps = propInfos[typeof(Person)];
            var person = Activator.CreateInstance(typeof(Person));
            var fnProp = typeProps["FirstName"];
            var lnProp = typeProps["LastName"];
            fnProp.Setter.Invoke(person, new object[] { "Wekoslav" });
            lnProp.Setter.Invoke(person, new object[] { "Stefanovski" });
            var name = fnProp.Getter.Invoke(person, null);
            var surname = lnProp.Getter.Invoke(person, null);
            return (name, surname);
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
