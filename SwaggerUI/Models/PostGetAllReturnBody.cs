using System.Collections.Generic;

namespace SwaggerUI.Models
{
    public class PostGetAllReturnBody : RequestBody
    {
        public int PagesCount { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<Url> Urls { get; set; }
    }
}
