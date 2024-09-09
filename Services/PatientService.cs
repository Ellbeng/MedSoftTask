using MedsoftTask1.DataAccess;
using MedsoftTask1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedsoftTask1.Services
{
    internal class PatientService
    {
        private readonly PatientRepository _patientRepo;

        public PatientService(PatientRepository patientRepo)
        {
            _patientRepo = patientRepo;
        }


        public DataTable GetPatients()
        {
            return _patientRepo.GetAllPatients();
        }

        public Patient GetPatientById(int patientId)
        {
            DataRow row = _patientRepo.GetPatientById(patientId);
            if (row == null) return null;

            return new Patient
            {
                ID = Convert.ToInt32(row["ID"]),
                FullName = row["FullName"].ToString(),
                Dob = Convert.ToDateTime(row["Dob"]),
                GenderID = Convert.ToInt32(row["GenderID"]),
                Phone = row["Phone"].ToString(),
                Address = row["Address"].ToString()
            };
        }

        public void AddPatient(Patient patient)
        {
            _patientRepo.AddPatient(patient);
        }

        public void UpdatePatient(Patient patient)
        {
            _patientRepo.UpdatePatient(patient);
        }

        public void DeletePatient(int patientId)
        {
            _patientRepo.DeletePatient(patientId);
        }
    }
}
