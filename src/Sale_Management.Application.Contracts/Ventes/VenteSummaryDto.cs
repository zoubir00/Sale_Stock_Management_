using System;
using System.Collections.Generic;
using System.Text;

namespace Sale_Management.Ventes
{
    public class VenteSummaryDto
    {
        public List<VenteDto> IndividualSales { get; set; }
        public int TotalQuantity { get; set; }             
        public double TotalAmount { get; set; }
    }
}
