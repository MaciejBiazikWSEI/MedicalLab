using MedicalLab.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
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
        private readonly MedicalLabContext _context = new MedicalLabContext();
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
            _context.Patients.Load();
            _context.Testers.Load();
            patientViewSource.Source = _context.Patients.Local.ToObservableCollection();
            testerViewSource.Source = _context.Testers.Local.ToObservableCollection();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _context.Dispose();
            base.OnClosing(e);
        }
    }
}
