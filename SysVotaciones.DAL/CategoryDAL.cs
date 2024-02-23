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

        public List<Category> GetCategories ()
        {
            try
            {
                var cmd = new SqlCommand("SELECT * FROM CATEGORIA", _connection);

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
                SqlCommand cmd = new("INSERT INTO CATEGORIA (NOMBRE) VALUES (@category)", _connection);
                cmd.Parameters.AddWithValue("category", category.Name);

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
    }
}
