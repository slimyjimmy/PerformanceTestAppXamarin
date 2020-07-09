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
        private Stopwatch Stopwatchy { get; set; }
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
            Stopwatchy.Stop();
            var data = e.Reading;
            TestResults.Add(new TestResult(Stopwatchy.Elapsed.TotalMilliseconds * 1000000, "Test finished successfully (X:" + data.Acceleration.X + ", Y: " + data.Acceleration.Y + ", Z: " + data.Acceleration.Z));
            if (--NumberOfIterationsLeft > 0)
            {
                Test();
            } else
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