﻿using System;
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

        static int[] szavazatok = new int[4];
        static string a = "nope";
        static List<Kerdes> kerdessor = new List<Kerdes>();
        static int nyertkerdes = 0;
        public struct Segitseghasznalat
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
                Gamebody(ref segitsegek);
            }
        }

        private static byte Lobby()
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

        private static byte Gamebody(ref Segitseghasznalat s)
        {
            nyertkerdes = 0;
            for (int i = 0; i < kerdessor.Count; i++)
            {
                char megjeloltvalasz='0';
                Core.Kiirmindent(kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek, ref a);

                bool done = false;
                while (!done)
                {

                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (Core.Betutjelolt(key))
                    {
                        done = Core.Valaszakerdesre(key, kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek, ref megjeloltvalasz,a);
                        if (!Core.Betutjelolt(key) ||kerdessor[i].valaszok[Core.Megszerezindexet(megjeloltvalasz)]=="" )//A felezésnél kihúzott válaszokat ne lehessen bejelölni
                        {
                            done = false;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nKi van húzva az a válasz!");
                            Console.ResetColor();


                            Console.ReadKey();
                            Core.Kiirmindent(kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek,ref a);
                        }
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        Core.Kiirsegitsegek(s);
                        bool done2 = false;
                        while (!done2)
                        {
                            ConsoleKeyInfo key2 = Console.ReadKey(true);
                            if (key2.Key == ConsoleKey.Spacebar)
                            {

                                if (Core.Megerosites("Biztos, hogy szeretnél távozni?\n(A jelenlegi pénzed megmarad, de nem fogod tudni folytatni.)"))
                                {
                                    Eredmenylogolas(nyeremenyosszegek[nyertkerdes]);
                                    return 0;
                                }
                                else
                                {
                                    done2 = true;
                                    Core.Kiirmindent(kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek, ref a);
                                }
                            }

                            else if (key2.Key == ConsoleKey.Escape)
                            {
                                done2 = true;
                                Core.Kiirmindent(kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek, ref a);
                            }

                            else if (key2.Key == ConsoleKey.Q)
                            {
                                if (Core.Megerosites("Biztos, hogy igénybe akarod venni ezt a segítséget?"))
                                {
                                    kerdessor[i]=Core.Felezes(kerdessor[i], nyertkerdes,i, kerdessor, nyeremenyosszegek,ref s);
                                    Core.Kiirmindent(kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek, ref a);
                                    done2 = true;
                                }
                                else
                                {
                                    done2 = true;
                                    Core.Kiirmindent(kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek, ref a);
                                }
                            }
                            else if (key2.Key == ConsoleKey.W)
                            {
                                if (Core.Megerosites("Biztos, hogy igénybe akarod venni ezt a segítséget?"))
                                {
                                    done2 = true;
                                    a=Core.Telefonossegitseg(kerdessor[i],nyertkerdes,ref s);
                                    Core.Kiirmindent(kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek, ref a);
                                }
                                else
                                {
                                    done2 = true;
                                    Core.Kiirmindent(kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek, ref a);
                                }
                            }
                            else if (key2.Key == ConsoleKey.E)
                            {
                                if (Core.Megerosites("Biztos, hogy igénybe akarod venni ezt a segítséget?"))
                                {
                                    szavazatok=Core.Kozonsegsegitsege(ref kerdessor, i, nyertkerdes, ref s);
                                    kerdessor[i] = Core.Kiirkozonseget(kerdessor, i,szavazatok);
                                    Core.Kiirmindent(kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek, ref a);
                                    done2 = true;
                                }
                                else
                                {
                                    done2 = true;
                                    Console.Clear();
                                    Core.Kiirmindent(kerdessor[i], nyertkerdes, i, kerdessor, nyeremenyosszegek, ref a);
                                }
                            }
                        }
                    }

                }
                if (Core.Visszajelzes(kerdessor[i].jovalasz, megjeloltvalasz) == true)
                {
                    nyertkerdes++;
                    a = "nope";
                }
                else
                {
                    Lose();
                    return 0;
                }
            }
            if (nyertkerdes == 15) Win();
            return 0;
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
            if (Core.Megerosites("El akarod menteni az eredményedet?"))
                {
                    var sw = new StreamWriter(@"eredmenyek.txt",append: true);
                    Console.Write("Név:");

                    string nev = Console.ReadLine();

                    sw.WriteLine(DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString()+ ';' + nev + ';' +nyeremeny);
                    sw.Close();
                }
            Endgame();
        }

        private static void Eredmenymegjelenites()
        {
            if(Core.Megerosites("Megjeleníted az eredményeket?"))
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (!File.Exists(@"eredmenyek.txt")) File.Create(@"eredmenyek.txt").Close();
                var sr = new StreamReader(@"eredmenyek.txt");
                while (!sr.EndOfStream)
                {
                    string[] sor = sr.ReadLine().Split(';');
                    for (int i = 0; i < sor.Length; i++)
                    {
                        Console.Write("{0,-25}", sor[i]);
                    }
                    Console.WriteLine();
                }
                sr.Close();


                Console.ResetColor();

                Console.ReadKey();
            
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
            a = "nope";
            s.telefonsegitseg = true;
            s.kozonsegsegitseg = true;
            s.felezes = true;
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
            kerdesek.Clear();
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
                Thread.Sleep(11);
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
                Thread.Sleep(11);
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
