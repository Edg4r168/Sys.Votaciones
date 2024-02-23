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
    public class StudentDAL
    {
        private readonly SqlConnection _connection;

        public StudentDAL(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public List<Student> GetStudents()
        {
            try
            {
                string query = "SELECT s.*, y.AÑO_CARRERA, c.NOMBRE " +
                                            "FROM ESTUDIANTE s " +
                                            "JOIN AÑO y ON s.ID_AÑO = y.ID " +
                                            "JOIN CARRERA c ON s.ID_CARRERA = c.ID;";

                SqlCommand cmd = new(query, _connection);

                _connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<Student> list = [];


                while (reader.Read())
                {
                    list.Add(new Student()
                    {
                        StudentCode = reader.GetString(0),
                        oCareerYear = new Year() { 
                            Id = reader.GetInt32(1),
                            CareerYear = reader.GetString(4) 
                        },
                        oCareer = new Career() { 
                            Id = reader.GetInt32(2), 
                            Name = reader.GetString(5)
                        },
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

        public Student? GetStudent(string studentCode)
        {
            try
            {
                string query = "SELECT s.*, y.AÑO_CARRERA, c.NOMBRE " +
                                        "FROM ESTUDIANTE s " +
                                        "JOIN AÑO y ON s.ID_AÑO = y.ID " +
                                        "JOIN CARRERA c ON s.ID_CARRERA = c.ID " +
                                        "WHERE s.CODIGO = @studentCode;";

                SqlCommand cmd = new(query, _connection);
                cmd.Parameters.AddWithValue("studentCode", studentCode);

                _connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                Student? student = null;

                if (reader.Read())
                {
                    student = new Student()
                    {
                        StudentCode = reader.GetString(0),
                        oCareerYear = new Year() {
                            Id = reader.GetInt32(1),
                            CareerYear= reader.GetString(4) 
                        },
                        oCareer = new Career() { 
                            Id = reader.GetInt32(2),
                            Name= reader.GetString(5) 
                        }
                    };
                }

                return student;
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

        public int SaveStudent(Student student)
        {
            try
            {
                _connection.Open();
                var cmd = new SqlCommand("", _connection);

                // Validar que el ID de la carrera existe
                cmd.CommandText = "SELECT COUNT(ID) AS Amount FROM CARRERA WHERE ID = @id;";
                cmd.Parameters.AddWithValue("id", student.CareerId);

                int careerCount = (int)cmd.ExecuteScalar();

                if (careerCount == 0) return 0;

                cmd.Parameters.Clear();
                // Validar que el ID del año existe
                cmd.CommandText = "SELECT COUNT(ID) AS Amount FROM AÑO WHERE ID = @id";
                cmd.Parameters.AddWithValue("id", student.CareerYearId);

                int yearCount = (int)cmd.ExecuteScalar();

                if (yearCount == 0) return 0;

                cmd.Parameters.Clear();
                // Si los IDs del año y de la carrera existen, guardar
                string query = "INSERT INTO ESTUDIANTE (CODIGO, ID_AÑO, ID_CARRERA, CONTRASEÑA) " +
                                               "VALUES (@codigo, @yearId, @careerId, @password);";

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("codigo", student.StudentCode);
                cmd.Parameters.AddWithValue("yearId", student.CareerYearId);
                cmd.Parameters.AddWithValue("careerId", student.CareerId);
                cmd.Parameters.AddWithValue("password", student.Password);

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

        public bool Login(User user)
        {
            try
            {
                string query = "SELECT COUNT(*) AS student " +
                                    "FROM ESTUDIANTE WHERE CODIGO = @studentCode AND CONTRASEÑA = @password;";

                var cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("studentCode", user.StudentCode);
                cmd.Parameters.AddWithValue("password", user.Password);

                _connection.Open();

                int countStudent = (int)cmd.ExecuteScalar();

                if (countStudent > 0) return true;

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

        public int DeleteStudent(string studentCode)
        {
            try
            {
                SqlCommand cmd = new("DELETE ESTUDIANTE WHERE CODIGO = @studentCode", _connection);
                cmd.Parameters.AddWithValue("studentCode", studentCode);

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

        public int UpdateStudent(Student student)
        {
            try
            {
                _connection.Open();

                if (student.CareerId != default)
                {
                    // Validar que el ID de la carrera existe
                    SqlCommand cmdCareer = new("SELECT COUNT(ID) AS Amount FROM CARRERA WHERE ID = @id;", _connection);
                    cmdCareer.Parameters.AddWithValue("id", student.CareerId);

                    int careerCount = (int)cmdCareer.ExecuteScalar();

                    if (careerCount == 0) return 0;
                }

                if (student.CareerYearId != default)
                {
                    // Validar que el ID de la año existe
                    SqlCommand cmdYear = new("SELECT COUNT(ID) AS Amount FROM AÑO WHERE ID = @id", _connection);
                    cmdYear.Parameters.AddWithValue("id", student.CareerYearId);

                    int yearCount = (int)cmdYear.ExecuteScalar();

                    if (yearCount == 0) return 0;
                }
                

                // Si los IDs del año y de la carrera existen, guardar
                SqlCommand cmd = new("sp_UpdateStudent", _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("studentCode", student.StudentCode);

                cmd.Parameters.AddWithValue("yearId",
                    student.CareerYearId == default
                    ? DBNull.Value
                    : student.CareerYearId);

                cmd.Parameters.AddWithValue("careerId",
                    student.CareerId == default
                    ? DBNull.Value
                    : student.CareerId);

                cmd.Parameters.AddWithValue("password",
                    string.IsNullOrWhiteSpace(student.Password)
                    ? DBNull.Value
                    : student.Password);

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
