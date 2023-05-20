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
        protected int feladatokSzama;
        protected int[] jelentkezokSzama;
        protected Beosztott[,] jelentkezok;
        protected List<Beosztott[]> Mind;
        protected bool[,] kepesElvegezni;


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

        protected bool ft(int feladat, int beosztottIndexe)
        {
            return kepesElvegezni[feladat, beosztottIndexe];
        }


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


        protected void Kereses()
        {
            bool van = false;
            Mind = new List<Beosztott[]>();
            Beosztott[] Eredmeny = new Beosztott[feladatokSzama];
            Probal(0, ref van, ref Eredmeny);

            if (Mind.Count == 0)
            {
                throw new MegbizasNemTeljesithetoKivetel();
            }
            
        }

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

                        if (feladat == feladatokSzama - 1)
                        {
                            van = true;
                            Mind.Add((Beosztott[])Eredmeny.Clone());
                        }
                        else
                        {
                            Probal(feladat + 1, ref van, ref Eredmeny);
                        }
                        Eredmeny[feladat] = null;
                    }

                }
                i++;
            }
        }


        protected List<Beosztott[]> OptimalisMegoldasokKeresese(IFeladat[] feladatok)
        {
            List<Beosztott[]> eredmeny = new List<Beosztott[]>();
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
