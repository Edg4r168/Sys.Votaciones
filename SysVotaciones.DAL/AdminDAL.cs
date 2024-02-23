using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVotaciones.EN;

namespace SysVotaciones.DAL;

public class AdminDAL
{
    private readonly SqlConnection _connection;

    public AdminDAL (string connectionString)
    {
        _connection = new SqlConnection(connectionString);
    }
    public bool Login(Admin admin)
    {
        try
        {
            string query = "SELECT COUNT(ID) AS Admin " +
                                "FROM ADMINISTRADOR WHERE USUARIO = @user AND CONTRASEÑA = @password;";

            var cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("user", admin.User);
            cmd.Parameters.AddWithValue("password", admin.Password);

            _connection.Open();

            int countAdmin = (int)cmd.ExecuteScalar();

            if (countAdmin > 0) return true;

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }
    }
   
}

    

