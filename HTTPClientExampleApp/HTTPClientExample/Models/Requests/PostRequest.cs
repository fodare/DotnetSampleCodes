using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HTTPClientExample.Models.Requests
{
    public class PostRequest
    {
        public int userId { get; set; }
        public string? title { get; set; }
        public string? body { get; set; }

        public int? id { get; set; }
    }
}
