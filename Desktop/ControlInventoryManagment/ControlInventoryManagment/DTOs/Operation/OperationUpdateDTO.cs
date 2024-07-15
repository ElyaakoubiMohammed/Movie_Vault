using System;
using System.Collections.Generic;

namespace ControlInventoryManagment.DTOs;

    public class OperationUpdateDTO
    {
        public int Id { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public required List<int> ProductIds { get; set; }
    }

