using System;
using System.Collections.Generic;

namespace ControlInventoryManagment.DTOs.Operation
{
    public class OperationCreateDTO
    {
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public required List<ControlInventoryManagment.Models.Product> Products { get; set; }  // Assuming you are passing the IDs of the Products
    }
}
