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
    public class ContestDAL
    {
        private readonly SqlConnection _connection;

        public ContestDAL(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public List<Contest> GetContests(int offset = 0, int amount = 0)
        {
            try
            {
                string query = "SELECT con.*, tCon.NOMBRE " +
                                    "FROM CONCURSO con " +
                                    "JOIN TIPOCONCURSO tCon ON con.ID_TIPOCONCURSO = tCon.ID " +
                                    "ORDER BY con.ID DESC OFFSET @offset ROWS " +
                                    "FETCH NEXT @amount ROWS ONLY;";

                SqlCommand cmd = new(query, _connection);
                cmd.Parameters.AddWithValue("offset", offset);
                cmd.Parameters.AddWithValue("amount", amount == 0 ? 6 : amount);

                _connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<Contest> list = [];


                while (reader.Read())
                {
                    list.Add(new Contest()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        State = Convert.ToInt32(reader.GetBoolean(3)),
                        oTypeContest = new TypeContest() { 
                            Id  = reader.GetInt32(4),
                            Name = reader.GetString(5),
                        }
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

        public int GetTotalContests()
        {
            try
            {
                var cmd = new SqlCommand("SELECT TOTAL FROM TOTAL_CONCURSOS", _connection);

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

        public Contest? GetContest(int id)
        {
            try
            {
                string query = "SELECT con.*, tcon.NOMBRE " +
                                    "FROM CONCURSO con " +
                                    "JOIN TIPOCONCURSO tcon ON con.ID_TIPOCONCURSO = tcon.ID " +
                                    "WHERE con.ID = @id;";

                SqlCommand cmd = new(query, _connection);
                cmd.Parameters.AddWithValue("id", id);

                _connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                Contest? contest = null;

                if (reader.Read())
                {
                    contest = new Contest()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        State = Convert.ToInt32(reader.GetBoolean(3)),
                        oTypeContest = new TypeContest()
                        {
                            Id = reader.GetInt32(4),
                            Name = reader.GetString(5),
                        }
                    };
                }

                return contest;
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

        public int SaveContest(Contest contest)
        {
            try
            {
                _connection.Open();
                var cmd = new SqlCommand("", _connection);

                // Validar que el ID del tipo de cuncurso existe
                cmd.CommandText = "SELECT COUNT(ID) AS Amount FROM TIPOCONCURSO WHERE ID = @id";
                cmd.Parameters.AddWithValue("id", contest.TypeContestId);

                int count = (int)cmd.ExecuteScalar();

                if (count == 0) return 0;

                cmd.Parameters.Clear();

                string query = "INSERT INTO CONCURSO (NOMBRE, DESCRIPCION, ESTADO, ID_TIPOCONCURSO) " +
                                            "VALUES (@name, @description, @state, @idTypeContest); SELECT SCOPE_IDENTITY() AS ID;";

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("name", contest.Name);
                cmd.Parameters.AddWithValue("description", contest.Description);
                cmd.Parameters.AddWithValue("state", contest.State);
                cmd.Parameters.AddWithValue("idTypeContest", contest.TypeContestId);


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

        public int DeleteContest(int id)
        {
            try
            {
                SqlCommand cmd = new("DELETE CONCURSO WHERE ID = @id", _connection);
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

        public int UpdateContest(Contest contest)
        {
            try
            {
                _connection.Open();
                var cmd = new SqlCommand("", _connection);

                if (contest.TypeContestId != default)
                {
                    // Validar que el ID del tipo de cuncurso existe
                    cmd.CommandText = "SELECT COUNT(ID) AS Amount FROM TIPOCONCURSO WHERE ID = @id";
                    cmd.Parameters.AddWithValue("id", contest.TypeContestId);

                    int count = (int)cmd.ExecuteScalar();

                    if (count == 0) return 0;

                    cmd.Parameters.Clear();
                }

                cmd.CommandText = "sp_UpdateContest";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id", contest.Id);
                cmd.Parameters.AddWithValue("name",
                    string.IsNullOrWhiteSpace(contest.Name)
                    ? DBNull.Value
                    : contest.Name);

                cmd.Parameters.AddWithValue("description",
                    string.IsNullOrWhiteSpace(contest.Description)
                    ? DBNull.Value
                    : contest.Description);

                cmd.Parameters.AddWithValue("state", contest.State);

                cmd.Parameters.AddWithValue("typeContestId",
                    contest.TypeContestId == default
                    ? DBNull.Value
                    : contest.TypeContestId);


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

        public List<Contest> SearchContests(string keyword)
        {
            try
            {
                string query = "SELECT con.*, tcon.NOMBRE " +
                                "FROM CONCURSO con " +
                                "JOIN TIPOCONCURSO tcon ON con.ID_TIPOCONCURSO = tcon.ID " +
                                "WHERE con.NOMBRE LIKE @keyword  + '%'";

                var cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("keyword", keyword);

                _connection.Open();

                var reader = cmd.ExecuteReader();
                List<Contest> list = [];

                while (reader.Read())
                {
                    list.Add(new Contest()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        State = Convert.ToInt32(reader.GetBoolean(3)),
                        oTypeContest = new TypeContest()
                        {
                            Id = reader.GetInt32(4),
                            Name = reader.GetString(5),
                        }
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
