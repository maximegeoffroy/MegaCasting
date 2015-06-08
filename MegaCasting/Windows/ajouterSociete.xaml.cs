using MegaCasting.DB;
using MegaCasting.UserControls;
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

        public Boolean VerificationFormulaire()
        {
            this.CacheLabelErreur();
            Boolean ok = true;

            if (this.textBoxRaisonSociale.Text.Trim() == "")
            {
                this.labelErreurRaisonSociale.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (this.textBoxTelephone.Text.Trim() == "" || !regexTelephone(this.textBoxTelephone.Text))
            {
                this.labelErreurTelephone.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (this.textBoxEmail.Text.Trim() == "" || !regexEmail(this.textBoxEmail.Text))
            {
                this.labelErreurEmail.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (this.textBoxSiret.Text.Trim() == "" || !regexSiret(this.textBoxSiret.Text))
            {
                this.labelErreurSiret.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (this.textBoxAdresseNumero.Text.Trim() == "")
            {
                this.labelErreurAdresseNumero.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (this.textBoxAdresseRue.Text.Trim() == "")
            {
                this.labelErreurAdresseRue.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (this.textBoxAdresseCodePostal.Text.Trim() == "" || !regexCodePostal(this.textBoxAdresseCodePostal.Text))
            {
                this.labelErreurAdresseCodePostal.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            if (this.textBoxAdresseVille.Text.Trim() == "")
            {
                this.labelErreurAdresseVille.Visibility = System.Windows.Visibility.Visible;
                ok = false;
            }
            return ok;
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

        private void boutonAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (VerificationFormulaire())
            {
                Adresse adresse = new Adresse();
                int num;
                Boolean isOk = true;
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
                    if (this.radioAnnonceur.IsChecked == true)
                    {
                        Annonceur a = new Annonceur();
                        a.RaisonSociale = this.textBoxRaisonSociale.Text;
                        a.Telephone = this.textBoxTelephone.Text;
                        a.Email = this.textBoxEmail.Text;
                        a.NumeroSiret = this.textBoxSiret.Text;
                        adresse.Rue = this.textBoxAdresseRue.Text;
                        adresse.CodePostal = this.textBoxAdresseCodePostal.Text;
                        adresse.Ville = this.textBoxAdresseVille.Text;
                        a.Adresse = adresse;

                        if (isOk == true)
                        {
                            try
                            {
                                App.dbContext.Societes.Add(a);
                                App.dbContext.SaveChanges();

                                this.parentSocieteControl.Societes.Add(a);
                                this.parentSocieteControl.selectedSociete = a;
                                this.Close();
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    }
                    else if (radioDiffuseur.IsChecked == true)
                    {
                        Diffuseur d = new Diffuseur();
                        d.RaisonSociale = this.textBoxRaisonSociale.Text;
                        d.Telephone = this.textBoxTelephone.Text;
                        d.Email = this.textBoxEmail.Text;
                        d.NumeroSiret = this.textBoxSiret.Text;
                        adresse.Numero = num;
                        adresse.Rue = this.textBoxAdresseRue.Text;
                        adresse.CodePostal = this.textBoxAdresseCodePostal.Text;
                        adresse.Ville = this.textBoxAdresseVille.Text;
                        d.Adresse = adresse;

                        if (isOk == true)
                        {
                            try
                            {


                                App.dbContext.Societes.Add(d);
                                App.dbContext.SaveChanges();

                                this.parentSocieteControl.Societes.Add(d);
                                this.parentSocieteControl.selectedSociete = d;
                                this.Close();
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
}
