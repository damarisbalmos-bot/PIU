using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LibrarieModele;
using NivelStocareDate;
using System.Collections.ObjectModel;


namespace Baterii
{
    public partial class MainWindow : Window
    {
        private const int LUNGIME_MAX_NUME = 15;
        private const int CANTITATE_MIN = 1;

        static IStocareData admin = StocareFactory.GetAdministratorStocare();
        public ObservableCollection<Baterie> ColectieBaterii { get; set; } = new ObservableCollection<Baterie>();
        public MainWindow()
        {
            InitializeComponent();
            foreach (var bat in admin.GetBaterii())
            {
                ColectieBaterii.Add(bat);
            }
            this.DataContext = this;
        }
        private int ValideazaDateBaterie()
        {
            int codEroare = 0;
            ResetErori();
            if (string.IsNullOrWhiteSpace(txtNume.Text))
            {
                codEroare = 1;
                txtNume.Foreground = Brushes.Red;
                errNume.Text = "Numele este obligatoriu!";
            }
            else if (txtNume.Text.Length > LUNGIME_MAX_NUME)
            {
                codEroare = 2;
                txtNume.Foreground = Brushes.Red;
                errNume.Text = $"Max {LUNGIME_MAX_NUME} caractere!";
            }
            if (dpData.SelectedDate == null)
            {
                codEroare = 3;
                dpData.Foreground = Brushes.Red;
                errData.Text = "Data expirarii este obligatorie!";
            }
            if (string.IsNullOrWhiteSpace(txtCantitate.Text))
            {
                codEroare = 4;
                txtCantitate.Foreground = Brushes.Red;
                errCantitate.Text = "Cantitatea este obligatorie!";
            }
            else if (!int.TryParse(txtCantitate.Text, out int cant) || cant < CANTITATE_MIN)
            {
                codEroare = 5;
                txtCantitate.Foreground = Brushes.Red;
                errCantitate.Text = "Cantitate invalida!";
            }
            if (string.IsNullOrWhiteSpace(txtProducator.Text))
            {
                codEroare = 6;
                txtProducator.Foreground = Brushes.Red;
                errProducator.Text = "Producatorul este obligatoriu!";
            }
            return codEroare;
        }
        private void ResetErori()
        {
            txtNume.Foreground = Brushes.Black;
            dpData.Foreground = Brushes.Black;
            txtCantitate.Foreground = Brushes.Black;
            txtProducator.Foreground = Brushes.Black;
            errNume.Text = "";
            errData.Text = "";
            errCantitate.Text = "";
            errProducator.Text = "";
        }
        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            int eroare = ValideazaDateBaterie();
            if (eroare != 0)
            {
                txtStatus.Text = "Eroare la validare!";
                return;
            }
            TipBaterie tip = TipBaterie.Alcalina;
            if (rbLitiu.IsChecked == true) tip = TipBaterie.Litiu;
            else if (rbNichelMetal.IsChecked == true) tip = TipBaterie.NichelMetal;
            else if (rbPlumb.IsChecked == true) tip = TipBaterie.Plumb;
            OptiuniBaterie optiuni = OptiuniBaterie.Niciuna;
            if (chkReincarcabila.IsChecked == true) optiuni |= OptiuniBaterie.Reincarcabila;
            if (chkImpermeabila.IsChecked == true) optiuni |= OptiuniBaterie.Impermeabila;
            if (chkTempScazuta.IsChecked == true) optiuni |= OptiuniBaterie.TemperaturaScazuta;
            if (chkTempRidicata.IsChecked == true) optiuni |= OptiuniBaterie.TemperaturaRidicata;
            Producator prod = new Producator(txtProducator.Text, "", "");
            Baterie baterie = new Baterie(
                txtNume.Text,
                tip,
                dpData.SelectedDate.Value,
                int.Parse(txtCantitate.Text),
                prod,
                optiuni
            );
            ColectieBaterii.Add(baterie);
            txtStatus.Text = $"Bateria '{baterie.Nume}' a fost adaugata!";
            ExecutaReset();
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ExecutaReset();
        }
        private void ExecutaReset()
        { 
            txtNume.Text = "";
            txtCantitate.Text = "";
            txtProducator.Text = "";
            dpData.SelectedDate = null;
            rbAlcalina.IsChecked = true;
            chkReincarcabila.IsChecked = false;
            chkImpermeabila.IsChecked = false;
            chkTempScazuta.IsChecked = false;
            chkTempRidicata.IsChecked = false;
            ResetErori();
            txtStatus.Text = "Campurile au fost resetate.";
        }
        private void mnuAdauga_Click(object sender, RoutedEventArgs e)
        {
            AscundeToatePanourile(); 
            pnlAdauga.Visibility = Visibility.Visible;
            

        }
        private void mnuModifica_Click(object sender, RoutedEventArgs e)
        {
            AscundeToatePanourile(); 
            pnlModifica.Visibility = Visibility.Visible; 
            
            cmbBaterii.DisplayMemberPath = "Nume";
        }
        private void cmbBaterii_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbBaterii.SelectedItem is not Baterie b) return;
            pnlModifica.DataContext = b;
        }
        private void btnActualizeaza_Click(object sender, RoutedEventArgs e)
        {
            if (cmbBaterii.SelectedItem is not Baterie b)
            {
                txtStatus.Text = "Selecteaza o baterie!";
                return;
            }
            if (!int.TryParse(txtModCantitate.Text, out int cantitate))
            {
                txtModCantitate.Foreground = Brushes.Red;
                errModCantitate.Text = "Cantitate invalida!";
                return;
            }

            if (dpModData.SelectedDate == null)
            {
                dpModData.Foreground = Brushes.Red;
                errModData.Text = "Data este obligatorie!";
                return;
            }


            admin.UpdateBaterie(b);
            txtStatus.Text = $"Bateria '{b.Nume}' a fost actualizata!";
        }
        private void btnAnuleaza_Click(object sender, RoutedEventArgs e)
        {
            pnlModifica.Visibility = Visibility.Collapsed;
            pnlAdauga.Visibility = Visibility.Visible;
        }
        private void mnuCauta_Click(object sender, RoutedEventArgs e)
        {
            AscundeToatePanourile(); 
            pnlCauta.Visibility = Visibility.Visible; 

        }
        private void btnCauta_Click(object sender, RoutedEventArgs e)
        {
            TipBaterie tip = (TipBaterie)cmbCautaTip.SelectedIndex;

            List<Baterie> rezultate = admin.GetBaterii(tip);

            if (rezultate.Count == 0)
            {
                txtNrRezultate.Text = "Nu au fost gasite baterii.";
                lvRezultate.ItemsSource = null;
            }
            else
            {
                txtNrRezultate.Text = $"Au fost gasite {rezultate.Count} baterii:";
                lvRezultate.ItemsSource = rezultate;
            }

            txtStatus.Text = $"Cautare finalizata: {rezultate.Count} rezultate.";
        }
        private void mnuVizualizeaza_Click(object sender, RoutedEventArgs e)
        {
            AscundeToatePanourile();
            pnlVizualizeaza.Visibility = Visibility.Visible;

            IncarcaInventar();
        }
        private void IncarcaInventar()
        {

            if (ColectieBaterii.Count == 0)
            {
                txtNrTotal.Text = "Nu exista baterii in inventar.";
            }
            else
            {
                txtNrTotal.Text = $"Total baterii: {ColectieBaterii.Count}";
            }

            txtStatus.Text = $"Inventar incarcat: {ColectieBaterii.Count} baterii.";
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            IncarcaInventar();
        }
        private void mnuIesire_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void mnuSterge_Click(object sender, RoutedEventArgs e)
        {
            AscundeToatePanourile(); 
            pnlSterge.Visibility = Visibility.Visible; 

            borderDetalii.Visibility = Visibility.Collapsed;
            txtConfirmare.Text = "";
            
        }
        private void cmbSterge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSterge.SelectedItem is not Baterie b)
            {
                borderDetalii.Visibility = Visibility.Collapsed;
                return;
            }
            txtDetNume.Text = b.Nume;
            txtDetTip.Text = b.Tip.ToString();
            txtDetCantitate.Text = b.Cantitate.ToString();
            txtDetData.Text = b.DataExpirare.ToString("dd/MM/yyyy");
            txtDetProducator.Text = b.Producator.Nume;

            borderDetalii.Visibility = Visibility.Visible;
            txtConfirmare.Text = $"Esti sigur ca vrei sa stergi bateria '{b.Nume}'?";
        }
        private void btnSterge_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSterge.SelectedItem is not Baterie b)
            {
                txtStatus.Text = "Selecteaza o baterie!";
                return;
            }

            bool ok = admin.StergeBaterie(b.Nume);

            if (ok)
            {
                txtStatus.Text = $"Bateria '{b.Nume}' a fost stearsa!";
                ColectieBaterii.Remove(b);
                borderDetalii.Visibility = Visibility.Collapsed;
                txtConfirmare.Text = "";
            }
            else
            {
                txtStatus.Text = "Eroare la stergere!";
            }
        }
        private void btnAnuleazaSterge_Click(object sender, RoutedEventArgs e)
        {
            pnlSterge.Visibility = Visibility.Collapsed;
            pnlAdauga.Visibility = Visibility.Visible;
        }
        private void AscundeToatePanourile()
        {
            pnlAdauga.Visibility = Visibility.Collapsed;
            pnlModifica.Visibility = Visibility.Collapsed;
            pnlCauta.Visibility = Visibility.Collapsed;
            pnlVizualizeaza.Visibility = Visibility.Collapsed;
            pnlSterge.Visibility = Visibility.Collapsed;
        }

    }
}