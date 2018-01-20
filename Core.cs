using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Kvizjatek
{
    class Core
    {
        internal static void Kiirmindent(Kerdes kerdes, int nyertkerdes, int i, List<Kerdes> kerdessor, List<string> nyeremenyosszegek)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("{0,-2}. kérdés", nyertkerdes + 1);

            Console.SetCursorPosition(Console.WindowWidth - 20, 0);
            Console.Write("Összeg:{0,-13}", nyeremenyosszegek[nyertkerdes]);


            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(0, 1);
            Console.WriteLine("\n{0}\n", kerdessor[i].kerdes);

            foreach (var s in kerdessor[i].valaszok)
            {
                Console.WriteLine(s);
            }

            Console.ResetColor();
            Console.WriteLine("Nyomd meg a 4 betű valamelyikét a válasz megjelöléséhez, vagy ENTERT a a további opciókhoz.");

        }


        public static char Megjeloles(ConsoleKeyInfo valasz)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nBiztosan megjelölöd a(z) {0} választ? Y/N", valasz.Key);
            Console.ResetColor();
            bool done = false;
            while (!done)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    switch (valasz.Key)
                    {
                        case ConsoleKey.A:
                            return 'A';

                        case ConsoleKey.B:
                            return 'B';

                        case ConsoleKey.C:
                            return 'C';

                        case ConsoleKey.D:
                            return 'D';

                    }
                }
                else if (key.Key == ConsoleKey.N)
                {
                    return'0';
                }
          
            }
            return '0';
        }

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

        internal static bool Visszajelzes(char jovalasz, char megjeloltvalasz)
        {
            if (jovalasz == megjeloltvalasz)
            {
                Console.ForegroundColor =(ConsoleColor)10;
                Console.WriteLine("\nJó válasz!");
                Console.ResetColor();
                Thread.Sleep(1500);

                return true;
            }
            else
            {
                Console.ForegroundColor = (ConsoleColor)12;
                Console.WriteLine("\nRossz válasz!");
                Console.ResetColor();
                Thread.Sleep(1500);

                return false;
            }
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

       public static bool Valaszakerdesre(ConsoleKeyInfo key, Kerdes kerdes, int nyertkerdes, int i, List<Kerdes> kerdessor, List<string> nyeremenyosszegek, ref char megjeloltvalasz)
        {
            {
                megjeloltvalasz = Core.Megjeloles(key);

                if (megjeloltvalasz == 'A' || megjeloltvalasz == 'B' || megjeloltvalasz == 'C' || megjeloltvalasz == 'D')
                    return true;
                else
                {
                    Console.Clear();
                    Core.Kiirmindent(kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek);

                    return false;
                }
            }
        }

        internal static bool Betutjelolt(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.B || key.Key == ConsoleKey.C || key.Key == ConsoleKey.D) return true;
            else return false;
        }
    }
}
