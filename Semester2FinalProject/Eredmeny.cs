using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal static class Eredmeny
    {
        public static void BeosztasMentese(Megbizas megbizas, IFeladat[] beosztas)
        {
            if (megbizas != null && beosztas != null)
            {
                StreamWriter streamWriter = new StreamWriter("beosztas.txt", false, Encoding.UTF8);
                streamWriter.WriteLine($"{megbizas.Megbizo} - {megbizas.Megnevezes} időbeosztás:");
                streamWriter.WriteLine();

                int megbizasSzakmaiÉrtékelése = 0;
                int megbizasMunkaOrak = 0;
                int megbizasTenylegesMunkaorak = 0;

                for (int i = 0; i < beosztas.Length; i++)
                {
                    Feladat feladat = beosztas[i] as Feladat;
                    streamWriter.WriteLine($"Feladat: {feladat.Megnevezes}:");
                    streamWriter.WriteLine($"Kezdődátum: {feladat.KezdoDatum.ToLongDateString()}");
                    streamWriter.WriteLine($"Időigény: {feladat.IdoIgeny} óra");
                    megbizasMunkaOrak += feladat.IdoIgeny;
                    streamWriter.WriteLine($"Ténylegesen ráfordított idő: {feladat.OsszesMunkaOra()}");
                    megbizasTenylegesMunkaorak += feladat.OsszesMunkaOra();

                    streamWriter.WriteLine("A feladaton dolgozó beosztottak:");
                    BeosztottLista beosztottak = feladat.Beosztottak;
                    int feladatSzakmaiÉrtékelése = 0;
                    for (int j = 0; j < beosztottak.Hossz(); j++)
                    {
                        streamWriter.WriteLine(beosztottak.BeosztottIndex(j).ToString());
                        feladatSzakmaiÉrtékelése += beosztottak.BeosztottIndex(j).SzakmaiErtekeles;
                        feladatSzakmaiÉrtékelése += megbizasSzakmaiÉrtékelése;
                    }
                    streamWriter.WriteLine();
                }
                streamWriter.WriteLine($"Tervezett munkaórák: {megbizasMunkaOrak} óra");
                streamWriter.WriteLine($"Ténylegesen ráfordított munkaórák: {megbizasTenylegesMunkaorak} óra");
                streamWriter.WriteLine($"A megbízáson dolgozó beosztottak összesített szakmai értékelése: {megbizasSzakmaiÉrtékelése}");

                streamWriter.Close();
            }
        }
    }
}
