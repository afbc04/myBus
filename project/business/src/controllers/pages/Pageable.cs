namespace Pages {

    public class PageInput {

        public long page {get; private set;}
        public long limit {get; private set;}
        public string? sort {get; private set;}
        public bool? order {get; private set;}

        public PageInput(HttpRequest request) : this(
            
            request.Query["page"],
            request.Query["limit"],
            request.Query["sort"],
            request.Query["order"]

            ) {}

        private PageInput(string? page, string? limit, string? sort, string? order) {

            if (page == null)
                this.page = 1;
            else {

                if (long.TryParse(page, out long page_result))
                    this.page = page_result < 1 ? 1 : page_result;
                else
                    this.page = 1;

            }
            
            if (limit == null)
                this.limit = 10;
            else {

                if (long.TryParse(limit, out long limit_result))
                    this.limit = limit_result < 1 ? 10 : limit_result;
                else
                    this.limit = 10;

            }

            this.sort = sort;
            this.order = null;

            if (order != null)
                this.order = order.ToUpper() switch {
                    "ASC" => true,
                    "DESC" => false,
                    _ => null
                };

            Console.WriteLine($"PAGE {this.page} {this.limit} {this.sort} {this.order}");

        }

        public string get_sql_filtering() {

            string sql = "";

            if (this.sort != null) {

                sql += $" ORDER BY {this.sort}";

                Console.WriteLine($"HIHIHI {sql}");

                if (this.order != null) {
                    string order = this.order == true ? "ASC" : "DESC";
                    sql += $" {order}";
                }

            }

            sql += $" LIMIT {this.limit} OFFSET {(this.page-1) * this.limit};";

            return sql;

        }

    }

    public class PageOutput {

        public long total_elements {get; private set;}
        public long page_elements {get; private set;}
        public long page {get; private set;}
        public long limit {get; private set;}
        public long total_pages {get; private set;}
        public bool empty {get; private set;}
        public bool all {get; private set;}
        public bool first_page {get; private set;}
        public bool last_page {get; private set;}
        public IList<object> data {get; private set;}

        public PageOutput(PageInput page_input,long total_elements, IList<object> data) {

            this.data = data;
            this.page = page_input.page;
            this.limit = page_input.limit;
            this.total_elements = total_elements;
            this.page_elements = data.Count();
            this.empty = this.page_elements == 0;
            this.all = this.page_elements == total_elements;

            this.total_pages = total_elements / page_input.limit;
            if (total_elements > this.total_pages * page_input.limit)
                this.total_pages++;

            this.first_page = this.page == 1;
            this.last_page = this.total_pages == 0 ? true : this.page == this.total_pages;

        }

        public IDictionary<string,object> to_json() {
            return new Dictionary<string,object> {
                ["totalElements"] = this.total_elements,
                ["pageElements"] = this.page_elements,
                ["page"] = this.page,
                ["limit"] = this.limit,
                ["totalPages"] = this.total_pages,
                ["empty"] = this.empty,
                ["all"] = this.all,
                ["firstPage"] = this.first_page,
                ["lastPage"] = this.last_page,
                ["data"] = this.data
            };
        }

    }

}