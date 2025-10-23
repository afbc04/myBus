using Pages;
namespace Queries;

public class QueryUser : IQuery {

    private Querieable query {get; set;}

    public QueryUser(Querieable query) {
        this.query = query;
    }
    
    public string get_sql_listing() {
        return query.page!.get_sql_listing();
    }

    public string get_sql_filtering() {
        return "";
    }

    public IList<string> validate() {
        return new List<string>();
    }

    public PageInput get_page() {
        return this.query.page!;
    }

}