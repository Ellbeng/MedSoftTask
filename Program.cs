using MedsoftTask1.DataAccess;
using MedsoftTask1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedsoftTask1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var dbContext = new DatabaseConnection();
            var patientRepository = new PatientRepository(dbContext);
            var genderRepository = new GenderRepository(dbContext);

            var patientService = new PatientService(patientRepository);
            var genderService = new GenderService(genderRepository);





            Application.Run(new Form1(patientService, genderService));
        }
    }
}
