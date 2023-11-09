﻿using Sale_Management.Articles;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Sale_Management.Ventes
{
    public class GetVenteDto : EntityDto<Guid>
    {
        public DateTime DateVente { get; set; }
        public Guid ClientId { get; set; }
        public string? clientName { get; set; }
        public int QtyTotal { get; set; }
        public double TotalAmount { get; set; }
        public bool IsValid { get; set; }
    }
   
}
