using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVotaciones.EN;

namespace SysVotaciones.DAL
{
    public class TypeContestDAL
    {
        private readonly SqlConnection _connection;

        public TypeContestDAL(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public List<TypeContest> GetTypesContests(int offset = 0, int amount = 0)
        {
            try
            {
                string query = "SELECT * FROM TIPOCONCURSO ORDER BY ID DESC OFFSET @offset ROWS " +
                                                        "FETCH NEXT @amount ROWS ONLY;";

                var cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("offset", offset);
                cmd.Parameters.AddWithValue("amount", amount == 0 ? 6 : amount);

                _connection.Open();

                var reader = cmd.ExecuteReader();
                List<TypeContest> list = [];

                while (reader.Read())
                {
                    list.Add(new TypeContest()
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

        public int GetTotalTypesContests()
        {
            try
            {
                var cmd = new SqlCommand("SELECT TOTAL FROM TOTAL_TIPOSCONCURSO", _connection);

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

        public TypeContest? GetTypeContest(int id)
        {
            try
            {
                SqlCommand cmd = new("SELECT * FROM TIPOCONCURSO WHERE ID = @id", _connection);
                cmd.Parameters.AddWithValue("id", id);

                _connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                TypeContest? typeContest = null;

                if (reader.Read())
                {
                    typeContest = new TypeContest()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }

                return typeContest;
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

        public int SaveTypeContest(TypeContest typeContest)
        {
            try
            {
                SqlCommand cmd = new("INSERT INTO TIPOCONCURSO (NOMBRE) VALUES (@typeContest); SELECT SCOPE_IDENTITY() AS ID;", _connection);
                cmd.Parameters.AddWithValue("typeContest", typeContest.Name);

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

        public int DeleteTypeContest(int id)
        {
            try
            {
                SqlCommand cmd = new("DELETE TIPOCONCURSO WHERE ID = @id", _connection);
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

        public int UpdateTypeContest(TypeContest typeContest)
        {
            try
            {
                SqlCommand cmd = new("sp_UpdateTypeContest", _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id", typeContest.Id);
                cmd.Parameters.AddWithValue("name",
                    string.IsNullOrWhiteSpace(typeContest.Name)
                    ? DBNull.Value
                    : typeContest.Name);

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

        public List<TypeContest> SearchTypesContests(string keyword)
        {
            try
            {
                var cmd = new SqlCommand("SELECT * FROM TIPOCONCURSO WHERE NOMBRE LIKE @keyword + '%'", _connection);
                cmd.Parameters.AddWithValue("keyword", keyword);

                _connection.Open();

                var reader = cmd.ExecuteReader();
                List<TypeContest> list = [];

                while (reader.Read())
                {
                    list.Add(new TypeContest()
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
