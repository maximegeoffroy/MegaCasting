using MegaCasting.DB;
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
using System.Windows.Shapes;

namespace MegaCasting
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        public Connexion()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void boutonConnexion_Click(object sender, RoutedEventArgs e)
        {
            Utilisateur utilisateurEntrer = new Utilisateur();
            utilisateurEntrer.Nom = this.textBoxNomUtilisateur.Text;
            utilisateurEntrer.Password = this.passwordBox.Password;

            Utilisateur utilisateurBDD = App.dbContext.Utilisateurs.FirstOrDefault(uTemp => uTemp.Nom == utilisateurEntrer.Nom && uTemp.Password == utilisateurEntrer.Password);

            if (utilisateurBDD != null)
            {
                MainWindow mw = new MainWindow();
                mw.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Il n'y a pas d'utilisateur de ce nom","Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                this.textBoxNomUtilisateur.Clear();
                this.passwordBox.Clear();
            }
        }

        private void textBoxNomUtilisateur_KeyUp(object sender, KeyEventArgs e)
        {
            this.boutonConnexion.IsEnabled = false;

            if (this.textBoxNomUtilisateur.Text != "")
            {
                this.boutonConnexion.IsEnabled = true;
            }
        }
    }
}
