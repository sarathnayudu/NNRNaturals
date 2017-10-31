using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNR.UIEntity.Model
{
  public  class OAuthModel
    {
        public string AuthToken { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public List<string> LstRoles { get; set; }
    }
}
