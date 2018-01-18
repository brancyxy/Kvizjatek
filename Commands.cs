using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvizjatek
{
    class Commands
    {
       public static void NyeremenytKiir(List<string> nyeremenyek)
        {
            Console.WriteLine("A kérdések megválaszolásával megszerezhető pénzösszeg:");
            for (int i = 0; i < nyeremenyek.Count; i++)
            {
                if ((i + 1) % 5 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine("{0, 2}. kérdés: {1, 13}", i+1, nyeremenyek[i]);
                Console.ResetColor();
            }
            Console.WriteLine("A nyeremény nem kumulatív, vagyis nem adódnak össze a kérdésekhez tartozó összegek.");
            Console.WriteLine("A pirossal jelölt határ elérése után az előző nyeremény garantált veszteség esetén is!");
            Next();
        }

       public static bool Bankolas()
        {
            Console.Clear();
            Console.SetCursorPosition((int)(Console.WindowWidth / 2) - 30, (int)(Console.WindowHeight / 2) - 1);
            Console.Write("Befejezed a jelenlegi összeggel? Y/N");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    return true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    return false;
                }
            }
        }

       public static void Szabalyzatkiiras()
       {
            Console.WriteLine("A játék során 15 kérdés lesz feltéve, minden kérdéshez tartozik egy pénzösszeg\nHa sikeresen megválaszolod a kérdést, akkor 3 lehetőséged van:\n1) Kibankolsz és elviszed a jelenlegi összeget\n2) Folytatod, és ha nyersz megnyered a 40 millió forintos főnyereményt\n3) Ha veszítesz, minden 5. kérdés után a nyeremény garantát, így ha már az 5. kérdésre válaszoltál, a nyeremény biztosított!");
            Next();
        }
        private static void Next()
        {
            Console.WriteLine("Nyomj meg egy billentyűt a folytatáshoz!");
            Console.ReadKey();
        }
    }
}
