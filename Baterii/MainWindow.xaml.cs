using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LibrarieModele;
using NivelStocareDate;


namespace Baterii
{
    public partial class MainWindow : Window
    {
        private const int LUNGIME_MAX_NUME = 15;
        private const int CANTITATE_MIN = 1;

        static IStocareData admin = StocareFactory.GetAdministratorStocare();

        public MainWindow()
        {
            InitializeComponent();
        }
        private int ValideazaDateBaterie()
        {
            int codEroare = 0;
            ResetErori();
            if (string.IsNullOrWhiteSpace(txtNume.Text))
            {
                codEroare = 1;
                lblNume.Foreground = Brushes.Red;
                errNume.Text = "Numele este obligatoriu!";
            }
            else if (txtNume.Text.Length > LUNGIME_MAX_NUME)
            {
                codEroare = 2;
                lblNume.Foreground = Brushes.Red;
                errNume.Text = $"Max {LUNGIME_MAX_NUME} caractere!";
            }
            if (dpData.SelectedDate == null)
            {
                codEroare = 3;
                lblData.Foreground = Brushes.Red;
                errData.Text = "Data expirarii este obligatorie!";
            }
            if (string.IsNullOrWhiteSpace(txtCantitate.Text))
            {
                codEroare = 4;
                lblCantitate.Foreground = Brushes.Red;
                errCantitate.Text = "Cantitatea este obligatorie!";
            }
            else if (!int.TryParse(txtCantitate.Text, out int cant) || cant < CANTITATE_MIN)
            {
                codEroare = 5;
                lblCantitate.Foreground = Brushes.Red;
                errCantitate.Text = "Cantitate invalida!";
            }
            if (string.IsNullOrWhiteSpace(txtProducator.Text))
            {
                codEroare = 6;
                lblProducator.Foreground = Brushes.Red;
                errProducator.Text = "Producatorul este obligatoriu!";
            }
            return codEroare;
        }
        private void ResetErori()
        {
            lblNume.Foreground = Brushes.Black;
            lblData.Foreground = Brushes.Black;
            lblCantitate.Foreground = Brushes.Black;
            lblProducator.Foreground = Brushes.Black;
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
            admin.AddBaterie(baterie);
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
            pnlAdauga.Visibility = Visibility.Visible;
            pnlModifica.Visibility = Visibility.Collapsed;
        }
        private void mnuModifica_Click(object sender, RoutedEventArgs e)
        {
            pnlAdauga.Visibility = Visibility.Collapsed;
            pnlModifica.Visibility = Visibility.Visible;
            cmbBaterii.ItemsSource = admin.GetBaterii();
            cmbBaterii.DisplayMemberPath = "Nume";
        }
        private void cmbBaterii_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbBaterii.SelectedItem is not Baterie b) return;
            txtModCantitate.Text = b.Cantitate.ToString();
            dpModData.SelectedDate = b.DataExpirare;
            txtModProducator.Text = b.Producator.Nume;
            rbModAlcalina.IsChecked = b.Tip == TipBaterie.Alcalina;
            rbModLitiu.IsChecked = b.Tip == TipBaterie.Litiu;
            rbModNichelMetal.IsChecked = b.Tip == TipBaterie.NichelMetal;
            rbModPlumb.IsChecked = b.Tip == TipBaterie.Plumb;
            chkModReincarcabila.IsChecked = b.Optiuni.HasFlag(OptiuniBaterie.Reincarcabila);
            chkModImpermeabila.IsChecked = b.Optiuni.HasFlag(OptiuniBaterie.Impermeabila);
            chkModTempScazuta.IsChecked = b.Optiuni.HasFlag(OptiuniBaterie.TemperaturaScazuta);
            chkModTempRidicata.IsChecked = b.Optiuni.HasFlag(OptiuniBaterie.TemperaturaRidicata);
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
                lblModCantitate.Foreground = Brushes.Red;
                errModCantitate.Text = "Cantitate invalida!";
                return;
            }

            if (dpModData.SelectedDate == null)
            {
                lblModData.Foreground = Brushes.Red;
                errModData.Text = "Data este obligatorie!";
                return;
            }
            b.Cantitate = cantitate;
            b.DataExpirare = dpModData.SelectedDate.Value;
            b.Producator.Nume = txtModProducator.Text;
            if (rbModLitiu.IsChecked == true) b.Tip = TipBaterie.Litiu;
            else if (rbModNichelMetal.IsChecked == true) b.Tip = TipBaterie.NichelMetal;
            else if (rbModPlumb.IsChecked == true) b.Tip = TipBaterie.Plumb;
            else b.Tip = TipBaterie.Alcalina;
            b.Optiuni = OptiuniBaterie.Niciuna;
            if (chkModReincarcabila.IsChecked == true) b.Optiuni |= OptiuniBaterie.Reincarcabila;
            if (chkModImpermeabila.IsChecked == true) b.Optiuni |= OptiuniBaterie.Impermeabila;
            if (chkModTempScazuta.IsChecked == true) b.Optiuni |= OptiuniBaterie.TemperaturaScazuta;
            if (chkModTempRidicata.IsChecked == true) b.Optiuni |= OptiuniBaterie.TemperaturaRidicata;

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
            pnlAdauga.Visibility = Visibility.Collapsed;
            pnlModifica.Visibility = Visibility.Collapsed;
            pnlCauta.Visibility = Visibility.Visible;
            //pnlVizualizeaza.Visibility = Visibility.Collapsed;
            lvRezultate.ItemsSource = null;
            txtNrRezultate.Text = "";
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
    }
}