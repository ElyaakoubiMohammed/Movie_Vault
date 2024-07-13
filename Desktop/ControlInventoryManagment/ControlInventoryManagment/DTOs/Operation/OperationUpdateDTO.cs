using System;
using System.Collections.Generic;

namespace ControlInventoryManagment.DTOs.Operation
{
    public class OperationUpdateDTO
    {
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public required List<int> ProductIds { get; set; }  // Assuming you are passing the IDs of the Products
    }
}
