using Npgsql;
using Pages;

namespace Models {

    public class TokenModel {

        public TokenModel() {}

        public async Task<AuthUser?> get(string ID) {

            string sql = "SELECT * FROM VAuthUsers WHERE id = @id;";
            return await ModelUtil.execute_query(sql, async cmd => {

                cmd.Parameters.AddWithValue("@id",ID);
                await using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync()) {

                    return new AuthUser(
                        reader.GetString(0), 
                        reader.GetString(1),
                        reader.GetInt32(2),
                        reader.GetChar(3),
                        reader.IsDBNull(4)
                        );

                }

                return null;

            });

        }

    }
}
