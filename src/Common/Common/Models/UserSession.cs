using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public class UserSession
    {
        public long SessionId { set; get; }

        public string AccessToken { set; get; }

        [JsonIgnore]
        public int LoginResult { set; get; }

        [JsonIgnore]
        public string LoginCaptionMessage { set; get; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string DisplayName { set; get; }

        public string PhoneNumber { set; get; }

        public bool IsSuperAdmin { get; set; }

        public List<int> RoleIds { set; get; } = new List<int>();
    }
}