using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ProductionOrder
{
    public class PoInformationParameter
    {
        public string? PoNo { get; set; }
        public string? Item { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? ReceiptState { get; set; }
    }
}