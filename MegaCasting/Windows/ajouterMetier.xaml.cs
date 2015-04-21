using MegaCasting.DB;
using MegaCasting.UserControls;
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
using System.Windows.Shapes;

namespace MegaCasting.Windows
{
    /// <summary>
    /// Logique d'interaction pour ajouterMetier.xaml
    /// </summary>
    public partial class ajouterMetier : Window
    {
        private ObservableCollection<Domaine_Metier> _domaineMetiers = new ObservableCollection<Domaine_Metier>();

        public ObservableCollection<Domaine_Metier> DomaineMetiers
        {
            get { return _domaineMetiers; }
            set { _domaineMetiers = value; }
        }
        

        private metierControl parentMetierControl;

        public ajouterMetier(metierControl metierControl)
        {
            this.DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.parentMetierControl = (metierControl)metierControl;
            InitializeComponent();

            App.dbContext.Domaine_Metier.ToList().ForEach(
                dm => DomaineMetiers.Add(dm)
                );
        }

        private void boutonAjouter_Click(object sender, RoutedEventArgs e)
        {
            Metier m = new Metier();
            m.Libelle = textBoxLibelle.Text;
            m.Domaine_Metier = (Domaine_Metier)this.listeDomaineMetier.SelectedItem;

            try
            {
                App.dbContext.Metiers.Add(m);
                App.dbContext.SaveChanges();

                this.parentMetierControl.Metiers.Add(m);
                this.parentMetierControl.selectedMetier = m;
                this.Close();
            }
            catch (Exception)
            {                
                throw;
            }

        }
    }
}
