using System;
using System.Collections.Generic;
using System.Text;

namespace Sale_Management.Ventes
{
    public class InputVente
    {
        public int clientId { get; set; }
        public List<int> articleIds { get; set; }
        public List<int> quantities { get; set; }
    }
}
