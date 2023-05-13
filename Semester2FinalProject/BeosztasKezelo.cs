using System;
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

        public BeosztasKezelo(Megbizas aktualisMegbizas, BeosztottLista beosztottak)
        {
            this.aktualisMegbizas = aktualisMegbizas;
            this.beosztottak = beosztottak;
        }

        public IFeladat[] BeosztasKeszites()
        {
            if (aktualisMegbizas.Feladatok.Length > beosztottak.Hossz())
            {
                throw new MegbizasNemTeljesithetoKivetel(aktualisMegbizas);
            }

            return null;
        }



    }
}
