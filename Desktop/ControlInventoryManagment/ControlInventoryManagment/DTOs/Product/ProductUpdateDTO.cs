using System;
using System.Collections.Generic;

namespace ControlInventoryManagment.DTOs.Product
{
    public class ProductUpdateDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int IdCategorie { get; set; }
        public int IdTag { get; set; }
        public int IdQr { get; set; }
        public required  string SerialNum { get; set; }
        public  required List<int> OperationIds { get; set; }  // IDs of the Operations
    }
}
