using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitukset1
{
    public enum Luvut
    {
        Yksi = 1,
        Kaksi = 2,
        Kolme = 3
    }

    class Program
    {
        static void Main(string[] args)
        {
            int luku;

            Console.Write("Anna luku > ");

            while (!int.TryParse(Console.ReadLine(), out luku))
            {
                Console.Write("Anna luku > ");
            }

            StringBuilder message = new StringBuilder();

            if(luku == (int)Luvut.Yksi)
            {
                message.AppendFormat("Annoit luvun {0}", Luvut.Yksi);
            }
            else if (luku == (int)Luvut.Kaksi)
            {
                message.AppendFormat("Annoit luvun {0}", Luvut.Kaksi);
            }
            else if (luku == (int)Luvut.Kolme)
            {
                message.AppendFormat("Annoit luvun {0}", Luvut.Kolme);
            }
            else
            {
                message.Append("Annoit jonku muun luvun");
            }

            Console.WriteLine(message.ToString());

            Console.ReadKey();
        }
    }
}
