using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SZGYA13C_SoftDrinks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<SoftDrinks> softDrink = new List<SoftDrinks>();
        public MainWindow()
        {
            InitializeComponent();

            softDrink = SoftDrinks.FromFile(@"..\..\..\src\softDrinks.txt");

            //4.feladat

            Random rnd = new Random();

            var curkosItalok = softDrink.Where(s => s.EdesitoAnyag == "cukor");
            var randomCukrosItal = rnd.Next(curkosItalok.Count());

            tbl1.Text = $"{softDrink[randomCukrosItal].Nev}";

            //5.feladat

            var gyumolcsMentes = softDrink.Where(s => s.GyumolcsTartalom <= 0)
                                          .MinBy(s => s.Ar);

            tbl2.Text = $"{gyumolcsMentes.Nev} - {gyumolcsMentes.Ar} Ft/l";

            //6.feladat

            List<string> gyarto = new List<string>();

            string[] gyartok = softDrink.Select(s => s.Nev)
                                        .Distinct()
                                        .ToArray();            

            foreach (var g in gyartok)
            {
                string s = g.Split(' ')[0];

                gyarto.Add(s);
            }

            tbl3.Text = $"{gyarto.Distinct().Count()}";

            //7.feladat

            var uditok = softDrink.GroupBy(s => s.Nev)
                                  .ToList();


        }
    }
}