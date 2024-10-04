using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SZGYA13C_SoftDrinks
{
    internal class SoftDrinks
    {
        public string Nev { get; set; }
        public string EdesitoAnyag { get; set; }
        public int Ar { get; set; }
        public string Csomagolas { get; set; }
        public int GyumolcsTartalom { get; set; } //%
        public int Darab { get; set; }

        public SoftDrinks (string nev, string edesitoAnyag, int ar, string csomagolas, int gyumolcsTartalom, int darab)
        {
            Nev = nev;
            EdesitoAnyag = edesitoAnyag;
            Ar = ar;
            Csomagolas = csomagolas;
            GyumolcsTartalom = gyumolcsTartalom;
            Darab = darab;
        }

        public static List<SoftDrinks> FromFile(string path)
        {
            List<SoftDrinks> softDrinks = new List<SoftDrinks>();

            string[] line = File.ReadAllLines(path);

            foreach (var l in line.Skip(1))
            {
                string[] s = l.Split(';');

                string Nev = s[0];
                string EdesitoAnyag = s[1];
                int Ar = int.Parse(s[2]);
                string Csomagolas = s[3];
                int GyumolcsTartalom = int.Parse(s[4]);
                int Darab = int.Parse(s[5]);

                SoftDrinks sofDrink = new (Nev, EdesitoAnyag, Ar, Csomagolas, GyumolcsTartalom, Darab);

                softDrinks.Add(sofDrink);
            }
            return softDrinks;
        }

        public override string ToString()
        {
            return $"{Nev}, {EdesitoAnyag}, {Ar}, {Csomagolas}, {GyumolcsTartalom}, {Darab}";
        }
    }
}
