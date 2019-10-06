using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Model.Entity.Dto
{
    public class TransactionDto
    {
        public string Sku { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}
