using System;
using System.Collections.Generic;
using ControlInventoryManagment.Models;
namespace ControlInventoryManagment.DTOs;

    public class OperationReadDTO
    {
        public int OperationId { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
    }

