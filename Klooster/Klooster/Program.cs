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
            Console.WriteLine("Welkom bij KloosterScape! Maak jou character aan:");
            Console.Write("Naam: ");
            string sNaam = Console.ReadLine();
            Pater pater = null;
            Persoonlijkheid persoonlijkheid = null;
            bool bCharacterAangemaakt = false;
            while (!bCharacterAangemaakt)
            {
                Console.Write("\nWil je zelf uw persoonlijkheid aanmaken? Y/N ");
                string sRandomPersoonlijkheid = Console.ReadLine();
                if (sRandomPersoonlijkheid == "Y")
                {
                    Console.Write("\nGoedheid: [0-99] ");
                    int iGoedheid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nCreativiteit: [0-99] ");
                    int iCreativiteit = Convert.ToInt32(Console.ReadLine());
                    persoonlijkheid = new Persoonlijkheid(iGoedheid, iCreativiteit);
                    bCharacterAangemaakt = true;
                }
                else if (sRandomPersoonlijkheid == "N")
                {
                    persoonlijkheid = new Persoonlijkheid();
                    bCharacterAangemaakt = true;
                }
                else
                {
                    Console.WriteLine("\nGelieve Y/N te typen!");
                }
            }
            pater = new Pater(sNaam, persoonlijkheid, inspiratie);
            Console.WriteLine(string.Format("Proficiat! De volgende pater is aangemaakt voor u: {0}", pater));
            //Pater yannis = new Pater(sNaam, new Persoonlijkheid(21, 66), inspiratie);
            for(int i = 1; i < 7; i++)
            {
                pater.Bid();
            }
            for (int i = 1; i < 12; i++)
            {
                pater.Luister(new Woord(new Gedachte(5, new Persoonlijkheid()), new Persoonlijkheid()));
            }
            pater.DenkNa();
            Console.WriteLine(pater.ToString());
            Console.Read();
        }
    }
}
