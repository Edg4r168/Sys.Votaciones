using System.Data;
using System.Data.SqlClient;
using SysVotaciones.EN;

namespace SysVotaciones.DAL
{
    public class CareerDAL
    {
        private readonly SqlConnection _connection;

        public CareerDAL(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public List<Career> GetCareers()
        {
            try
            {
                SqlCommand cmd = new("SELECT * FROM CARRERA", _connection);

                _connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<Career> list = [];


                while (reader.Read())
                {
                    list.Add(new Career()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }

                return list;
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

        public Career? GetCareer(int id)
        {
            try
            {
                SqlCommand cmd = new("SELECT * FROM CARRERA WHERE ID = @id", _connection);
                cmd.Parameters.AddWithValue("id", id);

                _connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                Career? career = null;

                if (reader.Read())
                {
                    career = new Career()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }

                return career;
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

        public int SaveCareer(Career career)
        {
            try
            {
                SqlCommand cmd = new("INSERT INTO CARRERA (NOMBRE) VALUES (@career)", _connection);
                cmd.Parameters.AddWithValue("career", career.Name);

                _connection.Open();

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
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

        public int DeleteCareer(int id)
        {
            try
            {
                SqlCommand cmd = new("DELETE CARRERA WHERE ID = @id", _connection);
                cmd.Parameters.AddWithValue("id", id);

                _connection.Open();

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
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

        public int UpdateCareer(Career career)
        {
            try
            {
                SqlCommand cmd = new("sp_UpdateCareer", _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id", career.Id);
                cmd.Parameters.AddWithValue("name",
                    string.IsNullOrWhiteSpace(career.Name)
                    ? DBNull.Value
                    : career.Name);

                _connection.Open();

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
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
}
