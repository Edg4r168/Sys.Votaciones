using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVotaciones.DAL;
using SysVotaciones.EN;

namespace SysVotaciones.BLL
{
    public class AdminBLL
    {
        private readonly AdminDAL _adminDAL;

        public AdminBLL(string conn)
        {
            _adminDAL = new AdminDAL(conn);
        }

        public LoginResult Login(Admin admin)
        {
            var response = new LoginResult();

            bool isValid = Helper.AllPropertiesHaveValue(admin);
            if (!isValid) return response;

            bool existsAdmin = _adminDAL.Login(admin);
            if (!existsAdmin) return response;

            string token = Helper.GenerateToken(admin.User, "admin");

            response.Logged = true;
            response.Token = token;

            return response;
        }
    }
}
