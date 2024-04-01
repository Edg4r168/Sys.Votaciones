using System.Data.SqlClient;
using System.Data;
using SysVotaciones.EN;

namespace SysVotaciones.DAL
{
    public class YearDAL
    {
        private readonly SqlConnection _connection;

        public YearDAL(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public List<Year> GetYears(int offset = 0, int amount = 0)
        {
            try
            {
                string query = "SELECT * FROM AÑO ORDER BY ID DESC OFFSET @offset ROWS " +
                                                                    "FETCH NEXT @amount ROWS ONLY;";
                SqlCommand cmd = new(query, _connection);
                cmd.Parameters.AddWithValue("offset", offset);
                cmd.Parameters.AddWithValue("amount", amount == 0 ? 6 : amount);

                _connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<Year> list = [];


                while (reader.Read())
                {
                    list.Add(new Year()
                    {
                        Id = reader.GetInt32(0),
                        CareerYear = reader.GetString(1),
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

        public int GetTotalYears()
        {
            try
            {
                var cmd = new SqlCommand("SELECT TOTAL FROM TOTAL_AÑOS", _connection);

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

        public Year? GetYear(int id)
        {
            try
            {
                SqlCommand cmd = new("SELECT * FROM AÑO WHERE ID = @id", _connection);
                cmd.Parameters.AddWithValue("id", id);

                _connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                Year? year = null;

                if (reader.Read())
                {
                    year = new Year()
                    {
                        Id = reader.GetInt32(0),
                        CareerYear = reader.GetString(1)
                    };
                }

                return year;
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

        public int SaveYear(Year year)
        {
            try
            {
                SqlCommand cmd = new("INSERT INTO AÑO (AÑO_CARRERA) VALUES (@careerYear); SELECT SCOPE_IDENTITY() AS ID;", _connection);
                cmd.Parameters.AddWithValue("careerYear", year.CareerYear);

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

        public int DeleteYear(int id) 
        {
            try
            {
                SqlCommand cmd = new("DELETE AÑO WHERE ID = @id", _connection);
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

        public int UpdateYear(Year year)
        {
            try
            {
                SqlCommand cmd = new("sp_UpdateYear", _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id", year.Id);
                cmd.Parameters.AddWithValue("careerYear",
                    string.IsNullOrWhiteSpace(year.CareerYear)
                    ? DBNull.Value
                    : year.CareerYear);

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

        public List<Year> SearchYears(string keyword)
        {
            try
            {
                var cmd = new SqlCommand("SELECT * FROM AÑO WHERE AÑO_CARRERA LIKE @keyword + '%'", _connection);
                cmd.Parameters.AddWithValue("keyword", keyword);

                _connection.Open();

                var reader = cmd.ExecuteReader();
                List<Year> list = [];

                while (reader.Read())
                {
                    list.Add(new Year()
                    {
                        Id = reader.GetInt32(0),
                        CareerYear = reader.GetString(1),
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
