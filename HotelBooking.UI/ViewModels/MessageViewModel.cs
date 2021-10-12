using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.UI.ViewModels
{
    public class MessageViewModel : BindableBase, INavigationAware
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }


        public MessageViewModel()
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Message = navigationContext.Parameters.GetValue<string>("message");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
