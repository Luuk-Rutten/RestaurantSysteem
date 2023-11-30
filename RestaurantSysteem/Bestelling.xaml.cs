using RestaurantSysteem.DataAccess;
using RestaurantSysteem.DataAccess.Entities;
using RestaurantSysteem.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace RestaurantSysteem
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    
    //erft over van Window
    public partial class TafelWindow : Window
    {
        private Tafel _tafel;

        private IEnumerable<MenuEntity> _menus;
        private IEnumerable<MenuTafelEntity> _menuTafels;

        private readonly MenuRepository _menuRepository;
        private readonly TafelRepository _tafelRepository;
        
        //initialiseer het tafelwindow
        public TafelWindow(Tafel tafel)
        {
            this._tafel = tafel;

            InitializeComponent();
            InitAvailableLanguages();

            Titel_tafel.Content = "Tafel Nr. " + tafel.Id;

            _menuRepository = new MenuRepository();
            _tafelRepository = new TafelRepository();
            var Tafel = _tafelRepository.Get(tafel.Id);
            this.TbPersonen.Text = Tafel.AantalPersonen.ToString();
            this.TbAllergie.Text = Tafel.Allergenen.ToString();
            this.WinePairingComboBox.SelectedIndex = Convert.ToInt32(Tafel.WinePairing);
            if (Tafel.Voertaal == "Nederlands")
            {
                this.LanguageComboBox.SelectedIndex = 1;
            }
            else if (Tafel.Voertaal == "Engels")
            {
                this.LanguageComboBox.SelectedIndex = 0;
            }

            _menus = _menuRepository.GetAll();
            foreach ( var menu in _menus )
            {
                this.MenuComboBox.Items.Add(new ComboBoxItem()
                {
                    Content = menu.Naam
                });
            }

            _menuTafels = _tafelRepository.GetAll();
            foreach (var menuTafel in _menuTafels.Where(m => m.TafelId == tafel.Id))
            {
                var menu = _menus.FirstOrDefault(m => m.Id == menuTafel.MenuId);
                this.ListView.Items.Add(menuTafel.Aantal + " " + menu.Naam);
            }

            this.MenuComboBox.SelectedIndex = 0;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var tafelEntity = new TafelEntity();
            tafelEntity.Id = this._tafel.Id;
            tafelEntity.AantalPersonen = Convert.ToInt32(this.TbPersonen.Text);
            tafelEntity.Allergenen = this.TbAllergie.Text;
            tafelEntity.Voertaal = this.LanguageComboBox.Text;
            string winePairing = this.WinePairingComboBox.Text;
            tafelEntity.WinePairing = winePairing == "Winepairing";
            _tafelRepository.Update(tafelEntity);
            base.OnClosing(e);
        }

        private void InitAvailableLanguages()
        {
            foreach (var language in Enum.GetValues(typeof(AvailableLanguages)))
            {
                LanguageComboBox.Items.Add(new ComboBoxItem()
                {
                    Content = language
                });
            }
        }

        
        //menu aantal ophogen in listview
        private void Button_Ophogen_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedItem != null)
            {
                int index = ListView.SelectedIndex;
                string item = ListView.SelectedItem.ToString();
                var number = GetItemNumber(item);
                var itemName = GetItemName(item);
                number++;

                ListView.Items[index] = number + " " + itemName;
            }
        }
        // menu aantal verminderen in listview
        private void Button_Verminderen_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedItem != null)
            {
                int index = ListView.SelectedIndex;
                string item = ListView.SelectedItem.ToString();
                var number = GetItemNumber(item);
                var itemName = GetItemName(item);
                number--;

                ListView.Items[index] = number + " " + itemName;
            }
        }

        //gekozen menu verwijderen
        private void Button_Click_Verwijder(object sender, RoutedEventArgs e)

        {
            int counter = 0;
            if (ListView.SelectedItem!= null)
            {
                ListView.Items.Remove(ListView.SelectedItem);

                counter = 0;
            }
        }
        // terug buton naar mainwindow
        private void ButtonTerug(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        // verstuur order naar keukenwindow
        private void Button_Click_verstuurkeuken(object sender, RoutedEventArgs e)
        {

            var PersonenCasted = TbPersonen.Text;
            var AllergieCasted = TbAllergie.Text;
            var LangCasted = (ComboBoxItem)LanguageComboBox.SelectedItem;
            var WineCasted = (ComboBoxItem)WinePairingComboBox.SelectedItem;

            if (LangCasted == null | WineCasted == null)
            {
                MessageBox.Show("Vul de taal en winepairing in!");
                return;
            }

            _tafel.AantalPersonen = Convert.ToInt32(this.TbPersonen.Text);
            _tafel.Allergenen = this.TbAllergie.Text;
            _tafel.Voertaal = this.LanguageComboBox.Text;
            string winePairing = this.WinePairingComboBox.Text;
            _tafel.WinePairing = winePairing == "Winepairing";
            _tafel.GekozenMenus = new Dictionary<DataModel.Menu, int>();

            // Sla gekozen menu's op in de database:
            foreach (var item in ListView.Items.Cast<string>())
            {
                var number = GetItemNumber(item);
                var itemName = GetItemName(item);
                var menuEntity = _menus.First(m => m.Naam == itemName);
                var entity = new MenuTafelEntity();
                entity.TafelId = _tafel.Id;
                entity.MenuId = _menus.First(m => m.Naam == itemName).Id;
                entity.Aantal = number;
                _tafelRepository.InsertOrUpdateMenuTafel(entity);

                var menu = new DataModel.Menu();
                menu.Id = menuEntity.Id;
                menu.Naam = menuEntity.Naam;
                menu.Voorgerecht = menuEntity.Voorgerecht;
                menu.Tussengerecht = menuEntity.Tussengerecht;
                menu.Hoofdgerecht = menuEntity.Hoofdgerecht;
                menu.Nagerecht = menuEntity.Nagerecht;
                _tafel.GekozenMenus.Add(menu, number);
            }

            // Verwijder niet meer gekozen menu's:
            var deletedTafelMenus = new List<MenuTafelEntity>();
            foreach (var tafelMenu in _menuTafels)
            {
                bool exists = false;
                foreach (var item in ListView.Items.Cast<string>())
                {
                    var menuName = GetItemName(item);
                    var menuId = _menus.FirstOrDefault(m => m.Naam == menuName)?.Id;
                    if (menuId != null && tafelMenu.MenuId == menuId)
                    {
                        exists = true;
                    }
                }

                if (!exists)
                {
                    _tafelRepository.DeleteMenuTafel(tafelMenu.MenuId, tafelMenu.TafelId);
                }
            }

            if (LangCasted != null && LangCasted.Content != null & WineCasted != null || WineCasted.Content != null)
            {
                Keuken Keuken = new Keuken(_tafel);
                Keuken.Show();
            }
        }

        private int GetItemNumber(string listItem)
        {
            int.TryParse(listItem.Substring(0, listItem.IndexOf(" ")), out int number);
            return number;
        }

        private string GetItemName(string listItem)
        {
            return listItem.Substring(listItem.IndexOf(" ") + 1);
        }

        public enum AvailableLanguages
        {
            Engels = 0,
            Nederlands = 1
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxPersonen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TbAllergie_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WinePairingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
        //voeg menu van drowpdown toe aan listview
        private void AddMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = this.MenuComboBox.SelectedItem as ComboBoxItem;
            var menu = selectedItem.Content.ToString();

            int counter = 1;
            if (ListView.Items.Count == 0)
            {
                ListView.Items.Add(counter + " " + menu);
            }
            else
            {
                var existingItem = ListView.Items.Cast<string>().FirstOrDefault(t => t.Contains(menu));
                
                if (existingItem != null)
                {
                    var existingIndex = Convert.ToInt32(existingItem.Substring(0, 1));
                    ListView.Items[ListView.Items.IndexOf(existingItem)] = (existingIndex + 1) + " " + menu;
                }
                else
                {
                    ListView.Items.Add(counter + " " + menu);
                }
            }
        }
    }

    internal class KeyPressEventArgs
    {
    }
}

 
