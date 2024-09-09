using MedsoftTask1.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedsoftTask1.Services
{
    internal class GenderService
    {
        private readonly GenderRepository _genderRepo;

        public GenderService(GenderRepository genderRepo)
        {
            _genderRepo = genderRepo;
        }

        public DataTable GetGenders()
        {
            return _genderRepo.GetAllGenders();
        }
    }
}
