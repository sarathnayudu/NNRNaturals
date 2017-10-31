using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNR.UIEntity.VM
{
   public class CreateUserVM:BaseVM
    {
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Password { get; set; }
    }
}
