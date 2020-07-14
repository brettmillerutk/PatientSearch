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


namespace PatientSearch.PatientSearchUI
{
    /// <summary>
    /// Interaction logic for PatientSearch.xaml
    /// </summary>
    public partial class PatientSearchView : UserControl
    {
        public PatientSearchView()
        {
            InitializeComponent();
            this.DataContext = new PatientSearchViewModel();
        }
    }
}
