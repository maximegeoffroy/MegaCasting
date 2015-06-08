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
    /// Logique d'interaction pour ajouterTypeContrat.xaml
    /// </summary>
    public partial class ajouterTypeContrat : Window
    {
        private contratControl parentContratControl;

        public ajouterTypeContrat(contratControl contratControl)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.parentContratControl = (contratControl)contratControl;
            InitializeComponent();
        }

        public Boolean VerificationFormulaire()
        {
            this.labelErreurLibelle.Visibility = System.Windows.Visibility.Hidden;
            Boolean ok = true;

            if (this.textBoxLibelle.Text.Trim() == "")
            {
                this.labelErreurLibelle.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            return ok;
        }

        private void boutonAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (VerificationFormulaire())
            {
                Type_Contrat tc = new Type_Contrat();
                tc.Libelle = textBoxLibelle.Text;

                try
                {
                    App.dbContext.Type_Contrat.Add(tc);
                    App.dbContext.SaveChanges();

                    this.parentContratControl.Contrats.Add(tc);
                    this.parentContratControl.selectedTypeContrat = tc;
                    this.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
