using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinPerformanceTest.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Index : ContentPage
    {
        public Index()
        {
            InitializeComponent();
        }


        protected async void OnClickedAccelerometerButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccelerometerTest());
        }

        protected async void OnClickedContactsButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactsTest());
        }

        protected async void OnClickedGeolocationButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GeolocationTest());
        }

        protected async void OnClickedFilesystemButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FilesystemTest());
        }
    }
}