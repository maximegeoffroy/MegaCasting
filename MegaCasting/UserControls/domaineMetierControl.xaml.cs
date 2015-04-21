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
        private ObservableCollection<Domaine_Metier> _domaineMetiers;

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

        private void boutonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.dbContext.Domaine_Metier.Remove(selectedDomaineMetier);
                App.dbContext.SaveChanges();

                DomaineMetiers.Remove(selectedDomaineMetier);
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        private void boutonNouveau_Click(object sender, RoutedEventArgs e)
        {
            ajouterDomaineMetier adm = new ajouterDomaineMetier(this);
            adm.Show();
        }
    }
}
