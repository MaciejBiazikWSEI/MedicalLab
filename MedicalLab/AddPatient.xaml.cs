using MedicalLab.Data;
using MedicalLab.Models;
using System;
using System.Windows;

namespace MedicalLab
{
    /// <summary>
    /// Interaction logic for AddTester.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        private int code = 0;

        public AddPatient()
        {
            InitializeComponent();
        }

        public AddPatient(Patient patient)
        {
            InitializeComponent();

            code = patient.Code;
            TextBoxFirstName.Text = patient.FirstName;
            TextBoxLastName.Text = patient.LastName;
            DatePickerBirth.SelectedDate = patient.DateOfBirth;
            TextBoxPesel.Text = patient.Pesel;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MedicalLabContext())
            {
                var patient = code == 0 ? new Patient() : context.Patients.Find(code);

                patient.FirstName = TextBoxFirstName.Text;
                patient.LastName = TextBoxLastName.Text;
                patient.DateOfBirth = DatePickerBirth.SelectedDate ?? DateTime.Today.AddDays(20); // if date null, ensure it's invalid (in the future) so an exception is triggered
                patient.Pesel = TextBoxPesel.Text;

                if (code == 0)
                    context.Patients.Add(patient);

                try
                {
                    context.SaveChanges();

                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    // TODO: Warning message?
                }
            }
        }
    }
}
