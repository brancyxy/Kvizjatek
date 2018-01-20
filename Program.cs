using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Kvizjatek
{
    class Program
    {
        static Random r = new Random();

        static List<string> nyeremenyosszegek = new List<string>();


        static List<Kerdes> kerdesek = new List<Kerdes>();


        static List<Kerdes> kerdessor = new List<Kerdes>();
        static int nyertkerdes = 0;
        struct Segitseghasznalat
        {
            public bool telefonsegitseg, kozonsegsegitseg, felezes;
        }
 
        static void Main(string[] args)
        {
            Console.Title = "Legyen Ön is milliomos!";
            var segitsegek = new Segitseghasznalat();
            while (true)
            {
                Ujjatek(ref segitsegek);
                Lobby();
                Gamebody();
            }
        }

        private static int Lobby()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Üdvözöllek a Legyen Ön is milliomosban!");
                Console.WriteLine("Nyomj ENTER-t a folytatáshoz, H-t a szabályzat megtekintéséhez, és SPACE-t a nyeremények kiírásához.\n");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    return 0;
                }
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    Core.NyeremenytKiir(nyeremenyosszegek);
                }
                else if (key.Key == ConsoleKey.H)
                {
                    Core.Szabalyzatkiiras();
                }
            }
        }

        private static void Gamebody()
        {
            nyertkerdes = 0;
            for (int i = 0; i < kerdessor.Count; i++)
            {
                Console.Clear();
                char megjeloltvalasz='0';
                Core.Kiirmindent(kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek);

                bool done = false;
                while (!done)
                {

                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (Core.Betutjelolt(key))
                    {
                        done= Core.Valaszakerdesre(key, kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek, ref megjeloltvalasz);
                    }
                    else if(key.Key== ConsoleKey.Enter)
                    {

                    }
                }


                // a segítség hívások  QWE lesznek.
                if (Core.Visszajelzes(kerdessor[i].jovalasz, megjeloltvalasz) == true) nyertkerdes++;
                else
                {
                    Lose();
                    i = 1234567;
                }
            }
            if (nyertkerdes == 15) Win();
        }

        static void Lose()
        {
            Console.Clear();

            Console.SetCursorPosition((int)(Console.WindowWidth / 2) - 20, (int)(Console.WindowHeight / 2) - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("GG REKT NUB");
            Console.ReadKey();
            Console.Clear();
            Console.ResetColor();
            Console.SetCursorPosition(5, (int)(Console.WindowHeight / 2) - 1);
            if (nyertkerdes >= 5)
            {
                if (nyertkerdes >= 10)
                {
                    Console.WriteLine("Mivel a 10. kérdésnél továbbjutottál, {0} nyereményed még így is van.", nyeremenyosszegek[10]);
                    Eredmenylogolas(nyeremenyosszegek[10]);
                }
                else
                {
                    Console.WriteLine("Mivel az 5. kérdésnél továbbjutottál, {0} nyereményed még így is van.", nyeremenyosszegek[5]);
                    Eredmenylogolas(nyeremenyosszegek[5]);
                }
            }
            Endgame();
        } 

        static void Win()
        {
            Console.Clear();

            Console.SetCursorPosition(5, (int)(Console.WindowHeight / 2) - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nagyon béna gratulálószöveg, megnyerted a {0} főnyereményt! Nyomj meg egy billentyűt a folytatáshoz! ",nyeremenyosszegek[nyertkerdes]);
            Console.ReadKey();
            Console.ResetColor();


            Eredmenylogolas(nyeremenyosszegek[nyertkerdes]);

        }

        private static void Eredmenylogolas(string nyeremeny)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("El akarod menteni az eredményedet? Y/N");

            bool done = false;
            while (!done)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    var sw = new StreamWriter(@"eredmenyek.txt",append: true);
                    Console.Write("Név:");

                    string nev = Console.ReadLine();

                    sw.WriteLine(DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString()+ ';' + nev + ';' +nyeremeny);
                    sw.Close();
                    done = true;

                }
                else if (key.Key == ConsoleKey.N)
                {
                    done = true;
                }
            }

            Endgame();
        }

        private static void Eredmenymegjelenites()
        {
            Console.WriteLine("Megjeleníted az eredményeket? Y/N");

            bool done = false;
            while (!done)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (!File.Exists(@"eredmenyek.txt")) File.Create(@"eredmenyek.txt");
                    var sr = new StreamReader(@"eredmenyek.txt");
                    while (!sr.EndOfStream)
                    {
                        string[] sor = sr.ReadLine().Split(';');
                        for (int i = 0; i < sor.Length; i++)
                        {
                            Console.Write("{0,-25}",sor[i]);
                        }
                        Console.WriteLine();
                    }
                    Console.ResetColor();

                    sr.Close();
                    done = true;
                    Console.ReadKey();
                }
                else if (key.Key == ConsoleKey.N)
                {
                    done = true;
                }
            }
        }

        private static void Endgame()
        {
            Eredmenymegjelenites();
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Nyomj ESCAPE-t a kilépéshez, ENTER-t új játék kezdéséhez.");

            bool done = false;
            while (!done)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    done = true;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }

        static void Ujjatek(ref Segitseghasznalat s)
        {
            Console.ResetColor();
            s.telefonsegitseg = false;
            s.kozonsegsegitseg = false;
            s.felezes = false;
            Console.Clear();
            Adatokatbe();
            Console.SetCursorPosition((int)(Console.WindowWidth / 2.5), 0);
            kerdessor = Kerdessor();
            Console.SetCursorPosition((int)(Console.WindowWidth / 2)-15, (int)(Console.WindowHeight / 2)-1);
            Console.WriteLine("Nyomj ENTERT a folytatáshoz");
            Console.SetCursorPosition(0, 0);

            bool done = false;
            while (!done)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    done = true;
                    Console.Clear();
                }
            }
        }

        static List<Kerdes> Kerdessor()
        {
            Console.Write("Kérdéssor létrehozása");
            Console.SetCursorPosition((int)(Console.WindowWidth / 2.5), 1);
            List<Kerdes> Kerdessor = new List<Kerdes>();
            Osszekever(ref kerdesek);
            for (int i = 0; i < 15; i++)
            {
                Kerdessor.Add(kerdesek[i]);
            }
            kerdesek.Clear(); //kitörli a memóriából a feleslegessé vált listát
            return Kerdessor;
        }

        static void Adatokatbe()
        {
            Nyeremenytbe(ref nyeremenyosszegek);
            Console.WriteLine("Kérdések beolvasása folyamatban");
            var sr = new StreamReader(@"kerdesek.txt",Encoding.UTF8);
            Kerdesfeltoltes(ref kerdesek,sr);
        }

        static void Osszekever(ref List<Kerdes> lista)
        {

            for (int t = 0; t < lista.Count; t++)
            {
                Console.Write(".");
                Kerdes tmp = lista[t];
                int rnd = r.Next(t, lista.Count);
                lista[t] = lista[rnd];
                lista[rnd] = tmp;
            }
        }

        static void Kerdesfeltoltes(ref List<Kerdes> lista, StreamReader sr)
        {
            while (!sr.EndOfStream)
            {
                Console.Write(".");
                string[] elsosor = new string[2];
                string[] valaszok = new string[4];
                elsosor = sr.ReadLine().Split(';'); // 1. elem a kérdés 2. a válasz
                for (int i = 0; i < valaszok.Length; i++)
                {
                    valaszok[i] = sr.ReadLine();
                }
                lista.Add(new Kerdes(elsosor[0], valaszok, Char.Parse(elsosor[1])));
            }
            sr.Close();
        }

        static void Nyeremenytbe(ref List<string> osszegek)
        {
            var sr = new StreamReader(@"nyeremenyek.txt", Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                osszegek.Add(sr.ReadLine());
            }
            sr.Close();
        }


    }
}
