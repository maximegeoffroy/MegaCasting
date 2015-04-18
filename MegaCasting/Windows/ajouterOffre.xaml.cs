using MegaCasting.DB;
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
    /// Logique d'interaction pour ajouterOffre.xaml
    /// </summary>
    public partial class ajouterOffre : Window
    {
        private List<Type_Contrat> _typesContrats = new List<Type_Contrat>();

        public List<Type_Contrat> TypesContrat
        {
            get { return _typesContrats; }
            set { _typesContrats = value; }
        }
        
        public ajouterOffre()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            App.dbContext.Type_Contrat.ToList().ForEach(
                    tc => TypesContrat.Add(tc)
                );
        }
    }
}
