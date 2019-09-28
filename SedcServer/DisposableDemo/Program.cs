using System;

namespace DisposableDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ResourceUser resourceUser = new ResourceUser()) {

            }

            //ResourceUser resourceUser = new ResourceUser();
            //try
            //{
            //    var x = 0;
            //    var y = 7 / x;
            //}
            //finally
            //{
            //    resourceUser.CloseFile();
            //}

            Console.ReadLine();
        }
    }
}
