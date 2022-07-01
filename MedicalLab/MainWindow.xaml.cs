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
                ComboBoxTesters.SelectedItem = context.Testers.OrderByDescending(x => x.Id).First();
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
                ComboBoxTesters.SelectedItem = selected;
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
                DataGridPatients.SelectedItem = context.Patients.OrderByDescending(x => x.Code).First();
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
                DataGridPatients.SelectedItem = selected;
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
                DataGridSamples.SelectedItem = context.Samples.OrderByDescending(x => x.Code).First();
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
                DataGridSamples.SelectedItem = selected;
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
                DataGridTests.SelectedItem = context.Tests.OrderByDescending(x => x.Code).First();
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
                DataGridTests.SelectedItem = selected;
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
            var selected = (Tester)ComboBoxTesters.SelectedItem;
            var isEnabled = selected.Id > 0;

            ButtonEditTester.IsEnabled = isEnabled;
            ButtonDeleteTester.IsEnabled = isEnabled && (selected?.Tests.Count == 0);
            ButtonAddTest.IsEnabled = isEnabled && DataGridSamples.SelectedItem is not null;

            RefreshTests();
        }
        private void DataGridPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Patient)DataGridPatients.SelectedItem;
            var isEnabled = selected is not null;

            ButtonEditPatient.IsEnabled = ButtonAddSample.IsEnabled = isEnabled;
            ButtonDeletePatient.IsEnabled = isEnabled && (selected?.Samples.Count == 0);

            RefreshSamples();
        }

        private void DataGridSamples_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Sample)DataGridSamples.SelectedItem;
            var isEnabled = selected is not null;

            ButtonEditSample.IsEnabled = isEnabled;
            ButtonDeleteSample.IsEnabled = isEnabled && (selected?.Tests.Count == 0);
            ButtonAddTest.IsEnabled = isEnabled && ((Tester)ComboBoxTesters.SelectedItem).Id > 0;

            RefreshTests();
        }

        private void DataGridTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var isEnabled = DataGridTests.SelectedItem is not null;

            ButtonDeleteTest.IsEnabled = ButtonEditTest.IsEnabled = isEnabled;
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
