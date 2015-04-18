using MegaCasting.DB;
using MegaCasting.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<Domaine_Metier> _domainesMetier = new ObservableCollection<Domaine_Metier>();

        public ObservableCollection<Domaine_Metier> DomainesMetier
        {
            get { return _domainesMetier; }
            set { _domainesMetier = value; }
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

            //Remplit la liste des domaines métier en fonction du contenu de la base de données
            App.dbContext.Domaine_Metier.ToList().ForEach(
                dm => DomainesMetier.Add(dm)
            );

            //Remplit la liste des annonceurs en fonction du contenu de la base de données
            App.dbContext.Societes.OfType<Annonceur>().ToList().ForEach(
                a => Annonceurs.Add(a)
            );
        }

        private void listeOffre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.listeTypesContrat.SelectedItem = selectedOffre.Type_Contrat;
            this.listeMetier.SelectedItem = selectedOffre.Metier;
            this.listeDomainesMetier.SelectedItem = selectedOffre.Domaine_Metier;
            this.listeAnnonceurs.SelectedItem = selectedOffre.Annonceur;
        }

        private void boutonEnregistrer_Click(object sender, RoutedEventArgs e)
        {

            selectedOffre.Type_Contrat = (Type_Contrat)this.listeTypesContrat.SelectedItem;
            selectedOffre.Metier = (Metier)this.listeMetier.SelectedItem;
            selectedOffre.Domaine_Metier = (Domaine_Metier)this.listeDomainesMetier.SelectedItem;
            selectedOffre.Annonceur = (Annonceur)this.listeAnnonceurs.SelectedItem;

            App.dbContext.SaveChangesAsync();
        }

        private void boutonNouvelle_Click(object sender, RoutedEventArgs e)
        {
            ajouterOffre ao = new ajouterOffre();
            ao.Show();
        }

    }
}
