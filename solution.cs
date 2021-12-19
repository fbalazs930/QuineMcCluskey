using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinneMcCluskey_TC02SC
{
    
    class Program
    {
        static int vSzama()
        {
            Console.WriteLine("Adja meg a változók számát:");
            return Convert.ToInt32(Console.ReadLine());
        }
        static int[] mIndex()
        {
            Console.WriteLine("Adja meg a minterm indexeket vesszőkkel elválasztva:");
            string[] indexek = Console.ReadLine().Split(',');
            int[] ind = new int[indexek.Length];
            for (int i = 0; i < indexek.Length; i++)
            {
                ind[i] = Convert.ToInt32(indexek[i]);
            }
            return ind;
        }
        static void feladat(int vSzam, int[] mIndexek)
        {
            Console.Write("Q(");
            for (int i = 65; i < 65 + vSzam; i++)//65 = 'A'
            {
                Console.Write(Convert.ToChar(i));
            }
            Console.Write(") mi(i=");
            for (int i = 0; i < mIndexek.Length; i++)//formázott kiírás
            {
                if (i < mIndexek.Length-1)
                {
                    Console.Write(mIndexek[i] + ", ");
                }
                else
                {
                    Console.Write(mIndexek[i] + ")\n");
                }
            }
        }
        static int hanyEgyes(string bin)
        {
            int egy=0;
            for (int i = 0; i < bin.Length; i++) if (bin[i] == '1') egy++;
            return egy;
        }
         static int hanyBit(int[] mIndexek)
        {
            int max = 0;
            for (int i = 1; i <= mIndexek[mIndexek.Length - 1]; i *= 2) max++;
            return max;
        }
        
        static void Main(string[] args)
        {
            #region 0. rész
            int vSzam = vSzama();//változók száma
            int[] mIndexek = mIndex();//minter indexek
            Array.Sort(mIndexek);
            Console.Clear();
            feladat(vSzam, mIndexek);
            int n, db = 0, ki;
            string[] bin = new string[mIndexek.Length];//mIndexek binári kódja
            string s = "";
            int max = hanyBit(mIndexek);//megadja hány biten kell tárolni
            

            for (int i = 0; i < mIndexek.Length; i++)//dec -> bin
            {
                n = mIndexek[i];
                for (int j = 0; j < max; j++)//felezgetés
                {
                    ki = (n % 2 == 0) ? 0 : 1;
                    s += ki;
                    n /= 2;
                    db++;
                }
                while (s.Length != max)//kipótolja nullákkal, amíg nem lesz olyan hosszú, ahány bit kell
                {
                    s += "0";
                }
                db = 0;
                for (int j = s.Length - 1; j >= 0; j--) bin[i] += s[j];//visszafele || 110 -> 011 
                s = "";
            }
            /*dec | bin kiírás
             * for (int i = 0; i < mIndexek.Length; i++)
                {
                    Console.WriteLine("{0,2} {1}", mIndexek[i], bin[i]);
                }
             */
            #endregion

            #region 1. rész
            int[] egyesek = new int[bin.Length];
            for (int i = 0; i < bin.Length; i++)
            {
                egyesek[i] = hanyEgyes(bin[i]);
            }

            List<int> egyesDb = new List<int>();
            for (int i = 0; i < egyesek.Length; i++)
            {
                if (egyesDb.Contains(egyesek[i]) == false)
                {
                    egyesDb.Add(egyesek[i]);
                }
            }
            egyesDb.Sort();
            int dbka = 0;
            string ujSorrend = "";
            for (int i = 0; i < egyesDb.Count; i++)
            {
                Console.WriteLine("_____{0}_____", egyesDb[i]);
                for (int j = 0; j < bin.Length; j++)
                {
                    if (hanyEgyes(bin[j]) == egyesDb[i])
                    {
                        dbka++;
                        Console.WriteLine(mIndexek[j]);
                        ujSorrend += mIndexek[j]+" ";
                    }
                }
                egyesDb[i] = dbka;
                dbka = 0;
            }
            #endregion

            #region 2. rész
            Console.WriteLine();
            int[] ujSorrendIndexek = new int[mIndexek.Length];
            for (int i = 0; i < ujSorrendIndexek.Length; i++)
            {
                string[] split = ujSorrend.Split(' ');
                if (split!=null)
                {
                    ujSorrendIndexek[i] = Convert.ToInt32(split[i]);
                }
            }


            int[] nullatolEgyesDbig = new int[egyesDb.Count];
            int sum = 0;
            for (int i = 0; i < egyesDb.Count; i++)
            {
                sum += egyesDb[i];
                nullatolEgyesDbig[i] = sum;
            }
            int a = 0, b = 0, c=0,d=0, e=0, f=0;

            int[] hatvanyok = new int[max];
            for (int i = 1; i < max; i*=2) hatvanyok[i] = i;
            Console.WriteLine("hatvanyok");
            foreach (int item in hatvanyok)
            {
                Console.WriteLine(item);
            }
            for (int i = a; i < egyesDb.Count-1; i++)
            {
                b += egyesDb[i];
                for (c = a; c < b; c++)
                {
                    for (d = nullatolEgyesDbig[i]; d < nullatolEgyesDbig[i+1]; d++)
                    {
                        for (int dzs = 0; dzs < hatvanyok.Length; dzs++)
                        {
                            if (ujSorrendIndexek[d] - ujSorrendIndexek[c] == hatvanyok[dzs])
                            {
                                Console.WriteLine("{0}, {1} ({2})", ujSorrendIndexek[c], ujSorrendIndexek[d], ujSorrendIndexek[d] - ujSorrendIndexek[c]);
                            }
                        }
                    }
                    d = nullatolEgyesDbig[i];
                    Console.WriteLine("_____");
                }
                a = c;
            }
            #endregion

            Console.ReadLine();
        }
    }
}
