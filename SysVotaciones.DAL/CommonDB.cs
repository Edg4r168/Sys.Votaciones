using System.Data.SqlClient;
using System.Data;

namespace SysVotaciones.DAL
{
    public class CommonDB
    {
        /*private static async Task<SqlConnection?> GetConnectionAsync()
        {
            var conn = new SqlConnection(_connectionString);
            try
            {
                await conn.OpenAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
                return null;
            }

            return conn;
        }

        public static async Task<SqlCommand?> GetCommandAsync()
        {
            using var cmd = new SqlCommand();
            var conn = await GetConnectionAsync();

            if (conn == null) return null;

            cmd.Connection = conn;
            return cmd;
        }

        public static async Task<int> ExecuteCommandAsync(SqlCommand cmd)
        {
            int rowAffected = 0;
            try
            {
                rowAffected = await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar comando: {ex.Message}");
                return 0;

            } finally
            {
                if (cmd.Connection != null)
                {
                    await cmd.Connection.CloseAsync();
                }
            }

            return rowAffected;
        }

        public static async Task<SqlDataReader?> ExecuteReaderAsync(SqlCommand cmd)
        {
            SqlDataReader reader;
            try
            {
                reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar comando: {ex.Message}");
                return null;
            }

            return reader;
        }*/

        private static SqlConnection GetConnection()
        {
            try
            {
                using SqlConnection conn = new(_connectionString);
                conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static SqlCommand GetCommand()
        {
            SqlCommand command = new();
            command.Connection = GetConnection();

            return command;
        }

        public static int ExecuteCommand(SqlCommand pCommnad)
        {
            int resutl = pCommnad.ExecuteNonQuery();
            pCommnad.Connection.Close();

            return resutl;
        }

        public static SqlDataReader ExecuteDataReader(SqlCommand pCommand)
        {
            var reader = pCommand.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Close();

            return reader;
        }
    }

}
