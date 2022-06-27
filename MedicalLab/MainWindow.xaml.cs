using MedicalLab.Data;
using MedicalLab.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MedicalLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly MedicalLabContext context = new MedicalLabContext();
        private CollectionViewSource testerViewSource;
        private CollectionViewSource patientViewSource;
        private CollectionViewSource testViewSource;

        public MainWindow()
        {
            InitializeComponent();
            patientViewSource = (CollectionViewSource)FindResource(nameof(patientViewSource));
            testerViewSource = (CollectionViewSource)FindResource(nameof(testerViewSource));
            testViewSource = (CollectionViewSource)FindResource(nameof(testViewSource));
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshPatients();

            context.Testers.Load();

            var dummyTester = new Tester();
            dummyTester.Id = 0;

            var testers = context.Testers.Local.ToObservableCollection();
            testers.Add(dummyTester);

            testerViewSource.Source = testers.OrderBy(x => x.Id);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            context.Dispose();
            base.OnClosing(e);
        }

        #region TesterButtons
        private void ButtonAddTester_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddTester();
            if(window.ShowDialog() == true)
            {
                context.Testers.Load();
                testerViewSource.Source = context.Testers.Local.ToObservableCollection().OrderBy(x => x.Id);
                // TODO: Auto select new?
            }
        }

        private void ButtonEditTester_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddTester((Tester)ComboBoxTesters.SelectedItem);
            if (window.ShowDialog() == true)
            {
                // TODO: Why doesn't refresh
                context.Testers.Load();
                testerViewSource.Source = context.Testers.Local.ToObservableCollection().OrderBy(x => x.Id);
                // TODO: Auto select edited?
            }
        }

        private void ButtonDeleteTester_Click(object sender, RoutedEventArgs e)
        {
        }
        #endregion

        #region PatientButtons
        private void ButtonAddPatient_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddPatient();
            if (window.ShowDialog() == true)
            {
                RefreshPatients();
                // TODO: Auto select new?
            }
        }

        private void ButtonEditPatient_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddPatient((Patient)DataGridPatients.SelectedItem);
            if (window.ShowDialog() == true)
            {
                // TODO: Why doesn't refresh
                RefreshPatients();
                // TODO: Auto select new?
            }
        }

        private void ButtonDeletePatient_Click(object sender, RoutedEventArgs e)
        {
        }
        #endregion

        #region SampleButtons
        private void ButtonAddSample_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonEditSample_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonDeleteSample_Click(object sender, RoutedEventArgs e)
        {
        }
        #endregion

        #region TestButtons
        private void ButtonAddTest_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonEditTest_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonDeleteTest_Click(object sender, RoutedEventArgs e)
        {
        }
        #endregion

        #region ResultButtons
        private void ButtonAddResult_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonEditResult_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonDeleteResult_Click(object sender, RoutedEventArgs e)
        {
        }
        #endregion

        #region SelectionChanged
        private void ComboBoxTesters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var isEnabled = ((Tester)ComboBoxTesters.SelectedItem).Id > 0;

            ButtonDeleteTester.IsEnabled = ButtonEditTester.IsEnabled = isEnabled;

            RefreshTests();
        }
        private void DataGridPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonDeletePatient.IsEnabled = ButtonEditPatient.IsEnabled = DataGridPatients.SelectedItem is not null;
        }

        private void DataGridSamples_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshTests();
        }
        #endregion

        #region Refresh
        private void RefreshPatients()
        {
            context.Patients.Load();
            patientViewSource.Source = context.Patients.Local.ToObservableCollection().OrderBy(x => x.Code);
        }

        private void RefreshTests()
        {
            context.Tests.Load();
            var selectedTesterId = ComboBoxTesters.SelectedItem is null ? 0 : ((Tester)ComboBoxTesters.SelectedItem).Id;
            var selectedSampleCode = DataGridSamples.SelectedItem is null ? 0 : ((Sample)DataGridSamples.SelectedItem).Code;
            var testCollection = context.Tests.Local.ToObservableCollection().OrderBy(x => x.Code).Where(x => x.SampleCode == selectedSampleCode);

            if (selectedTesterId > 0)
                testViewSource.Source = testCollection.Where(x => x.TesterId == selectedTesterId);
            else
                testViewSource.Source = testCollection;
        }
        #endregion
    }
}
