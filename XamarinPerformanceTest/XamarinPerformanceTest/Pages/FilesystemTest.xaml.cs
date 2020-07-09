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
    public partial class FilesystemTest : ContentPage
    {
        public FilesystemTest()
        {
            InitializeComponent();
        }

        protected void OnClickedStartBenchmark(object sender, EventArgs e)
        {

        }
    }
}