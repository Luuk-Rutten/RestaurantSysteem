using RestaurantSysteem.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RestaurantSysteem
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    
    // keuken erft van window
    public partial class Keuken : Window
    {
        private readonly Tafel _tafel;

        public Keuken(Tafel tafel)
        {
            _tafel = tafel;
            InitializeComponent();
            InitGerechten();
            InitTaal();
            InitWijn();
            InitPersonen();
            InitAllergie();
            TafelWindow(tafel);
        }

        private void TafelWindow(Tafel tafel)
        {
            try
            {
                

                TbTafelnr.Text = "Tafel Nr. " + tafel.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // allergie invoeren
        private void InitAllergie()
        {

            TbAllergieen.Text = $"Allergieen: {_tafel.Allergenen}";

        }

        //aantal personen weergeven
        private void InitPersonen()
        {

            TbAantalPers.Text = $"Aantal personen: {_tafel.AantalPersonen}";

        }
        // selecteer wel of geen winepairing
        private void InitWijn()
        {
            if (_tafel.WinePairing)
            {
                TbWijn.Text = "Winepairing";
            }
            else
            {
                TbWijn.Text = "Geen winepairing";
            }
        }

        //selecteer taal
        private void InitTaal()
        {
            TbTaal.Text = _tafel.Voertaal;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var bestellingBtn = sender as Button;


            if (bestellingBtn.Background == Brushes.Blue)
            {
                bestellingBtn.Background = Brushes.Orange;
                bestellingBtn.Foreground = Brushes.White;
            }
            else if (bestellingBtn.Background == Brushes.Orange)
            {
                bestellingBtn.Background = Brushes.Green;
            }
            else
            {
                bestellingBtn.Background = Brushes.Blue;
            }

        }

        private void InitGerechten()
        {
            foreach (var menu in _tafel.GekozenMenus)
            {
                var bestellingBtn = CreateGerechtButton(menu.Key.Naam, menu.Value);
                MenusStackPanel.Children.Add(bestellingBtn);
            };

            var voorgerechten = _tafel.GekozenMenus
                .SelectMany(x => new[] { x.Key.Voorgerecht }
                    .Select(gerecht => new { gerecht, aantal = x.Value }))
                .GroupBy(x => x.gerecht)
                .Select(x => new { Gerecht = x.Key, Aantal = x.Sum(y => y.aantal) });

            foreach (var voorgerecht in voorgerechten)
            {
                var voorgerechtBtn = CreateGerechtButton(voorgerecht.Gerecht, voorgerecht.Aantal);
                VoorgerechtStackPanel.Children.Add(voorgerechtBtn);
            }

            var tussengerechten = _tafel.GekozenMenus
                .SelectMany(x => new[] { x.Key.Tussengerecht }
                    .Select(gerecht => new { gerecht, aantal = x.Value }))
                .GroupBy(x => x.gerecht)
                .Select(x => new { Gerecht = x.Key, Aantal = x.Sum(y => y.aantal) });

            foreach (var tussengerecht in tussengerechten)
            {
                var tussengerechtBtn = CreateGerechtButton(tussengerecht.Gerecht, tussengerecht.Aantal);
                TussengerechtStackPanel.Children.Add(tussengerechtBtn);
            }

            var hoofdgerechten = _tafel.GekozenMenus
                .SelectMany(x => new[] { x.Key.Hoofdgerecht }
                    .Select(gerecht => new { gerecht, aantal = x.Value }))
                .GroupBy(x => x.gerecht)
                .Select(x => new { Gerecht = x.Key, Aantal = x.Sum(y => y.aantal) });

            foreach (var hoofdgerecht in hoofdgerechten)
            {
                var hoofdgerechtBtn = CreateGerechtButton(hoofdgerecht.Gerecht, hoofdgerecht.Aantal);
                HoofdgerechtStackPanel.Children.Add(hoofdgerechtBtn);
            }

            var nagerechten = _tafel.GekozenMenus
                .SelectMany(x => new[] { x.Key.Nagerecht }
                    .Select(gerecht => new { gerecht, aantal = x.Value }))
                .GroupBy(x => x.gerecht)
                .Select(x => new { Gerecht = x.Key, Aantal = x.Sum(y => y.aantal) });

            foreach (var nagerecht in nagerechten)
            {
                var nagerechtBtn = CreateGerechtButton(nagerecht.Gerecht, nagerecht.Aantal);
                NagerechtStackPanel.Children.Add(nagerechtBtn);
            }
        }

        private Button CreateGerechtButton(string naam, int aantal)
        {
            var button = new Button();

            button.Content = $"{naam} ({aantal}x)";
            button.Background = Brushes.Blue;
            button.Foreground = Brushes.White;
            button.Margin = new Thickness(5);
            button.Click += Button_Click;

            return button;
        }
    }
}




