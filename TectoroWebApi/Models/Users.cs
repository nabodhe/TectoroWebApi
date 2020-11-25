using System;
using System.Collections.Generic;

namespace TectoroWebApi
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Alias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int? Level { get; set; }
        public int? ManagerId { get; set; }
    }
}
