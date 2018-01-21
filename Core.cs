using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Kvizjatek
{
    class Core
    {
        static Random r = new Random();

        internal static void Kiirmindent(Kerdes kerdes, int nyertkerdes, int i, List<Kerdes> kerdessor, List<string> nyeremenyosszegek)
        {
            Console.Clear();
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
            if (Megerosites("\nBiztosan megjelölöd a(z)" + valasz.Key + " betűt?"))
            {
                Console.ResetColor();
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
            else
            {
                Console.ResetColor();
                return '0';
            }
            return '0';
        }

       public static void NyeremenytKiir(List<string> nyeremenyek)
       {
            Console.WriteLine("A kérdések megválaszolásával megszerezhető pénzösszeg:");
            for (int i = 1; i < nyeremenyek.Count; i++)
            {
                if (i % 5 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine("{0, 2}. kérdés: {1, 13}", i, nyeremenyek[i]);
                Console.ResetColor();
            }
            Console.WriteLine("A nyeremény nem kumulatív, vagyis nem adódnak össze a kérdésekhez tartozó összegek.");
            Console.WriteLine("A pirossal jelölt határ elérése után az előző nyeremény garantált veszteség esetén is!");
            Next();
       }

        internal static bool Megerosites(string s)
        {
            Console.WriteLine(s+ " Y/N");
            bool done = false;
            while (!done)
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
            return false;
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
            if (Core.Megerosites("Befejezed a jelenlegi összeggel ?"))
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    return true;
                }
            }
            return false;
        }

        internal static Kerdes Felezes(Kerdes kerdes, int nyertkerdes,int h, List<Kerdes> kerdessor, List<string> nyeremenyosszegek,ref Kvizjatek.Program.Segitseghasznalat s)
        {
            if (s.felezes == true)
            {
                s.felezes = false;
                Kerdes felezett = kerdes;
                Console.Clear();
                byte jovalasz = Megszerezindexet(kerdes.jovalasz);
                byte kihuzott = 0;
                int votma = 4;
                while(true)
                {
                    if (kihuzott == 2)
                    {
                        return felezett;
                    }
                        byte index = (byte)r.Next(0, felezett.valaszok.Length);
                        if (index != jovalasz && index != votma)
                        {
                            votma = index;
                            felezett.valaszok[index] = "";
                            kihuzott++;
                        }                  
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nA segítség már használt!");
                Console.ResetColor();
                return kerdes;
            }
        }

        public static byte Megszerezindexet(char c)
        {
            switch (c)
            {
                case 'A':
                    return 0;

                case 'B':
                    return 1;

                case 'C':
                    return 2;

                case 'D':
                    return 3;
            }
            return 123;
        }

        public static void Szabalyzatkiiras()
       {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("A játék során 15 kérdés lesz feltéve, minden kérdéshez tartozik egy pénzösszeg\nHa sikeresen megválaszolod a kérdést, akkor 3 lehetőséged van:\n1) Kibankolsz és elviszed a jelenlegi összeget\n2) Folytatod, és ha nyersz megnyered a 40 millió forintos főnyereményt\n3) Ha veszítesz, minden 5. kérdés után a nyeremény garantát, így ha már az 5. kérdésre válaszoltál, a nyeremény biztosított!");
            Console.ResetColor();

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

        internal static void Kiirsegitsegek(Kvizjatek.Program.Segitseghasznalat segitsegek)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Egyébb opciók:\n");
            Console.WriteLine("Bankolás (Jelenlegi pénzösszeg elvitele és játék befejezése)");
            Console.WriteLine("Segítségek:");

            Console.Write("Q) Felezés (elrejt 2 rossz választ)");
            Vizsgalsegitseget(segitsegek.felezes);
            Console.Write("W) Telefonos segítség (100% helyesség az első kérdésnél, utána kérdésenként -3%");
            Vizsgalsegitseget(segitsegek.telefonsegitseg);
            Console.WriteLine("E) Közönség segítség (2000 szavazat, 400 tippel, 1600 a következőképpen számol:");
            Console.Write("100-(Helyesen megválaszolt kérdések száma*3) %");
            Vizsgalsegitseget(segitsegek.kozonsegsegitseg);
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("Nyomd meg a kérdés betűjét az igénybevételhez, SPACE-t a bankoláshoz vagy ESCAPE-t a kilépéshez");
            Console.WriteLine();

        }

        private static void Vizsgalsegitseget(bool segitseg)
        {
            if (segitseg == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" Igénybevehető!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Elhasznált segítség!");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
