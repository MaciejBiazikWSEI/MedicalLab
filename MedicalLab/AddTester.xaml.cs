using MedicalLab.Data;
using MedicalLab.Models;
using System;
using System.Windows;

namespace MedicalLab
{
    /// <summary>
    /// Interaction logic for AddTester.xaml
    /// </summary>
    public partial class AddTester : Window
    {
        private int id = 0;

        public AddTester()
        {
            InitializeComponent();
        }

        public AddTester(Tester tester)
        {
            InitializeComponent();

            id = tester.Id;
            TextBoxFirstName.Text = tester.FirstName;
            TextBoxLastName.Text = tester.LastName;
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
                var tester = id == 0 ? new Tester() : context.Testers.Find(id);
            
                tester.FirstName = TextBoxFirstName.Text;
                tester.LastName = TextBoxLastName.Text;

                if (id == 0)
                    context.Testers.Add(tester);

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
