﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal class Beosztott
    {
        public string Nev { get; }
        public int MunkaOrak { get; }
        public int SzakmaiErtekeles { get; }

        public Beosztott(string nev, int munkaOrak, int szakmaiErtekeles)
        {
            Nev = nev;
            MunkaOrak = munkaOrak;
            SzakmaiErtekeles = szakmaiErtekeles;
        }

        public override string ToString()
        {
            return $"Név: {Nev}, Munkaórák: {MunkaOrak}, Szakmai értékelés: {SzakmaiErtekeles}";
        }
    }
}
