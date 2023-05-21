using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    // A feladat által meghatározott Beosztott objektumokat tárolni képes láncolt lista
    internal class BeosztottLista
    {
        // A láncolt lista elemei bennük a következő elem mutatójával és a Beosztott objektummal
        private class ListaElem
        {
            public ListaElem Kovetkezo { get; set; }
            public Beosztott Beosztott { get; set; }

            public ListaElem(Beosztott beosztott) 
            {
                Beosztott = beosztott;
            }

            public ListaElem(Beosztott beosztott, ListaElem kovetkezo)
            {
                Kovetkezo = kovetkezo;
                Beosztott = beosztott;
            }

        }

        private ListaElem fej;

        // Beszúrás munka órák szerinti sorrendben
        public void Hozzaadas(Beosztott beosztott)
        {
            if (fej == null)
            {
                fej = new ListaElem(beosztott);
            }
            else
            {
                if (fej.Beosztott.MunkaOrak >= beosztott.MunkaOrak)
                {
                    ListaElem ujElem = new ListaElem(beosztott, fej);
                    fej = ujElem;
                }
                else
                {
                    ListaElem elozoElem = null;
                    ListaElem aktualisElem = fej;

                    while (aktualisElem != null && aktualisElem.Beosztott.MunkaOrak < beosztott.MunkaOrak)
                    {
                        elozoElem = aktualisElem;
                        aktualisElem = aktualisElem.Kovetkezo;
                    }
                    if (aktualisElem == null)
                    {
                        ListaElem ujElem = new ListaElem(beosztott);
                        elozoElem.Kovetkezo = ujElem;
                    }
                    else
                    {
                        ListaElem ujElem = new ListaElem(beosztott, aktualisElem);
                        elozoElem.Kovetkezo = ujElem;
                    }
                }
            }

        }

        // Beosztott objektum indexének kereséses a listában
        public int BeosztottKereso(ref Beosztott beosztott)
        {
            int szamlalo = 0;
            ListaElem aktualisElem = fej;

            while (aktualisElem != null && aktualisElem.Beosztott != beosztott)
            {
                aktualisElem = aktualisElem.Kovetkezo;
                szamlalo++;
            }

            if (this.BeosztottIndex(szamlalo) == beosztott)
            {
                return szamlalo;
            }

            return -1;
            


        }

        // Adott indexű objektum elvtávolítása a listából
        public void Eltavolitas(int index)
        {
            int szamlalo = 0;
            ListaElem aktualisElem = fej;
            ListaElem elozoElem = null;

            while (aktualisElem != null && szamlalo != index)
            {
                elozoElem = aktualisElem;
                aktualisElem = aktualisElem.Kovetkezo;
                szamlalo++;
            }

            if (elozoElem ==  null)
            {
                fej = aktualisElem.Kovetkezo;
            }
            else
            {
                if (aktualisElem.Kovetkezo == null)
                {
                    elozoElem.Kovetkezo = null;
                }
                else
                {
                    elozoElem.Kovetkezo = aktualisElem.Kovetkezo;
                }
            }

            

        }

        // Adott indexű Beosztott objektum visszaadása a listából
        public Beosztott BeosztottIndex(int index)
        {
            int szamlalo = 0;
            ListaElem aktualisElem = fej;

            while (aktualisElem != null && szamlalo != index)
            {
                aktualisElem = aktualisElem.Kovetkezo;
                szamlalo++;
            }

            return aktualisElem.Beosztott;
        }

        // A lista hosszát megadó csak olvasható tulajdonság
        public int Hossz
        {
            get
            {
                int hossz = 0;

                ListaElem aktualisElem = fej;

                while (aktualisElem != null)
                {
                    hossz++;
                    aktualisElem = aktualisElem.Kovetkezo;
                }


                return hossz;
            }
        }


    }
}
