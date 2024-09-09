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
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }


        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Phone" && e.Value != null)
            {
                string phoneNumber = e.Value.ToString();
                if (phoneNumber.Length == 9) 
                {
                    e.Value = FormatPhoneNumber(phoneNumber);
                    e.FormattingApplied = true; 
                }
            }
        }

      
        private string FormatPhoneNumber(string phoneNumber)
        {
            return string.Format("{0}-{1}-{2}", phoneNumber.Substring(0, 3), phoneNumber.Substring(3, 3), phoneNumber.Substring(6, 3));
        }

        private void LoadPatients()
        {
            DataTable patientsTable = _patientService.GetPatients();
            dataGridView1.DataSource = patientsTable;

            dataGridView1.Columns["FullName"].HeaderText = "პაციენტის გვარი სახელი"; 
            dataGridView1.Columns["Dob"].HeaderText = "დაბადების თარიღი";
            dataGridView1.Columns["GenderName"].HeaderText = "სქესი"; 
            dataGridView1.Columns["Phone"].HeaderText = "მობილურის ნომერი";
            dataGridView1.Columns["Address"].HeaderText = "მისამართი";
           
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
                AddEditForm editForm = new AddEditForm(_patientService, _genderService, selectedPatientId);  
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadPatients(); 
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
                    LoadPatients();  
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
