using System;
using System.Collections.Generic;

namespace CircoParrotTools.Data.Models.DB
{
    public partial class FileTransactions
    {
        public int PurchaseOrderId { get; set; }
        public string LineNumber { get; set; }
        public int? ProductId { get; set; }
    }
}
