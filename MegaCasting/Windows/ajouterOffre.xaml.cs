using MegaCasting.DB;
using MegaCasting.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MegaCasting.Windows
{
    /// <summary>
    /// Logique d'interaction pour ajouterOffre.xaml
    /// </summary>
    public partial class ajouterOffre : Window
    {
        private ObservableCollection<Type_Contrat> _typesContrats = new ObservableCollection<Type_Contrat>();

        public ObservableCollection<Type_Contrat> TypesContrat
        {
            get { return _typesContrats; }
            set { _typesContrats = value; }
        }

        private ObservableCollection<Metier> _metiers = new ObservableCollection<Metier>();

        public ObservableCollection<Metier> Metiers
        {
            get { return _metiers; }
            set { _metiers = value; }
        }

        private ObservableCollection<Annonceur> _annonceurs = new ObservableCollection<Annonceur>();

        public ObservableCollection<Annonceur> Annonceurs
        {
            get { return _annonceurs; }
            set { _annonceurs = value; }
        }

        public Offre nouvelleOffre
        {
            get { return (Offre)GetValue(NouvelleOffreProperty); }
            set { SetValue(NouvelleOffreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NouvelleOffre.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NouvelleOffreProperty =
            DependencyProperty.Register("NouvelleOffre", typeof(Offre), typeof(Window), new PropertyMetadata(null));

        private offreControl parentOffreControl;
        
        public ajouterOffre(offreControl offreControl)
        {
            this.DataContext = this;

            this.parentOffreControl = (offreControl)offreControl;

            nouvelleOffre = new Offre();
            nouvelleOffre.DatePublication = DateTime.Now;
            nouvelleOffre.DateDebutContrat = DateTime.Now;

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
           
            InitializeComponent();

            Chargement();
        }

        public void Chargement()
        {
            //Remplit la combo box Type de contrat en fonction des données en base de données
            App.dbContext.Type_Contrat.ToList().ForEach(
                    tc => TypesContrat.Add(tc)
                );

            //Remplit la combo box Métier en fonction des données en base de données
            App.dbContext.Metiers.ToList().ForEach(
                    m => Metiers.Add(m)
                );

            //Remplit la combo box des Annonceur en fonction des données en base de données
            App.dbContext.Societes.OfType<Annonceur>().ToList().ForEach(
                    a => Annonceurs.Add(a)
                );
        }

        public Boolean VerificationFormulaire()
        {
            this.CacheStackPanel();
            Boolean ok = true;

            if (nouvelleOffre.Intitule == null || nouvelleOffre.Intitule.Trim() == "")
            {
                this.stackPanelIntitule.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }


            if (nouvelleOffre.Reference == null || nouvelleOffre.Reference.Trim() == "")
            {
                this.stackPanelReference.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (nouvelleOffre.DatePublication < DateTime.Today)
            {
                this.stackPanelDatePublication.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (nouvelleOffre.DureeDiffusion <= 0)
            {
                this.stackPanelDureeDiffusion.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (nouvelleOffre.DateDebutContrat < DateTime.Today || nouvelleOffre.DateDebutContrat <= nouvelleOffre.DatePublication)
            {
                this.stackPanelDateDebutContrat.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (nouvelleOffre.NbPostes <= 0)
            {
                this.stackPanelPoste.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (nouvelleOffre.DescriptionPoste == null || nouvelleOffre.DescriptionPoste.Trim() == "")
            {
                this.stackPanelDescriptionPoste.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (nouvelleOffre.DescriptionProfil == null || nouvelleOffre.DescriptionProfil.Trim() == "")
            {
                this.stackPanelDescriptionProfil.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (nouvelleOffre.Telephone == null || !regexTelephone(nouvelleOffre.Telephone))
            {
                this.stackPanelTelephone.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (nouvelleOffre.Email == null || !regexEmail(nouvelleOffre.Email))
            {
                this.stackPanelEmail.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            return ok;
        }

        public void CacheStackPanel()
        {
            this.stackPanelIntitule.Visibility = System.Windows.Visibility.Hidden;
            this.stackPanelReference.Visibility = System.Windows.Visibility.Hidden;
            this.stackPanelDatePublication.Visibility = System.Windows.Visibility.Hidden;
            this.stackPanelDureeDiffusion.Visibility = System.Windows.Visibility.Hidden;
            this.stackPanelDateDebutContrat.Visibility = System.Windows.Visibility.Hidden;
            this.stackPanelPoste.Visibility = System.Windows.Visibility.Hidden;
            this.stackPanelLocalisation.Visibility = System.Windows.Visibility.Hidden;
            this.stackPanelDescriptionPoste.Visibility = System.Windows.Visibility.Hidden;
            this.stackPanelDescriptionProfil.Visibility = System.Windows.Visibility.Hidden;
            this.stackPanelTelephone.Visibility = System.Windows.Visibility.Hidden;
            this.stackPanelEmail.Visibility = System.Windows.Visibility.Hidden;
        }

        private void boutonAjouter_Click(object sender, RoutedEventArgs e)
        {
            nouvelleOffre.Domaine_Metier = nouvelleOffre.Metier.Domaine_Metier;

            if (VerificationFormulaire())
            {
                try
                {
                    App.dbContext.Offres.Add(nouvelleOffre);
                    App.dbContext.SaveChanges();

                    this.parentOffreControl.Offres.Add(nouvelleOffre);
                    this.parentOffreControl.selectedOffre = nouvelleOffre;
                    this.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public Boolean regexTelephone(String telephone)
        {
            Regex myRegex = new Regex("^0[1-68]([-. ]?[0-9]{2}){4}$");

            return myRegex.IsMatch(telephone);
        }

        public Boolean regexEmail(String email)
        {
            Regex myRegex = new Regex(@"^[a-z0-9._-]+@[a-z0-9._-]{2,}\.[a-z]{2,4}$");

            return myRegex.IsMatch(email);
        }
    }
}
