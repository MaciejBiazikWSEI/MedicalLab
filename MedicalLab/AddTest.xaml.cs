using MedicalLab.Data;
using MedicalLab.Models;
using System;
using System.Windows;

namespace MedicalLab
{
    /// <summary>
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTest : Window
    {
        private int code = 0;
        private int testerId = 0;
        private int sampleCode = 0;

        public AddTest(Tester tester, Sample sample)
        {
            InitializeComponent();
            testerId = tester.Id;
            sampleCode = sample.Code;
        }

        public AddTest(Test test)
        {
            InitializeComponent();

            code = test.Code;
            DatePickerFinished.SelectedDate = test.DateFinished;
            TextBoxComment.Text = test.Comment;
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
                var test = code == 0 ? new Test() { TesterId = testerId, SampleCode = sampleCode } : context.Tests.Find(code);

                test.DateFinished = DatePickerFinished.SelectedDate;
                test.Comment = TextBoxComment.Text;

                if (code == 0)
                    context.Tests.Add(test);

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
