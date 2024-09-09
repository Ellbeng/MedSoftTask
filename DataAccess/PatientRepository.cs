using MedsoftTask1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedsoftTask1.DataAccess
{
    public interface IPatientRepository
    {
        DataTable GetAllPatients();
        DataRow GetPatientById(int patientId);
        void AddPatient(Patient patient);
        void UpdatePatient(Patient patient);

        void DeletePatient(int patientId);
    }

    internal class PatientRepository : IPatientRepository
    {
        private readonly DatabaseConnection _dbContext;

        public PatientRepository(DatabaseConnection dbConnection)
        {
            _dbContext = dbConnection;
        }

        public void AddPatient(Patient patient)
        {
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("AddPatient", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FullName", patient.FullName);
                cmd.Parameters.AddWithValue("@Dob", patient.Dob);
                cmd.Parameters.AddWithValue("@GenderId", patient.GenderID);
                cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(patient.Phone) ? (object)DBNull.Value : patient.Phone);
                cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(patient.Address) ? (object)DBNull.Value : patient.Address);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePatient(Patient patient)
        {
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UpdatePatient", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", patient.ID);
                cmd.Parameters.AddWithValue("@FullName", patient.FullName);
                cmd.Parameters.AddWithValue("@Dob", patient.Dob);
                cmd.Parameters.AddWithValue("@GenderId", patient.GenderID);
                cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(patient.Phone) ? (object)DBNull.Value : patient.Phone);
                cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(patient.Address) ? (object)DBNull.Value : patient.Address);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePatient(int id)
        {
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DeletePatient", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetAllPatients()
        {
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllPatients", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtPatients = new DataTable();
                da.Fill(dtPatients);

                return dtPatients;
            }
        }

        public DataRow GetPatientById(int patientId)
        {
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_GetPatientById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatientId", patientId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtPatient = new DataTable();
                da.Fill(dtPatient);

                return dtPatient.Rows.Count > 0 ? dtPatient.Rows[0] : null;
            }
        }
    }
}
