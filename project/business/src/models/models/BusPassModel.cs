using Npgsql;
using Queries;

namespace Models {

    public class BusPassModel {
        
        private static BusPass _serialize(NpgsqlDataReader reader) {
            return new BusPass(
                        reader.GetString(0), 
                        reader.GetDouble(1),
                        reader.GetInt16(2),
                        reader.GetInt32(3),
                        reader.GetBoolean(4));
        }

        private static BusPassFull _serialize_full(NpgsqlDataReader reader) {
            return new BusPassFull(
                        reader.GetString(0), 
                        reader.GetDouble(1),
                        reader.GetInt16(2),
                        reader.GetInt32(3),
                        reader.GetBoolean(4),
                        0,0);
        }

        public async Task<IList<BusPass>> clear() {
            return await ModelUtil.execute_clear<BusPass>(
                "SELECT id, discount, localityLevel, duration, active FROM BusPasses;",
                "UPDATE BusPasses SET active = false;",
                _serialize
            );
        }

        public async Task<BusPass?> get(string ID) {

            string sql = "SELECT id, discount, localityLevel, duration, active FROM BusPasses WHERE id = @id;";
            return await ModelUtil.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id",ID);
                await using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                    return _serialize(reader);

                return null;

            });

        }

        public async Task<BusPassFull?> get_full(string ID) {

            await using var conn = new NpgsqlConnection(ModelsManager.connection_string);
            await conn.OpenAsync();

            await using var transaction = await conn.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);
            await using (var cmd_set_transation_read_only = new NpgsqlCommand("SET TRANSACTION READ ONLY;", conn, transaction)) {
                await cmd_set_transation_read_only.ExecuteNonQueryAsync();
            }

            try {

                BusPassFull? bus_pass = null;
                string sql_search_bus_pass = "SELECT id, discount, localityLevel, duration, active FROM BusPasses WHERE id = @id;";

                await using (var cmd = new NpgsqlCommand(sql_search_bus_pass, conn, transaction)) {

                    cmd.Parameters.AddWithValue("@id", ID);
                    await using var reader = await cmd.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                        bus_pass = _serialize_full(reader);

                    await reader.CloseAsync();

                }

                if (bus_pass != null) {

                    long users_active = 0;
                    string sql_get_users_active = "SELECT COUNT(*) FROM Users WHERE busPassID = @id;";

                    await using (var cmd = new NpgsqlCommand(sql_get_users_active, conn, transaction)) {
                        cmd.Parameters.AddWithValue("@id", ID);
                        users_active = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    bus_pass.users_active = users_active;
                    bus_pass.users_expired = users_active;

                }

                await transaction.CommitAsync();
                return bus_pass;

            }
            catch {
                await transaction.RollbackAsync();
            }

            return null;

        }


        public async Task<bool> contains(string ID) {

            string sql = "SELECT COUNT(*) FROM BusPasses WHERE id = @id;";
            return await ModelUtil.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id", ID);
                var count = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                return count > 0;

            });

        }

        public async Task<bool> delete(string ID) {

            string sql = "UPDATE BusPasses SET active = false WHERE id = @id";
            return await ModelUtil.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id", ID);
                var lines = await cmd.ExecuteNonQueryAsync();
                return lines > 0;

            });

        }

        public async Task<bool> insert(BusPass bp) {

            try {

                string sql = @"
                    INSERT INTO BusPasses (id, discount, localityLevel, duration, active) 
                    VALUES (@id, @discount, @localityLevel, @duration, @active);";

                return await ModelUtil.execute_query(sql, async cmd => {
                    
                    cmd.Parameters.AddWithValue("@id", bp.ID);
                    cmd.Parameters.AddWithValue("@discount", bp.discount);
                    cmd.Parameters.AddWithValue("@localityLevel", bp.locality_level);
                    cmd.Parameters.AddWithValue("@duration", bp.duration_days);
                    cmd.Parameters.AddWithValue("@active", bp.is_active);
                    
                    await cmd.ExecuteNonQueryAsync();
                    return true;

                });

            }
            catch {
                return false;
            }

        }

        public async Task<bool> update(BusPass bp) {

            string sql = @"
                UPDATE BusPasses SET 
                    discount = @discount,
                    localityLevel = @localityLevel,
                    duration = @duration,
                    active = @active
                WHERE id = @id;";

            return await ModelUtil.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id", bp.ID);
                cmd.Parameters.AddWithValue("@discount", bp.discount);
                cmd.Parameters.AddWithValue("@localityLevel", bp.locality_level);
                cmd.Parameters.AddWithValue("@duration", bp.duration_days);
                cmd.Parameters.AddWithValue("@active", bp.is_active);

                var lines = await cmd.ExecuteNonQueryAsync();
                return lines > 0;

            });

        }


        public async Task<ModelListing<BusPass>> values(QueryBusPass querie) {
            return await ModelUtil.execute_get_list(querie,"BusPasses","id, discount, localityLevel, duration, active",_serialize);
        }

        public async Task<IList<string>> keys() {

            string sql = "SELECT id FROM BusPasses;";
            return await ModelUtil.execute_query(sql, async cmd => {

                var list = new List<string>();
                await using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                    list.Add(reader.GetString(0));
                
                return list;

            });

        }

        public async Task<long> size() {
            return await ModelUtil.execute_get_size("BusPasses");
        }

        public async Task<bool> empty() {
            return await this.size() == 0;
        }

    }
}
