using MegaCasting.DB;
using MegaCasting.UserControls;
using System;
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

namespace MegaCasting
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void offreBouton_Click(object sender, RoutedEventArgs e)
        {
            offreControl oc = new offreControl();
            this.mainContentControl.Content = oc;
        }

        private void societeBouton_Click(object sender, RoutedEventArgs e)
        {
            societeControl sc = new societeControl();
            this.mainContentControl.Content = sc;
        }

        private void contratBouton_Click(object sender, RoutedEventArgs e)
        {
            contratControl cc = new contratControl();
            this.mainContentControl.Content = cc;
        }

        private void domaineMetierBouton_Click(object sender, RoutedEventArgs e)
        {
            domaineMetierControl dmc = new domaineMetierControl();
            this.mainContentControl.Content = dmc;
        }

        private void metierBouton_Click(object sender, RoutedEventArgs e)
        {
            metierControl mc = new metierControl();
            this.mainContentControl.Content = mc;
        }
    }
}
