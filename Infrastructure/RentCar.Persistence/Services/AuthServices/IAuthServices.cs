using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Services.AuthServices
{
    public interface IAuthServices
    {
        public string GenerateToken(string id,string role);
    }
}
