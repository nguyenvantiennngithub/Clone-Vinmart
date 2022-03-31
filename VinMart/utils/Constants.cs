using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinMart.utils
{
    public class Constants
    {
        private static Constants instance;

        internal static Constants Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Constants();
                }
                return instance;
            }
            set => instance = value;
        }
        public string UserSession = "UserSession";
    }
}