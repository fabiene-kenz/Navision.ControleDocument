using System;
using System.Collections.Generic;
using System.Text;

namespace Navision.ControleDocuments.Models.ValuesToValidateModel
{
    public class ValuesToValidateModel
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public bool IsValidated { get; set; }
    }
}
