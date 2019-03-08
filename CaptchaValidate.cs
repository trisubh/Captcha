using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Captacha
{
    public class CaptchaValidate
    {
        private string s;
        public string StringToValidate { get { return s; } set { s = value; } }
        public bool validate(string s)
        {
            
            return true;
        }
    }

    public static class CaptcahaMode
    {
        public static string mode { get; set; }

        public static string isstrict { get; set; }
    }
}