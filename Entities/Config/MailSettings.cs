using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Email
{
    public class MailSettings
    {
        public string server { get; set; }   
        public int port { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public string sendername {  get; set; }

        public string senderemail {  get; set; }
    }
}
