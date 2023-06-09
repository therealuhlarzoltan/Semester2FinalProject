﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    // A feladat által meghatározott BesoztásKezelő osztály, a megbízást és a rendelkezésre álló besoztottakat átadva képes elkészíteni a beosztást
    // A visszalépéses keresés algoritmusát a VisszalepesesKereses osztályból örökli
    internal class BeosztasKezelo : VisszalepesesKereses
    {
        private Megbizas aktualisMegbizas;
        private BeosztottLista beosztottak;
        public event IdoIgenyTullepesKezelo IdoIgenyTullepes;

        public BeosztasKezelo(Megbizas aktualisMegbizas, BeosztottLista beosztottak)
        {
            // Saját adattagok inicializálása
            this.aktualisMegbizas = aktualisMegbizas;
            this.beosztottak = beosztottak;

            // A visszalépéses keresés "beállításai"
            feladatokSzama = aktualisMegbizas.Feladatok.Length;
            jelentkezokSzama = new int[feladatokSzama];
            for (int i = 0; i < feladatokSzama; i++)
            {
                jelentkezokSzama[i] = beosztottak.Hossz;
            }
            jelentkezok = new Beosztott[feladatokSzama, beosztottak.Hossz];
            for (int i = 0; i < jelentkezok.GetLength(0); i++)
            {
                for (int j = 0; j < jelentkezok.GetLength(1); j++)
                {
                    jelentkezok[i, j] = beosztottak.BeosztottIndex(j);
                }
            }
        }

        // A kívülről látható metódus ami elindítja a beosztás készítést
        public Feladat[] BeosztasKeszites()
        {

            // Nincs elég beosztott
            if (aktualisMegbizas.Feladatok.Length > beosztottak.Hossz)
            {
                throw new MegbizasNemTeljesithetoKivetel();
            }

            // A feladatok másolása --> az eredeti feladat objektumok érintetlenül maradnak
            Feladat[] feladatokMasolata = new Feladat[aktualisMegbizas.Feladatok.Length];
            for (int i = 0; i < feladatokMasolata.Length; i++)
            {
                IFeladat feladat = aktualisMegbizas.Feladatok[i];
                if (feladat is BackendFeladat)
                {
                    feladatokMasolata[i] = (feladat as BackendFeladat).Clone() as Feladat;
                }
                else if (feladat is FrontendFeladat)
                {
                    feladatokMasolata[i] = (feladat as FrontendFeladat).Clone() as Feladat;
                }
            }

            // Visszalépéses keresés
            KepesElvegezniMatrixFeltoltes(feladatokMasolata, beosztottak);
            Kereses();

            // Időigény szempontjából optimális megoldások kiválogatása
            List<Beosztott[]> optimalisMegoldasok = OptimalisMegoldasokKeresese(aktualisMegbizas.Feladatok);

            // Csak egy optimális megoldás van
            if (optimalisMegoldasok.Count == 1)
            {
                // Beosztottak hozzárendekése a feladatokhoz
                for (int i = 0; i < aktualisMegbizas.Feladatok.Length; i++)
                {
                    feladatokMasolata[i].BeosztottHozzaadas(optimalisMegoldasok.First()[i]);
                    // Ha idő túllépés történt --> esemény kiváltása
                    if (feladatokMasolata[i].IdoTullepes)
                    {
                        IdoIgenyTullepes(feladatokMasolata[i]);
                    }
                }
            }
            // Több optimális megoldás is van
            else
            {
                // Beosztottak hozzárendekése a feladatokhoz
                Beosztott[] megoldas = LegnagyobbSzakmaiErtekelesKeresese(optimalisMegoldasok);
                for (int i = 0; i < aktualisMegbizas.Feladatok.Length; i++)
                {
                    feladatokMasolata[i].BeosztottHozzaadas(optimalisMegoldasok.First()[i]);
                    // Ha idő túllépés történt --> esemény kiváltása
                    if (feladatokMasolata[i].IdoTullepes)
                    {
                        IdoIgenyTullepes(feladatokMasolata[i]);
                    }
                }
            }

            return feladatokMasolata;

        }

    }
}
