using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klooster
{
    class Program
    {
        static void Main(string[] args)
        {
            Inspiratie inspiratie = new Inspiratie();
            Pater yannis = new Pater("Yannis", new Persoonlijkheid(21, 41), inspiratie);
            yannis.Bid();
            Console.WriteLine(yannis.ToString());
            Console.Read();
        }
    }
}
