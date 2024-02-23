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

        public List<Contest> GetContests()
        {
            try
            {
                string query = "SELECT con.*, cat.NOMBRE " +
                                    "FROM CONCURSO con " +
                                    "JOIN CATEGORIA cat ON con.ID_CATEGORIA = cat.ID;";

                SqlCommand cmd = new(query, _connection);

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
                        oCategory = new Category() { 
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

        public Contest? GetContest(int id)
        {
            try
            {
                string query = "SELECT con.*, cat.NOMBRE " +
                                    "FROM CONCURSO con " +
                                    "JOIN CATEGORIA cat ON con.ID_CATEGORIA = cat.ID " +
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
                        oCategory = new Category()
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
                SqlCommand cmd = new("INSERT INTO CONCURSO (NOMBRE, DESCRIPCION, ESTADO) VALUES (@name, @description, @state)", _connection);
                cmd.Parameters.AddWithValue("name", contest.Name);
                cmd.Parameters.AddWithValue("description", contest.Description);
                cmd.Parameters.AddWithValue("state", contest.State);

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
                SqlCommand cmd = new("sp_UpdateContest", _connection);
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
