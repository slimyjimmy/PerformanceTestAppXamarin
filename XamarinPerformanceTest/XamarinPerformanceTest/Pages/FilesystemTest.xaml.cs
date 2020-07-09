using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
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
    public partial class FilesystemTest : ContentPage
    {
        private Stopwatch Stopwatchy { get; set; }
        private ObservableCollection<TestResult> TestResults { get; set; }
        private int NumberOfIterations { get; set; }
        private int NumberOfIterationsLeft { get; set; }
        private string PathToFile { get; set; }

        public FilesystemTest()
        {
            InitializeComponent();
            TestResults = new ObservableCollection<TestResult>();
            testResultsListView.ItemsSource = TestResults;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var statusReadStorage = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            var statusWriteStorage = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (statusReadStorage != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.StorageRead>();
            }
            if (statusWriteStorage != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
        }

        protected void OnClickedStartBenchmark(object sender, EventArgs e)
        {
            Stopwatchy = new Stopwatch();
            NumberOfIterations = int.Parse(Editor_NumberOfIterations.Text);
            NumberOfIterationsLeft = NumberOfIterations;
            SavePictureToFilesystem();
            TestResults.Clear();
            Test();
        }

        private void SavePictureToFilesystem()
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var bytes = Convert.FromBase64String(Base64.encodedFile);
            PathToFile = Path.Combine(directory, "image.png");
            File.WriteAllBytes(PathToFile, bytes);
        }

        private void ReadBytesFromFileSystem()
        {
            var bytes = File.ReadAllBytes(PathToFile);
        }

        private void Test()
        {
            Stopwatchy = new Stopwatch();
            Stopwatchy.Start();
            ReadBytesFromFileSystem();
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