using HotelBooking.Model.Model;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace HotelBooking.UI.ViewModels
{
    public interface ILoginViewModel
    {
        string Email { get; set; }
        DelegateCommand LoginCommand { get; set; }
        string PassWord { get; set; }
        ObservableCollection<User> Users { get; set; }

        User GetUserById(int id);
        Task LoadUsers();
        int LoginCheck();
    }
}