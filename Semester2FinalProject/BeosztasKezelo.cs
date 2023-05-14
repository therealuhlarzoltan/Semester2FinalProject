using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal class BeosztasKezelo
    {
        private Megbizas aktualisMegbizas;
        private BeosztottLista beosztottak;
        public event IdoIgenyTullepesKezelo IdoIgenyTullepes;

        private class Beosztas
        {
            public Hashtable beosztas;

            public Beosztas()
            {
                beosztas = new Hashtable();
            }

            public void Beoszt(Beosztott beosztott, Feladat feladat)
            {
                if (!beosztas.ContainsKey(beosztott))
                {
                    beosztas[beosztott] = feladat;
                }

                beosztas[beosztott] = feladat;
            }

            public void Kioszt(Beosztott beosztott, Feladat feladat)
            {
                if (beosztas.ContainsKey(beosztott))
                {
                    beosztas.Remove(beosztott);
                }
            }

            public bool TartalmazBeosztott(Beosztott beosztott)
            {
                return beosztas.ContainsKey((beosztott));
            }

            public Beosztas Export()
            {
                Beosztas objektum = new Beosztas();
                foreach (var kulcs in beosztas.Keys)
                {
                    Beosztott beosztott = kulcs as Beosztott;
                    Feladat feladat = beosztas[kulcs] as Feladat;
                    objektum.beosztas[beosztott] = feladat;
                }

                return objektum;
            }
        }

        public BeosztasKezelo(Megbizas aktualisMegbizas, BeosztottLista beosztottak)
        {
            this.aktualisMegbizas = aktualisMegbizas;
            this.beosztottak = beosztottak;
        }

        public Feladat[] BeosztasKeszites()
        {
            if (aktualisMegbizas.Feladatok.Length > beosztottak.Hossz())
            {
                throw new MegbizasNemTeljesithetoKivetel();
            }

            List<Beosztas> beosztasok = new List<Beosztas>();
            List<Feladat> feladatok = Array.ConvertAll(aktualisMegbizas.Feladatok, feladat => (feladat as Feladat)).ToList();
            feladatok.Sort((elso, masodik) => masodik.IdoIgeny.CompareTo(elso.IdoIgeny));

            BeosztasKeszitesPrivat(feladatok, beosztottak, new Beosztas(), ref beosztasok);

            int minimalisMunkaOrak = int.MaxValue;
            List<int> minimalisMunkaOrakIndexek = new List<int>();

            foreach (Beosztas beosztas in beosztasok)
            {
                List<Beosztott> beosztottak = new List<Beosztott>();
                foreach (var kulcs in beosztas.beosztas.Keys)
                {
                    beosztottak.Add(kulcs as Beosztott);
                }
                int orak = 0;
                foreach (Beosztott beosztott in beosztottak)
                {
                    orak += beosztott.MunkaOrak;
                }
                if (orak <= minimalisMunkaOrak)
                {
                    minimalisMunkaOrak = orak;
                    minimalisMunkaOrakIndexek.Add(beosztasok.IndexOf(beosztas));
                }

            }

            List<Beosztas> szurtBeosztasok = new List<Beosztas>();
            foreach (int index in minimalisMunkaOrakIndexek)
            {
                szurtBeosztasok.Add(beosztasok[index]);
            }

            int szakmaiErtekeles = int.MinValue;
            int szakmaiErtekelesIndex = 0;

            foreach (Beosztas beosztas in szurtBeosztasok)
            {
                List<Beosztott> beosztottak = new List<Beosztott>();
                foreach (var kulcs in beosztas.beosztas.Keys)
                {
                    beosztottak.Add(kulcs as Beosztott);
                }
                int ertekeles = 0;
                foreach (Beosztott beosztott in beosztottak)
                {
                    ertekeles += beosztott.SzakmaiErtekeles;
                }
                if (ertekeles > szakmaiErtekeles)
                {
                    szakmaiErtekeles = ertekeles;
                    szakmaiErtekelesIndex = szurtBeosztasok.IndexOf(beosztas);
                }
            }

            Hashtable kivalasztottBeosztas = szurtBeosztasok[szakmaiErtekelesIndex].beosztas;
            List<Beosztott> kulcsok = kivalasztottBeosztas.Keys.Cast<Beosztott>().ToList();
            for (int i = 0; i < kivalasztottBeosztas.Count; i++)
            {
                List<Feladat> beosztasFeladatok = new List<Feladat>();
                beosztasFeladatok = beosztasFeladatok.Add(kivalasztottBeosztas[kulcsok[i]]);
                int hossz = 0;
                if (beosztasFeladatok != null )
                {
                    hossz = beosztasFeladatok.Count;
                }
                for (int j = 0; j < hossz; j++)
                {
                    (aktualisMegbizas.Feladatok[FeladatIndexe(beosztasFeladatok[j])] as Feladat).BeosztottHozzaadas(kulcsok[i]);
                }
            }




            return Array.ConvertAll(aktualisMegbizas.Feladatok, feladat => (feladat as Feladat));

        }

        private int FeladatIndexe(Feladat feladat)
        {
            int index = 0;
            foreach (var aktualisFeladat in aktualisMegbizas.Feladatok)
            {
                if ((aktualisFeladat as Feladat).Megnevezes == feladat.Megnevezes)
                {
                    break;
                }
                index++;
            }

            return index;
        }

        private void BeosztasKeszitesPrivat(List<Feladat> feladatok, BeosztottLista beosztottak, Beosztas beosztas, ref List<Beosztas> eredmeny)
        {
            if (feladatok.Count == 0)
            {
                eredmeny.Add(beosztas.Export());
                return;
            }

            Feladat aktualisFeladat = feladatok[0];
            feladatok.RemoveAt(0);

            for (int i = 0; i < beosztottak.Hossz(); i++)
            {
                Beosztott beosztott = beosztottak.BeosztottIndex(i);

                // Még nem dolgozik más feladaton
                if (!beosztas.TartalmazBeosztott(beosztott))
                {
                    // El tudja végezni
                    if (aktualisFeladat.KepesElvegezni(beosztott))
                    {
                        // Hozzárendeljük a feladatot a beosztot
                        beosztas.Beoszt(beosztott, aktualisFeladat);

                        // Csökkentjük a beosztott munkaidejét a feladat időigényével
                        //int orak = beosztott.MunkaOrak;
                        beosztott.MunkaOrak -= aktualisFeladat.IdoIgeny;

                        // Rekurzívan folytatjuk a beosztás készítést a következő feladatokkal
                        BeosztasKeszitesPrivat(feladatok, beosztottak, beosztas, ref eredmeny);

                        // Visszalépés: eltávolítjuk a feladatot a beosztástól
                        beosztas.Kioszt(beosztott, aktualisFeladat);

                        // Visszaállítjuk a beosztott munkaidejét
                        beosztott.MunkaOrak += aktualisFeladat.IdoIgeny;
                    }
                }
            }

            // Visszatesszük a kiválasztott feladatot a listába
            feladatok.Insert(0, aktualisFeladat);

        }
    }
}
