using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedsoftTask1.DataAccess
{
    public interface IGenderRepository
    {
        DataTable GetAllGenders();
    }
    internal class GenderRepository : IGenderRepository
    {

        private readonly DatabaseConnection _dbConnection;

        public GenderRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public DataTable GetAllGenders()
        {
            using (SqlConnection conn = _dbConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllGenders", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtGenders = new DataTable();
                da.Fill(dtGenders);

                return dtGenders;
            }
        }


    }
}
