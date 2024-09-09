using MedsoftTask1.Forms;
using MedsoftTask1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedsoftTask1
{
    internal partial class Form1 : Form
    {
        private readonly PatientService _patientService;
        private readonly GenderService _genderService;

        public Form1(PatientService patientService, GenderService genderService)
        {
            _patientService = patientService;
            _genderService = genderService;
            InitializeComponent();
            LoadPatients();
        }

        private void LoadPatients()
        {
            DataTable patientsTable = _patientService.GetPatients();
            dataGridView1.DataSource = patientsTable;
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            AddEditForm addForm = new AddEditForm(_patientService, _genderService, null); 
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadPatients();  // Refresh the list after adding a new patient
            }

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedPatientId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);
                AddEditForm editForm = new AddEditForm(_patientService, _genderService, selectedPatientId);  // pass the selected patient ID
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadPatients();  // Refresh the list after editing a patient
                }
            }
            else
            {
                MessageBox.Show("აირჩიეთ პაციენტი რედაქტირებისთვის.");
            }

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedPatientId = (int)dataGridView1.CurrentRow.Cells["ID"].Value;
                var confirmation = MessageBox.Show("დარწმუნებული ხართ, რომ არჩეული პაციენტის წაშლა გსურთ?", "პაციენტის წაშლა", MessageBoxButtons.YesNo);

                if (confirmation == DialogResult.Yes)
                {
                    _patientService.DeletePatient(selectedPatientId);
                    LoadPatients();  // Refresh the list after deletion
                }
            }
            else
            {
                MessageBox.Show("აირჩიეთ პაციენტი წასაშლელად.");
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            LoadPatients();
        }
    }
}
