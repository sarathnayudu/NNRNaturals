using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNR.UIEntity.VM
{
  public  class LoginVM:BaseVM
    {
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private bool rememberMe;

        public bool RememberMe
        {
            get { return rememberMe; }
            set { rememberMe = value; }
        }
    }
}
