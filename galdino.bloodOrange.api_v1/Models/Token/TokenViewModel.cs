using System;

namespace galdino.bloodOrange.api_v1.Models.Token
{
    public class TokenViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public int Profile { get; set; }
        public bool Authenticate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime Expires { get; set; }
        public string Token { get; set; }
    }
}
