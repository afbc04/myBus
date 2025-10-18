namespace Pages {

    public class PageInput {

        public long page {get; private set;}
        public long limit {get; private set;}
        public string? sort {get; private set;}
        public bool? order {get; private set;}

        public PageInput(long page, long limit, string? sort, bool? order) {
            
            this.page = page;
            this.limit = limit;
            this.sort = sort;
            this.order = order;

        }

        public string get_sql_listing() {

            string sql = "";

            if (this.sort != null) {

                sql += $" ORDER BY {this.sort}";

                if (this.order != null) {
                    string order = this.order == true ? "ASC" : "DESC";
                    sql += $" {order}";
                }

            }

            sql += $" LIMIT {this.limit} OFFSET {(this.page-1) * this.limit}";

            return sql;

        }

    }

}