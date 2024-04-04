using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IEmailServices
    {
        Task sendActivationEmail(UsersModel user, string baseurl);
    }
}
