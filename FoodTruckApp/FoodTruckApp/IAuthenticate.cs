using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckApp
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync();

        Task<bool> LogoutAsync();
    }
}
