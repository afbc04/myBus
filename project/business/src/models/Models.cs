using Npgsql;

namespace Models {

    public class ModelsManager {

        public static readonly string connection_string = $@"
            Host=database;
            Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};
            Database=myBus;
            Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};
            Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")}";

        public static async Task<TResult> execute_query<TResult>(string sql, Func<NpgsqlCommand, Task<TResult>> func) {

            await using var conn = new NpgsqlConnection(ModelsManager.connection_string);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(sql, conn);
            return await func(cmd);

        }

    }

}