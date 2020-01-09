using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Repository.Interface;

namespace University.Repository
{
    public class UserTestRepository : IUserTestRepository
    {
        public int GetUserTestId()
        {
            return 1;
        }
    }
}
