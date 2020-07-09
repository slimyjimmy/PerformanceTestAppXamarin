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
    public partial class AccelerometerTest : ContentPage
    {
        private Stopwatch Stopwatch { get; set; }
        private ObservableCollection<TestResult> TestResults { get; set; }
        private int NumberOfIterations { get; set; }
        private int NumberOfIterationsLeft { get; set; }
        public AccelerometerTest()
        {
            InitializeComponent();
            TestResults = new ObservableCollection<TestResult>();
            testResultsListView.ItemsSource = TestResults;
        }

        protected void OnClickedStartBenchmark(object sender, EventArgs e)
        {
            Stopwatch = new Stopwatch();
            NumberOfIterations = int.Parse(Editor_NumberOfIterations.Text);
            NumberOfIterationsLeft = NumberOfIterations;
            TestResults.Clear();
            Test();
        }

        private void Test()
        {
            Stopwatch.StartNew();
            if (Accelerometer.IsMonitoring)
            {
                Accelerometer.Stop();
            }
            Accelerometer.Start(SensorSpeed.Fastest);
            Accelerometer.ReadingChanged += AccelerometerChanged;
        }

        private void AccelerometerChanged(object sender, AccelerometerChangedEventArgs e)
        {
            Accelerometer.ReadingChanged -= AccelerometerChanged;
            Stopwatch.Stop();
            var data = e.Reading;
            TestResults.Add(new TestResult(Stopwatch.ElapsedMilliseconds* 1000000, "Test finished successfully (X:" + data.Acceleration.X + ", Y: " + data.Acceleration.Y + ", Z: " + data.Acceleration.Z));
            if (--NumberOfIterationsLeft > 0)
            {
                Test();
            } else
            {

            }
        }
    }
}