using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    // A megbízás txt fájlból való beolvasásához szükséges metódusok "gyűjteménye"
    // A metódusok HibasMegbizasKivetel típusú kivételt dobnak ha a txt fájl tartalma nem megfelelő
    internal static class Beallitasok
    {
        static string[] beolvasottSorok;

        public static void Betoltes()
        {
            try
            {
                beolvasottSorok = File.ReadAllLines("megbizas.txt", Encoding.UTF8);
            }
            catch (Exception)
            {
                throw new HibasMegbizasKivetel();
            }
            
        }

        public static string[] MegbizasBetoltese()
        {
            
            string[] megbizas = new string[2];
            string sor;
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    sor = beolvasottSorok[i];
                    sor = sor.Split(':')[1].Trim();

                    if (sor == null || sor.Length == 0)
                    {
                        throw new HibasMegbizasKivetel();
                    }

                    megbizas[i] = sor;
                }
                catch (Exception)
                {
                    throw new HibasMegbizasKivetel();
                }
            }

            return megbizas;
        }

        public static BeosztottLista BeosztottakBetoltese()
        {
            BeosztottLista beosztottLista = new BeosztottLista();
            
            for (int i = 0; i < beolvasottSorok.Length; i++)
            {
                
                if (beolvasottSorok[i].Trim() == "Beosztottak:")
                {
                    string sor;
                    string[] adatok = new string[3];
                    for (int j = i + 1; j < beolvasottSorok.Length && beolvasottSorok[j].Trim().Length != 0; j++)
                    {
                        try
                        {
                            sor = beolvasottSorok[j];
                            adatok = sor.Split(';');
                            beosztottLista.Hozzaadas(new Beosztott(adatok[0], int.Parse(adatok[1]), int.Parse(adatok[2])));

                        }
                        catch (Exception)
                        {
                            throw new HibasMegbizasKivetel();
                        }
                    }

                    break;

                }
            }

            return beosztottLista;
        }

        public static Feladat[] FeladatokBetoltese()
        {
            int feladatSzamlalo = 0;
            Feladat[] feladatok = new Feladat[beolvasottSorok.Length];
            
            for (int i = 0; i < beolvasottSorok.Length; i++)
            {
                
                if (beolvasottSorok[i].Trim() == "Feladatok:")
                {
                    string sor;
                    string[] adatok = new string[4];
                    for (int j = i + 1; j < beolvasottSorok.Length && beolvasottSorok[j].Trim().Length != 0; j++)
                    {
                        try
                        {
                            sor = beolvasottSorok[j];
                            adatok = sor.Split(';');
                            if (adatok[0] == "BackendFeladat")
                            {
                                feladatok[feladatSzamlalo] = new BackendFeladat(adatok[1], DateTime.Parse(adatok[2]), int.Parse(adatok[3]));
                                feladatSzamlalo++;
                            }
                            else if (adatok[0] == "FrontendFeladat")
                            {
                                feladatok[feladatSzamlalo] = new FrontendFeladat(adatok[1], DateTime.Parse(adatok[2]), int.Parse(adatok[3]));
                                feladatSzamlalo++;
                            }
                            else
                            {
                                feladatok[feladatSzamlalo] = new FrontendFeladat(adatok[1], DateTime.Parse(adatok[2]), int.Parse(adatok[3]));
                                feladatSzamlalo++;
                            }

                        }
                        catch (Exception)
                        {
                            throw new HibasMegbizasKivetel();
                        }
                    }

                    break;

                }
            }

            Feladat[] ujTomb = new Feladat[feladatSzamlalo];

            for (int i = 0; i < feladatSzamlalo; i++)
            {
                ujTomb[i] = feladatok[i];
            }

            feladatok = ujTomb;

            return feladatok;

        }
    }
}
