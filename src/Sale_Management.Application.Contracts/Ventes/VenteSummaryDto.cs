using System;
using System.Collections.Generic;
using System.Text;

namespace Sale_Management.Ventes
{
    public class VenteSummaryDto
    {
        public List<VenteDto> IndividualSales { get; set; } // List of individual sales
        public int TotalQuantity { get; set; }             // Total quantity for all sales
        public double TotalAmount { get; set; }
    }
}
