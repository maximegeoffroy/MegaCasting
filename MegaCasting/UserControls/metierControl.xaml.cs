using MegaCasting.DB;
using MegaCasting.Windows;
using System;
using System.Collections.ObjectModel;
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

namespace MegaCasting.UserControls
{
    /// <summary>
    /// Logique d'interaction pour metierControl.xaml
    /// </summary>
    public partial class metierControl : UserControl
    {
        private ObservableCollection<Metier> _metiers = new ObservableCollection<Metier>();

        public ObservableCollection<Metier> Metiers
        {
            get { return _metiers; }
            set { _metiers = value; }
        }

        private ObservableCollection<Domaine_Metier> _domaineMetiers = new ObservableCollection<Domaine_Metier>();

        public ObservableCollection<Domaine_Metier> DomaineMetiers
        {
            get { return _domaineMetiers; }
            set { _domaineMetiers = value; }
        }

        public Metier selectedMetier
        {
            get { return (Metier)GetValue(selectedMetierProperty); }
            set { SetValue(selectedMetierProperty, value); }
        }

        // Using a DependencyProperty as the backing store for selectedMetier.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty selectedMetierProperty =
            DependencyProperty.Register("selectedMetier", typeof(Metier), typeof(UserControl), new PropertyMetadata(null));



        public metierControl()
        {
            this.DataContext = this;

            InitializeComponent();

            App.dbContext.Domaine_Metier.ToList().ForEach(
                dm => DomaineMetiers.Add(dm)
                );

            App.dbContext.Metiers.ToList().ForEach(
                m => Metiers.Add(m)
                );
        }

        public Boolean VerificationFormulaire()
        {
            this.labelErreurLibelle.Visibility = System.Windows.Visibility.Hidden;
            Boolean ok = true;

            if (selectedMetier.Libelle == null | selectedMetier.Libelle.Trim() == "")
            {
                this.labelErreurLibelle.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            return ok;
        }

        private void boutonNouveau_Click(object sender, RoutedEventArgs e)
        {
            ajouterMetier am = new ajouterMetier(this);
            am.Show();
        }

        private void boutonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMetier != null)
            {
                Offre o = App.dbContext.Offres.FirstOrDefault(oTemp => oTemp.IdentifiantMetier == selectedMetier.Identifiant);

                if (o == null)
                {
                    try
                    {
                        App.dbContext.Metiers.Remove(selectedMetier);
                        App.dbContext.SaveChanges();

                        Metiers.Remove(selectedMetier);
                        this.listeMetier.SelectedIndex = 0;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    MessageBox.Show("Impossible de supprimer le métier car il est rataché à une ou plusieurs offre(s)", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void boutonEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMetier != null)
            {
                this.libelleTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                this.listeDomaineMetier.GetBindingExpression(ComboBox.SelectedItemProperty).UpdateSource();

                if (VerificationFormulaire())
                {
                    try
                    {
                        App.dbContext.SaveChangesAsync();
                        MessageBox.Show("Métier enregistré", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
    }
}
