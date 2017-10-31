using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNR.UIEntity.Model
{
   public class TemplateModel
    {
        public int TemplateID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Users { get; set; }
        public bool IsActive { get; set; }
        public string FieldTitle { get; set; }
    }
}
