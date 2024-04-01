using System.Data;
using System.Data.SqlClient;
using SysVotaciones.EN;


namespace SysVotaciones.DAL
{
    public class CategoryDAL
    {
        private readonly SqlConnection _connection;

        public CategoryDAL (string connectionString)
        {
            _connection = new SqlConnection (connectionString);
        }

        public List<Category> GetCategories (int offset = 0, int amount = 0)
        {
            try
            {
                string query = "SELECT * FROM CATEGORIA ORDER BY ID DESC OFFSET @offset ROWS " +
                                                        "FETCH NEXT @amount ROWS ONLY;";

                var cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("offset", offset);
                cmd.Parameters.AddWithValue("amount", amount == 0 ? 6 : amount);

                _connection.Open();

                var reader = cmd.ExecuteReader();
                List<Category> list = [];

                while (reader.Read())
                {
                    list.Add(new Category()
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

        public int GetTotalCategories()
        {
            try
            {
                var cmd = new SqlCommand("SELECT TOTAL FROM TOTAL_CATEGORIAS", _connection);

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

        public Category? GetCategory(int id)
        {
            try
            {
                SqlCommand cmd = new("SELECT * FROM CATEGORIA WHERE ID = @id", _connection);
                cmd.Parameters.AddWithValue("id", id);

                _connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                Category? category = null;

                if (reader.Read())
                {
                    category = new Category()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }

                return category;
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

        public int SaveCategory(Category category)
        {
            try
            {
                SqlCommand cmd = new("INSERT INTO CATEGORIA (NOMBRE) VALUES (@category); SELECT SCOPE_IDENTITY() AS ID;", _connection);
                cmd.Parameters.AddWithValue("category", category.Name);

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

        public int DeleteCategory(int id)
        {
            try
            {
                SqlCommand cmd = new("DELETE CATEGORIA WHERE ID = @id", _connection);
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

        public int UpdateCategory(Category category)
        {
            try
            {
                SqlCommand cmd = new("sp_UpdateCategory", _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id", category.Id);
                cmd.Parameters.AddWithValue("name",
                    string.IsNullOrWhiteSpace(category.Name)
                    ? DBNull.Value
                    : category.Name);

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

        public List<Category> SearchCategories(string keyword)
        {
            try
            {
                var cmd = new SqlCommand("SELECT * FROM CATEGORIA WHERE NOMBRE LIKE @keyword + '%'", _connection);
                cmd.Parameters.AddWithValue("keyword", keyword);

                _connection.Open();

                var reader = cmd.ExecuteReader();
                List<Category> list = [];

                while (reader.Read())
                {
                    list.Add(new Category()
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
