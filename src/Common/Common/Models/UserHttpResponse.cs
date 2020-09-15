using System.Net;

namespace Common.Models
{
    public class UserHttpResponse
    {
        public bool IsSuccess { set; get; }
        public HttpStatusCode StatusCode { set; get; }
        public string Content { set; get; }
    }
}