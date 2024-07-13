using System;
using System.Collections.Generic;

namespace ControlInventoryManagment.DTOs.Product;

    public class ProductCreateDTO
    {
        public required string Name { get; set; }
        public int CategorieId { get; set; }
        public int TagId { get; set; }
        public int QrId { get; set; }
        public required string SerialNum { get; set; }
        public required List<ControlInventoryManagment.Models.Operation> Operation { get; set; }  // IDs of the Operations
    }

