using PacketHandlers;
using System.Text.RegularExpressions;

namespace Pages {

    public class PageRequest {

        private static List<string> order_possibilities = ["asc","desc"];

        public string? page {get; private set;}
        public string? limit {get; private set;}
        public string? sort {get; private set;}
        public List<(string,bool)> sort_list {get; private set;}

        public PageRequest(HttpRequest request) {

            this.sort  = PacketUtils.get_value_from_query(request, "sort")?.Trim();
            this.page  = PacketUtils.get_value_from_query(request, "page")?.Trim();
            this.limit = PacketUtils.get_value_from_query(request, "limit")?.Trim();
            this.sort_list = new();

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

                if (Regex.IsMatch(this.sort,@"(\w[\w_]*:-?1)(,(\w[\w_]*:-?1))*") == false)
                    error_list.Add($"Sort arguments are not valid. List should have the format : [<name>:(1 or -1)] (item divided by ',')");
                else {

                    string[] sorting_tokens = this.sort.Split(",");
                    string sort_opts = string.Join(", ",sort_possibilities.Keys);

                    foreach (string token in sorting_tokens) {

                        string[] token_args = token.Split(":");
                        bool is_asc = Convert.ToInt32(token_args[1]) == 1;

                        if (sort_possibilities.Keys.Contains(token_args[0].ToLower()) == false) 
                            error_list.Add($"Sort argument {token_args[0]} is not valid. Try these ones : [{sort_opts}]");
                        else 
                            this.sort_list.Add((sort_possibilities[token_args[0]],is_asc));

                    }

                }

            }

            return error_list;

        }

        public PageInput convert() {

            long page_page = 1;
            long page_limit = 10;

            if (this.page != null)
                page_page = Convert.ToInt64(this.page);

            if (this.limit != null)
                page_limit = Convert.ToInt64(this.limit);

            return new PageInput(page_page,page_limit,this.sort_list);

        }

    }

}