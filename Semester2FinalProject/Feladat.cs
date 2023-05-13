using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal abstract class Feladat : IFeladat
    {
        public string Megnevezes { get; }
        public DateTime KezdoDatum { get; }
        public int IdoIgeny { get; }

        public BeosztottLista Beosztottak { get; }

        public Feladat(string megnevezes, DateTime kezdoDatum, int idoIgeny)
        {
            Megnevezes = megnevezes;
            KezdoDatum = kezdoDatum;
            IdoIgeny = idoIgeny;
            Beosztottak = new BeosztottLista();
        }

        public bool KepesElvegezni(Beosztott beosztott)
        {
            if (beosztott.MunkaOrak >= IdoIgeny)
            {
                return true;
            }

            return false;

        }

        public void BesoztottHozzaadas(Beosztott beosztott)
        {
            Beosztottak.Hozzaadas(beosztott);
        }

        public void BesoztottEltavolitas(Beosztott beosztott)
        {
            int torlesIndex = Beosztottak.BeosztottKereso(ref beosztott);
            Beosztottak.Eltavolitas(torlesIndex);
        }

        public bool IdoTullepes()
        {
            int munkaOrak = 0;
            int beosztottak = Beosztottak.Hossz();

            for (int i = 0; i < beosztottak; i++)
            {
                munkaOrak += Beosztottak.BeosztottIndex(i).MunkaOrak;
            }

            if (munkaOrak > IdoIgeny)
            {
                return true;
            }

            return false;

        }

        public int OsszesMunkaOra()
        {
            int munkaOrak = 0;
            int beosztottak = Beosztottak.Hossz();

            for (int i = 0; i < beosztottak; i++)
            {
                munkaOrak += Beosztottak.BeosztottIndex(i).MunkaOrak;
            }

            return munkaOrak;
        }



    }
}
