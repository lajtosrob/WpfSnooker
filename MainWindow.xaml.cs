using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Wpf_Console
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Versenyzo> versenyzokListaja;
        public MainWindow()
        {
            InitializeComponent();
            versenyzokListaja = LoadFromCSV(".\\DataSource\\snooker.txt");
            dgTablazat.ItemsSource = versenyzokListaja;
        }

        private List<Versenyzo>? LoadFromCSV(string fileName)
        {
            var newList = new List<Versenyzo>();
            foreach (var CSV_line in File.ReadAllLines(fileName).Skip(1))
            {
                newList.Add(new Versenyzo(CSV_line));
            }
            return newList;
        }

        private void btnF3_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show($"3. feladat: A világranglistán {versenyzokListaja.Count} versenyző szerepel");
        }

        private void btnF4_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"4. feladat: A versenyzők átlagosan {versenyzokListaja.Average(x => x.Nyeremeny):f2} fontot kerestek");
        }

        private void btnF5_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"5. feladat: A legjobban kereső {txtOrszag.Text} versenyző adatait láthatja!");

            Versenyzo? keresettVersenyzo = versenyzokListaja.Where(x => x.Orszag == txtOrszag.Text).MaxBy(x => x.Nyeremeny);
            //Versenyzo? keresettVersenyzo = versenyzokListaja.Where(x => x.Orszag == txtOrszag.Text).OrderBy(x => x.Nyeremeny).Last();
            lblHelyezes.Content = keresettVersenyzo.Helyezes;
            lblNev.Content = keresettVersenyzo.Nev;
            lblOrszag.Content = keresettVersenyzo.Orszag;
            lblNyeremeny.Content = $"{keresettVersenyzo.Nyeremeny * int.Parse(txtArfolyam.Text):n3} Ft";
        }

        private void btnF6_Click(object sender, RoutedEventArgs e)
        {
            string seged = versenyzokListaja.Any(x => x.Orszag == "Norvégia2") ? "van" : "nincs";
            MessageBox.Show($"6. feladat: A versenyzők között {seged} norvég versenyző");

        }

        private void btnF7_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"7. feladat: Statisztika:");
            lbStatisztika.Items.Clear();


            foreach (var csoport in versenyzokListaja.GroupBy(x => x.Orszag))
            {
                if (csoport.Count() > int.Parse(txtFeladat7.Text))
                {
                    lbStatisztika.Items.Add($"{csoport.Key} - {csoport.Count()} fő");
                }
            }


        }
    }
}
