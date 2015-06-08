using MegaCasting.DB;
using MegaCasting.Windows;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MegaCasting.UserControls
{
    /// <summary>
    /// Logique d'interaction pour offreControl.xaml
    /// </summary>
    public partial class offreControl : UserControl
    {
        private ObservableCollection<Offre> _offres = new ObservableCollection<Offre>();

        public ObservableCollection<Offre> Offres
        {
            get { return _offres; }
            set { _offres = value; }
        }

        private ObservableCollection<Type_Contrat> _typesContrat = new ObservableCollection<Type_Contrat>();

        public ObservableCollection<Type_Contrat> TypesContrat
        {
            get { return _typesContrat; }
            set { _typesContrat = value; }
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
              
        public Offre selectedOffre
        {
            get { return (Offre)GetValue(selectedOffreProperty); }
            set { SetValue(selectedOffreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for selectedOffre.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty selectedOffreProperty =
            DependencyProperty.Register("selectedOffre", typeof(Offre), typeof(UserControl), new PropertyMetadata(null));

        public offreControl()
        {
            this.DataContext = this;

            InitializeComponent();

            Chargement();

        }

        public void Chargement()
        {
            //Remplit la liste des offres en fonction du contenu de la base de données
            App.dbContext.Offres.ToList().ForEach(
                o => Offres.Add(o)
            );

            //Remplit la liste des contrats en fonction du contenu de la base de données
            App.dbContext.Type_Contrat.ToList().ForEach(
                tc => TypesContrat.Add(tc)
            );

            //Remplit la liste des métiers en fonction du contenu de la base de données
            App.dbContext.Metiers.ToList().ForEach(
                m => Metiers.Add(m)
            );

            //Remplit la liste des annonceurs en fonction du contenu de la base de données
            App.dbContext.Societes.OfType<Annonceur>().ToList().ForEach(
                a => Annonceurs.Add(a)
            );
        }

        public Boolean VerificationFormulaire()
        {
            this.CacheStackPanel();
            Boolean ok = true;

            if (selectedOffre.Intitule == null || selectedOffre.Intitule.Trim() == "")
            {
                this.labelErreurIntitule.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }


            if (selectedOffre.Reference == null || selectedOffre.Reference.Trim() == "")
            {
                this.labelErreurReference.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (selectedOffre.DureeDiffusion <= 0 || this.textBoxDureeDiffusion.Text.Trim() == "")
            {
                this.labelErreurDureeDiffusion.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (selectedOffre.DateDebutContrat <= selectedOffre.DatePublication)
            {
                this.labelErreurDateDebutContrat.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (selectedOffre.NbPostes <= 0 || this.textBoxNbPoste.Text.Trim() == "")
            {
                this.labelErreurNbPoste.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (selectedOffre.DescriptionPoste == null || selectedOffre.DescriptionPoste.Trim() == "")
            {
                this.labelErreurDescriptionPoste.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (selectedOffre.DescriptionProfil == null || selectedOffre.DescriptionProfil.Trim() == "")
            {
                this.labelErreurDescriptionProfil.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (selectedOffre.Telephone == null || !regexTelephone(selectedOffre.Telephone))
            {
                this.labelErreurTelephone.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }

            if (selectedOffre.Email == null || !regexEmail(selectedOffre.Email))
            {
                this.labelErreurEmail.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            return ok;
        }

        public void CacheStackPanel()
        {
            this.labelErreurIntitule.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurReference.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurDatePublication.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurDureeDiffusion.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurDateDebutContrat.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurNbPoste.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurDescriptionPoste.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurDescriptionProfil.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurTelephone.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurEmail.Visibility = System.Windows.Visibility.Hidden;
        }

        public void UpdateSources()
        {
            this.textBoxIntitule.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxReference.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.datePickerDatePublication.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
            this.textBoxDureeDiffusion.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.datePickerDateDebutContrat.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
            this.textBoxNbPoste.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxLocalisation.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxDescriptionPoste.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxDescriptionProfil.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxTelephone.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxEmail.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.listeTypesContrat.GetBindingExpression(ComboBox.SelectedItemProperty).UpdateSource();
            this.listeMetier.GetBindingExpression(ComboBox.SelectedItemProperty).UpdateSource();
            this.listeAnnonceurs.GetBindingExpression(ComboBox.SelectedItemProperty).UpdateSource();
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

        private void boutonEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOffre != null)
            {
                int dureeDiffusion, nbPoste;
                Metier m = (Metier)this.listeMetier.SelectedItem;
                selectedOffre.Domaine_Metier = m.Domaine_Metier;

                this.UpdateSources();

                if (VerificationFormulaire())
                {
                    Boolean dureeDiffusionValide = int.TryParse(this.textBoxDureeDiffusion.Text, out dureeDiffusion);
                    Boolean nbPosteValide = int.TryParse(this.textBoxNbPoste.Text, out nbPoste);
                    if (dureeDiffusionValide && nbPosteValide)
                    {
                        try
                        {
                            App.dbContext.SaveChangesAsync();
                            MessageBox.Show("Offre enregistrée", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    else
                    {
                        if(dureeDiffusionValide == false)
                            this.labelErreurDureeDiffusion.Visibility = System.Windows.Visibility.Visible;

                        if (nbPosteValide == false)
                            this.labelErreurNbPoste.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
        }

        private void boutonNouvelle_Click(object sender, RoutedEventArgs e)
        {
            ajouterOffre ao = new ajouterOffre(this);
            ao.Show();
        }

        private void boutonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOffre != null)
            {
                try
                {
                    App.dbContext.Offres.Remove(selectedOffre);
                    App.dbContext.SaveChanges();

                    Offres.Remove(selectedOffre);
                    this.listeOffre.SelectedIndex = 0;
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }
    }
}
