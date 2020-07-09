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

namespace XamarinPerformanceTest.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeolocationTest : ContentPage
    {
        private Stopwatch Stopwatchy { get; set; }
        private ObservableCollection<TestResult> TestResults { get; set; }
        private int NumberOfIterations { get; set; }
        private int NumberOfIterationsLeft { get; set; }
        public GeolocationTest()
        {
            InitializeComponent();
            TestResults = new ObservableCollection<TestResult>();
            testResultsListView.ItemsSource = TestResults;
        }

        protected void OnClickedStartBenchmark(object sender, EventArgs e)
        {
            Stopwatchy = new Stopwatch();
            NumberOfIterations = int.Parse(Editor_NumberOfIterations.Text);
            NumberOfIterationsLeft = NumberOfIterations;
            TestResults.Clear();
            Test();
        }

        private async Task AccessGeolocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request);
                Stopwatchy.Stop();
                if (location != null)
                {
                    TestResults.Add(new TestResult(Stopwatchy.Elapsed.TotalMilliseconds * 1000000, $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}"));
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
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        private void Test()
        {
            Stopwatchy = new Stopwatch();
            Stopwatchy.Start();
            AccessGeolocation();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var statusLocation = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (statusLocation != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
        }
    }
}