using Npgsql;
using Pages;

namespace Models {

    public class UserModel {

        public UserModel() {}

        public async Task<IList<User>> clear() {

            var users_list = await values();

            string delete_sql = "DELETE FROM Users;";
            await ModelsManager.execute_query(delete_sql, async cmd => {

                await cmd.ExecuteNonQueryAsync();
                return true;

            });

            return users_list;

        }


        public async Task<User?> get(string ID) {

            string sql = "SELECT * FROM Users WHERE id = @id;";
            return await ModelsManager.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id",ID);
                await using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync()) {

                    return new User(
                        reader.GetString(0), 
                        reader.GetString(1),
                        reader.GetInt32(2),
                        ModelsManager.get_string(reader,3),
                        reader.GetChar(4),
                        ModelsManager.get_date(reader,5),
                        ModelsManager.get_date(reader,6),
                        ModelsManager.get_string(reader,7),
                        ModelsManager.get_string(reader,8),
                        ModelsManager.get_date(reader,9),
                        reader.GetChar(10),
                        ModelsManager.get_string(reader,11),
                        reader.GetDateTime(12),
                        reader.GetBoolean(13),
                        ModelsManager.get_bool(reader,14),
                        ModelsManager.get_string(reader,15),
                        ModelsManager.get_date(reader,16),
                        ModelsManager.get_date(reader,17)
                        );

                }

