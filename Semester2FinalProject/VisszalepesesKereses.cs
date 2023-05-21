using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    // A visszalépéses keresést megvalósító absztakt osztály
    // A visszalépéses keresés azon változatát valósítja meg ami az összes lehetséges megoldást megadja
    // Arra szolgál hogy a BeosztasKezelo osztály fel tudja használni a metódusait a beosztás elkészítésénél
    internal abstract class VisszalepesesKereses
    {
        // A visszalépéses keresés beállításai
        protected int feladatokSzama;
        protected int[] jelentkezokSzama;
        // A visszalépéses keresés értékkészlete
        protected Beosztott[,] jelentkezok;
        // Az összes lehetésges beosztást tároló lista
        protected List<Beosztott[]> Mind;
        // Azt mutatja meg hogy az i-edik feladatot képes-e elvégezni a j-edik beosztott
        protected bool[,] kepesElvegezni;

        // Meghatározza hogy melyik beosztott milyen feladatokat képes elvégezni
        protected void KepesElvegezniMatrixFeltoltes(Feladat[] feladatok, BeosztottLista beosztottak)
        {
            kepesElvegezni = new bool[feladatok.Length, beosztottak.Hossz];
            for (int i = 0; i < kepesElvegezni.GetLength(0); i++)
            {
                for (int j = 0; j < kepesElvegezni.GetLength(1); j++)
                {
                    kepesElvegezni[i, j] = feladatok[i].KepesElvegezni(beosztottak.BeosztottIndex(j));
                }
            }
        }

        // Az első szűrő feltétel, ha az aktuális feladatot nem tudja elvégezni a beosztott biztos nem kerülhet az adott heylre a részmegoldásban
        protected bool ft(int feladat, int beosztottIndexe)
        {
            return kepesElvegezni[feladat, beosztottIndexe];
        }

        // A második szűrőfeltétel, egy besoztott sem dolgozhat kétszer
        //
        protected bool fk(int feladat, ref Beosztott beosztott, Beosztott[] Eredmeny)
        {
            int i = 0;
            while (i <= feladat)
            {
                if (Eredmeny[i] == beosztott)
                {
                    return false;
                }
                i++;
            }

            return true;

        }

        // A visszalépéses keresést elindító metódus
        protected void Kereses()
        {
            bool van = false;
            Mind = new List<Beosztott[]>();
            // Az aktuális eredményt (kombinációt) tároló tömb
            Beosztott[] Eredmeny = new Beosztott[feladatokSzama];
            // A visszalépéses keresés elindítása
            Probal(0, ref van, ref Eredmeny);

            // Nincs olyan kombináció amelyben minden feladatot el tudna végezni valaki --> kivétel dobása
            if (Mind.Count == 0)
            {
                throw new MegbizasNemTeljesithetoKivetel();
            }
            
        }

        // A visszalépéses keresést megvalósító metódus
        protected void Probal(int feladat, ref bool van, ref Beosztott[] Eredmeny)
        {
            int i = 0;
            while (i < jelentkezokSzama[feladat])
            {
                if (ft(feladat, i))
                {
                    if (fk(feladat, ref jelentkezok[feladat, i], Eredmeny))
                    {
                        Eredmeny[feladat] = jelentkezok[feladat, i];

                        // Az összes feladatot kiosztottuk --> aktuális kombináció hozzáadása az eredményekhez
                        if (feladat == feladatokSzama - 1)
                        {
                            van = true;
                            // Az aktuális kombináció folyamatosan változik --> a megoldásokhoz az aktuális kombináció jelenlegi állapotáról készült másolatot adjuk
                            Mind.Add((Beosztott[])Eredmeny.Clone());
                        }
                        else
                        {
                            // Folytatjuk a beosztottak hozzárendelését a feladatokhoz
                            Probal(feladat + 1, ref van, ref Eredmeny);
                        }
                        // Új kombinációt készítünk
                        Eredmeny[feladat] = null;
                    }

                }
                i++;
            }
        }

        // Az optimális megoldások halmaza az összes megoldás részhalmaza --> kiválogatás
        protected List<Beosztott[]> OptimalisMegoldasokKeresese(IFeladat[] feladatok)
        {
            List<Beosztott[]> eredmeny = new List<Beosztott[]>();

            // Minimumkiválasztás minden egyes megoldásra
            int minimalisIdoTullepes = int.MaxValue;

            foreach (Beosztott[] megoldas in Mind)
            {
                int idoTullepes = 0;
                int index = 0;
                foreach (Beosztott beosztott in megoldas)
                {
                    idoTullepes += beosztott.MunkaOrak - feladatok[index].IdoIgeny;
                    index++;
                }
                if (idoTullepes < minimalisIdoTullepes)
                {
                    minimalisIdoTullepes = idoTullepes;
                }
            }

            // Ha a megoldás optimális hozzáadjuk az optimális megoldások listájához
            foreach (Beosztott[] megoldas in Mind)
            {
                int idoTullepes = 0;
                int index = 0;
                foreach (Beosztott beosztott in megoldas)
                {
                    idoTullepes += beosztott.MunkaOrak - feladatok[index].IdoIgeny;
                    index++;
                }
                if (idoTullepes == minimalisIdoTullepes)
                {
                    eredmeny.Add(megoldas);
                }
            }

            return eredmeny;
        }

        // Csak egy olyan lehetőségre vagyunk kíváncsiak ahol a beosztottak szakértelme a legnagyobb --> maximumkiválasztás minden megoldásra
        protected Beosztott[] LegnagyobbSzakmaiErtekelesKeresese(List<Beosztott[]> megoldasok)
        {
            int legnagyobbSzakmaiErtekeles = int.MinValue;
            int legnagyobbSzakmaiErtekelesIndexe = 0;

            int index = 0;
            foreach (Beosztott[] megoldas in megoldasok)
            {
                int szakmaiErtekeles = 0;
                
                foreach (Beosztott beosztott in megoldas)
                {
                    szakmaiErtekeles += beosztott.SzakmaiErtekeles;
                }
                if (szakmaiErtekeles > legnagyobbSzakmaiErtekeles)
                {
                    legnagyobbSzakmaiErtekeles = szakmaiErtekeles;
                    legnagyobbSzakmaiErtekelesIndexe = index;
                }
                index++;
            }

            return megoldasok[legnagyobbSzakmaiErtekelesIndexe];
        }

    }
}
