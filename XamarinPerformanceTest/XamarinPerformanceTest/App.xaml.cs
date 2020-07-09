using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPerformanceTest.Pages;

namespace XamarinPerformanceTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Index());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
