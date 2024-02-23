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

        /*public async Task<List<Year>> GetYearsAsync()
        {
            var cmd = await CommonDB.GetCommandAsync();
            //if (cmd == null) throw new Exception("Erro inesperado");

            cmd.CommandText = "SELECT * FROM AÑO";

            var reader = await CommonDB.ExecuteReaderAsync(cmd);
            List<Year> list = [];    

           if (reader != null)
           {
                while (await reader.ReadAsync())
                {
                    list.Add(new Year()
                    {
                        Id = reader.GetInt32(0),
                        CareerYear = reader.GetString(1),
                    });
                }
           }

           return list;
        }

        public async Task<Year> GetYearAsync(int id)
        {
            var cmd = await CommonDB.GetCommandAsync();
            cmd.CommandText = "SELECT * FROM AÑO WHERE ID = @id";
            cmd.Parameters.AddWithValue("id", id);

            var reader = await CommonDB.ExecuteReaderAsync(cmd);
            Year year = new();

            if (reader != null && await reader.ReadAsync())
            {
                year.Id = reader.GetInt32(0);
                year.CareerYear = reader.GetString(1);
            }

            return year;
        }

        public async Task<int> SaveYearAsync(Year year)
        {
            var cmd = await CommonDB.GetCommandAsync();
            cmd.CommandText = "INSERT INTO AÑO (AÑO_CARRERA) VALUES (@careerYear)";
            cmd.Parameters.AddWithValue("careerYear", year.CareerYear);

            int rowsAffected = await CommonDB.ExecuteCommandAsync(cmd);
            return rowsAffected;
        }*/

        public List<Year> GetYears()
        {
            //var cmd = CommonDB.GetCommand();
            //cmd.CommandText = "SELECT * FROM AÑO";
            //var reader = CommonDB.ExecuteDataReader(cmd);

            try
            {
                SqlCommand cmd = new("SELECT * FROM AÑO", _connection);

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

        public Year? GetYear(int id)
        {
            //var cmd = CommonDB.GetCommand();
            //cmd.CommandText = "SELECT * FROM AÑO WHERE ID = @id";
            //var reader = CommonDB.ExecuteDataReader(cmd);
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
            //var cmd = CommonDB.GetCommand();
            //cmd.CommandText = "INSERT INTO AÑO (AÑO_CARRERA) VALUES (@careerYear)";
            //cmd.Parameters.AddWithValue("careerYear", year.CareerYear);
            //int rowsAffected = CommonDB.ExecuteCommand(cmd);

            try
            {
                SqlCommand cmd = new("INSERT INTO AÑO (AÑO_CARRERA) VALUES (@careerYear)", _connection);
                cmd.Parameters.AddWithValue("careerYear", year.CareerYear);

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

        public int DeleteYear(int id) 
        {
            //var cmd = CommonDB.GetCommand();
            //cmd.CommandText = "DELETE AÑO WHERE ID = @id";
            //cmd.Parameters.AddWithValue("id", id);
            //int rowsAffected = CommonDB.ExecuteCommand(cmd);

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
            //var cmd = CommonDB.GetCommand();
            //cmd.CommandText = "sp_UpdateYear";
            //cmd.CommandType = CommandType.StoredProcedure;
            //int rowsAffected = CommonDB.ExecuteCommand(cmd);

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
    }
}
