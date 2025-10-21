using Npgsql;
using Pages;

namespace Models {

    public class BusPassModel {

        public BusPassModel() {}

        public async Task<IList<BusPass>> clear() {

            string list_sql = "SELECT * FROM BusPasses;";

            var bus_pass_list = await ModelsManager.execute_query(list_sql, async cmd => {

                var list = new List<BusPass>();
                await using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                    list.Add(new BusPass(
                        reader.GetString(0), 
                        reader.GetDouble(1),
                        reader.GetInt16(2),
                        reader.GetInt32(3),
                        reader.GetBoolean(4)
                        ));
                
                return list;

            });

            string delete_sql = "DELETE FROM BusPasses;";
            await ModelsManager.execute_query(delete_sql, async cmd => {

                await cmd.ExecuteNonQueryAsync();
                return true;

            });

            return bus_pass_list;

        }


        public async Task<BusPass?> get(string ID) {

            string sql = "SELECT * FROM BusPasses WHERE id = @id;";
            return await ModelsManager.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id",ID);
                await using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                    return new BusPass(
                        reader.GetString(0), 
                        reader.GetDouble(1),
                        reader.GetInt16(2),
                        reader.GetInt32(3),
                        reader.GetBoolean(4));

                return null;

            });

        }

        public async Task<bool> contains(string ID) {

            string sql = "SELECT COUNT(*) FROM BusPasses WHERE id = @id;";
            return await ModelsManager.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id", ID);
                
                var count = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                return count > 0;

            });

        }

        public async Task<bool> delete(string ID) {

            string sql = "DELETE FROM BusPasses WHERE id = @id";
            return await ModelsManager.execute_query(sql, async cmd => {

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

                return await ModelsManager.execute_query(sql, async cmd => {
                    
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

            return await ModelsManager.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id", bp.ID);
                cmd.Parameters.AddWithValue("@discount", bp.discount);
                cmd.Parameters.AddWithValue("@localityLevel", bp.locality_level);
                cmd.Parameters.AddWithValue("@duration", bp.duration_days);
                cmd.Parameters.AddWithValue("@active", bp.is_active);

                var lines = await cmd.ExecuteNonQueryAsync();
                return lines > 0;

            });

        }


        public async Task<IList<BusPass>> values(PageInput page) {

            string sql = "SELECT * FROM BusPasses";
            sql += page.get_sql_listing();
            sql += ";";

            return await ModelsManager.execute_query(sql, async cmd => {

                var list = new List<BusPass>();
                await using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                    list.Add(new BusPass(
                        reader.GetString(0), 
                        reader.GetDouble(1),
                        reader.GetInt16(2),
                        reader.GetInt32(3),
                        reader.GetBoolean(4)));
                
                return list;

            });

        }

        public async Task<IList<string>> keys() {

            string sql = "SELECT id FROM BusPasses;";
            return await ModelsManager.execute_query(sql, async cmd => {

                var list = new List<string>();
                
                await using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                    list.Add(reader.GetString(0));
                
                return list;

            });

        }

        public async Task<long> size() {

            string sql = "SELECT COUNT(*) FROM BusPasses;";
            return await ModelsManager.execute_query(sql, async cmd =>
                Convert.ToInt64(await cmd.ExecuteScalarAsync()));

        }

        public async Task<bool> empty() {

            return await this.size() == 0;

        }

    }
}
