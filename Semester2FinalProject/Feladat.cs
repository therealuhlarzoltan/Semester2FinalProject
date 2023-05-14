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
        public bool Kesz { get; private set; }

        public int HatralevoIdo { get; private set; }

        public BeosztottLista Beosztottak { get; }

        public Feladat(string megnevezes, DateTime kezdoDatum, int idoIgeny)
        {
            Megnevezes = megnevezes;
            KezdoDatum = kezdoDatum;
            IdoIgeny = idoIgeny;
            Beosztottak = new BeosztottLista();
            HatralevoIdo = idoIgeny;
            Kesz = false;
        }

        public bool KepesElvegezni(Beosztott beosztott)
        {
            if (beosztott.MunkaOrak >= HatralevoIdo)
            {
                return true;
            }

            return false;

        }


        public void BeosztottHozzaadas(Beosztott beosztott)
        {
            Beosztottak.Hozzaadas(beosztott);
            HatralevoIdo -= beosztott.MunkaOrak;
            if (HatralevoIdo <= 0)
            {
                Kesz = true;
            }
        }

        public void BesoztottEltavolitas(Beosztott beosztott)
        {
            int torlesIndex = Beosztottak.BeosztottKereso(ref beosztott);
            Beosztottak.Eltavolitas(torlesIndex);
            HatralevoIdo += beosztott.MunkaOrak;
            if (HatralevoIdo > 0)
            {
                Kesz = false;
            }
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

        public int OsszesSzakertelem()
        {
            int szakertelem = 0;
            int beosztottak = Beosztottak.Hossz();

            for (int i = 0; i < beosztottak; i++)
            {
                szakertelem += Beosztottak.BeosztottIndex(i).SzakmaiErtekeles;
            }

            return szakertelem;
        }



    }
}
