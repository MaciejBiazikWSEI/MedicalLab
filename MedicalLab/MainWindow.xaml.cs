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

        public MainWindow()
        {
            InitializeComponent();
            patientViewSource = (CollectionViewSource)FindResource(nameof(patientViewSource));
            testerViewSource = (CollectionViewSource)FindResource(nameof(testerViewSource));
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.Patients.Load();
            patientViewSource.Source = context.Patients.Local.ToObservableCollection();

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

        private void ComboBoxTesters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var isEnabled = ((Tester)ComboBoxTesters.SelectedItem).Id > 0;
            
            ButtonDeleteTester.IsEnabled = ButtonEditTester.IsEnabled = isEnabled;
        }

        private void ButtonDeleteTester_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Why doesn't delete
            context.Testers.Remove((Tester)ComboBoxTesters.SelectedItem);
            context.SaveChanges();

            context.Testers.Load();
            testerViewSource.Source = context.Testers.Local.ToObservableCollection().OrderBy(x => x.Id);
        }
    }
}
