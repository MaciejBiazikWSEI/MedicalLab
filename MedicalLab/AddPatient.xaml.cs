using MedicalLab.Data;
using MedicalLab.Models;
using System;
using System.Windows;

namespace MedicalLab
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
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
                if (DatePickerBirth.SelectedDate is null)
                    return;

                var patient = code == 0 ? new Patient() : context.Patients.Find(code);

                patient.FirstName = TextBoxFirstName.Text;
                patient.LastName = TextBoxLastName.Text;
                patient.DateOfBirth = (DateTime)DatePickerBirth.SelectedDate;

                if (code == 0)
                    context.Patients.Add(patient);

                try
                {
                    context.SaveChanges();

                    DialogResult = true;
                    Close();
                }
                catch
                {
                    // Do nothing
                }
            }
        }
    }
}
