using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.utils
{
    public class UserSession
    {
        private string username;
        private string displayName;
        private int position;
        private int id;
        private string email;

        public UserSession()
        {
        }

        public UserSession(string username, string displayName, int position, int id, string email)
        {
            this.Username = username;
            this.DisplayName = displayName;
            this.Id = id;
            this.Position = position;
            this.Email= email;
        }

        public string Username { get => username; set => username = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public int Position { get => position; set => position = value; }
        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
    }
}