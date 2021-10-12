using HotelBooking.Model.Model;
using HotelBooking.UI.Events;
using Prism.Events;
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

namespace HotelBooking.UI.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            eventAggregator.GetEvent<LoginEvent>().Subscribe(Recieved);
        }

        private void Recieved(User obj)
        {
            PWBox.Clear();
        }
    }
}
