using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FoodTruckApp
{
    partial class Authenticator: IAuthenticate
    {
        partial Task<bool> AuthenticateAsync();

        partial Task<bool> LogoutAsync();

    }
}
