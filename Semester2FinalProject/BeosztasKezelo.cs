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
        public event MegbizasNemTeljesithetoKezelo MegbizasNemTeljesitheto;
        public event HataridoTullepesKezelo HataridoTullepes;

        public BeosztasKezelo(Megbizas aktualisMegbizas, BeosztottLista beosztottak)
        {
            this.aktualisMegbizas = aktualisMegbizas;
            this.beosztottak = beosztottak;
        }


    }
}
