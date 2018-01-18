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


        static List<Kerdes> konnyukerdesek = new List<Kerdes>();
        static List<Kerdes> kozepeskerdesek = new List<Kerdes>();
        static List<Kerdes> nehezkerdesek = new List<Kerdes>();

        static List<Kerdes> kerdessor = new List<Kerdes>();
        static int nyertkerdes = 0;
        static bool telefonsegitseg = false;
        static bool kozonsegsegitseg = false;
        static bool felezes = false;
        static string nulla = "0 Ft";
 
        static void Main(string[] args)
        {
            while (true)
            {
                Ujjatek();
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
                    Commands.NyeremenytKiir(nyeremenyosszegek);
                }
                else if (key.Key == ConsoleKey.H)
                {
                    Commands.Szabalyzatkiiras();
                }
            }
            return 0;
        }

        private static void Gamebody()
        {
            nyertkerdes = 0;
            for (int i = 0; i < kerdessor.Count; i++)
            {
                Console.Clear();
                char megjeloltvalasz='0';
                Console.SetCursorPosition(0, 0);
                Console.Write("{0,-2}. kérdés", nyertkerdes + 1);


                Console.SetCursorPosition(Console.WindowWidth - 20, 0);
                if (nyertkerdes > 0) Console.Write("Összeg:{0,-13}", nyeremenyosszegek[nyertkerdes - 1]);
                else Console.Write("Összeg:{0,-13}", nulla);


                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(0, 1);
                Console.WriteLine("\n{0}\n", kerdessor[i].kerdes);

                foreach (var s in kerdessor[i].valaszok)
                {
                    Console.WriteLine(s);
                }
                Console.ResetColor();
                Console.WriteLine("Nyomd meg a 4 betű valamelyikét a válasz megjelöléséhez, vagy ENTERT a a további opciókhoz.");

                Valaszakerdesre(ref megjeloltvalasz);


                //Ki kell még találni, hogy melyik billentyűk lesznek a segítség hívások, de most QWE lesz.
                if (megjeloltvalasz==kerdessor[i].jovalasz) nyertkerdes++;
                else
                {
                    i = 69696969; //most ez a szám tetszett ahhoz, hogy kilépjen a ciklusból
                    Lose();
                   
                }
            }
            if (nyertkerdes == 15) Win();
        }

        private static void Valaszakerdesre(ref char k)
        {
            bool done = false;
            while (!done)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.A)
                {
                    done= Commands.Megjeloles("A");
                    if (done == true)
                    {
                        k = 'A';
                    }
                }
                else if (key.Key == ConsoleKey.B)
                {
                    done = Commands.Megjeloles("B");
                    if (done == true)
                    {
                        k = 'B';
                    }
                }
                else if (key.Key == ConsoleKey.C)
                {
                    done = Commands.Megjeloles("C");
                    if (done == true)
                    {
                        k = 'C';
                    }
                }
                else if (key.Key == ConsoleKey.D)
                {
                    done = Commands.Megjeloles("D");
                    if (done == true)
                    {
                        k = 'D';
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                   //uigduiguidfiufd
                }
            }
        }

        static int Lose()
        {
            Console.Clear();

            Console.SetCursorPosition((int)(Console.WindowWidth / 2) - 30, (int)(Console.WindowHeight / 2) - 1);
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
                    Console.WriteLine("Mivel a 10. kérdésnél továbbjutottál, {0} nyereményed még így is van.", nyeremenyosszegek[9]);
                    Eredmenylogolas(nyeremenyosszegek[9]);
                }
                else
                {
                    Console.WriteLine("Mivel az 5. kérdésnél továbbjutottál, {0} nyereményed még így is van.", nyeremenyosszegek[4]);
                    Eredmenylogolas(nyeremenyosszegek[4]);
                }
            }
            Endgame();
            return 0;
        } 
        static void Win()
        {
            Console.Clear();

            Console.SetCursorPosition(5, (int)(Console.WindowHeight / 2) - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nagyon béna gratulálószöveg, megnyerted a {0} főnyereményt! Nyomj meg egy billentyűt a folytatáshoz! ",nyeremenyosszegek[nyertkerdes-1]);
            Console.ReadKey();
            Console.ResetColor();


            Eredmenylogolas(nyeremenyosszegek[nyertkerdes - 1]);

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


            ///az eredményeket vhol itt fogja megjeleníteni
            Endgame();
        }

        private static void Endgame()
        {
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

        static void Ujjatek()
        {
            Console.ResetColor();
            telefonsegitseg = false;
            kozonsegsegitseg = false;
            felezes = false;
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

        static List<Kerdes> Kerdessor() //Összekeveri a 3 listát, majd mindháromból az első 5 lesz a kérdéssor
        {
            Console.Write("Kérdéssor létrehozása");
            Console.SetCursorPosition((int)(Console.WindowWidth / 2.5), 1);
            List<Kerdes> Kerdessor = new List<Kerdes>();
            Osszekever(ref konnyukerdesek);
            Osszekever(ref kozepeskerdesek);
            Osszekever(ref nehezkerdesek);
            for (int i = 0; i < 5; i++)
            {
                Kerdessor.Add(konnyukerdesek[i]);
            }
            for (int i = 0; i < 5; i++)
            {
                Kerdessor.Add(kozepeskerdesek[i]);
            }
            for (int i = 0; i < 5; i++)
            {
                Kerdessor.Add(nehezkerdesek[i]);
            }
            konnyukerdesek.Clear(); //kitörli a memóriából a feleslegessé vált listákat
            kozepeskerdesek.Clear();
            nehezkerdesek.Clear();
            return Kerdessor;
        }

        static void Adatokatbe()
        {
            Nyeremenytbe(ref nyeremenyosszegek);
            Console.WriteLine("Kérdések beolvasása folyamatban");
            var sr1 = new StreamReader(@"konnyukerdesek.txt",Encoding.UTF8);
            Kerdesfeltoltes(ref konnyukerdesek,sr1);
            var sr2 = new StreamReader(@"kozepeskerdesek.txt");
            Kerdesfeltoltes(ref kozepeskerdesek,sr2);
            var sr3 = new StreamReader(@"nehezkerdesek.txt");
            Kerdesfeltoltes(ref nehezkerdesek,sr3);
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
