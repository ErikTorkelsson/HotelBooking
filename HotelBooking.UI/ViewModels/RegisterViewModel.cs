using HotelBooking.DataAcces.Services;
using HotelBooking.Model.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace HotelBooking.UI.ViewModels
{
    public class RegisterViewModel : BindableBase
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;
        private readonly IUserDataService _service;
        private readonly IRegionManager _regionManager;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public DelegateCommand SaveUserCommand { get; set; }

        public RegisterViewModel(IUserDataService service, IRegionManager regionManager)
        {
            SaveUserCommand = new DelegateCommand(Execute, CanExecute);
            _service = service;
            _regionManager = regionManager;
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute()
        {
            Register();
        }

        public void Register()
        {
            int.TryParse(PhoneNumber, out int phoneNumber);

            if(phoneNumber > 0 && Password.Length > 0)
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(Password);

                User user = new User
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Address = Address,
                    PhoneNumber = phoneNumber,
                    Email = Email,
                    PassWord = passwordHash
                };

                _service.SaveUser(user);

                var p = new NavigationParameters();
                p.Add("message", $"Du är nu registrerad.\nLogga in för att fortsätta.");
                _regionManager.RequestNavigate("ContentRegion", "MessageView", p);
            }
            else
            {
                MessageBox.Show("Du har inte fyllt i fälten korrekt");
            }

            
        }
    }
}
