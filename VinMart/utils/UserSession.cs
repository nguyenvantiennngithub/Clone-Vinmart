﻿using System;
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

        public UserSession()
        {
        }

        public UserSession(string username, string displayName, int position, int id)
        {
            this.Username = username;
            this.DisplayName = displayName;
            this.Id = id;
            this.Position = position;
        }

        public string Username { get => username; set => username = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public int Position { get => position; set => position = value; }
        public int Id { get => id; set => id = value; }
    }
}