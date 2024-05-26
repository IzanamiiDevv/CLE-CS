using System;
using CLE;

namespace Main
{
    class Program : Command
    {
        public static void Main(string[] args)
        {
            string command = "cd";
            try
            {
                string output = Exec(command);
                Console.WriteLine("Command output:\n" + output);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}