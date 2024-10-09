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

            //8.feladat

            var edesitoanagyok = softDrink.GroupBy(s => s.EdesitoAnyag).Select(s => $"{s.Key} - {s.Count()}");

            File.WriteAllLines(@"..\..\..\src\sweetening.txt", edesitoanagyok);
            
        }

        private void btnAjanlat_Click(object sender, RoutedEventArgs e)
        {
            //9.feladat

            string uditonev = tb1.Text;

            var uditok = softDrink.Where(s => s.Nev.Contains(uditonev)).ToList();

            if (string.IsNullOrEmpty(uditonev) || !uditok.Any())
            {
                MessageBox.Show($"Nincs ilyen üdítőnk!", "Sikeres", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var uditoList = uditok.Select(i => i.ToString()).ToArray();

                File.WriteAllLines(@"..\..\..\src\offer.txt", uditoList);

                //foreach (var i in uditoList)
                //{
                //    File.WriteAllText($@"..\..\..\src\offer_{i.Split(' ').First()}.txt", i);
                //}

                MessageBox.Show($"{uditok.Count}db üdítő van, aminek átlag ára: {Math.Round(uditok.Average(s => s.Ar)), 2}", "Sikeres", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            



        }

        private void ujFelvetelbtn_Click(object sender, RoutedEventArgs e)
        {
            //10.feladat 

            if (string.IsNullOrEmpty(ujTb1.Text))
            {
                MessageBox.Show("Nincs megadva az üdítő neve!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (string.IsNullOrEmpty(ujTb2.Text))
            {
                MessageBox.Show("Nincs megadva az üdítő édesítése!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (string.IsNullOrEmpty(ujTb3.Text))
            {
                MessageBox.Show("Nincs megadva az üdítő ára!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!int.TryParse(ujTb3.Text, out int resultAr))
            {
                MessageBox.Show("Ez nem Ár!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                ujTb3.Text = string.Empty;
            }
            else if (string.IsNullOrEmpty(ujTb4.Text))
            {
                MessageBox.Show("Nincs megadva az üdítő csomagolás típusa!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (string.IsNullOrEmpty(ujTb5.Text))
            {
                MessageBox.Show("Nincs megadva az üdítő gyümölcs tartalma!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!int.TryParse(ujTb5.Text, out int resultGyum))
            {
                MessageBox.Show("Nem jó a gyümölcs tartalom!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                ujTb5.Text = string.Empty;
            }
            else
            {
                string[] ujDrink = [$"{ujTb1.Text};{ujTb2.Text};{ujTb3.Text};{ujTb4.Text};{ujTb5.Text};12"];

                File.AppendAllLines(@"..\..\..\src\softDrinks.txt", ujDrink);

                MessageBox.Show("Sikeres hozzáadás!", "SIKER", MessageBoxButton.OK, MessageBoxImage.Information);
                ujTb1.Text = string.Empty;
                ujTb2.Text = string.Empty;
                ujTb3.Text = string.Empty;
                ujTb4.Text = string.Empty;
                ujTb5.Text = string.Empty;
            }


           

        }
    }
}