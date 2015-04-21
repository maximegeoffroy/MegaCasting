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
    /// Logique d'interaction pour ajouterDomaineMetier.xaml
    /// </summary>
    public partial class ajouterDomaineMetier : Window
    {
        private domaineMetierControl parentDomaineMetierControl;

        public ajouterDomaineMetier(domaineMetierControl domaineMetierControl)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.parentDomaineMetierControl = (domaineMetierControl)domaineMetierControl;
            InitializeComponent();
        }

        private void boutonAjouter_Click(object sender, RoutedEventArgs e)
        {
            Domaine_Metier dm = new Domaine_Metier();
            dm.Libelle = this.textBoxLibelle.Text;

            try
            {
                App.dbContext.Domaine_Metier.Add(dm);
                App.dbContext.SaveChanges();

                this.parentDomaineMetierControl.DomaineMetiers.Add(dm);
                this.parentDomaineMetierControl.selectedDomaineMetier = dm;
                this.Close();
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
