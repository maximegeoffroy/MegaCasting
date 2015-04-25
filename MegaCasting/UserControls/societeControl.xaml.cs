using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Logique d'interaction pour societeControl.xaml
    /// </summary>
    public partial class societeControl : UserControl
    {

        private ObservableCollection<Societe> _societes = new ObservableCollection<Societe>();

        public ObservableCollection<Societe> Societes
        {
            get { return _societes; }
            set { _societes = value; }
        }



        public Societe selectedSociete
        {
            get { return (Societe)GetValue(selectedSocieteProperty); }
            set { SetValue(selectedSocieteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for selectedSociete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty selectedSocieteProperty =
            DependencyProperty.Register("selectedSociete", typeof(Societe), typeof(UserControl), new PropertyMetadata(null));

        
        
        public societeControl()
        {
            this.DataContext = this;

            InitializeComponent();

            App.dbContext.Societes.ToList().ForEach(
                s => Societes.Add(s)
                );
        }

        private void boutonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.dbContext.Societes.Remove(selectedSociete);
                App.dbContext.SaveChanges();

                Societes.Remove(selectedSociete);

            }
            catch (System.Exception)
            {                
                throw;
            }

        }

        private void boutonNouveau_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
