using MegaCasting.DB;
using MegaCasting.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public Boolean regexTelephone(String telephone)
        {
            Regex myRegex = new Regex("^0[1-68]([-. ]?[0-9]{2}){4}$");

            return myRegex.IsMatch(telephone);
        }

        public Boolean regexEmail(String email)
        {
            Regex myRegex = new Regex(@"^[a-z0-9._-]+@[a-z0-9._-]{2,}\.[a-z]{2,4}$");

            return myRegex.IsMatch(email);
        }

        public Boolean regexSiret(String numeroSiret)
        {
            Regex myRegex = new Regex(@"^[0-9]{14}$");

            return myRegex.IsMatch(numeroSiret);
        }

        public Boolean regexCodePostal(String codePostal)
        {
            Regex myRegex = new Regex(@"^[0-9]{5}$");

            return myRegex.IsMatch(codePostal);
        }

        public void CacheLabelErreur()
        {
            this.labelErreurRaisonSociale.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurTelephone.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurEmail.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurSiret.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurAdresseNumero.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurAdresseRue.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurAdresseCodePostal.Visibility = System.Windows.Visibility.Hidden;
            this.labelErreurAdresseVille.Visibility = System.Windows.Visibility.Hidden;
        }

        public Boolean VerificationFormulaire()
        {
            Boolean ok = true;
            this.CacheLabelErreur();

            if (textBoxRaisonSociale.Text.Trim() == "")
            {
                this.labelErreurRaisonSociale.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (textBoxTelephone.Text.Trim() == "" || !regexTelephone(this.textBoxTelephone.Text))
            {
                this.labelErreurTelephone.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (textBoxEmail.Text.Trim() == "" || !regexEmail(this.textBoxEmail.Text))
            {
                this.labelErreurEmail.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (textBoxSiret.Text.Trim() == "" || !regexSiret(this.textBoxSiret.Text))
            {
                this.labelErreurSiret.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (textBoxAdresseNumero.Text.Trim() == "")
            {
                this.labelErreurAdresseNumero.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (textBoxAdresseRue.Text.Trim() == "")
            {
                this.labelErreurAdresseRue.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (textBoxAdresseCodePostal.Text.Trim() == "" || !regexCodePostal(this.textBoxAdresseCodePostal.Text))
            {
                this.labelErreurAdresseCodePostal.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (textBoxAdresseVille.Text.Trim() == "")
            {
                this.labelErreurAdresseVille.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            return ok;
        }

        public void UpdateSources()
        {
            this.textBoxRaisonSociale.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxTelephone.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxEmail.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxSiret.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxAdresseNumero.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxAdresseRue.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxAdresseCodePostal.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.textBoxAdresseVille.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void boutonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSociete != null)
            {
                Offre o = App.dbContext.Offres.FirstOrDefault(oTemp => oTemp.Annonceur.Identifiant == selectedSociete.Identifiant);

                if (o == null)
                {
                    try
                    {
                        App.dbContext.Societes.Remove(selectedSociete);
                        App.dbContext.SaveChanges();

                        Societes.Remove(selectedSociete);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    MessageBox.Show("Impossible de supprimer l'annonceur car il est rataché à une ou plusieurs offre(s)", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void boutonNouveau_Click(object sender, RoutedEventArgs e)
        {
            ajouterSociete ajoutSoc = new ajouterSociete(this);
            ajoutSoc.Show();
        }

        private void boutonEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSociete != null)
            {
                Boolean isOk = true;
                int num;
                Adresse adresse = selectedSociete.Adresse;
                this.UpdateSources();
                if (VerificationFormulaire())
                {
                    if (int.TryParse(this.textBoxAdresseNumero.Text, out num))
                    {
                        adresse.Numero = num;
                    }
                    else
                    {
                        isOk = false;
                    }

                    if (isOk == true)
                    {
                        if (selectedSociete is Annonceur)
                        {
                            if (radioAnnonceur.IsChecked == true)
                            {
                                try
                                {
                                    App.dbContext.SaveChangesAsync();
                                    MessageBox.Show("Annonceur enregistré", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);

                                }
                                catch (Exception)
                                {

                                    throw;
                                }
                            }
                            else
                            {
                                try
                                {
                                    Diffuseur d = new Diffuseur();
                                    d.RaisonSociale = this.textBoxRaisonSociale.Text;
                                    d.Telephone = this.textBoxTelephone.Text;
                                    d.Email = this.textBoxEmail.Text;
                                    d.NumeroSiret = this.textBoxSiret.Text;
                                    d.Adresse = adresse;

                                    App.dbContext.Societes.Remove(selectedSociete);
                                    Societes.Remove(selectedSociete);

                                    App.dbContext.Societes.Add(d);
                                    App.dbContext.SaveChangesAsync();
                                    MessageBox.Show("Diffuseur enregistré", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);

                                    Societes.Add(d);
                                    listeSociete.SelectedItem = d;
                                }
                                catch (Exception)
                                {

                                    throw;
                                }
                            }
                        }
                        else
                        {
                            if (radioDiffuseur.IsChecked == true)
                            {
                                try
                                {
                                    App.dbContext.SaveChangesAsync();
                                    MessageBox.Show("Diffuseur enregistré", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);

                                }
                                catch (Exception)
                                {

                                    throw;
                                }
                            }
                            else
                            {
                                try
                                {
                                    Annonceur a = new Annonceur();
                                    a.RaisonSociale = this.textBoxRaisonSociale.Text;
                                    a.Telephone = this.textBoxTelephone.Text;
                                    a.Email = this.textBoxEmail.Text;
                                    a.NumeroSiret = this.textBoxSiret.Text;
                                    a.Adresse = adresse;

                                    App.dbContext.Societes.Remove(selectedSociete);
                                    Societes.Remove(selectedSociete);

                                    App.dbContext.Societes.Add(a);
                                    App.dbContext.SaveChangesAsync();
                                    MessageBox.Show("Annonceur enregistré", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);

                                    Societes.Add(a);
                                    listeSociete.SelectedItem = a;
                                }
                                catch (Exception)
                                {

                                    throw;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.labelErreurAdresseNumero.Visibility = System.Windows.Visibility.Visible;
                    }      
                }
            }
        }

        private void listeSociete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedSociete is Annonceur)
            {
                radioAnnonceur.IsChecked = true;
            }
            else
            {
                radioDiffuseur.IsChecked = true;
            }
        }
    }
}
