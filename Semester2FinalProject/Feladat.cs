using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    // Minden feladat ős osztálya, a közös metódusok és tulajdonságok itt találhatók
    internal abstract class Feladat : IFeladat
    {
        public string Megnevezes { get; }
        public DateTime KezdoDatum { get; }
        public int IdoIgeny { get; }

        public int HatralevoIdo { get; private set; }
        public bool Kesz { get; private set; }
        public Beosztott Beosztott { get; private set; }

        public Feladat(string megnevezes, DateTime kezdoDatum, int idoIgeny)
        {
            Megnevezes = megnevezes;
            KezdoDatum = kezdoDatum;
            IdoIgeny = idoIgeny;
            HatralevoIdo = idoIgeny;
            Kesz = false;
        }

        // A visszalépéses keresés beállításánál van szerepe
        public bool KepesElvegezni(Beosztott beosztott)
        {
            if (beosztott.MunkaOrak >= IdoIgeny)
            {
                return true;
            }

            return false;

        }

        // Beosztott hozzárendelése a feladathoz
        public void BeosztottHozzaadas(Beosztott beosztott)
        {
            Beosztott = beosztott;
            HatralevoIdo -= beosztott.MunkaOrak;
            if (HatralevoIdo <= 0)
            {
                Kesz = true;
            }
        }

        // Beosztott eltávolítása
        public void BesoztottEltavolitas()
        {
            HatralevoIdo += Beosztott.MunkaOrak;
            Beosztott = null;
            if (HatralevoIdo > 0)
            {
                Kesz = false;
            }
        }

        // Visszaadja hogy optimálisan sikerült-e megoldani a feladatot
        public bool IdoTullepes
        {
            get
            {
                return HatralevoIdo < 0;
            }
        }

        public int OsszesMunkaOra
        {
            get
            {
                return Beosztott.MunkaOrak;
            }
        }

        public int OsszesSzakertelem
        {
            get
            {
                return Beosztott.SzakmaiErtekeles;
            }
        }

        public override string ToString()
        {
            return $"Megnevezés: {Megnevezes}; Kezdő dátum: {KezdoDatum}; Idő igény: {IdoIgeny}";
        }


    }
}
