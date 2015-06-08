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
    /// Logique d'interaction pour contratControl.xaml
    /// </summary>
    public partial class contratControl : UserControl
    {
        private ObservableCollection<Type_Contrat> _contrats = new ObservableCollection<Type_Contrat>();

        public ObservableCollection<Type_Contrat> Contrats
        {
            get { return _contrats; }
            set { _contrats = value; }
        }

        public Type_Contrat selectedTypeContrat
        {
            get { return (Type_Contrat)GetValue(selectedTypeContratProperty); }
            set { SetValue(selectedTypeContratProperty, value); }
        }

        // Using a DependencyProperty as the backing store for selectedTypeContrat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty selectedTypeContratProperty =
            DependencyProperty.Register("selectedTypeContrat", typeof(Type_Contrat), typeof(UserControl), new PropertyMetadata(null));

        
        public contratControl()
        {
            this.DataContext = this;

            InitializeComponent();

            App.dbContext.Type_Contrat.ToList().ForEach(
                c => Contrats.Add(c)
            );
        }

        public Boolean VerificationFormulaire()
        {
            this.labelErreurLibelle.Visibility = System.Windows.Visibility.Hidden;
            Boolean ok = true;

            if (selectedTypeContrat.Libelle == null | selectedTypeContrat.Libelle.Trim() == "")
            {
                this.labelErreurLibelle.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            return ok;
        }

        private void boutonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if(selectedTypeContrat != null)
            {
                Offre o = App.dbContext.Offres.FirstOrDefault(oTemp => oTemp.Type_Contrat.Identifiant == selectedTypeContrat.Identifiant);

                if (o == null)
                {
                    try
                    {
                        App.dbContext.Type_Contrat.Remove(selectedTypeContrat);
                        App.dbContext.SaveChanges();

                        Contrats.Remove(selectedTypeContrat);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    MessageBox.Show("Impossible de supprimer le type de contrat car il est rataché à une ou plusieurs offre(s)", "Erreur de suppression",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }

        private void boutonNouveau_Click(object sender, RoutedEventArgs e)
        {
            ajouterTypeContrat atc = new ajouterTypeContrat(this);
            atc.Show();
        }

        private void boutonEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTypeContrat != null)
            {
                this.libelleTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                if (VerificationFormulaire())
                {
                    try
                    {
                        App.dbContext.SaveChangesAsync();
                        MessageBox.Show("Contrat enregistré", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
    }
}
