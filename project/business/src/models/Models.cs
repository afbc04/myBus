using Npgsql;

namespace Models {

    public class DatabaseInit {

        public static async Task init() {

            await ModelTableCreator.country_codes();
            await ModelTableCreator.bus_passes();
            await ModelTableCreator.users();
            await ModelTableCreator.users_view();

        }

    }


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

        public static string? get_string(NpgsqlDataReader reader, int index) {
            return reader.IsDBNull(index) ? null : reader.GetString(index);
        }

        public static int? get_int(NpgsqlDataReader reader, int index) {
            return reader.IsDBNull(index) ? null : reader.GetInt32(index);
        }

        public static double? get_double(NpgsqlDataReader reader, int index) {
            return reader.IsDBNull(index) ? null : reader.GetDouble(index);
        }

        public static bool? get_bool(NpgsqlDataReader reader, int index) {
            return reader.IsDBNull(index) ? null : reader.GetBoolean(index);
        }

        public static DateOnly? get_date(NpgsqlDataReader reader, int index) {
            return reader.IsDBNull(index) ? null : DateOnly.FromDateTime(reader.GetDateTime(index));
        }

        public static DateTime? get_date_time(NpgsqlDataReader reader, int index) {
            return reader.IsDBNull(index) ? null : reader.GetDateTime(index);
        }

    }

}