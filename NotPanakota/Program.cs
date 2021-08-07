using System;

namespace NotPanakota
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ConnectWithMinjustAPI connect = new ConnectWithMinjustAPI();
            Console.WriteLine(connect.GetDocument(9).ToString());
            //Console.WriteLine(connect.CheckAvailable());
        }
    }
}
