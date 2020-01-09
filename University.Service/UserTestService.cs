using University.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Service.Interface;
using University.Repository.Interface;

namespace University.Service
{
    public class UserTestService: IUserTestService
    {
        private IUserTestRepository _userTestRepository;
        public UserTestService(IUserTestRepository userTestRepository)
        {
            _userTestRepository = userTestRepository;
        }

        public int GetUserTestId()
        {
           return _userTestRepository.GetUserTestId();
        }
    }
}
