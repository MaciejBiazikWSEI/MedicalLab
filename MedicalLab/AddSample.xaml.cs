﻿using MedicalLab.Data;
using MedicalLab.Models;
using System;
using System.Windows;

namespace MedicalLab
{
    /// <summary>
    /// Interaction logic for AddSample.xaml
    /// </summary>
    public partial class AddSample : Window
    {
        private int code = 0;
        private int patientCode = 0;

        public AddSample(Patient patient)
        {
            InitializeComponent();
            patientCode = patient.Code;
        }

        public AddSample(Sample sample)
        {
            InitializeComponent();

            code = sample.Code;
            TextBoxComment.Text = sample.Comment;
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
                var sample = code == 0 ? new Sample() { PatientCode = patientCode } : context.Samples.Find(code);

                sample.Comment = TextBoxComment.Text;

                if (code == 0)
                    context.Samples.Add(sample);

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
