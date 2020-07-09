using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using XamarinPerformanceTest.DependencyServices;

namespace XamarinPerformanceTest.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsTest : ContentPage
    {
        private Stopwatch Stopwatchy { get; set; }
        private ObservableCollection<TestResult> TestResults { get; set; }
        private int NumberOfIterations { get; set; }
        private int NumberOfIterationsLeft { get; set; }
        public ContactsTest()
        {
            InitializeComponent();
            TestResults = new ObservableCollection<TestResult>();
            testResultsListView.ItemsSource = TestResults;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var status = await Permissions.CheckStatusAsync<Permissions.ContactsWrite>();
            if (status != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.ContactsWrite>();
            }
        }

        protected void OnClickedStartBenchmark(object sender, EventArgs e)
        {
            Stopwatchy = new Stopwatch();
            NumberOfIterations = int.Parse(Editor_NumberOfIterations.Text);
            NumberOfIterationsLeft = NumberOfIterations;
            TestResults.Clear();
            Test();
        }

        private void Test()
        {
            Stopwatchy = new Stopwatch();
            Stopwatchy.Start();
            IWriteContactService writeContactService = DependencyService.Get<IWriteContactService>();
            var random = new Random();
            Stopwatchy.Start();
            var result = writeContactService.WriteContact(new Contact("xamarin", "App", random.Next(1000).ToString()));
            Stopwatchy.Stop();
            TestResults.Add(new TestResult(Stopwatchy.Elapsed.TotalMilliseconds * 1000000, "Test finished successfully"));
            if (--NumberOfIterationsLeft > 0)
            {
                Test();
            }
            else
            {
                var durationSum = 0.0;
                foreach (var testResult in TestResults)
                {
                    durationSum += testResult.Duration;
                }
                var durationAvg = durationSum / TestResults.Count;
                TestResults.Add(new TestResult(durationAvg, "(AVERAGE) ALL TESTS FINISHED"));
            }
        }
    }
}