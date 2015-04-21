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

            App.dbContext.Metiers.ToList().ForEach(
                m => Metiers.Add(m)
                );
        }

        private void boutonNouveau_Click(object sender, RoutedEventArgs e)
        {
            ajouterMetier am = new ajouterMetier(this);
            am.Show();
        }
        
        private void boutonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.dbContext.Metiers.Remove(selectedMetier);
                App.dbContext.SaveChanges();

                Metiers.Remove(selectedMetier);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
