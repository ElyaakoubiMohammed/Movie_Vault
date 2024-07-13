using System;
using System.Collections.Generic;
using  ControlInventoryManagment.Models;

namespace ControlInventoryManagment.DTOs.Operation
{
    public class OperationReadDTO
    {
        public int OperationId { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public required List<ControlInventoryManagment.Models.Product> Products { get; set; }
    }
}
