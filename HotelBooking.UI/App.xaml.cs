using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Services;
using System.ComponentModel;
using HotelBooking.DataAcces;
using HotelBooking.UI.Views;
using Prism.Regions;
using HotelBooking.DataAcces.Services;
using HotelBooking.UI.ViewModels;

namespace HotelBooking.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<HotelBookingDbContext>();
            containerRegistry.RegisterScoped<IUserDataService, UserDataService>();
            containerRegistry.RegisterScoped<IHotelDataService, HotelDataService>();
            containerRegistry.RegisterScoped<IRoomDataService, RoomDataService>();
            containerRegistry.RegisterScoped<IBookingDataService, BookingDataService>();

            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.RegisterForNavigation<HotelView>();
            containerRegistry.RegisterForNavigation<RoomView>();
            containerRegistry.RegisterForNavigation<BookingView>();
            containerRegistry.RegisterForNavigation<UserDetailsView>();
            containerRegistry.RegisterForNavigation<RegisterView>();
            containerRegistry.RegisterForNavigation<MessageView>();
            containerRegistry.RegisterForNavigation<EditBookingView>();
            // register other needed services here
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }


        protected override void OnInitialized()
        {
            base.OnInitialized();
            var regionManager = Container.Resolve<IRegionManager>();
            var contentRegion = regionManager.Regions["ContentRegion"];
            var LoginView = Container.Resolve<LoginView>();
            var hotelView = Container.Resolve<HotelView>();
            var roomView = Container.Resolve<RoomView>();
            var bookingView = Container.Resolve<BookingView>();
            var userDetailsView = Container.Resolve<UserDetailsView>();
            var editBookingView = Container.Resolve<EditBookingView>();

            contentRegion.Add(LoginView);
            contentRegion.Add(hotelView);
            contentRegion.Add(roomView);
            contentRegion.Add(bookingView);
            contentRegion.Add(userDetailsView);
            contentRegion.Add(editBookingView);


        }
    }
}