                return null;

            });

        }

        public async Task<UserFull?> get_full(string ID) {

            string sql = "SELECT * FROM VUsers WHERE id = @id;";
            return await ModelsManager.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id",ID);
                await using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync()) {

                    string? country_code_id = ModelsManager.get_string(reader,11);
                    CountryCode? country_code = null;

                    if (country_code_id != null) {
                        country_code = new CountryCode(country_code_id,reader.GetString(12));
                    }

                    string? bus_pass_id = ModelsManager.get_string(reader,16);
                    UserBusPass? bus_pass = null;

                    if (bus_pass_id != null) {
                        bus_pass = new UserBusPass(
                            ID,
                            reader.GetDateTime(17),
                            reader.GetDateTime(18),
                            new BusPass(
                                bus_pass_id,
                                reader.GetDouble(19),
                                reader.GetInt16(20),
                                reader.GetInt32(21),
                                reader.GetBoolean(22)
                                )
                            );
                    }

                    User user = new(
                        ID,
                        reader.GetString(1),
                        reader.GetInt32(2),
                        ModelsManager.get_string(reader,3),
                        reader.GetChar(4),
                        ModelsManager.get_date(reader,5),
                        ModelsManager.get_date(reader,6),
                        ModelsManager.get_string(reader,7),
                        ModelsManager.get_string(reader,8),
                        ModelsManager.get_date(reader,9),
                        reader.GetChar(10),
                        country_code_id,
                        reader.GetDateTime(13),
                        reader.GetBoolean(14),
                        ModelsManager.get_bool(reader,15),
                        null,
                        null,
                        null
                        );

                    return new UserFull(user,country_code,bus_pass);

                }

                return null;

            });

        }

        public async Task<bool> contains(string ID) {

            string sql = "SELECT COUNT(*) FROM Users WHERE id = @id;";
            return await ModelsManager.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id", ID);
                
                var count = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                return count > 0;

            });

        }

        public async Task<bool> delete(string ID) {

            string sql = "DELETE FROM Users WHERE id = @id";
            return await ModelsManager.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id", ID);
                
                var lines = await cmd.ExecuteNonQueryAsync();
                return lines > 0;

            });

        }

        public async Task<bool> insert(User user) {

            try {

                string sql = @"INSERT INTO Users 
                    (id, password, salt, name, level, adminSinceDate, inactiveDate, inactiveAccountUser, email, birthDate, sex, countryCode, accountCreation, public, disablePerson, busPassID, busPassValidFrom, busPassValidUntil) 
                VALUES (@id, @password, @salt, @name, @level, @adminSinceDate, NULL, NULL, @email, @birthDate, @sex, @countryCode, @accountCreation, @public, @disablePerson, NULL, NULL, NULL);";

                return await ModelsManager.execute_query(sql, async cmd => {
                    
                    cmd.Parameters.AddWithValue("@id", user.ID);
                    cmd.Parameters.AddWithValue("@password", user.password);
                    cmd.Parameters.AddWithValue("@salt", user.salt);
                    cmd.Parameters.AddWithValue("@name", user.name ?? (object) DBNull.Value);
                    cmd.Parameters.AddWithValue("@level", user.level);
                    cmd.Parameters.AddWithValue("@adminSinceDate", user.admin_since_date ?? (object) DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", user.email ?? (object) DBNull.Value);
                    cmd.Parameters.AddWithValue("@birthDate", user.birth_date ?? (object) DBNull.Value);
                    cmd.Parameters.AddWithValue("@sex", user.sex);
                    cmd.Parameters.AddWithValue("@countryCode", user.country_code ?? (object) DBNull.Value);
                    cmd.Parameters.AddWithValue("@accountCreation", user.account_creation);
                    cmd.Parameters.AddWithValue("@public", user.is_public);
                    cmd.Parameters.AddWithValue("@disablePerson", user.is_disable_person ?? (object) DBNull.Value);
                    
                    await cmd.ExecuteNonQueryAsync();
                    return true;

                });

            }
            catch {
                return false;
            }

        }

        public async Task<bool> update(User user) {

            string sql = @"UPDATE Users 
                        SET password = @password,
                            name = @name,
                            level = @level,
                            adminSinceDate = @adminSinceDate,
                            inactiveDate = @inactiveDate,
                            inactiveAccountUser = @inactiveAccountUser,
                            email = @email,
                            birthDate = @birthDate,
                            sex = @sex,
                            countryCode = @countryCode,
                            accountCreation = @accountCreation,
                            public = @public,
                            disablePerson = @disablePerson
                        WHERE id = @id;";

            return await ModelsManager.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id", user.ID);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@name", user.name ?? (object) DBNull.Value);
                cmd.Parameters.AddWithValue("@level", user.level);
                cmd.Parameters.AddWithValue("@adminSinceDate", user.admin_since_date ?? (object) DBNull.Value);
                cmd.Parameters.AddWithValue("@inactiveDate", user.inactive_date ?? (object) DBNull.Value);
                cmd.Parameters.AddWithValue("@inactiveAccountUser", user.user_who_inactivated_account ?? (object) DBNull.Value);
                cmd.Parameters.AddWithValue("@email", user.email ?? (object) DBNull.Value);
                cmd.Parameters.AddWithValue("@birthDate", user.birth_date ?? (object) DBNull.Value);
                cmd.Parameters.AddWithValue("@sex", user.sex);
                cmd.Parameters.AddWithValue("@countryCode", user.country_code ?? (object) DBNull.Value);
                cmd.Parameters.AddWithValue("@accountCreation", user.account_creation);
                cmd.Parameters.AddWithValue("@public", user.is_public);
                cmd.Parameters.AddWithValue("@disablePerson", user.is_disable_person ?? (object) DBNull.Value);

                var lines = await cmd.ExecuteNonQueryAsync();
                return lines > 0;

            });

        }

        public async Task<IList<UserList>> list(PageInput page) {

            string sql = "SELECT id, name, level, countryCode, inactiveDate FROM Users";
            sql += page.get_sql_listing();
            sql += ";";

            return await ModelsManager.execute_query(sql, async cmd => {

                var list = new List<UserList>();
                await using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                    list.Add(new UserList(
                        reader.GetString(0),
                        ModelsManager.get_string(reader,1),
                        reader.GetChar(2),
                        ModelsManager.get_string(reader,3),
                        ModelsManager.get_date(reader,4) == null
                        ));
                
                return list;

            });

        }

        public async Task<IList<User>> values() {

            var list = new List<User>();

            foreach (string key in await keys()) {
                User? user = await get(key);
                list.Add(user!);
            }

            return list;

        }

        public async Task<IList<string>> keys() {

            string sql = "SELECT id FROM Users;";
            return await ModelsManager.execute_query(sql, async cmd => {

                var list = new List<string>();
                
                await using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                    list.Add(reader.GetString(0));
                
                return list;

            });

        }

        public async Task<long> size() {

            string sql = "SELECT COUNT(*) FROM Users;";
            return await ModelsManager.execute_query(sql, async cmd =>
                Convert.ToInt64(await cmd.ExecuteScalarAsync()));

        }

        public async Task<bool> empty() {

            return await this.size() == 0;

        }

    }
}
