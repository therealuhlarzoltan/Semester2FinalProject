using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal class BeosztottLista
    {
        private class ListaElem
        {
            public ListaElem Kovetkezo { get; set; }
            public Beosztott Beosztott { get; set; }

            public ListaElem(Beosztott beosztott) 
            {
                Beosztott = beosztott;
            }

        }

        private ListaElem fej;

        public void Hozzaadas(Beosztott beosztott)
        {

        }

        public void Eltavolitas(Beosztott beosztott)
        {

        }

        public Beosztott BeosztottIndex(int index)
        {
            return null;
        }


    }
}
