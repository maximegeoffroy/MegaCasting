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
    /// Logique d'interaction pour domaineMetierControl.xaml
    /// </summary>
    public partial class domaineMetierControl : UserControl
    {
        private ObservableCollection<Domaine_Metier> _domaineMetiers = new ObservableCollection<Domaine_Metier>();

        public ObservableCollection<Domaine_Metier> DomaineMetiers
        {
            get { return _domaineMetiers; }
            set { _domaineMetiers = value; }
        }



        public Domaine_Metier selectedDomaineMetier
        {
            get { return (Domaine_Metier)GetValue(selectedDomaineMetierProperty); }
            set { SetValue(selectedDomaineMetierProperty, value); }
        }

        // Using a DependencyProperty as the backing store for selectedDomaineMetier.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty selectedDomaineMetierProperty =
            DependencyProperty.Register("selectedDomaineMetier", typeof(Domaine_Metier), typeof(UserControl), new PropertyMetadata(null));


        public domaineMetierControl()
        {
            this.DataContext = this;


            InitializeComponent();

            App.dbContext.Domaine_Metier.ToList().ForEach(
                dm => DomaineMetiers.Add(dm)
                );
        }

        public Boolean VerificationFormulaire()
        {
            this.labelErreurLibelle.Visibility = System.Windows.Visibility.Hidden;
            Boolean ok = true;

            if (selectedDomaineMetier.Libelle == null | selectedDomaineMetier.Libelle.Trim() == "")
            {
                this.labelErreurLibelle.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            return ok;
        }

        private void boutonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDomaineMetier != null)
            {
                Metier m = App.dbContext.Metiers.FirstOrDefault(mTemp => mTemp.IdentifiantDomaine_Metier == selectedDomaineMetier.Identifiant);

                if (m == null)
                {
                    try
                    {
                        App.dbContext.Domaine_Metier.Remove(selectedDomaineMetier);
                        App.dbContext.SaveChanges();

                        DomaineMetiers.Remove(selectedDomaineMetier);
                        this.listeDomaineMetier.SelectedIndex = 0;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    MessageBox.Show("Impossible de supprimer le domaine métier car il est rataché à un ou plusieurs métier(s)", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void boutonNouveau_Click(object sender, RoutedEventArgs e)
        {
            ajouterDomaineMetier adm = new ajouterDomaineMetier(this);
            adm.Show();
        }

        private void boutonEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDomaineMetier != null)
            {
                this.textBoxLibelle.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                if (VerificationFormulaire())
                {
                    try
                    {
                        App.dbContext.SaveChangesAsync();
                        MessageBox.Show("Domaine métier enregistré", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
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
