using Npgsql;
using Pages;

namespace Models {

    public class CountryCodeModel {

        public CountryCodeModel() {}

        public async Task<IList<CountryCode>> clear() {

            string list_sql = "SELECT * FROM CountryCodes;";

            var country_code_list = await ModelsManager.execute_query(list_sql, async cmd => {

                var list = new List<CountryCode>();
                await using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                    list.Add(new CountryCode(
                        reader.GetString(0), 
                        reader.GetString(1)
                        ));
                
                return list;

            });

            string delete_sql = "DELETE FROM CountryCodes;";
            await ModelsManager.execute_query(delete_sql, async cmd => {

                await cmd.ExecuteNonQueryAsync();
                return true;

            });

            return country_code_list;

        }


        public async Task<CountryCode?> get(string ID) {

            string sql = "SELECT id, name FROM CountryCodes WHERE id = @id;";
            return await ModelsManager.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id",ID);
                await using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                    return new CountryCode(reader.GetString(0), reader.GetString(1));

                return null;

            });

        }

        public async Task<bool> contains(string ID) {

            string sql = "SELECT COUNT(*) FROM CountryCodes WHERE id = @id;";
            return await ModelsManager.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id", ID);
                
                var count = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                return count > 0;

            });

        }

        public async Task<bool> delete(string ID) {

            string sql = "DELETE FROM CountryCodes WHERE id = @id";
            return await ModelsManager.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id", ID);
                
                var lines = await cmd.ExecuteNonQueryAsync();
                return lines > 0;

            });

        }

        public async Task<bool> insert(CountryCode cc) {

            try {

                string sql = "INSERT INTO CountryCodes (id, name) VALUES (@id, @name);";

                return await ModelsManager.execute_query(sql, async cmd => {
                    
                    cmd.Parameters.AddWithValue("@id", cc.ID);
                    cmd.Parameters.AddWithValue("@name", cc.name);
                    
                    await cmd.ExecuteNonQueryAsync();
                    return true;

                });

            }
            catch {
                return false;
            }

        }

        public async Task<bool> update(CountryCode cc) {

            string sql = "UPDATE CountryCodes SET name = @name WHERE id = @id;";

            return await ModelsManager.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id", cc.ID);
                cmd.Parameters.AddWithValue("@name", cc.name);

                var lines = await cmd.ExecuteNonQueryAsync();
                return lines > 0;

            });

        }


        public async Task<IList<CountryCode>> values(PageInput page) {

            string sql = "SELECT id, name FROM CountryCodes";
            sql += page.get_sql_listing();
            sql += ";";

            return await ModelsManager.execute_query(sql, async cmd => {

                var list = new List<CountryCode>();
                await using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                    list.Add(new CountryCode(reader.GetString(0), reader.GetString(1)));
                
                return list;

            });

        }

        public async Task<IList<string>> keys() {

            string sql = "SELECT id FROM CountryCodes;";
            return await ModelsManager.execute_query(sql, async cmd => {

                var list = new List<string>();
                
                await using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                    list.Add(reader.GetString(0));
                
                return list;

            });

        }

        public async Task<long> size() {

            string sql = "SELECT COUNT(*) FROM CountryCodes;";
            return await ModelsManager.execute_query(sql, async cmd =>
                Convert.ToInt64(await cmd.ExecuteScalarAsync()));

        }

        public async Task<bool> empty() {

            return await this.size() == 0;

        }

    }
}
