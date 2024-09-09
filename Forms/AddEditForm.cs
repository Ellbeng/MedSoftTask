using MedsoftTask1.Helpers;
using MedsoftTask1.Models;
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

namespace MedsoftTask1.Forms
{
    internal partial class AddEditForm : Form
    {
        private readonly PatientService _patientService;
        private readonly GenderService _genderService;
        private readonly int? _patientId;
        public AddEditForm(PatientService patientService, GenderService genderService, int? patientId)
        {
            _patientService = patientService;
            _genderService = genderService;
            _patientId = patientId;
            InitializeComponent();
            LoadGenders();

            if (_patientId.HasValue)
            {
                LoadPatientDetails(_patientId.Value);  // Load details for editing
            }
        }

        private void LoadGenders()
        {
            DataTable gendersTable = _genderService.GetGenders();
            cmbGender.DataSource = gendersTable;
            cmbGender.DisplayMember = "GenderName";
            cmbGender.ValueMember = "GenderID";
        }

        private void LoadPatientDetails(int patientId)
        {
            Patient patient = _patientService.GetPatientById(patientId);
            if (patient != null)
            {
                txtFullName.Text = patient.FullName;
                dtpDob.Value = patient.Dob;
                cmbGender.SelectedValue = patient.GenderID;
                txtPhone.Text = patient.Phone;
                txtAddress.Text = patient.Address;
            }
        }

        private void AddEditForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                Patient patient = new Patient
                {
                    FullName = txtFullName.Text.Trim(),
                    Dob = dtpDob.Value,
                    GenderID = (int)cmbGender.SelectedValue,
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                if (_patientId.HasValue)
                {
                    patient.ID = _patientId.Value;
                    _patientService.UpdatePatient(patient);  // Update existing patient
                }
                else
                {
                    _patientService.AddPatient(patient);  // Add new patient
                }

                this.DialogResult = DialogResult.OK;  // Close form after successful save
                this.Close();
            }
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("სახელის და გვარის შეყვანა აუცილებელია.");
                return false;
            }

            if (cmbGender.SelectedItem == null)
            {
                MessageBox.Show("სქესის შეყვანა აუცილებელია.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtPhone.Text) && !ValidationHelper.IsPhoneNumberValid(txtPhone.Text))
            {
                MessageBox.Show("არასწორი ნომერი. ნომრის პირველი ციფრი უნდა იყოს 5 და უნდა შედგებოდეს 9 ციფრისგან.");
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }
    }
}
