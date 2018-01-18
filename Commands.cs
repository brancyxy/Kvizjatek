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
            Console.WriteLine();
        }

        public static void Bankolas()
        {

        }
    }
}
