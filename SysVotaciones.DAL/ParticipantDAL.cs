using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVotaciones.EN;

namespace SysVotaciones.DAL;

public class ParticipantDAL
{
    private readonly SqlConnection _connection;

    public ParticipantDAL(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
    }

    public List<Participant> GetParticipants(int offset = 0, int amount = 0)
    {
        try
        {
            string query = "SELECT p.*, c.NOMBRE " +
                            "FROM PARTICIPANTE p " +
                            "JOIN CONCURSO c ON p.ID_CONCURSO = c.ID " +
                            "ORDER BY ID DESC OFFSET @offset ROWS " +
                            "FETCH NEXT @amount ROWS ONLY;";

            var cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("offset", offset);
            cmd.Parameters.AddWithValue("amount", amount == 0 ? 6 : amount);

            _connection.Open();

            var reader = cmd.ExecuteReader();
            List<Participant> list = [];

            while (reader.Read())
            {
                list.Add(new Participant()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    LastName = reader.GetString(2),
                    StudentCode = reader.GetString(3),
                    oContest = new Contest()
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

    public int GetTotalParticipants()
    {
        try
        {
            var cmd = new SqlCommand("SELECT TOTAL FROM TOTAL_PARTICIPANTES", _connection);

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

    public Participant? GetParticipant(int id)
    {
        try
        {
            SqlCommand cmd = new("SELECT p.*, c.NOMBRE " +
                                    "FROM PARTICIPANTE p " +
                                    "JOIN CONCURSO c ON p.ID_CONCURSO = c.ID " +
                                    "WHERE p.ID = @id", _connection);

            cmd.Parameters.AddWithValue("id", id);

            _connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            Participant? participant = null;

            if (reader.Read())
            {
                participant = new Participant()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    LastName = reader.GetString(2),
                    StudentCode = reader.GetString(3),
                    oContest = new Contest()
                    {
                        Id = reader.GetInt32(4),
                        Name = reader.GetString(5),
                    }
                };
            }

            return participant;
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

    public int SaveParticipant(Participant participant)
    {
        try
        {
            _connection.Open();
            var cmd = new SqlCommand("", _connection);

            // Validar que el ID del concurso existe
            cmd.CommandText = "SELECT COUNT(ID) AS Amount FROM CONCURSO WHERE ID = @id;";
            cmd.Parameters.AddWithValue("id", participant.ContestId);

            int contestCount = (int)cmd.ExecuteScalar();

            if (contestCount == 0) return 0;

            string query = "INSERT INTO PARTICIPANTE (NOMBRE, APELLIDO, CODIGO, ID_CONCURSO) " +
                                            "VALUES (@name, @lastName, @code, @costestId); SELECT SCOPE_IDENTITY() AS ID;";

            cmd.Parameters.Clear();
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("name", participant.Name);
            cmd.Parameters.AddWithValue("lastName", participant.LastName);
            cmd.Parameters.AddWithValue("code", participant.StudentCode);
            cmd.Parameters.AddWithValue("costestId", participant.ContestId);


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

    public int DeleteParticipant(int id)
    {
        try
        {
            SqlCommand cmd = new("DELETE PARTICIPANTE WHERE ID = @id", _connection);
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

    public int UpdateParticipant(Participant participant)
    {
        try
        {
            _connection.Open();
            var cmd = new SqlCommand("", _connection);

            // Validar que el ID del concurso existe
            cmd.CommandText = "SELECT COUNT(ID) AS Amount FROM CONCURSO WHERE ID = @id;";
            cmd.Parameters.AddWithValue("id", participant.ContestId);

            int contestCount = (int)cmd.ExecuteScalar();

            if (contestCount == 0) return 0;

            cmd.Parameters.Clear();
            cmd.CommandText = "sp_UpdateParticipant";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("id", participant.Id);

            cmd.Parameters.AddWithValue("name",
                string.IsNullOrWhiteSpace(participant.Name)
                ? DBNull.Value
                : participant.Name);

            cmd.Parameters.AddWithValue("lastName",
                string.IsNullOrWhiteSpace(participant.LastName)
                ? DBNull.Value
                : participant.LastName);

            cmd.Parameters.AddWithValue("code",
                string.IsNullOrWhiteSpace(participant.StudentCode)
                ? DBNull.Value
                : participant.StudentCode);

            cmd.Parameters.AddWithValue("constestId",
                participant.ContestId == default
                ? DBNull.Value
                : participant.ContestId);


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

    public List<Participant> SearchParticipants(string keyword)
    {
        try
        {
            string[] fullName = keyword.Split(' ');
            string name = fullName[0];
            string lastName = "";

            string query = "SELECT p.*, c.NOMBRE " +
                            "FROM PARTICIPANTE p " +
                            "JOIN CONCURSO c ON p.ID_CONCURSO = c.ID " +
                            "WHERE p.NOMBRE LIKE @name + '%' ";
                                    

            bool hasLastName = fullName.Length > 1;

            if (hasLastName)
            {
                lastName = fullName[1];
                query += "AND p.APELLIDO LIKE @lastName + '%'";
            }

            var cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("name", name);
            if (hasLastName) cmd.Parameters.AddWithValue("lastName", lastName);
            

            _connection.Open();

            var reader = cmd.ExecuteReader();
            List<Participant> list = [];

            while (reader.Read())
            {
                list.Add(new Participant()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    LastName = reader.GetString(2),
                    StudentCode = reader.GetString(3),
                    oContest = new Contest()
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

