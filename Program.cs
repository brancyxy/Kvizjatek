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

        static void Main(string[] args)
        {   
            Adatokatbe();
            Kerdessor();
            Console.ReadKey();
        }

        private static List<Kerdes> Kerdessor() //Összekeveri a 3 listát, majd az első 5 mindháromból lesz a kérdéssor
        {
            Console.WriteLine("Kérdéssor létrehozása");
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
            Console.WriteLine("\nKész!");
            return Kerdessor;
        }

        private static void Adatokatbe()
        {
            Nyeremenytbe(ref nyeremenyosszegek);
            Console.WriteLine("Kérdések beolvasása folyamatban");
            var sr1 = new StreamReader(@"konnyukerdesek.txt",Encoding.UTF8);
            Kerdesfeltoltes(ref konnyukerdesek,sr1);
            var sr2 = new StreamReader(@"kozepeskerdesek.txt");
            Kerdesfeltoltes(ref kozepeskerdesek,sr2);
            var sr3 = new StreamReader(@"nehezkerdesek.txt");
            Kerdesfeltoltes(ref nehezkerdesek,sr3);
            Console.WriteLine("\nKész!");
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


        private static void Kerdesfeltoltes(ref List<Kerdes> lista, StreamReader sr)
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
