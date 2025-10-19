using Npgsql;

namespace Models {

    public class ModelTableCreator {

        public static async Task country_codes() {

            await using var conn = new NpgsqlConnection(ModelsManager.connection_string);
            await conn.OpenAsync();

            var table_to_be_created = @"
                CREATE TABLE IF NOT EXISTS CountryCodes (
                    id CHAR(3) PRIMARY KEY,
                    name VARCHAR(20) NOT NULL
                );
            ";

            await using var cmd = new NpgsqlCommand(table_to_be_created, conn);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task bus_passes() {

            await using var conn = new NpgsqlConnection(ModelsManager.connection_string);
            await conn.OpenAsync();

            var table_to_be_created = @"
                CREATE TABLE IF NOT EXISTS BusPasses (
                  id CHAR(3) PRIMARY KEY,
                  discount NUMERIC(5,2) NOT NULL,
                  localityLevel SMALLINT NOT NULL,
                  duration INT NOT NULL,
                  active BOOLEAN NOT NULL
                );
            ";

            await using var cmd = new NpgsqlCommand(table_to_be_created, conn);
            await cmd.ExecuteNonQueryAsync();
        }

    }

}