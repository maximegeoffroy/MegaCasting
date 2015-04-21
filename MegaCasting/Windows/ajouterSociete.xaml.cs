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
    /// Logique d'interaction pour ajouterSociete.xaml
    /// </summary>
    public partial class ajouterSociete : Window
    {
        private societeControl parentSocieteControl;

        public ajouterSociete(societeControl societeControl)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.parentSocieteControl = (societeControl)societeControl;
            InitializeComponent();
        }

        private void boutonAjouter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
