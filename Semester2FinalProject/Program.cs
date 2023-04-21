using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] megbizasAdatok;
            BeosztottLista beosztottLista;
            IFeladat[] feladatok;

            try
            {
                Beallitasok.Betoltes();
                megbizasAdatok = Beallitasok.MegbizasBetoltese();
                beosztottLista = Beallitasok.BeosztottakBetoltese();
                feladatok = Beallitasok.FeladatokBetoltese();
                Megbizas megbizas = new Megbizas(megbizasAdatok[0], megbizasAdatok[1], feladatok);
                BeosztasKezelo beosztasKezelo = new BeosztasKezelo(megbizas, beosztottLista);
            }
            catch (HibasMegbizasKivetel)
            {
                Console.WriteLine("A program nem tudta kezelni a megbízást -- A megbízáshoz tartozó txt fájl hibás adatokat tartalmazott!");
            }

            

            
            
        }
    }
}
