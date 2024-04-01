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

        public List<Career> GetCareers(int offset = 0, int amount = 0)
        {
            try
            {
                string query = "SELECT * FROM CARRERA ORDER BY ID DESC OFFSET @offset ROWS " +
                                                       "FETCH NEXT @amount ROWS ONLY;";

                SqlCommand cmd = new(query, _connection);
                cmd.Parameters.AddWithValue("offset", offset);
                cmd.Parameters.AddWithValue("amount", amount == 0 ? 6 : amount);

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

        public int GetTotalCareers()
        {
            try
            {
                var cmd = new SqlCommand("SELECT TOTAL FROM TOTAL_CARRERAS", _connection);

                _connection.Open();

                int total = (int)cmd.ExecuteScalar();

                return total;
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
                SqlCommand cmd = new("INSERT INTO CARRERA (NOMBRE) VALUES (@career); SELECT SCOPE_IDENTITY() AS ID;", _connection);
                cmd.Parameters.AddWithValue("career", career.Name);

                _connection.Open();

                int currentId = Convert.ToInt32(cmd.ExecuteScalar());
                return currentId;
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

        public List<Career> SearchCareers(string keyword)
        {
            try
            {
                var cmd = new SqlCommand("SELECT * FROM CARRERA WHERE NOMBRE LIKE @keyword + '%'", _connection);
                cmd.Parameters.AddWithValue("keyword", keyword);

                _connection.Open();

                var reader = cmd.ExecuteReader();
                List<Career> list = [];

                while (reader.Read())
                {
                    list.Add(new Career()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
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
    }
}
