using PacketHandlers;

namespace Pages {

    public class PageRequest {

        private static List<string> order_possibilities = ["asc","desc"];

        public string? page {get; private set;}
        public string? limit {get; private set;}
        public string? sort {get; private set;}
        public string? order {get; private set;}

        public PageRequest(HttpRequest request) {

            this.sort  = PacketUtils.get_value_from_query(request, "sort")?.Trim();
            this.order = PacketUtils.get_value_from_query(request, "order")?.Trim();
            this.page  = PacketUtils.get_value_from_query(request, "page")?.Trim();
            this.limit = PacketUtils.get_value_from_query(request, "limit")?.Trim();

            Console.WriteLine($"P {this.page} L {this.limit} S {this.sort} O {this.order}");
        }

        public IList<string> validate(IDictionary<string,string> sort_possibilities) {

            var error_list = new List<string>();

            if (page != null) {

                if (long.TryParse(page, out long page_result)) {
                    if (page_result <= 0)
                        error_list.Add("Page number must be a positive number");
                }
                else
                    error_list.Add("Page number is not an integer number");

            }

            if (limit != null) {

                if (long.TryParse(limit, out long limit_result)) {
                    if (limit_result <= 0)
                        error_list.Add("Limit must be a positive number");
                }
                else
                    error_list.Add("Limit is not an integer number");

            }

            if (this.sort != null) {

                if (sort_possibilities.Keys.Contains(this.sort!.ToLower()) == false) {
                    string sort_opts = string.Join(", ",sort_possibilities.Keys);
                    error_list.Add($"Sort argument is not valid. Try these ones : [{sort_opts}]");
                }
                else {
                    this.sort = sort_possibilities[this.sort];
                }

            }

            if (this.order != null && PageRequest.order_possibilities.Contains(this.order!.ToLower()) == false) {
                error_list.Add($"""Order argument is not valid. Try these ones : ["asc", "desc"]""");
            }

            return error_list;

        }

        public PageInput convert() {

            long page_page = 1;
            long page_limit = 10;
            bool? page_order = null;

            if (this.page != null)
                page_page = Convert.ToInt64(this.page);

            if (this.limit != null)
                page_limit = Convert.ToInt64(this.limit);
            
            if (this.order != null)
                page_order = order.ToUpper() switch {
                    "ASC" => true,
                    "DESC" => false,
                    _ => null
                };

            return new PageInput(page_page,page_limit,this.sort,page_order);

        }

    }

}