using Newtonsoft.Json.Linq;

namespace BlogManagement.GraphQLAPI.Queries.Models
{
    // This is the generic model for making GraphQL query
    public class GraphQLQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}
