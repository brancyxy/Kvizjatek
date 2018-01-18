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

                Gamebody();


                if(nyertkerdes==15) Win();
            }
        }

        private static void Gamebody()
        {
            for (int i = 0; i < kerdessor.Count; i++)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("{0,-2}. kérdés", nyertkerdes + 1);


                Console.SetCursorPosition(Console.WindowWidth - 20, 0);
                if (nyertkerdes > 0) Console.Write("Összeg:{0,-13}", nyeremenyosszegek[nyertkerdes - 1]);
                else Console.Write("Összeg:{0,-13}", nulla);
                Console.ReadKey();

                nyertkerdes++;
            }
        }

        static void Lose()
        {
            Console.Clear();

            Console.SetCursorPosition((int)(Console.WindowWidth / 2) - 30, (int)(Console.WindowHeight / 2) - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("GG REKT NUB");
            //// ha 5nél nagyobb.....
            Console.ReadKey();
            Console.ResetColor();
            Eredmenylogolas();
        } 
        static void Win()
        {
            Console.Clear();

            Console.SetCursorPosition((int)(Console.WindowWidth / 2) - 30, (int)(Console.WindowHeight / 2) - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nagyon béna gratulálószöveg, megnyerted a {0} főnyereményt!",nyeremenyosszegek[nyertkerdes-1]);
            Console.ReadKey();
            Console.ResetColor();


            Eredmenylogolas();

        }

        private static void Eredmenylogolas()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("El akarod menteni az eredményedet? Y/N");

            bool done = false;
            while (!done)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    var sw = new StreamWriter(@"eredmenyek.txt");
                    Console.Write("Név:");
                    string nev = Console.ReadLine();
                    sw.Write(DateTime.Now.ToLongDateString()+ ';' + nev + ';' + nyeremenyosszegek[nyertkerdes - 1]);
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

        private static void Endgame()
        {
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
            nyertkerdes = 0;
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
