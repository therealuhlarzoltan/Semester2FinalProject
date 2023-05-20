using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal class Program
    {
      
        static void IdoTullepesKijelzo(IFeladat feladat)
        {
            Feladat feladat1 = feladat as Feladat;
            Console.WriteLine($"A(z) {feladat1.Megnevezes} feladatnál időtúllépés történt; tervezett munkaórák : {feladat1.IdoIgeny}, ténylegesen ráfordított munkaórák: {feladat1.OsszesMunkaOra}");
        }
        
        static void Main(string[] args)
        {
            //ListaTeszt();

            // Szükséges változók deklarálása
            string[] megbizasAdatok;
            BeosztottLista beosztottLista;
            IFeladat[] feladatok;
            IFeladat[] beosztas;

            // Szükséges változók inicializálása, a try blokkok miatt szükséges
            Megbizas megbizas = null;
            BeosztasKezelo beosztasKezelo = null;

            // Az összes szükséges adatot megpróbáljuk betölteni a txt-ből
            try
            {
                Beallitasok.Betoltes();
                megbizasAdatok = Beallitasok.MegbizasBetoltese();
                beosztottLista = Beallitasok.BeosztottakBetoltese();
                feladatok = Beallitasok.FeladatokBetoltese();
                megbizas = new Megbizas(megbizasAdatok[0], megbizasAdatok[1], feladatok);
                beosztasKezelo = new BeosztasKezelo(megbizas, beosztottLista);
                beosztasKezelo.IdoIgenyTullepes += IdoTullepesKijelzo;
            }
            catch (HibasMegbizasKivetel)
            {
                // A betöltés közben hiba történt, vagyis a txt szövege nem követte a megadott formátumot
                Console.WriteLine("A program nem tudta kezelni a megbízást -- A megbízáshoz tartozó txt fájl hibás adatokat tartalmazott!");
            }

            // Megpróbáljuk elkészíteni a besoztást
            try
            {
                beosztas = beosztasKezelo?.BeosztasKeszites();
                Eredmeny.BeosztasMentese(megbizas, beosztas);
                if (beosztasKezelo != null)
                {
                    Console.WriteLine("A beosztás elkészült -- A 'beosztas.txt' fájlban olvasható.");
                }
                
            }
            catch (MegbizasNemTeljesithetoKivetel kivetel)
            {
                Console.WriteLine();
                Console.WriteLine($"A megbízást nem lehet teljesíteni!");
            }
            

            

            
            
        }
    }
}
