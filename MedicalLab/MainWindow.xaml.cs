using MedicalLab.Data;
using MedicalLab.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

// Global TODOs: grey out buttons where necessary, auto select

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
        private CollectionViewSource sampleViewSource;
        private CollectionViewSource testViewSource;

        public MainWindow()
        {
            InitializeComponent();
            patientViewSource = (CollectionViewSource)FindResource(nameof(patientViewSource));
            testerViewSource = (CollectionViewSource)FindResource(nameof(testerViewSource));
            sampleViewSource = (CollectionViewSource)FindResource(nameof(sampleViewSource));
            testViewSource = (CollectionViewSource)FindResource(nameof(testViewSource));
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Add dummy tester as "all"
            context.Testers.Add(new Tester() { Id = 0 });

            RefreshTesters();
            RefreshPatients();
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
            if (window.ShowDialog() == true)
            {
                RefreshTesters();
                // TODO: Auto select new?
            }
        }

        private void ButtonEditTester_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Tester)ComboBoxTesters.SelectedItem;
            var window = new AddTester(selected);
            if (window.ShowDialog() == true)
            {
                context.Entry(selected).Reload();
                RefreshTesters();
                // TODO: Auto select edited?
            }
        }

        private void ButtonDeleteTester_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Tester)ComboBoxTesters.SelectedItem;
            using (var context2 = new MedicalLabContext())
            {
                // necessary to use a separate context to avoid saving the dummy tester
                context2.Remove(selected);
                context2.SaveChanges();
            }
            context.Entry(selected).Reload();
            RefreshTesters();
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
            var selected = (Patient)DataGridPatients.SelectedItem;
            var window = new AddPatient(selected);
            if (window.ShowDialog() == true)
            {
                context.Entry(selected).Reload();
                RefreshPatients();
                // TODO: Auto select new?
            }
        }

        private void ButtonDeletePatient_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Patient)DataGridPatients.SelectedItem;
            using (var context2 = new MedicalLabContext())
            {
                // necessary to use a separate context to avoid saving the dummy tester
                context2.Remove(selected);
                context2.SaveChanges();
            }
            context.Entry(selected).Reload();
            RefreshPatients();
        }
        #endregion

        #region SampleButtons
        private void ButtonAddSample_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddSample((Patient)DataGridPatients.SelectedItem);
            if (window.ShowDialog() == true)
            {
                RefreshSamples();
                // TODO: Auto select new?
            }
        }

        private void ButtonEditSample_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Sample)DataGridSamples.SelectedItem;
            var window = new AddSample(selected);
            if (window.ShowDialog() == true)
            {
                context.Entry(selected).Reload();
                RefreshSamples();
                // TODO: Auto select new?
            }
        }

        private void ButtonDeleteSample_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Sample)DataGridSamples.SelectedItem;
            using (var context2 = new MedicalLabContext())
            {
                // necessary to use a separate context to avoid saving the dummy tester
                context2.Remove(selected);
                context2.SaveChanges();
            }
            // TODO: Why crash?
            context.Entry(selected).Reload();
            RefreshSamples();
        }
        #endregion

        #region TestButtons
        private void ButtonAddTest_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddTest((Tester)ComboBoxTesters.SelectedItem, (Sample)DataGridSamples.SelectedItem);
            if (window.ShowDialog() == true)
            {
                RefreshTests();
                // TODO: Auto select new?
            }
        }

        private void ButtonEditTest_Click(object sender, RoutedEventArgs e)
        {

            var selected = (Test)DataGridTests.SelectedItem;
            var window = new AddTest(selected);
            if (window.ShowDialog() == true)
            {
                context.Entry(selected).Reload();
                RefreshTests();
                // TODO: Auto select new?
            }
        }

        private void ButtonDeleteTest_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Test)DataGridTests.SelectedItem;
            using (var context2 = new MedicalLabContext())
            {
                // necessary to use a separate context to avoid saving the dummy tester
                context2.Remove(selected);
                context2.SaveChanges();
            }
            context.Entry(selected).Reload();
            RefreshTests();
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

            RefreshSamples();
        }

        private void DataGridSamples_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshTests();
        }
        #endregion

        #region Refresh
        private void RefreshTesters()
        {
            context.Testers.Load();
            testerViewSource.Source = context.Testers.Local.ToObservableCollection().OrderBy(x => x.Id);
        }

        private void RefreshPatients()
        {
            context.Patients.Load();
            patientViewSource.Source = context.Patients.Local.ToObservableCollection().OrderBy(x => x.Code);
        }

        private void RefreshSamples()
        {
            context.Samples.Load();

            var selectedPatientCode = DataGridPatients.SelectedItem is null ? 0 : ((Patient)DataGridPatients.SelectedItem).Code;
            sampleViewSource.Source = context.Samples.Local.ToObservableCollection().OrderBy(x => x.Code).Where(x => x.PatientCode == selectedPatientCode);
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
