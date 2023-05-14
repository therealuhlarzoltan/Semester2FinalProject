using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal class Program
    {
        static void ListaTeszt()
        {
            BeosztottLista lista = new BeosztottLista();
            Beosztott elso = new Beosztott("első", 1, 1);
            Beosztott masodik = new Beosztott("második", 2, 1);
            Beosztott harmadik = new Beosztott("harmadik", 3, 1);
            Beosztott negyedik = new Beosztott("negyedik", 4, 1);
            Beosztott otodik = new Beosztott("ötödik", 5, 1);

            lista.Hozzaadas(elso);
            lista.Hozzaadas(masodik);
            lista.Hozzaadas(harmadik);
            lista.Hozzaadas(negyedik);
            lista.Hozzaadas(otodik);

            int hossz = lista.Hossz();
            Beosztott elsoBeosztott = lista.BeosztottIndex(0);
            Beosztott otodikBeosztott = lista.BeosztottIndex(4);
            Beosztott harmadikBeosztott = lista.BeosztottIndex(2);

            int index1 = lista.BeosztottKereso(ref elso);
            int index3 = lista.BeosztottKereso(ref harmadik);
            int index5 = lista.BeosztottKereso(ref otodik);

            lista.Eltavolitas(0);
            lista.Eltavolitas(3);
            lista.Eltavolitas(2);
        }
        static void IdoTullepesKijelzo(IFeladat feladat)
        {
            Feladat feladat1 = feladat as Feladat;
            Console.WriteLine($"A {feladat1.Megnevezes} feladatnál időtúllépés történt; tervezett munkaórák : {feladat1.IdoIgeny}, ténylegesen ráfordított munkaórák: {feladat1.OsszesMunkaOra()}");
        }
        
        static void Main(string[] args)
        {
            //ListaTeszt();

            string[] megbizasAdatok;
            BeosztottLista beosztottLista;
            IFeladat[] feladatok;
            IFeladat[] beosztas;
            Megbizas megbizas = null;
            BeosztasKezelo beosztasKezelo = null;

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
                Console.WriteLine("A program nem tudta kezelni a megbízást -- A megbízáshoz tartozó txt fájl hibás adatokat tartalmazott!");
            }

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
                Console.WriteLine($"A megbízást nem lehet teljesíteni -- Nincs elég besoztott a teljesítéshez!");
            }
            

            

            
            
        }
    }
}
